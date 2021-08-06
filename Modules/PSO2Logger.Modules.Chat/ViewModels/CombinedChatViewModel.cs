using Prism.Commands;
using Prism.Mvvm;
using Prism.Regions;
using PSO2Logger.Core;
using PSO2Logger.Interfaces;
using PSO2Logger.Models;
using PSO2Logger.Modules.Chat.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace PSO2Logger.Modules.Chat.ViewModels {
    public class CombinedChatViewModel : BindableBase {
        private readonly ChatDataStore chatDataStore;

        public CombinedChatViewModel(IRegionManager regionManager, ILogService<ChatLine> logService, IWatcherService watcherService) {
            chatDataStore = new(logService, watcherService);
            var navigationParameters = GenerateNavigationParameters(chatDataStore).ToArray();

            // TODO: Refactor this section, having magic number.
            regionManager.RequestNavigate(RegionName.PublicChatRegion, ViewName.ChatBody, navigationParameters[0]);
            regionManager.RequestNavigate(RegionName.PublicChatRegion, ViewName.ChatBody, navigationParameters[1]);
            regionManager.RequestNavigate(RegionName.PublicChatRegion, ViewName.ChatBody, navigationParameters[2]);
            regionManager.RequestNavigate(RegionName.PublicChatRegion, ViewName.ChatBody, navigationParameters[3]);
            regionManager.RequestNavigate(RegionName.PublicChatRegion, ViewName.ChatBody, navigationParameters[4]);
        }

        private static IEnumerable<NavigationParameters> GenerateNavigationParameters(ChatDataStore dataStore) {
            foreach (ChatType type in Enum.GetValues(typeof(ChatType))) {
                yield return new NavigationParameters() {
                    { nameof(ChatDataStore), dataStore },
                    { nameof(ChatType), type },
                };
            }
        }
    }
}
