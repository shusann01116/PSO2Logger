using PSO2Logger.Interfaces;
using PSO2Logger.Interfaces.Core;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace PSO2Logger.Services {
    public class FileWatcherService : IWatcherService, IDisposable {
        /// <summary>
        /// ファイルが変更されたときに通知します。
        /// </summary>
        /// <seealso cref="WatcherEventArgs"/>
        public event EventHandler<WatcherEventArgs> OnChanged;

        private bool dispose = false;

        /// <summary>
        /// 監視するファイル名
        /// </summary>
        public string FileName { get; set; }

        /// <summary>
        /// 監視するフォルダーパス
        /// </summary>
        public string FolderPath { get; set; }

        /// <summary>
        /// 1ループの長さ
        /// </summary>
        public int SleepMilliSec { get; set; }

        /// <summary>
        /// <see cref="true">のときイベントを発火します。</see>
        /// </summary>
        public bool CanRaiseEvent { get; set; } = false;

        public FileWatcherService() {
        }

        /// <summary>
        /// フォルダー内のファイルの変更を通知します。
        /// 標準ループ時間<see cref="SleepMilliSec"/>は1000msです。
        /// </summary>
        /// <param name="folderPath">監視フォルダーパス</param>
        /// <param name="filePath">監視ファイル名</param>
        public FileWatcherService(string folderPath, string filePath)
            : this(folderPath, filePath, 1000) {
        }

        /// <summary>
        /// フォルダー内のファイルの変更を通知します。
        /// </summary>
        /// <param name="folderPath">監視フォルダー名<</param>
        /// <param name="filePath">監視ファイル名</param>
        /// <param name="sleepMilliSec">検知ループ時間(ms)</param>
        public FileWatcherService(string folderPath, string filePath, int sleepMilliSec) {
            FolderPath = folderPath;
            FileName = filePath;
            SleepMilliSec = sleepMilliSec;
        }

        private string GetFilePath(string fileName) => Path.Combine(FolderPath, fileName);

        void IWatcherService.RunScanRoop() {
            var oldFileNames = Directory.GetFiles(FolderPath);
            var oldLastWrite = File.GetLastWriteTime(GetFilePath(FileName));

            while (!dispose) {
                while (CanRaiseEvent) {
                    var newFileNames = Directory.GetFiles(FolderPath);
                    if (oldFileNames.Length != newFileNames.Length) {
                        OnChanged?.Invoke(this, new WatcherEventArgs(newFileNames.Except(oldFileNames), WatchType.FileCreated));
                        oldFileNames = newFileNames;
                    }

                    var newLastWrite = File.GetLastWriteTime(GetFilePath(FileName));
                    if (oldLastWrite != newLastWrite) {
                        OnChanged?.Invoke(this, new WatcherEventArgs(new List<string> { FileName }, WatchType.FileChanged));
                        oldLastWrite = newLastWrite;
                    }
                    Thread.Sleep(SleepMilliSec);
                }
                Thread.Sleep(SleepMilliSec);
            }
        }

        public void Dispose() {
            dispose = true;
        }
    }
}
