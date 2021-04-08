using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Diagnostics;
using System.Globalization;
using System.Web;
using Timetabled.Data;
using Timetabled.GUI;

namespace Timetabled.Forms {
    public partial class MainForm : Form {
        Storage storage;
        MainGui gui;
        public MainForm() {
            InitializeComponent();
            storage = new Storage();
        }

        private void Form_OnLoad(object sender, EventArgs e) {
            storage.Load();
            gui = new MainGui(Controls, storage);
        }

        private void Form_OnClosed(object sender, FormClosedEventArgs e) {
            if (gui.Groups.Latest != "") gui.UnloadSchedule(gui.Dates.Latest, gui.Groups.Latest);
            storage.Unload();
        }

        private void DisplayScheduleButton_Click(object sender, EventArgs e) {
            var openForm = new OpenScheduleDialog(storage, gui);
            openForm.ShowDialog();
            
        }

        private void OpenDatabase(object sender, EventArgs e) {
            var dbManager = new DatabaseEditor(storage);
            dbManager.Show();
        }
    }
}
