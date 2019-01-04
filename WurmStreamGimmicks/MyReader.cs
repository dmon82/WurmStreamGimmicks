using System;
using System.IO;

namespace WurmStreamGimmicks {
    internal class MyReader : BinaryReader {
        public MyReader(string filename)
            : base(new FileStream(filename, FileMode.Open, FileAccess.Read, FileShare.Read), System.Text.Encoding.Unicode) {
        }

        public DateTime ReadDateTime() {
            return new DateTime(ReadLong(), DateTimeKind.Utc);
        }

        public short ReadShort() {
            return ReadInt16();
        }

        public ushort ReadUShort() {
            return ReadUInt16();
        }

        public int ReadInt() {
            return ReadInt32();
        }

        public uint ReadUInt() {
            return ReadUInt32();
        }

        public float ReadFloat() {
            return ReadSingle();
        }

        public long ReadLong() {
            return base.ReadInt64();
        }

        public ulong ReadULong() {
            return base.ReadUInt64();
        }
    }
}
