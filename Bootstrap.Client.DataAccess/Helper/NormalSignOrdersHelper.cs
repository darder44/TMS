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
    public class NormalSignOrdersBody
    {
        public NormalSignOrders Header { get; set; }
        public IEnumerable<NormalSignOrders> Details { get; set; }
        public string SignOrderStatus { get; set; }
        public string SdnSendDate { get; set; }
    }

    public class CheckSdnBackBody
    {
        public string TMSKey { get; set; }
        public string SdnSendWho { get; set; }
    }

    public class NormalSignOrdersCheckBody
    {
        public IEnumerable<NormalSignOrders> Details { get; set; }

        public string SignOrderStatus { get; set; }
    }

    public class NormalUnShippedOrdersBody
    {
        public IEnumerable<NormalSignOrders> selections { get; set; }
        public string rsccode { get; set; }
        public string rbccode { get; set; }
        public string rbcname { get; set; }
    }

    public class NormalCostDataBody
    {
        public IEnumerable<NormalSignOrders> CostData { get; set; }
        public string TMSKey { get; set; }
    }

    public class NormalShippingCostResult
    {
        public IEnumerable<NormalSignOrders> ShippingCost { get; set; }
        public IEnumerable<NormalSignOrders> RouteData { get; set; }
        public IEnumerable<NormalSignOrders> SignOrderData { get; set; }
    }

    public class NormalShippingCostResultDetail
    {
        public NormalSignOrders OrderData { get; set; }
        public NormalSignOrders DeliveryDatexAddressData { get; set; }
        public NormalSignOrders RouteNoxStorerKeyData { get; set; }
    }

    /// <summary>
    /// modal
    /// </summary>
    public class NormalDeliveryAbnormalModal
    {
        /// <summary>
        /// 已選訂單
        /// </summary>
        public IEnumerable<NormalSignOrders> Selects { get; set; }
        /// <summary>
        /// 異常原因
        /// </summary>
        public string ErrorCode { get; set; }
        
    }

    public static class NormalSignOrdersHelper
    {

        public static IEnumerable<NormalSignOrders> RetrieveOrders(string facility, string storers, string tmskey, string routeno, string externorderkey, string sdnback, string signstatus, string orderdates, string orderdatee, string doroutedates, string doroutedatee, string deliverydates, string deliverydatee) => DbContextManager.Create<NormalSignOrders>().RetrieveOrders(facility, storers, tmskey, routeno, externorderkey, sdnback, signstatus, orderdates, orderdatee, doroutedates, doroutedatee, deliverydates, deliverydatee);

        public static IEnumerable<NormalSignOrders> RetrieveDetailByTMSKey(string TMSKey, string facility) => DbContextManager.Create<NormalSignOrders>().RetrieveDetailByTMSKey(TMSKey, facility);

        public static IEnumerable<NormalSignOrders> RetrieveOrderTrackByTMSKey(string TMSKey, string facility) => DbContextManager.Create<NormalSignOrders>().RetrieveOrderTrackByTMSKey(TMSKey, facility);

        public static IEnumerable<NormalSignOrders> RetrieveSignOrderDataByTMSKey(string TMSKey) => DbContextManager.Create<NormalSignOrders>().RetrieveSignOrderDataByTMSKey(TMSKey);



        public static IEnumerable<NormalSignOrders> RetrievesShippingCost(string TMSKey, string facility, string User) => DbContextManager.Create<NormalSignOrders>().RetrievesShippingCost(TMSKey, facility, User);

        public static IEnumerable<NormalSignOrders> RetrievesRouteData(string TMSKey, string facility) => DbContextManager.Create<NormalSignOrders>().RetrievesRouteData(TMSKey, facility);

        // public static NormalSignOrders RetrievesOrderData(string TMSKey, string facility) => DbContextManager.Create<NormalSignOrders>().RetrievesOrderData(TMSKey, facility);

        // public static NormalSignOrders RetrievesDeliveryDatexAddressData(string ShipToAddress, string DeliveryDate, string facility) => DbContextManager.Create<NormalSignOrders>().RetrievesDeliveryDatexAddressData(ShipToAddress, DeliveryDate, facility);

        // public static NormalSignOrders RetrievesRouteNoxStorerKeyData(string RouteNo, string StorerKey, string facility) => DbContextManager.Create<NormalSignOrders>().RetrievesRouteNoxStorerKeyData(StorerKey, RouteNo, facility);

        public static NormalSignOrders RetrieveCostCode(string StorerKey, string CostCode) => DbContextManager.Create<NormalSignOrders>().RetrieveCostCode(StorerKey, CostCode);

        public static NormalSignOrders RetriveVehicleKeyByRouteNo(string RouteNo) => DbContextManager.Create<NormalSignOrders>().RetriveVehicleKeyByRouteNo(RouteNo);

        public static IEnumerable<NormalSignOrders> RetriveVehicleKeyByTMSKey(string TMSKey) => DbContextManager.Create<NormalSignOrders>().RetriveVehicleKeyByTMSKey(TMSKey);

        public static string CheckVehicleKey(string VehicleKey, string Driver, string Receiver) => DbContextManager.Create<NormalSignOrders>().CheckVehicleKey(VehicleKey,Driver,Receiver);

        public static bool CheckCostCode(string CostCode, string StorerKey) => DbContextManager.Create<NormalSignOrders>().CheckCostCode(CostCode,StorerKey);

        public static IEnumerable<NormalSignOrders> RetrieveDriver(string VehicleKey) => DbContextManager.Create<NormalSignOrders>().RetrieveDriver(VehicleKey);

        public static IEnumerable<NormalSignOrders> RetrieveReceiver(string VehicleKey) => DbContextManager.Create<NormalSignOrders>().RetrieveReceiver(VehicleKey);

        
        public static bool SignOrders(NormalSignOrdersBody value,string UserName,IEnumerable<string> Role)
        {
            var ret = DbContextManager.Create<NormalSignOrders>().SignOrders(value, UserName, Role);
            if (ret) CacheHelper.ClearAll();
            return ret;
        }

        public static string CheckSignOrders(NormalSignOrdersCheckBody value,string UserName)
        {
            var ret = DbContextManager.Create<NormalSignOrders>().CheckSignOrders(value, UserName);
            if (ret == "1") CacheHelper.ClearAll();
            return ret;
        }

        
        public static bool NormalOrders(IEnumerable<NormalSignOrders> value,string UserName,IEnumerable<string> Role)
        {
            var ret = DbContextManager.Create<NormalSignOrders>().NormalOrders(value, UserName, Role);
            if (ret) CacheHelper.ClearAll();
            return ret;
        }

        public static bool CheckOrderStatus(IEnumerable<NormalSignOrders> value,string UserName,IEnumerable<string> Role)
        {
            var ret = DbContextManager.Create<NormalSignOrders>().CheckOrderStatus(value, UserName, Role);
            return ret;
        }

        public static bool UnShippedOrders(NormalUnShippedOrdersBody value,string UserName,IEnumerable<string> Role)
        {
            var ret = DbContextManager.Create<NormalSignOrders>().UnShippedOrders(value, UserName, Role);
            if (ret) CacheHelper.ClearAll();
            return ret;
        }

        public static string BreakRoute(NormalDeliveryAbnormalModal value, string UserName, IEnumerable<string> Role)
        {
            string ret = DbContextManager.Create<NormalSignOrders>().BreakRoute(value, UserName, Role);
            CacheHelper.ClearAll();
            return ret;
        }

        public static string ClearSignOrders(IEnumerable<NormalSignOrders> value, string UserName,IEnumerable<string> Role)
        {
            var ret = DbContextManager.Create<NormalSignOrders>().ClearSignOrders(value, UserName, Role);
            if (ret == "1") CacheHelper.ClearAll();
            return ret;
        }

        public static string Calculate(IEnumerable<NormalSignOrders> value, string UserName,IEnumerable<string> Role)
        {
            var ret = DbContextManager.Create<NormalSignOrders>().Calculate(value, UserName, Role);
            if (ret == "1") CacheHelper.ClearAll();
            return ret;
        }

        public static string CheckSdnBack(string TMSKey,IEnumerable<string> Role,BootstrapUser User)
        {
            var ret = DbContextManager.Create<NormalSignOrders>().CheckSdnBack(TMSKey, Role, User);
            if (ret == "1") CacheHelper.ClearAll();
            return ret;
        }
        public static bool SaveCostData(NormalCostDataBody value, string UserName)
        {
            var ret = DbContextManager.Create<NormalSignOrders>().SaveCostData(value, UserName);
            if (ret) CacheHelper.ClearAll();
            return ret;
        }



    }
}
