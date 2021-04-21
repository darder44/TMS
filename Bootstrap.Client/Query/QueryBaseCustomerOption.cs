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
    public class QueryBaseCustomerOption : CustomPaginationOption
    {
        /// <summary>
        /// 
        /// </summary>
        public string StorerKeys { get; set; }

        /// <summary>
        /// 
        /// </summary>    
        public string ConsigneeKey { get; set; }

        /// <summary>
        /// 
        /// </summary>  
        public string AreaCode { get; set; }

        /// <summary>
        /// 
        /// </summary>  
        public string Zip { get; set; }

        /// <summary>
        /// 
        /// </summary>  
        public string FullName { get; set; }

        /// <summary>
        /// 
        /// </summary>  
        public string ShortName { get; set; }

        /// <summary>
        /// 
        /// </summary>  
        public string Contact { get; set; }

        /// <summary>
        /// 
        /// </summary>  
        public string Phone { get; set; }
        
        /// <summary>
        /// 
        /// </summary>  
        public string ShipToAddress { get; set; }

        /// <summary>
        /// 
        /// </summary>  
        public string VehicleType { get; set; }

        /// <summary>
        /// 
        /// </summary>  
        public string DemandCode1 { get; set; }

        /// <summary>
        /// 
        /// </summary>  
        public string DemandCode2 { get; set; }

        /// <summary>
        /// 
        /// </summary>  
        public string ChannelType { get; set; }

        /// <summary>
        /// 
        /// </summary>  
        public string PickTool { get; set; }

        /// <summary>
        /// 
        /// </summary>  
        public string Channel { get; set; }

        /// <summary>
        /// 
        /// </summary>  
        public int? CodeDate1 { get; set; }

        /// <summary>
        /// 
        /// </summary>  
        public int? CodeDate2 { get; set; }

        /// <summary>
        /// 
        /// </summary>  
        public string Notes { get; set; }

        /// <summary>
        /// 
        /// </summary>  
        public string Fax { get; set; }

        /// <summary>
        /// 
        /// </summary>  
        public string PalletType { get; set; }

        /// <summary>
        /// 
        /// </summary>  
        public string PalletSpec { get; set; }

        /// <summary>
        /// 
        /// </summary>  
        public string Penalties { get; set; }

        /// <summary>
        /// 
        /// </summary>  
        public string DC { get; set; }

        /// <summary>
        /// 
        /// </summary>  
        public string UpdateSource { get; set; }

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
        public string CustGroup { get; set; }

        /// <summary>
        /// 
        /// </summary>  
        public string CustomerType { get; set; }

        /// <summary>
        /// 
        /// </summary>  
        public string InvoiceAddress { get; set; }

        /// <summary>
        /// 
        /// </summary>  
        public string OrderGroup { get; set; }

        /// <summary>
        /// 
        /// </summary>  
        public string CustomerEAN { get; set; }

        /// <summary>
        /// 
        /// </summary>  
        public string SalesOffice { get; set; }

        /// <summary>
        /// 
        /// </summary>  
        public string SoldTo { get; set; }
        /// <summary>
        /// 
        /// </summary>  
        public string SoldToName { get; set; }
        /// <summary>
        /// 
        /// </summary>  
        public string SoldToAddress { get; set; }

        /// <summary>
        /// 
        /// </summary>  
        public string ShipTo { get; set; }
        /// <summary>
        /// 
        /// </summary>  
        public string ShipToName { get; set; }

        /// <summary>
        /// 
        /// </summary>  
        public string SN { get; set; }

        /// <summary>
        /// 
        /// </summary>  
        public string PaperSize { get; set; }

        /// <summary>
        /// 查詢欄位
        /// </summary>
        private IEnumerable<BaseCustomer> InternalQueryData(IEnumerable<BaseCustomer> data, out int dataCount)
        {
            //basecustomer.js queryParams 調用
            if (!string.IsNullOrEmpty(StorerKeys))
            {
                var storerkeys = StorerKeys.Split(",");
                data = data.Where( d => storerkeys.Contains(d.StorerKey));
            }
            if (!string.IsNullOrEmpty(ConsigneeKey))
            {
                data = data.Where(t => t.ConsigneeKey.Contains(ConsigneeKey));
            }
            if (!string.IsNullOrEmpty(FullName))
            {
                data = data.Where(t => t.FullName.Contains(FullName));
            }

            dataCount = data.Count();

            switch (Sort)
            {
                case "StorerKey":
                    data = Order == "asc" ? data.OrderBy(t => t.StorerKey) : data.OrderByDescending(t => t.StorerKey);
                    break;
                case "ConsigneeKey":
                    data = Order == "asc" ? data.OrderBy(t => t.ConsigneeKey) : data.OrderByDescending(t => t.ConsigneeKey);
                    break;
                case "AreaCode":
                    data = Order == "asc" ? data.OrderBy(t => t.AreaCode) : data.OrderByDescending(t => t.AreaCode);
                    break;
                case "Zip":
                    data = Order == "asc" ? data.OrderBy(t => t.Zip) : data.OrderByDescending(t => t.Zip);
                    break;
                case "FullName":
                    data = Order == "asc" ? data.OrderBy(t => t.FullName) : data.OrderByDescending(t => t.FullName);
                    break;
                case "ShortName":
                    data = Order == "asc" ? data.OrderBy(t => t.ShortName) : data.OrderByDescending(t => t.ShortName);
                    break;
                case "Contact":
                    data = Order == "asc" ? data.OrderBy(t => t.Contact) : data.OrderByDescending(t => t.Contact);
                    break;
                case "Phone":
                    data = Order == "asc" ? data.OrderBy(t => t.Phone) : data.OrderByDescending(t => t.Phone);
                    break;
                case "ShipToAddress":
                    data = Order == "asc" ? data.OrderBy(t => t.ShipToAddress) : data.OrderByDescending(t => t.ShipToAddress);
                    break;
                case "VehicleType":
                    data = Order == "asc" ? data.OrderBy(t => t.VehicleType) : data.OrderByDescending(t => t.VehicleType);
                    break;
                case "DemandCode1":
                    data = Order == "asc" ? data.OrderBy(t => t.DemandCode1) : data.OrderByDescending(t => t.DemandCode1);
                    break;    
                case "DemandCode2":
                    data = Order == "asc" ? data.OrderBy(t => t.DemandCode2) : data.OrderByDescending(t => t.DemandCode2);
                    break;
                case "ChannelType":
                    data = Order == "asc" ? data.OrderBy(t => t.ChannelType) : data.OrderByDescending(t => t.ChannelType);
                    break;
                case "PickTool":
                    data = Order == "asc" ? data.OrderBy(t => t.PickTool) : data.OrderByDescending(t => t.PickTool);
                    break;
                case "Channel":
                    data = Order == "asc" ? data.OrderBy(t => t.Channel) : data.OrderByDescending(t => t.Channel);
                    break;
                case "CodeDate1":
                    data = Order == "asc" ? data.OrderBy(t => t.CodeDate1) : data.OrderByDescending(t => t.CodeDate1);
                    break;
                case "CodeDate2":
                    data = Order == "asc" ? data.OrderBy(t => t.CodeDate2) : data.OrderByDescending(t => t.CodeDate2);
                    break;      
                case "Notes":
                    data = Order == "asc" ? data.OrderBy(t => t.Notes) : data.OrderByDescending(t => t.Notes);
                    break; 
                case "Fax":
                    data = Order == "asc" ? data.OrderBy(t => t.Fax) : data.OrderByDescending(t => t.Fax);
                    break; 
                case "PalletType":
                    data = Order == "asc" ? data.OrderBy(t => t.PalletType) : data.OrderByDescending(t => t.PalletType);
                    break; 
                case "PalletSpec":
                    data = Order == "asc" ? data.OrderBy(t => t.PalletSpec) : data.OrderByDescending(t => t.PalletSpec);
                    break;     
                case "Penalties":
                    data = Order == "asc" ? data.OrderBy(t => t.Penalties) : data.OrderByDescending(t => t.Penalties);
                    break;   
                case "DC":
                    data = Order == "asc" ? data.OrderBy(t => t.DC) : data.OrderByDescending(t => t.DC);
                    break;   
                case "UpdateSource":
                    data = Order == "asc" ? data.OrderBy(t => t.UpdateSource) : data.OrderByDescending(t => t.UpdateSource);
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
                case "CustGroup":
                    data = Order == "asc" ? data.OrderBy(t => t.CustGroup) : data.OrderByDescending(t => t.CustGroup);
                    break;  
                case "CustomerType":
                    data = Order == "asc" ? data.OrderBy(t => t.CustomerType) : data.OrderByDescending(t => t.CustomerType);
                    break;  
                case "InvoiceAddress":
                    data = Order == "asc" ? data.OrderBy(t => t.InvoiceAddress) : data.OrderByDescending(t => t.InvoiceAddress);
                    break;  
                case "OrderGroup":
                    data = Order == "asc" ? data.OrderBy(t => t.OrderGroup) : data.OrderByDescending(t => t.OrderGroup);
                    break;  
                case "CustomerEAN":
                    data = Order == "asc" ? data.OrderBy(t => t.CustomerEAN) : data.OrderByDescending(t => t.CustomerEAN);
                    break;  
                case "SalesOffice":
                    data = Order == "asc" ? data.OrderBy(t => t.SalesOffice) : data.OrderByDescending(t => t.SalesOffice);
                    break;                      
                case "SoldTo":
                    data = Order == "asc" ? data.OrderBy(t => t.SoldTo) : data.OrderByDescending(t => t.SoldTo);
                    break;
                case "SoldToName":
                    data = Order == "asc" ? data.OrderBy(t => t.SoldToName) : data.OrderByDescending(t => t.SoldToName);
                    break;  
                case "SoldToAddress":
                    data = Order == "asc" ? data.OrderBy(t => t.SoldToAddress) : data.OrderByDescending(t => t.SoldToAddress);
                    break;  
                case "ShipTo":
                    data = Order == "asc" ? data.OrderBy(t => t.ShipTo) : data.OrderByDescending(t => t.ShipTo);
                    break;  
                case "ShipToName":
                    data = Order == "asc" ? data.OrderBy(t => t.ShipToName) : data.OrderByDescending(t => t.ShipToName);
                    break; 
                case "SN":
                    data = Order == "asc" ? data.OrderBy(t => t.SN) : data.OrderByDescending(t => t.SN);
                    break;  
                case "PaperSize":
                    data = Order == "asc" ? data.OrderBy(t => t.PaperSize) : data.OrderByDescending(t => t.PaperSize);
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
            var data = InternalQueryData(BaseCustomerHelper.Retrieves(), out dataCount);
            //basecustomer.js queryParams 調用
            var ret = new QueryData<object>();
            ret.total = dataCount;

            ret.rows = data.Select(u => new
            {
                Id = u.StorerKey + u.ConsigneeKey,
                u.StorerKey,
                u.ConsigneeKey,
                u.AreaCode,
                u.Zip,
                u.FullName,
                u.ShortName,
                u.Contact,
                u.Phone,
                u.ShipToAddress,
                u.VehicleType,
                u.DemandCode1,
                u.DemandCode2,
                u.ChannelType,
                u.PickTool,
                u.Channel,
                u.CodeDate1,
                u.CodeDate2,
                u.Notes,
                u.Fax,
                u.PalletType,
                u.PalletSpec,
                u.Penalties,
                u.DC,
                u.UpdateSource,
                u.AddWho,
                u.Adddate,
                u.EditWho,
                u.EditDate,
                u.CustGroup,
                u.CustomerType,
                u.InvoiceAddress,
                u.OrderGroup,
                u.CustomerEAN,
                u.SalesOffice,
                u.SoldTo,
                u.SoldToName,
                u.SoldToAddress,
                u.ShipTo,
                u.ShipToName,
                u.SN,
                u.PaperSize,
            });
            return ret;
        }

        /// <summary>
        /// 
        /// </summary>  
        public IEnumerable<BaseCustomer> RetrievesExcel()
        {
            var dataCount = 0;
            var data = InternalQueryData(BaseCustomerHelper.Retrieves(), out dataCount);
            return data;
        }
    }
}
