using System.IO;
using System.Web.Script.Serialization;
using LinguaSeries.Models;

namespace LinguaSeries.Services
{
    public class ProgressService
    {
        private readonly string _path;
        public ProgressService(string path) { _path = path; }

        public UserProgress Load() => File.Exists(_path)
            ? new JavaScriptSerializer().Deserialize<UserProgress>(File.ReadAllText(_path)) ?? new UserProgress()
            : new UserProgress();

        public void Save(UserProgress progress) => File.WriteAllText(_path, new JavaScriptSerializer().Serialize(progress));
    }
}
