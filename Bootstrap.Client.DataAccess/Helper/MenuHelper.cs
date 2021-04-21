using Bootstrap.Security;
using Bootstrap.Security.DataAccess;
using Bootstrap.Security.Mvc;
using Longbow.Cache;
using Longbow.Data;
using System.Collections.Generic;
using System.Linq;

namespace Bootstrap.Client.DataAccess
{
    /// <summary>
    /// 菜單操作類
    /// </summary>
    public static class MenuHelper
    {
        /// <summary>
        /// 通過當前用戶獲取所有菜單數據緩存鍵名稱 "BootstrapMenu-RetrieveMenus"
        /// </summary>
        public const string RetrieveMenusAll = DbHelper.RetrieveMenusAll;

        /// <summary>
        /// 獲取指定用戶的應用程序菜單
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="activeUrl"></param>
        /// <returns></returns>
        public static IEnumerable<BootstrapMenu> RetrieveAppMenus(string userName, string activeUrl)
        {
            var appId = BootstrapAppContext.AppId;
            var menus = RetrieveAllMenus(userName).Where(m => m.Category == "1" && m.IsResource == 0 && m.Application == appId);
            return DbHelper.CascadeMenus(menus, activeUrl);
        }

        /// <summary>
        /// 通過用戶獲得所有菜單
        /// </summary>
        /// <param name="userName"></param>
        /// <returns></returns>
        public static IEnumerable<BootstrapMenu> RetrieveAllMenus(string userName) => CacheManager.GetOrAdd($"{RetrieveMenusAll}-{userName}", key => DbContextManager.Create<Menu>()?.RetrieveAllMenus(userName), RetrieveMenusAll) ?? new BootstrapMenu[0];

        /// <summary>
        /// 通過當前用戶名與指定菜單路徑獲取此菜單下所有授權按鈕集合 (userName, url, auths) => bool
        /// </summary>
        /// <param name="userName">當前操作用戶名</param>
        /// <param name="url">資源按鈕所屬菜單</param>
        /// <param name="auths">資源授權碼</param>
        /// <returns></returns>
        public static bool AuthorizateButtons(string userName, string url, string auths) => DbContextManager.Create<Menu>()?.AuthorizateButtons(userName, url, auths) ?? false;

        /// <summary>
        /// 通過當前用戶名獲得所有菜單，層次化後集合
        /// </summary>
        /// <param name="userName"></param>
        /// <returns></returns>
        public static IEnumerable<BootstrapMenu> RetrieveMenus(string userName)
        {
            var menus = RetrieveAllMenus(userName);
            return DbHelper.CascadeMenus(menus);
        }

    }
}
