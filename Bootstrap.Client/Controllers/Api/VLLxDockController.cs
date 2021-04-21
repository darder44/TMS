using Bootstrap.Client.Query;
using Longbow.Web.Mvc;
using Microsoft.AspNetCore.Mvc;
using Bootstrap.Client.DataAccess;
using Bootstrap.Client.DataAccess.Helper;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Caching.Memory;
using Bootstrap.Client.Query.ExportExcelBody;

namespace Bootstrap.Client.Controllers.Api
{
    ///<summary>
    ///Controller
    ///</summary>
    [Route("api/[controller]")]
    [ApiController]
    public class VLLxDockController : ControllerBase
    {
        /// <summary>
        /// 
        /// </summary>
        [HttpGet]
        public QueryData<object> RetrieveData([FromQuery]QueryVLLxDockOption value)
        {
            return value.RetrieveData(BestHelper.GetFacility(User)); 
        }

        ///<summary>
        ///
        ///</summary>
        [HttpPost("ExportExcel")]
        public string ExportExcel([FromServices] IMemoryCache cache, [FromBody]ExportExcelBody<QueryVLLxDockOption> value)
        {
            if (value.Sheets.Count() != 1) return "";
            var sheet = value.Sheets.SingleOrDefault();
            value.Query.Sort = sheet.sortName;
            var data = value.Query.RetrievesExcel(BestHelper.GetFacility(User));            
            var param = new NpoiParam<VLLxDock>
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

            List<NpoiParam<VLLxDock>> list = new List<NpoiParam<VLLxDock>>(){ param };

            return cache.SetDownloadCache(NpoiHelper.ExportExcel(list, value.FileName));
        }
    }
}
