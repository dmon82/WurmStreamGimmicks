using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WurmStreamGimmicks {
    class Config {
        public string Filename { get; protected set; }
        public string PlayersFolder { get; set; }

        public Config(string filename) {
            Filename = filename;
        }

        public Config(MyReader reader) {
            Deserialise(reader);
        }

        public void Serialise(MyWriter writer) {
            writer.Write((int)0); // Version.

            writer.Write(Filename);
            writer.Write(PlayersFolder);
        }

        public void Deserialise(MyReader reader) {
            int version = reader.ReadInt();

        }
    }
}
