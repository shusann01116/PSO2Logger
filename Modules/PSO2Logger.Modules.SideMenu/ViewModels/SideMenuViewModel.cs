using Prism.Commands;
using Prism.Mvvm;
using Prism.Regions;
using PSO2Logger.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace PSO2Logger.Modules.SideMenu.ViewModels {
    public class SideMenuViewModel : BindableBase {
        IRegionManager regionManager;

        private DelegateCommand<string> requestNavigateCommand;
        public DelegateCommand<string> RequestNavigateCommand => requestNavigateCommand ??= new DelegateCommand<string>(RequestNavigate);

        public SideMenuViewModel(IRegionManager regionManager) {
            this.regionManager = regionManager;
        }

        void RequestNavigate(string navigatePath) {
            _ = navigatePath ?? throw new ArgumentNullException($"{navigatePath} cannnot be null");

            regionManager.RequestNavigate(RegionNames.ContentRegion, navigatePath);
        }

    }
}
