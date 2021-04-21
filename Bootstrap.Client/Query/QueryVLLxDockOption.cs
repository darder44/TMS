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
    public class QueryVLLxDockOption : CustomPaginationOption
    {
        /// <summary>
        /// 倉庫
        /// </summary>
        public string Facility { get; set; }
        /// <summary>
        /// 路線編號
        /// </summary>
        public string RouteNo { get; set; }
        /// <summary>
        /// 碼頭
        /// </summary>
        public string Dock { get; set; }
        /// <summary>
        /// 碼頭描述
        /// </summary>
        public string Notes { get; set; }
        /// <summary>
        /// 司機
        /// </summary>
        public string Driver { get; set; }
        /// <summary>
        /// 車號
        /// </summary>
        public string VehicleKey { get; set; }
        /// <summary>
        /// 箱數
        /// </summary>
        public string CaseQty { get; set; }
        /// <summary>
        /// 材積
        /// </summary>
        public string Cube { get; set; }
        /// <summary>
        /// 重量
        /// </summary>
        public string Weight { get; set; }
        /// <summary>
        /// 板數
        /// </summary>
        public string PalletQty { get; set; }
        /// <summary>
        /// 報到日期
        /// </summary>
        public string ExpectDate { get; set; }
        /// <summary>
        /// 報到時間
        /// </summary>
        public string ExpectTime { get; set; }
        /// <summary>
        /// 揀貨單號
        /// </summary>
        public string Stop { get; set; }
        /// <summary>
        /// 客戶名稱
        /// </summary>
        public string C_Company { get; set; }
        /// <summary>
        /// 報到日期(起)
        /// </summary>
        public DateTime? ExpectDate_Start { get; set; }
        /// <summary>
        /// 報到日期(迄)
        /// </summary>
        public DateTime? ExpectDate_End { get; set; }


        
        /// <summary>
        /// 查詢欄位
        /// </summary>
        private IEnumerable<VLLxDock> InternalQueryData(IEnumerable<VLLxDock> data, out int dataCount)
        {
            if (!string.IsNullOrEmpty(RouteNo))
            {
                data = data.Where(t => t.RouteNo.Contains(RouteNo.Trim().ToUpper()));
            }
            if (!string.IsNullOrEmpty(Dock))
            {
                data = data.Where(t => t.Dock.Contains(Dock.Trim().ToUpper()));
            }
            if (!string.IsNullOrEmpty(Driver))
            {
                data = data.Where(t => t.Driver.Contains(Driver.Trim().ToUpper()));
            }
            if (!string.IsNullOrEmpty(VehicleKey))
            {
                data = data.Where(t => t.VehicleKey.Contains(VehicleKey.Trim().ToUpper()));
            }
            if (!string.IsNullOrEmpty(Stop))
            {
                data = data.Where(t => t.Stop.Contains(Stop.Trim().ToUpper()));
            }
            //報到日期
            if (ExpectDate_Start != null)
            {
                if (ExpectDate_End != null)
                {
                    data = data.Where(t => t.ExpectDate >= ExpectDate_Start);
                } 
                else
                {
                    data = data.Where(t => t.ExpectDate == ExpectDate_Start);
                }
            }
            if (ExpectDate_End != null)
            {
                if (ExpectDate_Start != null)
                {
                    data = data.Where(t => t.ExpectDate <= ExpectDate_End);
                }
                else
                {
                    data = data.Where(t => t.ExpectDate == ExpectDate_End);
                }
            }


            dataCount = data.Count();
            switch (Sort)
            {
                case "RouteNo":
                    data = Order == "asc" ? data.OrderBy(t => t.RouteNo) : data.OrderByDescending(t => t.RouteNo);
                    break;
                case "Dock":
                    data = Order == "asc" ? data.OrderBy(t => t.Dock) : data.OrderByDescending(t => t.RouteNo);
                    break;
                case "Notes":
                    data = Order == "asc" ? data.OrderBy(t => t.Notes) : data.OrderByDescending(t => t.Notes);
                    break;
                case "Driver":
                    data = Order == "asc" ? data.OrderBy(t => t.Driver) : data.OrderByDescending(t => t.Driver);
                    break;
                case "VehicleKey":
                    data = Order == "asc" ? data.OrderBy(t => t.VehicleKey) : data.OrderByDescending(t => t.VehicleKey);
                    break;
                case "CaseQty":
                    data = Order == "asc" ? data.OrderBy(t => t.CaseQty) : data.OrderByDescending(t => t.CaseQty);
                    break;
                case "Cube":
                    data = Order == "asc" ? data.OrderBy(t => t.Cube) : data.OrderByDescending(t => t.Cube);
                    break;
                case "Weight":
                    data = Order == "asc" ? data.OrderBy(t => t.Weight) : data.OrderByDescending(t => t.Weight);
                    break;
                case "PalletQty":
                    data = Order == "asc" ? data.OrderBy(t => t.PalletQty) : data.OrderByDescending(t => t.PalletQty);
                    break;
                case "ExpectTime":
                    data = Order == "asc" ? data.OrderBy(t => t.ExpectTime) : data.OrderByDescending(t => t.ExpectTime);
                    break;
                case "ExpectDate":
                    data = Order == "asc" ? data.OrderBy(t => t.ExpectDate) : data.OrderByDescending(t => t.ExpectDate);
                    break;    
                case "Stop":
                    data = Order == "asc" ? data.OrderBy(t => t.Stop) : data.OrderByDescending(t => t.Stop);
                    break;
                case "C_Company":
                    data = Order == "asc" ? data.OrderBy(t => t.C_Company) : data.OrderByDescending(t => t.C_Company);
                    break;

            }     
            if (Limit != 0) data = data.Skip(Offset).Take(Limit);
            return data;
        }

        /// <summary>
        /// 
        /// </summary>
        public QueryData<object> RetrieveData(string UserFacility)
        {
            var dataCount = 0;
            var data = InternalQueryData(VLLxDockHelper.Retrieves(string.IsNullOrEmpty(UserFacility) ? Facility : UserFacility), out dataCount);         
            var ret = new QueryData<object>();
            ret.total = dataCount;
           
            //重新查詢欄位資料
            ret.rows = data.Select(u => new
            {
                u.RouteNo,
                u.Dock,
                u.Notes,
                u.Driver,                
                u.VehicleKey,
                u.CaseQty,
                u.Cube,
                u.Weight,
                u.PalletQty,
                u.ExpectDate,
                u.ExpectTime,
                u.Stop,
                u.C_Company,
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
        public IEnumerable<VLLxDock> RetrievesExcel(string UserFacility)
        {
            var dataCount = 0;
            var data = InternalQueryData(VLLxDockHelper.Retrieves(string.IsNullOrEmpty(UserFacility) ? Facility : UserFacility), out dataCount);
            
            return data;
        }
    }
}
