using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WurmStreamGimmicks {
    static class Extentions {
        public static bool EqualsToTheSecond(this DateTime value, DateTime compare) {
            return value.Hour.Equals(compare.Hour) &&
                value.Minute.Equals(compare.Minute) &&
                value.Second.Equals(compare.Second);
        }
    }
}
