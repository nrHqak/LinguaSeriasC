using System.Collections.Generic;

namespace LinguaSeries.Models
{
    public class ContentItem
    {
        public string Title { get; set; }
        public string Genre { get; set; }
        public string Level { get; set; }
        public string Description { get; set; }
        public double Rating { get; set; }
        public string Image { get; set; }
        public string WatchLink { get; set; }
        public List<string> VocabularyWords { get; set; }

        public override string ToString() => Title;
    }
}
