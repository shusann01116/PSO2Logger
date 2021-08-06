using Prism.Mvvm;
using Prism.Regions;
using PSO2Logger.Core;
using PSO2Logger.Interfaces;
using PSO2Logger.Interfaces.Core;
using PSO2Logger.Models;
using PSO2Logger.Modules.Chat.Models;
using System.Collections.ObjectModel;

namespace PSO2Logger.Modules.Chat.ViewModels {
    public class ChatBodyViewModel : BindableBase, INavigationAware {
        private ChatDataStore chatDataStore;

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

        public ChatType ChatType { get; set; }

        private ObservableCollection<ChatLine> chatsLines;
        public ObservableCollection<ChatLine> ChatLines {
            get { return chatsLines; }
            set { SetProperty(ref chatsLines, value); }
        }

        public ChatBodyViewModel() {
        }

        public void OnNavigatedTo(NavigationContext navigationContext) {
            this.chatDataStore = (ChatDataStore)navigationContext.Parameters[nameof(ChatDataStore)];
            this.ChatType = (ChatType)navigationContext.Parameters[nameof(ChatType)];
        }

        public bool IsNavigationTarget(NavigationContext navigationContext) {
            return false;
        }

        public void OnNavigatedFrom(NavigationContext navigationContext) {

        }
    }
}
