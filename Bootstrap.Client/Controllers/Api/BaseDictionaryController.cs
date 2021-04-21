using Bootstrap.Client.Query;
using Bootstrap.Client.DataAccess;
using Longbow.Web.Mvc;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using Bootstrap.Client.DataAccess.Helper;
using Microsoft.Extensions.Caching.Memory;
using Bootstrap.Client.Query.ExportExcelBody;

namespace Bootstrap.Client.Controllers.Api
{
    ///<summary>
    ///客戶主檔Controller
    ///</summary>
    [Route("api/[controller]")]
    [ApiController]
    public class BaseDictionaryController : ControllerBase
    {
        ///<summary>
        ///
        ///</summary>
        [HttpGet] 
        public QueryData<object> Get([FromQuery]QueryBaseDictionaryOption value)
        {
            return value.RetrieveData();
        }

        /// <summary>
        /// 根據CodeName取得Code
        /// </summary>
        [HttpGet("{CodeName}")]
        public IEnumerable<BaseDictionary> RetrieveCodes([FromQuery] string CodeName)
        {
            return BaseDictionaryHelper.Retrieves().Where( d => d.CodeName == CodeName).OrderBy( d => d.Code);
        }

        /// <summary>
        /// 新建/更新
        /// </summary>
        /// <param name="value"></param>
        [HttpPost]
        [ButtonAuthorize(Url = "~/TMS/BaseDictionary", Auth = "add,edit")]
        public bool Save([FromBody]BaseDictionary value)
        {
            value.EditWho = User.Identity.Name;
            value.EditDate = DateTime.Now;            
            if (string.IsNullOrEmpty(value.Id))
            {
                value.AddWho = value.EditWho;
                value.AddDate = value.EditDate;
                return BaseDictionaryHelper.Save(value);   
            }
            else
            {
                return BaseDictionaryHelper.Update(value);        
            }
        }

        /// <summary>
        /// 刪除
        /// </summary>
        /// <param name="values"></param>
        [HttpDelete]
        [ButtonAuthorize(Url = "~/TMS/BaseDictionary", Auth = "del")]
        public bool Delete([FromBody]IEnumerable<BaseDictionary> values)
        {
            return BaseDictionaryHelper.Delete(values);
        }

        /// <summary>
        /// 
        /// </summary>        
        [HttpPost("ExportExcel")]
        public string ExportExcel([FromServices] IMemoryCache cache, [FromBody]ExportExcelBody<QueryBaseDictionaryOption> value)
        {
            if (value.Sheets.Count() != 1) return "";
            var sheet = value.Sheets.SingleOrDefault();
            value.Query.Sort = sheet.sortName;
            var data = value.Query.RetrievesExcel();            
            var param = new NpoiParam<BaseDictionary>
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

            List<NpoiParam<BaseDictionary>> list = new List<NpoiParam<BaseDictionary>>(){ param };

            return cache.SetDownloadCache(NpoiHelper.ExportExcel(list, value.FileName));
        }
        
    }
}
