using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace WurmStreamGimmicks {
    class Config {
        public string Filename { get; protected set; }
        public string PlayersFolder { get; set; }
        public List<IGimmick> Gimmicks { get; protected set; }
        public Size MainWindowSize { get; set; }
        public int[] GimmickColumnSize { get; set; }

        public Config(string filename) {
            Filename = filename;
            Gimmicks = new List<IGimmick>();
            PlayersFolder = string.Empty;
            MainWindowSize = new Size(569, 529);
            GimmickColumnSize = new int[] { 78, 112, 42, 273 };
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
            writer.Write(MainWindowSize.Width);
            writer.Write(MainWindowSize.Height);
            writer.Write(GimmickColumnSize.Length);
            for (int i = 0; i < GimmickColumnSize.Length; i++)
                writer.Write(GimmickColumnSize[i]);

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
            MainWindowSize = new Size(reader.ReadInt(), reader.ReadInt());
            
            int count = reader.ReadInt();
            GimmickColumnSize = new int[count];
            for (int i = 0; i < count; i++)
                GimmickColumnSize[i] = reader.ReadInt();

            Core.Logger.Log(LogLevel.Debug, "Deserialising gimmicks.");

            count = reader.ReadInt();
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
