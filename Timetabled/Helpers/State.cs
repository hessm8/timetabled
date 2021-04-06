using System;

namespace Timetabled.Helpers {
    public class State<TField> {
        public TField Previous { get; private set; }
        public TField Latest { get; private set; }
        private Func<TField> UpdateWith { get; }
        public State(Func<TField> updateGetter) {
            UpdateWith = updateGetter;
            Latest = UpdateWith();
            Previous = UpdateWith();
        }
        public void Update() {
            Previous = Latest;
            Latest = UpdateWith();
        }
    }
}
