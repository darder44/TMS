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
    public static class NormalOrdersHelper
    {
        public class SaveOrderDetail
        {
            public NormalOrders Header { get; set; }
            public IEnumerable<NormalOrders> Detail { get; set; }
        }

        public static IEnumerable<NormalOrders> RetrieveDetailByTMSKey(string TMSKey, string facility) => DbContextManager.Create<NormalOrders>().RetrieveDetailByTMSKey(TMSKey, facility);
        public static IEnumerable<NormalOrders> RetrievesExcel(string facility) => DbContextManager.Create<NormalOrders>().RetrievesExcel(facility);
        public static NormalOrders RetrieveSkuByStorerKey(string sku, string storerkey, string facility) => DbContextManager.Create<NormalOrders>().RetrieveSkuByStorerKey(sku,storerkey, facility);
        public static NormalOrders RetrieveConsigneeKeyByStorerKeyAndConsigneeKey(string ConsigneeKey, string storerkey, string facility) => DbContextManager.Create<NormalOrders>().RetrieveConsigneeKeyByStorerKeyAndConsigneeKey(ConsigneeKey,storerkey, facility);
        public static NormalOrders RetrieveExternOrderKey(string ExternOrderKey, string storerkey) => DbContextManager.Create<NormalOrders>().RetrieveExternOrderKey(ExternOrderKey,storerkey);

        public static IEnumerable<NormalOrders> Retrieves(string storers, string ordertypes, string tmskey, string externorderkey, string orderdates, string orderdatee, string deliverydates, string deliverydatee, string adddates, string adddatee, string facility) 
            => DbContextManager.Create<NormalOrders>().Retrieves(storers, ordertypes, tmskey, externorderkey, orderdates, orderdatee, deliverydates, deliverydatee, adddates, adddatee, facility);

        public class SaveOrdersDetail
        {
            public NormalOrders Header { get; set; }
            public IEnumerable<NormalOrders> Detail { get; set; }
        }

        public static ServerResponse SaveHeaderDetail(SaveOrderDetail value, string UserName, string Facility) 
        {
            var ret = DbContextManager.Create<NormalOrders>().SaveHeaderDetail(value.Header,value.Detail,UserName,Facility);
            if (ret.success) CacheHelper.ClearAll();
            return ret;
        }

        public static ServerResponse DeleteOrders(IEnumerable<string> TMSKey, string UserName) 
        {
            var ret = DbContextManager.Create<NormalOrders>().DeleteOrders(TMSKey, UserName);
            if (ret.success) CacheHelper.ClearAll();
            return ret;
        }

        //ASN轉RC訂單
        public static ServerResponse ASNtoRC(string asnkey, string storerkey, string consigneekey, string warehouse, string facility, string user) => DbContextManager.Create<NormalOrders>().ASNtoRC(asnkey, storerkey, consigneekey, warehouse, facility, user);

    }
}
