using Prism.Mvvm;
using PSO2Logger.Core;
using PSO2Logger.Interfaces;
using PSO2Logger.Interfaces.Core;
using PSO2Logger.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PSO2Logger.Modules.Chat.Models {
    class ChatDataStore : BindableBase {
        private ILogService<ChatLine> logService;
        private IWatcherService watcherService;
        public ObservableCollection<ChatLine> ChatLines { get; set; } = new();
        public ObservableCollection<ChatLine> PublicChats { get; set; } = new();
        public ObservableCollection<ChatLine> PartyChats { get; set; } = new();
        public ObservableCollection<ChatLine> TeamChats { get; set; } = new();
        public ObservableCollection<ChatLine> WhisperChats { get; set; } = new();
        public ObservableCollection<ChatLine> GroupChats { get; set; } = new();

        public ChatDataStore(ILogService<ChatLine> logService, IWatcherService watcherService) {
            this.logService = logService;
            this.watcherService = watcherService;

            this.logService.InitializeStream();
            ChatLines.AddRange(logService.GetNewLines());

            this.watcherService.FolderPath = PSO2Directories.DefaultLogPath;
            this.watcherService.FileName = PSO2Directories.GetLatestFileName(PSO2Directories.DefaultLogPath);
            this.watcherService.CanRaiseEvent = true;
            this.watcherService.OnChanged += OnChanged;
            this.watcherService.RunScanRoopAsync().Await();

            ChatLines.CollectionChanged += OnChatLinesChanged;
            AddLinesToEachCategory(ChatLines);
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

        // TODO: Fileの変更を検知
        private void OnFileCreated() {
            throw new NotImplementedException();
        }

        private void OnFileChanged() {
            ChatLines.AddRange(this.logService.GetNewLines());
        }

        private void OnChatLinesChanged(object sender, NotifyCollectionChangedEventArgs e) {
            if (e.NewItems is null) return;

            AddLinesToEachCategory(e.NewItems.Cast<ChatLine>().ToList());
        }

        private void AddLinesToEachCategory(IList<ChatLine> lines) {
            PublicChats.AddRange(lines.Where(x => x.ChatType == ChatType.PUBLIC));
            PartyChats.AddRange(lines.Where(x => x.ChatType == ChatType.PARTY));
            TeamChats.AddRange(lines.Where(x => x.ChatType == ChatType.GUILD));
            WhisperChats.AddRange(lines.Where(x => x.ChatType == ChatType.REPLY));
            GroupChats.AddRange(lines.Where(x => x.ChatType == ChatType.GROUP));
        }
    }
}
