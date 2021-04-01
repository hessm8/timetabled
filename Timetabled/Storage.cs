using System;
using System.Collections.Generic;
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

        public Dictionary<DateTime, Dictionary<string, Lesson[]>> schedules
            = new Dictionary<DateTime, Dictionary<string, Lesson[]>>();
        private string DataFilepath => "data.json";
        private string SchedulesFilepath => "schedules.json";

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
        }
        public void Save() {
            string serializedData = JsonConvert.SerializeObject(data, serializerSettings);
            File.WriteAllText(DataFilepath, serializedData);

            serializedData = JsonConvert.SerializeObject(schedules, serializerSettings);
            File.WriteAllText(SchedulesFilepath, serializedData);
        }

        public string ScheduleJSON() {
            if (File.Exists(SchedulesFilepath)) {
                if (new FileInfo(SchedulesFilepath).Length != 0) {
                    return File.ReadAllText(SchedulesFilepath);
                }
            }
            return null;
        }
    }

    [Serializable()]
    public class Lesson : ISerializable {
        public Lesson(string _subject, string _teacher, string _room) {
            subject = _subject;
            teacher = _teacher;
            room = _room;
        }
        readonly string subject, teacher, room;

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