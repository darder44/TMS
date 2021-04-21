using Bootstrap.Client.DataAccess;
using Longbow.Web.Mvc;
using System.Linq;
using System;
using Bootstrap.Client.DataAccess.Helper;
using System.Collections.Generic;

namespace Bootstrap.Client.Query
{
    /// <summary>
    /// 
    /// </summary>
    public class QueryNormalOrdersOption : CustomPaginationOption
    {
        /// <summary>
        /// 貨主代號
        /// </summary>
        public string StorerKeys { get; set; }
        /// <summary>
        /// 訂單單號
        /// </summary>
        public string TMSKey { get; set; }
        /// <summary>
        /// 訂單狀態
        /// </summary>
        public string Status { get; set; }
        /// <summary>
        /// 貨主單號
        /// </summary>
        public string ExternOrderKey { get; set; }
        /// <summary>
        /// 訂單類型
        /// </summary>
        public string OrderTypes { get; set; }
        /// <summary>
        /// 訂單日期(起)
        /// </summary>
        public DateTime? OrderDate_Start { get; set; }
        /// <summary>
        /// 訂單日期(訖)
        /// </summary>
        public DateTime? OrderDate_End { get; set; }
        /// <summary>
        /// 到貨日期(起)
        /// </summary>
        public DateTime? DeliveryDate_Start { get; set; }
        /// <summary>
        /// 到貨日期(訖)
        /// </summary>
        public DateTime? DeliveryDate_End { get; set; }
        /// <summary>
        /// 轉入日期(起)
        /// </summary>
        public DateTime? AddDate_Start { get; set; }
        /// <summary>
        /// 轉入日期(訖)
        /// </summary>
        public DateTime? AddDate_End { get; set; }

        /// <summary>
        /// 查詢欄位
        /// </summary>
        private IEnumerable<NormalOrders> InternalQueryData(IEnumerable<NormalOrders> data, out int dataCount)
        {
            switch (Sort)
            {
                case "StorerKey":
                    data = Order == "asc" ? data.OrderBy(t => t.StorerKey) : data.OrderByDescending(t => t.StorerKey);
                    break;
                case "Facility":
                    data = Order == "asc" ? data.OrderBy(t => t.Facility) : data.OrderByDescending(t => t.Facility);
                    break;
                case "TMSKey":
                    data = Order == "asc" ? data.OrderBy(t => t.TMSKey) : data.OrderByDescending(t => t.TMSKey);
                    break;
                case "Status":
                    data = Order == "asc" ? data.OrderBy(t => t.Status) : data.OrderByDescending(t => t.Status);
                    break;
                case "ExternOrderKey":
                    data = Order == "asc" ? data.OrderBy(t => t.ExternOrderKey) : data.OrderByDescending(t => t.ExternOrderKey);
                    break;
                case "OrderType":
                    data = Order == "asc" ? data.OrderBy(t => t.OrderType) : data.OrderByDescending(t => t.OrderType);
                    break;
                case "OrderDate":
                    data = Order == "asc" ? data.OrderBy(t => t.OrderDate) : data.OrderByDescending(t => t.OrderDate);
                    break;
                case "DeliveryDate":
                    data = Order == "asc" ? data.OrderBy(t => t.DeliveryDate) : data.OrderByDescending(t => t.DeliveryDate);
                    break;
                case "AddDate":
                    data = Order == "asc" ? data.OrderBy(t => t.AddDate) : data.OrderByDescending(t => t.AddDate);
                    break;
                case "ConsigneeKey":
                    data = Order == "asc" ? data.OrderBy(t => t.ConsigneeKey) : data.OrderByDescending(t => t.ConsigneeKey);
                    break;
                case "ShortName":
                    data = Order == "asc" ? data.OrderBy(t => t.ShortName) : data.OrderByDescending(t => t.ShortName);
                    break;
                case "PickUpConsigneeKey":
                    data = Order == "asc" ? data.OrderBy(t => t.PickUpConsigneeKey) : data.OrderByDescending(t => t.PickUpConsigneeKey);
                    break;
                case "PickUpName":
                    data = Order == "asc" ? data.OrderBy(t => t.PickUpName) : data.OrderByDescending(t => t.PickUpName);
                    break;
                case "PickUpAddress":
                    data = Order == "asc" ? data.OrderBy(t => t.PickUpAddress) : data.OrderByDescending(t => t.PickUpAddress);
                    break;
                case "ShipToAddress":
                    data = Order == "asc" ? data.OrderBy(t => t.ShipToAddress) : data.OrderByDescending(t => t.ShipToAddress);
                    break;
                case "Zip":
                    data = Order == "asc" ? data.OrderBy(t => t.Zip) : data.OrderByDescending(t => t.Zip);
                    break;
                case "Contact":
                    data = Order == "asc" ? data.OrderBy(t => t.Contact) : data.OrderByDescending(t => t.Contact);
                    break;
                case "Phone":
                    data = Order == "asc" ? data.OrderBy(t => t.Phone) : data.OrderByDescending(t => t.Phone);
                    break;
                case "Notes":
                    data = Order == "asc" ? data.OrderBy(t => t.Notes) : data.OrderByDescending(t => t.Notes);
                    break;
            }
            dataCount = data.Count();
            if (Limit != 0) data = data.Skip(Offset).Take(Limit);
            return data;
        }

