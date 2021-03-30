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

namespace Timetabled {
    public partial class MainForm : Form {
        Storage storage;
        GuiManager gui;
        public MainForm() {
            InitializeComponent();
        }

        private void mainDataGrid_CellContentClick(object sender, DataGridViewCellEventArgs e) {

        }

        private void InitSchedule() {
            //var test = new GroupBox() {
            //    Text = "Понедельник",
            //    Location = new Point(400, 100),

            //};

            //test.Controls.Add(new Button() { 
            //    Text = "Писька",
            //});
            //test.Controls.Add(new ComboBox() {
            //    Text = "Писосник",
            //    Location = new Point(0, 20)
            //});

            //Controls.Add(test);
        }

        private void Form_OnLoad(object sender, EventArgs e) {
            storage = new Storage();
            storage.Load();

            SelectData.SelectedIndex = 0;



            LoadItems();

            gui = new GuiManager(Controls);
            gui.CreateSchedule();


            //Label l = new Label();
            //l.Location = new Point(100, 100);
            //l.Text = "qqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqq";
            //FlowLayoutPanel pan = flowLayoutPanel1.Clone;
            //Controls.Add(pan);
            //Debug.Print(storage.data.groups.Last());
        }

        private void Form_OnClosed(object sender, FormClosedEventArgs e) {
            storage.Save();
        }

        private void LoadItems() {
            storage.data.subjects.ForEach(i => comboBox1.Items.Add(i));
            storage.data.teachers.ForEach(i => comboBox2.Items.Add(i));
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

                switch (SelectData.SelectedItem) {
                    case "Группа": {
                        //list = storage.data.groups;
                        break;
                    }
                    case "Преподаватель": {
                        comboBox2.Items.Add(AddDataText.Text);
                        break;
                    }
                    case "Дисциплина": {
                        comboBox1.Items.Add(AddDataText.Text);
                        break;
                    }
                    case "Аудитория": {
                        //list = storage.data.rooms;
                        break;
                    }
                    default: {
                        //list = null;
                        break;
                    }
                }

            } else MessageBox.Show("Данный элемент уже есть в списке", "Ошибка данных");
        }

        private void textBox2_Leave(object sender, EventArgs e) {
            if (textBox2.Text == "") return;
            if (!storage.data.rooms.Contains(textBox2.Text)) {

                var res = MessageBox.Show("Такой аудитории не существует,\nхотите ли вы добавить ее?",
                    "Ошибка данных", MessageBoxButtons.YesNo);
                if (res == DialogResult.Yes) {
                    storage.data.rooms.Add(textBox2.Text);
                } else textBox2.Clear();
            }
        }
    }
}
