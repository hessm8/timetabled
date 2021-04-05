using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Runtime.Serialization;
using Newtonsoft.Json;

namespace Timetabled {
    public class Storage {
        public class ScheduleData {
            public ScheduleData() {
                groups = new List<string>();
                subjects = new List<string>();
                teachers = new List<string>();
                rooms = new List<string>();
            }
            public List<string> this[string i] {
                get {
                    switch (i) {
                        case "Группа": return groups;
                        case "Дисциплина": return subjects;
                        case "Преподаватель": return teachers;
                        case "Аудитория": return rooms;
                        default: throw new Exception("Invalid data");
                    }
                }
            }
            public List<string> groups, subjects, teachers, rooms;
        }
        public Storage() {
            Data = new ScheduleData();
            Settings = new AppSettings();
            Schedules = new Dictionary<DateTime, Dictionary<string, Lesson[]>>();
        }

        // Stored items
        public ScheduleData Data { get; set; }
        public AppSettings Settings { get; set; }
        public Dictionary<DateTime, Dictionary<string, Lesson[]>> Schedules { get; set; }

        // JSON Convertation
        public static JsonSerializerSettings JsonSettings => new JsonSerializerSettings {            
            Formatting = Formatting.Indented,
            DateFormatString = "yyyy-MM-dd"
        };
        private string DataFilepath => "data.json";
        private string SchedulesFilepath => "schedules.json";
        private string SettingsFilepath => "settings.json";

        public void Load() {
            string serializedData;
            if (File.Exists(DataFilepath)) {
                serializedData = File.ReadAllText(DataFilepath);
                if (new FileInfo(DataFilepath).Length != 0) {
                    Data = JsonConvert.DeserializeObject<ScheduleData>(serializedData, JsonSettings);
                }
            }
            if (File.Exists(SchedulesFilepath)) {
                serializedData = File.ReadAllText(SchedulesFilepath);
                if (new FileInfo(SchedulesFilepath).Length != 0) {
                    Schedules = (Dictionary<DateTime, Dictionary<string, Lesson[]>>)JsonConvert
                        .DeserializeObject(serializedData, Schedules.GetType(), JsonSettings);
                }
            }
            if (File.Exists(SettingsFilepath)) {
                serializedData = File.ReadAllText(SettingsFilepath);
                if (new FileInfo(SettingsFilepath).Length != 0) {
                    Settings = JsonConvert.DeserializeObject<AppSettings>(serializedData, JsonSettings);
                }
            }
        }
        public void Unload() {
            string serializedData = JsonConvert.SerializeObject(Data, JsonSettings);
            File.WriteAllText(DataFilepath, serializedData);

            serializedData = JsonConvert.SerializeObject(Schedules, JsonSettings);
            File.WriteAllText(SchedulesFilepath, serializedData);

            serializedData = JsonConvert.SerializeObject(Settings, JsonSettings);
            File.WriteAllText(SettingsFilepath, serializedData);
        }

        public string SerializeOnDate(DateTime date) {
            if (!Schedules.ContainsKey(date)) return null;
            return JsonConvert.SerializeObject(Schedules[date], JsonSettings);
        }
    }

    [Serializable()]
    public class Lesson : ISerializable {
        public Lesson(string _subject = null, string _teacher = null,
            string _room = null) {
            subject = _subject;
            teacher = _teacher;
            room = _room;
        }
        public string this[int i] {
            get { 
                switch (i) {
                    case 0: return subject;
                    case 1: return teacher;
                    case 2: return room;
                    default: return null;
                }
            }
            set {
                switch (i) {
                    case 0: subject = value; break;
                    case 1: teacher = value; break;
                    case 2: room = value; break;
                    default: break;
                }
            }
        }
        public string subject, teacher, room;

        //Deserialization
        public Lesson(SerializationInfo info, StreamingContext ctxt) {
            subject = (string)info.GetValue("subject", typeof(string));
            teacher = (string)info.GetValue("teacher", typeof(string));
            room = (string)info.GetValue("room", typeof(string));
        }
        //Serialization
        public void GetObjectData(SerializationInfo info, StreamingContext ctxt) {
            info.AddValue("subject", subject);
            info.AddValue("teacher", teacher);
            info.AddValue("room", room);
        }
    }
}