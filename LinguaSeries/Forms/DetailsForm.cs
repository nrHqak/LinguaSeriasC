using System;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using LinguaSeries.Controls;
using LinguaSeries.Models;
using LinguaSeries.Services;

namespace LinguaSeries.Forms
{
    public class DetailsForm : Form
    {
        public DetailsForm(ContentItem item, string level, ProgressService progressService)
        {
            Text = item.Title; Size = new Size(760, 600); BackColor = Theme.Background; StartPosition = FormStartPosition.CenterParent;
            Controls.Add(new Label { Text = item.Title, Left = 40, Top = 30, ForeColor = Theme.Text, Font = new Font("Segoe UI", 22, FontStyle.Bold), AutoSize = true });
            Controls.Add(new Label { Text = $"{item.Genre} • ★{item.Rating} • {item.Level}", Left = 40, Top = 80, ForeColor = Theme.SecondaryText, AutoSize = true });
            Controls.Add(new Label { Text = item.Description, Left = 40, Top = 120, Width = 660, Height = 120, ForeColor = Theme.Text });
            Controls.Add(new Label { Text = $"Why this level: suitable for {level} due to clear dialogues and level-matched vocabulary.", Left = 40, Top = 250, Width = 660, ForeColor = Theme.SecondaryText });
            Controls.Add(new Label { Text = "Vocabulary: " + string.Join(", ", item.VocabularyWords ?? Enumerable.Empty<string>()), Left = 40, Top = 300, Width = 660, Height = 100, ForeColor = Theme.Text });
            var watch = new RoundedButton { Text = "WATCH NOW", Left = 40, Top = 450, Width = 180, Height = 48 };
            watch.Click += (s, e) =>
            {
                var progress = progressService.Load();
                if (!progress.WatchedTitles.Contains(item.Title)) progress.WatchedTitles.Add(item.Title);
                progress.LearnedWords += (item.VocabularyWords?.Count ?? 0);
                progressService.Save(progress);
                Process.Start(item.WatchLink);
            };
            Controls.Add(watch);
        }
    }
}
