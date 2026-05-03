using System.Drawing;
using System.Windows.Forms;
using LinguaSeries.Controls;

namespace LinguaSeries.Forms
{
    public class WelcomeForm : Form
    {
        public WelcomeForm()
        {
            Text = "LinguaSeries"; Size = new Size(900, 560); StartPosition = FormStartPosition.CenterScreen; BackColor = Theme.Background;
            Controls.Add(new Label { Text = "Learn English with Series & Movies", ForeColor = Theme.Text, Font = new Font("Segoe UI", 24, FontStyle.Bold), AutoSize = true, Top = 120, Left = 120 });
            Controls.Add(new Label { Text = "Smart recommendations for your language level.", ForeColor = Theme.SecondaryText, Font = new Font("Segoe UI", 12), AutoSize = true, Top = 180, Left = 120 });
            var start = new RoundedButton { Text = "Start Learning", Top = 260, Left = 120, Width = 220, Height = 50 };
            start.Click += (s, e) => { new LevelSelectionForm().Show(); Hide(); };
            Controls.Add(start);
        }
    }
}
