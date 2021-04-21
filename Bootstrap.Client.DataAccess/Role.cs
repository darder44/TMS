using Bootstrap.Security.DataAccess;
using System.Collections.Generic;

namespace Bootstrap.Client.DataAccess
{
    /// <summary>
    /// 
    /// </summary>
    public class Role
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="userName"></param>
        /// <returns></returns>
        public virtual IEnumerable<string> RetrievesByUserName(string userName) => DbHelper.RetrieveRolesByUserName(userName);

        /// <summary>
        /// 根據菜單url查詢某個所擁有的角色
        /// 從NavigatorRole表查
        /// 從Navigators -> GroupNavigatorRole -> Role查查詢某個用戶所擁有的角色
        /// </summary>
        /// <param name="url"></param>
        /// <param name="appId"></param>
        /// <returns></returns>
        public virtual IEnumerable<string> RetrievesByUrl(string url, string appId) => DbHelper.RetrieveRolesByUrl(url, appId);
    }
}
