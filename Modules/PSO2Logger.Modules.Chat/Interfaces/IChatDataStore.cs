using PSO2Logger.Interfaces;
using PSO2Logger.Models;
using Reactive.Bindings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PSO2Logger.Modules.Chat.Interfaces {
    interface IChatDataStore {
        ReactiveCollection<ChatLine> ChatLines { get; }
        ILogService<ChatLine> LogService { get; }
        IWatcherService WatcherService { get; }

        IList<ChatLine> GetNewLines();
        void OverrideLogService(ILogService<ChatLine> logService);
        void OverrideWatcherService(IWatcherService watcherService);
    }
}
