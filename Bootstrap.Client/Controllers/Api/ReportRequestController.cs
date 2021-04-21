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
using Microsoft.AspNetCore.Mvc.Filters;

namespace Bootstrap.Client.Controllers.Api
{
    ///<summary>
    ///Controller
    ///</summary>
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class ReportRequestController : ControllerBase
    {
        ///<summary>
        ///
        ///</summary>
        [HttpGet]
        public QueryData<object> RetrieveData([FromQuery] QueryReportRequestOption value)
        {
            return value.RetrieveData();
        }


        ///<summary>
        ///
        ///</summary>
        [HttpPost]
        public string ExportExcel([FromServices] IMemoryCache cache, [FromBody]ExportExcelBody<QueryReportRequestOption> value)
        {
            if (value.Sheets.Count() != 4) return "";
            List<NpoiParam<ReportRequest>> list = new List<NpoiParam<ReportRequest>>();
            value.Query.Sort = value.Sheets.ToList()[0].sortName;
            var CostDetail = value.Query.RetrievesExcel("");
            value.Query.Sort = value.Sheets.ToList()[1].sortName;
            var ReceivableDetail = value.Query.RetrievesExcel("ReceivableDetail");
            value.Query.Sort = value.Sheets.ToList()[2].sortName;
            var RPDetail = value.Query.RetrievesExcel("RPDetail");
            value.Query.Sort = value.Sheets.ToList()[3].sortName;
            var DailyReport = value.Query.RetrievesExcel("DailyReport");
            
            int loop = 1;
            foreach (var sheet in value.Sheets)
            {
                var param = new NpoiParam<ReportRequest>
                {
                    Data = (loop == 1) ? CostDetail : (loop == 2) ? ReceivableDetail : (loop == 3) ? RPDetail : DailyReport,
                    SheetName = sheet.SheetName,              //Sheet要叫什麼名子
                    ColumnMapping = sheet.Columns,            //欄位對應 (處理Excel欄名、格式轉換)
                    ShowHeader = true,                        //是否畫表頭
                    IsAutoFit = true,                         //是否啟用自動欄寬
                };
                list.Add(param);
                loop ++;
            }
            
            return cache.SetDownloadCache(NpoiHelper.ExportExcel(list, value.FileName));
        }
    }
}
