using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WurmStreamGimmicks {
    class Player {
        public static Dictionary<string, Player> Table = new Dictionary<string, Player>();

        public string Name { get; protected set; }
        private FileWatcher EventsWatcher;
        private FileWatcher CombatWatcher;
        private FileWatcher SkillsWatcher;
        private StreamReader EventsReader;
        private StreamReader CombatReader;
        private StreamReader SkillsReader;

        private List<BaseGimmick> _Gimmicks = new List<BaseGimmick>();
        public List<BaseGimmick> Gimmicks {
            get { return _Gimmicks; }
        }

        public BaseGimmick this[int index] {
            get {
                if (index < 0 || index >= _Gimmicks.Count)
                    return null;

                return _Gimmicks[index];
            }
        }

        public Player(string name) {
            Name = name;

            string eventFile = String.Format(@"d:\games\wurm\players\{0}\logs\_Event.{1}-{2:D2}.txt", name, DateTime.Now.Year, DateTime.Now.Month);
            string combatFile = String.Format(@"d:\games\wurm\players\{0}\logs\_Combat.{1}-{2:D2}.txt", name, DateTime.Now.Year, DateTime.Now.Month);
            string skillsFile = String.Format(@"d:\games\wurm\players\{0}\logs\_Skills.{1}-{2:D2}.txt", name, DateTime.Now.Year, DateTime.Now.Month);

            EventsWatcher = new FileWatcher(eventFile);
            CombatWatcher = new FileWatcher(combatFile);
            SkillsWatcher = new FileWatcher(skillsFile);

            EventsReader = new StreamReader(new FileStream(eventFile, FileMode.Open, FileAccess.Read, FileShare.ReadWrite));
            CombatReader = new StreamReader(new FileStream(combatFile, FileMode.Open, FileAccess.Read, FileShare.ReadWrite));
            SkillsReader = new StreamReader(new FileStream(skillsFile, FileMode.Open, FileAccess.Read, FileShare.ReadWrite));
        }

        public void BeginWatch() {
            EventsWatcher.EnableRaisingEvents =
                CombatWatcher.EnableRaisingEvents =
                SkillsWatcher.EnableRaisingEvents = true;

            EventsReader.ReadToEnd();
            CombatReader.ReadToEnd();
            SkillsReader.ReadToEnd();

            EventsWatcher.Changed += EventsWatcher_Changed;
            CombatWatcher.Changed += CombatWatcher_Changed;
            SkillsWatcher.Changed += SkillsWatcher_Changed;
        }

        void SkillsWatcher_Changed(object sender, FileSystemEventArgs e) {
            string line = string.Empty;

            while (!string.IsNullOrWhiteSpace(line = SkillsReader.ReadLine()))
                Watch(line);
        }

        void CombatWatcher_Changed(object sender, FileSystemEventArgs e) {
            string line = string.Empty;

            while (!string.IsNullOrWhiteSpace(line = CombatReader.ReadLine()))
                Watch(line);
        }

        void EventsWatcher_Changed(object sender, System.IO.FileSystemEventArgs e) {
            string line = string.Empty;

            while (!string.IsNullOrWhiteSpace(line = EventsReader.ReadLine()))
                Watch(line);
        }

        public void Watch(string line) {
            int count = _Gimmicks.Count;

            for (int i = 0; i < count; i++)
                this[i].Watch(line);
        }

        public void EndWatch() {
            EventsWatcher.EnableRaisingEvents =
                CombatWatcher.EnableRaisingEvents =
                SkillsWatcher.EnableRaisingEvents = false;
        }

        public override string ToString() {
            return String.Format("{0} [{1}]", Name, _Gimmicks.Count);
        }
    }
}
