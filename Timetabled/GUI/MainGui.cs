using System;
using System.Collections.Generic;
using System.Linq;
using System.Drawing;
using System.Windows.Forms;
using System.Diagnostics;
using System.Text.RegularExpressions;
using System.Globalization;
using Timetabled.Helpers;
using Timetabled.Data;
using System.IO;
using Timetabled.Forms;
using System.ComponentModel;

namespace Timetabled.GUI {
    public class MainGui : GuiManager {
        public override void Initialize() {
            Calendar = Access<MonthCalendar>("SelectDate");
            var testLoc = Access<Control>("TestLoc");
            scheduleOffset = testLoc.Location;
            testLoc.Dispose();

            var placeholderGroup = Access<ComboBox>("placeholderGroup");
            GroupField = new ScheduleField(this) {
                Location = placeholderGroup.Location,
                Size = placeholderGroup.Size
            };
            placeholderGroup.Dispose();

            Controls.Add(GroupField);
            //SelectFirstGroup();
            SelectLatestDate();

            Dates = new State<DateTime>(() => Calendar.SelectionStart);
            Groups = new State<string>(() => GroupField.Text);

            var calSize = Calendar.Size;

            if (CultureInfo.InstalledUICulture.Name == "ru-RU") {
                Calendar.Location = new Point(Calendar.Location.X + Calendar.Size.Width / 5, Calendar.Location.Y);
            }

            var start = Calendar.Location;
            var arrowSize = new Size(21, 30);
            Controls.Add(leftArrow = new Button() {
                Location = start,
                Size = arrowSize,                
                Text = "◀"                
            });
            Controls.Add(rightArrow = new Button() {
                Location = new Point(start.X + calSize.Width - arrowSize.Width, start.Y),
                Size = arrowSize,
                Text = "▶"
            });

            leftArrow.BringToFront();
            rightArrow.BringToFront();

            leftArrow.Click += LeftArrow_Click;
            rightArrow.Click += RightArrow_Click;

            
            Calendar.DateChanged += OnDateChange;
            GroupField.TextChanged += OnGroupChange;
            //Calendar.MouseDown += Calendar_MouseDown;
        }

        public void SelectFirstGroup() {
            if (Storage.Data.groups.Count > 0) {
                GroupField.Text = Storage.Data.groups[0];
            }
        }

        private void RightArrow_Click(object sender, EventArgs e) {
            Calendar.SelectionStart = Dates.Latest.AddDays(7);
            SelectEntireWeek();
        }

        private void LeftArrow_Click(object sender, EventArgs e) {
            Calendar.SelectionStart = Dates.Latest.AddDays(-7);
            SelectEntireWeek();
        }

        Button rightArrow;
        Button leftArrow;

        //private void Calendar_MouseDown(object sender, MouseEventArgs e) {
        //    // Use to cancel DateChanged thing
        //    if (e.Y < 24) {
        //        Calendar.DateChanged -= OnDateChange;
        //        if (e.X < 20) { } //Left arrow
        //        else if (e.X > 205) { } // Right arrow
        //        else { } // middle
        //    }
        //}

        public MainGui(Control.ControlCollection _control)
            : base(_control) {
            AllFields = new ScheduleField[groupCount, groupCount, fieldCount];
            CreateSchedule();
        }

        #region Constants & Measurements

        const int fieldCount = 3;
        const int groupCount = 6;
        private readonly Size fieldSize = new Size(150, 20);
        //private readonly Point scheduleOffset = new Point(300, 20);
        private Point scheduleOffset { get; set; }

        #endregion

        public MonthCalendar Calendar { get; private set; }
        public ScheduleField GroupField { get; private set; }
        public ScheduleField[,,] AllFields { get; }

        #region Schedule Creation

        public void CreateSchedule() {
            Controls.AddRange(CreateDayBox());
            LoadSchedule();
            scheduleLoaded = true;
        }
        private Control[] CreateDayBox() {
            var elements = new Control[groupCount];

            for (int day = 0; day < groupCount; day++) {
                int distance = 200;

                var dayString = Culture.DateTimeFormat.GetDayName((DayOfWeek)day + 1);
                dayString = Culture.TextInfo.ToTitleCase(dayString);

                var dayGroup = new GroupBox() {
                    Text = dayString,
                    Location = new Point(
                        day * distance + scheduleOffset.X,
                        scheduleOffset.Y),
                    Size = new Size(fieldSize.Width + 42, 465)
                };

                dayGroup.Controls.AddRange(CreateLessonBox(day));

                elements[day] = dayGroup;
            }

            return elements;
        }
        private Control[] CreateLessonBox(int day) {
            var elements = new Control[groupCount];

            for (int lesson = 0; lesson < groupCount; lesson++) {
                int lessonOffset = lesson * (fieldSize.Height * fieldCount + 10);

                var lessonPanel = new Panel() {
                    Location = new Point(fieldSize.Height, lessonOffset + 30),
                    Size = new Size( // Thick border around fields, yay
                        fieldSize.Width + 2,
                        fieldCount * fieldSize.Height + 3),
                    BorderStyle = BorderStyle.FixedSingle
                };
                lessonPanel.Controls.AddRange(CreateLessonFields(day, lesson));

                elements[lesson] = lessonPanel;
            }

            return elements;
        }
        private ComboBox[] CreateLessonFields(int day, int lesson) {
            var elements = new ComboBox[fieldCount];

            for (int type = 0; type < fieldCount; type++) {
                var field = new ScheduleField(this, (FieldType)type, (day, lesson)) {
                    Location = new Point(0, fieldSize.Height * type),
                    Size = fieldSize
                };
                elements[type] = field;
                AllFields[day, lesson, type] = field;
            }
            return elements;
        }

