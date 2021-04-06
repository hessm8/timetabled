using System;
using System.Collections.Generic;
using System.Linq;
using System.Drawing;
using System.Windows.Forms;
using System.Diagnostics;
using System.Text.RegularExpressions;
using System.Globalization;
using Timetabled.Helpers;
using Timetabled.Data;

namespace Timetabled.GUI {
    public abstract class GuiManager {
        public abstract void Initialize();
        public GuiManager(Control.ControlCollection _control, Storage _storage) {
            Controls = _control;
            Storage = _storage;
            Initialize();
        }
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
}