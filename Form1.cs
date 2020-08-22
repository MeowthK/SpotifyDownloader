using System;
using CSAudioRecorder;
using System.Windows.Forms;
using System.IO;
using System.Collections;
using System.Drawing;
using System.Collections.Generic;
using System.Linq;

namespace SpotifyDownloader
{
    public partial class Form1 : Form
    {
        List<AudioRecorder> recorders = null;

        string spotifyTitle = "Spotify Free";
        string downloadPath = "";
        Song currentSong;

        Queue songQueue = null;
        bool folded = true;
        bool errLogCollapsed = true;
        Size initialSize;

        public Form1()
        {
            InitializeComponent();

            Load += (o, e) =>
            {
                initialSize = Size;
                recorders = new List<AudioRecorder>();

                var scroller = new LabelScroller(trackName, 500, 40);
                scroller.Enabled = true;

                songQueue = new Queue();

                SpotifyTitleNotifier.TitleChanged += (ob, ev) =>
                {
                    spotifyTitle = SpotifyTitleNotifier.CurrentTitle;
                    currentSong = CreateSongFromTitle(spotifyTitle);
                    currentSong.Location = @"" + downloadPath + "\\" + GetValidFileName(currentSong.Artist) + "\\" + GetValidFileName(currentSong.SongTitle) + ".mp3";

                    if (!IsSongAnAd(currentSong))
                        songQueue.Enqueue(currentSong);

                    scroller.ScrolledText = spotifyTitle == SpotifyTitleNotifier.SPOTIFY_FREE_STR ? "No Track" : spotifyTitle;
                    Text = spotifyTitle;

                    if (!cbResumeRecord.Checked)
                        return;

                    if (SpotifyTitleNotifier.PlayerPaused || IsSongAnAd(currentSong))
                    {
                        if (recorders.Count > 0)
                        {
                            var lastInLine = recorders[recorders.Count - 1];

                            if (lastInLine != null && lastInLine.IsRecording)
                            {
                                lastInLine.Stop();
                            }

                            if (IsSongAnAd(currentSong) && File.Exists(lastInLine.FileName + ".INCOMPLETE"))
                                File.Delete(lastInLine.FileName + ".INCOMPLETE");
                        }

                        return;
                    }

                    StartRecording();
                };

                SpotifyTitleNotifier.StartMonitoring();

                pnlShowErrLog.MouseDown += (obj, ev) =>
                {
                    if (errLogCollapsed)
                    {
                        pnlErrLog.Visible = false;
                        Height = 140;
                    }
                    else
                    {
                        pnlErrLog.Visible = true;
                        Height = initialSize.Height;
                    }

                    errLogCollapsed = !errLogCollapsed;
                };

                cbResumeRecord.CheckedChanged += (obj, ev) =>
                {
                    if (recorders.Count > 0)
                    {
                        if (!cbResumeRecord.Checked && recorders[0].IsRecording)
                        {
                            recorders[0].Stop();
                            recorders.Remove(recorders[0]);
                        }
                    }
                };

                Closing += (obj, ev) =>
                {
                    SpotifyTitleNotifier.PauseMonitoring();

                    foreach (var rec in recorders)
                        rec.Stop();

                    recorders.Clear();
                };
            };

            SelectDownloadLocation();
        }

        void SelectDownloadLocation()
        {
            using (var fbd = new FolderBrowserDialog())
            {
                fbd.Description = "Select where to put your streamed MP3 files\nIgnore to download to relative path.";
                fbd.RootFolder = Environment.SpecialFolder.MyComputer;
                fbd.ShowDialog();

                Activate();

                if (fbd.SelectedPath != string.Empty)
                    downloadPath = fbd.SelectedPath;
            }
        }

        bool IsSongAnAd(Song song)
        {
            return song.Artist == "Advertisement" || song.Artist == "Spotify";
        }

