using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization;
using System.Text.Json;
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
                        case "Преподаватель": return teachers;
                        case "Дисциплина": return subjects;
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
        
        public Dictionary<DateTime, Schedule> schedules = new Dictionary<DateTime, Schedule>();
        private string DataFilepath => "data.json";
        private string SchedulesFilepath => "schedules.json";
        
        //public class LessonJsonConverter : JsonConverter<Dictionary<DateTime, Schedule>> {
        //    public override Dictionary<DateTime, Schedule> Read(
        //        ref Utf8JsonReader reader,
        //        Type typeToConvert,
        //        JsonSerializerOptions options) =>
        //            DateTimeOffset.ParseExact(reader.GetString(),
        //                "MM/dd/yyyy", CultureInfo.InvariantCulture);

        //    public override void Write(
        //        Utf8JsonWriter writer,
        //        DateTimeOffset dateTimeValue,
        //        JsonSerializerOptions options) =>
        //            writer.WriteStringValue(dateTimeValue.ToString(
        //                "MM/dd/yyyy", CultureInfo.InvariantCulture));
        //}


        public void Load() {
            string serializedData;
            if (File.Exists(DataFilepath)) {
                serializedData = File.ReadAllText(DataFilepath);
                if (new FileInfo(DataFilepath).Length != 0) {
                    data = JsonConvert.DeserializeObject<Data>(serializedData);
                }
            }
            if (File.Exists(SchedulesFilepath)) {
                serializedData = File.ReadAllText(SchedulesFilepath);
                if (new FileInfo(SchedulesFilepath).Length != 0) {
                    schedules = (Dictionary<DateTime, Schedule>)JsonConvert
                        .DeserializeObject(serializedData, schedules.GetType());
                }
            }
        }
        public void Save() {
            string serializedData = JsonConvert.SerializeObject(data);
            File.WriteAllText(DataFilepath, serializedData);

            serializedData = JsonConvert.SerializeObject(schedules);
            File.WriteAllText(SchedulesFilepath, serializedData);
        }
    }

    public class Schedule {
        public Schedule(string _group, params Lesson[] _lessons) {
            group = _group;
            lessons = _lessons;
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

        public string group;
        public Lesson[] lessons;
    }
}