        /// <summary>
        /// 
        /// </summary>
        public QueryData<object> Retrieves(string facility)
        {
            var storers = string.IsNullOrEmpty(StorerKeys) ? "" : string.Join(",", StorerKeys.Split(",").Select(p => string.Format("'{0}'", p)).ToArray());
            var ordertypes = string.IsNullOrEmpty(OrderTypes) ? "" : string.Join(",", OrderTypes.Split(",").Select(p => string.Format("'{0}'", p)).ToArray());
            var tmskey = string.IsNullOrEmpty(TMSKey) ? "" : TMSKey;
            var externorderkey = string.IsNullOrEmpty(ExternOrderKey) ? "" : ExternOrderKey;
            var orderdates = OrderDate_Start.HasValue ? DataComparison.DateTimeConvert(OrderDate_Start) : "";
            var orderdatee = OrderDate_End.HasValue ? DataComparison.DateTimeConvert(OrderDate_End) : "";
            var deliverydates = DeliveryDate_Start.HasValue ? DataComparison.DateTimeConvert(DeliveryDate_Start) : "";
            var deliverydatee = DeliveryDate_End.HasValue ? DataComparison.DateTimeConvert(DeliveryDate_End) : "";
            var adddates = AddDate_Start.HasValue ? DataComparison.DateTimeConvert(AddDate_Start) : "";
            var adddatee = AddDate_End.HasValue ? DataComparison.DateTimeConvert(AddDate_End) : "";

            var data = NormalOrdersHelper.Retrieves(storers, ordertypes, tmskey, externorderkey, orderdates, orderdatee, deliverydates, deliverydatee, adddates, adddatee, facility);

            if (!string.IsNullOrEmpty(Status))
            {
                data = data.Where( d => d.Status.Trim() == Status.Trim());
            }

            var dataCount = 0;
            data = InternalQueryData(data, out dataCount);
            var ret = new QueryData<object>();
            ret.total = dataCount;
            ret.rows = data.Select(u => new
            {
                u.StorerKey,
                u.Facility,
                u.TMSKey,
                u.ExternOrderKey,
                u.CustomerOrderKey,
                u.OrderType,
                u.OrderDate,
                u.DeliveryDate,
                u.AddDate,
                u.ConsigneeKey,
                u.ShortName,
                u.PickUpConsigneeKey,
                u.PickUpName,
                u.PickUpAddress,
                u.ShipToAddress,
                u.Zip,
                u.Contact,
                u.Phone,
                u.Notes,
                u.Status
            });
            return ret;
        }

        /// <summary>
        /// 
        /// </summary>
        public IEnumerable<NormalOrders> RetrievesExcel(string facility)
        {
            
            var data = NormalOrdersHelper.RetrievesExcel(facility);

            if (!string.IsNullOrEmpty(StorerKeys))
            {
                var storerkeys = StorerKeys.Split(",");
                data = data.Where( d => storerkeys.Contains(d.StorerKey));
            }

            if (!string.IsNullOrEmpty(OrderTypes))
            {
                var ordertypes = OrderTypes.Split(",");
                data = data.Where( d => ordertypes.Contains(d.OrderType));
            }

            if (!string.IsNullOrEmpty(TMSKey))
            {
                data = data.Where( d => d.TMSKey.Contains(TMSKey));
            }

            if (!string.IsNullOrEmpty(ExternOrderKey))
            {
                data = data.Where( d => d.ExternOrderKey.Contains(ExternOrderKey));
            }

            if (OrderDate_Start != null)
            {
                data = data.Where( d => DataComparison.DateTimeGreaterThan(DataComparison.DateTimeConvert(d.OrderDate), OrderDate_Start));
            }

            if (OrderDate_End != null)
            {
                data = data.Where( d => DataComparison.DateTimeLessThan(DataComparison.DateTimeConvert(d.OrderDate), OrderDate_End));
            }

            if (DeliveryDate_Start != null)
            {
                data = data.Where( d => DataComparison.DateTimeGreaterThan(DataComparison.DateTimeConvert(d.DeliveryDate), DeliveryDate_Start));
            }

            if (DeliveryDate_End != null)
            {
                data = data.Where( d => DataComparison.DateTimeLessThan(DataComparison.DateTimeConvert(d.DeliveryDate), DeliveryDate_End));
            }

            if (AddDate_Start != null)
            {
                data = data.Where( d => d.AddDate >= AddDate_Start);
            }

            if (AddDate_End != null)
            {
                data = data.Where( d => d.AddDate <= AddDate_End);
            }

            var dataCount = 0;
            data = InternalQueryData(data, out dataCount);
            return data;
        }
    }
}
