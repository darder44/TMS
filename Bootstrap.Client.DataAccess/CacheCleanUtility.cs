using Longbow.Cache;
using System.Collections.Generic;
using System.Linq;

namespace Bootstrap.Client.DataAccess
{
    /// <summary>
    /// 緩存清理操作類
    /// </summary>
    public static class CacheCleanUtility
    {
        private const string RetrieveAllRolesDataKey = "BootstrapAdminRoleMiddleware-RetrieveRoles";
        /// <summary>
        /// 清理緩存
        /// </summary>
        /// <param name="roleIds"></param>
        /// <param name="userIds"></param>
        /// <param name="groupIds"></param>
        /// <param name="menuIds"></param>
        /// <param name="appIds"></param>
        /// <param name="dictIds"></param>
        /// <param name="cacheKey"></param>
        public static void ClearCache(IEnumerable<string> roleIds = null, IEnumerable<string> userIds = null, IEnumerable<string> groupIds = null, IEnumerable<string> menuIds = null, IEnumerable<string> appIds = null, IEnumerable<string> dictIds = null, string cacheKey = null)
        {
            var cacheKeys = new List<string>();
            var corsKeys = new List<string>();
            if (roleIds != null)
            {
                cacheKeys.Add(MenuHelper.RetrieveMenusAll + "*");
                cacheKeys.Add(RetrieveAllRolesDataKey + "*");
                corsKeys.Add(MenuHelper.RetrieveMenusAll + "*");
            }
            if (userIds != null)
            {
                userIds.ToList().ForEach(id =>
                {          
                    cacheKeys.Add(MenuHelper.RetrieveMenusAll + "*");
                });
            }
            if (groupIds != null)
            {
                cacheKeys.Add(MenuHelper.RetrieveMenusAll + "*");
                cacheKeys.Add(RetrieveAllRolesDataKey + "*");             
                corsKeys.Add(MenuHelper.RetrieveMenusAll + "*");
            }
            if (menuIds != null)
            {
                cacheKeys.Add(MenuHelper.RetrieveMenusAll + "*");
                corsKeys.Add(MenuHelper.RetrieveMenusAll + "*");
            }
            if (appIds != null)
            {
                cacheKeys.Add("AppHelper-RetrieveAppsBy*");
                corsKeys.Add("AppHelper-RetrieveAppsBy*");
            }
            if (dictIds != null)
            {
                cacheKeys.Add(DictHelper.RetrieveDictsDataKey + "*");
                corsKeys.Add(DictHelper.RetrieveDictsDataKey + "*");
            }
            if (cacheKey != null)
            {
                cacheKeys.Add(cacheKey);
                corsKeys.Add(cacheKey);
            }
            CacheManager.Clear(cacheKeys);
            CacheManager.CorsClear(corsKeys.Distinct());
        }
    }
}
