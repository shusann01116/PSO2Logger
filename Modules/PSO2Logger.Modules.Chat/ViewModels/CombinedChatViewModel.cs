using Prism.Commands;
using Prism.Mvvm;
using Prism.Regions;
using PSO2Logger.Core;
using PSO2Logger.Interfaces;
using PSO2Logger.Models;
using PSO2Logger.Modules.Chat.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;

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
        }
    }
}
