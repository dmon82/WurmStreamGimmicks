using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WurmStreamGimmicks {
    /// <summary>
    ///     Log types as a bitmask
    /// </summary>
    enum LogType {
        None = 0,
        Events = 1,
        Combat = 2,
        Skills = 4
    }
}
