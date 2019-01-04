using System;
using System.IO;

namespace WurmStreamGimmicks {
    internal class MyWriter : BinaryWriter {
        public MyWriter(string filename)
            : base(new FileStream(filename, FileMode.Create, FileAccess.Write, FileShare.Read), System.Text.Encoding.Unicode) {
        }

        public void Write(DateTime value) {
            Write(value.ToUniversalTime().Ticks);
        }
    }
}
