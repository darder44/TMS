using Bootstrap.Client.DataAccess;
using Bootstrap.Client.DataAccess.Helper;
using Bootstrap.Security;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Bootstrap.Client.Models
{
    /// <summary>
    /// 自定義modal
    /// </summary>
    public class NormalModel : NavigatorBarModel
    {

        /// <summary>
        /// 自定義
        /// </summary>
        public NormalModel(ControllerBase controller) : base(controller)
        {
            StorerKeys = BaseStorerHelper.Retrieves().Select(d => new KeyValuePair<string, string>(d.ChineseName, d.StorerKey)).OrderBy(g => g.Key); 
            //StorerKeys = Base_StorerkeyHelper.Retrieves().Where( sk => sk.type == "1").Select(d => new KeyValuePair<string, string>(d.Company, d.StorerKey.Trim())).OrderBy(g => g.Key);            
            VehicleTypes = BaseVehicleTypeHelper.Retrieves().Select(d => new KeyValuePair<string, string>(d.Description, d.VehicleType)).OrderBy(g => g.Key);
            Companies = BaseShippingCompanyHelper.Retrieves().Select(d => new KeyValuePair<string, string>(d.FullName, d.CompanyKey)).OrderBy(g => g.Key); 
            var Basedics = BaseDictionaryHelper.Retrieves();  
            Roles = RoleHelper.RetrievesByUserName(controller.User.Identity.Name);                        
            BestAdmin = Roles.Where( r => Basedics.Where(d => d.CodeName == "BestAdmin").Any( d => d.Code.Contains(r))).Count() > 0;
            OrderTypes = Basedics.Where( d => d.CodeName == "OrderType").Select(d => new KeyValuePair<string, string>(d.Description, d.Code)).OrderBy(g => g.Key);
            OrderStatus = Basedics.Where( d => d.CodeName == "OrderStatus").Select(d => new KeyValuePair<string, string>(d.Description, d.Code)).OrderBy(g => g.Value);            
            Facilities = BaseFacilityHelper.Retrieves().Select(d => new KeyValuePair<string, string>(d.Description, d.Facility)).OrderBy(g => g.Key);
            AreaCodes = Basedics.Where( d => d.CodeName == "AreaCode").Select(d => new KeyValuePair<string, string>(d.Description, d.Code)).OrderBy(g => g.Key);
            ExtraDemandCodes = Basedics.Where( d => d.CodeName == "ExtraDemandCode").Select(d => new KeyValuePair<string, string>(d.Description, d.Code)).OrderBy(g => g.Value);                     
            RSCCode = Basedics.Where( d => d.CodeName == "RSCCode").Select(d => new KeyValuePair<string, string>(d.Description, d.Code)).OrderBy(g => g.Value);
            RBCCode = Basedics.Where( d => d.CodeName == "RBCCode").Select(d => new KeyValuePair<string, string>(d.Description, d.Code)).OrderBy(g => g.Value);
            DeliveryAbNormalCode = Basedics.Where( d => d.CodeName == "DeliveryAbNormalCode").Select(d => new KeyValuePair<string, string>(d.Description, d.Code)).OrderBy(g => g.Key);
        }
        /// <summary>
        /// 是否為管理階級
        /// </summary>
        public bool BestAdmin {get; protected set;}
        /// <summary>
        /// 使用者角色
        /// </summary>
        public IEnumerable<string> Roles { get; private set; }
        /// <summary>
        /// 貨主
        /// </summary>
        public IEnumerable<KeyValuePair<string, string>> StorerKeys { get; private set; }
        /// <summary>
        /// 訂單類別
        /// </summary>
        public IEnumerable<KeyValuePair<string, string>> OrderTypes { get; private set; }
        /// <summary>
        /// 訂單狀態
        /// </summary>
        public IEnumerable<KeyValuePair<string, string>> OrderStatus { get; private set; }
        /// <summary>
        /// 區域代碼
        /// </summary>
        public IEnumerable<KeyValuePair<string, string>> AreaCodes { get; private set; }
        /// <summary>
        /// 異常原因
        /// </summary>
        public IEnumerable<KeyValuePair<string, string>> RSCCode { get; private set; }
        /// <summary>
        /// 出車異常原因
        /// </summary>
        public IEnumerable<KeyValuePair<string, string>> DeliveryAbNormalCode { get; private set; }
        /// <summary>
        /// 責任歸屬
        /// </summary>
        public IEnumerable<KeyValuePair<string, string>> RBCCode { get; private set; }
        /// <summary>
        /// 倉別
        /// </summary>
        public IEnumerable<KeyValuePair<string, string>> Facilities { get; private set; }
        /// <summary>
        /// 特殊需求
        /// </summary>
        public IEnumerable<KeyValuePair<string, string>> ExtraDemandCodes { get; private set; }
        /// <summary>
        /// 車種代碼
        /// </summary>
        public IEnumerable<KeyValuePair<string, string>> VehicleTypes { get; private set; }     

        /// <summary>
        /// 貨運公司
        /// </summary>
        public IEnumerable<KeyValuePair<string, string>> Companies { get; private set; }     
        
    }

    
}
