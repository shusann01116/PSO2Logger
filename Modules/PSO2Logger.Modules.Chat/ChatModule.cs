using Prism.Ioc;
using Prism.Modularity;
using Prism.Regions;
using PSO2Logger.Core;
using PSO2Logger.Modules.Chat.Views;

namespace PSO2Logger.Modules.Chat {
    public class ChatModule : IModule {
        public void OnInitialized(IContainerProvider containerProvider) {
            var regionManager = containerProvider.Resolve<IRegionManager>();
            regionManager.RegisterViewWithRegion<CombinedChat>(RegionName.ContentRegion);
        }

        public void RegisterTypes(IContainerRegistry containerRegistry) {
            containerRegistry.RegisterForNavigation<CombinedChat>();
        }
    }
}
