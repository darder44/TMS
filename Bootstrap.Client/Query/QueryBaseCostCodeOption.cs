using Bootstrap.Client.DataAccess;
using Longbow.Web.Mvc;
using System.Linq;
using System;
using System.Collections.Generic;
using Bootstrap.Client.DataAccess.Helper;

namespace Bootstrap.Client.Query
{
    /// <summary>
    /// 
    /// </summary>
    public class QueryBaseCostCodeOption : CustomPaginationOption
    {
        /// <summary>
        /// 
        /// </summary>
        public string StorerKeys { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string CostCode { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string CostName { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string UOM { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public double Receivable { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public double Payable { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string AreaStart { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string AreaEnd { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string Notes { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string AddUser { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string CostKind { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string ARnoDistribution { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string APnoDistribution { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string MinimumCharge { get; set; }
        /// <summary>
        /// 
        /// </summary>  
        public string Facility { get; set; }
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
        private IEnumerable<BaseCostCode> InternalQueryData(IEnumerable<BaseCostCode> data, out int dataCount)
        {
            //BaseCostCode.js queryParams 調用
            if (!string.IsNullOrEmpty(StorerKeys))
            {
                var storerkeys = StorerKeys.Split(",");
                data = data.Where( d => storerkeys.Contains(d.Storerkey));
            }
            if (!string.IsNullOrEmpty(CostCode))
            {
                data = data.Where(t => t.CostCode.Contains(CostCode));
            }

            dataCount = data.Count();
            switch (Sort)
            {
                case "Storerkey":
                    data = Order == "asc" ? data.OrderBy(t => t.Storerkey) : data.OrderByDescending(t => t.Storerkey);
                    break;
                case "CostCode":
                    data = Order == "asc" ? data.OrderBy(t => t.CostCode) : data.OrderByDescending(t => t.CostCode);
                    break;
                case "CostName":
                    data = Order == "asc" ? data.OrderBy(t => t.CostName) : data.OrderByDescending(t => t.CostName);
                    break;
                case "UOM":
                    data = Order == "asc" ? data.OrderBy(t => t.UOM) : data.OrderByDescending(t => t.UOM);
                    break;
                case "Receivable":
                    data = Order == "asc" ? data.OrderBy(t => t.Receivable) : data.OrderByDescending(t => t.Receivable);
                    break;
                case "Payable":
                    data = Order == "asc" ? data.OrderBy(t => t.Payable) : data.OrderByDescending(t => t.Payable);
                    break;
                case "AreaStart":
                    data = Order == "asc" ? data.OrderBy(t => t.AreaStart) : data.OrderByDescending(t => t.AreaStart);
                    break;
                case "AreaEnd":
                    data = Order == "asc" ? data.OrderBy(t => t.AreaEnd) : data.OrderByDescending(t => t.AreaEnd);
                    break;
                case "Notes":
                    data = Order == "asc" ? data.OrderBy(t => t.Notes) : data.OrderByDescending(t => t.Notes);
                    break;
                case "CostKind":
                    data = Order == "asc" ? data.OrderBy(t => t.CostKind) : data.OrderByDescending(t => t.CostKind);
                    break;    
                case "ARnoDistribution":
                    data = Order == "asc" ? data.OrderBy(t => t.ARnoDistribution) : data.OrderByDescending(t => t.ARnoDistribution);
                    break;
                case "APnoDistribution":
                    data = Order == "asc" ? data.OrderBy(t => t.APnoDistribution) : data.OrderByDescending(t => t.APnoDistribution);
                    break;
                case "MinimumCharge":
                    data = Order == "asc" ? data.OrderBy(t => t.MinimumCharge) : data.OrderByDescending(t => t.MinimumCharge);
                    break;     
                case "Facility":
                    data = Order == "asc" ? data.OrderBy(t => t.Facility) : data.OrderByDescending(t => t.Facility);
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
            var data = InternalQueryData(BaseCostCodeHelper.Retrieves(), out dataCount);        
            var ret = new QueryData<object>();
            ret.total = dataCount;           
            ret.rows = data.Select(u => new
            {
                Id = u.Storerkey + u.CostCode + u.Facility,
                u.Storerkey,
                u.CostCode,
                u.CostName,
                u.UOM,
                u.Receivable,
                u.Payable,
                u.AreaStart,
                u.AreaEnd,
                u.Notes,
                u.CostKind,
                u.ARnoDistribution,
                u.APnoDistribution,
                u.MinimumCharge,
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
        public IEnumerable<BaseCostCode> RetrievesExcel()
        {
            var dataCount = 0;
            var data = InternalQueryData(BaseCostCodeHelper.Retrieves(), out dataCount);
            return data;
        }
    }
}
