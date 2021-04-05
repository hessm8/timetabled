using System;
using System.Collections.Generic;
using System.Linq;
using System.Drawing;
using System.Windows.Forms;
using System.Diagnostics;
using System.Text.RegularExpressions;
using System.Globalization;

namespace Timetabled {
    public abstract class GuiManager {
        public GuiManager(Control.ControlCollection _control, Storage _storage) {
            Controls = _control;
            Storage = _storage;
            Initialize();
        }
        public abstract void Initialize();
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
        public MainGui(Control.ControlCollection _control, Storage _storage)
            : base(_control, _storage) {
            AllFields = new DataField[groupCount, groupCount, fieldCount];
            CreateSchedule();
        }
        public override void Initialize() {
            Calendar = Access<MonthCalendar>("SelectDate");

            GroupField = new DataField(this) {
                Location = new Point(25, 391),
                Size = new Size(200, 25),
                Text = Storage.Data["Группа"][0]
            };
            Controls.Add(GroupField);

            SelectLatestDate(); // Hidden hacky solution to a bug

            Calendar.DateChanged += Calendar_DateChanged;
        }

        #region Constants & Measurements

        const int fieldCount = 3;
        const int groupCount = 6;
        private readonly Size fieldSize = new Size(150, 20);
        private readonly Point scheduleOffset = new Point(300, 20);

        #endregion

        public MonthCalendar Calendar { get; private set; }
        public DataField GroupField { get; private set; }
        public DataField[,,] AllFields { get; }

        #region Schedule Creation

        public void CreateSchedule() {
            Controls.AddRange(CreateDayBox());
            LoadSchedule(datePrevious, GroupField.Text);
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
                var field = new DataField(this, (FieldType)type, (day, lesson)) {
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

        DateTime datePrevious;
        DateTime dateLatest;

        private void RefreshDates() {
            datePrevious = dateLatest;
            dateLatest = Calendar.SelectionStart;
        }
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

            datePrevious = Calendar.SelectionStart;
            dateLatest = Calendar.SelectionStart;
        }

        private void Calendar_DateChanged(object sender, DateRangeEventArgs e) {
            SelectEntireWeek();
            RefreshDates();

            var group = GroupField.Text;
            if (scheduleLoaded) {
                SaveSchedule(datePrevious, group);
                LoadSchedule(dateLatest, group);
            }
        }

        public void LoadSchedule(DateTime date, string group) {
            var classes = Storage.Schedules;
            for (int day = 0; day < groupCount; day++) {
                var curDate = date.AddDays(day);

                bool hasDate = classes.ContainsKey(curDate);
                bool hasGroup = false;
                var groupLessons = new Dictionary<string, Lesson[]>();
                var lessons = new Lesson[6];

                if (hasDate) {
                    groupLessons = classes[curDate];
                    hasGroup = classes[curDate].ContainsKey(group);
                }

                for (int lesson = 0; lesson < groupCount; lesson++) {
                    for (int type = 0; type < fieldCount; type++) {
                        string text = hasGroup ? groupLessons[group][lesson][type] : "";
                        AllFields[day, lesson, type].Text = text;
                    }
                }
            }
        }
        public void SaveSchedule(DateTime date, string group) {
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

            previous = SelectItem.SelectedItem.ToString();
            last = SelectItem.SelectedItem.ToString();

            Load();
            SelectItem.SelectedIndexChanged += OnIndexChange;
        }

        private void OnIndexChange(object sender, EventArgs e) {
            SelectItem.SelectedIndexChanged -= OnIndexChange;

            Refresh();
            Unload();
            Load();

            SelectItem.SelectedIndexChanged += OnIndexChange;
        }

        public DataGridView DataGrid { get; private set; }
        public ListBox SelectItem { get; private set; }
        private DataGridViewColumn Column => DataGrid.Columns[0];


        #region Mess

        string previous;
        string last;

        void Refresh() {
            previous = last;
            last = SelectItem.SelectedItem.ToString();
        }

        private void Load() {
            Column.HeaderText = last;
            var list = Storage.Data[last];

            DataGrid.Rows.Clear();
            foreach (var i in list) {
                DataGrid.Rows.Add(i);
            }
        }
        private void Unload() {
            var list = Storage.Data[previous];

            list.Clear();
            for (int i = 0; i < DataGrid.RowCount-1; i++) {
                var value = DataGrid.Rows[i].Cells[0].Value;                
                if (value != null) list.Add(value.ToString());
            }
        }

        #endregion
    }
}