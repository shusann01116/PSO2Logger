using Prism.Mvvm;
using PSO2Logger.Core;
using PSO2Logger.Interfaces;
using PSO2Logger.Interfaces.Core;
using PSO2Logger.Models;
using System.Collections.ObjectModel;

namespace PSO2Logger.Modules.Chat.ViewModels {
    public class ChatBodyViewModel : BindableBase {
        private ILogService<ChatLine> logService;
        private IWatcherService watcherService;

        private string folderPath;
        public string FolderPath {
            get { return folderPath; }
            set { SetProperty(ref folderPath, value); }
        }

        private string fileName;
        public string FileName {
            get { return fileName; }
            set { SetProperty(ref fileName, value); }
        }

        private ObservableCollection<ChatLine> chatsLines;
        public ObservableCollection<ChatLine> ChatLines {
            get { return chatsLines; }
            set { SetProperty(ref chatsLines, value); }
        }

        public ChatBodyViewModel(ILogService<ChatLine> logService, IWatcherService watcherService) {
            FolderPath = PSO2Directories.DefaultLogPath;
            FileName = PSO2Directories.GetLatestFileName(FolderPath);

            this.logService = logService;
            logService.FolderPath = FolderPath;
            logService.InitializeStream();

            this.watcherService = watcherService;
            watcherService.FolderPath = FolderPath;

            ChatLines = new ObservableCollection<ChatLine>(logService.GetNewLines());

            watcherService.OnChanged += OnChanged;
        }

        private void OnChanged(object sender, WatcherEventArgs e) {
            switch (e.WatchType) {
                case WatchType.FileChanged:
                    OnFileChanged();
                    break;
                case WatchType.FileCreated:
                    OnFileCreated();
                    break;
                default:
                    break;
            }
        }

        private void OnFileCreated() {

        }

        private void OnFileChanged() {

        }
    }
}
