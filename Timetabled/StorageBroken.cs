//using System;
//using System.Collections.Generic;
//using System.Diagnostics;
//using System.IO;
//using System.Runtime.Serialization;
//using Newtonsoft.Json;

//namespace Timetabled {
//    public class Storage {
//        public interface IStoredItem {
//            string Path { get; }
//            void Load();
//            void Unload();
//        }
//        public class StoredItem<TItem> : IStoredItem where TItem : class {
//            public StoredItem(TItem item, string filename) {
//                DataItem = item;
//                Path = filename + ".json";
//            }
//            public string Path { get; }
//            public TItem DataItem { get; private set; }
//            public void Load() {
//                if (File.Exists(Path)) {
//                    var serializedData = File.ReadAllText(Path);
//                    if (new FileInfo(Path).Length != 0) {
//                        DataItem = JsonConvert.DeserializeObject<TItem>(serializedData, JsonSettings);
//                    }
//                }
//            }
//            public void Unload() {
//                var serializedData = JsonConvert.SerializeObject(DataItem, JsonSettings);
//                File.WriteAllText(Path, serializedData);
//            }
//        }
//        public class ScheduleData {
//            public ScheduleData() {
//                groups = new List<string>();
//                subjects = new List<string>();
//                teachers = new List<string>();
//                rooms = new List<string>();
//            }
//            public List<string> this[string i] {
//                get {
//                    switch (i) {
//                        case "Группа": return groups;
//                        case "Дисциплина": return subjects;
//                        case "Преподаватель": return teachers;
//                        case "Аудитория": return rooms;
//                        default: throw new Exception("Invalid data");
//                    }
//                }
//            }
//            public List<string> groups { get; set; }
//            public List<string> subjects { get; set; }
//            public List<string> teachers { get; set; }
//            public List<string> rooms { get; set; }
//        }
//        public Storage() {
//            Data = new ScheduleData();
//            Settings = new AppSettings();
//            Schedules = new Dictionary<DateTime, Dictionary<string, Lesson[]>>();
//            UpdateStoredItems();
//        }

//        private void UpdateStoredItems() {
//            StoredItems = new List<IStoredItem>() {
//                new StoredItem<ScheduleData>(Data, "data"),
//                new StoredItem<Dictionary<DateTime, Dictionary<string, Lesson[]>>>(Schedules, "schedules"),
//                new StoredItem<AppSettings>(Settings, "settings")
//            };
//        }

//        List<IStoredItem> StoredItems { get; set; }

//        // Stored items
//        public ScheduleData Data;
//        public AppSettings Settings;
//        public Dictionary<DateTime, Dictionary<string, Lesson[]>> Schedules;

//        // JSON Convertation
//        public static JsonSerializerSettings JsonSettings => new JsonSerializerSettings {
//            Formatting = Formatting.Indented,
//            DateFormatString = "yyyy-MM-dd"
//        };
//        public void Load() {
//            //UpdateStoredItems();
//            StoredItems.ForEach(s => s.Load());
//        }
//        public void Unload() {
//            //UpdateStoredItems();
//            StoredItems.ForEach(s => s.Unload());
//        }
//        public string SerializeOnDate(DateTime date) {
//            return JsonConvert.SerializeObject(Schedules[date], JsonSettings);
//        }
//    }

//    [Serializable()]
//    public class Lesson : ISerializable {
//        public Lesson(string _subject = null, string _teacher = null,
//            string _room = null) {
//            subject = _subject;
//            teacher = _teacher;
//            room = _room;
//        }
//        public string this[int i] {
//            get {
//                switch (i) {
//                    case 0: return subject;
//                    case 1: return teacher;
//                    case 2: return room;
//                    default: return null;
//                }
//            }
//            set {
//                switch (i) {
//                    case 0: subject = value; break;
//                    case 1: teacher = value; break;
//                    case 2: room = value; break;
//                    default: break;
//                }
//            }
//        }
//        public string subject, teacher, room;

//        //Deserialization
//        public Lesson(SerializationInfo info, StreamingContext ctxt) {
//            subject = (string)info.GetValue("subject", typeof(string));
//            teacher = (string)info.GetValue("teacher", typeof(string));
//            room = (string)info.GetValue("room", typeof(string));
//        }
//        //Serialization
//        public void GetObjectData(SerializationInfo info, StreamingContext ctxt) {
//            info.AddValue("subject", subject);
//            info.AddValue("teacher", teacher);
//            info.AddValue("room", room);
//        }
//    }
//}