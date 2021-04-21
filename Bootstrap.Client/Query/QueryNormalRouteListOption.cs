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
    public class QueryNormalRouteListOption : CustomPaginationOption
    {
        /// <summary>
        /// 路線編號
        /// </summary>
        public string RouteNo { get; set; }
        /// <summary>
        /// 出車日期(起)
        /// </summary>
        public DateTime? DoRouteDate_Start { get; set; }
        /// <summary>
        /// 出車日期(起)
        /// </summary>
        public DateTime? DoRouteDate_End { get; set; }
        /// <summary>
        /// 回傳狀態
        /// </summary>
        public string ToWMS { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string VehicleKey { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public QueryData<object> RetrieveData(string facility)
        {
            var data = NormalRouteListHelper.Retrieves(facility);
            
            if (!string.IsNullOrEmpty(RouteNo))
            {
                data = data.Where(t => t.RouteNo.Trim().ToUpper() == RouteNo.Trim().ToUpper());
            }
            if (DoRouteDate_Start != null)
            {
                if (DoRouteDate_End != null)
                {
                    data = data.Where(t => t.DoRouteDate >= DoRouteDate_Start);
                }
                else
                {
                    data = data.Where(t => t.DoRouteDate == DoRouteDate_Start);
                }
            }
            if (DoRouteDate_End != null)
            {
                if (DoRouteDate_Start != null)
                {
                    data = data.Where(t => t.DoRouteDate <= DoRouteDate_End);
                }
                else
                {
                    data = data.Where(t => t.DoRouteDate == DoRouteDate_End);
                }
            }
            if (!string.IsNullOrEmpty(ToWMS))
            {
                data = data.Where(t => t.ToWMS.Trim().ToUpper() == ToWMS.Trim().ToUpper());
            }
            if (!string.IsNullOrEmpty(VehicleKey))
            {
                data = data.Where(t => t.VehicleKey.Contains(VehicleKey));
            }
            
            var ret = new QueryData<object>();
            var users = UserHelper.Retrieves();
            var newrows = data.GroupJoin(users, d => string.IsNullOrEmpty(d.AddWho) ? "" : d.AddWho.ToUpper().Trim(), u => u.UserName.ToUpper().Trim(), (d, u) => new
            {
                data = d,
                user = u
            }).SelectMany(d => d.user.DefaultIfEmpty(), (d, u) => new
            {
                d.data.RouteNo,
                d.data.FacilityName,
                d.data.DoRouteDate,
                d.data.ExpectDate,
                d.data.ExpectTime,
                d.data.CaseQty,
                d.data.PalletQty,
                d.data.Cube,
                d.data.Weight,
                d.data.DockNo,
                d.data.VehicleKey,
                d.data.Driver,
                d.data.DriverPhone,
                d.data.Notes,
                d.data.DriveTimes,
                d.data.VLListCount,
                d.data.VLListPrintDate,
                d.data.AddDate,
                d.data.AddWho,
                d.data.EditDate,
                d.data.EditWho,
                d.data.ToWMS,
                d.data.OrderType,
                DisplayName = (u != null) ? u.DisplayName : ""
            });
            ret.total = newrows.Count();
            switch (Sort)
            {
                case "RouteNo":
                    newrows = Order == "asc" ? newrows.OrderBy(t => t.RouteNo) : newrows.OrderByDescending(t => t.RouteNo);
                break;
                case "FacilityName":
                    newrows = Order == "asc" ? newrows.OrderBy(t => t.FacilityName) : newrows.OrderByDescending(t => t.FacilityName);
                break;
                case "DoRouteDate":
                    newrows = Order == "asc" ? newrows.OrderBy(t => t.DoRouteDate) : newrows.OrderByDescending(t => t.DoRouteDate);
                break;
                case "ExpectDate":
                    newrows = Order == "asc" ? newrows.OrderBy(t => t.ExpectDate) : newrows.OrderByDescending(t => t.ExpectDate);
                break;
                case "ExpectTime":
                    newrows = Order == "asc" ? newrows.OrderBy(t => t.ExpectTime) : newrows.OrderByDescending(t => t.ExpectTime);
                break;
                case "CaseQty":
                    newrows = Order == "asc" ? newrows.OrderBy(t => t.CaseQty) : newrows.OrderByDescending(t => t.CaseQty);
                break;
                case "PalletQty":
                    newrows = Order == "asc" ? newrows.OrderBy(t => t.PalletQty) : newrows.OrderByDescending(t => t.PalletQty);
                break;
                case "Cube":
                    newrows = Order == "asc" ? newrows.OrderBy(t => t.Cube) : newrows.OrderByDescending(t => t.Cube);
                break;
                case "Weight":
                    newrows = Order == "asc" ? newrows.OrderBy(t => t.Weight) : newrows.OrderByDescending(t => t.Weight);
                break;
                case "DockNo":
                    newrows = Order == "asc" ? newrows.OrderBy(t => t.DockNo) : newrows.OrderByDescending(t => t.DockNo);
                break;
                case "VehicleKey":
                    newrows = Order == "asc" ? newrows.OrderBy(t => t.VehicleKey) : newrows.OrderByDescending(t => t.VehicleKey);
                break;
                case "Driver":
                    newrows = Order == "asc" ? newrows.OrderBy(t => t.Driver) : newrows.OrderByDescending(t => t.Driver);
                break;
                case "DriverPhone":
                    newrows = Order == "asc" ? newrows.OrderBy(t => t.DriverPhone) : newrows.OrderByDescending(t => t.DriverPhone);
                break;
                case "Notes":
                    newrows = Order == "asc" ? newrows.OrderBy(t => t.Notes) : newrows.OrderByDescending(t => t.Notes);
                break;
                case "DriveTimes":
                    newrows = Order == "asc" ? newrows.OrderBy(t => t.DriveTimes) : newrows.OrderByDescending(t => t.DriveTimes);
                break;
                case "VLListCount":
                    newrows = Order == "asc" ? newrows.OrderBy(t => t.VLListCount) : newrows.OrderByDescending(t => t.VLListCount);
                break;
                case "VLListPrintDate":
                    newrows = Order == "asc" ? newrows.OrderBy(t => t.VLListPrintDate) : newrows.OrderByDescending(t => t.VLListPrintDate);
                break;
                case "AddDate":
                    newrows = Order == "asc" ? newrows.OrderBy(t => t.AddDate) : newrows.OrderByDescending(t => t.AddDate);
                break;
                case "AddWho":
                    newrows = Order == "asc" ? newrows.OrderBy(t => t.AddWho) : newrows.OrderByDescending(t => t.AddWho);
                break;
                case "EditDate":
                    newrows = Order == "asc" ? newrows.OrderBy(t => t.EditDate) : newrows.OrderByDescending(t => t.EditDate);
                break;
                case "EditWho":
                    newrows = Order == "asc" ? newrows.OrderBy(t => t.EditWho) : newrows.OrderByDescending(t => t.EditWho);
                break;
                case "ToWMS":
                    newrows = Order == "asc" ? newrows.OrderBy(t => t.ToWMS) : newrows.OrderByDescending(t => t.ToWMS);
                break;
            }            
            if (Limit != 0) newrows = newrows.Skip(Offset).Take(Limit);
            
            ret.rows = newrows.Select(u => new
            {
                u.RouteNo,
                u.FacilityName,
                u.DoRouteDate,
                u.ExpectDate,
                u.ExpectTime,
                u.CaseQty,
                u.PalletQty,
                u.Cube,
                u.Weight,
                u.DockNo,
                u.VehicleKey,
                u.Driver,
                u.DriverPhone,
                u.Notes,
                u.DriveTimes,
                u.VLListCount,
                u.VLListPrintDate,
                u.AddDate,
                u.AddWho,
                u.EditDate,
                u.EditWho,
                u.ToWMS,
                u.OrderType,
                u.DisplayName
            });
            return ret;
        }
    }
}
