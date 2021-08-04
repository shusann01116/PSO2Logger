using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;

namespace PSO2Logger.Modules.Settings.ViewModels {
    public class SettingViewModel : BindableBase {
        private string _folderPath;
        public string FolderPath {
            get { return _folderPath; }
            set { SetProperty(ref _folderPath, value); }
        }

        public SettingViewModel() {

        }
    }
}
