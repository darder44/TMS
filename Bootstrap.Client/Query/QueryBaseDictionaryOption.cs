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
    public class QueryBaseDictionaryOption : CustomPaginationOption
    {
        /// <summary>
        /// 
        /// </summary>
        public string CodeName { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string Code { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string Description { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string Facility { get; set; }

        /// <summary>
        /// 查詢欄位
        /// </summary>
        private IEnumerable<BaseDictionary> InternalQueryData(IEnumerable<BaseDictionary> data, out int dataCount)
        {
            if (!string.IsNullOrEmpty(CodeName))
            {
                data = data.Where(t => t.CodeName.Contains(CodeName));
            }
            if (!string.IsNullOrEmpty(Code))
            {
                data = data.Where(t => t.Code.Contains(Code));
            }
            if (!string.IsNullOrEmpty(Description))
            {
                data = data.Where(t => t.Description.Contains(Description));
            }
            if (!string.IsNullOrEmpty(Facility))
            {
                data = data.Where(t => t.Facility.Contains(Facility));
            }

            dataCount = data.Count();

            switch (Sort)
            {
                case "CodeName":
                    data = Order == "asc" ? data.OrderBy(t => t.CodeName) : data.OrderByDescending(t => t.CodeName);
                    break;
                case "Code":
                    data = Order == "asc" ? data.OrderBy(t => t.Code) : data.OrderByDescending(t => t.Code);
                    break;
                case "Description":
                    data = Order == "asc" ? data.OrderBy(t => t.Description) : data.OrderByDescending(t => t.Description);
                    break;
                case "Facility":
                    data = Order == "asc" ? data.OrderBy(t => t.Facility) : data.OrderByDescending(t => t.Facility);
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
            var data = InternalQueryData(BaseDictionaryHelper.Retrieves(), out dataCount);            
            var ret = new QueryData<object>();
            ret.total = dataCount;        
            
            ret.rows = data.Select(u => new
            {
                Id = u.CodeName + u.CodeName,
                u.CodeName,
                u.Code,
                u.Description,
                u.Facility,
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
        public IEnumerable<BaseDictionary> RetrievesExcel()
        {
            var dataCount = 0;
            var data = InternalQueryData(BaseDictionaryHelper.Retrieves(), out dataCount);
            return data;
        }
    }
}
