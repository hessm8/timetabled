using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using System.Diagnostics;

namespace Timetabled {
    public class GuiManager {
        private Control.ControlCollection controls;
        private Storage storage;
        private MonthCalendar selectedDate;
        private DateTime preDate;
        private ComboBox selectedGroup;
        #region Constants & Measurements

        const int fieldCount = 3;
        const int groupCount = 6;
        const int maxFields = 1000;//groupCount * groupCount * fieldCount;
        private readonly Size fieldSize = new Size(150, 20);
        private readonly Point scheduleOffset = new Point(300, 20);

        #endregion
        public GuiManager(Control.ControlCollection _control, Storage _storage) {
            controls = _control;
            storage = _storage;
            selectedDate = (MonthCalendar)controls.Find("SelectDate", true)[0];
            selectedGroup = (ComboBox)controls.Find("groupSelect", true)[0];

            selectedDate.DateChanged += new DateRangeEventHandler(
                (sender, e) => OnDateChange(sender, e));

            selectedGroup.DropDown += new EventHandler((sender, e) => {
                selectedGroup.Items.Clear();
                selectedGroup.Items.AddRange(storage.data.groups.ToArray());
            });

            SelectEntireWeek();
            preDate = selectedDate.SelectionStart;
        }
        bool scheduleLoaded = false;

        private List<ComboBox> allFields = new List<ComboBox>(maxFields);
        public ComboBox AccessField(int day, int lesson, int type) {
            return allFields[(day * groupCount * fieldCount)
                + (lesson * fieldCount) + type];
        }


        DateTime datePrevious;
        DateTime dateLatest;
        private void RefreshDates() {
            datePrevious = dateLatest;
            dateLatest = selectedDate.SelectionStart;
        }
        private void SelectEntireWeek() {
            int s = (int)selectedDate.SelectionStart.DayOfWeek;
            var d = selectedDate.SelectionStart;
            selectedDate.SelectionStart = d.AddDays(1 - s);
            selectedDate.SelectionEnd = d.AddDays(7 - s);
        }

        private void OnDateChange(object sender, DateRangeEventArgs e) {
            SelectEntireWeek();
            RefreshDates();

            var group = selectedGroup.Text;
            if (scheduleLoaded) {
                SaveSchedule(datePrevious, group);
                //LoadSchedule(dateLatest, group);
            }
        }

        private void SaveSchedule(DateTime date, string group) {
            var list = storage.schedules;
            //Debug.WriteLine("BRUH");
            for (int day = 0; day < groupCount; day++) {
                var curDate = date.AddDays(day);
                if (!list.ContainsKey(date)) {
                    list.Add(date, new Dictionary<string, Lesson[]>());
                }

                var daySchedule = list[date];

                var lessons = new Lesson[groupCount];
                for (int lesson = 0; lesson < groupCount; lesson++) {
                    var texts = new string[fieldCount];
                    for (int type = 0; type < fieldCount; type++) {
                        lessons[lesson][type] = AccessField(day, lesson, type).Text ?? "";                        
                    }
                }
                if (!daySchedule.ContainsKey(group)) daySchedule.Add(group, lessons);
                else daySchedule[group] = lessons;
            }

            //storage.schedules.Add(date, new Dictionary<string, Lesson[]>() {
            //    ["ПКС-81"] = new Lesson[] {
            //        new Lesson("Математика", "Монголов", "404"),
            //        new Lesson("Русский язык", "Маратов", "205")
            //    },
            //    ["Брух-55"] = new Lesson[] {
            //        new Lesson("АКС", "Обама", "111"),
            //        new Lesson("Искусство подтирания", "Крупенко", "222")
            //    }
            //});
        }



        //public void SaveSchedule() {
        //    var group = selectedGroup.Text;
        //    var date = selectedDate.SelectionStart;
        //    for (int day = 0; day < groupCount; day++) {
        //        var curDate = date.AddDays(day);
        //        storage.schedules.Add(curDate, new Dictionary<string, Lesson[]>());

        //        var daySchedule = storage.schedules[curDate];
        //        //if (daySchedule == null) daySchedule = new Dictionary<string, Lesson[]>();

        //        var lessons = new Lesson[groupCount];
        //        for (int lesson = 0; lesson < groupCount; lesson++) {
        //            var texts = new string[fieldCount];
        //            for (int type = 0; type < fieldCount; type++) {
        //                lessons[lesson][type] = AccessField(day, lesson, type).Text ?? "";
        //            }
        //        }
        //        daySchedule.Add(group, lessons);
        //    }
        //}

        public void LoadSchedule(DateTime date, string group) {
            for (int day = 0; day < groupCount; day++) {
                var lessons = storage.schedules[date.AddDays(day)][group];
                for (int lesson = 0; lesson < groupCount; lesson++) {
                    var texts = new string[fieldCount];
                    for (int type = 0; type < fieldCount; type++) {
                        AccessField(day, lesson, type).Text = lessons[lesson][type];
                    }
                }
            }
        }

        //public void LoadSchedule() {
        //    var group = "ПКС-69";
        //    var date = selectedDate.SelectionStart;
        //    for (int day = 0; day < groupCount; day++) {
        //        var lessons = storage.schedules[date.AddDays(day)][group];
        //        for (int lesson = 0; lesson < groupCount; lesson++) {
        //            var texts = new string[fieldCount];
        //            for (int type = 0; type < fieldCount; type++) {
        //                AccessField(day, lesson, type).Text = lessons[lesson][type];
        //            }
        //        }
        //    }
        //}


        #region Schedule Creation

        public void CreateSchedule() {
            controls.AddRange(CreateDayBox());
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

                dayGroup.Controls.AddRange(CreateLessonBox());

                elements[day] = dayGroup;
            }

            return elements;
        }
        private Control[] CreateLessonBox() {
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
                lessonPanel.Controls.AddRange(CreateLessonFields());

                elements[lesson] = lessonPanel;
            }

            return elements;
        }
        private ComboBox[] CreateLessonFields() {
            var elements = new ComboBox[fieldCount];

            for (int i = 0; i < fieldCount; i++) {
                var field = new ComboBox() {
                    Location = new Point(0, fieldSize.Height * i),
                    Size = fieldSize,
                };
                elements[i] = AssignField(field, (FieldType)i);
            }
            return elements;
        }
        private ComboBox AssignField(ComboBox box, FieldType type) {
            var category = fieldString[type];
            var data = storage.data[category];

            // Database check
            box.Leave += new EventHandler((sender, e) => {
                if (box.Text == "") return;
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
            // Autocomplete
            box.KeyDown += new KeyEventHandler((sender, e) => {
                if (e.KeyCode == Keys.Enter) {
                    box.Text = data.Find(t => t.ToLower()
                    .Contains(box.Text.ToLower())) ?? box.Text;
                }
            });

            allFields.Add(box);
            return box;
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
            Room
        }
        private Dictionary<FieldType, string> fieldString => new Dictionary<FieldType, string>() {
            [FieldType.Subject] = "Дисциплина",
            [FieldType.Teacher] = "Преподаватель",
            [FieldType.Room] = "Аудитория"
        };

        #endregion
    }
}