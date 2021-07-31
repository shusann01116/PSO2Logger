using System;
using System.Text;

namespace PSO2Logger.Interfaces {
    public interface IWatcherService {
        string FolderPath { get; }
        string FileName { get; }

        event EventHandler<WatcherEventArgs> OnFileChanged;

        void ChangeFileName(string fileName);
        void ChangeFolder(string folderPath);
    }

    public enum WatchType {
        FileChanged,
        FileCreated,
    }
}
