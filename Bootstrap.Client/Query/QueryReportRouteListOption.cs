using System.Collections.Generic;
using Longbow.Web.Mvc;
using System.Linq;
using System;
using Bootstrap.Client.DataAccess.Helper;
using Bootstrap.Client.DataAccess;
using System.Text;
using System.Text.RegularExpressions;

namespace Bootstrap.Client.Query
{

    /// <summary>
    /// 
    /// </summary>
    public class QueryReportRouteListOption : CustomPaginationOption
    {
        /// <summary>
        /// 到貨日期
        /// </summary>
        public DateTime? DeliveryDate { get; set; }
        /// <summary>
        /// 區碼
        /// </summary>
        public string AreaCode { get; set; }
        /// <summary>
        /// 運送區域
        /// </summary>
        public string AreaDescription { get; set; }
        /// <summary>
        /// 暫存區
        /// </summary>
        public string DockNo { get; set; }
        /// <summary>
        /// 縣市別
        /// </summary>
        public string City { get; set; }
        /// <summary>
        /// 貨運公司
        /// </summary>
        public string CompanyName { get; set; }
        /// <summary>
        /// 車號
        /// </summary>
        public string VehicleKey { get; set; }
        /// <summary>
        /// 車次
        /// </summary>
        public string DriveTimes { get; set; }
        /// <summary>
        /// 一單多車
        /// </summary>
        public string MultiVehicle { get; set; }
        /// <summary>
        /// 司機
        /// </summary>
        public string Driver { get; set; }
        /// <summary>
        /// 可載重量
        /// </summary>
        public string LoadingWeight { get; set; }
        /// <summary>
        /// 可載材積
        /// </summary>
        public string LoadingCube { get; set; }
        /// <summary>
        /// 路線編號
        /// </summary>
        public string RouteNo { get; set; }
        /// <summary>
        /// 運送點數
        /// </summary>
        public string CountOrders { get; set; }
        /// <summary>
        /// 運送箱數
        /// </summary>
        public string ShipCaseQty { get; set; }
        /// <summary>
        /// 運送個數
        /// </summary>
        public string ShipQty { get; set; }
        /// <summary>
        /// 運送板數
        /// </summary>
        public string ShipPalletQty { get; set; }
        /// <summary>
        /// 運送重量
        /// </summary>
        public string ShipWeight { get; set; }
        /// <summary>
        /// 運送材積
        /// </summary>
        public string ShipCube { get; set; }
        /// <summary>
        /// 貨運公司代碼
        /// </summary>
        public string CompanyKey { get; set; }
        /// <summary>
        /// 備註
        /// </summary>
        public string Notes { get; set; }
        /// <summary>
        /// 預計報到時間
        /// </summary>
        public DateTime? ExpectDate { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string StorerKeys { get; set; }
        /// <summary>
        /// 到貨時間(起)
        /// </summary>
        public DateTime? DeliveryDateS { get; set; }
        /// <summary>
        /// 到貨時間(訖)
        /// </summary>
        public DateTime? DeliveryDateE { get; set; }
        /// <summary>
        /// 出車時間(起)
        /// </summary>
        public DateTime? DoRouteDateS { get; set; }
        /// <summary>
        /// 出車時間(迄)
        /// </summary>
        public DateTime? DoRouteDateE { get; set; }
        /// <summary>
        /// 品號
        /// </summary>
        public string Sku { get; set; }
         /// <summary>
        /// 品名
        /// </summary>
        public string Descr { get; set; }
        
        /// <summary>
        /// 查詢欄位
        /// </summary>
        private IEnumerable<ReportRouteList> InternalQueryData(IEnumerable<ReportRouteList> data, out int dataCount)
        {
            if (!string.IsNullOrEmpty(StorerKeys))
            {
                var storerkeys = StorerKeys.Split(",");
                data = data.Where( d => storerkeys.Contains(d.StorerKey));
            }

            if (!string.IsNullOrEmpty(RouteNo))
            {
                data = data.Where(t => t.RouteNo.Contains(RouteNo));
            }

            dataCount = data.Count();

            switch (Sort)
            {
                case "DeliveryDate":
                    data = Order == "asc" ? data.OrderBy(t => t.DeliveryDate) : data.OrderByDescending(t => t.DeliveryDate);
                    break;
                case "AreaCode":
                    data = Order == "asc" ? data.OrderBy(t => t.AreaCode) : data.OrderByDescending(t => t.AreaCode);
                    break;
                case "AreaDescription":
                    data = Order == "asc" ? data.OrderBy(t => t.AreaDescription) : data.OrderByDescending(t => t.AreaDescription);
                    break;
                case "DockNo":
                    data = Order == "asc" ? data.OrderBy(t => t.DockNo) : data.OrderByDescending(t => t.DockNo);
                    break;
                case "City":
                    data = Order == "asc" ? data.OrderBy(t => t.City) : data.OrderByDescending(t => t.City);
                    break;
                case "CompanyName":
                    data = Order == "asc" ? data.OrderBy(t => t.CompanyName) : data.OrderByDescending(t => t.CompanyName);
                    break;
                case "VehicleKey":
                    data = Order == "asc" ? data.OrderBy(t => t.VehicleKey) : data.OrderByDescending(t => t.VehicleKey);
                    break;
                case "DriveTimes":
                    data = Order == "asc" ? data.OrderBy(t => t.DriveTimes) : data.OrderByDescending(t => t.DriveTimes);
                    break;
                case "MultiVehicle":
                    data = Order == "asc" ? data.OrderBy(t => t.MultiVehicle) : data.OrderByDescending(t => t.MultiVehicle);
                    break;
                case "Driver":
                    data = Order == "asc" ? data.OrderBy(t => t.Driver) : data.OrderByDescending(t => t.Driver);
                    break;
                case "LoadingWeight":
                    data = Order == "asc" ? data.OrderBy(t => t.LoadingWeight) : data.OrderByDescending(t => t.LoadingWeight);
                    break;    
                case "LoadingCube":
                    data = Order == "asc" ? data.OrderBy(t => t.LoadingCube) : data.OrderByDescending(t => t.LoadingCube);
                    break;
                case "RouteNo":
                    data = Order == "asc" ? data.OrderBy(t => t.RouteNo) : data.OrderByDescending(t => t.RouteNo);
                    break;
                case "CountOrders":
                    data = Order == "asc" ? data.OrderBy(t => t.CountOrders) : data.OrderByDescending(t => t.CountOrders);
                    break;
                case "ShipCaseQty":
                    data = Order == "asc" ? data.OrderBy(t => t.ShipCaseQty) : data.OrderByDescending(t => t.ShipCaseQty);
                    break;
                case "ShipQty":
                    data = Order == "asc" ? data.OrderBy(t => t.ShipQty) : data.OrderByDescending(t => t.ShipQty);
                    break;
                case "ShipPalletQty":
                    data = Order == "asc" ? data.OrderBy(t => t.ShipPalletQty) : data.OrderByDescending(t => t.ShipPalletQty);
                    break;
                case "ShipWeight":
                    data = Order == "asc" ? data.OrderBy(t => t.ShipWeight) : data.OrderByDescending(t => t.ShipWeight);
                    break;
                case "ShipCube":
                    data = Order == "asc" ? data.OrderBy(t => t.ShipCube) : data.OrderByDescending(t => t.ShipCube);
                    break; 
                case "CompanyKey":
                    data = Order == "asc" ? data.OrderBy(t => t.CompanyKey) : data.OrderByDescending(t => t.CompanyKey);
                    break; 
                case "Notes":
                    data = Order == "asc" ? data.OrderBy(t => t.Notes) : data.OrderByDescending(t => t.Notes);
                    break; 
                case "ExpectDate":
                    data = Order == "asc" ? data.OrderBy(t => t.ExpectDate) : data.OrderByDescending(t => t.ExpectDate);
                    break; 
                case "StorerKey":
                    data = Order == "asc" ? data.OrderBy(t => t.StorerKey) : data.OrderByDescending(t => t.StorerKey);
                    break;
                case "DoRouteDate":
                    data = Order == "asc" ? data.OrderBy(t => t.DoRouteDate) : data.OrderByDescending(t => t.DoRouteDate);
                    break;                                                                                                         
            }     
            if (Limit != 0) data = data.Skip(Offset).Take(Limit);
            return data;
        }

        /// <summary>
        /// 
        /// </summary>
        public QueryData<object> RetrieveData(string facility)
        {
            var dataCount = 0;
            //var storers = string.IsNullOrEmpty(StorerKey) ? "" : string.Join(",", StorerKey.Split(",").Select(p => string.Format("'{0}'", p)).ToArray());
            //var warehouse = string.IsNullOrEmpty(WareHouse) ? "" : string.Join(",", WareHouse.Split(",").Select(p => string.Format("'{0}'", p)).ToArray());
            var doroutedates = DoRouteDateS.HasValue ? DataComparison.DateTimeConvert(DoRouteDateS) : "";
            var doroutedatee = DoRouteDateE.HasValue ? DataComparison.DateTimeConvert(DoRouteDateE) : "";
            var deliverydates = DeliveryDateS.HasValue ? DataComparison.DateTimeConvert(DeliveryDateS) : "";
            var deliverydatee = DeliveryDateE.HasValue ? DataComparison.DateTimeConvert(DeliveryDateE) : "";      
            var data = InternalQueryData(ReportRouteListHelper.Retrieves(deliverydates, deliverydatee, doroutedates, doroutedatee), out dataCount);
            var ret = new QueryData<object>();
            ret.total = dataCount;
           
            //重新查詢欄位資料
            ret.rows = data.Select(u => new
            {
                u.DeliveryDate,
                u.AreaCode,
                u.AreaDescription,
                u.DockNo,
                u.City,
                u.CompanyName,
                u.VehicleKey,
                u.DriveTimes,
                u.MultiVehicle,
                u.Driver,
                u.LoadingWeight,
                u.LoadingCube,
                u.RouteNo,
                u.CountOrders,
                u.ShipCaseQty,
                u.ShipQty,
                u.ShipPalletQty,
                u.ShipWeight, 
                u.ShipCube,
                u.CompanyKey,
                u.Notes,
                u.StorerKey,
                u.ExpectDate,
                u.DoRouteDate                 
            });  
            return ret;
        }

        /// <summary>
        /// 
        /// </summary>  
        public IEnumerable<ReportRouteList> RetrievesExcel(string facility)
        {
            var dataCount = 0;
            //var storers = string.IsNullOrEmpty(StorerKey) ? "" : string.Join(",", StorerKey.Split(",").Select(p => string.Format("'{0}'", p)).ToArray());
            //var warehouse = string.IsNullOrEmpty(WareHouse) ? "" : string.Join(",", WareHouse.Split(",").Select(p => string.Format("'{0}'", p)).ToArray());
            var doroutedates = DoRouteDateS.HasValue ? DataComparison.DateTimeConvert(DoRouteDateS) : "";
            var doroutedatee = DoRouteDateE.HasValue ? DataComparison.DateTimeConvert(DoRouteDateE) : "";
            var deliverydates = DeliveryDateS.HasValue ? DataComparison.DateTimeConvert(DeliveryDateS) : "";
            var deliverydatee = DeliveryDateE.HasValue ? DataComparison.DateTimeConvert(DeliveryDateE) : "";
            var data = InternalQueryData(ReportRouteListHelper.Retrieves( deliverydates, deliverydatee, doroutedates, doroutedatee), out dataCount);

            return data;
        }
    }
}
