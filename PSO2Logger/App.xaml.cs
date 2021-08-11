using Microsoft.Extensions.Configuration;
using Prism.Ioc;
using Prism.Modularity;
using PSO2Logger.Interfaces;
using PSO2Logger.Models;
using PSO2Logger.Modules.Chat;
using PSO2Logger.Modules.Settings;
using PSO2Logger.Services;
using PSO2Logger.Views;
using System.IO;
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
            containerRegistry.RegisterInstance<IConfiguration>(AddConfiguration());
            // TODO: Initialize Services with SavedFolderPath
            containerRegistry.RegisterSingleton<IWatcherService, MockWatcherService>();
            containerRegistry.RegisterSingleton<ILogService<ChatLine>, ChatLogService>();
        }

        protected override void ConfigureModuleCatalog(IModuleCatalog moduleCatalog) {
            moduleCatalog.AddModule<ChatModule>();
            moduleCatalog.AddModule<SettingsModule>();
        }

        private IConfiguration AddConfiguration() {
            IConfigurationBuilder builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json");
#if DEBUG
            builder.AddJsonFile("appsettings.Development.json", optional: true, reloadOnChange: true);
#else
            builder.AddJsonFile("appsettings.Production.json", optional: true, reloadOnChange: true);
#endif
            return builder.Build();
        }
    }
}
