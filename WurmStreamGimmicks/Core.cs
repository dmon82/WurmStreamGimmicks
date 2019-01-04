using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;

namespace WurmStreamGimmicks {
    /// <summary>
    ///     Main class in the application.
    /// </summary>
    public class Core {
        private static Assembly m_CoreAssembly = Assembly.GetExecutingAssembly();
        private static Process m_CoreProcess = Process.GetCurrentProcess();
        private static string m_ExePath = m_CoreProcess.MainModule.FileName;
        private static string m_BaseDirectory = Path.GetDirectoryName(m_ExePath);
        private static Thread m_CoreThread = Thread.CurrentThread;
        private static Version m_Version = m_CoreAssembly.GetName().Version;
        
        /// <summary>
        ///     Gets the executing assembly.
        /// </summary>
        public static Assembly CoreAssembly {
            get { return m_CoreAssembly; }
        }

        /// <summary>
        ///     Gets the current process.
        /// </summary>
        public static Process CoreProcess {
            get { return m_CoreProcess; }
        }

        /// <summary>
        ///     Gets the exe path of the application.
        /// </summary>
        public static string ExePath {
            get { return m_ExePath; }
        }

        /// <summary>
        ///     Gets the directory of the application's exe path.
        /// </summary>
        public static string BaseDirectory {
            get { return m_BaseDirectory; }
        }

        /// <summary>
        ///     Gets the current thread.
        /// </summary>
        public static Thread CoreThread {
            get { return m_CoreThread; }
        }

        /// <summary>
        ///     Gets a formatted string of the application's version.
        /// </summary>
        public static string VersionString {
            get {
                return String.Format("v{0}.{1} build {2} (r{3})",
                    m_Version.Major, m_Version.Minor, m_Version.Build, m_Version.Revision);
            }
        }

        /// <summary>
        ///     Gets the version object of the main module of the current process.
        /// </summary>
        public static Version Version {
            get { return m_Version; }
        }

        public static readonly Logger Logger = new Logger("Core", LogLevel.Always);

        private static bool _Running = true;
        private static State _State = State.Idle;
        private static string _OutputFile = @"d:\rec\stream\wurmgimmick.txt";
        private static TimeSpan GimmickSwitchInterval = TimeSpan.FromSeconds(10);
        public static Random Rng = new Random();

        [STAThreadAttribute()]
        public static void Main(string[] args) {
            string _Item = "Corbita";
            string line = @"[12:16:59] You attach a peg to the corbita.";
            Console.WriteLine(Regex.IsMatch(line, String.Format(@"You attach.+to the {0}", _Item.ToLower())));
            Console.WriteLine(Regex.IsMatch(line, String.Format(@"attaches.+to the {0}", _Item.ToLower())));

            /*Console.ReadLine();
            return;*/
            Console.WriteLine("Wurm stream gimmicks {0}", VersionString);

            while (_Running) {
                switch (_State) {
                    case State.Idle:
                    case State.Running:
                        Console.WriteLine();
                        Console.WriteLine(" (1) Add/remove gimmicks for character");
                        Console.WriteLine(" (2) List enabled gimmicks");
                        Console.WriteLine(" (3) List disabled gimmicks");
                        Console.WriteLine();
                        if (_State == State.Running) Console.WriteLine(" Stop [r]unning");
                        else Console.WriteLine(" [R]un");
                        Console.WriteLine(" [Q]uit");
                        Console.WriteLine();
                        Console.Write("Choice: ");

                        string choice = Console.ReadLine().ToLower();
                        Console.WriteLine();

                        Player player = null;

                        switch (choice) {
                            default:
                                Console.WriteLine(":: Invalid choice.");
                                break;
                            case "q":
                                _Running = false;
                                _State = State.Idle;
                                break;
                            case "1": //enable or disable gimmick for player
                                player = PickCharacter();
                                if (player == null) break;
                                AddGimmicks(player);

                                break;
                            case "2": // list enabled gimmicks
                                foreach (Player playerGimmick in Player.Table.Values) {
                                    Console.WriteLine(" :: {0}", playerGimmick);
                                    foreach (BaseGimmick gimmick in playerGimmick.Gimmicks)
                                        Console.WriteLine("    [ON] {0}", gimmick.Name);
                                }
                                Console.WriteLine();
                                break;
                            case "3":
                                foreach (Player playerGimmick in Player.Table.Values) {
                                    Console.WriteLine(" :: {0}", playerGimmick);
                                    foreach (BaseGimmick gimmick in BaseGimmick.Gimmicks)
                                        if (!playerGimmick.Gimmicks.Contains(gimmick))
                                            Console.WriteLine("    [OFF] {0}", gimmick.Name);
                                }
                                Console.WriteLine();
                                break;
                            case "r":
                                if (_State == State.Running) {
                                    _State = State.Idle;
                                }
                                else {
                                    if (Player.Table.Count <= 0) {
                                        Console.WriteLine(":: No players have been set up.");
                                    }
                                    else {
                                        _State = State.Running;
                                        new Thread(new ThreadStart(WorkerThread)).Start();
                                    }
                                }

                                break;
                        }
                        break;
                }
            }
        }

