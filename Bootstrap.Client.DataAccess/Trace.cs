using Longbow.Web.Mvc;
using PetaPoco;
using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace Bootstrap.Client.DataAccess
{
    /// <summary>
    /// 用戶訪問數據實體類
    /// </summary>
    [TableName("Traces")]
    public class Trace
    {
        /// <summary>
        /// 獲得/設置 操作日誌主鍵ID
        /// </summary>
        public string? Id { get; set; }

        /// <summary>
        /// 獲得/設置 用戶名稱
        /// </summary>
        [DisplayName("用戶名稱")]
        public string UserName { get; set; } = "";

        /// <summary>
        /// 獲得/設置 操作時間
        /// </summary>
        [DisplayName("操作時間")]
        public DateTime LogTime { get; set; }

        /// <summary>
        /// 獲得/設置 客戶端IP
        /// </summary>
        [DisplayName("登錄主機")]
        public string Ip { get; set; } = "";

        /// <summary>
        /// 獲得/設置 客戶端地點
        /// </summary>
        [DisplayName("操作地點")]
        public string City { get; set; } = "";

        /// <summary>
        /// 獲得/設置 客戶端瀏覽器
        /// </summary>
        [DisplayName("瀏覽器")]
        public string Browser { get; set; } = "";

        /// <summary>
        /// 獲得/設置 客戶端操作系統
        /// </summary>
        [DisplayName("操作系統")]
        public string OS { get; set; } = "";

        /// <summary>
        /// 獲取/設置 請求網址
        /// </summary>
        [DisplayName("操作頁面")]
        public string RequestUrl { get; set; } = "";

        /// <summary>
        /// 獲得/設置 客戶端 UserAgent
        /// </summary>
        [DisplayName("UserAgent")]
        public string UserAgent { get; set; } = "";

        /// <summary>
        /// 獲得/設置 客戶端 Referer
        /// </summary>
        [DisplayName("Referer")]
        public string Referer { get; set; } = "";

        /// <summary>
        /// 保存用戶訪問數據歷史記錄
        /// </summary>
        /// <param name="p"></param>
        public virtual bool Save(Trace p)
        {
            using var db = DbManager.Create();
            db.Save(p);
            ClearTraces();
            return true;
        }

        private static void ClearTraces() => System.Threading.Tasks.Task.Run(() =>
        {
            using var db = DbManager.Create();
            return db.Execute("delete from Traces where LogTime < @0", DateTime.Now.AddMonths(0 - DictHelper.RetrieveAccessLogPeriod()));
        });
    }
}
