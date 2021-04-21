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
    public class QueryReportGoodDistributionOption : CustomPaginationOption
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
        /// 區域
        /// </summary>
        public string AreaDescription { get; set; }
        /// <summary>
        /// 車號
        /// </summary>
        public string VehicleKey { get; set; }
        /// <summary>
        /// 路線編號
        /// </summary>
        public string RouteNo { get; set; }
        /// <summary>
        /// 品號
        /// </summary>
        public string Sku { get; set; }
        /// <summary>
        /// 品名
        /// </summary>
        public string Descr { get; set; }
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
        /// 貨主
        /// </summary>
        public string StorerKey { get; set; }
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
        /// 查詢欄位
        /// </summary>
        private IEnumerable<ReportGoodDistribution> InternalQueryData(IEnumerable<ReportGoodDistribution> data, out int dataCount)
        {
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
                case "VehicleKey":
                    data = Order == "asc" ? data.OrderBy(t => t.VehicleKey) : data.OrderByDescending(t => t.VehicleKey);
                    break;   
                case "RouteNo":
                    data = Order == "asc" ? data.OrderBy(t => t.RouteNo) : data.OrderByDescending(t => t.RouteNo);
                    break;
                case "Sku":
                    data = Order == "asc" ? data.OrderBy(t => t.Sku) : data.OrderByDescending(t => t.Sku);
                    break;
                case "Descr":
                    data = Order == "asc" ? data.OrderBy(t => t.Descr) : data.OrderByDescending(t => t.Descr);
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
                case "ExpectDate":
                    data = Order == "asc" ? data.OrderBy(t => t.ExpectDate) : data.OrderByDescending(t => t.ExpectDate);
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
            var data = InternalQueryData(ReportGoodDistributionHelper.Retrieves( deliverydates, deliverydatee, doroutedates, doroutedatee), out dataCount);
            var ret = new QueryData<object>();
            ret.total = dataCount;
           
            //重新查詢欄位資料
            ret.rows = data.Select(u => new
            {
                u.DeliveryDate,
                u.AreaCode,
                u.AreaDescription,
                u.VehicleKey,
                u.RouteNo,
                u.Sku,
                u.Descr,
                u.ShipCaseQty,
                u.ShipQty,
                u.ShipPalletQty,
                u.ShipWeight, 
                u.ShipCube,
                u.ExpectDate, 
                u.DoRouteDate                 
            });  
            return ret;
        }

        /// <summary>
        /// 
        /// </summary>  
        public IEnumerable<ReportGoodDistribution> RetrievesExcel(string facility)
        {
            var dataCount = 0;
           // var storers = string.IsNullOrEmpty(StorerKey) ? "" : string.Join(",", StorerKey.Split(",").Select(p => string.Format("'{0}'", p)).ToArray());
           // var warehouse = string.IsNullOrEmpty(WareHouse) ? "" : string.Join(",", WareHouse.Split(",").Select(p => string.Format("'{0}'", p)).ToArray());
            var doroutedates = DoRouteDateS.HasValue ? DataComparison.DateTimeConvert(DoRouteDateS) : "";
            var doroutedatee = DoRouteDateE.HasValue ? DataComparison.DateTimeConvert(DoRouteDateE) : "";
            var deliverydates = DeliveryDateS.HasValue ? DataComparison.DateTimeConvert(DeliveryDateS) : "";
            var deliverydatee = DeliveryDateE.HasValue ? DataComparison.DateTimeConvert(DeliveryDateE) : "";
            var data = InternalQueryData(ReportGoodDistributionHelper.Retrieves( deliverydates, deliverydatee, doroutedates, doroutedatee), out dataCount);
            return data;
        }
    }
}
