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
    public static class BaseDictionaryHelper
    {
        /// <summary>
        /// 所有字典表緩存數據鍵值
        /// </summary>
        public const string RetrievesDictionaryDataKey = "BaseDictionaryHelper-Retrieves";
        public static IEnumerable<BaseDictionary> Retrieves() => CacheManager.GetOrAdd(RetrievesDictionaryDataKey, key => DbContextManager.Create<BaseDictionary>().Retrieves());

        /// <summary>
        /// 保存新建/更新的
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static bool Save(BaseDictionary value)
        {
            var ret = DbContextManager.Create<BaseDictionary>().Save(value);
            if (ret) CacheHelper.ClearBestDictionary();
            return ret;
        }

        public static bool Update(BaseDictionary value)
        {
            var ret = DbContextManager.Create<BaseDictionary>().Update(value);
            if (ret) CacheHelper.ClearBestDictionary();
            return ret;
        }

        /// <summary>
        /// 刪除
        /// </summary>
        /// <param name="values"></param>
        public static bool Delete(IEnumerable<BaseDictionary> values)
        {
            var ret = DbContextManager.Create<BaseDictionary>().Delete(values);
            if (ret) CacheHelper.ClearBestDictionary();
            return ret;
        }

    }


}
