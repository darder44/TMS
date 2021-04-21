using Bootstrap.Security.DataAccess;
using System.Collections.Generic;

namespace Bootstrap.Client.DataAccess
{
    /// <summary>
    /// 
    /// </summary>
    public class App
    {
        /// <summary>
        /// 根據指定用戶名獲得授權應用程序集合
        /// </summary>
        /// <param name="userName"></param>
        /// <returns></returns>
        public virtual IEnumerable<string> RetrievesByUserName(string userName) => DbHelper.RetrieveAppsByUserName(userName);
    }
}
