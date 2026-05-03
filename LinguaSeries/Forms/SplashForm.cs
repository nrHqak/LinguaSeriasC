using System;
using System.Drawing;
using System.Windows.Forms;

namespace LinguaSeries.Forms
{
    public class SplashForm : Form
    {
        private readonly Timer _timer = new Timer();
        private readonly ProgressBar _progress = new ProgressBar();
        public SplashForm()
        {
            FormBorderStyle = FormBorderStyle.None;
            StartPosition = FormStartPosition.CenterScreen;
            Size = new Size(560, 320);
            BackColor = Theme.Background;
            Opacity = 0;

            Controls.Add(new Label { Text = "LinguaSeries", ForeColor = Theme.Text, Font = new Font("Segoe UI", 30, FontStyle.Bold), AutoSize = true, Left = 140, Top = 100 });
            _progress.Left = 80; _progress.Top = 220; _progress.Width = 400; _progress.Style = ProgressBarStyle.Continuous;
            Controls.Add(_progress);

            _timer.Interval = 50;
            _timer.Tick += (s, e) => { if (Opacity < 1) Opacity += 0.08; _progress.Value = Math.Min(100, _progress.Value + 4); if (_progress.Value >= 100) Close(); };
            Shown += (s, e) => _timer.Start();
        }
    }
}
