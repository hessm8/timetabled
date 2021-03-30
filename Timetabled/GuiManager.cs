using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace Timetabled {
    public class GuiManager {
        Control.ControlCollection controls;
        Point elementOffset => new Point(0, 30);

        public GuiManager(Control.ControlCollection _control) {
            controls = _control;
        }

        public void CreateSchedule() {
            var test = new GroupBox() {
                Text = "Понедельник",
                Location = new Point(400, 100),

            };

            test.Controls.Add(new Button() {
            });
            test.Controls.Add(new ComboBox() {
                Location = new Point(0, 20)
            });

            controls.Add(test);
        }
    }
}
