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
    public static class BaseShippingCompanyHelper
    {
        /// <summary>
        /// 所有貨運公司緩存數據鍵值
        /// </summary>
        public const string RetrievesShippingCompanyDataKey = "BaseShippingCompanyHelper-Retrieves";
        public static IEnumerable<BaseShippingCompany> Retrieves() => CacheManager.GetOrAdd(RetrievesShippingCompanyDataKey, key => DbContextManager.Create<BaseShippingCompany>().Retrieves());

        public static BaseShippingCompany RetrieveByCompanyKey(string CompanyKey) => DbContextManager.Create<BaseShippingCompany>().RetrieveByCompanyKey(CompanyKey);
        /// <summary>
        /// 保存新建/更新的
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static bool Save(BaseShippingCompany value)
        {
            var ret = DbContextManager.Create<BaseShippingCompany>().Save(value);
            if (ret) CacheHelper.ClearBaseShippingCompany();
            return ret;
        }

        public static bool Update(BaseShippingCompany value)
        {
            var ret = DbContextManager.Create<BaseShippingCompany>().Update(value);
            if (ret) CacheHelper.ClearBaseShippingCompany();
            return ret;
        }

        /// <summary>
        /// 刪除
        /// </summary>
        /// <param name="values"></param>
        public static bool Delete (IEnumerable<string> values)
        {
            var ret = DbContextManager.Create<BaseShippingCompany>().Delete(values);
            if (ret) CacheHelper.ClearBaseShippingCompany();
            return ret;
        }


    }


}
