using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Timetabled.GUI;

namespace Timetabled.Forms {
    public partial class AddGroupForm : Form {
        public AddGroupForm() {
            InitializeComponent();
        }

        bool added = false;

        private void AcceptGroup_Click(object sender, EventArgs e) {
            if (GroupInput.Text != "") {
                MainForm.Storage.Data.groups.Add(GroupInput.Text);
                added = true;
                Close();
            }
        }

        private void GroupInput_Leave(object sender, EventArgs e) {
            GroupInput.Text = GuiManager.CensorField(GroupInput.Text);
        }

        private void AddGroupForm_FormClosing(object sender, FormClosingEventArgs e) {
            if (!added) e.Cancel = true;
        }
    }
}
