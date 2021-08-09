using System.Collections.Generic;

namespace PSO2Logger.Interfaces {
    public interface ISettingService {
        /// <summary>
        /// Gets value corresponds to <paramref name="key"/>
        /// </summary>
        /// <typeparam name="TValue">Return type.</typeparam>
        /// <param name="key">Key to search.</param>
        /// <returns>Value which corresponds to value.</returns>
        public TValue Getvalue<TValue>(object key);

        /// <summary>
        /// Try get value which corresponds to <paramref name="key"/>
        /// </summary>
        /// <typeparam name="TValue">Return type.</typeparam>
        /// <param name="key">Key to search.</param>
        /// <param name="result">Value which corresponds to value.</param>
        /// <returns>Represent if corresponding <paramref name="key"/> was found.</returns>
        public bool TryGetValue<TValue>(object key, ref TValue result);

        /// <summary>
        /// Set value to Settings dictionary.
        /// </summary>
        /// <param name="keyValue">KeyValuePair to add.</param>
        public void SetValue(KeyValuePair<object, object> keyValue);
    }
}
