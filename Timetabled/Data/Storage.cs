using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization;
using Newtonsoft.Json;

using GroupToLessons = System.Collections.Generic
    .Dictionary<string, Timetabled.Data.Lesson[]>;

namespace Timetabled.Data {
    public class Storage {        
        public Storage() {
            Data = new ScheduleData();
            Settings = new AppSettings();
            Schedules = new Dictionary<DateTime, GroupToLessons>();
        }

        // Stored items
        public ScheduleData Data { get; set; }
        public AppSettings Settings { get; set; }
        public Dictionary<DateTime, GroupToLessons> Schedules { get; set; }

        // JSON Convertation
        public static JsonSerializerSettings JsonSettings => new JsonSerializerSettings {            
            Formatting = Formatting.Indented,
            DateFormatString = "yyyy-MM-dd"
        };
        private string DataFilepath => "data\\scheduleData.json";
        private string SchedulesFilepath => "data\\schedules.json";
        private string SettingsFilepath => "data\\settings.json";

        public void Load() {
            string serializedData;
            var path = Path.GetFullPath("data");
            if (Directory.Exists(path)) {
                if (File.Exists(DataFilepath)) {
                    serializedData = File.ReadAllText(DataFilepath);
                    if (new FileInfo(DataFilepath).Length != 0) {
                        Data = JsonConvert.DeserializeObject<ScheduleData>(serializedData, JsonSettings);
                    }
                }
                if (File.Exists(SchedulesFilepath)) {
                    serializedData = File.ReadAllText(SchedulesFilepath);
                    if (new FileInfo(SchedulesFilepath).Length != 0) {
                        Schedules = (Dictionary<DateTime, GroupToLessons>)JsonConvert
                            .DeserializeObject(serializedData, Schedules.GetType(), JsonSettings);
                    }
                }
                if (File.Exists(SettingsFilepath)) {
                    serializedData = File.ReadAllText(SettingsFilepath);
                    if (new FileInfo(SettingsFilepath).Length != 0) {
                        Settings = JsonConvert.DeserializeObject<AppSettings>(serializedData, JsonSettings);
                    }
                }
            } else Directory.CreateDirectory(path);            
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
}