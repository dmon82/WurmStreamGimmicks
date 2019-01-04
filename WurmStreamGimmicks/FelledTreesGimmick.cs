using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WurmStreamGimmicks {
    class FelledTreesGimmick : BaseGimmick {
        private int _TreeCount = 0;

        public FelledTreesGimmick()
            : base("Felled trees", "You cut down the", true, false, false) {
        }

        public override bool Watch(string line) {
            //System.Text.RegularExpressions.Regex.IsMatch(line, "You cut down the")
            if (line.Contains(this.Phrase)) {
                _TreeCount++;

                return this.Dirty = true;
            }

            return false;
        }

        public override string ToString() {
            base.ToString();
            return String.Format("Felled trees: {0:N0}", _TreeCount);
        }
    }
}
