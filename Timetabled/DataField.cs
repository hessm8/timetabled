using System;
using System.Collections.Generic;
using System.Linq;
using System.Drawing;
using System.Windows.Forms;
using System.Diagnostics;
using System.Text.RegularExpressions;

namespace Timetabled {
    public class DataField : ComboBox {
        // Gui access
        private MainGui GuiManager { get; }
        private Storage Storage => GuiManager.Storage;
        private ControlCollection ParentControls => GuiManager.Controls;

        // Extension data
        public FieldType Type { get; }
        private List<string> Category { get; }
        private string CategoryName { get; }
        public (int day, int lesson) Position { get; }

        public DataField(MainGui _guiManager, FieldType _type, (int day, int lesson) _pos) {
            GuiManager = _guiManager;
            Type = _type;
            Position = _pos;

            CategoryName = Categories[Type];
            Category = Storage.Data[CategoryName];

            SubscribeActions();
        }
        public DataField(MainGui _guiManager) {
            GuiManager = _guiManager;
            Type = FieldType.Group;

            CategoryName = Categories[Type];
            Category = Storage.Data[CategoryName];        

            SubscribeActions();
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
            Items.AddRange(Category.ToArray());
        }
        private void DataField_KeyDown(object sender, KeyEventArgs e) {            
            switch (e.KeyCode) {
                // Autocomplete
                case Keys.Enter:
                    if (!e.Alt) {
                        // Based on input
                        Text = Category.Find(t => t.ToLower()
                        .Contains(Text.ToLower())) ?? Text;
                    } else {
                        // On random
                        var randomIndex = new Random().Next(Category.Count);
                        Text = Category[randomIndex];
                    }
                    break;
                // Jump to the next day
                case Keys.Tab:
                    if (e.Control && Type != FieldType.Group) {
                        GuiManager.AllFields[Position.day + 1, Position.lesson, 0].Focus();
                    }
                    break;
            }
        }
        private void DataField_Leave(object sender, EventArgs e) {
            // Skip if empty
            if (Text == "") return;
            // Leave only Russian and special characters
            Text = Regex.Replace(Text, @"[^-.ЁёА-Яа-я0-9\s]", "");
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