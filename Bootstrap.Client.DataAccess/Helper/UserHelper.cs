using Bootstrap.Security;
using Bootstrap.Security.DataAccess;
using Longbow.Cache;
using Longbow.Data;
using System.Collections.Generic;

namespace Bootstrap.Client.DataAccess
{
    /// <summary>
    /// 用戶表相關操作類
    /// </summary>
    public static class UserHelper
    {
        /// <summary>
        /// 獲取所有用戶緩存數據鍵值
        /// </summary>
        public const string RetrieveUsersDataKey = "UserHelper-RetrieveUsers";

        /// <summary>
        /// 查詢所有用戶
        /// </summary>
        /// <returns></returns>
        public static IEnumerable<User> Retrieves() => CacheManager.GetOrAdd(RetrieveUsersDataKey, key => DbContextManager.Create<User>().Retrieves());
        
        /// <summary>
        /// 通過登錄名獲取登錄用戶方法
        /// </summary>
        /// <param name="userName"></param>
        /// <returns></returns>
        public static BootstrapUser RetrieveUserByUserName(string? userName) => string.IsNullOrEmpty(userName) ? null : CacheManager.GetOrAdd(string.Format("{0}-{1}", DbHelper.RetrieveUsersByNameDataKey, userName), k => DbContextManager.Create<User>()?.RetrieveUserByUserName(userName), DbHelper.RetrieveUsersByNameDataKey);
    }
}
