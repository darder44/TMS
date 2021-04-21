using Bootstrap.Security.Mvc;
using Longbow.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using PetaPoco;
using System;
using System.Collections.Specialized;

namespace Bootstrap.Client.DataAccess
{
    /// <summary>
    /// 數據庫連接操作類
    /// </summary>
    internal static class DbManager
    {
        /// <summary>
        /// 創建 IDatabase 實例方法
        /// </summary>
        /// <param name="connectionName"></param>
        /// <param name="keepAlive"></param>
        /// <param name="enableLog">是否記錄日誌</param>
        /// <returns></returns>
        public static IDatabase Create(string? connectionName = null, bool keepAlive = false, bool enableLog = true)
        {
            if (Mappers.GetMapper(typeof(Exceptions), null) == null) Mappers.Register(typeof(Exceptions).Assembly, new BootstrapDataAccessConventionMapper());
            var db = Longbow.Data.DbManager.Create(connectionName, keepAlive);
            db.CommandTimeout = 270;
            db.ExceptionThrown += (sender, args) => args.Exception.Log(new NameValueCollection() { ["LastCmd"] = db.LastCommand });
            if (enableLog)
            {
                db.OnCommandExecuted(async provider =>
                {
                    var context = provider.GetRequiredService<IHttpContextAccessor>();
                    var userName = context.HttpContext?.User.Identity.Name;
                    var log = new DBLog()
                    {
                        LogTime = DateTime.Now,
                        SQL = db.LastCommand,
                        UserName = userName
                    };
                    await DBLogTask.AddDBLog(log).ConfigureAwait(false);
                });
            }
            return db;
        }

        /// <summary>
        /// 創建 Sqlite 類型的 IDatabase 實例
        /// </summary>
        /// <param name="connectionName">配置文件中配置的數據庫連接字符串名稱</param>
        /// <param name="keepAlive">是否保持連接，默認為 false</param>
        /// <returns></returns>
        public static IDatabase CreateSqlite(string? connectionName = "client", bool keepAlive = false)
        {
            // 此方法為演示同時連接不同的數據庫操作

            // 此處注釋為獲取連接字符串的不同方法
            //var conn = Bootstrap.Security.Mvc.BootstrapAppContext.Configuration["ConnectionStrings:client"];
            //var conn = Bootstrap.Security.Mvc.BootstrapAppContext.Configuration.GetSection("ConnectionStrings").GetValue("client", "");

            var conn = BootstrapAppContext.Configuration.GetConnectionString(connectionName);
            var db = Longbow.Data.DbManager.Create(new DatabaseOption()
            {
                ProviderName = DatabaseProviderType.SQLite,
                ConnectionString = conn,
                KeepAlive = keepAlive
            });
            db.ExceptionThrown += (sender, args) => args.Exception.Log(new NameValueCollection() { ["LastCmd"] = db.LastCommand });
            return db;
        }
    }
}