        private static void WorkerThread() {
            BaseGimmick currentGimmick = null;
            DateTime nextGimmickSwitch = DateTime.Now.Add(GimmickSwitchInterval);
            Player player = null;

            foreach (Player playerInTable in Player.Table.Values) playerInTable.BeginWatch();

            try {
                while (_State == State.Running) {
                    // Switch to a new gimmick.
                    if (player == null || currentGimmick == null || DateTime.Now >= nextGimmickSwitch) {
                        Console.WriteLine(":: Selecting new gimmick to display");
                        
                        int index = 0;
                        int playerIndex = Rng.Next(Player.Table.Count);
                        foreach (string currentPlayer in Player.Table.Keys)
                            if (playerIndex == index++) {
                                player = Player.Table[currentPlayer];
                                break;
                            }

                        Console.WriteLine(":: New player is '{0}'.", player);

                        nextGimmickSwitch = DateTime.Now.Add(GimmickSwitchInterval);
                        BaseGimmick nextGimmick;

                        if (player.Gimmicks.Count > 1) while ((nextGimmick = player.Gimmicks[Rng.Next(player.Gimmicks.Count)]) != currentGimmick) ;
                        else if (player.Gimmicks.Count == 1) nextGimmick = player.Gimmicks[0];
                        else nextGimmick = currentGimmick;

                        Console.WriteLine(":: Now displaying {0} gimmick", nextGimmick.Name);
                        currentGimmick = nextGimmick;
                    }

                    // Update output file displayed on stream as text read from file.
                    if (currentGimmick.Dirty) {
                        Console.WriteLine(":: Current gimmick is dirty, updating output file.");
                        StreamWriter writer = new StreamWriter(new FileStream(_OutputFile, FileMode.Create, FileAccess.Write, FileShare.ReadWrite));
                        writer.Write(currentGimmick.ToString());
                        writer.Flush();
                        writer.Close();
                        writer = null;
                    }

                    Thread.Sleep(1000);
                }

                foreach (Player playerInTable in Player.Table.Values) playerInTable.EndWatch();
            }
            catch (Exception e) {
                Console.WriteLine(e);
            }
        }

        static Player PickCharacter() {
            Console.WriteLine();
            Console.WriteLine("Pick character:");

            string[] characters = Directory.GetDirectories(@"d:\games\wurm\players");
            for (int i = 0; i < characters.Length; i++) characters[i] = characters[i].Substring(characters[i].LastIndexOf('\\')+1);
            for (int i = 0; i < characters.Length; i++)
                Console.WriteLine(" ({0}) {1}", i, characters[i]);

            Console.WriteLine();
            Console.Write(" Choice: ");

            string choice = Console.ReadLine();
            int index;

            if (!int.TryParse(choice, out index)) {
                Console.WriteLine(":: Invalid choice.");
                return null;
            }

            if (index < 0 || index >= characters.Length) {
                Console.WriteLine(":: Invalid choice.");
                return null;
            }

            if (!Player.Table.ContainsKey(characters[index]))
                Player.Table.Add(characters[index], new Player(characters[index]));

            return Player.Table[characters[index]];
        }

        static void AddGimmicks(Player player) {
            while (true) {
                Console.WriteLine();
                Console.WriteLine("Add new gimmicks:");
                for (int i = 0; i < BaseGimmick.Count; i++)
                    Console.WriteLine(" ({0}) {1}", i, BaseGimmick.Gimmicks[i].Name);
                Console.WriteLine();
                Console.WriteLine(" (x) Return");
                Console.WriteLine();
                Console.Write("Choice: ");

                string choice = Console.ReadLine();

                if (choice.Equals("x"))
                    break;

                int index;

                if (!int.TryParse(choice, out index)) {
                    Console.WriteLine(":: Invalid choice");
                    continue;
                }

                BaseGimmick gimmick = (BaseGimmick)System.Activator.CreateInstance(BaseGimmick.Gimmicks[index].GetType());
                player.Gimmicks.Add(gimmick);
            }
        }

        static void EnableDisableGimmicks(Player player) {
            while (true) {
                Console.WriteLine();
                Console.WriteLine("Disable/Enable gimmicks:");

                for (int i = 0; i < BaseGimmick.Count; i++)
                    Console.WriteLine(" ({0}) [{1}] {2}", i, (player.Gimmicks.Contains(BaseGimmick.Gimmicks[i]) ? "ON" : "OFF"), BaseGimmick.Gimmicks[i].Name);

                Console.WriteLine();
                Console.WriteLine(" (x) Return");
                Console.WriteLine();

                Console.Write("Choice: ");

                string choice = Console.ReadLine();

                if (choice.Equals("x"))
                    break;

                int index;

                if (!int.TryParse(choice, out index) || index < 0 || index >= BaseGimmick.Count) {
                    Console.WriteLine(":: Invalid choice.");
                    return;
                }

                BaseGimmick gimmick = BaseGimmick.Gimmicks[index];

                if (player.Gimmicks.Contains(gimmick)) player.Gimmicks.Remove(gimmick);
                else player.Gimmicks.Add(gimmick);
            }
        }
    }
}