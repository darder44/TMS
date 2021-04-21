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
    public static class BaseStorerHelper
    {

        /// <summary>
        /// 貨主主檔緩存數據鍵值
        /// </summary>
        public const string RetrievesStorersDataKey = "BaseStorerHelper-Retrieves";
        public static IEnumerable<BaseStorer> Retrieves() => CacheManager.GetOrAdd(RetrievesStorersDataKey, key => DbContextManager.Create<BaseStorer>().Retrieves());
        /// <summary>
        /// 訂單資訊
        /// </summary>
        public static IEnumerable<BaseStorer> RetrievesByReportShipList(IEnumerable<string> values) => DbContextManager.Create<BaseStorer>().RetrievesByReportShipList(values);

        public static BaseStorer RetrieveByStorerKey(string StorerKey) => DbContextManager.Create<BaseStorer>().RetrieveByStorerKey(StorerKey);


        /// <summary>
        /// 保存新建/更新的
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static bool Save(BaseStorer value)
        {
            var ret = DbContextManager.Create<BaseStorer>().Save(value);
            if (ret) CacheCleanUtility.ClearCache(cacheKey: $"{BaseStorerHelper.RetrievesStorersDataKey}*");
            return ret;
        }

        public static bool Update(BaseStorer value)
        {
            var ret = DbContextManager.Create<BaseStorer>().Update(value);
            if (ret) CacheCleanUtility.ClearCache(cacheKey: $"{BaseStorerHelper.RetrievesStorersDataKey}*");
            return ret;
        }

        /// <summary>
        /// 刪除
        /// </summary>
        /// <param name="Values"></param>

        public static bool Delete(IEnumerable<BaseStorer> Values)
        {
            var ret = DbContextManager.Create<BaseStorer>().Delete(Values);
            if (ret) CacheCleanUtility.ClearCache(cacheKey: $"{BaseStorerHelper.RetrievesStorersDataKey}*");
            return ret;
        }
    }
}
