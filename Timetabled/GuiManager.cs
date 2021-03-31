using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace Timetabled {
    public class GuiManager {
        Control.ControlCollection controls;
        Storage storage;
        Point offset => new Point(200, 20);

        public GuiManager(Control.ControlCollection _control, Storage _storage) {
            controls = _control;
            storage = _storage;
        }

        Dictionary<string, List<ComboBox>> fields = new Dictionary<string, List<ComboBox>>(); 

        public void CreateSchedule() {
            fields.Add("Преподаватель", new List<ComboBox>());
            fields.Add("Дисциплина", new List<ComboBox>());
            fields.Add("Аудитория", new List<ComboBox>());

            for (int dayIndex = 0; dayIndex < 7; dayIndex++) {
                var pos = new Point(dayIndex * offset.X + 30, 30);
                var size = new Size(192, 475);
                var comboSize = new Size(150, 20);

                var dayGroup = new GroupBox() {
                    Name = $"dayGroup{dayIndex + 1}",
                    Text = $"День {dayIndex + 1}",
                    Location = pos,
                    Size = size
                };

                for (int lessonIndex = 0; lessonIndex < 6; lessonIndex++) {
                    int lessonOffset = lessonIndex * 70;

                    var lessonPanel = new Panel() {
                        Location = new Point(20, pos.Y + lessonOffset),
                        Size = new Size(152, 63),
                        BorderStyle = BorderStyle.FixedSingle                        
                    };

                    var subjectBox = new ComboBox() {
                        //Name = $"subjectBox{dayIndex + 1}",
                        Location = new Point(0, 0),
                        Size = comboSize
                    };
                    fields["Дисциплина"].Add(subjectBox);

                    var teacherBox = new ComboBox() {
                        //Name = $"teacherBox{dayIndex + 1}",
                        Location = new Point(0, offset.Y),
                        Size = comboSize,
                    };
                    fields["Преподаватель"].Add(subjectBox);

                    var roomBox = new ComboBox() {
                        //Name = $"roomBox{dayIndex + 1}",
                        Location = new Point(0, offset.Y * 2),
                        Size = comboSize
                    };
                    fields["Аудитория"].Add(subjectBox);

                    lessonPanel.Controls.AddRange(new Control[] {
                        subjectBox, teacherBox, roomBox
                    });

                    dayGroup.Controls.Add(lessonPanel);
                }

                InitializeFields(); // Doesn't work, probably because it gets added to controls beforehand

                controls.Add(dayGroup);
            }
        }

        public void InitializeFields() {
            foreach (var pair in fields) {
                foreach (var combo in pair.Value) {
                    var text = combo.Text;

                    combo.Leave += new EventHandler((sender, e) => {
                        if (text == "") return;
                        if (!storage.data[pair.Key].Contains(text)) {

                            var res = MessageBox.Show($"Такой записи типа {pair.Key} нет в базе данных.\nДобавить?",
                                "Ошибка данных", MessageBoxButtons.YesNo);
                            if (res == DialogResult.Yes) {
                                storage.data.rooms.Add(text);
                            } else text = "";
                        }
                    });
                }
            }
        }
    }
}
