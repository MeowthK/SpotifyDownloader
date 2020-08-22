using System;
using System.Diagnostics;
using System.Windows.Forms;

namespace SpotifyDownloader
{
    public static class SpotifyTitleNotifier
    {
        public static readonly string SPOTIFY_FREE_STR = "Spotify Free";

        public static event EventHandler TitleChanged;
        private static string _currentTitle = SPOTIFY_FREE_STR;

        private static Timer timer = null;

        private static string currentTitle
        {
            get => _currentTitle;
            set
            {
                if (_currentTitle != value)
                {
                    _currentTitle = value;

                    if (TitleChanged != null)
                        TitleChanged.Invoke(null, EventArgs.Empty);
                }
            }
        }

        public static bool PlayerPaused { get => _currentTitle == SPOTIFY_FREE_STR; }

        public static string CurrentTitle { get => currentTitle; }

        public static void StartMonitoring()
        {
            if (timer != null && timer.Enabled) // timer is already running, no need to reexecute lines of codes below this scope.
                return;

            timer = new Timer { Interval = 100 };
            timer.Tick += (o, e) =>
            {
                var procs = Process.GetProcessesByName("Spotify");

                for (int i = 0; i < procs.Length; i++)
                {
                    var procTitle = procs[i].MainWindowTitle;

                    if (procTitle.Length > 0 && _currentTitle != procTitle)
                    {
                        currentTitle = procTitle;
                        break;
                    }
                }
            };

            timer.Start();
        }

        public static void PauseMonitoring()
        {
            if ((timer != null) && !timer.Enabled)
                return;

            timer.Stop();
        }
    }
}
