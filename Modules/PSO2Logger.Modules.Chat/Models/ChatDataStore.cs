using Prism.Mvvm;
using PSO2Logger.Core;
using PSO2Logger.Interfaces;
using PSO2Logger.Interfaces.Core;
using PSO2Logger.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PSO2Logger.Modules.Chat.Models {
    class ChatDataStore : BindableBase {
        private ILogService<ChatLine> _logService;
        private IWatcherService _watcherService;
        public ObservableCollection<ChatLine> ChatLines { get; set; } = new();

        public ChatDataStore(ILogService<ChatLine> logService, IWatcherService watcherService) {
            _logService = logService;
            _watcherService = watcherService;
            

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
