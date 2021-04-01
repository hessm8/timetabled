using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace Timetabled {
    public class GuiManager {
        private Control.ControlCollection controls;
        private Storage storage;
        #region Constants & Measurements

        const int fieldCount = 3;
        const int groupCount = 6;
        const int maxFields = groupCount * groupCount * fieldCount;
        private readonly Size fieldSize = new Size(150, 20);
        private readonly Point scheduleOffset = new Point(100, 150);

        #endregion
        public GuiManager(Control.ControlCollection _control, Storage _storage) {
            controls = _control;
            storage = _storage;
        }

        private List<ComboBox> allFields = new List<ComboBox>(maxFields);
        public ComboBox AccessField(int day, int lesson, int type) 
            => allFields[day * lesson * type];


        #region Schedule Creation

        public void CreateSchedule() {
            controls.AddRange(CreateDayBox());
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
        private enum FieldType {
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