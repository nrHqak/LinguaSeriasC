using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web.Script.Serialization;
using LinguaSeries.Models;

namespace LinguaSeries.Services
{
    public class DataService
    {
        private readonly List<ContentItem> _items;
        public DataService(string filePath)
        {
            var json = File.ReadAllText(filePath);
            _items = new JavaScriptSerializer().Deserialize<List<ContentItem>>(json) ?? new List<ContentItem>();
        }

        public List<ContentItem> GetByLevel(string level) => _items.Where(i => i.Level == level).ToList();
        public List<ContentItem> Search(string level, string q)
        {
            q = (q ?? "").ToLowerInvariant();
            return GetByLevel(level).Where(x => x.Title.ToLowerInvariant().Contains(q) || x.Genre.ToLowerInvariant().Contains(q)).ToList();
        }
    }
}
