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

namespace Timetabled {
    public partial class MainForm : Form {
        Storage storage;
        GuiManager gui;
        public MainForm() {
            InitializeComponent();
            storage = new Storage();
        }

        private void Form_OnLoad(object sender, EventArgs e) {
            storage.Load();
            gui = new GuiManager(Controls, storage).CreateSchedule();
            AddDataSelect.SelectedIndex = 0;
        }

        private void Form_OnClosed(object sender, FormClosedEventArgs e) {
            gui.SaveSchedule(SelectDate.SelectionStart, gui.GroupField.Text);
            storage.Unload();
        }

        private void AddDataButton_Click(object sender, EventArgs e) {
            var category = storage.Data[AddDataSelect.SelectedItem.ToString()];
            var text = AddDataText.Text;

            if (text.Length == 0) {
                MessageBox.Show("Введите данные", "Подсказка");
                return;
            }

            if (!category.Contains(text)) {
                category.Add(text);
            } else MessageBox.Show("Данный элемент уже есть в списке", "Ошибка данных");
        }

        private void DisplayScheduleButton_Click(object sender, EventArgs e) {
            var date = SelectDate.SelectionStart;
            var serializedString = storage.SerializeOnDate(date);

            var file = "viewer.html";
            var args = "?schedule=" + Uri.EscapeDataString(serializedString)
                + "&date=" + date.ToShortDateString();

            if (storage.Settings.DefaultBrowser == null) {
                storage.Settings.CheckDefaultBrowser(file);
            }

            Process.Start(storage.Settings.DefaultBrowser, file + args);
        }

        private void OpenDatabase(object sender, EventArgs e) {
            var dbManager = new DatabaseManager();
            dbManager.Show();
        }
    }
}
