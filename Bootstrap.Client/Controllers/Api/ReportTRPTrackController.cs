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
    /// <summary>
    /// ReportTRPTrack
    /// </summary>
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class ReportTRPTrackController : ControllerBase
    {
        /// <summary>
        /// 
        /// </summary>
        [HttpGet]
        public QueryData<object> RetrieveData([FromQuery]QueryReportTRPTrackOption value)
        {
            return value.RetrieveData(); 
        }

        ///<summary>
        ///
        ///</summary>
        [HttpPost]
        public string ExportExcel([FromServices] IMemoryCache cache, [FromBody]ExportExcelBody<QueryReportTRPTrackOption> value)
        {
            if (value.Sheets.Count() != 2) return "";
            List<NpoiParam<ReportTRPTrack>> list = new List<NpoiParam<ReportTRPTrack>>();
            var i = 0;
            foreach (var sheet in value.Sheets)
            {
                value.Query.Sort = sheet.sortName;
                var data = value.Query.RetrievesExcel(i != 0);
                i ++;
                var param = new NpoiParam<ReportTRPTrack>
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
                list.Add(param);
            }
            
            return cache.SetDownloadCache(NpoiHelper.ExportExcel(list, value.FileName));
        }
    }
}
