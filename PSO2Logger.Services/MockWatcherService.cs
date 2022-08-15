using PSO2Logger.Interfaces;
using PSO2Logger.Interfaces.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace PSO2Logger.Services {
    // TODO: ReWriteWatcherService which implements new file detector.
    public class MockWatcherService : IWatcherService {
        public string FolderPath { get; set; }
        public string FileName { get; set; }
        public bool CanRaiseEvent { get; set; } = true;

        public event EventHandler<WatcherEventArgs> OnChanged;

        public void RunScanRoop() {
            var watcherEventArgs = new WatcherEventArgs(null, WatchType.FileChanged);
            while (CanRaiseEvent is true) {
                Thread.Sleep(1000);
                OnChanged?.Invoke(this, watcherEventArgs);
            }
        }
    }
}