        #endregion

        #region Schedule Loading 

        public void UnloadSchedule(DateTime date, string group) {
            var classes = Storage.Schedules;

            for (int d = 0; d < groupCount; d++) {
                var lessons = new List<Lesson>();
                var fieldsBlank = new List<bool>();

                // Read all lessons
                for (int l = 0; l < groupCount; l++) {
                    var lesson = new Lesson();
                    bool blank = true;

                    // Read all type fields
                    for (int t = 0; t < fieldCount; t++) {
                        var text = AllFields[d, l, t].Text;
                        lesson[t] = text;
                        // If all fields are blank
                        blank = blank && text == "";
                    }

                    lessons.Add(lesson);
                    fieldsBlank.Add(blank);
                }

                // Remove empty lessons at end
                for (int l = groupCount - 1; l >= 0; l--) {
                    if (fieldsBlank[l]) lessons.RemoveAt(l);
                    else break;
                }

                if (lessons.Count == 0) continue;

                var day = date.AddDays(d);

                if (!classes.ContainsKey(day)) {
                    classes[day] = new Dictionary<string, Lesson[]>();
                }

                var arr = lessons.ToArray();
                if (!classes[day].ContainsKey(group)) {
                    classes[day].Add(group, arr);
                } else classes[day][group] = arr;
            }
        }
        public void LoadSchedule() {
            var classes = Storage.Schedules;
            var group = Groups.Latest;

            ClearFields();

            for (int d = 0; d < groupCount; d++) {
                var day = Dates.Latest.AddDays(d);

                // Skip if no day or group
                if (!classes.ContainsKey(day)) continue;
                if (!classes[day].ContainsKey(group)) continue;

                var lessons = classes[day][group];
                for (int l = 0; l < lessons.Length; l++) {
                    for (int t = 0; t < fieldCount; t++) {
                        AllFields[d, l, t].Text = lessons[l][t];
                    }
                }
            }
        }
        public bool OpenSchedule(int dayOfweek, bool inBrowser) {
            UnloadSchedule(Dates.Latest, Groups.Latest);
            var selectedDate = Dates.Latest.AddDays(dayOfweek);
            var serializedString = Storage.SerializeOnDate(selectedDate);

            if (serializedString == null) {
                var msg = MessageBox.Show("Расписание на выбранную дату не заполнено", "Ошибка");
                return false;
            }

            var file = "lib\\viewer.html";
            var args = "?schedule=" + Uri.EscapeDataString(serializedString)
                + "&date=" + Dates.Latest.ToShortDateString();

            if (Storage.Settings.DefaultBrowser == null) {
                Storage.Settings.CheckDefaultBrowser(file);
            }

            var fullPath = new FileInfo(file).FullName;
            var fileURL = new Uri(fullPath).AbsoluteUri;

            var link = fileURL + args;
            if (!inBrowser) {
                var viewer = new ScheduleViewer(link);
            } else Process.Start(Storage.Settings.DefaultBrowser, link);

            return true;
        }



        public void ClearSchedule() {           
            Storage.Schedules.Clear();
            for (int i = 0; i < 4; i++) {
                Storage.Data[i].Clear();
            }
            ClearFields();
            GroupField.Text = "";
        }

        private void ClearFields() {
            foreach (var f in AllFields) f.Text = "";
        }

        public void FieldsEnable(bool state) {
            foreach (var f in AllFields) f.Enabled = state;
        }

        #endregion

        #region Schedule Commands & Events

        public State<DateTime> Dates { get; private set; }
        public State<string> Groups { get; private set; }

        private void SelectEntireWeek() {
            Calendar.DateChanged -= OnDateChange;

            var dayWeek = Calendar.SelectionStart.DayOfWeek;
            int s = (int)dayWeek;
            if (dayWeek == DayOfWeek.Sunday) s += 7;

            var d = Calendar.SelectionStart;
            Calendar.SelectionStart = d.AddDays(1 - s);
            Calendar.SelectionEnd = d.AddDays(7 - s);

            Calendar.DateChanged += OnDateChange;
        }

        private void SelectLatestDate() {
            var date = DateTime.Now;
            if (Storage.Schedules.Count > 0) {
                date = Storage.Schedules.Keys
                    .OrderByDescending(k => k)
                    .ToArray()[0];
            }

            Calendar.SelectionStart = date;
            SelectEntireWeek();            
        }

        private void OnGroupChange(object sender, EventArgs e) {
            if (scheduleLoaded && Storage.Data["Группа"].Contains(GroupField.Text)) {
                Groups.Update();
                UnloadSchedule(Dates.Previous, Groups.Previous);
                LoadSchedule();
            }
        }
        private void OnDateChange(object sender, DateRangeEventArgs e) {
            SelectEntireWeek();

            Dates.Update();

            if (scheduleLoaded && Groups.Latest != "") {
                UnloadSchedule(Dates.Previous, Groups.Latest);
                LoadSchedule();
            }
        }

        #endregion
    }
}