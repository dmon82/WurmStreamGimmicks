using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WurmStreamGimmicks {
    class Player {
        public static Dictionary<string, Player> Table = new Dictionary<string, Player>();
        static Player() {
            string[] playernames = Directory.GetDirectories(Core.Config.PlayersFolder);
            foreach (string playername in playernames) {
                string substr = playername.Substring(playername.LastIndexOf('\\') + 1);
                Table.Add(substr, new Player(substr));
                substr = null;
            }
            playernames = null;
        }

        private bool _Watching = false;
        public bool Watching { get { return _Watching; } }

        public string Name { get; protected set; }
        private FileWatcher EventsWatcher;
        private FileWatcher CombatWatcher;
        private FileWatcher SkillsWatcher;
        private StreamReader EventsReader;
        private StreamReader CombatReader;
        private StreamReader SkillsReader;

        private List<IGimmick> _Gimmicks = new List<IGimmick>();
        public List<IGimmick> Gimmicks {
            get { return _Gimmicks; }
        }

        public IGimmick this[int index] {
            get {
                if (index < 0 || index >= _Gimmicks.Count)
                    return null;

                return _Gimmicks[index];
            }
        }

        public Player(string name) {
            Name = name;
        }

        public void BeginWatch() {
            Core.Logger.Log(LogLevel.Info, "Starting watch on {0}.", this);

            if (!_Watching) {
                Core.Logger.Log(LogLevel.Info, "Have to initialise watch on {0} first though.", this);

                string eventFile = String.Format(@"d:\games\wurm\players\{0}\logs\_Event.{1}-{2:D2}.txt", this.Name, DateTime.Now.Year, DateTime.Now.Month);
                string combatFile = String.Format(@"d:\games\wurm\players\{0}\logs\_Combat.{1}-{2:D2}.txt", this.Name, DateTime.Now.Year, DateTime.Now.Month);
                string skillsFile = String.Format(@"d:\games\wurm\players\{0}\logs\_Skills.{1}-{2:D2}.txt", this.Name, DateTime.Now.Year, DateTime.Now.Month);

                EventsWatcher = new FileWatcher(eventFile);
                CombatWatcher = new FileWatcher(combatFile);
                SkillsWatcher = new FileWatcher(skillsFile);

                EventsReader = new StreamReader(new FileStream(eventFile, FileMode.Open, FileAccess.Read, FileShare.ReadWrite));
                CombatReader = new StreamReader(new FileStream(combatFile, FileMode.Open, FileAccess.Read, FileShare.ReadWrite));
                SkillsReader = new StreamReader(new FileStream(skillsFile, FileMode.Open, FileAccess.Read, FileShare.ReadWrite));

                EventsWatcher.EnableRaisingEvents =
                    CombatWatcher.EnableRaisingEvents =
                    SkillsWatcher.EnableRaisingEvents = true;
            }

            EventsReader.ReadToEnd();
            CombatReader.ReadToEnd();
            SkillsReader.ReadToEnd();

            if (!_Watching) {
                Core.Logger.Log(LogLevel.Info, "Adding watch callbacks on {0}.", this);

                EventsWatcher.Changed += EventsWatcher_Changed;
                CombatWatcher.Changed += CombatWatcher_Changed;
                SkillsWatcher.Changed += SkillsWatcher_Changed;
                _Watching = true;
            }
        }

        void SkillsWatcher_Changed(object sender, FileSystemEventArgs e) {
            string line = string.Empty;

            while (!string.IsNullOrWhiteSpace(line = SkillsReader.ReadLine()))
                Watch(line, LogType.Skills);
        }

        void CombatWatcher_Changed(object sender, FileSystemEventArgs e) {
            string line = string.Empty;

            while (!string.IsNullOrWhiteSpace(line = CombatReader.ReadLine()))
                Watch(line, LogType.Combat);
        }

        void EventsWatcher_Changed(object sender, System.IO.FileSystemEventArgs e) {
            string line = string.Empty;

            while (!string.IsNullOrWhiteSpace(line = EventsReader.ReadLine()))
                Watch(line, LogType.Events);
        }

        public void Watch(string line, LogType logType) {
            int count = _Gimmicks.Count;

            for (int i = 0; i < count; i++)
                if (this[i].Enabled && (this[i].Logs & logType) == logType) this[i].Watch(line, this);
        }

        public void EndWatch() {
            if (!_Watching)
                return;

            Core.Logger.Log(LogLevel.Info, "Ending watch on {0}.", this);

            EventsWatcher.EnableRaisingEvents =
                CombatWatcher.EnableRaisingEvents =
                SkillsWatcher.EnableRaisingEvents = false;

            EventsWatcher.Changed -= EventsWatcher_Changed;
            CombatWatcher.Changed -= CombatWatcher_Changed;
            SkillsWatcher.Changed -= SkillsWatcher_Changed;

            _Watching = false;
        }

        public void Dispose() {
            if (_Watching)
                EndWatch();
            
            if (EventsWatcher != null) EventsWatcher.Dispose();
            EventsWatcher = null;

            if (CombatWatcher != null) CombatWatcher.Dispose();
            CombatWatcher = null;

            if (SkillsWatcher != null) SkillsWatcher.Dispose();
            SkillsWatcher = null;

            if (EventsReader != null) EventsReader.Dispose();
            EventsReader = null;

            if (CombatReader != null) CombatReader.Dispose();
            CombatReader = null;

            if (SkillsReader != null) SkillsReader.Dispose();
            SkillsReader = null;

            _Gimmicks.Clear();
            _Gimmicks = null;

            this.Name = null;
        }

        public override bool Equals(object obj) {
            if (obj == null) return false;

            if (obj is string)
                return ((string)obj).Equals(this.Name);

            if (obj is Player)
                return ((Player)obj).Name.Equals(this.Name);

            return false;
        }

        public override int GetHashCode() {
            return this.Name.GetHashCode();
        }

        public override string ToString() {
            return String.Format("{0} [{1}]", Name, _Gimmicks.Count);
        }
    }
}
