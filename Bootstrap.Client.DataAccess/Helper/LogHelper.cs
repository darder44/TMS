using Longbow.Web.Mvc;
using Longbow.Data;
using PetaPoco;
using System;
using System.Collections.Generic;

namespace Bootstrap.Client.DataAccess
{
    /// <summary>
    /// 操作日誌相關操作類
    /// </summary>
    public static class LogHelper
    {
        /// <summary>
        /// 保存新增的日誌信息
        /// </summary>
        /// <param name="log"></param>
        /// <returns></returns>
        public static bool Save(Log log)
        {
            log.LogTime = DateTime.Now;
            return DbContextManager.Create<Log>()?.Save(log) ?? false;
        }
    }
}
