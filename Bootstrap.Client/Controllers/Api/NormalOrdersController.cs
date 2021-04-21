using Bootstrap.Client.Query;
using Bootstrap.Client.DataAccess;
using Longbow.Web.Mvc;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Bootstrap.Client.DataAccess.Helper;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Caching.Memory;
using Bootstrap.Client.Query.ExportExcelBody;
using Microsoft.AspNetCore.Http;

namespace Bootstrap.Client.Controllers.Api
{
    /// <summary>
    /// 訂單維護Controller
    /// </summary>
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class NormalOrdersController : ControllerBase
    {
        ///<summary>
        ///
        ///</summary>
        [HttpGet]
        public QueryData<object> Retrieves([FromQuery]QueryNormalOrdersOption value)
        {
            return value.Retrieves(BestHelper.GetFacility(User)); 
        }

        ///<summary>
        ///
        ///</summary>
        [HttpGet]
        public IEnumerable<NormalOrders> RetrievesExcel([FromQuery]QueryNormalOrdersOption value)
        {
            return value.RetrievesExcel(BestHelper.GetFacility(User)); 
        }

        ///<summary>
        ///
        ///</summary>
        [HttpGet]
        public IEnumerable<NormalOrders> RetrieveDetailByTMSKey([FromQuery]string TMSKey)
        {
            return NormalOrdersHelper.RetrieveDetailByTMSKey(TMSKey, BestHelper.GetFacility(User)); 
        }

        ///<summary>
        ///
        ///</summary>
        [HttpGet]
        public NormalOrders RetrieveSkuByStorerKey([FromQuery]string sku, string storerkey)
        {
            return NormalOrdersHelper.RetrieveSkuByStorerKey(sku, storerkey, BestHelper.GetFacility(User));
        }

        ///<summary>
        ///
        ///</summary>
        [HttpGet]
        public NormalOrders RetrieveConsigneeKeyByStorerKeyAndConsigneeKey([FromQuery]string ConsigneeKey, string storerkey)
        {
            return NormalOrdersHelper.RetrieveConsigneeKeyByStorerKeyAndConsigneeKey(ConsigneeKey, storerkey, BestHelper.GetFacility(User));
        }

        ///<summary>
        ///
        ///</summary>
        [HttpGet]
        public NormalOrders RetrieveExternOrderKey([FromQuery]string ExternOrderKey, string storerkey)
        {
            return NormalOrdersHelper.RetrieveExternOrderKey(ExternOrderKey, storerkey);
        }

        ///<summary>
        /// ASN轉RC訂單
        ///</summary>
        [HttpGet]
        public ServerResponse ASNtoRC([FromQuery]string asnkeys, string storerkey, string consigneekey)
        {
            var user = UserHelper.Retrieves().FirstOrDefault(u => u.UserName.ToLowerInvariant() == User.Identity.Name.ToLowerInvariant());
            var warehouse = BestHelper.GetWareHouse(BestHelper.GetGroupCode(user.Id));
            var facility = BestHelper.GetFacility(User);
            var asnkey = string.IsNullOrEmpty(asnkeys) ? "" : string.Join(",", asnkeys.Split(",").Select(p => string.Format("'{0}'", p)).ToArray());
            return NormalOrdersHelper.ASNtoRC(asnkey, storerkey, consigneekey, warehouse, facility, User.Identity.Name);
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="value"></param>
        [HttpPost]
        [ButtonAuthorize(Url = "~/TMS/NormalOrders", Auth = "add,edit")]
        public ServerResponse SaveHeaderDetail([FromBody] NormalOrdersHelper.SaveOrderDetail value)
        {
            return NormalOrdersHelper.SaveHeaderDetail(value, User.Identity.Name,BestHelper.GetFacility(User));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="values"></param>
        [HttpDelete]
        public ServerResponse DeleteOrders([FromBody]IEnumerable<string> values)
        {
            return NormalOrdersHelper.DeleteOrders(values, User.Identity.Name);
        }

        ///<summary>
        ///
        ///</summary>
        [HttpPost]
        public string ExportExcel([FromServices] IMemoryCache cache, [FromBody]ExportExcelBody<QueryNormalOrdersOption> value)
        {
            if (value.Sheets.Count() != 1) return "";            
            var sheet = value.Sheets.SingleOrDefault();
            value.Query.Sort = sheet.sortName;
            var data = value.Query.RetrievesExcel(BestHelper.GetFacility(User));
            var param = new NpoiParam<NormalOrders>
            {                
                Data = data,                              //資料               
                SheetName = sheet.SheetName,              //Sheet要叫什麼名子
                ColumnMapping = sheet.Columns,            //欄位對應 (處理Excel欄名、格式轉換) 
                /*       
                FontStyle = new FontStyle                 //是否需自定Excel字型大小
                {
                    FontName = "Calibri",
                    FontHeightInPoints = 11,
                },
                */
                ShowHeader = true,                        //是否畫表頭
                IsAutoFit = true,                         //是否啟用自動欄寬
            };

            List<NpoiParam<NormalOrders>> list = new List<NpoiParam<NormalOrders>>(){ param };

            return cache.SetDownloadCache(NpoiHelper.ExportExcel(list, value.FileName));
        }
    }
}
