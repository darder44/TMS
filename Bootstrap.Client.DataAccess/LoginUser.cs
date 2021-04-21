using Longbow.Web.Mvc;
using PetaPoco;
using System;
using System.Collections.Generic;

namespace Bootstrap.Client.DataAccess
{
    /// <summary>
    /// 登錄用戶信息實體類
    /// </summary>
    [TableName("LoginLogs")]
    public class LoginUser
    {
        /// <summary>
        /// 獲得/設置 Id
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// 獲得/設置 用戶名
        /// </summary>
        public string UserName { get; set; }

        /// <summary>
        /// 獲得/設置 登錄時間
        /// </summary>
        public DateTime LoginTime { get; set; }

        /// <summary>
        /// 獲得/設置 登錄IP地址
        /// </summary>
        public string Ip { get; set; }

        /// <summary>
        /// 獲得/設置 登錄瀏覽器
        /// </summary>
        public string Browser { get; set; }

        /// <summary>
        /// 獲得/設置 登錄操作系統
        /// </summary>
        public string OS { get; set; }

        /// <summary>
        /// 獲得/設置 登錄地點
        /// </summary>
        public string City { get; set; }

        /// <summary>
        /// 獲得/設置 登錄是否成功
        /// </summary>
        public string Result { get; set; }

        /// <summary>
        /// 獲得/設置 用戶 UserAgent
        /// </summary>
        public string UserAgent { get; set; }

        /// <summary>
        /// 保存登錄用戶數據
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public virtual bool Log(LoginUser user)
        {
            var db = DbManager.Create();
            db.Save(user);
            return true;
        }

        /// <summary>
        /// 獲得登錄用戶的分頁數據
        /// </summary>
        /// <param name="po"></param>
        /// <param name="startTime"></param>
        /// <param name="endTime"></param>
        /// <param name="ip"></param>
        /// <returns></returns>
        public virtual Page<LoginUser> RetrieveByPages(PaginationOption po, DateTime? startTime, DateTime? endTime, string ip)
        {
            var sql = new Sql("select * from LoginLogs");
            if (startTime.HasValue) sql.Where("LoginTime >= @0", startTime.Value);
            if (endTime.HasValue) sql.Where("LoginTime < @0", endTime.Value.AddDays(1));
            if (!string.IsNullOrEmpty(ip)) sql.Where("ip = @0", ip);
            sql.OrderBy($"{po.Sort} {po.Order}");
            return DbManager.Create().Page<LoginUser>(po.PageIndex, po.Limit, sql);
        }

        /// <summary>
        /// 獲取所有登錄數據
        /// </summary>
        /// <returns></returns>
        public virtual IEnumerable<LoginUser> RetrieveAll(DateTime? startTime, DateTime? endTime, string ip)
        {
            var sql = new Sql("select UserName, LoginTime, Ip, Browser, OS, City, Result from LoginLogs");
            if (startTime.HasValue) sql.Where("LoginTime >= @0", startTime.Value);
            if (endTime.HasValue) sql.Where("LoginTime < @0", endTime.Value.AddDays(1));
            if (!string.IsNullOrEmpty(ip)) sql.Where("ip = @0", ip);
            sql.OrderBy($"LoginTime");
            return DbManager.Create().Fetch<LoginUser>(sql);
        }
    }
}
