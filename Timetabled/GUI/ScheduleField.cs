using System;
using System.Collections.Generic;
using System.Linq;
using System.Drawing;
using System.Windows.Forms;
using System.Diagnostics;
using System.Text.RegularExpressions;
using Timetabled.Helpers;
using Timetabled.Data;
using Timetabled.Forms;

namespace Timetabled.GUI {
    public class ScheduleField : ComboBox {
        // Gui access
        private MainGui Gui { get; }
        private Storage Storage => Gui.Storage;
        private ControlCollection ParentControls => Gui.Controls;

        // Extension data
        public FieldType Type { get; }
        private List<string> Category { get; }
        private string CategoryName { get; }
        public (int day, int lesson) Position { get; }

        public ScheduleField(MainGui _guiManager, FieldType _type, (int day, int lesson) _pos) {
            Gui = _guiManager;
            Type = _type;
            Position = _pos;

            CategoryName = Categories[Type];
            Category = Storage.Data[CategoryName];

            SubscribeActions();
        }
        public ScheduleField(MainGui _guiManager) {
            Gui = _guiManager;
            Type = FieldType.Group;

            CategoryName = Categories[Type];
            Category = Storage.Data[CategoryName];        

            SubscribeActions();
        }

        private DateTime Date => Gui.Calendar.SelectionStart.AddDays(Position.day);    

        /// <summary>
        /// Checks if current item from dropdown list is available across groups
        /// </summary>
        /// <param name="item">Item from dropdown list of this ScheduleField</param>
        /// <returns>True if other groups don't have this item chosen</returns>
        private bool ItemAvailable(string item) {
            if (Storage.Schedules.ContainsKey(Date)) {
                // All other groups on the current date
                foreach (var group in Storage.Schedules[Date]) {
                    var curGroup = Gui.GroupField;
                    if (group.Key == curGroup.Text) continue;
                    
                    var lessons = group.Value;
                    // If current group has a lesson on the date
                    if (Position.lesson < lessons.Length) {
                        var lesson = lessons[Position.lesson];
                        var otherGroupItem = lesson[(int)Type];
                        if (otherGroupItem == item) return false;
                    }
                }
            }
            return true;
        }

        public void SubscribeActions() {
            // Database and text field check on leave
            Leave += DataField_Leave;
            // Refresh the list on dropdown
            DropDown += DataField_DropDown;
            // Key binds
            KeyDown += DataField_KeyDown;
        }

        // Subscribed actions
        private void DataField_DropDown(object sender, EventArgs e) {
            Items.Clear();
            if (Type == FieldType.Teacher || Type == FieldType.Room) {
                foreach (var item in Category) {
                    if (ItemAvailable(item)) Items.Add(item);
                }
            } else Items.AddRange(Category.ToArray());
        }
        private void DataField_KeyDown(object sender, KeyEventArgs e) {            
            switch (e.KeyCode) {
                // Autocomplete
                case Keys.Enter:
                    if (!e.Alt) {
                        // Based on input
                        Text = Category.Find(t => t.ToLower()
                        .Contains(Text.ToLower())) ?? Text;
                    } else FillRandom();
                    break;
                // Jump to the next day
                case Keys.Tab:
                    if (e.Control && Type != FieldType.Group) {
                        Gui.AllFields[Position.day + 1, Position.lesson, 0].Focus();
                    }
                    break;
            }
        }
        private void DataField_Leave(object sender, EventArgs e) {
            // Skip if empty
            if (Text == "") return;
            // Leave only Russian and special characters
            Text = GuiManager.CensorField(Text);
            // Ask to add if item is not in database
            if (!Category.Contains(Text)) {
                var popupResult = MessageBox.Show(
                    $"Элемента нет в списке [{CategoryName}],\nДобавить его в базу данных?",
                    "Ошибка данных", MessageBoxButtons.YesNo);

                if (popupResult == DialogResult.Yes) {
                    Category.Add(Text);
                } else Text = "";
            }            
        }

        public void FillRandom() {
            if (Category.Count > 0) {                
                var item = Category[Helper.Random(Category.Count)];

                if (ItemAvailable(item)) Text = item;
                else FillRandom();
            }
        }

        private Dictionary<FieldType, string> Categories => new Dictionary<FieldType, string>() {
            [FieldType.Subject] = "Дисциплина",
            [FieldType.Teacher] = "Преподаватель",
            [FieldType.Room] = "Аудитория",
            [FieldType.Group] = "Группа"
        };
    }

    public enum FieldType {
        Subject,
        Teacher,
        Room,
        Group
    }
}