using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace WurmStreamGimmicks {
    /*class PartsRemainingGimmick : BaseGimmick {
        private bool _Initialised = false;
        private int _TotalParts = 0;
        private int _Attached = 0;
        private string _Item = string.Empty;

        public PartsRemainingGimmick()
            : base("Parts remaining", " to be finished.", true, false, false) {
        }

        public override bool Watch(string line) {
            if (!_Initialised && line.EndsWith(Phrase)) {
                // [11:29:28] You see a caravel under construction. Ql: 1.5432862, Dam: 0.0. The caravel needs 299 tenon, and 600 peg, and 150 tar, and 400 hull plank, and 80 deck board to be finished.
                _Item = Regex.Match(line, "You see a (.+) under construction").Groups[1].Value;
                _Item = _Item.Substring(0, 1).ToUpper() + _Item.Substring(1);
                Console.WriteLine(":: Initialising '{0}' under construction", _Item);

                string result = Regex.Match(line, " needs (.+) to be finished").Groups[1].Value;
                MatchCollection matches = Regex.Matches(result, @"\d+");

                int count;

                foreach (Match match in matches) {
                    count = int.Parse(match.Value);
                    _TotalParts += count;
                    Console.WriteLine(":: Adding {0} parts to the total required, now at {1}.", count, _TotalParts);
                }

                _Initialised = true;
                Dirty = true;

                return true;
            }
            else if (Regex.IsMatch(line, String.Format(@"You attach.+to the {0}", _Item.ToLower())) || Regex.IsMatch(line, String.Format(@"attaches.+to the {0}", _Item.ToLower()))) {
                Console.WriteLine(":: Part was attached to {0} [{1}/{2}] ({3})", _Item, _Attached, _TotalParts, line);
                _Attached++;
                Dirty = true;

                return true;
            }

            return false;
        }

        public override string ToString() {
            base.ToString();

            return String.Format("{0} parts remaining: {1:N0}", _Item, _TotalParts - _Attached);
        }
    }*/
}
