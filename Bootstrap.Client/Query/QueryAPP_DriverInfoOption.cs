using Bootstrap.Client.DataAccess;
using Longbow.Web.Mvc;
using System.Linq;
using System;
//using Bootstrap.Client.DataAccess.BestApp.Helper;
using Bootstrap.Client.DataAccess.Helper;

namespace Bootstrap.Client.Query
{
    /// <summary>
    /// 過濾司機
    /// </summary>
    public class QueryAPP_DriverInfoOption : CustomPaginationOption
    {
        /// <summary>
        /// 
        /// </summary>
        public string Driver_Phone { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string VEHICLE_ID_NO { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string Driver { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string Status { get; set; }

        /// <summary>
        /// 取得過濾的司機清單
        /// </summary>
        public QueryData<object> RetrieveData()
        {
            var data = APP_DriverInfoHelper.Retrieves();
            if (!string.IsNullOrEmpty(Driver_Phone))
            {
                data = data.Where(t => t.Driver_Phone.Contains(Driver_Phone));
            }
            if (!string.IsNullOrEmpty(VEHICLE_ID_NO))
            {
                data = data.Where(t => t.VEHICLE_ID_NO.Contains(VEHICLE_ID_NO));
            }
            if (!string.IsNullOrEmpty(Driver))
            {
                data = data.Where(t => t.Driver.Contains(Driver));
            }
            if (!string.IsNullOrEmpty(Status))
            {
                data = data.Where(t => t.Status.Contains(Status));
            }           
            var ret = new QueryData<object>();
            ret.total = data.Count();
            switch (Sort)
            {
                case "Driver_Phone":
                    data = Order == "asc" ? data.OrderBy(t => t.Driver_Phone) : data.OrderByDescending(t => t.Driver_Phone);
                    break;
                case "VEHICLE_ID_NO":
                    data = Order == "asc" ? data.OrderBy(t => t.VEHICLE_ID_NO) : data.OrderByDescending(t => t.VEHICLE_ID_NO);
                    break;
                case "Driver":
                    data = Order == "asc" ? data.OrderBy(t => t.Driver) : data.OrderByDescending(t => t.Driver);
                    break;
                case "Status":
                    data = Order == "asc" ? data.OrderBy(t => t.Status) : data.OrderByDescending(t => t.Status);
                    break;  
            }
            ret.rows = data.Skip(Offset).Take(Limit).Select(u => new
            {
                u.Driver_Phone,
                u.VEHICLE_ID_NO,
                u.Driver,
                u.Status,
                u.PushToken,
                u.Platform
            });
            return ret;
        }
    }
}
