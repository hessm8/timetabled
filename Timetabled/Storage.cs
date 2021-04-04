using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Runtime.Serialization;
using Newtonsoft.Json;

namespace Timetabled {
    public class Storage {
        public class Data {
            public Data() {
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
            public List<string> groups { get; set; }
            public List<string> subjects { get; set; }
            public List<string> teachers { get; set; }
            public List<string> rooms { get; set; }
        }

        public Data data = new Data();
        public AppSettings settings = new AppSettings();

        public Dictionary<DateTime, Dictionary<string, Lesson[]>> schedules
            = new Dictionary<DateTime, Dictionary<string, Lesson[]>>();
        private string DataFilepath => "data.json";
        private string SchedulesFilepath => "schedules.json";
        private string SettingsFilepath => "settings.json";

        JsonSerializerSettings serializerSettings = new JsonSerializerSettings {
            DateFormatString = "yyyy-MM-dd"
        };
        public void Load() {
            string serializedData;
            if (File.Exists(DataFilepath)) {
                serializedData = File.ReadAllText(DataFilepath);
                if (new FileInfo(DataFilepath).Length != 0) {
                    data = JsonConvert.DeserializeObject<Data>(serializedData, serializerSettings);
                }
            }
            if (File.Exists(SchedulesFilepath)) {
                serializedData = File.ReadAllText(SchedulesFilepath);
                if (new FileInfo(SchedulesFilepath).Length != 0) {
                    schedules = (Dictionary<DateTime, Dictionary<string, Lesson[]>>)JsonConvert
                        .DeserializeObject(serializedData, schedules.GetType(), serializerSettings);
                }
            }
            if (File.Exists(SettingsFilepath)) {
                serializedData = File.ReadAllText(SettingsFilepath);
                if (new FileInfo(SettingsFilepath).Length != 0) {
                    settings = JsonConvert.DeserializeObject<AppSettings>(serializedData, serializerSettings);
                }
            }
        }
        public void Unload() {
            string serializedData = JsonConvert.SerializeObject(data, serializerSettings);
            File.WriteAllText(DataFilepath, serializedData);

            serializedData = JsonConvert.SerializeObject(schedules, serializerSettings);
            File.WriteAllText(SchedulesFilepath, serializedData);

            serializedData = JsonConvert.SerializeObject(settings, serializerSettings);
            File.WriteAllText(SettingsFilepath, serializedData);
        }
        public string SerializeOnDate(DateTime date) {
            var serializedData = JsonConvert.SerializeObject(schedules[date], serializerSettings);
            return serializedData;
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