        void StartRecording()
        {
            // Stop least recent streamed audio
            if (recorders.Count > 0)
            {
                var lastRecorder = recorders[recorders.Count - 1];

                if (File.Exists(lastRecorder.FileName + ".INCOMPLETE"))
                    File.Delete(lastRecorder.FileName + ".INCOMPLETE");

                if (lastRecorder.IsRecording)
                    lastRecorder.Stop();
            }

            // ADS!!! Just ignore :3
            if (IsSongAnAd(currentSong))
                return;

            var parentPath = @"" + downloadPath + "//" + GetValidFileName(currentSong.Artist);

            if (!Directory.Exists(parentPath))
                Directory.CreateDirectory(parentPath);

            var spotifyTitleWEx = currentSong.Location;

            if (File.Exists(spotifyTitleWEx) && !File.Exists(spotifyTitleWEx + ".INCOMPLETE"))
                return;

            File.WriteAllText(spotifyTitleWEx + ".INCOMPLETE", "Delete if MP3 file is already done streaming.");

            #region Recorder Initializers
            var recorder = new AudioRecorder
            {
                FileName = spotifyTitleWEx,
                Format = Format.MP3,
                Bitrate = Bitrate.bits320,
                Samplerate = Samplerate.esamples44100,
                Bits = Bits.bits16,
                Channels = Channels.channels2,
                Mode = Mode.WasapiLoopbackCapture,
                ACMTag = "PCM",
                DeviceIndex = 0,
                Latency = 1000,
                StopOnSilence = true,
                StopOnSilenceSeconds = 2,
                StopOnSilenceVal = 0,
                TagTitle = currentSong.SongTitle,
                TagArtists = new List<string>(){ currentSong.Artist },
                TagComment = "Ripped from Spotify by Meowth"
            };

            var defDeviceIndex = recorder.GetDeviceDefaultIndex(Mode.WasapiLoopbackCapture);

            if (defDeviceIndex != -1)
                recorder.DeviceIndex = defDeviceIndex;

            recorder.RecordError += LogError;
            recorder.RecordDone += AddMP3Tags;
            recorder.RecordProgress += FixIndicatorText;

            recorders.Add(recorder);

            recorder.Start();
            lblIndicator.Text = "Streaming";
            #endregion
        }

        void FixIndicatorText(object sender, EventArgs e)
        {
            if (lblIndicator.Text != "Streaming")
                lblIndicator.Text = "Streaming";
        }

        void LogError(object sender, MessageArgs e)
        {
            tbErrorLog.Text += currentSong.SongTitle + "| Error: " + e.Number + ";" + e.String + "\r\n";
        }

        void AddMP3Tags(object sender, EventArgs e)
        {
            lblIndicator.Text = "Stopped";

            var recorder = sender as AudioRecorder;
            recorders.Remove(recorder);

            recorder.RecordError -= LogError;
            recorder.RecordDone -= AddMP3Tags;
            recorder.RecordProgress -= FixIndicatorText;

            if (recorder.IsRecording)
                recorder.Stop();

            var tempSong = currentSong;

            if (songQueue.Count > 0)
                tempSong = (Song)songQueue.Dequeue();

            recorder.SetID3Tags(recorder.FileName);
        }

        Song CreateSongFromTitle(string title)
        {
            var song = new Song
            {
                Artist = "Unknown Artist",
                SongTitle = "Unknown Song",
                Location = "Undefined"
            };

            var splitted = title.Split(new char[]{ '-' }, 2);

            if (splitted.Length >= 1)
                song.Artist = splitted[0].Trim();

            if (splitted.Length >= 2)
                song.SongTitle = splitted[1].Trim();

            return song;
        }

        string GetValidFileName(string fName)
        {
            var invalidChars = Path.GetInvalidFileNameChars();
            List<char> validFName = new List<char>();

            for (int i = 0; i < fName.Length; i++)
            {
                if (invalidChars.Contains(fName[i]))
                    continue;

                validFName.Add(fName[i]);
            }

            return new string(validFName.ToArray());
        }

        protected override void WndProc(ref Message m)
        {
            // override titlebar double-click
            if (m.Msg == 0x00A3)
            {
                if (folded)
                {
                    Height = 0;
                    Text = spotifyTitle;
                }
                else
                {
                    Height = !errLogCollapsed ? 140 : initialSize.Height;
                    Text = "Spotify Downloader";
                }

                folded = !folded;

                m.Result = IntPtr.Zero;
                return;
            }

            base.WndProc(ref m);
        }
    }

    struct Song
    {
        public string Artist;
        public string SongTitle;
        public string Location;

        public bool IsEmpty
        {
            get => (Artist == null || Artist.Length == 0) && (SongTitle == null || SongTitle.Length == 0);
        }
    }
}
