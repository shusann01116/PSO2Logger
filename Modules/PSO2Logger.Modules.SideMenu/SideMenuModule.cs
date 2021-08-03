using Prism.Ioc;
using Prism.Modularity;
using Prism.Regions;
using PSO2Logger.Core;
using PSO2Logger.Modules.SideMenu.Views;

namespace PSO2Logger.Modules.SideMenu {
    public class SideMenuModule : IModule {
        public void OnInitialized(IContainerProvider containerProvider) {
            var regionManager = containerProvider.Resolve<IRegionManager>();
            regionManager.RegisterViewWithRegion(RegionNames.HamburgerMenuRegion, typeof(Views.SideMenu));
        }

        public void RegisterTypes(IContainerRegistry containerRegistry) {
            
        }
    }
}