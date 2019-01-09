using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Windows.Forms;

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

        private static Config _Config = null;
        internal static Config Config { get { return _Config; } }

        internal static readonly Logger Logger = new Logger("Core", LogLevel.Always);

        public static Random Rng = new Random();

        [STAThreadAttribute()]
        public static void Main(string[] args) {
            string configFilename = Path.Combine(Core.BaseDirectory, "config.bin");

            if (!File.Exists(configFilename)) _Config = WurmStreamGimmicks.Config.Initialise(configFilename);
            else using (MyReader reader = new MyReader(configFilename)) { _Config = new WurmStreamGimmicks.Config(); _Config.Deserialise(reader); reader.Close(); }

            Application.Run(new frmMain());

            using (MyWriter writer = new MyWriter(configFilename)) {
                _Config.Serialise(writer);
                writer.Flush();
                writer.Close();
            }

            foreach (Player player in Player.Table.Values)
                player.Dispose();

            return;
            string _Item = "Corbita";
            string line = @"[12:16:59] You attach a peg to the corbita.";
            Console.WriteLine(Regex.IsMatch(line, String.Format(@"You attach.+to the {0}", _Item.ToLower())));
            Console.WriteLine(Regex.IsMatch(line, String.Format(@"attaches.+to the {0}", _Item.ToLower())));
        }
    }
}