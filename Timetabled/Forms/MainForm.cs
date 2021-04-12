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
        public static Storage Storage { get; } = new Storage();
        MainGui mainGui;
        DatabaseGui dbGui;
        public MainForm() {
            InitializeComponent();

            Size = MaximumSize;

            ViewScheduleMenuItem.Click += ViewSchedule;
            editToolStripMenuItem.Click += OpenDatabase;
            clearToolStripMenuItem.Click += ClearDatabase;
            clearToolStripMenuItem.Click += GroupsCheck;
            AcceptChangesButton.Click += AcceptChanges;
            AcceptChangesButton.Click += GroupsCheck;
            CancelChangesButton.Click += LoadFromStorage;
        }

        private void ClearDatabase(object sender, EventArgs e) {
            var res = MessageBox.Show("Вы действительно хотите очистить данные?",
                "Предупреждение",
                MessageBoxButtons.YesNo);
            if (res == DialogResult.Yes) {
                mainGui.ClearSchedule();
                LoadFromStorage(sender, e);
            }
        }

        private void Form_OnLoad(object sender, EventArgs e) {
            Storage.Load();
            mainGui = new MainGui(Controls);
            dbGui = new DatabaseGui(Controls);

            if (Storage.Data.groups.Count == 0) {
                var add = new AddGroupForm();
                add.ShowDialog();
            }
            SelectFirstGroup(sender, e);
            LoadFromStorage(sender, e);
        }

        private void GroupsCheck(object sender, EventArgs e) {
            var groupsEmpty = Storage.Data.groups.Count == 0;
            mainGui.FieldsEnable(!groupsEmpty);
            if (!groupsEmpty && mainGui.GroupField.Text == "") SelectFirstGroup(sender, e);
        }

        private void SelectFirstGroup(object sender, EventArgs e) {
            mainGui.SelectFirstGroup();
        }

        private void Form_OnClosed(object sender, FormClosedEventArgs e) {
            if (mainGui.Groups.Latest != "") mainGui.UnloadSchedule(mainGui.Dates.Latest, mainGui.Groups.Latest);
            Storage.Unload();
        }

        private void ViewSchedule(object sender, EventArgs e) {
            var openForm = new OpenScheduleDialog(Storage, mainGui);
            openForm.ShowDialog();
            
        }

        private void OpenDatabase(object sender, EventArgs e) {
            var dbManager = new DatabaseEditor(Storage);
            dbManager.Show();
        }

        private void HelpMenuItem_Click(object sender, EventArgs e) {
            new AboutBox().Show();
        }

        private void AcceptChanges(object sender, EventArgs e) {
            dbGui.UnloadCategory(dbGui.Selected.Latest);
            dbGui.UnloadToStorage();
        }

        private void LoadFromStorage(object sender, EventArgs e) {
            dbGui.LoadFromStorage();
            dbGui.LoadNewCategory();
        }

        private void FillRandomData(object sender, EventArgs e) {
            mainGui.FieldsAllRandom();
        }
    }
}
