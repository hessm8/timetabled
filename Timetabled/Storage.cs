using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
//using Newtonsoft.Json.Converters;
using System.Diagnostics;
using System.Windows.Forms;
using System.Drawing;

namespace Timetabled {
    public class Storage {
        public class ScheduleData {
            public ScheduleData() {
                schedules = new List<GroupSchedules>();
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

            public List<GroupSchedules> schedules { get; set; }
            public List<string> groups { get; set; }
            public List<string> subjects { get; set; }
            public List<string> teachers { get; set; }
            public List<string> rooms { get; set; }
        }
        public class GroupSchedules {
            public struct Schedule {
                public Schedule(DateTime _date, string _subject,
                    string _teacher, string _room) {
                    date = _date;
                    subject = _subject;
                    teacher = _teacher;
                    room = _room;
                }

                DateTime date;
                string subject, teacher, room;
            }

            public string name;
            public List<Schedule> schedules;
        }

        public ScheduleData data { get; private set; }
        private string FilePath => "data.json";
        public Storage() => data = new ScheduleData();
        public void Load() {
            if (File.Exists(FilePath)) {
                string serializedData = File.ReadAllText(FilePath);
                if (new FileInfo(FilePath).Length != 0) 
                    data = JsonConvert.DeserializeObject<ScheduleData>(serializedData);
            }
        }
        public void Save() {
            string serializedData = JsonConvert.SerializeObject(data);
            File.WriteAllText(FilePath, serializedData);
        }
    }
}