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
    public class QueryDataComparisonOrdersOption : CustomPaginationOption
    {
        /// <summary>
        /// 
        /// </summary>
        public string StorerKey { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public QueryData<object> RetrieveData()
        {
            var data = DataComparisonOrdersHelper.Retrieves();
            if (!string.IsNullOrEmpty(StorerKey))
            {
                data = data.Where(t => t.StorerKey.Contains(StorerKey));
            }
            var ret = new QueryData<object>();
            ret.total = data.Count();
            switch (Sort)
            {
                case "StorerKey":
                    data = Order == "asc" ? data.OrderBy(t => t.StorerKey) : data.OrderByDescending(t => t.StorerKey);
                    break;
                case "Facility":
                    data = Order == "asc" ? data.OrderBy(t => t.Facility) : data.OrderByDescending(t => t.Facility);
                    break;
                case "ExternOrderKey":
                    data = Order == "asc" ? data.OrderBy(t => t.ExternOrderKey) : data.OrderByDescending(t => t.ExternOrderKey);
                    break;
                case "ExternLineNumber":
                    data = Order == "asc" ? data.OrderBy(t => t.ExternLineNumber) : data.OrderByDescending(t => t.ExternLineNumber);
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
                case "PickUpConsigneeKey":
                    data = Order == "asc" ? data.OrderBy(t => t.PickUpConsigneeKey) : data.OrderByDescending(t => t.PickUpConsigneeKey);
                    break;
                case "ConsigneeKey":
                    data = Order == "asc" ? data.OrderBy(t => t.ConsigneeKey) : data.OrderByDescending(t => t.ConsigneeKey);
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
                case "InvoiceNo":
                    data = Order == "asc" ? data.OrderBy(t => t.InvoiceNo) : data.OrderByDescending(t => t.InvoiceNo);
                    break;
                case "Notes":
                    data = Order == "asc" ? data.OrderBy(t => t.Notes) : data.OrderByDescending(t => t.Notes);
                    break;
                case "OTQty":
                    data = Order == "asc" ? data.OrderBy(t => t.OTQty) : data.OrderByDescending(t => t.OTQty);
                    break;
                case "Sku":
                    data = Order == "asc" ? data.OrderBy(t => t.Sku) : data.OrderByDescending(t => t.Sku);
                    break;
                case "OrderQty":
                    data = Order == "asc" ? data.OrderBy(t => t.OrderQty) : data.OrderByDescending(t => t.OrderQty);
                    break;
            }
            if (Limit != 0) data = data.Skip(Offset).Take(Limit);
            //重新查詢欄位資料
            ret.rows = data.Select(u => new
            {
                Id = u.StorerKey,
                u.StorerKey,
                u.ExternOrderKey,
                u.ExternLineNumber,
                u.CustomerOrderKey,
                u.OrderType,
                u.OrderDate,
                u.DeliveryDate,
                u.PickUpConsigneeKey,
                u.ConsigneeKey,
                u.UrgentMark,
                u.ReserveMark,
                u.ColdMark,
                u.InvoiceNo,
                u.Notes,
                u.OTQty,
                u.Sku,
                u.OrderQty
            });
            return ret;
        }
    }
}

