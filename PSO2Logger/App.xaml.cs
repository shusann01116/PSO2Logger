using Prism.Ioc;
using Prism.Modularity;
using PSO2Logger.Interfaces;
using PSO2Logger.Models;
using PSO2Logger.Modules.Chat;
using PSO2Logger.Modules.Settings;
using PSO2Logger.Services;
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
            // TODO: Initialize Services with SavedFolderPath
            containerRegistry.RegisterSingleton<IWatcherService, MockWatcherService>();
            containerRegistry.RegisterSingleton<ILogService<ChatLine>, ChatLogService>();
        }

        protected override void ConfigureModuleCatalog(IModuleCatalog moduleCatalog) {
            moduleCatalog.AddModule<ChatModule>();
            moduleCatalog.AddModule<SettingsModule>();
        }
    }
}
