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
    public static class Base_SLHelper
    {
        /// <summary>
        /// 所有倉別緩存數據鍵值
        /// </summary>
        public const string RetrieveSLsDataKey = "Base_SLHelper-RetrieveSLs";
        /// <summary>
        /// 當前貨主的倉別緩存數據鍵值
        /// </summary>
        public const string RetrieveByStorerkeyDataKey = "Base_SLHelper-RetrieveByStorerkey";
        public static IEnumerable<Base_SL> Retrieves() => CacheManager.GetOrAdd(RetrieveSLsDataKey, key => DbContextManager.Create<Base_SL>().Retrieves()); 
        public static IEnumerable<Base_SL> RetrieveByStorerkey(string StorerKey) => string.IsNullOrEmpty(StorerKey) ? null : CacheManager.GetOrAdd(string.Format("{0}-{1}", RetrieveByStorerkeyDataKey, StorerKey), key => DbContextManager.Create<Base_SL>()?.RetrieveByStorerkey(StorerKey), RetrieveByStorerkeyDataKey);

        public static bool Save(Base_SL sl)
        {
            var ret = DbContextManager.Create<Base_SL>().Save(sl);
            if (ret) CacheHelper.ClearBestSL();
            return ret;
        }
        public static bool Delete(IEnumerable<string> values)
        {
            var ret = DbContextManager.Create<Base_SL>().Delete(values);
            if (ret) CacheHelper.ClearBestSL();
            return ret;
        }
    }
}
