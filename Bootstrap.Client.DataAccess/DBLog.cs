﻿using Longbow.Web.Mvc;
using PetaPoco;
using System;
using System.ComponentModel;

namespace Bootstrap.Client.DataAccess
{
    /// <summary>
    /// 後台數據庫腳本執行日誌實體類
    /// </summary>
    [TableName("DBLogs")]
    public class DBLog
    {

        /// <summary>
        /// 獲得/設置 主鍵ID
        /// </summary>
        public string? Id { get; set; }

        /// <summary>
        /// 獲得/設置 當前登陸名
        /// </summary>
        [DisplayName("所屬用戶")]
        public string? UserName { get; set; }

        /// <summary>
        /// 獲得/設置 數據庫執行腳本
        /// </summary>
        [DisplayName("腳本內容")]
        public string SQL { get; set; } = "";

        /// <summary>
        /// 獲取/設置 用戶角色關聯狀態 checked 標示已經關聯 '' 標示未關聯
        /// </summary>
        [DisplayName("執行時間")]
        public DateTime LogTime { get; set; }

        /// <summary>
        /// 查詢所有SQL日誌信息
        /// </summary>
        /// <param name="po"></param>
        /// <param name="startTime"></param>
        /// <param name="endTime"></param>
        /// <param name="userName"></param>
        /// <returns></returns>
        public virtual Page<DBLog> RetrievePages(PaginationOption po, DateTime? startTime, DateTime? endTime, string? userName)
        {
            if (string.IsNullOrEmpty(po.Sort)) po.Sort = "LogTime";
            if (string.IsNullOrEmpty(po.Order)) po.Order = "desc";
            var sql = new Sql("select * from DBLogs");
            if (startTime.HasValue) sql.Where("LogTime >= @0", startTime.Value);
            if (endTime.HasValue) sql.Where("LogTime < @0", endTime.Value.AddDays(1).AddSeconds(-1));
            if (startTime == null && endTime == null) sql.Where("LogTime > @0", DateTime.Today.AddMonths(0 - DictHelper.RetrieveExceptionsLogPeriod()));
            if (!string.IsNullOrEmpty(userName)) sql.Where("UserName = @0", userName);
            sql.OrderBy($"{po.Sort} {po.Order}");

            using var db = DbManager.Create();
            return db.Page<DBLog>(po.PageIndex, po.Limit, sql);
        }

        /// <summary>
        /// 刪除日誌信息
        /// </summary>
        /// <returns></returns>
        private static void DeleteLogAsync()
        {
            System.Threading.Tasks.Task.Run(() =>
            {
                var dtm = DateTime.Now.AddMonths(0 - DictHelper.RetrieveLogsPeriod());
                DbManager.Create().Execute("delete from DBLogs where LogTime < @0", dtm);
            });
        }

        /// <summary>
        /// 保存新增的日誌信息
        /// </summary>
        /// <param name="p"></param>
        /// <returns></returns>
        public virtual bool Save(DBLog p)
        {
            if (p == null) throw new ArgumentNullException(nameof(p));
            DeleteLogAsync();
            using var db = DbManager.Create(enableLog: false);
            db.Save(p);
            return true;
        }
    }
}
