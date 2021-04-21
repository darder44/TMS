﻿using Longbow.Web.Mvc;
using PetaPoco;
using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace Bootstrap.Client.DataAccess
{
    /// <summary>
    /// 操作日誌實體類
    /// </summary>
    [TableName("Logs")]
    public class Log : Trace
    {
        /// <summary>
        /// 獲得/設置 操作類型
        /// </summary>
        [DisplayName("操作類型")]
        public string CRUD { get; set; } = "";

        /// <summary>
        /// 獲得/設置 請求數據
        /// </summary>
        public string RequestData { get; set; } = "";

        /// <summary>
        /// 查詢所有操作日誌信息
        /// </summary>
        /// <param name="po"></param>
        /// <param name="startTime"></param>
        /// <param name="endTime"></param>
        /// <param name="opType"></param>
        /// <returns></returns>
        public new virtual Page<Log> RetrievePages(PaginationOption po, DateTime? startTime, DateTime? endTime, string? opType)
        {
            if (string.IsNullOrEmpty(po.Order)) po.Order = "desc";
            if (string.IsNullOrEmpty(po.Sort)) po.Sort = "LogTime";
            var sql = new Sql("select * from Logs");
            if (startTime.HasValue) sql.Where("LogTime >= @0", startTime.Value);
            if (endTime.HasValue) sql.Where("LogTime < @0", endTime.Value.AddDays(1).AddSeconds(-1));
            if (startTime == null && endTime == null) sql.Where("LogTime > @0", DateTime.Today.AddMonths(0 - DictHelper.RetrieveExceptionsLogPeriod()));
            if (!string.IsNullOrEmpty(opType)) sql.Where("CRUD = @0", opType);
            sql.OrderBy($"{po.Sort} {po.Order}");

            using var db = DbManager.Create();
            return db.Page<Log>(po.PageIndex, po.Limit, sql);
        }

        /// <summary>
        /// 查詢所有操作日誌
        /// </summary>
        /// <param name="startTime"></param>
        /// <param name="endTime"></param>
        /// <param name="opType"></param>
        /// <returns></returns>
        public new virtual IEnumerable<Log> RetrieveAll(DateTime? startTime, DateTime? endTime, string? opType)
        {
            var sql = new Sql("select CRUD, UserName, LogTime, Ip, Browser, OS, City, RequestUrl, RequestData from Logs");
            if (startTime.HasValue) sql.Where("LogTime >= @0", startTime.Value);
            if (endTime.HasValue) sql.Where("LogTime < @0", endTime.Value.AddDays(1).AddSeconds(-1));
            if (!string.IsNullOrEmpty(opType)) sql.Where("CRUD = @0", opType);
            sql.OrderBy("LogTime");

            using var db = DbManager.Create();
            return db.Fetch<Log>(sql);
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
                using var db = DbManager.Create();
                db.Execute("delete from Logs where LogTime < @0", dtm);
            });
        }

        /// <summary>
        /// 保存新增的日誌信息
        /// </summary>
        /// <param name="p"></param>
        /// <returns></returns>
        public virtual bool Save(Log p)
        {
            if (p == null) throw new ArgumentNullException(nameof(p));
            DeleteLogAsync();
            using var db = DbManager.Create();
            db.Save(p);
            return true;
        }
    }
}
