using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Timetabled.Data;
using Timetabled.GUI;

namespace Timetabled.Helpers {
    public static class Extensions {
        public static ScheduleData Copy(this ScheduleData data) {
            var copy = new ScheduleData() {
                groups = data.groups.ToList(),
                subjects = data.subjects.ToList(),
                teachers = data.teachers.ToList(),
                rooms = data.rooms.ToList()
            };
            return copy;
        }
        public static void AssignFrom(this ScheduleData data, ScheduleData from) {
            data.groups = from.groups.ToList();
            data.subjects = from.subjects.ToList();
            data.teachers = from.teachers.ToList();
            data.rooms = from.rooms.ToList();
        }
    }
}
