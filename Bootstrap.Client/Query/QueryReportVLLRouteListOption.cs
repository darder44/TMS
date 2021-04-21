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
    public class QueryReportVLLRouteListOption : CustomPaginationOption
    {
        /// <summary>
        /// 路線編號
        /// </summary>
        public string RouteNo { get; set; }
        /// <summary>
        /// 出車日期
        /// </summary>
        public DateTime? ExpectDate { get; set; }
        /// <summary>
        /// 列印次數
        /// </summary>
        public string VLListCount { get; set; }
        /// <summary>
        /// 列印時間
        /// </summary>
        public string VLListPrintDate { get; set; }
        /// <summary>
        /// 車牌號碼
        /// </summary>
        public string VehicleKey { get; set; }
        /// <summary>
        /// 駕駛人
        /// </summary>
        public string Driver { get; set; }
        /// <summary>
        /// 車次
        /// </summary>
        public string DriveTimes { get; set; }
        /// <summary>
        /// 運輸公司
        /// </summary>
        public string CompanyKey { get; set; }
        /// <summary>
        /// 排車個數
        /// </summary>
        public string OrderQty { get; set; }
        /// <summary>
        /// 揀貨個數
        /// </summary>
        public string ShipQty { get; set; }
        /// <summary>
        /// 排車材積
        /// </summary>
        public string Cube { get; set; }
        /// <summary>
        /// 排車重量
        /// </summary>
        public string Weight { get; set; }
        /// <summary>
        /// 路線編號(起)
        /// </summary>
        public string RouteNo_Start { get; set; }
        /// <summary>
        /// 路線編號(迄)
        /// </summary>
        public string RouteNo_End { get; set; }
        /// <summary>
        /// 出車日期(起)
        /// </summary>
        public DateTime? DoRouteDate_Start { get; set; }
        /// <summary>
        /// 出車日期(迄)
        /// </summary>
        public DateTime? DoRouteDate_End { get; set; }
        /// <summary>
        /// 出車日期(迄)
        /// </summary>
        public string RouteNoS { get; set; }
        /// <summary>
        /// WaveKey
        /// </summary>
        public string WaveKey { get; set; }
        
        /// <summary>
        /// 查詢欄位
        /// </summary>

        private IEnumerable<ReportVLLRouteList> InternalQueryData(IEnumerable<ReportVLLRouteList> data, out int dataCount)
        {
            dataCount = data.Count();

            switch (Sort)
            {
                case "RouteNo":
                    data = Order == "asc" ? data.OrderBy(t => t.RouteNo) : data.OrderByDescending(t => t.RouteNo);
                    break;
                case "WaveKey":
                    data = Order == "asc" ? data.OrderBy(t => t.WaveKey) : data.OrderByDescending(t => t.WaveKey);
                    break;
                case "ExpectDate":
                    data = Order == "asc" ? data.OrderBy(t => t.ExpectDate) : data.OrderByDescending(t => t.ExpectDate);
                    break;
                case "VLListCount":
                    data = Order == "asc" ? data.OrderBy(t => t.VLListCount) : data.OrderByDescending(t => t.VLListCount);
                    break;
                case "VLListPrintDate":
                    data = Order == "asc" ? data.OrderBy(t => t.VLListPrintDate) : data.OrderByDescending(t => t.VLListPrintDate);
                    break;
                case "VehicleKey":
                    data = Order == "asc" ? data.OrderBy(t => t.VehicleKey) : data.OrderByDescending(t => t.VehicleKey);
                    break;
                case "Driver":
                    data = Order == "asc" ? data.OrderBy(t => t.Driver) : data.OrderByDescending(t => t.Driver);
                    break;
                case "DriveTimes":
                    data = Order == "asc" ? data.OrderBy(t => t.DriveTimes) : data.OrderByDescending(t => t.DriveTimes);
                    break;
                case "CompanyKey":
                    data = Order == "asc" ? data.OrderBy(t => t.CompanyKey) : data.OrderByDescending(t => t.CompanyKey);
                    break;
                case "OrderQty":
                    data = Order == "asc" ? data.OrderBy(t => t.OrderQty) : data.OrderByDescending(t => t.OrderQty);
                    break;
                case "ShipQty":
                    data = Order == "asc" ? data.OrderBy(t => t.ShipQty) : data.OrderByDescending(t => t.ShipQty);
                    break;    
                case "Cube":
                    data = Order == "asc" ? data.OrderBy(t => t.Cube) : data.OrderByDescending(t => t.Cube);
                    break;
                case "Weight":
                    data = Order == "asc" ? data.OrderBy(t => t.Weight) : data.OrderByDescending(t => t.Weight);
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
            var wavekey = string.IsNullOrEmpty(WaveKey) ? "" : WaveKey;
            var routenostart = string.IsNullOrEmpty(RouteNo_Start) ? "" : RouteNo_Start;
            var routenoend = string.IsNullOrEmpty(RouteNo_End) ? "" : RouteNo_End;
            var doroutedatestart = DoRouteDate_Start.HasValue ? DataComparison.DateTimeConvert(DoRouteDate_Start) : "";
            var doroutedateend = DoRouteDate_End.HasValue ? DataComparison.DateTimeConvert(DoRouteDate_End) : "";

            var dataCount = 0;
            var data = InternalQueryData(ReportVLLRouteListHelper.Retrieves(facility, wavekey, routenostart, routenoend, doroutedatestart, doroutedateend), out dataCount);

            var ret = new QueryData<object>();
            ret.total = dataCount;
            //重新查詢欄位資料
            ret.rows = data.Select(u => new
            {
                u.RouteNo,
                u.ExpectDate,
                u.VLListCount,
                u.VLListPrintDate,
                u.VehicleKey,
                u.Driver,
                u.DriveTimes,
                u.CompanyKey        
            });  
            return ret;
        }

        /// <summary>
        /// 
        /// </summary>  
        public IEnumerable<ReportVLLRouteList> RetrievesExcel(string facility)
        {
            var wavekey = string.IsNullOrEmpty(WaveKey) ? "" : WaveKey;
            var routenostart = string.IsNullOrEmpty(RouteNo_Start) ? "" : RouteNo_Start;
            var routenoend = string.IsNullOrEmpty(RouteNo_End) ? "" : RouteNo_End;
            var doroutedatestart = DoRouteDate_Start.HasValue ? DataComparison.DateTimeConvert(DoRouteDate_Start) : "";
            var doroutedateend = DoRouteDate_End.HasValue ? DataComparison.DateTimeConvert(DoRouteDate_End) : "";

            var dataCount = 0;
            var data = InternalQueryData(ReportVLLRouteListHelper.RetrievesExcel(facility,wavekey, routenostart, routenoend, doroutedatestart, doroutedateend), out dataCount);
            
            return data;
        }
    }
}
