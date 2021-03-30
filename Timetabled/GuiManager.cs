using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace Timetabled {
    public class GuiManager {
        Control.ControlCollection controls;
        Point offset => new Point(200, 20);

        public GuiManager(Control.ControlCollection _control) {
            controls = _control;
        }

        public void CreateSchedule() {
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

                    var teacherBox = new ComboBox() {
                        //Name = $"teacherBox{dayIndex + 1}",
                        Location = new Point(0, offset.Y),
                        Size = comboSize
                    };

                    var roomBox = new ComboBox() {
                        //Name = $"roomBox{dayIndex + 1}",
                        Location = new Point(0, offset.Y * 2),
                        Size = comboSize
                    };

                    lessonPanel.Controls.AddRange(new Control[] {
                        subjectBox, teacherBox, roomBox
                    });

                    dayGroup.Controls.Add(lessonPanel);
                }

                controls.Add(dayGroup);
            }
        }
    }
}
