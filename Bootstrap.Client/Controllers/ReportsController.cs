using System.Collections.Generic;
using System.Linq;
using Bootstrap.Client.Models;
using Microsoft.AspNetCore.Mvc;
using Stimulsoft.Report.Mvc;
using Stimulsoft.Report;
using Stimulsoft.Report.Web;
using Bootstrap.Client.DataAccess.Helper;
using Bootstrap.Client.DataAccess.ShipListReport.Helper;
using Bootstrap.Security.Mvc;
using System.IO;

namespace Bootstrap.Client.Controllers
{
    /// <summary>
    /// 報表controller 
    /// </summary>
    public class ReportsController : Controller
    {
        static ReportsController()
        {
            //取得序號啟動Stimulsoft
            Stimulsoft.Base.StiLicense.Key = BootstrapAppContext.Configuration["StiLicense"];
        }

        private bool initialed { get; set; } = false;

        /// <summary>
        /// 
        /// </summary>
        private void InitialFonts()
        {
            if (initialed) return;
            var path = StiNetCoreHelper.MapPath(this, "Fonts");
            DirectoryInfo dyInfo = new DirectoryInfo(path);
            //獲取文件夾下所有的文件
            foreach (FileInfo feInfo in dyInfo.GetFiles())
            {
                if (feInfo.Extension.ToLowerInvariant() != ".ttf") continue;
                Stimulsoft.Base.StiFontCollection.AddFontFile(feInfo.FullName);
            }
            initialed = true;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public IActionResult Report()
        {
            return View(new NavigatorBarModel(this));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        private IActionResult InternalGetReport(string mrt, string ReportName, object value)
        {
            InitialFonts();
            // Create the report object
            StiReport report = new StiReport();
            report.Load(StiNetCoreHelper.MapPath(this, $"Reports/{mrt}.mrt"));            
            report.ReportName = ReportName;
            report.Dictionary.Clear();
            report.RegData(mrt, value);
            report.Dictionary.Synchronize();
            return StiNetCoreViewer.ViewerEventResult(this, report);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        private IActionResult Preview(string mrt, string reportname)
        {
            return InternalGetReport(mrt, string.IsNullOrEmpty(reportname) ? mrt : reportname, null);
        }

        /// <summary>
        /// 透過RouteNo取得VLL裝載報表
        /// </summary>
        /// <returns></returns>
        private IActionResult ReportVLLRouteList(IEnumerable<string> RouteNos)
        {
            if (RouteNos.Count() == 0) return View();
            
            var ret = ReportVLLRouteListHelper.RetrieveByRouteNo(RouteNos, BestHelper.GetFacility(User));
            ret.All( d =>
            {                  
                d.AddWho = BestHelper.GetPrintReportUserName(d.AddWho);
                return true;
            });
            return InternalGetReport("ReportVLLRouteList", "ReportVLLRouteList" + RouteNos, ret);
        }

        /// <summary>
        /// 透過RouteNo取得VLL裝載明細報表
        /// </summary>
        /// <returns></returns>
        private IActionResult ReportVLLRouteListDetail(IEnumerable<string> RouteNos)
        {
            if (RouteNos.Count() == 0) return View();
            
            var ret = ReportVLLRouteListHelper.RetrieveDetailByRouteNo(RouteNos, BestHelper.GetFacility(User));
            ret.All( d =>
            {                  
                d.AddWho = BestHelper.GetPrintReportUserName(d.AddWho);
                return true;
            });
            return InternalGetReport("ReportVLLRouteListDetail", "ReportVLLRouteListDetail" + RouteNos, ret);
        }

        /// <summary>
        /// 透過RouteNo取得出貨報表
        /// </summary>
        /// <returns></returns>
        private IActionResult ReportShipList(IEnumerable<string> RouteNos, IEnumerable<string> TMSKeys, string ShipListReport)
        {
            if (RouteNos.Count() == 0 || string.IsNullOrEmpty(ShipListReport)) return View();               
            return InternalGetReport(ShipListReport, ShipListReport, ShipListReportHelper.FetchShipListReport(RouteNos, TMSKeys, ShipListReport));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public IActionResult GetReport(string id)
        {
            StiRequestParams requestParams = StiNetCoreViewer.GetRequestParams(this);            
            var RouteNo = requestParams.HttpContext.Request.Query["RouteNo"].ToString();
            var ShipListReport = requestParams.HttpContext.Request.Query["ShipListReport"].ToString();            
            switch (id)
            {
                case "Preview":
                    return Preview(requestParams.HttpContext.Request.Query["mrt"].ToString(), requestParams.HttpContext.Request.Query["reportname"].ToString());
                case "ReportVLLRouteList":
                    var vllroutes = RouteNo.Split(',');
                    return ReportVLLRouteList(vllroutes);
                case "ReportVLLRouteListDetail":
                    var vllroutesdetail = RouteNo.Split(',');
                    return ReportVLLRouteListDetail(vllroutesdetail);
                case "ReportShipList":
                    var routes = RouteNo.Split(',');
                    var tmskeys = requestParams.HttpContext.Request.Query["TMSKey"].ToString().Split(',');
                    return ReportShipList(routes, tmskeys, ShipListReport);
                default:
                    return View();
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public IActionResult ViewerEvent()
        {
            return StiNetCoreViewer.ViewerEventResult(this);
        }

    }
}
