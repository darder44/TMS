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
    public static class BaseVehicleTypeHelper
    {
        /// <summary>
        /// 所有車種緩存數據鍵值
        /// </summary>
        public const string RetrievesVehicleTypeDataKey = "BaseVehicleTypeHelper-Retrieves";
        /// <summary>
        /// 查詢車種
        /// </summary>
        /// <param name=""></param>
        /// <returns></returns>
        public static IEnumerable<BaseVehicleType> Retrieves() => CacheManager.GetOrAdd(RetrievesVehicleTypeDataKey, key => DbContextManager.Create<BaseVehicleType>().Retrieves());     
    }


}
