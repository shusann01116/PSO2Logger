using Microsoft.Extensions.Configuration;
using Prism.Commands;
using Prism.Mvvm;
using PSO2Logger.Core;
using PSO2Logger.Interfaces;
using PSO2Logger.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace PSO2Logger.Modules.Settings.ViewModels {
    public class SettingViewModel : BindableBase {
        private readonly IConfigurationRoot configuration;
        private readonly ILogService<ChatLine> logService;
        private readonly IWatcherService watcherService;

        private string _folderPath;

        public string FolderPath {
            get { return _folderPath; }
            set { 
                SetProperty(ref _folderPath, value);
                UpdateServiceFolderPath(value);
            }
        }

        private bool _showCommand;
        public bool IsShowCommand {
            get { return _showCommand; }
            set { SetProperty(ref _showCommand, value); }
        }

        public SettingViewModel(IConfigurationRoot configuration, ILogService<ChatLine> logService, IWatcherService watcherService) {
            this.configuration = configuration;
            this.logService = logService;
            this.watcherService = watcherService;

            FolderPath = logService.FolderPath;
            IsShowCommand = bool.Parse(configuration.GetSection(SettingKeys.IsShowCommand).Value);
        }

        private void UpdateServiceFolderPath(string folderPath) {
            if (!Directory.Exists(folderPath))
                return;

            this.logService.FolderPath = folderPath;
            this.watcherService.FolderPath = folderPath;
        }
    }
}
