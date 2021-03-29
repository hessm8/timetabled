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
    public partial class Form1 : Form {
        Storage storage;
        public Form1() {
            InitializeComponent();
        }

        private void mainDataGrid_CellContentClick(object sender, DataGridViewCellEventArgs e) {

        }

        private void Form1_Load(object sender, EventArgs e) {
            storage = new Storage();
            storage.Load();

            SelectData.SelectedIndex = 0;

            //Label l = new Label();
            //l.Location = new Point(100, 100);
            //l.Text = "qqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqq";
            //FlowLayoutPanel pan = flowLayoutPanel1.Clone;
            //Controls.Add(pan);
            //Debug.Print(storage.data.groups.Last());
        }

        private void Form1_FormClosed(object sender, FormClosedEventArgs e) {
            storage.Save();            
        }

        private void AddDataButton_Click(object sender, EventArgs e) {
            List<string> list;

            switch (SelectData.SelectedItem) {
                case "Группа": { 
                    list = storage.data.groups;
                    break;
                }
                case "Преподаватель": {
                    list = storage.data.teachers;
                    break;
                }
                case "Дисциплина": {
                    list = storage.data.subjects;
                    break;
                }
                case "Аудитория": {
                    list = storage.data.rooms;
                    break;
                }
                default: {
                    list = null;
                    break;
                }
            }

            if (AddDataText.Text == "") {
                MessageBox.Show("Введите данные");
                return;
            }

            if (!list.Contains(AddDataText.Text)) {
                list.Add(AddDataText.Text);
            } else MessageBox.Show("Данный элемент уже есть в списке");
        }
    }
}
