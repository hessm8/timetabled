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
        private GuiManager GuiManager { get; }
        private Storage Storage => GuiManager.Storage;
        private ControlCollection ParentControls => GuiManager.Controls;

        // Extension data
        public FieldType Type { get; }
        private List<string> Category { get; }
        private string CategoryName { get; }
        public (int day, int lesson) Position { get; }

        public DataField(GuiManager _guiManager, FieldType _type, (int day, int lesson) _pos) {
            GuiManager = _guiManager;
            Type = _type;
            Position = _pos;

            CategoryName = Categories[Type];
            Category = Storage.data[CategoryName];

            AssignField();
        }
        public DataField(GuiManager _guiManager) {
            GuiManager = _guiManager;
            Type = FieldType.Group;

            CategoryName = Categories[Type];
            Category = Storage.data[CategoryName];        

            AssignField();
        }

        public void AssignField() {
            // Database and text field check on leave
            Leave += new EventHandler((sender, e) => {
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
            });
            // Refresh the list on dropdown
            DropDown += new EventHandler((sender, e) => {
                Items.Clear();
                Items.AddRange(Category.ToArray());
            });
            // Key binds
            KeyDown += new KeyEventHandler((sender, e) => {
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
                    // Random item
                    case Keys.Tab:
                        if (e.Control && Type != FieldType.Group) {
                            GuiManager.allFields[Position.day + 1, Position.lesson, 0].Focus();
                        }
                        break;
                }
            });
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
