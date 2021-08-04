using Prism.Ioc;
using Prism.Modularity;
using PSO2Logger.Modules.Chat.Views;

namespace PSO2Logger.Modules.Chat {
    public class ChatModule : IModule {
        public void OnInitialized(IContainerProvider containerProvider) {
        }

        public void RegisterTypes(IContainerRegistry containerRegistry) {
           containerRegistry.RegisterForNavigation<ChatBody>();
        }
    }
}
