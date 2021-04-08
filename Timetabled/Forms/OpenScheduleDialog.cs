﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Timetabled.Data;
using Timetabled.GUI;

namespace Timetabled.Forms {
    public partial class OpenScheduleDialog : Form {
        Storage storage;
        MainGui gui;
        public OpenScheduleDialog(Storage _storage, MainGui _gui) {
            InitializeComponent();
            storage = _storage;
            gui = _gui;
        }

        private void OpenScheduleDialog_Load(object sender, EventArgs e) {
            SelectDayOfWeek.SelectedIndex = 0;
        }

        private void SendOpenRequest(object sender, EventArgs e) {
            if (gui.OpenSchedule(SelectDayOfWeek.SelectedIndex)) {
                Close();
            }
        }
    }
}
