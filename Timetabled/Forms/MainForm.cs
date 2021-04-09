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
using System.Media;
using System.IO;

namespace Timetabled.Forms {
    public partial class MainForm : Form {
        Storage storage;
        MainGui mainGui;
        DatabaseGui dbGui;
        public MainForm() {
            InitializeComponent();
            storage = new Storage();

            Size = MaximumSize;

            ViewScheduleMenuItem.Click += ViewSchedule;
            EditDataMenuItem.Click += OpenDatabase;
            AcceptChangesButton.Click += AcceptChangesButton_Click;
            CancelChangesButton.Click += CancelChangesButton_Click;


        }

        private void Form_OnLoad(object sender, EventArgs e) {
            storage.Load();
            mainGui = new MainGui(Controls, storage);
            dbGui = new DatabaseGui(Controls, storage);
        }

        private void Form_OnClosed(object sender, FormClosedEventArgs e) {
            if (mainGui.Groups.Latest != "") mainGui.UnloadSchedule(mainGui.Dates.Latest, mainGui.Groups.Latest);
            storage.Unload();
        }

        private void ViewSchedule(object sender, EventArgs e) {
            var openForm = new OpenScheduleDialog(storage, mainGui);
            openForm.ShowDialog();
            
        }

        private void OpenDatabase(object sender, EventArgs e) {
            var dbManager = new DatabaseEditor(storage);
            dbManager.Show();
        }

        private void HelpMenuItem_Click(object sender, EventArgs e) {
            new AboutBox().Show();
        }

        private void AcceptChangesButton_Click(object sender, EventArgs e) {
            dbGui.UnloadCategory(dbGui.Selected.Latest);
            dbGui.UnloadToStorage();
        }

        private void CancelChangesButton_Click(object sender, EventArgs e) {
            dbGui.LoadFromStorage();
            dbGui.LoadNewCategory();
        }
    }
}
