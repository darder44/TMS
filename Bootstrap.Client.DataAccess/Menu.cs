using Bootstrap.Security;
using Bootstrap.Security.DataAccess;
using System.Collections.Generic;

namespace Bootstrap.Client.DataAccess
{
    /// <summary>
    /// 菜單實體類
    /// </summary>
    public class Menu : BootstrapMenu
    {
        /// <summary>
        /// 通過當前用戶名獲得所有菜單
        /// </summary>
        /// <param name="userName">當前登錄的用戶名</param>
        /// <returns></returns>
        public virtual IEnumerable<BootstrapMenu> RetrieveAllMenus(string userName) => DbHelper.RetrieveAllMenus(userName);

        /// <summary>
        /// 通過當前用戶名與指定菜單路徑獲取此菜單下所有授權按鈕集合 (userName, url, auths) => bool
        /// </summary>
        /// <param name="userName">當前操作用戶名</param>
        /// <param name="url">資源按鈕所屬菜單</param>
        /// <param name="auths">資源授權碼</param>
        /// <returns></returns>
        public virtual bool AuthorizateButtons(string userName, string url, string auths)
        {
            var menus = MenuHelper.RetrieveAllMenus(userName);
            return DbHelper.AuthorizateButtons(menus, url, auths);
        }
    }
}
