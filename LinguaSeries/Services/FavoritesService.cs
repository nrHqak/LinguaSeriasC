using System.Collections.Generic;
using System.IO;
using System.Web.Script.Serialization;

namespace LinguaSeries.Services
{
    public class FavoritesService
    {
        private readonly string _path;
        private readonly HashSet<string> _favorites;

        public FavoritesService(string path)
        {
            _path = path;
            _favorites = File.Exists(path)
                ? new HashSet<string>(new JavaScriptSerializer().Deserialize<List<string>>(File.ReadAllText(path)) ?? new List<string>())
                : new HashSet<string>();
        }

        public bool IsFavorite(string title) => _favorites.Contains(title);
        public void Toggle(string title)
        {
            if (!_favorites.Add(title)) _favorites.Remove(title);
            File.WriteAllText(_path, new JavaScriptSerializer().Serialize(new List<string>(_favorites)));
        }
    }
}
