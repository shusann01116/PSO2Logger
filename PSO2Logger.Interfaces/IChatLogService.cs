using PSO2Logger.Models;
using System;
using System.Collections.Generic;

namespace PSO2Logger.Interfaces {
    public interface ILogService<T> {
        string FolderPath { get; set; }
        string FileName { get; set; }

        /// <summary>
        /// 新しく追加された列を返します。
        /// </summary>
        /// <returns></returns>
        IEnumerable<T> GetNewLines();
        
        /// <summary>
        /// 一致した列を返します。
        /// </summary>
        /// <param name="id">検索に用いる数値。</param>
        /// <returns>一致した列</returns>
        T GetLine(int id);
        void InitializeStream();
    }
}
