using System;
using System.Collections.Generic;
using System.Linq;
using System.Drawing;
using System.Windows.Forms;
using System.Diagnostics;
using System.Text.RegularExpressions;

namespace Timetabled {
    public class GuiManager {
        private Control.ControlCollection controls;
        private Storage storage;
        private MonthCalendar calendar;
        private ComboBox groupField;
        #region Constants & Measurements

        const int fieldCount = 3;
        const int groupCount = 6;
        private readonly Size fieldSize = new Size(150, 20);
        private readonly Point scheduleOffset = new Point(300, 20);

        #endregion
        public GuiManager(Control.ControlCollection _control, Storage _storage) {
            controls = _control;
            storage = _storage;
            calendar = (MonthCalendar)controls.Find("SelectDate", false)[0];
            groupField = (ComboBox)controls.Find("groupSelect", false)[0];

            SelectLatestDate(); // Hidden hacky solution to a bug

            onDateChange = (sender, e) => OnDateChange(sender, e);
            calendar.DateChanged += onDateChange;

            AssignField(ref groupField, FieldType.Group);
            groupField.Text = storage.data.groups[0];
        }

        bool scheduleLoaded = false;
        private ComboBox[,,] allFields = new ComboBox[groupCount, groupCount, fieldCount];

        #region Date & Setting Schedule 

        DateTime datePrevious;
        DateTime dateLatest;

        private void RefreshDates() {
            datePrevious = dateLatest;
            dateLatest = calendar.SelectionStart;
        }
        private void SelectEntireWeek() {
            calendar.DateChanged -= onDateChange;

            var dayWeek = calendar.SelectionStart.DayOfWeek;
            int s = (int)dayWeek;
            if (dayWeek == DayOfWeek.Sunday) s += 7;

            var d = calendar.SelectionStart;
            calendar.SelectionStart = d.AddDays(1 - s);
            calendar.SelectionEnd = d.AddDays(7 - s);

            calendar.DateChanged += onDateChange;
        }
        private void SelectLatestDate() {
            storage.schedules.OrderByDescending(k => k.Key);
            var scheduleKeys = storage.schedules.Keys.ToArray();
            var date = scheduleKeys.Length == 0 ? DateTime.Now : scheduleKeys[0];
            calendar.SelectionStart = date.AddDays(7);

            SelectEntireWeek();

            datePrevious = calendar.SelectionStart;
            dateLatest = calendar.SelectionStart;
        }

        DateRangeEventHandler onDateChange;

        private void OnDateChange(object sender, DateRangeEventArgs e) {
            SelectEntireWeek();
            RefreshDates();

            var group = groupField.Text;
            if (scheduleLoaded) {
                SaveSchedule(datePrevious, group);
                LoadSchedule(dateLatest, group);
            }
        }
        private void SaveSchedule(DateTime date, string group) {
            var classes = storage.schedules;
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
        public void LoadSchedule(DateTime date, string group) {
            var classes = storage.schedules;
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

        #endregion

        #region Schedule Creation

        public void CreateSchedule() {
            controls.AddRange(CreateDayBox());
            LoadSchedule(datePrevious, groupField.Text);
            scheduleLoaded = true;
        }
        private Control[] CreateDayBox() {
            var elements = new Control[groupCount];

            for (int day = 0; day < groupCount; day++) {
                int distance = 200;

                var dayGroup = new GroupBox() {
                    Name = $"dayGroup{day + 1}",
                    Text = dayString[day],
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
                var field = new ComboBox() {
                    Location = new Point(0, fieldSize.Height * type),
                    Size = fieldSize,
                };
                AssignField(ref field, (FieldType)type, (day, lesson, type));
                elements[type] = field;
                allFields[day, lesson, type] = field;
            }
            return elements;
        }
        private void AssignField(ref ComboBox field, FieldType type,
            (int day, int lesson, int type) pos = default) {
            var category = fieldString[type];
            var data = storage.data[category];
            var box = field;

            // Database and text field check on leave
            box.Leave += new EventHandler((sender, e) => {
                // Skip if empty
                if (box.Text == "") return;
                // Leave only Russian and special characters
                box.Text = Regex.Replace(box.Text, @"[^-.ЁёА-Яа-я0-9\s]", "");
                // Ask to add if item is not in database
                if (!data.Contains(box.Text)) {
                    var popupResult = MessageBox.Show(
                        $"Элемента нет в списке [{category}],\nДобавить его в базу данных?",
                        "Ошибка данных", MessageBoxButtons.YesNo);

                    if (popupResult == DialogResult.Yes) {
                        data.Add(box.Text);
                    } else box.Text = "";
                }
            });
            // Refresh the list on dropdown
            box.DropDown += new EventHandler((sender, e) => {
                box.Items.Clear();
                box.Items.AddRange(data.ToArray());
            });
            // Key binds
            box.KeyDown += new KeyEventHandler((sender, e) => {      
                switch (e.KeyCode) {
                    // Autocomplete
                    case Keys.Enter:
                        if (!e.Alt) {
                            // Based on input
                            box.Text = data.Find(t => t.ToLower()
                            .Contains(box.Text.ToLower())) ?? box.Text;
                        } else {
                            // On random
                            var randomIndex = new Random().Next(data.Count);
                            box.Text = data[randomIndex];
                        }
                        break;
                    // Random item
                    case Keys.Tab:
                        if (e.Control) {
                            allFields[pos.day + 1, pos.lesson, pos.type].Focus();
                        }
                        break;

                    //break;                    
                }
            });
        }

        #endregion

        #region String Conversions

        private readonly string[] dayString = {
            "Понедельник",
            "Вторник",
            "Среда",
            "Четверг",
            "Пятница",
            "Суббота",
            "Воскресенье"
        };
        public enum FieldType {
            Subject,
            Teacher,
            Room,
            Group
        }
        private Dictionary<FieldType, string> fieldString => new Dictionary<FieldType, string>() {
            [FieldType.Subject] = "Дисциплина",
            [FieldType.Teacher] = "Преподаватель",
            [FieldType.Room] = "Аудитория",
            [FieldType.Group] = "Группа"
        };

        #endregion
    }
}