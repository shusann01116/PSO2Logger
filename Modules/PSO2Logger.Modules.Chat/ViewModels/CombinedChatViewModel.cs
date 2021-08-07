using Prism.Mvvm;
using PSO2Logger.Interfaces;
using PSO2Logger.Models;
using PSO2Logger.Modules.Chat.Models;
using System.Collections.ObjectModel;
using System.Windows.Data;

namespace PSO2Logger.Modules.Chat.ViewModels {
    public class CombinedChatViewModel : BindableBase {
        private readonly ChatDataStore chatDataStore;

        public ObservableCollection<ChatLine> PublicChats { get; set; }
        public ObservableCollection<ChatLine> PartyChats { get; set; } 
        public ObservableCollection<ChatLine> TeamChats { get; set; }
        public ObservableCollection<ChatLine> WhisperChats { get; set; } 
        public ObservableCollection<ChatLine> GroupChats { get; set; } 

        public CombinedChatViewModel(ILogService<ChatLine> logService, IWatcherService watcherService) {
            chatDataStore = new ChatDataStore(logService, watcherService);

            PublicChats = chatDataStore.PublicChats;
            PartyChats = chatDataStore.PartyChats;
            TeamChats = chatDataStore.TeamChats;
            WhisperChats = chatDataStore.WhisperChats;
            GroupChats = chatDataStore.GroupChats;

            BindingOperations.EnableCollectionSynchronization(this.PublicChats, new object());
            BindingOperations.EnableCollectionSynchronization(this.PartyChats, new object());
            BindingOperations.EnableCollectionSynchronization(this.TeamChats, new object());
            BindingOperations.EnableCollectionSynchronization(this.WhisperChats, new object());
            BindingOperations.EnableCollectionSynchronization(this.GroupChats, new object());
        }
    }
}
