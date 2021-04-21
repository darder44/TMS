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
    public class QueryNormalStandbyOrdersOption : CustomPaginationOption
    {
        /// <summary>
        /// 
        /// </summary>
        public string StorerKeys { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string OrderStatus { get; set; }
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
        public string Zip { get; set; }
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
        public DateTime? DeliveryDate_Start { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public DateTime? DeliveryDate_End { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string AreaCode { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string  OrderType { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string  OrderTypes { get; set; }
        


        /// <summary>
        /// 
        /// </summary>
        public QueryData<object> RetrieveOrders(string facility)
        {
            var data = NormalStandbyOrdersHelper.Retrieves(facility);
            data = QueryData(data);
            return SelectData(InternalRetrieveData(data));
        }

        /// <summary>
        /// 
        /// </summary>
        public QueryData<object> RetrievesCustomerTemp(string facility)
        {
            var data = NormalStandbyOrdersHelper.RetrievesCustomerTemp(facility);
            data = QueryData(data);
            return SelectCustomerTemp(InternalRetrieveData(data));
        }

        /// <summary>
        /// 
        /// </summary>
        public QueryData<object> RetrievesReturnOrders(string facility)
        {
            var data = NormalStandbyOrdersHelper.RetrievesReturnOrders(facility);
            data = QueryData(data);
            return SelectReturnOrders(InternalRetrieveData(data));
        }

        /// <summary>
        /// 
        /// </summary>
        public IEnumerable<NormalStandbyOrders> QueryData(IEnumerable<NormalStandbyOrders> data)
        {

            if (!string.IsNullOrEmpty(AreaCode))
            {
                data = data.Where(t => t.AreaCode.Trim().ToUpper() == AreaCode.Trim().ToUpper());
            }
            if (!string.IsNullOrEmpty(OrderType))
            {
                data = data.Where(t => t.OrderType.Trim().ToUpper() == OrderType.Trim().ToUpper());
            }
            if (!string.IsNullOrEmpty(StorerKeys))
            {
                var storerkeys = StorerKeys.Split(",");
                data = data.Where( d => storerkeys.Contains(d.StorerKey));
            }
            if (!string.IsNullOrEmpty(OrderStatus))
            {
                var orderstatus = OrderStatus.Split(",");
                data = data.Where( d => OrderStatus.Contains(d.Status));
            }
            if (!string.IsNullOrEmpty(OrderTypes))
            {
                var ordertypes = OrderTypes.Split(",");
                data = data.Where( d => OrderTypes.Contains(d.OrderType));
            }
            if (!string.IsNullOrEmpty(TMSKey))
            {
                data = data.Where(t => t.TMSKey.Trim().ToUpper() == TMSKey.Trim().ToUpper());
            }
            if (!string.IsNullOrEmpty(ExternOrderKey))
            {
                data = data.Where(t => t.ExternOrderKey.Trim().ToUpper() == ExternOrderKey.Trim().ToUpper());
            }
            if (!string.IsNullOrEmpty(ConsigneeKey))
            {
                data = data.Where(t => t.ConsigneeKey.Trim().ToUpper() == ConsigneeKey.Trim().ToUpper());
            }
            if (!string.IsNullOrEmpty(Zip))
            {
                data = data.Where(t => t.Zip.Trim().ToUpper() == Zip.Trim().ToUpper());
            }
            if (OrderDate_Start != null)
            {
                if (OrderDate_End != null)
                {
                    data = data.Where(t => t.OrderDate >= OrderDate_Start);
                } 
                else
                {
                    data = data.Where(t => t.OrderDate == OrderDate_Start);
                }
            }
            if (OrderDate_End != null)
            {
                if (OrderDate_Start != null)
                {
                    data = data.Where(t => t.OrderDate <= OrderDate_End);
                }
                else
                {
                    data = data.Where(t => t.OrderDate == OrderDate_End);
                }
            }
            if (DeliveryDate_Start != null)
            {
                if (DeliveryDate_End != null)
                {
                    data = data.Where(t => t.DeliveryDate >= DeliveryDate_Start);
                } 
                else
                {
                    data = data.Where(t => t.DeliveryDate == DeliveryDate_Start);
                }
            }
            if (DeliveryDate_End != null)
            {
                if (DeliveryDate_Start != null)
                {
                    data = data.Where(t => t.DeliveryDate <= DeliveryDate_End);
                }
                else
                {
                    data = data.Where(t => t.DeliveryDate == DeliveryDate_End);
                }
            }

            return data;
        }

        /// <summary>
        /// 查詢
        /// </summary>
        public IEnumerable<NormalStandbyOrders> InternalRetrieveData(IEnumerable<NormalStandbyOrders> data)
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
                case "WaveKey":
                    data = Order == "asc" ? data.OrderBy(t => t.WaveKey) : data.OrderByDescending(t => t.WaveKey);
                break;
                case "AllocateStatus":
                    data = Order == "asc" ? data.OrderBy(t => t.AllocateStatus) : data.OrderByDescending(t => t.AllocateStatus);
                break;
                case "ExternOrderKey":
                    data = Order == "asc" ? data.OrderBy(t => t.ExternOrderKey) : data.OrderByDescending(t => t.ExternOrderKey);
                break;
                case "CustomerOrderKey":
                    data = Order == "asc" ? data.OrderBy(t => t.CustomerOrderKey) : data.OrderByDescending(t => t.CustomerOrderKey);
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
                case "ConsigneeKey":
                    data = Order == "asc" ? data.OrderBy(t => t.ConsigneeKey) : data.OrderByDescending(t => t.ConsigneeKey);
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
                case "ShortName":
                    data = Order == "asc" ? data.OrderBy(t => t.ShortName) : data.OrderByDescending(t => t.ShortName);
                break;
                case "ShipTo":
                    data = Order == "asc" ? data.OrderBy(t => t.ShipTo) : data.OrderByDescending(t => t.ShipTo);
                break;
                case "ShipToName":
                    data = Order == "asc" ? data.OrderBy(t => t.ShipToName) : data.OrderByDescending(t => t.ShipToName);
                break;
                case "ShipToAddress":
                    data = Order == "asc" ? data.OrderBy(t => t.ShipToAddress) : data.OrderByDescending(t => t.ShipToAddress);
                break;
                case "Zip":
                    data = Order == "asc" ? data.OrderBy(t => t.Zip) : data.OrderByDescending(t => t.Zip);
                break;
                case "AreaCode":
                    data = Order == "asc" ? data.OrderBy(t => t.AreaCode) : data.OrderByDescending(t => t.AreaCode);
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
                case "CaseQty":
                    data = Order == "asc" ? data.OrderBy(t => t.CaseQty) : data.OrderByDescending(t => t.CaseQty);
                break;
                case "PalletQty":
                    data = Order == "asc" ? data.OrderBy(t => t.PalletQty) : data.OrderByDescending(t => t.PalletQty);
                break;
                case "Cube":
                    data = Order == "asc" ? data.OrderBy(t => t.Cube) : data.OrderByDescending(t => t.Cube);
                break;
                case "Weight":
                    data = Order == "asc" ? data.OrderBy(t => t.Weight) : data.OrderByDescending(t => t.Weight);
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
                case "OriginalRouteNo":
                    data = Order == "asc" ? data.OrderBy(t => t.OriginalRouteNo) : data.OrderByDescending(t => t.OriginalRouteNo);
                break;

            }
            return data;            
        }

        /// <summary>
        /// 
        /// </summary>
        public QueryData<object> SelectData(IEnumerable<NormalStandbyOrders> data)
        {
            var ret = new QueryData<object>();
            ret.total = data.Count();
            if (Limit != 0) data = data.Skip(Offset).Take(Limit);
            ret.rows = data.Select(u => new
            {
                u.StorerKey,
                u.Facility,
                u.TMSKey,
                u.ExternOrderKey,
                u.Zip,
                u.AreaCode,
                u.OrderType,
                u.OrderDate,
                u.DeliveryDate,
                u.ConsigneeKey,
                u.PickUpConsigneeKey,
                u.PickUpName,
                u.PickUpAddress,
                u.ShortName,
                u.ShipTo,
                u.ShipToName,
                u.ShipToAddress,
                u.Contact,
                u.Phone,
                u.CustomerOrderKey,
                u.Notes,
                u.CaseQty,
                u.PalletQty,
                u.Cube,
                u.Weight,
                u.UrgentMark,
                u.ReserveMark,
                u.ColdMark,
                u.OriginalRouteNo,
                u.WaveKey,
                u.AllocateStatus,
                u.Status
            });
            return ret;
        }

        /// <summary>
        /// 
        /// </summary>
        public QueryData<Object> SelectCustomerTemp(IEnumerable<NormalStandbyOrders> data)
        {
            var ret = new QueryData<object>();
            ret.total = data.Count();
            if (Limit != 0) data = data.Skip(Offset).Take(Limit);
            ret.rows = data.Select(u => new
            {
                u.StorerKey,               
                u.ConsigneeKey,
                u.TMSKey,
                u.ExternOrderKey,
                u.ShortName,        
                u.ShipToAddress,
                u.Zip,
                u.Contact,
                u.Phone              
            });
            return ret;
        }

        /// <summary>
        /// 
        /// </summary>
        public QueryData<Object> SelectReturnOrders(IEnumerable<NormalStandbyOrders> data)
        {
            var ret = new QueryData<object>();
            ret.total = data.Count();
            if (Limit != 0) data = data.Skip(Offset).Take(Limit);
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
                u.ConsigneeKey,
                u.PickUpConsigneeKey,
                u.PickUpName,
                u.PickUpAddress,
                u.ShortName,
                u.ShipTo,
                u.ShipToName,
                u.ShipToAddress,
                u.Zip,
                u.AreaCode,
                u.Contact,
                u.Phone,
                u.Notes,
                u.CaseQty,
                u.PalletQty,
                u.Cube,
                u.Weight,
                u.UrgentMark,
                u.ReserveMark,
                u.ColdMark,
                u.OriginalRouteNo             
            });
            return ret;
        }

    }
}
