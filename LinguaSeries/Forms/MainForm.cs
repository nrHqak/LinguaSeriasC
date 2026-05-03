using System;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using LinguaSeries.Controls;
using LinguaSeries.Services;

namespace LinguaSeries.Forms
{
    public class MainForm : Form
    {
        private readonly string _level;
        private readonly DataService _data;
        private readonly FavoritesService _fav;
        private readonly ProgressService _progressSvc;
        private readonly FlowLayoutPanel _cards = new FlowLayoutPanel();
        private readonly TextBox _search = new TextBox();
        public MainForm(string level)
        {
            _level = level;
            Text = $"LinguaSeries - {_level}"; WindowState = FormWindowState.Maximized; BackColor = Theme.Background;
            var baseDir = AppDomain.CurrentDomain.BaseDirectory;
            _data = new DataService(Path.Combine(baseDir, "Data", "content.json"));
            _fav = new FavoritesService(Path.Combine(baseDir, "favorites.json"));
            _progressSvc = new ProgressService(Path.Combine(baseDir, "progress.json"));

            var sidebar = new Panel { Dock = DockStyle.Left, Width = 180, BackColor = Theme.Card };
            foreach (var t in new[] { "Home", "Recommended", "Favorites", "Progress", "Settings", "About" })
                sidebar.Controls.Add(new Label { Text = t, ForeColor = Theme.Text, Left = 20, Top = 30 + sidebar.Controls.Count * 40, Width = 140 });
            Controls.Add(sidebar);

            var top = new Panel { Dock = DockStyle.Top, Height = 70, BackColor = Theme.Background };
            _search.SetBounds(220, 18, 360, 32); _search.TextChanged += (s, e) => Render();
            top.Controls.Add(_search);
            top.Controls.Add(new Label { Text = $"Level: {_level}", ForeColor = Theme.Accent, Left = 620, Top = 24, Width = 200 });
            Controls.Add(top);

            _cards.Dock = DockStyle.Fill; _cards.AutoScroll = true; _cards.Padding = new Padding(20);
            Controls.Add(_cards);
            Render();
        }

        private void Render()
        {
            _cards.Controls.Clear();
            var items = string.IsNullOrWhiteSpace(_search.Text) ? _data.GetByLevel(_level) : _data.Search(_level, _search.Text);
            foreach (var item in items)
            {
                var card = new ContentCard(item, _fav.IsFavorite(item.Title));
                card.DetailsRequested += (s, e) => new DetailsForm(item, _level, _progressSvc).ShowDialog();
                card.FavoriteToggled += (s, e) => { _fav.Toggle(item.Title); Render(); };
                _cards.Controls.Add(card);
            }
        }
    }
}
