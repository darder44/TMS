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
    public class QueryNormalCutOrdersOption : CustomPaginationOption
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
        public QueryData<object> RetrieveOrders(string facility)
        {
            var data = NormalCutOrdersHelper.RetrieveOrders(facility);
            if (!string.IsNullOrEmpty(StorerKeys))
            {
                var storerkeys = StorerKeys.Split(",");
                data = data.Where( d => storerkeys.Contains(d.StorerKey));
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
            
            var ret = new QueryData<object>();
            ret.total = data.Count();
            switch (Sort)
            {
                case "StorerKey":
                    data = Order == "asc" ? data.OrderBy(t => t.StorerKey) : data.OrderByDescending(t => t.StorerKey);
                break;
                case "TMSKey":
                    data = Order == "asc" ? data.OrderBy(t => t.TMSKey) : data.OrderByDescending(t => t.TMSKey);
                break;
                case "ExternOrderKey":
                    data = Order == "asc" ? data.OrderBy(t => t.ExternOrderKey) : data.OrderByDescending(t => t.ExternOrderKey);
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
                case "ShortName":
                    data = Order == "asc" ? data.OrderBy(t => t.ShortName) : data.OrderByDescending(t => t.ShortName);
                break;
                case "ShipToAddress":
                    data = Order == "asc" ? data.OrderBy(t => t.ShipToAddress) : data.OrderByDescending(t => t.ShipToAddress);
                break;
                case "Zip":
                    data = Order == "asc" ? data.OrderBy(t => t.Zip) : data.OrderByDescending(t => t.Zip);
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

            }
            if (Limit != 0) data = data.Skip(Offset).Take(Limit);
            ret.rows = data.Select(u => new
            {
                u.StorerKey,
                u.TMSKey,
                u.ExternOrderKey,
                u.OrderDate,
                u.DeliveryDate,
                u.ConsigneeKey,
                u.ShortName,
                u.ShipToAddress,
                u.Zip,
                u.CaseQty,
                u.PalletQty,
                u.Cube,
                u.Weight,
                u.Notes,
            });
            return ret;
        }
    }
}
