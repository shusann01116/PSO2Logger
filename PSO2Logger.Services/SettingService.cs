using PSO2Logger.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PSO2Logger.Services {
    public class SettingService : ISettingService {
        private Dictionary<object, object> settings;

        public SettingService() {
            settings = new Dictionary<object, object>();
        }

        public TValue Getvalue<TValue>(object key) {
            settings.TryGetValue(key, out var value);
            if (value is not TValue result)
                return default(TValue);

            return result;
        }

        public bool TryGetValue<TValue>(object key, ref TValue result) {
            if (settings.TryGetValue(key, out var innnerResult) is false)
                return false;

            if (innnerResult is not TValue) {
                return false;
            }

            result = (TValue)innnerResult;
            return true;
        }

        public void SetValue(KeyValuePair<object, object> keyValue) {
            if (settings.ContainsKey(keyValue.Key))
                throw new ArgumentException("The key is already registered.");

            settings.Add(keyValue.Key, keyValue.Value);
        }
    }
}
