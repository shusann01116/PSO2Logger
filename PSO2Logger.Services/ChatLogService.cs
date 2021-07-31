using PSO2Logger.Interfaces;
using PSO2Logger.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace PSO2Logger.Services {
    public class ChatLogService : ILogService<ChatLine> {
        private StreamReader streamReader;
        private FileStream fileStream;
        private static List<ChatLine> chatLines = new List<ChatLine>();

        public string FileName { get; set; }
        public string FolderPath { get; set; }

        public ChatLogService() {
            InitializeStream();
        }

        private void InitializeStream() {
            if (!(streamReader is null) || !(fileStream is null)) {
                streamReader.Close();
            }

            var filePath = GetFilePath(FileName);

            fileStream = new FileStream(filePath, FileMode.OpenOrCreate, FileAccess.ReadWrite);
            streamReader = new StreamReader(fileStream);
        }

        private string GetFilePath(string fileName) => Path.Combine(FolderPath, fileName);

        private IEnumerable<ChatLine> ParseLines(string lines) {
            foreach (var line in lines.Split($"\r\n")) {
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
    }
}
