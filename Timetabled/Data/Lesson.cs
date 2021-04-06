using System;
using System.Runtime.Serialization;

namespace Timetabled.Data {
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
