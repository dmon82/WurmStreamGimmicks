using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WurmStreamGimmicks {
    abstract class BaseGimmick {
        private static BaseGimmick[] _Gimmicks = new BaseGimmick[] {
            new RareRollGimmick(),
            new PartsRemainingGimmick(),
            new FelledTreesGimmick(),
        };
        public static BaseGimmick[] Gimmicks { get { return _Gimmicks; } }
        
        public string Name { get; protected set; }
        public string Phrase { get; protected set; }
        public bool Events { get; protected set; }
        public bool Combat { get; protected set; }
        public bool Skills { get; protected set; }
        public bool Dirty { get; protected set; }
                
        public static int Count { get { return _Gimmicks.Length; } }
        
        public BaseGimmick(string name, string phrase, bool watchEvent, bool watchCombat, bool watchSkill) {
            Name = name;
            Phrase = phrase;
            Events = watchEvent;
            Combat = watchCombat;
            Skills = watchSkill;
        }

        abstract public bool Watch(string line);

        public override string ToString() {
            Dirty = false;

            return Name;
        }
    }
}
