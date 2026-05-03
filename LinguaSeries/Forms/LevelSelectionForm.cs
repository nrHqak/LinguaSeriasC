using System.Drawing;
using System.Windows.Forms;
using LinguaSeries.Controls;

namespace LinguaSeries.Forms
{
    public class LevelSelectionForm : Form
    {
        public LevelSelectionForm()
        {
            Text = "Choose Level"; Size = new Size(900, 560); StartPosition = FormStartPosition.CenterScreen; BackColor = Theme.Background;
            Controls.Add(new Label { Text = "Choose your English level", ForeColor = Theme.Text, Font = new Font("Segoe UI", 22, FontStyle.Bold), AutoSize = true, Top = 50, Left = 240 });
            var flow = new FlowLayoutPanel { Left = 120, Top = 140, Width = 660, Height = 280 };
            foreach (var l in new[] { "A1", "A2", "B1", "B2", "C1" })
            {
                var b = new RoundedButton { Text = l, Width = 120, Height = 120, Font = new Font("Segoe UI", 20, FontStyle.Bold), Margin = new Padding(12) };
                b.Click += (s, e) => { new MainForm(l).Show(); Hide(); };
                flow.Controls.Add(b);
            }
            Controls.Add(flow);
        }
    }
}
