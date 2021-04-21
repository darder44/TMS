using Bootstrap.Client.DataAccess;
using Longbow.Web.Mvc;
using System.Linq;
using System;
using Bootstrap.Client.DataAccess.Helper;

namespace Bootstrap.Client.Query
{

    /// <summary>
    /// 
    /// </summary>
    public class QueryReportShipmentOption : CustomPaginationOption
    {
        /// <summary>
        /// 貨主
        /// </summary>
        public string StorerKey { get; set; }
        /// <summary>
        /// 路線編號
        /// </summary>
        public string RouteNo { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public DateTime? DoRouteDate_Start { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public DateTime? DoRouteDate_End { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public DateTime? DeliveryDate_Start { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public DateTime? DeliveryDate_End { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public QueryData<object> RetrieveData(string facility)
        {
            var storers = string.IsNullOrEmpty(StorerKey) ? "" : string.Join(",", StorerKey.Split(",").Select(p => string.Format("'{0}'", p)).ToArray());
            var routeno = string.IsNullOrEmpty(RouteNo) ? "" : RouteNo;
            var carleavedates = DoRouteDate_Start.HasValue ? DataComparison.DateTimeConvert(DoRouteDate_Start) : "";
            var carleavedatee = DoRouteDate_End.HasValue ? DataComparison.DateTimeConvert(DoRouteDate_End) : "";
            var deliverydates = DeliveryDate_Start.HasValue ? DataComparison.DateTimeConvert(DeliveryDate_Start) : "";
            var deliverydatee = DeliveryDate_End.HasValue ? DataComparison.DateTimeConvert(DeliveryDate_End) : "";

            var data = ReportShipmentHelper.Retrieves(storers, routeno, carleavedates, carleavedatee, deliverydates, deliverydatee, facility);
            
            var ret = new QueryData<object>();
            ret.total = data.Count();

            switch (Sort)
            {
                case "RouteNo":
                    data = Order == "asc" ? data.OrderBy(t => t.RouteNo) : data.OrderByDescending(t => t.RouteNo);
                    break;
                case "DoRouteDate":
                    data = Order == "asc" ? data.OrderBy(t => t.DoRouteDate) : data.OrderByDescending(t => t.DoRouteDate);
                    break;
                case "DeliveryDate":
                    data = Order == "asc" ? data.OrderBy(t => t.DeliveryDate) : data.OrderByDescending(t => t.DeliveryDate);
                    break;
                case "Driver":
                    data = Order == "asc" ? data.OrderBy(t => t.Driver) : data.OrderByDescending(t => t.Driver);
                    break;
                case "VehicleKey":
                    data = Order == "asc" ? data.OrderBy(t => t.VehicleKey) : data.OrderByDescending(t => t.VehicleKey);
                    break;
                case "ShipCaseQty":
                    data = Order == "asc" ? data.OrderBy(t => t.ShipCaseQty) : data.OrderByDescending(t => t.ShipCaseQty);
                    break; 
                case "ShipQty":
                    data = Order == "asc" ? data.OrderBy(t => t.ShipQty) : data.OrderByDescending(t => t.ShipQty);
                    break;       
                case "OrderQty":
                    data = Order == "asc" ? data.OrderBy(t => t.OrderQty) : data.OrderByDescending(t => t.OrderQty);
                    break;   
                case "Cube":
                    data = Order == "asc" ? data.OrderBy(t => t.Cube) : data.OrderByDescending(t => t.Cube);
                    break;   
                case "Weight":
                    data = Order == "asc" ? data.OrderBy(t => t.Weight) : data.OrderByDescending(t => t.Weight);
                    break;                                                                                                                                                 
            }     
            if (Limit != 0) data = data.Skip(Offset).Take(Limit);
            //重新查詢欄位資料
            ret.rows = data.Select(u => new
            {
                u.RouteNo,
                u.DoRouteDate,
                u.DeliveryDate,            
                u.Driver,  
                u.VehicleKey,
                u.ShipQty,                
                u.ShipCaseQty,             
                u.OrderQty,
                u.Cube,
                u.Weight         
            });  
            return ret;
        }
    }
}
