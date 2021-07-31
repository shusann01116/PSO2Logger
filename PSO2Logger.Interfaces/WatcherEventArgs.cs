using System;
using System.Collections.Generic;

namespace PSO2Logger.Interfaces {
    public class WatcherEventArgs : EventArgs {
        public IEnumerable<string> FileNames { get; set; }
        public WatchType WatchType { get; set; }

        public WatcherEventArgs(IEnumerable<string> fileNames, WatchType watchType) {
            this.FileNames = fileNames;
            this.WatchType = watchType;
        }
    }
}
