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

            var center = new TableLayoutPanel
            {
                Dock = DockStyle.Fill,
                ColumnCount = 1,
                RowCount = 3,
                BackColor = Theme.Background
            };
            center.RowStyles.Add(new RowStyle(SizeType.Percent, 42));
            center.RowStyles.Add(new RowStyle(SizeType.AutoSize));
            center.RowStyles.Add(new RowStyle(SizeType.Percent, 58));

            var content = new Panel { Width = 760, Height = 240, Anchor = AnchorStyles.None };
            var title = new Label
            {
                Text = "Learn English with Series & Movies",
                ForeColor = Theme.Text,
                Font = new Font("Segoe UI", 34, FontStyle.Bold),
                AutoSize = false,
                TextAlign = ContentAlignment.MiddleCenter,
                Width = 760,
                Height = 96,
                Top = 0
            };
            var subtitle = new Label
            {
                Text = "Smart recommendations for your language level.",
                ForeColor = Theme.SecondaryText,
                Font = new Font("Segoe UI", 15),
                AutoSize = false,
                TextAlign = ContentAlignment.MiddleCenter,
                Width = 760,
                Height = 38,
                Top = 98
            };
            var start = new RoundedButton { Text = "Start Learning", Width = 260, Height = 56, Radius = 24, Font = new Font("Segoe UI", 12, FontStyle.Bold), Left = 250, Top = 160 };
            start.Click += (s, e) => { new LevelSelectionForm().Show(); Hide(); };

            content.Controls.Add(title);
            content.Controls.Add(subtitle);
            content.Controls.Add(start);
            center.Controls.Add(content, 0, 1);
            Controls.Add(center);
        }
    }
}
