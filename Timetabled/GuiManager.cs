using System;
using System.Collections.Generic;
using System.Linq;
using System.Drawing;
using System.Windows.Forms;
using System.Diagnostics;
using System.Text.RegularExpressions;
using System.Globalization;

namespace Timetabled {
    public class GuiManager {
        public Control.ControlCollection Controls { get; }
        public Storage Storage { get; }
        public MonthCalendar Calendar { get; }
        public DataField GroupField { get; }

        public DataField[,,] allFields = new DataField[groupCount, groupCount, fieldCount];

        private bool scheduleLoaded = false;

        private CultureInfo culture = new CultureInfo("ru-RU");

        #region Constants & Measurements

        const int fieldCount = 3;
        const int groupCount = 6;
        private readonly Size fieldSize = new Size(150, 20);
        private readonly Point scheduleOffset = new Point(300, 20);

        #endregion
        public GuiManager(Control.ControlCollection _control, Storage _storage) {
            Controls = _control;
            Storage = _storage;

            Calendar = (MonthCalendar)Controls.Find("SelectDate", false)[0];

            GroupField = new DataField(this) {
                Location = new Point(25, 391),
                Size = new Size(200, 25),
                //Text = Storage.Data["Группа"][0]
            };
            Controls.Add(GroupField);

            SelectLatestDate(); // Hidden hacky solution to a bug

            onDateChange = (sender, e) => OnDateChange(sender, e);
            Calendar.DateChanged += onDateChange;
        }

        #region Date & Setting Schedule 

        DateTime datePrevious;
        DateTime dateLatest;

        private void RefreshDates() {
            datePrevious = dateLatest;
            dateLatest = Calendar.SelectionStart;
        }
        private void SelectEntireWeek() {
            Calendar.DateChanged -= onDateChange;

            var dayWeek = Calendar.SelectionStart.DayOfWeek;
            int s = (int)dayWeek;
            if (dayWeek == DayOfWeek.Sunday) s += 7;

            var d = Calendar.SelectionStart;
            Calendar.SelectionStart = d.AddDays(1 - s);
            Calendar.SelectionEnd = d.AddDays(7 - s);

            Calendar.DateChanged += onDateChange;
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

        DateRangeEventHandler onDateChange;

        private void OnDateChange(object sender, DateRangeEventArgs e) {
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
                        allFields[day, lesson, type].Text = text;
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
                        curLesson[type] = allFields[dayIndex, lessonIndex, type].Text;
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

        #region Schedule Creation

        public GuiManager CreateSchedule() {
            Controls.AddRange(CreateDayBox());
            LoadSchedule(datePrevious, GroupField.Text);
            scheduleLoaded = true;
            return this;
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
                allFields[day, lesson, type] = field;
            }
            return elements;
        }

        #endregion
    }
}