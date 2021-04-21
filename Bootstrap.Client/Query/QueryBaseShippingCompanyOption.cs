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
    public class QueryBaseShippingCompanyOption : CustomPaginationOption
    {
        /// <summary>
        /// 公司代碼
        /// </summary>
        public string CompanyKey { get; set; }
        /// <summary>
        /// 中文名稱
        /// </summary>
        public string FullName { get; set; }
        /// <summary>
        /// 英文名稱
        /// </summary>
        public string EngName { get; set; }
        /// <summary>
        /// 簡稱
        /// </summary>
        public string ShortName { get; set; }
        /// <summary>
        /// 電話
        /// </summary>
        public string Phone { get; set; }
        /// <summary>
        /// 聯絡人
        /// </summary>
        public string Contact { get; set; }
        /// <summary>
        /// 地址
        /// </summary>
        public string Address { get; set; }
        /// <summary>
        /// 說明
        /// </summary>
        public string Description { get; set; }
        /// <summary>
        /// 新增
        /// </summary>
        public string AddWho { get; set; }
        /// <summary>
        /// 新增日期
        /// </summary>
        public DateTime? AddDate { get; set; }
        /// <summary>
        /// 編輯
        /// </summary>
        public string EditWho { get; set; }
        /// <summary>
        /// 編輯日期
        /// </summary>
        public DateTime? EditDate { get; set; }

        /// <summary>
        /// 查詢欄位
        /// </summary>
        private IEnumerable<BaseShippingCompany> InternalQueryData(IEnumerable<BaseShippingCompany> data, out int dataCount)
        {
            //baseshippingcompany.js queryParams 調用
            if (!string.IsNullOrEmpty(CompanyKey))
            {
                data = data.Where(t => t.CompanyKey.Contains(CompanyKey));
            }
            if (!string.IsNullOrEmpty(FullName))
            {
                data = data.Where(t => t.FullName.Contains(FullName));
            }
            if (!string.IsNullOrEmpty(Contact))
            {
                data = data.Where(t => t.Contact.Contains(Contact));
            }
            if (!string.IsNullOrEmpty(Phone))
            {
                data = data.Where(t => t.Phone.Contains(Phone));
            }

            dataCount = data.Count();

            switch (Sort)
            {
                case "CompanyKey":
                    data = Order == "asc" ? data.OrderBy(t => t.CompanyKey) : data.OrderByDescending(t => t.CompanyKey);
                    break;
                case "FullName":
                    data = Order == "asc" ? data.OrderBy(t => t.FullName) : data.OrderByDescending(t => t.FullName);
                    break;
                case "EngName":
                    data = Order == "asc" ? data.OrderBy(t => t.EngName) : data.OrderByDescending(t => t.EngName);
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
            var data = InternalQueryData(BaseShippingCompanyHelper.Retrieves(), out dataCount);            
            var ret = new QueryData<object>();
            ret.total = dataCount;            
            ret.rows = data.Select(u => new
            {
                Id = u.CompanyKey,
                u.CompanyKey,
                u.FullName,
                u.EngName,
                u.ShortName,
                u.Phone,
                u.Contact,
                u.Address,
                u.Description,
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
        public IEnumerable<BaseShippingCompany> RetrievesExcel()
        {
            var dataCount = 0;
            var data = InternalQueryData(BaseShippingCompanyHelper.Retrieves(), out dataCount);
            return data;
        }
    }
}
