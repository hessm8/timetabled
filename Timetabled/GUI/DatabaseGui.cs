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
            var testGrid = Access<DataGridView>("testGrid");
            bool test = testGrid != null;
            DataGrid = new DataGridView() {
                Location = test ? testGrid.Location : new Point(102, 10),                
                AllowUserToDeleteRows = true,
                AllowUserToAddRows = true,
                AllowUserToResizeColumns = false,
                AllowUserToResizeRows = false,
                RowHeadersWidthSizeMode = DataGridViewRowHeadersWidthSizeMode.DisableResizing
            };
            if (test) {
                DataGrid.Size = testGrid.Size;
            }           
            testGrid.Dispose();

            DataGrid.Columns.Add("Header", " ");
            Header.Width = 184;
            Controls.Add(DataGrid);

            SelectList = Access<ListBox>("AddDataSelect");
            SelectList.SelectedIndex = 0;

            Selected = new State<string>(() => SelectList.SelectedItem.ToString());

            EditedData = new ScheduleData();
            LoadFromStorage();
            LoadNewCategory();

            SelectList.SelectedIndexChanged += OnIndexChange;
        }

        private void OnIndexChange(object sender, EventArgs e) {
            Selected.Update();                      
            UnloadCategory(Selected.Previous);
            LoadNewCategory();
        }

        public DataGridView DataGrid { get; private set; }
        public ListBox SelectList { get; private set; }
        private DataGridViewColumn Header => DataGrid.Columns[0];

        public ScheduleData EditedData { get; private set; }
        public State<string> Selected { get; private set; }

        #region Loading Data

        public void LoadFromStorage() {            
            for (int listIndex = 0; listIndex < 4; listIndex++) {
                EditedData[listIndex].Clear();
                foreach (var i in Storage.Data[listIndex]) {
                    EditedData[listIndex].Add(i);
                }
            }
        }
        public void UnloadToStorage() {
            for (int listIndex = 0; listIndex < 4; listIndex++) {
                Storage.Data[listIndex].Clear();
                foreach (var i in EditedData[listIndex]) {
                    Storage.Data[listIndex].Add(i);
                }
            }
        }
        public void LoadNewCategory() {
            // Update header when loading
            Header.HeaderText = Selected.Latest;
            
            var unloadFrom = EditedData[Selected.Latest];
            // Load to the datagrid view
            DataGrid.Rows.Clear();
            foreach (var i in unloadFrom) {
                DataGrid.Rows.Add(i);
            }
        }
        public void UnloadCategory(string category) {
            var loadTo = EditedData[category];
            // Load to local storage
            loadTo.Clear();
            for (int i = 0; i < DataGrid.RowCount - 1; i++) {
                var value = DataGrid[0, i].Value;
                if (value != null) loadTo.Add(value.ToString());
            }
        }

        #endregion
    }
}