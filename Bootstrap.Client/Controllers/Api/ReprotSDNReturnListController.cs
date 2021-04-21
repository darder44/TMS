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
    public class ReportSDNReturnListController : ControllerBase
    {
        ///<summary>
        ///
        ///</summary>
        [HttpGet]
        public QueryData<object> RetrieveData([FromQuery] QueryReportSDNReturnListOption value)
        {
            return value.RetrieveData();
        }

        ///<summary>
        ///
        ///</summary>
        [HttpGet]
        public IEnumerable<object> RetrieveDetail([FromQuery] string TMSKey,string ordermethod)
        {
            var data = ReportSDNReturnListHelper.RetrieveDetail(TMSKey,ordermethod);
            var users = UserHelper.Retrieves();
            return data.GroupJoin(users, d => string.IsNullOrEmpty(d.AddWho) ? "" : d.AddWho.ToUpper().Trim(), u => u.UserName.ToUpper().Trim(), (d, u) => new
            {
                data = d,
                user = u
            }).SelectMany(d => d.user.DefaultIfEmpty(), (d, u) => new
            {
                d.data.RouteNo,
                d.data.Sequence,
                d.data.TMSKey,
                d.data.ExternOrderKey,
                d.data.FromFacility,
                d.data.ToFacility,
                d.data.ScheduleDate,
                d.data.StartAddress,
                d.data.Driver,
                d.data.VehicleKey,
                d.data.Company,
                d.data.Status,
                d.data.AddDate,
                d.data.AddWho,
                DisplayName = (u != null) ? u.DisplayName : ""
            });
        }


        ///<summary>
        ///
        ///</summary>
        [HttpPost]
        public string ExportExcel([FromServices] IMemoryCache cache, [FromBody]ExportExcelBody<QueryReportSDNReturnListOption> value)
        {
            if (value.Sheets.Count() != 2) return "";
            List<NpoiParam<ReportSDNReturnList>> list = new List<NpoiParam<ReportSDNReturnList>>();
            var users = UserHelper.Retrieves();
            value.Query.Sort = value.Sheets.FirstOrDefault()?.sortName ?? "";
            var header = value.Query.RetrievesExcel();
            header.All(d => 
            {
                var user = users.Where(u => string.IsNullOrEmpty(d.SdnSendWho) ? false : d.SdnSendWho.ToUpper().Trim().Equals(u.UserName.ToUpper().Trim())).SingleOrDefault();
                d.SdnSendWho = user == null ? "" : $"{user.DisplayName}({d.SdnSendWho})";
                return true;
            });
            var tmskeys = string.Join(",", header.Select( t => string.Format("'{0}'", t.TMSKey) ).Distinct());
            value.Query.Sort = value.Sheets.LastOrDefault()?.sortName ?? "";
            var detail = value.Query.RetrievesExcelDetail(tmskeys);
            detail.All(d => 
            {
                var user = users.Where(u => string.IsNullOrEmpty(d.SdnSendWho) ? false : d.SdnSendWho.ToUpper().Trim().Equals(u.UserName.ToUpper().Trim())).SingleOrDefault();
                d.SdnSendWho = user == null ? "" : $"{user.DisplayName}({d.SdnSendWho})";

                user = users.Where(u => string.IsNullOrEmpty(d.AddWho) ? false : d.AddWho.ToUpper().Trim().Equals(u.UserName.ToUpper().Trim())).SingleOrDefault();
                d.AddWho = user == null ? "" : $"{user.DisplayName}({d.AddWho})";
                return true;
            });
            foreach (var sheet in value.Sheets)
            {
                var param = new NpoiParam<ReportSDNReturnList>
                {                
                    Data = list.Count == 0 ? header : detail, //資料               
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
