using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace WurmStreamGimmicks {
    class SkillGainGimmick : BaseGimmick, IGimmick {
        public static readonly string Tooltip = "idk yet";
        public override string Help { get { return SkillGainGimmick.Tooltip; } }
        private static Regex _Regex = new Regex(@"(\w[\w\s]+) increased by (\d+\.\d+) to (\d+\.\d+)");

        /// <summary>
        ///     Gets or sets the skill name monitored.
        /// </summary>
        public string SkillName { get; set; }

        /// <summary>
        ///     Gets or sets the number of skill ticks.
        /// </summary>
        public int Ticks { get; set; }

        /// <summary>
        ///     Gets or sets the total amount of skill gained.
        /// </summary>
        public double Gain { get; set; }

        /// <summary>
        ///     Gets or sets the smallest skill tick/
        /// </summary>
        public double SmallestGain { get; set; }

        /// <summary>
        ///     Gets or sets the biggest skill tick.
        /// </summary>
        public double LargestGain { get; set; }

        public SkillGainGimmick() : base("", LogType.Skills, "", false, "", "", new List<string>()) { }
        public override string Compile() {
            return "override compile";
        }

        public void Watch(string line, Player player) {
            Match match = _Regex.Match(line);
            
            if (!match.Success) return;
        }

        public void Serialise(MyWriter writer) {
            base.Serialise(writer);
            writer.Write((int)0); // File format vesioning.
        }

        public void Deserialise(MyReader reader) {
            base.Deserialise(reader);
            int version = reader.ReadInt();
        }
    }
}
