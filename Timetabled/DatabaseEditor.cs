using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Timetabled {
    public partial class DatabaseEditor : Form {
        Storage storage;
        DatabaseGui gui;
        public DatabaseEditor(Storage _storage) {
            InitializeComponent();
            storage = _storage;
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e) {

        }

        private void DatabaseManager_Load(object sender, EventArgs e) {
            gui = new DatabaseGui(Controls, storage);
        }

        private void AddDataSelect_SelectedIndexChanged(object sender, EventArgs e) {

        }
    }
}
