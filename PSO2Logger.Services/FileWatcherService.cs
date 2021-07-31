using PSO2Logger.Interfaces;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace PSO2Logger.Services {
    class FileWatcherService : IWatcherService, IDisposable {
        /// <summary>
        /// ファイルが変更されたときに通知します。
        /// </summary>
        public event EventHandler<WatcherEventArgs> OnFileChanged;

        private bool dispose = false;

        public string FileName { get; private set; }
        public string FolderPath { get; private set; }

        public int SleepMilliSec { get; set; }
        public bool CanRaiseEvent { get; set; } = false;

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

            RunScanRoopAsync().Await();
        }

        /// <summary>
        /// 参照フォルダーパスを変更します。
        /// </summary>
        /// <param name="folderPath"></param>
        public void ChangeFolder(string folderPath) {
            if (!Directory.Exists(folderPath))
                throw new DirectoryNotFoundException();

            this.FolderPath = folderPath;
        }

        /// <summary>
        /// 参照ファイル名を変更します。
        /// </summary>
        /// <param name="fileName">ファイル名</param>
        public void ChangeFileName(string fileName) {
            var filePath = GetFilePath(fileName);

            if (!File.Exists(filePath))
                throw new FileNotFoundException();

            this.FileName = filePath;
        }

        private string GetFilePath(string fileName) => Path.Combine(FolderPath, fileName);

        private async Task RunScanRoopAsync() {
            await Task.Run(RunScanRoop);
        }

        private void RunScanRoop() {
            if (!Directory.Exists(FolderPath))
                throw new DirectoryNotFoundException();

            if (!File.Exists(FileName))
                throw new FileNotFoundException();

            var ofileNames = Directory.GetFiles(FolderPath);
            var olastWrite = File.GetLastWriteTime(FileName);

            while (!dispose) {
                while (CanRaiseEvent) {
                    var nfileNames = Directory.GetFiles(FolderPath);
                    if (ofileNames.Length != nfileNames.Length) {
                        OnFileChanged?.Invoke(this, new WatcherEventArgs(nfileNames.Except(ofileNames), WatchType.FileCreated));
                        ofileNames = nfileNames;
                    }

                    var nlastWrite = File.GetLastWriteTime(FileName);
                    if (olastWrite != nlastWrite) {
                        OnFileChanged?.Invoke(this, new WatcherEventArgs(new List<string> { FileName }, WatchType.FileChanged));
                        olastWrite = nlastWrite;
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
