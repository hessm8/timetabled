using System;
using System.Collections.Generic;
using System.Linq;
using System.Drawing;
using System.Windows.Forms;
using System.Diagnostics;
using System.Text.RegularExpressions;
using System.Globalization;
using Timetabled.Helpers;

namespace Timetabled {
    public abstract class GuiManager {
        public abstract void Initialize();
        public GuiManager(Control.ControlCollection _control, Storage _storage) {
            Controls = _control;
            Storage = _storage;
            Initialize();
        }
        protected TControl Access<TControl>(string name) where TControl : Control {
            var control = Controls.Find(name, false)[0];
            return control is TControl ? (TControl)control : null;
        }

        // Acessors
        public Control.ControlCollection Controls { get; }
        public Storage Storage { get; }
        
        // Additional things
        public CultureInfo culture = new CultureInfo("ru-RU");
        protected bool scheduleLoaded = false;
    }
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
            Storage.Schedules.OrderByDescending(k => k.Key);
            var scheduleKeys = Storage.Schedules.Keys.ToArray();
            var date = scheduleKeys.Length == 0 ? DateTime.Now : scheduleKeys[0];
            Calendar.SelectionStart = date.AddDays(7);

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

            if (scheduleLoaded) {
                UnloadSchedule(Dates.Previous, Groups.Latest);
                LoadSchedule();
            }
        }
        
        public void UnloadSchedule(DateTime date, string group) {
            var classes = Storage.Schedules;
            for (int dayIndex = 0; dayIndex < groupCount; dayIndex++) {
                var curDate = date.AddDays(dayIndex);
                bool hasDate = classes.ContainsKey(curDate);
                bool hasGroup = false;

                var groupLessons = new Dictionary<string, Lesson[]>();

                if (hasDate) {
                    groupLessons = classes[curDate];
                    hasGroup = classes[curDate].ContainsKey(group);
                }

                var lessonList = new Lesson[6];
                for (int lessonIndex = 0; lessonIndex < groupCount; lessonIndex++) {
                    var curLesson = new Lesson();

                    for (int type = 0; type < fieldCount; type++) {
                        curLesson[type] = AllFields[dayIndex, lessonIndex, type].Text;
                    }

                    lessonList[lessonIndex] = curLesson;
                }

                if (!hasGroup) groupLessons.Add(group, lessonList);
                else groupLessons[group] = lessonList;

                if (!hasDate) classes.Add(curDate, groupLessons);
                else classes[curDate] = groupLessons;
            }
        }
        public void LoadSchedule() {
            var classes = Storage.Schedules;
            for (int day = 0; day < groupCount; day++) {
                var curDate = Dates.Latest.AddDays(day);

                bool hasDate = classes.ContainsKey(curDate);
                bool hasGroup = false;
                var groupLessons = new Dictionary<string, Lesson[]>();
                var lessons = new Lesson[6];

                if (hasDate) {
                    groupLessons = classes[curDate];
                    hasGroup = classes[curDate].ContainsKey(Groups.Latest);
                }

                for (int lesson = 0; lesson < groupCount; lesson++) {
                    for (int type = 0; type < fieldCount; type++) {
                        string text = hasGroup ? groupLessons[Groups.Latest][lesson][type] : "";
                        AllFields[day, lesson, type].Text = text;
                    }
                }
            }
        }

        #endregion
    }
    public class DatabaseGui : GuiManager {
        public DatabaseGui(Control.ControlCollection _control, Storage _storage)
            : base(_control, _storage) { }
        public override void Initialize() {
            DataGrid = new DataGridView() {
                Location = new Point(70, 20),
                AllowUserToDeleteRows = true,
                AllowUserToAddRows = true,
                AllowUserToResizeColumns = false,
                AllowUserToResizeRows = false
            };
            DataGrid.Columns.Add("Datagrid1", " ");
            Controls.Add(DataGrid);

            SelectItem = Access<ListBox>("AddDataSelect");


            SelectItem.SelectedIndex = 0;

            selected = new State<string>(() => SelectItem.SelectedItem.ToString());

            Load();
            SelectItem.SelectedIndexChanged += OnIndexChange;            
        }

        private void OnIndexChange(object sender, EventArgs e) {
            SelectItem.SelectedIndexChanged -= OnIndexChange;

            selected.Update();
            Unload();
            Load();

            SelectItem.SelectedIndexChanged += OnIndexChange;
        }

        public DataGridView DataGrid { get; private set; }
        public ListBox SelectItem { get; private set; }
        private DataGridViewColumn Header => DataGrid.Columns[0];

        #region Mess

        State<string> selected;

        private void Load() {
            Header.HeaderText = selected.Latest;
            var unloadFrom = Storage.Data[selected.Latest];

            DataGrid.Rows.Clear();
            foreach (var i in unloadFrom) {
                DataGrid.Rows.Add(i);
            }
        }
        private void Unload() {
            var loadTo = Storage.Data[selected.Previous];

            loadTo.Clear();
            for (int i = 0; i < DataGrid.RowCount-1; i++) {
                var value = DataGrid[0, i].Value;            
                if (value != null) loadTo.Add(value.ToString());
            }
        }

        #endregion
    }
}