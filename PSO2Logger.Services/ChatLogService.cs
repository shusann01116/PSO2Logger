using PSO2Logger.Interfaces;
using PSO2Logger.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace PSO2Logger.Services {
    public class ChatLogService : ILogService<ChatLine>, IDisposable {
        private static readonly string defaultLogPath =
            Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "SEGA", "PHANTASYSTARONLINE2", "log_ngs");

        private StreamReader streamReader;
        private FileStream fileStream;
        private static List<ChatLine> chatLines = new List<ChatLine>();

        public string FileName { get; set; }
        public string FolderPath { get; set; }

        public ChatLogService() {
            FolderPath = defaultLogPath;
            FileName = GetLatestFileName(FolderPath);
        }

        public void InitializeStream() {
            FileName ??= GetLatestFileName(FolderPath);
            _ = FileName ?? throw new Exception($"FileName property cannot be null");

            if (!Directory.Exists(FolderPath))
                throw new DirectoryNotFoundException(FolderPath);

            if (!File.Exists(Path.Combine(FolderPath, FileName)))
                throw new FileNotFoundException(FileName);

            if (!(streamReader is null && fileStream is null)) {
                streamReader.Close();
                fileStream.Close();
            }

            var filePath = Path.Combine(FolderPath, FileName);

            fileStream = new FileStream(filePath, FileMode.OpenOrCreate, FileAccess.ReadWrite);
            streamReader = new StreamReader(fileStream);
        }

        private IEnumerable<ChatLine> ParseLines(string lines) {
            foreach (var line in lines.Split($"\r\n")) {
                if (string.IsNullOrEmpty(line))
                    continue;

                yield return ParseLine(line);
            }
        }

        private ChatLine ParseLine(string line) {
            var substr = line.Split('\t');

            var timeStamp = DateTime.Parse(substr[(int)ChatParameters.TimeStamp]);
            var chatNum = int.Parse(substr[(int)ChatParameters.ChatNum]);
            var chatType = (ChatType)Enum.Parse(typeof(ChatType), substr[(int)ChatParameters.ChatType]);
            var playerId = int.Parse(substr[(int)ChatParameters.PlayerID]);
            var playerIdName = substr[(int)ChatParameters.PlayerIDName];
            var chatBody = substr[(int)ChatParameters.ChatBody].Trim('"');

            return new ChatLine() {
                TimeStamp = timeStamp,
                ChatNum = chatNum,
                ChatType = chatType,
                PlayerID = playerId,
                PlayerIDName = playerIdName,
                ChatBody = chatBody,
            };
        }

        public IEnumerable<ChatLine> GetNewLines() {
            var lines = streamReader.ReadToEnd();
            return ParseLines(lines);
        }

        public ChatLine GetLine(int id) {
            return chatLines.FirstOrDefault(x => x.ChatNum == id);
        }

        private static string GetLatestFileName(string folderPath) {
            if (!Directory.Exists(folderPath)) return null;

            // ChatLog20210701_00.txt
            var logFiles = Directory.EnumerateFiles(folderPath)
                                    .Where(x => x.Contains("ChatLog"))
                                    .Where(x => !x.Contains("Symbol"))
                                    .OrderByDescending(x => x);

            return logFiles.First();
        }

        public void Dispose() {
            streamReader.Close();
            fileStream.Close();
        }
    }
}
