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
                var size = new Size(200, 600);
                var comboSize = new Size(150, 20);

                var dayGroup = new GroupBox() {
                    Name = $"dayGroup{dayIndex + 1}",
                    Text = $"День {dayIndex + 1}",
                    Location = pos,
                    Size = size
                };

                for (int lessonIndex = 0; lessonIndex < 6; lessonIndex++) {
                    int lessonOffset = lessonIndex * 70;

                    var subjectBox = new ComboBox() {
                        //Name = $"subjectBox{dayIndex + 1}",
                        Location = new Point(20, pos.Y + lessonOffset),
                        Size = comboSize
                    };

                    var teacherBox = new ComboBox() {
                        //Name = $"teacherBox{dayIndex + 1}",
                        Location = new Point(20, pos.Y + offset.Y + lessonOffset),
                        Size = comboSize
                    };

                    var roomBox = new ComboBox() {
                        //Name = $"roomBox{dayIndex + 1}",
                        Location = new Point(20, pos.Y + offset.Y * 2 + lessonOffset),
                        Size = comboSize
                    };

                    dayGroup.Controls.AddRange(new Control[] {
                        subjectBox, teacherBox, roomBox
                    });
                }

                controls.Add(dayGroup);
            }
        }
    }
}
