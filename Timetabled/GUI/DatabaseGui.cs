using System;
using System.Collections.Generic;
using System.Linq;
using System.Drawing;
using System.Windows.Forms;
using System.Diagnostics;
using System.Text.RegularExpressions;
using System.Globalization;
using Timetabled.Helpers;
using Timetabled.Data;

namespace Timetabled.GUI {
    public class DatabaseGui : GuiManager {
        public DatabaseGui(Control.ControlCollection _control, Storage _storage)
            : base(_control, _storage) { }
        public override void Initialize() {
            DataGrid = new DataGridView() {
                Location = new Point(12, 86),
                AllowUserToDeleteRows = true,
                AllowUserToAddRows = true,
                AllowUserToResizeColumns = false,
                AllowUserToResizeRows = false
            };
            DataGrid.Columns.Add("Datagrid1", " ");
            Controls.Add(DataGrid);

            SelectItem = Access<ListBox>("AddDataSelect");


            SelectItem.SelectedIndex = 0;

            selected = new State<string>(() => SelectItem.SelectedItem.ToString());

            ScheduleData = new ScheduleData();
            LoadData();
            LoadCategoryData();

            SelectItem.SelectedIndexChanged += OnIndexChange;
        }

        public void LoadData() => ScheduleData = Storage.LoadScheduleData();
        public void UnloadData() { 
            Storage.UnloadScheduleData(ScheduleData);
            Storage.Data = Storage.LoadScheduleData();
        }

        private void OnIndexChange(object sender, EventArgs e) {
            SelectItem.SelectedIndexChanged -= OnIndexChange;

            selected.Update();
            UnloadCategoryData();
            LoadCategoryData();

            SelectItem.SelectedIndexChanged += OnIndexChange;
        }

        public DataGridView DataGrid { get; private set; }
        public ListBox SelectItem { get; private set; }
        private DataGridViewColumn Header => DataGrid.Columns[0];

        public ScheduleData ScheduleData { get; private set; }

        #region Mess

        State<string> selected;

        private void UnloadCategoryData() {            
            var loadTo = ScheduleData[selected.Previous];

            loadTo.Clear();
            for (int i = 0; i < DataGrid.RowCount - 1; i++) {
                var value = DataGrid[0, i].Value;
                if (value != null) loadTo.Add(value.ToString());
            }

            if (Storage.Settings.AutosaveOnCategoryChange) {
                Storage.Data[selected.Previous] = loadTo.ToList();
            }
        }

        private void LoadCategoryData() {
            Header.HeaderText = selected.Latest;
            var unloadFrom = ScheduleData[selected.Latest];

            DataGrid.Rows.Clear();
            foreach (var i in unloadFrom) {
                DataGrid.Rows.Add(i);
            }
        }

        #endregion
    }
}
