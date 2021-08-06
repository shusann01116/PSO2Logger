using PSO2Logger.Interfaces.Core;
using System;
using System.Threading.Tasks;

namespace PSO2Logger.Interfaces {
    public interface IWatcherService {
        string FolderPath { get; set; }
        string FileName { get; set; }
        bool CanRaiseEvent { get; set; }

        event EventHandler<WatcherEventArgs> OnChanged;
        void RunScanRoop();
        async Task RunScanRoopAsync() {
            await Task.Run(RunScanRoop);
        }
    }

    public enum WatchType {
        FileChanged,
        FileCreated,
    }
}
