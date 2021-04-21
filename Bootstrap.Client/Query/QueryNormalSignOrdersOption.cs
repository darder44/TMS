using Bootstrap.Client.DataAccess;
using Longbow.Web.Mvc;
using System.Linq;
using System;
using Bootstrap.Client.DataAccess.Helper;
using System.Collections.Generic;
using Bootstrap.Client;

namespace Bootstrap.Client.Query
{
    /// <summary>
    /// 
    /// </summary>
    public class QueryNormalSignOrdersOption : CustomPaginationOption
    {
        /// <summary>
        /// 
        /// </summary>
        public string StorerKeys { get; set; }
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
        public string RouteNo { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string SdnBack { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string SignStatus { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public DateTime? OrderDate_Start { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public DateTime? OrderDate_End { get; set; }
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
        public QueryData<object> RetrieveOrders(string facility)
        {
            // var data = NormalSignOrdersHelper.RetrieveOrders(facility);
            // //貨主
            // if (!string.IsNullOrEmpty(StorerKeys))
            // {
            //     var storerkeys = StorerKeys.Split(",");
            //     data = data.Where( d => storerkeys.Contains(d.StorerKey));
            // }
            // //訂單號碼
            // if (!string.IsNullOrEmpty(TMSKey))
            // {
            //     data = data.Where(t => t.TMSKey.Contains(TMSKey));
            // }
            // //貨主單號
            // if (!string.IsNullOrEmpty(ExternOrderKey))
            // {
            //     data = data.Where(t => t.ExternOrderKey.Contains(ExternOrderKey));
            // }
            // //路線編號
            // if (!string.IsNullOrEmpty(RouteNo))
            // {
            //     data = data.Where(t => t.RouteNo.Contains(RouteNo));
            // }
            // //簽單回傳
            // if (!string.IsNullOrEmpty(SdnBack))
            // {
            //     var sdnback = SdnBack.Split(",");
            //     data = data.Where( d => sdnback.Contains(d.SdnBack));
            // }
            // //簽單狀態
            // if (!string.IsNullOrEmpty(SignStatus))
            // {
            //     var signstatus = SignStatus.Split(",");
            //     data = data.Where( d => signstatus.Contains(d.ConfirmNotes));
            // }
            // //訂單日期
            // if (OrderDate_Start != null)
            // {
            //     if (OrderDate_End != null)
            //     {
            //         data = data.Where(t => t.OrderDate >= OrderDate_Start);
            //     } 
            //     else
            //     {
            //         data = data.Where(t => t.OrderDate == OrderDate_Start);
            //     }
            // }
            // if (OrderDate_End != null)
            // {
            //     if (OrderDate_Start != null)
            //     {
            //         data = data.Where(t => t.OrderDate <= OrderDate_End);
            //     }
            //     else
            //     {
            //         data = data.Where(t => t.OrderDate == OrderDate_End);
            //     }
            // }
            // //出車日期
            // if (DoRouteDate_Start != null)
            // {
            //     if (DoRouteDate_End != null)
            //     {
            //         data = data.Where(t => t.DoRouteDate >= DoRouteDate_Start);
            //     } 
            //     else
            //     {
            //         data = data.Where(t => t.DoRouteDate == DoRouteDate_Start);
            //     }
            // }
            // if (DoRouteDate_End != null)
            // {
            //     if (DoRouteDate_Start != null)
            //     {
            //         data = data.Where(t => t.DoRouteDate <= DoRouteDate_End);
            //     }
            //     else
            //     {
            //         data = data.Where(t => t.DoRouteDate == DoRouteDate_End);
            //     }
            // }
            // //到貨日期
            // if (DeliveryDate_Start != null)
            // {
            //     if (DeliveryDate_End != null)
            //     {
            //         data = data.Where(t => t.DeliveryDate >= DeliveryDate_Start);
            //     } 
            //     else
            //     {
            //         data = data.Where(t => t.DeliveryDate == DeliveryDate_Start);
            //     }
            // }
            // if (DeliveryDate_End != null)
            // {
            //     if (DeliveryDate_Start != null)
            //     {
            //         data = data.Where(t => t.DeliveryDate <= DeliveryDate_End);
            //     }
            //     else
            //     {
            //         data = data.Where(t => t.DeliveryDate == DeliveryDate_End);
            //     }
            // }
            var storers = string.IsNullOrEmpty(StorerKeys) ? "" : string.Join(",", StorerKeys.Split(",").Select(p => string.Format("'{0}'", p)).ToArray());
            var tmskey = string.IsNullOrEmpty(TMSKey) ? "" : TMSKey;
            var routeno = string.IsNullOrEmpty(RouteNo) ? "" : RouteNo;
            var externorderkey = string.IsNullOrEmpty(ExternOrderKey) ? "" : ExternOrderKey;
            var sdnback = string.IsNullOrEmpty(SdnBack) ? "" : string.Join(",", SdnBack.Split(",").Select(p => string.Format("'{0}'", p)).ToArray());
            var signstatus = string.IsNullOrEmpty(SignStatus) ? "" : string.Join(",", SignStatus.Split(",").Select(p => string.Format("'{0}'", p)).ToArray());
            var orderdates = OrderDate_Start.HasValue ? DataComparison.DateTimeConvert(OrderDate_Start) : "";
            var orderdatee = OrderDate_End.HasValue ? DataComparison.DateTimeConvert(OrderDate_End) : "";
            var doroutedates = DoRouteDate_Start.HasValue ? DataComparison.DateTimeConvert(DoRouteDate_Start) : "";
            var doroutedatee = DoRouteDate_End.HasValue ? DataComparison.DateTimeConvert(DoRouteDate_End) : "";
            var deliverydates = DeliveryDate_Start.HasValue ? DataComparison.DateTimeConvert(DeliveryDate_Start) : "";
            var deliverydatee = DeliveryDate_End.HasValue ? DataComparison.DateTimeConvert(DeliveryDate_End) : "";

            var dataCount = 0;
            var data = InternalRetrieveData(NormalSignOrdersHelper.RetrieveOrders(facility, storers, tmskey, routeno, externorderkey, sdnback, signstatus, orderdates, orderdatee, doroutedates, doroutedatee, deliverydates, deliverydatee), out dataCount);

            var ret = new QueryData<object>();
            ret.total = dataCount;
            ret.rows = data.Select(u => new
            {
                u.StorerKey,
                u.RouteNo,
                u.Facility,
                u.TMSKey,
                u.ExternOrderKey,
                u.CustomerOrderKey,
                u.OrderType,
                u.OrderDate,
                u.DeliveryDate,
                u.DoRouteDate,
                u.ConsigneeKey,
                u.ShortName,
                u.PickUpConsigneeKey,
                u.PickUpName,
                u.PickUpAddress,
                u.ShipToAddress,
                u.Zip,
                u.Contact,
                u.Phone,
                u.UrgentMark,
                u.ReserveMark,
                u.ColdMark,
                u.ConfirmNotes,
                u.ConfirmDate,
                u.ConfirmUser,
                u.SdnBack,
                u.SdnSendDate,
                u.SdnSendWho,
                u.InvBack,
                u.CustHandle,
                u.CostStatus,
                u.SdnNotes
            });
            return ret;
        }

        /// <summary>
        /// 查詢
        /// </summary>
        public IEnumerable<NormalSignOrders> InternalRetrieveData(IEnumerable<NormalSignOrders> data, out int dataCount)
        {
            dataCount = data.Count();
            switch (Sort)
            {
                case "StorerKey":
                    data = Order == "asc" ? data.OrderBy(t => t.StorerKey) : data.OrderByDescending(t => t.StorerKey);
                    break;
                case "RouteNo":
                    data = Order == "asc" ? data.OrderBy(t => t.RouteNo) : data.OrderByDescending(t => t.RouteNo);
                    break;
                case "Facility":
                    data = Order == "asc" ? data.OrderBy(t => t.Facility) : data.OrderByDescending(t => t.Facility);
                    break;
                case "TMSKey":
                    data = Order == "asc" ? data.OrderBy(t => t.TMSKey) : data.OrderByDescending(t => t.TMSKey);
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
                case "DoRouteDate":
                    data = Order == "asc" ? data.OrderBy(t => t.DoRouteDate) : data.OrderByDescending(t => t.DoRouteDate);
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
                case "UrgentMark":
                    data = Order == "asc" ? data.OrderBy(t => t.UrgentMark) : data.OrderByDescending(t => t.UrgentMark);
                    break;
                case "ReserveMark":
                    data = Order == "asc" ? data.OrderBy(t => t.ReserveMark) : data.OrderByDescending(t => t.ReserveMark);
                    break;
                case "ColdMark":
                    data = Order == "asc" ? data.OrderBy(t => t.ColdMark) : data.OrderByDescending(t => t.ColdMark);
                    break;
                case "ConfirmNotes":
                    data = Order == "asc" ? data.OrderBy(t => t.ConfirmNotes) : data.OrderByDescending(t => t.ConfirmNotes);
                    break;
                case "ConfirmDate":
                    data = Order == "asc" ? data.OrderBy(t => t.ConfirmDate) : data.OrderByDescending(t => t.ConfirmDate);
                    break;
                case "ConfirmUser":
                    data = Order == "asc" ? data.OrderBy(t => t.ConfirmUser) : data.OrderByDescending(t => t.ConfirmUser);
                    break;
                case "SdnBack":
                    data = Order == "asc" ? data.OrderBy(t => t.SdnBack) : data.OrderByDescending(t => t.SdnBack);
                    break;
                case "SdnSendDate":
                    data = Order == "asc" ? data.OrderBy(t => t.SdnSendDate) : data.OrderByDescending(t => t.SdnSendDate);
                    break;
                case "SdnSendWho":
                    data = Order == "asc" ? data.OrderBy(t => t.SdnSendWho) : data.OrderByDescending(t => t.SdnSendWho);
                    break;
                case "InvBack":
                    data = Order == "asc" ? data.OrderBy(t => t.InvBack) : data.OrderByDescending(t => t.InvBack);
                    break;
                case "CustHandle":
                    data = Order == "asc" ? data.OrderBy(t => t.CustHandle) : data.OrderByDescending(t => t.CustHandle);
                    break;
                case "SdnNotes":
                    data = Order == "asc" ? data.OrderBy(t => t.SdnNotes) : data.OrderByDescending(t => t.SdnNotes);
                    break;

            }
            if (Limit != 0) data = data.Skip(Offset).Take(Limit);
            return data;
        }
    }
}
