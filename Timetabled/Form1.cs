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

namespace Timetabled {
    public partial class MainForm : Form {
        Storage storage;
        GuiManager gui;
        public MainForm() {
            InitializeComponent();
        }

        private void Form_OnLoad(object sender, EventArgs e) {
            storage = new Storage();
            storage.Load();

            SelectData.SelectedIndex = 0;

            gui = new GuiManager(Controls, storage);
            gui.CreateSchedule();
        }

        private void Form_OnClosed(object sender, FormClosedEventArgs e) {
            storage.Save();
        }

        private void AddDataButton_Click(object sender, EventArgs e) {
            var category = storage.data[SelectData.SelectedItem.ToString()];
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

            string args = "?schedule=" + storage.ScheduleJSON();
            string fileName = @"viewer.html";
            //new Process() {
            //    StartInfo = new ProcessStartInfo() {
            //        UseShellExecute = true,
            //        FileName = fileName,
            //        Arguments = args
            //    }
            //}.Start();
            //Process.Start(GetDefaultBrowserPath(), url);

            //var process = Process.Start(fileName + args);
        }
    }
}
