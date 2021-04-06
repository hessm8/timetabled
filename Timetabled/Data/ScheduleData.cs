using System;
using System.Collections.Generic;

namespace Timetabled.Data {
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
}
