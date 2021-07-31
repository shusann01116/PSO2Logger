using Prism.Ioc;
using PSO2Logger.Views;
using System.Windows;

namespace PSO2Logger {
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App {
        protected override Window CreateShell() {
            return Container.Resolve<MainWindow>();
        }

        protected override void RegisterTypes(IContainerRegistry containerRegistry) {

        }
    }
}
