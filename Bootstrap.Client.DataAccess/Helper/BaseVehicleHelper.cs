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
    public static class BaseVehicleHelper
    {
        /// <summary>
        /// 所有車輛資料緩存數據鍵值
        /// </summary>
        public const string RetrievesVehicleDataKey = "BaseVehicleHelper-Retrieves";
        public static IEnumerable<BaseVehicle> Retrieves() => CacheManager.GetOrAdd(RetrievesVehicleDataKey, key => DbContextManager.Create<BaseVehicle>().Retrieves());        

        public static BaseVehicle RetrieveByVehicleKeyAndDriver(string VehicleKey, string Driver) => DbContextManager.Create<BaseVehicle>().RetrieveByVehicleKeyAndDriver(VehicleKey, Driver);

        /// <summary>
        /// 保存新建/更新的
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static bool Save(BaseVehicle value)
        {
            var ret = DbContextManager.Create<BaseVehicle>().Save(value);
            if (ret) CacheHelper.ClearBaseVehicle();
            return ret;
        }

        public static bool Update(BaseVehicle value)
        {
            var ret = DbContextManager.Create<BaseVehicle>().Update(value);
            if (ret) CacheHelper.ClearBaseVehicle();
            return ret;
        }

        /// <summary>
        /// 刪除
        /// </summary>
        /// <param name="value"></param>
        public static bool Delete(IEnumerable<BaseVehicle> values)
        {
            var ret = DbContextManager.Create<BaseVehicle>().Delete(values);
            if (ret) CacheHelper.ClearBaseVehicle();
            return ret;
        }


    }


}
