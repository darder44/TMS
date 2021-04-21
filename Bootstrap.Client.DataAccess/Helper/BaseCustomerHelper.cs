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
    public static class BaseCustomerHelper
    {
        /// <summary>
        /// 所有客戶主檔緩存數據鍵值
        /// </summary>
        public const string RetrievesCustomerDataKey = "BaseCustomerHelper-Retrieves";
        public static IEnumerable<BaseCustomer> Retrieves() => CacheManager.GetOrAdd(RetrievesCustomerDataKey, key => DbContextManager.Create<BaseCustomer>().Retrieves());

        public static BaseCustomer RetrieveByStorerKeyAndConsigneeKey(string StorerKey, string ConsigneeKey) => DbContextManager.Create<BaseCustomer>().RetrieveByStorerKeyAndConsigneeKey(StorerKey, ConsigneeKey);

        /// <summary>
        /// 保存新建/更新的
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static bool Save(BaseCustomer value)
        {
            var ret = DbContextManager.Create<BaseCustomer>().Save(value);
            if (ret) CacheHelper.ClearBaseCustomer();
            return ret;
        }

        public static bool Update(BaseCustomer value)
        {
            var ret = DbContextManager.Create<BaseCustomer>().Update(value);
            if (ret) CacheHelper.ClearBaseCustomer();
            return ret;
        }

        /// <summary>
        /// 刪除
        /// </summary>
        /// <param name="values"></param>
        public static bool Delete(IEnumerable<BaseCustomer> values)
        {
            var ret = DbContextManager.Create<BaseCustomer>().Delete(values);
            if (ret) CacheHelper.ClearBaseCustomer();
            return ret;
        }


    }


}
