using Bootstrap.Security;
using Bootstrap.Security.DataAccess;
using Longbow.Web.Mvc;
using PetaPoco;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using Longbow.Data;
using Longbow.Cache;
using Bootstrap.Client.DataAccess;
using static Bootstrap.Client.DataAccess.APP_DriverInfo;

namespace Bootstrap.Client.DataAccess.Helper
{
    public class NormalCutOrdersBody
    {
        public string TMSKey {get; set; }
        public IEnumerable<NormalCutOrders> Details { get; set; }
        public IEnumerable<NormalCutOrders> NewDetails { get; set; }
    }
    public static class NormalCutOrdersHelper
    {

        public static IEnumerable<NormalCutOrders> RetrieveOrders(string facility) => DbContextManager.Create<NormalCutOrders>().RetrieveOrders(facility);

        public static IEnumerable<NormalCutOrders> RetrieveDetailByTMSKey(string TMSKey) => DbContextManager.Create<NormalCutOrders>().RetrieveDetailByTMSKey(TMSKey);

        public static NormalCutOrders RetrieveSkuByTMSKey(double qty, string tmskey, string sku, string orderlinenumber) => DbContextManager.Create<NormalCutOrders>().RetrieveSkuByTMSKey(qty,tmskey,sku, orderlinenumber);

        
        public static bool CutOrders(NormalCutOrdersBody value,string UserName)
        {
            var ret = DbContextManager.Create<NormalCutOrders>().CutOrders(value, UserName);
            if (ret) CacheHelper.ClearAll();
            return ret;
        }



    }
}
