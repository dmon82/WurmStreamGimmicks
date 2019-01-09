using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WurmStreamGimmicks {
    class Config {
        public string Filename { get; protected set; }
        public string PlayersFolder { get; set; }
        public List<IGimmick> Gimmicks { get; protected set; }
        public TimeSpan GimmickSwitchInterval { get; protected set; }
        
        public Config(string filename) {
            Filename = filename;
            Gimmicks = new List<IGimmick>();
            PlayersFolder = string.Empty;
            GimmickSwitchInterval = TimeSpan.Zero;
        }

        public Config(MyReader reader) {
            Deserialise(reader);
        }

        public Config() {
        }

        internal static Config Initialise(string filename) {
            Core.Logger.Log(LogLevel.Config, "Initialising config file for the first time.");
            Config config = new Config(filename);

            Core.Logger.Log(LogLevel.Config, "Opening config file '{0}' for writing.", filename);
            MyWriter writer = new MyWriter(filename);

            config.Serialise(writer);

            Core.Logger.Log(LogLevel.Config, "Flushing and closing writer.");
            writer.Flush();
            writer.Close();
            writer = null;

            Core.Logger.Log(LogLevel.Config, "Returning fresh config file.");
            return config;
        }

        public void Serialise(MyWriter writer) {
            Core.Logger.Log(LogLevel.Debug, "Serialising config file.");

            writer.Write((int)0); // Version.

            writer.Write(Filename);
            writer.Write(PlayersFolder);
            writer.Write(GimmickSwitchInterval.Ticks);

            Core.Logger.Log(LogLevel.Debug, "Serialising gimmicks.");

            writer.Write(Gimmicks.Count);
            foreach (IGimmick gimmick in Gimmicks) {
                writer.Write(gimmick.GetType().FullName);
                gimmick.Serialise(writer);
            }

            Core.Logger.Log(LogLevel.Debug, "Serialisation complete.");
        }

        public void Deserialise(MyReader reader) {
            Core.Logger.Log(LogLevel.Debug, "Deserialising config file.");

            int version = reader.ReadInt();

            Filename = reader.ReadString();
            PlayersFolder = reader.ReadString();
            GimmickSwitchInterval = TimeSpan.FromTicks(reader.ReadLong());

            Core.Logger.Log(LogLevel.Debug, "Deserialising gimmicks.");

            int count = reader.ReadInt();
            Gimmicks = new List<IGimmick>();

            while (count-- > 0) {
                string fullname = reader.ReadString();
                IGimmick gimmick = (IGimmick)Activator.CreateInstance(Type.GetType(fullname, true, false), reader);
                Gimmicks.Add(gimmick);
            }

            Core.Logger.Log(LogLevel.Debug, "Deserialisation complete.");
        }
    }
}
