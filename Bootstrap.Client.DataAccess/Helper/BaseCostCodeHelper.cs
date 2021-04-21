using Bootstrap.Security;
using Bootstrap.Security.DataAccess;
using Longbow.Cache;
using Longbow.Web.Mvc;
using PetaPoco;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using Longbow.Data;

namespace Bootstrap.Client.DataAccess.Helper
{
    public static class BaseCostCodeHelper
    {
        /// <summary>
        /// 所有計費代碼緩存數據鍵值
        /// </summary>
        public const string RetrievesCostCodeDataKey = "BaseCostCodeHelper-Retrieves";
        public static IEnumerable<BaseCostCode> Retrieves() => CacheManager.GetOrAdd(RetrievesCostCodeDataKey, key => DbContextManager.Create<BaseCostCode>().Retrieves()); 

        public static IEnumerable<BaseCostCode> RetrieveByStorerkeyAndCostCode(string Storerkey, string CostCode) => DbContextManager.Create<BaseCostCode>().RetrieveByStorerkeyAndCostCode(Storerkey, CostCode);

        public static IEnumerable<BaseCostCode> RetrieveByStorerkeyAndCostKind(string Storerkey) => DbContextManager.Create<BaseCostCode>().RetrieveByStorerkeyAndCostKind(Storerkey);

        public static IEnumerable<BaseCostCode> RetrieveCostCodeByStorerkeyAndCostKind(string Storerkey, string CostKind) => DbContextManager.Create<BaseCostCode>().RetrieveCostCodeByStorerkeyAndCostKind(Storerkey, CostKind);

        /// <summary>
        /// 保存新建/更新的
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static bool Save(BaseCostCode value)
        {
            var ret = DbContextManager.Create<BaseCostCode>().Save(value);
            if (ret) CacheHelper.ClearBestCostCode();
            return ret;
        }

        public static bool Update(BaseCostCode value)
        {
            var ret = DbContextManager.Create<BaseCostCode>().Update(value);
            if (ret) CacheHelper.ClearBestCostCode();
            return ret;
        }

        /// <summary>
        /// 刪除
        /// </summary>
        /// <param name="values"></param>
        public static bool Delete(IEnumerable<BaseCostCode> values)
        {
            var ret = DbContextManager.Create<BaseCostCode>().Delete(values);
            if (ret) CacheHelper.ClearBestCostCode();
            return ret;
        }

    }
}
