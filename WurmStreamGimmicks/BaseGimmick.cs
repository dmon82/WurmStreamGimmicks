using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WurmStreamGimmicks {
    abstract class BaseGimmick {
        public virtual string Name { get; set; }
        public virtual LogType Logs { get; set; }
        public virtual string Template { get; set; }
        public virtual bool Collective { get; set; }
        public virtual List<string> Players { get; set; }
        public abstract string Help { get; }

        public virtual string Pattern { get; set; }
        public virtual bool Events { get; set; }
        public virtual  bool Combat { get; set; }
        public virtual bool Skills { get; set; }
        public virtual string OutputFile { get; set; }
        public virtual bool Enabled { get; set; }

        public BaseGimmick(string name, LogType logType, string template, bool collective, string pattern, string outputFile, List<string> players) {
            Name = name;
            Logs = logType;
            Template = template;
            Collective = collective;
            Pattern = pattern;
            OutputFile = outputFile;
            Players = players;
            Enabled = false;

            Events = (logType & LogType.Events) == LogType.Events;
            Combat = (logType & LogType.Combat) == LogType.Combat;
            Skills = (logType & LogType.Skills) == LogType.Skills;
        }

        public BaseGimmick(string name, LogType logType, string template, bool collective, string pattern, string outputFile, params string[] players)
            : this(name, logType, template, collective, pattern, outputFile, players.ToList()) {
        }

        public BaseGimmick(MyReader reader) {
            Deserialise(reader);
        }

        public virtual void Write() {
            try {
                System.IO.File.WriteAllText(OutputFile, Compile());
            }
            catch (Exception e) {
                Core.Logger.Log(LogLevel.Severe, "{0} could not update its output file '{1}'.", Name, OutputFile);
                Core.Logger.Log(LogLevel.Severe, e.ToString());
            }
        }

        public abstract string Compile();

        public virtual void Serialise(MyWriter writer) {
            writer.Write((int)0); // File versioning.

            writer.Write(Name);
            writer.Write((int)Logs);
            writer.Write(Template);
            writer.Write(Collective);
            writer.Write(Players.Count);
            Players.ForEach(x => writer.Write(x));

            writer.Write(Pattern);
            writer.Write(OutputFile);
            writer.Write(Enabled);
        }

        public virtual void Deserialise(MyReader reader) {
            int version = reader.ReadInt();

            Name = reader.ReadString();
            Logs = (LogType)reader.ReadInt();
            Template = reader.ReadString();
            Collective = reader.ReadBoolean();
            int count = reader.ReadInt();
            if (count > 0) Players = new List<string>();
            while (count-- > 0) Players.Add(reader.ReadString());
            
            Pattern = reader.ReadString();
            OutputFile = reader.ReadString();
            Enabled = reader.ReadBoolean();
        }
    }
}
