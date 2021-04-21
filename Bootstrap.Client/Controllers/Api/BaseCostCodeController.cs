using Bootstrap.Client.Query;
using Bootstrap.Client.DataAccess;
using Longbow.Web.Mvc;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using Bootstrap.Client.DataAccess.Helper;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Caching.Memory;
using Bootstrap.Client.Query.ExportExcelBody;

namespace Bootstrap.Admin.Controllers.Api
{
    ///<summary>
    ///計費代碼Controller
    ///</summary>
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class BaseCostCodeController : ControllerBase
    {
        ///<summary>
        ///
        ///</summary>
        [HttpGet] 
        public QueryData<object> Get([FromQuery]QueryBaseCostCodeOption value)
        {
            return value.RetrieveData();
        }
        ///<summary>
        /// 查詢新增主鍵重覆
        ///</summary>
        // [HttpGet("{RetrieveByStorerkeyAndCostCode}")]
        public IEnumerable<BaseCostCode> RetrieveByStorerkeyAndCostCode([FromQuery]string Storerkey, string CostCode)
        {
            return BaseCostCodeHelper.RetrieveByStorerkeyAndCostCode(Storerkey, CostCode);
        }
        ///<summary>
        /// 查詢新增主鍵重覆
        ///</summary>
        public IEnumerable<BaseCostCode> RetrieveCostCodeByStorerkeyAndCostKind([FromQuery]string Storerkey, string CostKind)
        {
            return BaseCostCodeHelper.RetrieveCostCodeByStorerkeyAndCostKind(Storerkey, CostKind);
        }

        ///<summary>
        /// 查詢新增主鍵重覆
        ///</summary>
        // [HttpGet("{RetrieveByStorerkeyAndCostKind}")]
        public IEnumerable<BaseCostCode> RetrieveByStorerkeyAndCostKind([FromQuery]string Storerkey)
        {
            return BaseCostCodeHelper.RetrieveByStorerkeyAndCostKind(Storerkey);
        }
        
        /// <summary>
        /// 新建/更新
        /// </summary>
        /// <param name="value"></param>
        [HttpPost]
        [ButtonAuthorize(Url = "~/TMS/BaseCostCode", Auth = "add,edit")]
        public bool Save([FromBody]BaseCostCode value)
        {
            value.EditWho = User.Identity.Name;
            value.EditDate = DateTime.Now;            
            if (string.IsNullOrEmpty(value.Id))
            {
                value.AddWho = value.EditWho;
                value.AddDate = value.EditDate;     
                return BaseCostCodeHelper.Save(value);   
            }
            else
            {
                return BaseCostCodeHelper.Update(value);        
            }
        }    
        
        /// <summary>
        /// 刪除
        /// </summary>
        [HttpDelete]
        [ButtonAuthorize(Url = "~/TMS/BaseCostCode", Auth = "del")]
        public bool Delete([FromBody]IEnumerable<BaseCostCode> values)
        {
            return BaseCostCodeHelper.Delete(values);
        }

        ///<summary>
        ///
        ///</summary>
        [HttpPost]
        public string ExportExcel([FromServices] IMemoryCache cache, [FromBody]ExportExcelBody<QueryBaseCostCodeOption> value)
        {
            if (value.Sheets.Count() != 1) return "";
            var sheet = value.Sheets.SingleOrDefault();
            value.Query.Sort = sheet.sortName;
            var data = value.Query.RetrievesExcel();            
            var param = new NpoiParam<BaseCostCode>
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

            List<NpoiParam<BaseCostCode>> list = new List<NpoiParam<BaseCostCode>>(){ param };

            return cache.SetDownloadCache(NpoiHelper.ExportExcel(list, value.FileName));
        }

    }
}
