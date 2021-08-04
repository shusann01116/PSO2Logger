using Prism.Commands;
using Prism.Mvvm;
using Prism.Regions;
using PSO2Logger.Core;
using PSO2Logger.Modules.Chat.Views;

namespace PSO2Logger.ViewModels {
    public class MainWindowViewModel : BindableBase {
        private string _title = "Prism Application";
        public string Title {
            get { return _title; }
            set { SetProperty(ref _title, value); }
        }

        private IRegionManager _regionManager;

        private DelegateCommand<string> _requestNavigateCommand;
        public DelegateCommand<string> RequestNavigateCommand => _requestNavigateCommand ??= new DelegateCommand<string>(Navigate);
        public MainWindowViewModel(IRegionManager regionManager) {
            _regionManager = regionManager;
        }

        public void Navigate(string navigatePath) {
            _regionManager.RequestNavigate(RegionName.ContentRegion, navigatePath);
        }
    }
}
