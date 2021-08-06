using PSO2Logger.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PSO2Logger.Core {
    public class PSO2Directories : ModelBase {
        public static readonly string DefaultLogPath =
            Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "SEGA", "PHANTASYSTARONLINE2", "log_ngs");

        private string _logPath = DefaultLogPath;
        public string LogPath {
            get => _logPath;
            set {
                if (!Directory.Exists(_logPath))
                    throw new Exception($"The path {value} does not exists.");

                SetProperty(ref _logPath, value);
            }
        }

        public static string GetLatestFileName(string folderPath) {
            if (!Directory.Exists(folderPath)) return null;

            var logFiles = Directory.EnumerateFiles(folderPath)
                                    .Where(x => x.Contains("ChatLog"))
                                    .Where(x => !x.Contains("Symbol"))
                                    .OrderByDescending(x => x);

            return logFiles.First();
        }
    }
}
