using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;

namespace WurmStreamGimmicks {
    internal class FileWatcher : IDisposable {
        private Timer _Timer;
        private long _Length;

        private string _Directory;
        private string _Filename;
        private string _Path;

        public bool EnableRaisingEvents {
            get { return _Timer != null; }
            set {
                if (value) {
                    if (_Timer != null)
                        return;

                    _Timer = new Timer(new TimerCallback(Poll), null, 0, 500);
                }
                else {
                    if (_Timer == null)
                        return;
                    _Timer.Change(Timeout.Infinite, Timeout.Infinite);
                    _Timer.Dispose();
                    _Timer = null;
                }
            }
        }

        public event FileSystemEventHandler Changed;

        public FileWatcher(string fullpath) {
            if (!File.Exists(fullpath))
                //throw new FileNotFoundException(fullpath);
                File.Create(fullpath);

            _Length = new FileInfo(fullpath).Length;
            _Path = fullpath;
            _Directory = Path.GetDirectoryName(fullpath);
            _Filename = Path.GetFileName(fullpath);
        }

        public void Dispose() {
            EnableRaisingEvents = false;
            _Path = null;
            _Filename = null;
            _Directory = null;
        }

        private void Poll(object state) {
            long newLength = new FileInfo(_Path).Length;

            if (newLength != _Length) {
                _Length = newLength;

                if (Changed != null)
                    Changed(this, new FileSystemEventArgs(WatcherChangeTypes.Changed, _Directory, _Filename));
            }
        }
    }
}
