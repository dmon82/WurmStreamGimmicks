using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WurmStreamGimmicks {
    class CounterGimmick : IGimmick {
        public static readonly string Tooltip = "%c = global counter, %s = session counter";

        public string Name { get; set; }
        public LogType Logs { get; set; }
        public string Template { get; set; }
        public bool Collective { get; set; }
        public List<string> Players { get; set; } // TODO as string, no reference, since we replace the objects a lot atm in frmMain.CheckPlayersFolder()
        public string Help { get { return CounterGimmick.Tooltip; } }

        public string Pattern { get; set; }
        public int SessionCount { get; set; }
        public int GlobalCount { get; set; }
        public bool Events { get; set; }
        public bool Combat { get; set; }
        public bool Skills { get; set; }
        public string OutputFile { get; set; }
        public bool Enabled { get; set; }

        public CounterGimmick(string name, string pattern, string template, bool collective, bool events, bool combat, bool skills, List<string> players) {
            Name = name;
            Pattern = pattern;
            Template = template;
            Collective = collective;
            Players = players;
            SessionCount = 0;
            GlobalCount = 0;
            Events = events;
            Combat = combat;
            Skills = skills;
            OutputFile = ".\\output.txt";
            Enabled = false;
        }

        public CounterGimmick(string name, string pattern, string template, bool collective, bool events, bool combat, bool skills, params string[] players)
            : this(name, pattern, template, collective, events, combat, skills, players.ToList()) {
        }

        public CounterGimmick(MyReader reader) {
            Players = new List<string>();
            Deserialise(reader);
            SessionCount = 0;
        }

        public void Watch(string line, Player player) {
            Core.Logger.Log(LogLevel.Finer, "{0} watching line '{1}'.", this.Name, line);

            if (System.Text.RegularExpressions.Regex.IsMatch(line, Pattern)) {
                SessionCount++;
                GlobalCount++;
                Write();
            }
        }

        public string Compile() {
            string compiled = Template.Replace("%c", GlobalCount.ToString("N0")).Replace("%s", SessionCount.ToString("N0"));

            Core.Logger.Log(LogLevel.Finer, "{0} compiling {1} into {2}.", Name, Template, compiled);

            return compiled;
        }

        private void Write() {  
            try {
                System.IO.File.WriteAllText(OutputFile, Compile());
            }
            catch (Exception e) {
                Core.Logger.Log(LogLevel.Severe, "{0} could not write its output to '{1}'.", Name, OutputFile);
                Core.Logger.Log(LogLevel.Severe, e.ToString());
            }
        }

        public void Serialise(MyWriter writer) {
            writer.Write((int)0); // Version.

            writer.Write(Name);
            writer.Write((int)Logs);
            writer.Write(Pattern);
            writer.Write(Template);
            writer.Write(Collective);
            writer.Write(Players.Count);
            foreach (string player in Players) writer.Write(player);
            writer.Write(SessionCount);
            writer.Write(GlobalCount);
            writer.Write(Events);
            writer.Write(Combat);
            writer.Write(Skills);
            writer.Write(OutputFile);
            writer.Write(Enabled);
        }

        public void Deserialise(MyReader reader) {
            int version = reader.ReadInt();

            Name = reader.ReadString();
            Logs = (LogType)reader.ReadInt();
            Pattern = reader.ReadString();
            Template = reader.ReadString();
            Collective = reader.ReadBoolean();
            int count = reader.ReadInt();
            while (count-- > 0) Players.Add(reader.ReadString());
            SessionCount = reader.ReadInt();
            GlobalCount = reader.ReadInt();
            Events = reader.ReadBoolean();
            Combat = reader.ReadBoolean();
            Skills = reader.ReadBoolean();
            OutputFile = reader.ReadString();
            Enabled = reader.ReadBoolean();
        }
    }
}
