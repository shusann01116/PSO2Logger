using PSO2Logger.Models;
using System;
using System.Collections.Generic;

namespace PSO2Logger.Interfaces {
    /// <summary>
    /// <see cref="FolderPath"/>内から<see cref="FileName"/>を指定し、新しい行を取得するサービスです。
    /// </summary>
    /// <typeparam name="T">取り扱うログの型。</typeparam>
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

        /// <summary>
        /// Streamを初期化して、<see cref="GetNewLines"/>や<see cref="GetLine(int)"/>が利用できるようにします。
        /// </summary>
        void InitializeStream();
    }
}
