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

namespace Timetabled.GUI {
    public class MainGui : GuiManager {
        public override void Initialize() {
            Calendar = Access<MonthCalendar>("SelectDate");

            GroupField = new ScheduleField(this) {
                Location = new Point(25, 391),
                Size = new Size(200, 25),
            };
            if (Storage.Data["Группа"].Count > 0) {
                GroupField.Text = Storage.Data["Группа"][0];
            }
            Controls.Add(GroupField);

            SelectLatestDate();

            Calendar.DateChanged += Calendar_DateChanged;
            GroupField.TextChanged += GroupField_TextChanged;
        }
        public MainGui(Control.ControlCollection _control, Storage _storage)
            : base(_control, _storage) {
            AllFields = new ScheduleField[groupCount, groupCount, fieldCount];
            CreateSchedule();
        }

        #region Constants & Measurements

        const int fieldCount = 3;
        const int groupCount = 6;
        private readonly Size fieldSize = new Size(150, 20);
        private readonly Point scheduleOffset = new Point(300, 20);

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

                var dayString = culture.DateTimeFormat.GetDayName((DayOfWeek)day + 1);
                dayString = culture.TextInfo.ToTitleCase(dayString);

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

        #region Date & Setting Schedule 

        public State<DateTime> Dates { get; private set; }
        public State<string> Groups { get; private set; }

        private void SelectEntireWeek() {
            Calendar.DateChanged -= Calendar_DateChanged;

            var dayWeek = Calendar.SelectionStart.DayOfWeek;
            int s = (int)dayWeek;
            if (dayWeek == DayOfWeek.Sunday) s += 7;

            var d = Calendar.SelectionStart;
            Calendar.SelectionStart = d.AddDays(1 - s);
            Calendar.SelectionEnd = d.AddDays(7 - s);

            Calendar.DateChanged += Calendar_DateChanged;
        }
        private void SelectLatestDate() {
            var ordered = Storage.Schedules.Keys.OrderByDescending(k => k).ToArray();
            var date = ordered.Length == 0 ? DateTime.Now : ordered[0];
            Calendar.SelectionStart = date;//.AddDays(7);

            SelectEntireWeek();

            Dates = new State<DateTime>(() => Calendar.SelectionStart);
            Groups = new State<string>(() => GroupField.Text);
        }

        private void GroupField_TextChanged(object sender, EventArgs e) {
            if (scheduleLoaded && Storage.Data["Группа"].Contains(GroupField.Text)) {
                Groups.Update();
                UnloadSchedule(Dates.Previous, Groups.Previous);
                LoadSchedule();
            }
        }
        private void Calendar_DateChanged(object sender, DateRangeEventArgs e) {
            SelectEntireWeek();
            Dates.Update();

            if (scheduleLoaded && Groups.Latest != "") {
                UnloadSchedule(Dates.Previous, Groups.Latest);
                LoadSchedule();
            }
        }
        
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
                for (int l = groupCount-1; l >= 0; l--) {
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

            // Clear all fields
            foreach (var f in AllFields) f.Text = "";

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

        public void OpenSchedule() {
            UnloadSchedule(Dates.Latest, Groups.Latest);
            var serializedString = Storage.SerializeOnDate(Dates.Latest);

            if (serializedString == null) return;

            var file = "lib\\viewer.html";
            var args = "?schedule=" + Uri.EscapeDataString(serializedString)
                + "&date=" + Dates.Latest.ToShortDateString();

            if (Storage.Settings.DefaultBrowser == null) {
                Storage.Settings.CheckDefaultBrowser(file);
            }

            Process.Start(Storage.Settings.DefaultBrowser, file + args);
        }

        #endregion
    }
}