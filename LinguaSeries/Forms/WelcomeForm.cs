using System.Drawing;
using System.Windows.Forms;
using LinguaSeries.Controls;

namespace LinguaSeries.Forms
{
    public class WelcomeForm : Form
    {
        public WelcomeForm()
        {
            Text = "LinguaSeries";
            Size = new Size(980, 640);
            MinimumSize = new Size(900, 600);
            StartPosition = FormStartPosition.CenterScreen;
            BackColor = Theme.Background;

            var container = new Panel { Dock = DockStyle.Fill, Padding = new Padding(120, 120, 120, 120) };
            container.Controls.Add(new Label { Text = "Learn English with Series & Movies", ForeColor = Theme.Text, Font = new Font("Segoe UI", 36, FontStyle.Bold), AutoSize = true, Top = 10, Left = 0 });
            container.Controls.Add(new Label { Text = "Smart recommendations for your language level.", ForeColor = Theme.SecondaryText, Font = new Font("Segoe UI", 15), AutoSize = true, Top = 90, Left = 0 });

            var start = new RoundedButton { Text = "Start Learning", Top = 180, Left = 0, Width = 260, Height = 56, Radius = 24, Font = new Font("Segoe UI", 12, FontStyle.Bold) };
            start.Click += (s, e) => { new LevelSelectionForm().Show(); Hide(); };
            container.Controls.Add(start);

            Controls.Add(container);
        }
    }
}
