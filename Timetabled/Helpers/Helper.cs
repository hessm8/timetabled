using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Timetabled.Helpers {
    public static class Helper {
        private static int randomSeed = 0;
        public static int Random(int count) {
            randomSeed %= 100;
            return new Random(randomSeed++).Next(count);
        }
    }
}
