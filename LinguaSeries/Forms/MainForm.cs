using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using LinguaSeries.Controls;
using LinguaSeries.Models;
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
        private readonly Panel _contentPanel = new Panel();
        private readonly Label _sectionTitle = new Label();
        private string _currentSection = "Home";

        public MainForm(string level)
        {
            _level = level;
            Text = $"LinguaSeries - {_level}";
            WindowState = FormWindowState.Maximized;
            MinimumSize = new Size(1100, 700);
            BackColor = Theme.Background;

            var baseDir = AppDomain.CurrentDomain.BaseDirectory;
            _data = new DataService(Path.Combine(baseDir, "Data", "content.json"));
            _fav = new FavoritesService(Path.Combine(baseDir, "favorites.json"));
            _progressSvc = new ProgressService(Path.Combine(baseDir, "progress.json"));

            var sidebar = BuildSidebar();
            var top = BuildTopBar();
            BuildContentArea();

            Controls.Add(_contentPanel);
            Controls.Add(top);
            Controls.Add(sidebar);

            RenderSection();
        }

        private Panel BuildSidebar()
        {
            var sidebar = new Panel { Dock = DockStyle.Left, Width = 220, BackColor = Theme.Card, Padding = new Padding(16, 18, 16, 18) };
            sidebar.Controls.Add(new Label { Text = "LinguaSeries", ForeColor = Theme.Text, Font = new Font("Segoe UI", 16, FontStyle.Bold), Dock = DockStyle.Top, Height = 50 });

            var navPanel = new FlowLayoutPanel { Dock = DockStyle.Fill, FlowDirection = FlowDirection.TopDown, WrapContents = false, Padding = new Padding(0, 8, 0, 0), AutoScroll = true };
            foreach (var section in new[] { "Home", "Recommended", "Favorites", "Progress" })
            {
                var btn = new RoundedButton
                {
                    Text = section,
                    Width = 180,
                    Height = 42,
                    Margin = new Padding(0, 0, 0, 10),
                    Radius = 16,
                    BackColor = Color.FromArgb(44, 44, 44),
                    ForeColor = Theme.Text,
                    Font = new Font("Segoe UI", 10, FontStyle.Regular)
                };
                btn.Click += (s, e) => { _currentSection = section; RenderSection(); };
                navPanel.Controls.Add(btn);
            }
            sidebar.Controls.Add(navPanel);
            return sidebar;
        }

        private Panel BuildTopBar()
        {
            var top = new Panel { Dock = DockStyle.Top, Height = 82, BackColor = Theme.Background, Padding = new Padding(24, 20, 24, 12) };
            _search.Width = 320;
            _search.Height = 32;
            _search.Font = new Font("Segoe UI", 10);
            _search.BorderStyle = BorderStyle.FixedSingle;
            _search.TextChanged += (s, e) => { if (_currentSection == "Home" || _currentSection == "Recommended" || _currentSection == "Favorites") RenderCards(); };

            _sectionTitle.Text = "Home";
            _sectionTitle.ForeColor = Theme.Text;
            _sectionTitle.Font = new Font("Segoe UI", 14, FontStyle.Bold);
            _sectionTitle.AutoSize = true;
            _sectionTitle.Left = 380;
            _sectionTitle.Top = 24;

            _search.Left = 24;
            _search.Top = 22;

            var levelBadge = new Label { Text = $"Level: {_level}", ForeColor = Theme.Accent, Font = new Font("Segoe UI", 11, FontStyle.Bold), AutoSize = true, Left = 520, Top = 26 };
            var avatar = new Label { Text = "👤", AutoSize = true, ForeColor = Theme.Text, Left = 640, Top = 23, Font = new Font("Segoe UI Emoji", 13) };

            top.Controls.Add(_search);
            top.Controls.Add(_sectionTitle);
            top.Controls.Add(levelBadge);
            top.Controls.Add(avatar);
            return top;
        }

        private void BuildContentArea()
        {
            _contentPanel.Dock = DockStyle.Fill;
            _contentPanel.Padding = new Padding(24, 8, 24, 24);
            _contentPanel.BackColor = Theme.Background;

            _cards.Dock = DockStyle.Fill;
            _cards.AutoScroll = true;
            _cards.WrapContents = true;
            _cards.Padding = new Padding(0, 8, 0, 0);
            _cards.BackColor = Theme.Background;
        }

        private void RenderSection()
        {
            _sectionTitle.Text = _currentSection;
            _contentPanel.Controls.Clear();

            switch (_currentSection)
            {
                case "Home":
                case "Recommended":
                case "Favorites":
                    _contentPanel.Controls.Add(_cards);
                    RenderCards();
                    break;
                case "Progress":
                    RenderProgress();
                    break;

            }
        }

        private void RenderCards()
        {
            _cards.Controls.Clear();
            IEnumerable<ContentItem> items = _data.GetByLevel(_level);
            if (_currentSection == "Favorites") items = items.Where(x => _fav.IsFavorite(x.Title));

            var q = (_search.Text ?? string.Empty).Trim().ToLowerInvariant();
            if (!string.IsNullOrWhiteSpace(q)) items = items.Where(x => x.Title.ToLowerInvariant().Contains(q) || x.Genre.ToLowerInvariant().Contains(q));

            foreach (var item in items)
            {
                var card = new ContentCard(item, _fav.IsFavorite(item.Title));
                card.DetailsRequested += (s, e) => new DetailsForm(item, _level, _progressSvc).ShowDialog();
                card.FavoriteToggled += (s, e) => { _fav.Toggle(item.Title); RenderCards(); };
                _cards.Controls.Add(card);
            }
        }

        private void RenderProgress()
        {
            var progress = _progressSvc.Load();
            var panel = new Panel { Dock = DockStyle.Top, Height = 240, BackColor = Theme.Card, Padding = new Padding(24) };
            panel.Controls.Add(new Label { Text = $"Watched: {progress.WatchedTitles.Count}", Top = 20, Left = 24, ForeColor = Theme.Text, Font = new Font("Segoe UI", 13, FontStyle.Bold), AutoSize = true });
            panel.Controls.Add(new Label { Text = $"Words learned: {progress.LearnedWords}", Top = 60, Left = 24, ForeColor = Theme.SecondaryText, Font = new Font("Segoe UI", 11), AutoSize = true });
            var bar = new ProgressBar { Top = 105, Left = 24, Width = 500, Height = 24, Maximum = 1000, Value = Math.Min(1000, progress.LearnedWords) };
            panel.Controls.Add(bar);
            panel.Controls.Add(new Label { Text = string.Join(", ", progress.WatchedTitles.Take(8)), Top = 145, Left = 24, Width = 800, Height = 60, ForeColor = Theme.Text });
            _contentPanel.Controls.Add(panel);
        }

    }
}
