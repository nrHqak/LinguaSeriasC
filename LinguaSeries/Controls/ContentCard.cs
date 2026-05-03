using System;
using System.Drawing;
using System.Windows.Forms;
using LinguaSeries.Models;

namespace LinguaSeries.Controls
{
    public class ContentCard : Panel
    {
        public event EventHandler DetailsRequested;
        public event EventHandler FavoriteToggled;
        public ContentItem Item { get; }

        public ContentCard(ContentItem item, bool isFavorite)
        {
            Item = item;
            Size = new Size(220, 280);
            BackColor = Theme.Card;
            Margin = new Padding(12);

            var title = new Label { Text = item.Title, ForeColor = Theme.Text, Left = 12, Top = 140, Width = 190 };
            var meta = new Label { Text = $"{item.Genre} • ★{item.Rating}", ForeColor = Theme.SecondaryText, Left = 12, Top = 165, Width = 190 };
            var lvl = new Label { Text = item.Level, ForeColor = Theme.Accent, Left = 12, Top = 188, Width = 190 };
            var poster = new Panel { Left = 12, Top = 12, Width = 196, Height = 120, BackColor = Color.FromArgb(50, 50, 50) };
            var details = new RoundedButton { Text = "Details", Left = 12, Top = 220, Width = 95, Height = 35 };
            var fav = new RoundedButton { Text = isFavorite ? "♥" : "♡", Left = 113, Top = 220, Width = 95, Height = 35, BackColor = Color.FromArgb(44, 44, 44) };
            details.Click += (s, e) => DetailsRequested?.Invoke(this, EventArgs.Empty);
            fav.Click += (s, e) => FavoriteToggled?.Invoke(this, EventArgs.Empty);

            MouseEnter += (s, e) => BackColor = Color.FromArgb(40, 40, 40);
            MouseLeave += (s, e) => BackColor = Theme.Card;

            Controls.AddRange(new Control[] { poster, title, meta, lvl, details, fav });
        }
    }
}
