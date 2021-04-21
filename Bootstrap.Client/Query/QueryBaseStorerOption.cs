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
    public class QueryBaseStorerOption : CustomPaginationOption
    {
        /// <summary>
        /// 
        /// </summary>
        public string Id { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string StorerKeys { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string Facility { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string ChineseName { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string EnglishName { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string ShortName { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string Phone { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string Contact { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string Address { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string Description { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string ShipListReport { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string StorerStatus { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string AddWho { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public DateTime? AddDate { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string EditWho { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public DateTime? EditDate { get; set; }

        /// <summary>
        /// 查詢欄位
        /// </summary>
        private IEnumerable<BaseStorer> InternalQueryData(IEnumerable<BaseStorer> data, out int dataCount)
        {
            if (!string.IsNullOrEmpty(StorerKeys))
            {
                var storerkeys = StorerKeys.Split(",");
                data = data.Where( d => storerkeys.Contains(d.StorerKey));
            }

            dataCount = data.Count();

            switch (Sort)
            {
                case "StorerKey":
                    data = Order == "asc" ? data.OrderBy(t => t.StorerKey) : data.OrderByDescending(t => t.StorerKey);
                break;
                case "Facility":
                    data = Order == "asc" ? data.OrderBy(t => t.Facility) : data.OrderByDescending(t => t.Facility);
                break;
                case "ChineseName":
                    data = Order == "asc" ? data.OrderBy(t => t.ChineseName) : data.OrderByDescending(t => t.ChineseName);
                break;
                case "EnglishName":
                    data = Order == "asc" ? data.OrderBy(t => t.EnglishName) : data.OrderByDescending(t => t.EnglishName);
                break;
                case "ShortName":
                    data = Order == "asc" ? data.OrderBy(t => t.ShortName) : data.OrderByDescending(t => t.ShortName);
                break;
                case "Phone":
                    data = Order == "asc" ? data.OrderBy(t => t.Phone) : data.OrderByDescending(t => t.Phone);
                break;
                case "Contact":
                    data = Order == "asc" ? data.OrderBy(t => t.Contact) : data.OrderByDescending(t => t.Contact);
                break;
                case "Address":
                    data = Order == "asc" ? data.OrderBy(t => t.Address) : data.OrderByDescending(t => t.Address);
                break;
                case "Description":
                    data = Order == "asc" ? data.OrderBy(t => t.Description) : data.OrderByDescending(t => t.Description);
                break;
                case "ShipListReport":
                    data = Order == "asc" ? data.OrderBy(t => t.ShipListReport) : data.OrderByDescending(t => t.ShipListReport);
                break;
                case "StorerStatus":
                    data = Order == "asc" ? data.OrderBy(t => t.StorerStatus) : data.OrderByDescending(t => t.StorerStatus);
                break;
                case "AddWho":
                    data = Order == "asc" ? data.OrderBy(t => t.AddWho) : data.OrderByDescending(t => t.AddWho);
                break;
                case "AddDate":
                    data = Order == "asc" ? data.OrderBy(t => t.AddDate) : data.OrderByDescending(t => t.AddDate);
                break;
                case "EditWho":
                    data = Order == "asc" ? data.OrderBy(t => t.EditWho) : data.OrderByDescending(t => t.EditWho);
                break;
                case "EditDate":
                    data = Order == "asc" ? data.OrderBy(t => t.EditDate) : data.OrderByDescending(t => t.EditDate);
                break;

            }
            if (Limit != 0) data = data.Skip(Offset).Take(Limit);
            return data;
        }

        /// <summary>
        /// 
        /// </summary>
        public QueryData<object> RetrieveData()
        {
            var dataCount = 0;
            var data = InternalQueryData(BaseStorerHelper.Retrieves(), out dataCount);                   
            var ret = new QueryData<object>();
            ret.total = dataCount;
            
            //重新查詢欄位資料
            ret.rows = data.Select(u => new
            {
                Id = u.StorerKey + u.Facility,
                u.StorerKey,
                u.Facility,
                u.ChineseName,
                u.EnglishName,
                u.ShortName,
                u.Phone,
                u.Contact,
                u.Address,
                u.Description,
                u.ShipListReport,
                u.StorerStatus,
                u.AddWho,
                u.AddDate,
                u.EditWho,
                u.EditDate
            });
            return ret;
        }

        /// <summary>
        /// 
        /// </summary>  
        public IEnumerable<BaseStorer> RetrievesExcel()
        {
            var dataCount = 0;
            var data = InternalQueryData(BaseStorerHelper.Retrieves(), out dataCount);
            return data;
        }
    }
}
