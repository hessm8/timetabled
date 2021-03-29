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

namespace Timetabled {
    public class Storage {
        public ScheduleData data;
        private string filePath;

        public Storage() {
            data = new ScheduleData();
            filePath = "data.json";
        }

        public void Load() {
            if (File.Exists(filePath)) {
                string serializedData = File.ReadAllText(filePath);
                if (new FileInfo(filePath).Length != 0) 
                    data = JsonConvert.DeserializeObject<ScheduleData>(serializedData);
            }
        }
        public void Save() {
            string serializedData = JsonConvert.SerializeObject(data);
            File.WriteAllText(filePath, serializedData);
        }

        public class ScheduleData {
            public ScheduleData() {
                groups = new List<string>();
                subjects = new List<string>();
                teachers = new List<string>();
                rooms = new List<string>();
            }

            public List<string> groups { get; set; }
            public List<string> subjects { get; set; }
            public List<string> teachers { get; set; }
            public List<string> rooms { get; set; }
        }
    }
}
