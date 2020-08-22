using System;
using System.Windows.Forms;

namespace SpotifyDownloader
{
    class LabelScroller
    {
        readonly Timer _timer = null;
        bool enabled = false;
        string scrolledText = string.Empty;
        int currentScroll = 0;

        public int ScrollDelay
        {
            get => _timer.Interval;
            set
            {
                if (_timer.Interval != value)
                    _timer.Interval = value;
            }
        }

        public int CharDisplayLimit { get; set; } = 6;
        public string ScrolledText
        {
            get => scrolledText;
            set
            {
                if (scrolledText != value)
                {
                    scrolledText = value;

                    if (scrolledText.Length > CharDisplayLimit)
                        scrolledText += "   ";

                    LabelScrolled.Text = TruncateText(scrolledText, CharDisplayLimit);
                }
            }
        }

        public Label LabelScrolled { get; } = null;
        public bool Enabled
        {
            get => enabled;
            set
            {
                if (enabled != value)
                {
                    enabled = value;

                    if (enabled)
                        _timer.Start();
                    else
                        _timer.Stop();
                }
            }
        }

        public LabelScroller(Label labelToScroll, int scrollDelay = 300, int charDisplayLimit = 6)
        {
            CharDisplayLimit = charDisplayLimit;
            LabelScrolled = labelToScroll;
            ScrolledText = LabelScrolled.Text;

            _timer = new Timer();
            _timer.Interval = scrollDelay;
            _timer.Tick += ScrollText;
        }

        void ScrollText(object sender, EventArgs e)
        {
            var _labelText = scrolledText.ToCharArray();

            if (_labelText.Length == 0 || _labelText.Length <= CharDisplayLimit)
                return;

            if (currentScroll++ >= _labelText.Length)
                currentScroll = 1;

            string newLabel = scrolledText;

            // rotate the string
            for (int x = 0; x < currentScroll; x++)
            {
                char firstChar = _labelText[0];
                for (int i = 1; i < _labelText.Length; i++)
                    _labelText[i - 1] = _labelText[i];

                _labelText[_labelText.Length - 1] = firstChar;

                newLabel = new string(_labelText);
            }

            newLabel = TruncateText(newLabel, CharDisplayLimit);
            LabelScrolled.Text = newLabel;
        }

        string TruncateText(string stringToTruncate, int lengthToCut)
        {
            if (stringToTruncate.Length <= lengthToCut)
                return stringToTruncate;

            return stringToTruncate.Remove(lengthToCut, stringToTruncate.Length - lengthToCut);
        }
    }
}
