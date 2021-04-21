using Bootstrap.Security.DataAccess;
using Longbow.Cache;
using Longbow.Data;
using System.Collections.Generic;

namespace Bootstrap.Client.DataAccess
{
    /// <summary>
    /// 應用操作幫助類
    /// </summary>
    public static class AppHelper
    {
        /// <summary>
        /// 根據指定用戶名獲得授權應用程序集合
        /// </summary>
        /// <param name="userName"></param>
        /// <returns></returns>
        public static IEnumerable<string> RetrievesByUserName(string userName) => CacheManager.GetOrAdd($"{DbHelper.RetrieveAppsByUserNameDataKey}-{userName}", key => DbContextManager.Create<App>()?.RetrievesByUserName(userName), DbHelper.RetrieveAppsByUserNameDataKey) ?? new string[0];
    }
}
