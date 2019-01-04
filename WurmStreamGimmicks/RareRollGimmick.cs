using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WurmStreamGimmicks {
    class RareRollGimmick : BaseGimmick {
        public int Rolls { get; protected set; }

        public RareRollGimmick()
            : base("Rare rolls", "You have a moment of inspiration", true, false, false) {
                Dirty = true;
        }

        public override bool Watch(string line) {
            if (line.Contains(this.Phrase)) {
                Rolls++;

                return true;
            }

            return false;
        }

        public override string ToString() {
            base.ToString();

            return String.Format("Rare rolls: {0:N0}", Rolls);
        }
    }
}
