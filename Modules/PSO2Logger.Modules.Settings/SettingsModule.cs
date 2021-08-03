using Prism.Ioc;
using Prism.Modularity;
using Prism.Regions;
using PSO2Logger.Modules.Settings.Views;
using PSO2Logger.Core;

namespace PSO2Logger.Modules.Settings {
    public class SettingsModule : IModule {
        public void OnInitialized(IContainerProvider containerProvider) {
        }

        public void RegisterTypes(IContainerRegistry containerRegistry) {
            containerRegistry.RegisterForNavigation<Setting>();
        }
    }
}