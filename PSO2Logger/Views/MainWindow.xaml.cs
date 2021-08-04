using MahApps.Metro.Controls;
using Prism.Ioc;
using Prism.Regions;
using PSO2Logger.Core;

namespace PSO2Logger.Views {
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : MetroWindow {
        public MainWindow() {
            InitializeComponent();

            var regionManager = ContainerLocator.Current.Resolve<IRegionManager>();
            RegionManager.SetRegionName(this.ContentRegion, RegionName.ContentRegion);
            RegionManager.SetRegionManager(this.ContentRegion, regionManager);
        }
    }
}
