using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;

namespace WurmStreamGimmicks {
    internal class Logger : IDisposable {
        public delegate void LogEventHandler(string message);

        private string _Name;
        public string Name {
            get { return _Name; }
            set { _Name = value; }
        }

        private LogLevel _Level;
        public LogLevel Level {
            get { return _Level; }
            set { _Level = value; }
        }

        private StreamWriter _Writer;

        public event LogEventHandler Logged;

        private object _Lock = new object();

        public Logger(string name, LogLevel level) {
            if (string.IsNullOrEmpty(name))
                throw new ArgumentException("Logger must have a name.");

            _Name = name;
            _Level = level;

            string path = Core.BaseDirectory;
            string filename = "logfile.log";

            // Log rotate.
            for (int i = 5; i >= 0; i--) {
                string rotateTo = Path.Combine(path, String.Format("logfile.{0}.log", i + 1));
                string rotateFrom = Path.Combine(path, (i > 0 ? String.Format("logfile.{0}.log", i) : "logfile.log"));

                Console.WriteLine("Rotating from {0} to {1}.", Path.GetFileName(rotateFrom), Path.GetFileName(rotateTo));

                if (i == 5 && File.Exists(rotateTo))
                    File.Delete(rotateTo);

                if (File.Exists(rotateFrom))
                    File.Move(rotateFrom, rotateTo);
            }

            _Writer = new StreamWriter(FileStream.Synchronized(new FileStream(
                Path.Combine(path, filename), FileMode.Append, FileAccess.Write, FileShare.Read)));
        }

        private string[] _Spaces = new string[] { "", " ", "  ", "   ", "    ", "     " };

        public Logger(string name)
            : this(name, LogLevel.Silent) {
        }

        public void Dispose() {
            _Writer.Flush();
            _Writer.Close();
            _Writer.Dispose();
            _Writer = null;
            _Name = null;
        }

        public void Log(LogLevel level, string message) {
            lock (_Lock) {
                if (level < _Level)
                    return;

                string output = String.Format(":: {0}{1} :: [{2}] :: {3}",
                    level.ToString().ToUpper(), _Spaces[7 - level.ToString().Length],
                    DateTime.Now.ToLongTimeString(),
                    message);

                _Writer.WriteLine(output);
                _Writer.Flush();

                if (level >= _Level) {
                    Console.WriteLine(output);
                    if (Logged != null) Logged(output);
                }
            }
        }

        public void Log(LogLevel level, string format, object arg0) {
            Log(level, String.Format(format, arg0));
        }

        public void Log(LogLevel level, string format, object arg0, object arg1) {
            Log(level, String.Format(format, arg0, arg1));
        }

        public void Log(LogLevel level, string format, object arg0, object arg1, object arg2) {
            Log(level, String.Format(format, arg0, arg1, arg2));
        }

        public void Log(LogLevel level, string format, params object[] args) {
            Log(level, String.Format(format, args));
        }
    }
}
