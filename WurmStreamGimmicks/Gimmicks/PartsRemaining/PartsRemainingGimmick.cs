using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace WurmStreamGimmicks {
    class PartsRemainingGimmick : IGimmick {
        public static readonly string Tooltip = "%t = total part, %r = remaining, %a = attached, %i = itemname\r\nLeave \"Item name\" empty and examine the unfinished item instead!";

        public string Name { get; set; }
        public LogType Logs { get; set; }
        public string Template { get; set; }
        public bool Collective { get; set; }
        public List<string> Players { get; set; }
        public string Help { get { return PartsRemainingGimmick.Tooltip; } }

        public string Pattern { get; set; }
        public bool Events { get; set; }
        public bool Combat { get; set; }
        public bool Skills { get; set; }
        public string OutputFile { get; set; }
        public bool Enabled { get; set; }

        public string Itemname { get; set; }
        public int TotalParts { get; set; }
        public int PartsAttached { get; set; }

        private bool _ThirdParty = false;
        /// <summary>
        ///     Gets or sets if characters that aren't monitored directly should count towards attached parts.
        /// </summary>
        public bool ThirdParty {
            get { return _ThirdParty; }
            set {
                if (value == _ThirdParty) return;

                if (value) _ThirdParties = new Dictionary<string, DateTime>();
                else _ThirdParties = null;

                _ThirdParty = value;
            }
        }

        /// <summary>
        ///     Tracks the time stamps of third parties attaching parts. This is used when two or more local
        ///     characters are monitored, seeing an outside party character attaching parts.
        /// </summary>
        private Dictionary<string, DateTime> _ThirdParties;

        public PartsRemainingGimmick(string name, string pattern, string template, bool collective, bool events, bool combat, bool skills, List<string> players) {
            Name = name;
            Pattern = pattern;
            Template = template;
            Collective = collective;
            Players = players;
            Events = events;
            Combat = combat;
            Skills = skills;
            OutputFile = ".\\output.txt";
            Enabled = false;
            Itemname = string.Empty;
            TotalParts = 0;
            PartsAttached = 0;
        }

        public PartsRemainingGimmick(string name, string pattern, string template, bool collective, bool events, bool combat, bool skills, params string[] players)
            : this(name, pattern, template, collective, events, combat, skills, players.ToList()) {
        }

        public PartsRemainingGimmick(MyReader reader) {
            Players = new List<string>();
            Deserialise(reader);
        }

        public string Compile() {
            return Template.Replace("%t", TotalParts.ToString("N0"))
                .Replace("%r", (TotalParts - PartsAttached).ToString("N0"))
                .Replace("%a", PartsAttached.ToString("N0"))
                .Replace("%i", Itemname);
        }

        public void Watch(string line, Player player) {
            Core.Logger.Log(LogLevel.Finer, "{0} watching line '{1}'.", Name, line);

            // Matches against this line, grouping it by item name (group 1), and the listing of parts (group 2).
            Match match = Regex.Match(line, "You see a (.+) under construction.+needs (.+) to be finished");

            // The match has to be a complete success to be considered an examine on an unfinished item.
            if (match.Success) {
                Core.Logger.Log(LogLevel.Config, "{0} not initialised, unfinished item was examined.", Name);

                // Capitalise the first letter or not?
                string itemName = match.Groups[1].Value.Substring(0, 1).ToUpperInvariant() +
                    match.Groups[1].Value.Substring(1);

                if (string.Empty.Equals(this.Itemname) || itemName.Equals(Itemname, StringComparison.InvariantCultureIgnoreCase)) {
                    Core.Logger.Log(LogLevel.Config, "{0} will use {1} as the unfinished item or update it.", Name, Itemname);

                    // Match only digit characters into a match collection.
                    MatchCollection matches = Regex.Matches(match.Groups[2].Value, @"\d+");
                    int count;

                    // Try and parse them all, "should" never fail but using TryParse anyway.
                    foreach (Match part in matches) {
                        if (int.TryParse(part.Value, out count)) {
                            Core.Logger.Log(LogLevel.Config, "Adding {0:N0} parts to {1:N0} total parts required.", count, TotalParts);
                            TotalParts += count;
                        }
                        else {
                            Core.Logger.Log(LogLevel.Severe, "'{0}' could not be parsed as a number of parts of the unfinished item.", part.Value);
                        }
                    }

                    matches = null;

                    Core.Logger.Log(LogLevel.Config, "{0:N0} total parts required for the unfinished {1}", TotalParts, Itemname);

                    this.Pattern = String.Format("You attach.+to the {0}", Itemname.ToLowerInvariant());
                    Core.Logger.Log(LogLevel.Config, "Forcing {0}'s trigger pattern to '{1}'.", this.Name, this.Pattern);

                    // Update the output file.
                    Write();
                }
            } // if (match.success)
            else {
                Core.Logger.Log(LogLevel.Fine, "The line does not seem to describe an unfinished item under construction.");
            } // end if (match.Success)

            match = null;

            // While the item name is empty, the gimmick is considered uninitialised.
            if (!string.Empty.Equals(this.Itemname)) {
                if (Regex.IsMatch(line, Pattern)) {
                    PartsAttached++;
                    Write();

                    Core.Logger.Log(LogLevel.Fine, "{0:N0} parts have been attached to {1}, {2:N0} parts remaining.", PartsAttached, Itemname, TotalParts - PartsAttached);
                }

                // Do we monitor characters outside the selected ones at all?
                if (ThirdParty) {
                    match = Regex.Match(line, String.Format(@"(\w+) attaches.+to the {0}", Itemname));

                    // Does this line match the third party character pattern?
                    if (match.Success && !Players.Contains(match.Groups[1].Value)) {
                        // Have we seen this character attach parts before?
                        if (_ThirdParties.ContainsKey(match.Groups[1].Value)) {
                            // Has this message been seen in the same second?
                            if (_ThirdParties[match.Groups[1].Value].EqualsToTheSecond(DateTime.Now)) {
                                Core.Logger.Log(LogLevel.Finer, "{0} found duplicate third party character message from {1}.", Name, match.Groups[1].Value);
                            }
                            else {
                                Core.Logger.Log(LogLevel.Finer, "{0} found unique or first third party character message from {1}.", Name, match.Groups[1].Value);
                                _ThirdParties[match.Groups[1].Value] = DateTime.Now;
                                PartsAttached++;
                                Write();
                            } // end if (seen in same second?)
                        } // if (seen character before?)
                        else {
                            Core.Logger.Log(LogLevel.Finer, "{0} found first third party character {1} attaching parts.", Name, match.Groups[1].Value);
                            _ThirdParties.Add(match.Groups[1].Value, DateTime.Now);
                            PartsAttached++;
                            Write();
                        } // end if (seen this character before?)
                    } // end if (regex match on 3rd party character?)

                    match = null;
                } // end if (ThirdParty)
            } // end if (string.Empty.Equals(this.Itemname))
        }

        private void Write() {
            try {
                System.IO.File.WriteAllText(OutputFile, Compile());
            }
            catch (Exception e) {
                Core.Logger.Log(LogLevel.Severe, "{0} could not update its output file '{1}'.", Name, OutputFile);
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
            writer.Write(Events);
            writer.Write(Combat);
            writer.Write(Skills);
            writer.Write(OutputFile);
            writer.Write(Enabled);

            writer.Write(Itemname);
            writer.Write(TotalParts);
            writer.Write(PartsAttached);
            writer.Write(ThirdParty);
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
            Events = reader.ReadBoolean();
            Combat = reader.ReadBoolean();
            Skills = reader.ReadBoolean();
            OutputFile = reader.ReadString();
            Enabled = reader.ReadBoolean();

            Itemname = reader.ReadString();
            TotalParts = reader.ReadInt();
            PartsAttached = reader.ReadInt();
            ThirdParty = reader.ReadBoolean();

            if (ThirdParty) _ThirdParties = new Dictionary<string, DateTime>();
        }
    }
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
