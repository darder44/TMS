using Longbow.Web.Mvc;
using PetaPoco;
using System;
using System.ComponentModel;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Data.Common;

namespace Bootstrap.Client.DataAccess
{
    /// <summary>
    /// 異常實體類
    /// </summary>
    [PrimaryKey("Id", AutoIncrement = true)]
    public class Exceptions
    {
        /// <summary>
        /// 獲得/設置 主鍵
        /// </summary>
        public string? Id { get; set; }

        /// <summary>
        /// 獲得/設置 主鍵
        /// </summary>
        public string AppDomainName { get; set; } = "";

        /// <summary>
        /// 獲得/設置 用戶請求頁面地址
        /// </summary>
        [DisplayName("請求網址")]
        public string ErrorPage { get; set; } = "";

        /// <summary>
        /// 獲得/設置 用戶 ID
        /// </summary>
        [DisplayName("用戶名")]
        public string? UserId { get; set; }

        /// <summary>
        /// 獲得/設置 用戶 IP
        /// </summary>
        [DisplayName("登錄主機")]
        public string? UserIp { get; set; }

        /// <summary>
        /// 獲得/設置 異常類型
        /// </summary>
        [DisplayName("異常類型")]
        public string? ExceptionType { get; set; }

        /// <summary>
        /// 獲得/設置 異常錯誤描述信息
        /// </summary>
        [DisplayName("異常描述")]
        public string Message { get; set; } = "";

        /// <summary>
        /// 獲得/設置 異常堆棧信息
        /// </summary>
        public string? StackTrace { get; set; }

        /// <summary>
        /// 獲得/設置 日誌時間戳
        /// </summary>
        [DisplayName("記錄時間")]
        public DateTime LogTime { get; set; }

        /// <summary>
        /// 獲得/設置 時間描述 2分鐘內為剛剛
        /// </summary>
        [ResultColumn]
        public string Period { get; set; } = "";

        /// <summary>
        /// 獲得/設置 分類信息
        /// </summary>
        public string Category { get; set; } = "";

        private static void ClearExceptions() => System.Threading.Tasks.Task.Run(() =>
        {
            DbManager.Create().Execute("delete from Exceptions where LogTime < @0", DateTime.Now.AddMonths(0 - DictHelper.RetrieveExceptionsLogPeriod()));
        });

        /// <summary>
        /// 
        /// </summary>
        /// <param name="ex"></param>
        /// <param name="additionalInfo"></param>
        /// <returns></returns>
        public virtual bool Log(Exception ex, NameValueCollection additionalInfo)
        {
            if (ex == null) return true;

            var errorPage = additionalInfo?["ErrorPage"] ?? (ex.GetType().Name.Length > 50 ? ex.GetType().Name.Substring(0, 50) : ex.GetType().Name);
            var loopEx = ex;
            var category = "App";
            while (loopEx != null)
            {
                if (typeof(DbException).IsAssignableFrom(loopEx.GetType()))
                {
                    category = "DB";
                    break;
                }
#pragma warning disable CS8600 // 將 null 文本或可能的 null 值轉換為非 null 類型。
                loopEx = loopEx.InnerException;
#pragma warning restore CS8600 // 將 null 文本或可能的 null 值轉換為非 null 類型。
            }
            try
            {
                // 防止數據庫寫入操作失敗後陷入死循環
                // fix https://gitee.com/LongbowEnterprise/dashboard/issues?id=I136OP
                using (var db = Longbow.Data.DbManager.Create())
                {
                    db.Insert(new Exceptions
                    {
                        AppDomainName = AppDomain.CurrentDomain.FriendlyName,
                        ErrorPage = errorPage,
                        UserId = additionalInfo?["UserId"],
                        UserIp = additionalInfo?["UserIp"],
                        ExceptionType = ex.GetType().FullName,
                        Message = ex.Message,
                        StackTrace = ex.StackTrace,
                        LogTime = DateTime.Now,
                        Category = category
                    });
                }
                ClearExceptions();
            }
            catch { }
            return true;
        }       
    }
}
