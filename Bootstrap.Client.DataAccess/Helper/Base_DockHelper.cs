using Bootstrap.Security;
using Bootstrap.Security.DataAccess;
using Longbow.Cache;
using Longbow.Web.Mvc;
using Longbow.Data;
using PetaPoco;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace Bootstrap.Client.DataAccess.Helper
{
    public static class Base_DockHelper
    {
        /// <summary>
        /// 所有碼頭緩存數據鍵值
        /// </summary>
        public const string RetrieveDocksDataKey = "Base_DockHelper-Retrieves";
        public static IEnumerable<Base_Dock> Retrieves() => CacheManager.GetOrAdd(RetrieveDocksDataKey, key => DbContextManager.Create<Base_Dock>().Retrieves());
        public static bool Save(Base_Dock value)
        {
            var ret = DbContextManager.Create<Base_Dock>().Save(value);
            if (ret) CacheHelper.ClearBestDock();
            return ret;
        }
        public static bool Update(Base_Dock value)
        {
            var ret = DbContextManager.Create<Base_Dock>().Update(value);
            if (ret) CacheHelper.ClearBestDock();
            return ret;
        }
        public static bool Delete(IEnumerable<string> values)
        {
            var ret = DbContextManager.Create<Base_Dock>().Delete(values);
            if (ret) CacheHelper.ClearBestDock();
            return ret;
        }

    }
}
