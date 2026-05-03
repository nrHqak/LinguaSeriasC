using System.Drawing;
using System.Windows.Forms;
using LinguaSeries.Controls;

namespace LinguaSeries.Forms
{
    public class LevelSelectionForm : Form
    {
        public LevelSelectionForm()
        {
            Text = "Choose Level";
            Size = new Size(980, 640);
            MinimumSize = new Size(900, 600);
            StartPosition = FormStartPosition.CenterScreen;
            BackColor = Theme.Background;

            var title = new Label
            {
                Text = "Choose your English level",
                ForeColor = Theme.Text,
                Font = new Font("Segoe UI", 24, FontStyle.Bold),
                AutoSize = true,
                Dock = DockStyle.Top,
                Padding = new Padding(0, 40, 0, 20),
                TextAlign = ContentAlignment.MiddleCenter
            };

            var wrap = new Panel { Dock = DockStyle.Fill, Padding = new Padding(100, 20, 100, 80) };
            var flow = new FlowLayoutPanel
            {
                Dock = DockStyle.Fill,
                WrapContents = true,
                FlowDirection = FlowDirection.LeftToRight,
                AutoScroll = true,
                Padding = new Padding(10)
            };

            foreach (var l in new[] { "A1", "A2", "B1", "B2", "C1" })
            {
                var b = new RoundedButton
                {
                    Text = l,
                    Width = 150,
                    Height = 150,
                    Font = new Font("Segoe UI", 26, FontStyle.Bold),
                    Margin = new Padding(16),
                    Radius = 28
                };
                b.Click += (s, e) => { new MainForm(l).Show(); Hide(); };
                flow.Controls.Add(b);
            }

            wrap.Controls.Add(flow);
            Controls.Add(wrap);
            Controls.Add(title);
        }
    }
}
