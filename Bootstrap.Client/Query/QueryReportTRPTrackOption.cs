using System.Collections.Generic;
using Longbow.Web.Mvc;
using System.Linq;
using System;
using Bootstrap.Client.DataAccess.Helper;
using Bootstrap.Client.DataAccess;

namespace Bootstrap.Client.Query
{
    /// <summary>
    /// 
    /// </summary>
    public class QueryReportTRPTrackOption : CustomPaginationOption
    {
        /// <summary>
        /// StorerKeys
        /// </summary>
        public string StorerKeys { get; set; }
        /// <summary>
        /// 訂單類別
        /// </summary>
        public string OrderTypes { get; set; }
        /// <summary>
        /// 訂單狀態
        /// </summary>
        public string OrderStatus { get; set; }
        /// <summary>
        /// 區碼
        /// </summary>
        public string AreaCodes { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string RouteNo { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string WaveKey { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string TMSKey { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string ExternOrderKey { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string ConsigneeKey { get; set; }
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
        /// 查詢欄位
        /// </summary>
        private IEnumerable<ReportTRPTrack> InternalQueryData(IEnumerable<ReportTRPTrack> data, out int dataCount)
        {
            dataCount = data.Count();
            switch (Sort)
            {
                case "ConsigneeKey":
                    data = Order == "asc" ? data.OrderBy(t => t.ConsigneeKey) : data.OrderByDescending(t => t.ConsigneeKey);
                break;
                case "OrderType":
                    data = Order == "asc" ? data.OrderBy(t => t.OrderType) : data.OrderByDescending(t => t.OrderType);
                break;
                case "OrderStatus":
                    data = Order == "asc" ? data.OrderBy(t => t.OrderStatus) : data.OrderByDescending(t => t.OrderStatus);
                break;
                case "AreaCode":
                    data = Order == "asc" ? data.OrderBy(t => t.AreaCode) : data.OrderByDescending(t => t.AreaCode);
                break;
                case "VehicleKey":
                    data = Order == "asc" ? data.OrderBy(t => t.VehicleKey) : data.OrderByDescending(t => t.VehicleKey);
                break;
                case "Driver":
                    data = Order == "asc" ? data.OrderBy(t => t.Driver) : data.OrderByDescending(t => t.Driver);
                break;
                case "RouteNo":
                    data = Order == "asc" ? data.OrderBy(t => t.RouteNo) : data.OrderByDescending(t => t.RouteNo);
                break;
                case "OrderDate":
                    data = Order == "asc" ? data.OrderBy(t => t.OrderDate) : data.OrderByDescending(t => t.OrderDate);
                break;
                case "DeliveryDate":
                    data = Order == "asc" ? data.OrderBy(t => t.DeliveryDate) : data.OrderByDescending(t => t.DeliveryDate);
                break;
                case "DoRouteDate":
                    data = Order == "asc" ? data.OrderBy(t => t.DeliveryDate) : data.OrderByDescending(t => t.DeliveryDate);
                break;
                case "SignDate":
                    data = Order == "asc" ? data.OrderBy(t => t.SignDate) : data.OrderByDescending(t => t.SignDate);
                break;
                case "StorerKey":
                    data = Order == "asc" ? data.OrderBy(t => t.StorerKey) : data.OrderByDescending(t => t.StorerKey);
                break;
                case "TMSKey":
                    data = Order == "asc" ? data.OrderBy(t => t.TMSKey) : data.OrderByDescending(t => t.TMSKey);
                break;
                case "ExternOrderKey":
                    data = Order == "asc" ? data.OrderBy(t => t.ExternOrderKey) : data.OrderByDescending(t => t.ExternOrderKey);
                break;
                case "ShortName":
                    data = Order == "asc" ? data.OrderBy(t => t.ShortName) : data.OrderByDescending(t => t.ShortName);
                break;
                case "SoldTo":
                    data = Order == "asc" ? data.OrderBy(t => t.SoldTo) : data.OrderByDescending(t => t.SoldTo);
                break;
                case "SoldToName":
                    data = Order == "asc" ? data.OrderBy(t => t.SoldToName) : data.OrderByDescending(t => t.SoldToName);
                break;
                case "ShipTo":
                    data = Order == "asc" ? data.OrderBy(t => t.ShipTo) : data.OrderByDescending(t => t.ShipTo);
                break;
                case "ShipToName":
                    data = Order == "asc" ? data.OrderBy(t => t.ShipToName) : data.OrderByDescending(t => t.ShipToName);
                break;
                case "StartAddress":
                    data = Order == "asc" ? data.OrderBy(t => t.StartAddress) : data.OrderByDescending(t => t.StartAddress);
                break;
                case "EndAddress":
                    data = Order == "asc" ? data.OrderBy(t => t.EndAddress) : data.OrderByDescending(t => t.EndAddress);
                break;
                case "City":
                    data = Order == "asc" ? data.OrderBy(t => t.City) : data.OrderByDescending(t => t.City);
                break;
                case "PalletQty":
                    data = Order == "asc" ? data.OrderBy(t => t.PalletQty) : data.OrderByDescending(t => t.PalletQty);
                break;
                case "CaseQty":
                    data = Order == "asc" ? data.OrderBy(t => t.CaseQty) : data.OrderByDescending(t => t.CaseQty);
                break;
                case "OrderQty":
                    data = Order == "asc" ? data.OrderBy(t => t.OrderQty) : data.OrderByDescending(t => t.OrderQty);
                break;
                case "TotalQty":
                    data = Order == "asc" ? data.OrderBy(t => t.TotalQty) : data.OrderByDescending(t => t.TotalQty);
                break;
                case "ShipQty":
                    data = Order == "asc" ? data.OrderBy(t => t.ShipQty) : data.OrderByDescending(t => t.ShipQty);
                break;
                case "ShipCaseQty":
                    data = Order == "asc" ? data.OrderBy(t => t.ShipCaseQty) : data.OrderByDescending(t => t.ShipCaseQty);
                break;
                case "SignQty":
                    data = Order == "asc" ? data.OrderBy(t => t.SignQty) : data.OrderByDescending(t => t.SignQty);
                break;
                case "Cube":
                    data = Order == "asc" ? data.OrderBy(t => t.Cube) : data.OrderByDescending(t => t.Cube);
                break;
                case "Weight":
                    data = Order == "asc" ? data.OrderBy(t => t.Weight) : data.OrderByDescending(t => t.Weight);
                break;
                case "Notes":
                    data = Order == "asc" ? data.OrderBy(t => t.Notes) : data.OrderByDescending(t => t.Notes);
                break;
                case "OnTime":
                    data = Order == "asc" ? data.OrderBy(t => t.OnTime) : data.OrderByDescending(t => t.OnTime);
                break;
                case "Delay":
                    data = Order == "asc" ? data.OrderBy(t => t.Delay) : data.OrderByDescending(t => t.Delay);
                break;
                case "AddDate":
                    data = Order == "asc" ? data.OrderBy(t => t.AddDate) : data.OrderByDescending(t => t.AddDate);
                break;
                case "WaveKey":
                    data = Order == "asc" ? data.OrderBy(t => t.WaveKey) : data.OrderByDescending(t => t.WaveKey);
                break;
            }
            if (Limit != 0) data = data.Skip(Offset).Take(Limit);
            return data;
        }

        /// <summary>
        /// 
        /// </summary>
        public QueryData<object> RetrieveData()
        {            
            var storers = string.IsNullOrEmpty(StorerKeys) ? "" : string.Join(",", StorerKeys.Split(",").Select(p => string.Format("'{0}'", p)).ToArray());
            var ordertypes = string.IsNullOrEmpty(OrderTypes) ? "" : string.Join(",", OrderTypes.Split(",").Select(p => string.Format("'{0}'", p)).ToArray());
            var orderstatus = string.IsNullOrEmpty(OrderStatus) ? "" : string.Join(",", OrderStatus.Split(",").Select(p => string.Format("'{0}'", p)).ToArray());
            var consigneeKey = string.IsNullOrEmpty(ConsigneeKey) ? "" : ConsigneeKey;
            var wavekey = string.IsNullOrEmpty(WaveKey) ? "" : WaveKey;
            var tmskey = string.IsNullOrEmpty(TMSKey) ? "" : TMSKey;
            var externorderkey = string.IsNullOrEmpty(ExternOrderKey) ? "" : ExternOrderKey;
            var areacodes = string.IsNullOrEmpty(AreaCodes) ? "" : string.Join(",", AreaCodes.Split(",").Select(p => string.Format("'{0}'", p)).ToArray());
            var routeno = string.IsNullOrEmpty(RouteNo) ? "" : RouteNo;
            var carleavedates = DoRouteDate_Start.HasValue ? DataComparison.DateTimeConvert(DoRouteDate_Start) : "";
            var carleavedatee = DoRouteDate_End.HasValue ? DataComparison.DateTimeConvert(DoRouteDate_End) : "";
            var deliverydates = DeliveryDate_Start.HasValue ? DataComparison.DateTimeConvert(DeliveryDate_Start) : "";
            var deliverydatee = DeliveryDate_End.HasValue ? DataComparison.DateTimeConvert(DeliveryDate_End) : "";

            var dataCount = 0;
            var data = InternalQueryData(ReportTRPTrackHelper.Retrieves("Detail", storers, ordertypes, orderstatus, consigneeKey, wavekey, tmskey, externorderkey, areacodes, routeno, carleavedates, carleavedatee, deliverydates, deliverydatee), out dataCount);
              
            var ret = new QueryData<object>();
            ret.total = dataCount;

            ret.rows = data.Select(u => new
            {
                u.ConsigneeKey,
                u.OrderType,
                u.OrderStatus,
                u.AreaCode,
                u.VehicleKey,
                u.Driver,
                u.RouteNo,
                u.OrderDate,
                u.DeliveryDate,
                u.DoRouteDate,
                u.SignDate,
                u.StorerKey,
                u.WaveKey,
                u.TMSKey,
                u.ExternOrderKey,
                u.ShortName,
                u.StartAddress,
                u.EndAddress,
                u.City,
                u.PalletQty,
                u.CaseQty,
                u.OrderQty,
                u.TotalQty,
                u.ShipQty,
                u.ShipCaseQty,
                u.TotalShipQty,
                u.SignQty,
                u.Cube,
                u.Weight,
                u.Notes,
                u.OnTime,
                u.Delay,
                u.AddDate,
                u.ShipTo,
                u.SoldTo,
                u.ShipToName,
                u.SoldToName
            });
            return ret;
        }

        /// <summary>
        /// 
        /// </summary>  
        public IEnumerable<ReportTRPTrack> RetrievesExcel(bool IsDetail)
        {
            var storers = string.IsNullOrEmpty(StorerKeys) ? "" : string.Join(",", StorerKeys.Split(",").Select(p => string.Format("'{0}'", p)).ToArray());
            var ordertypes = string.IsNullOrEmpty(OrderTypes) ? "" : string.Join(",", OrderTypes.Split(",").Select(p => string.Format("'{0}'", p)).ToArray());
            var orderstatus = string.IsNullOrEmpty(OrderStatus) ? "" : string.Join(",", OrderStatus.Split(",").Select(p => string.Format("'{0}'", p)).ToArray());
            var consigneeKey = string.IsNullOrEmpty(ConsigneeKey) ? "" : ConsigneeKey;
            var wavekey = string.IsNullOrEmpty(WaveKey) ? "" : WaveKey;
            var tmskey = string.IsNullOrEmpty(TMSKey) ? "" : TMSKey;
            var externorderkey = string.IsNullOrEmpty(ExternOrderKey) ? "" : ExternOrderKey;
            var areacodes = string.IsNullOrEmpty(AreaCodes) ? "" : string.Join(",", AreaCodes.Split(",").Select(p => string.Format("'{0}'", p)).ToArray());
            var routeno = string.IsNullOrEmpty(RouteNo) ? "" : RouteNo;
            var carleavedates = DoRouteDate_Start.HasValue ? DataComparison.DateTimeConvert(DoRouteDate_Start) : "";
            var carleavedatee = DoRouteDate_End.HasValue ? DataComparison.DateTimeConvert(DoRouteDate_End) : "";
            var deliverydates = DeliveryDate_Start.HasValue ? DataComparison.DateTimeConvert(DeliveryDate_Start) : "";
            var deliverydatee = DeliveryDate_End.HasValue ? DataComparison.DateTimeConvert(DeliveryDate_End) : "";
            var SheetName = IsDetail ? "Detail" : "Header";
            
            var dataCount = 0;
            var data = InternalQueryData(ReportTRPTrackHelper.Retrieves(SheetName, storers, ordertypes, orderstatus, consigneeKey, wavekey, tmskey, externorderkey, areacodes, routeno, carleavedates, carleavedatee, deliverydates, deliverydatee), out dataCount);
            
            return data;
        }
    }
}
