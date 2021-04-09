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
using Timetabled.Forms;

namespace Timetabled.GUI {
    public abstract class GuiManager {
        public abstract void Initialize();
        public GuiManager(Control.ControlCollection _control) {
            Controls = _control;
            Initialize();
        }
        protected TControl Access<TControl>(string name) where TControl : Control {
            var controls = Controls.Find(name, false);
            if (controls.Length > 0) return (TControl)controls[0];
            return null;
        }

        // Acessors
        public Control.ControlCollection Controls { get; }
        public Storage Storage => MainForm.Storage;
        
        // Additional things
        //public CultureInfo culture = new CultureInfo("ru-RU");
        protected bool scheduleLoaded = false;

        public static string AllowedFieldCharacters => @"[^-.A-Za-zЁёА-Яа-я0-9\s]";
        public static CultureInfo Culture => CultureInfo.InstalledUICulture;
        public static RegionInfo Region => RegionInfo.CurrentRegion;
        public static string CensorField(string text) {
            return Regex.Replace(text, AllowedFieldCharacters, "");
        }
    }
}