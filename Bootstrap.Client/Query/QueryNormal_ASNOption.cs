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
    public class QueryNormal_ASNOption : CustomPaginationOption
    {
        /// <summary>
        /// 貨主代號
        /// </summary>
        public string StorerKey { get; set; }
        /// <summary>
        /// 驗收單號
        /// </summary>
        public string ASNKey { get; set; }
        /// <summary>
        /// 貨主通知單號
        /// </summary>
        public string ExternASNKey { get; set; }        

        /// <summary>
        /// 
        /// </summary>
        public QueryData<object> RetrieveData()
        {
            var data = Normal_ASNHelper.RetrievesASNnotinReceipt();
            if (!string.IsNullOrEmpty(ExternASNKey))
            {
                data = data.Where(t => t.ExternASNKey.Contains(ExternASNKey));
            }
            if (!string.IsNullOrEmpty(ASNKey))
            {
                data = data.Where(t => t.ASNKey.Contains(ASNKey));
            }
            if (!string.IsNullOrEmpty(StorerKey))
            {
                data = data.Where(t => t.StorerKey.Contains(StorerKey.ToUpper()));
            }
            var ret = new QueryData<object>();
            ret.total = data.Count();
            switch (Sort)
            {
                case "StorerKey":
                    data = Order == "asc" ? data.OrderBy(t => t.StorerKey) : data.OrderByDescending(t => t.StorerKey);
                break;
                case "ASNKey":
                    data = Order == "asc" ? data.OrderBy(t => t.ASNKey) : data.OrderByDescending(t => t.ASNKey);
                break;                
                case "ExternASNKey":
                    data = Order == "asc" ? data.OrderBy(t => t.ExternASNKey) : data.OrderByDescending(t => t.ExternASNKey);
                break;
                case "ASNDate":
                    data = Order == "asc" ? data.OrderBy(t => t.ASNDate) : data.OrderByDescending(t => t.ASNDate);
                break;
                case "ASNType":
                    data = Order == "asc" ? data.OrderBy(t => t.ASNType) : data.OrderByDescending(t => t.ASNType);
                break;
                case "OrderQty":
                    data = Order == "asc" ? data.OrderBy(t => t.OrderQty) : data.OrderByDescending(t => t.OrderQty);
                break;
                case "CaseQty":
                    data = Order == "asc" ? data.OrderBy(t => t.CaseQty) : data.OrderByDescending(t => t.CaseQty);
                break;
                case "Status":
                    data = Order == "asc" ? data.OrderBy(t => t.Status) : data.OrderByDescending(t => t.Status);
                break;
                case "SellersReference2":
                    data = Order == "asc" ? data.OrderBy(t => t.SellersReference2) : data.OrderByDescending(t => t.SellersReference2);
                break;
                case "TransMethod":
                    data = Order == "asc" ? data.OrderBy(t => t.TransMethod) : data.OrderByDescending(t => t.TransMethod);
                break;
            }
            if (Limit != 0) data = data.Skip(Offset).Take(Limit);
            //重新查詢欄位資料
            ret.rows = data.Select(u => new
            {
                u.StorerKey,
                u.ASNKey,
                u.ExternASNKey,
                u.ASNDate,
                u.ASNType,
                u.OrderQty,
                u.CaseQty,
                u.Status,
                u.SellersReference2,
                u.TransMethod
            });
            return ret;
        }
    }
}
