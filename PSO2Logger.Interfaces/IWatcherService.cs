using PSO2Logger.Interfaces.Core;
using System;

namespace PSO2Logger.Interfaces {
    public interface IWatcherService {
        string FolderPath { get; set; }
        string FileName { get; set; }

        event EventHandler<WatcherEventArgs> OnChanged;
    }

    public enum WatchType {
        FileChanged,
        FileCreated,
    }
}
