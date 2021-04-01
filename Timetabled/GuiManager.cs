using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace Timetabled {
    public class GuiManager {
        Control.ControlCollection controls;
        Storage storage;
        Point offset => new Point(200, 20);
        Size comboSize => new Size(150, 20);

        public GuiManager(Control.ControlCollection _control, Storage _storage) {
            controls = _control;
            storage = _storage;
        }

        //Dictionary<string, List<ComboBox>> fields = new Dictionary<string, List<ComboBox>>();
        //Dictionary<string, List<string>> fields = new Dictionary<string, List<string>>();


        public void CreateSchedule() {
            controls.AddRange(CreateDayBox());
        }

        private Control[] CreateDayBox() {
            var elements = new Control[6];

            for (int day = 0; day < 6; day++) {
                var dayGroup = new GroupBox() {
                    Name = $"dayGroup{day + 1}",
                    Text = $"День {day + 1}",
                    Location = new Point(day * offset.X + 30, 30),
                    Size = new Size(192, 475)
                };

                dayGroup.Controls.AddRange(CreateLessonBox());

                elements[day] = dayGroup;
            }

            return elements;
        }

        private Control[] CreateLessonBox() {
            var elements = new Control[6]; 

            for (int lesson = 0; lesson < 6; lesson++) {
                int lessonOffset = lesson * 70;

                var lessonPanel = new Panel() {
                    Location = new Point(20, 30 + lessonOffset),
                    Size = new Size(152, 63),
                    BorderStyle = BorderStyle.FixedSingle
                };
                lessonPanel.Controls.AddRange(CreateLessonFields());

                elements[lesson] = lessonPanel;
            }

            return elements;
        }

        private Control[] CreateLessonFields() {
            var subjectBox = new ComboBox() {
                //Name = $"subjectBox({dayIndex + 1}{lessonIndex + 1})",
                Location = new Point(0, 0),
                Size = comboSize
            };
            var teacherBox = new ComboBox() {
                //Name = $"teacherBox({dayIndex + 1}{lessonIndex + 1})",
                Location = new Point(0, offset.Y),
                Size = comboSize,
            };
            var roomBox = new ComboBox() {
                //Name = $"roomBox({dayIndex + 1}{lessonIndex + 1})",
                Location = new Point(0, offset.Y * 2),
                Size = comboSize
            };
            return new Control[] { subjectBox, teacherBox, roomBox };
        }




        //public void CreateSchedule() {
        //    //fields.Add("Преподаватель", new List<string>());
        //    //fields.Add("Дисциплина", new List<string>());
        //    //fields.Add("Аудитория", new List<string>());

        //    //fields.Add("Преподаватель", new List<ComboBox>());
        //    //fields.Add("Дисциплина", new List<ComboBox>());
        //    //fields.Add("Аудитория", new List<ComboBox>());

        //    for (int dayIndex = 0; dayIndex < 6; dayIndex++) {
        //        var pos = new Point(dayIndex * offset.X + 30, 30);
        //        var size = new Size(192, 475);
        //        var comboSize = new Size(150, 20);

        //        var dayGroup = new GroupBox() {
        //            Name = $"dayGroup{dayIndex + 1}",
        //            Text = $"День {dayIndex + 1}",
        //            Location = pos,
        //            Size = size
        //        };

        //        for (int lessonIndex = 0; lessonIndex < 6; lessonIndex++) {
        //            int lessonOffset = lessonIndex * 70;

        //            var lessonPanel = new Panel() {
        //                Location = new Point(20, pos.Y + lessonOffset),
        //                Size = new Size(152, 63),
        //                BorderStyle = BorderStyle.FixedSingle                        
        //            };

        //            var subjectBox = new ComboBox() {
        //                Name = $"subjectBox({dayIndex + 1}{lessonIndex + 1})",
        //                Location = new Point(0, 0),
        //                Size = comboSize
        //            };
        //            //fields["Дисциплина"].Add(subjectBox.Name);

        //            var teacherBox = new ComboBox() {
        //                Name = $"teacherBox({dayIndex + 1}{lessonIndex + 1})",
        //                Location = new Point(0, offset.Y),
        //                Size = comboSize,
        //            };
        //            //fields["Преподаватель"].Add(subjectBox.Name);

        //            var roomBox = new ComboBox() {
        //                Name = $"roomBox({dayIndex + 1}{lessonIndex + 1})",
        //                Location = new Point(0, offset.Y * 2),
        //                Size = comboSize
        //            };
        //            //fields["Аудитория"].Add(subjectBox.Name);

        //            //roomBox.Leave += new EventHandler((sender, e) => {

        //            //    if (roomBox.Text == "") return;
        //            //    if (!storage.data.rooms.Contains(roomBox.Text)) {

        //            //        var res = MessageBox.Show("Такой аудитории не существует,\nхотите ли вы добавить ее?",
        //            //            "Ошибка данных", MessageBoxButtons.YesNo);
        //            //        if (res == DialogResult.Yes) {
        //            //            storage.data.rooms.Add(roomBox.Text);
        //            //        } else roomBox.Text = "";
        //            //    }
        //            //});               


        //            lessonPanel.Controls.AddRange(new Control[] {
        //                subjectBox, teacherBox, roomBox
        //            });

        //            dayGroup.Controls.Add(lessonPanel);
        //        }

        //        controls.Add(dayGroup);
        //    }

        //    //controls.Find(fields["Аудитория"][0], true)[0].Dispose();
        //    //controls.Find(fields["Аудитория"][1], true)[0].Dispose();

        //    //controls.Find(fields["Преподаватель"][3], true)[0].Dispose();
        //    //controls.Find(fields["Преподаватель"][5], true)[0].Dispose();
        //    //InitFields();
        //}

        //public void InitFields() {
        //    foreach (var pair in fields) {
        //        foreach (var name in pair.Value) {
        //            var combo = controls.Find(name, true)[0];
        //            var text = combo.Text;

        //            //controls.RemoveByKey(name);

        //            //controls.Remove(combo);

        //            combo.Leave += new EventHandler((sender, e) => {
        //                if (text == "") return;
        //                if (!storage.data[pair.Key].Contains(text)) {

        //                    var res = MessageBox.Show($"Такой записи типа {pair.Key} нет в базе данных.\nДобавить?",
        //                        "Ошибка данных", MessageBoxButtons.YesNo);
        //                    if (res == DialogResult.Yes) {
        //                        storage.data.rooms.Add(text);
        //                    } else text = "";
        //                }
        //            });

        //            //controls.Remove(combo);
        //        }
        //    }
        //}

        //public void InitializeFields() {
        //    foreach (var pair in fields) {
        //        foreach (var combo in pair.Value) {
        //            var text = combo.Text;

        //            combo.Leave += new EventHandler((sender, e) => {
        //                if (text == "") return;
        //                if (!storage.data[pair.Key].Contains(text)) {

        //                    var res = MessageBox.Show($"Такой записи типа {pair.Key} нет в базе данных.\nДобавить?",
        //                        "Ошибка данных", MessageBoxButtons.YesNo);
        //                    if (res == DialogResult.Yes) {
        //                        storage.data.rooms.Add(text);
        //                    } else text = "";
        //                }
        //            });
        //        }
        //    }
        //}
    }
}
