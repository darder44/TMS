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
    public static class BaseFacilityHelper
    {
        /// <summary>
        /// 所有轉運站緩存數據鍵值
        /// </summary>
        public const string RetrievesFacilityDataKey = "BaseFacilityHelper-Retrieves";
        public static IEnumerable<BaseFacility> Retrieves() => CacheManager.GetOrAdd(RetrievesFacilityDataKey, key => DbContextManager.Create<BaseFacility>().Retrieves());

        /// <summary>
        /// 保存新建/更新的
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static bool Save(BaseFacility value)
        {
            var ret = DbContextManager.Create<BaseFacility>().Save(value);
            if (ret) CacheHelper.ClearBestFacility();
            return ret;
        }

        public static bool Update(BaseFacility value)
        {
            var ret = DbContextManager.Create<BaseFacility>().Update(value);
            if (ret) CacheHelper.ClearBestFacility();
            return ret;
        }

        /// <summary>
        /// 刪除
        /// </summary>
        /// <param name="values"></param>
        public static bool Delete(IEnumerable<string> values)
        {
            var ret = DbContextManager.Create<BaseFacility>().Delete(values);
            if (ret) CacheHelper.ClearBestFacility();
            return ret;
        }

    }


}
