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
    public static class NormalStandbyOrdersHelper
    {

        public static IEnumerable<NormalStandbyOrders> Retrieves(string facility) => DbContextManager.Create<NormalStandbyOrders>().RetrievesByOrders(facility);

        public static IEnumerable<NormalStandbyOrders> RetrieveSelectedOrders(IEnumerable<string> TMSKey, string facility)
        {
            string[] arr = TMSKey.ToArray();
            var data = Retrieves(facility);
            data = data.Where(t => arr.Contains(t.TMSKey));
            return data;
        }

        public static IEnumerable<NormalStandbyOrders> RetrievesCustomerTemp(string facility) => DbContextManager.Create<NormalStandbyOrders>().RetrievesCustomerTemp(facility);
        public static IEnumerable<NormalStandbyOrders> RetrievesReturnOrders(string facility) => DbContextManager.Create<NormalStandbyOrders>().RetrievesReturnOrders(facility);
        public static NormalStandbyOrders RetrievesCustomerByConsigneeKeyAndStorerKey(string storerkey, string consigneekey, string tmskey, string facility) => DbContextManager.Create<NormalStandbyOrders>().RetrievesCustomerByConsigneeKeyAndStorerKey(storerkey, consigneekey, tmskey, facility);
        public static ServerResponse SaveCustomer(NormalStandbyOrders value, string UserName, string facility) => DbContextManager.Create<NormalStandbyOrders>().SaveCustomer(value,UserName, facility);
        public static CreateNewRouteResponse CreateNewRoute(BaseVehicle Driver, IEnumerable<NormalStandbyOrders> SelectedOrders, string UserName, string DoRouteDate, string Dock, string ExpectDate, string ExpectTime, bool Transfer, string ToFacility)
        {
            var ret = DbContextManager.Create<NormalStandbyOrders>().CreateNewRoute(Driver, SelectedOrders, UserName, DoRouteDate, Dock, ExpectDate, ExpectTime, Transfer, ToFacility);
            if (ret.success) CacheHelper.ClearAll();
            return ret;
        }

        public static string SelectRouteNo(IEnumerable<NormalStandbyOrders> SelectOrders)
        {
            return DbContextManager.Create<NormalStandbyOrders>().SelectRouteNo(SelectOrders);
        }

        public static int OrderImport(string UserName, string facility) 
        {
            var ret = DbContextManager.Create<NormalStandbyOrders>().OrderImport(UserName, facility);
            CacheHelper.ClearAll();
            return ret;
        }

        public static bool ReserveOrders(IEnumerable<NormalStandbyOrders> SelectOrders,string UserName) 
        {
            var ret = DbContextManager.Create<NormalStandbyOrders>().ReserveOrders(SelectOrders,UserName);
            if (ret) CacheHelper.ClearAll();
            return ret;
        }

        public static bool ReturnOrder(IEnumerable<NormalStandbyOrders> SelectOrders,string UserName) 
        {
            var ret = DbContextManager.Create<NormalStandbyOrders>().ReturnOrder(SelectOrders,UserName);
            if (ret) CacheHelper.ClearAll();
            return ret;
        }

        public static bool UpdateSkuData(IEnumerable<NormalStandbyOrders> SelectOrders,string UserName) 
        {
            var ret = DbContextManager.Create<NormalStandbyOrders>().UpdateSkuData(SelectOrders,UserName);
            if (ret) CacheHelper.ClearAll();
            return ret;
        }

    }

    /// <summary>
    /// ??????modal
    /// </summary>
    public class NormalStandbyOrdersModal
    {
        /// <summary>
        /// ????????????
        /// </summary>
        public BaseVehicle Driver { get; set; }
        /// <summary>
        /// ????????????
        /// </summary>
        public IEnumerable<NormalStandbyOrders> SelectedOrders { get; set; }
        /// <summary>
        /// ?????????
        /// </summary>
        public string DoRouteDate { get; set; }
        /// <summary>
        /// ????????????
        /// </summary>
        public string Dock { get; set; }
        /// <summary>
        /// ??????????????????
        /// </summary>
        public string ExpectDate { get; set; }
        /// <summary>
        /// ??????????????????
        /// </summary>
        public string ExpectTime { get; set; }
        /// <summary>
        /// ????????????
        /// </summary>
        public bool Transfer { get; set; }
        /// <summary>
        /// ???????????????
        /// </summary>
        public string ToFacility { get; set; }
    }
    /// <summary>
    /// ????????????modal
    /// </summary>
    public class SelectRouteNoFromOrders
    {
        /// <summary>
        /// ????????????
        /// </summary>
        public IEnumerable<NormalStandbyOrders> SelectedOrders { get; set; }
    }

    /// <summary>
    /// ????????????
    /// </summary>
    public class CreateNewRouteResponse
    {
        /// <summary>
        /// ????????????
        /// </summary>
        public bool success { get; set; }
        /// <summary>
        /// ??????
        /// </summary>
        public string message { get; set; }
        /// <summary>
        /// ??????
        /// </summary>
        public string routeno { get; set; }
    }
}
