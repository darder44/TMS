using Bootstrap.Client.Query;
using Bootstrap.Client.DataAccess;
using Longbow.Web.Mvc;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using Bootstrap.Client.DataAccess.Helper;
using System.Threading.Tasks;
using Microsoft.Extensions.Caching.Memory;
using Bootstrap.Client.Query.ExportExcelBody;

namespace Bootstrap.Client.Controllers.Api
{
    ///<summary>
    ///客戶主檔Controller
    ///</summary>
    [Route("api/[controller]")]
    [ApiController]
    public class BaseCustomerController : ControllerBase
    {
        ///<summary>
        ///
        ///</summary>
        [HttpGet] 
        public QueryData<object> Get([FromQuery]QueryBaseCustomerOption value)
        {
            return value.RetrieveData();
        }

        ///<summary>
        /// 查詢新增主鍵重覆
        ///</summary>
        [HttpGet("{RetrieveByStorerKeyAndConsigneeKey}")]
        public BaseCustomer RetrieveByStorerKeyAndConsigneeKey([FromQuery]string StorerKey, string ConsigneeKey)
        {
            return BaseCustomerHelper.RetrieveByStorerKeyAndConsigneeKey(StorerKey, ConsigneeKey);
        }

        ///<summary>
        ///
        ///</summary>
        [HttpPost("ExportExcel")]
        public string ExportExcel([FromServices] IMemoryCache cache, [FromBody]ExportExcelBody<QueryBaseCustomerOption> value)
        {
            if (value.Sheets.Count() != 1) return "";
            var sheet = value.Sheets.SingleOrDefault();
            value.Query.Sort = sheet.sortName;
            var data = value.Query.RetrievesExcel();            
            var param = new NpoiParam<BaseCustomer>
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

            List<NpoiParam<BaseCustomer>> list = new List<NpoiParam<BaseCustomer>>(){ param };

            return cache.SetDownloadCache(NpoiHelper.ExportExcel(list, value.FileName));
        }

        /// <summary>
        /// 新建/更新
        /// </summary>
        /// <param name="value"></param>
        [HttpPost]
        [ButtonAuthorize(Url = "~/TMS/BaseCustomer", Auth = "add,edit")]
        public bool Save([FromBody]BaseCustomer value)
        {
            value.EditWho = User.Identity.Name;
            value.EditDate = DateTime.Now;
            if (string.IsNullOrEmpty(value.Id))
            {
                value.AddWho = value.EditWho;
                value.Adddate = value.EditDate;
                return BaseCustomerHelper.Save(value);   
            }
            else
            {
                return BaseCustomerHelper.Update(value);        
            }
        }

        /// <summary>
        /// 刪除
        /// </summary>
        /// <param name="values"></param>
        [HttpDelete]
        [ButtonAuthorize(Url = "~/TMS/BaseCustomer", Auth = "del")]
        public bool Delete([FromBody]IEnumerable<BaseCustomer> values)
        {
            return BaseCustomerHelper.Delete(values);
        }
        
    }
}
