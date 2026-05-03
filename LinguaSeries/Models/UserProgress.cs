using System.Collections.Generic;

namespace LinguaSeries.Models
{
    public class UserProgress
    {
        public List<string> WatchedTitles { get; set; } = new List<string>();
        public int LearnedWords { get; set; }
    }
}
