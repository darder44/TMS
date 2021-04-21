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
    public class QueryBaseFacilityOption : CustomPaginationOption
    {
        /// <summary>
        /// 
        /// </summary>
        public string Facility { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string Description { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string Code { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string Phone { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string Address { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string AddWho { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public DateTime? Adddate { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string EditWho { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public DateTime? EditDate { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string Type { get; set; }

        /// <summary>
        /// 查詢欄位
        /// </summary>
        private IEnumerable<BaseFacility> InternalQueryData(IEnumerable<BaseFacility> data, out int dataCount)
        {
            //basefacility.js queryParams 調用
            if (!string.IsNullOrEmpty(Facility))
            {
                data = data.Where(t => t.Facility.Contains(Facility));
            }
            if (!string.IsNullOrEmpty(Type))
            {
                data = data.Where(t => t.Type.Contains(Type));
            }

            dataCount = data.Count();

            switch (Sort)
            {
                case "Facility":
                    data = Order == "asc" ? data.OrderBy(t => t.Facility) : data.OrderByDescending(t => t.Facility);
                    break;
                case "Description":
                    data = Order == "asc" ? data.OrderBy(t => t.Description) : data.OrderByDescending(t => t.Description);
                    break;
                case "Phone":
                    data = Order == "asc" ? data.OrderBy(t => t.Phone) : data.OrderByDescending(t => t.Phone);
                    break;
                case "Address":
                    data = Order == "asc" ? data.OrderBy(t => t.Address) : data.OrderByDescending(t => t.Address);
                    break;
                case "AddWho":
                    data = Order == "asc" ? data.OrderBy(t => t.AddWho) : data.OrderByDescending(t => t.AddWho);
                    break;
                case "Adddate":
                    data = Order == "asc" ? data.OrderBy(t => t.Adddate) : data.OrderByDescending(t => t.Adddate);
                    break;
                case "EditWho":
                    data = Order == "asc" ? data.OrderBy(t => t.EditWho) : data.OrderByDescending(t => t.EditWho);
                    break;
                case "EditDate":
                    data = Order == "asc" ? data.OrderBy(t => t.EditDate) : data.OrderByDescending(t => t.EditDate);
                    break;
                case "Type":
                    data = Order == "asc" ? data.OrderBy(t => t.Type) : data.OrderByDescending(t => t.Type);
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
            var data = InternalQueryData(BaseFacilityHelper.Retrieves(), out dataCount);            
            var ret = new QueryData<object>();
            ret.total = dataCount;        
               
            ret.rows = data.Select(u => new
            {
                u.ID,
                u.Facility,
                u.Description,
                u.Phone,
                u.Address,
                u.AddWho,
                u.Adddate,
                u.EditWho,
                u.EditDate,
                u.Type
            });
            return ret;
        }

        /// <summary>
        /// 
        /// </summary>  
        public IEnumerable<BaseFacility> RetrievesExcel()
        {
            var dataCount = 0;
            var data = InternalQueryData(BaseFacilityHelper.Retrieves(), out dataCount);
            return data;
        }
    }
}
