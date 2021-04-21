using Bootstrap.Client.Models;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using Microsoft.Extensions.Caching.Memory;
using Bootstrap.Client.DataAccess.Helper;

namespace Bootstrap.Client.Controllers
{
    /// <summary>
    /// TMS控制器
    /// </summary>
    [Authorize]
    public class TMSController : Controller
    {
        /// <summary>
        /// 
        /// </summary>
        [AllowAnonymous]
        public ActionResult Download([FromServices] IMemoryCache cache, string token)
        {
            var downloadCache = cache.Get<DownloadCache>(token);
            if (downloadCache == null) return NotFound();
            cache.Remove(token);
            return File(downloadCache.mem, "application/octet-stream", downloadCache.fileName);
        }

        /// <summary>
        /// 貨主
        /// </summary>
        /// <returns></returns>
        public ActionResult BaseStorer() => View(new NormalModel(this));
        /// <summary>
        /// 客戶主檔
        /// </summary>
        /// <returns></returns>
        public ActionResult BaseCustomer() => View(new NormalModel(this));

        /// <summary>
        /// 轉運站
        /// </summary>
        /// <returns></returns>
        public ActionResult BaseFacility() => View(new NormalModel(this));

        /// <summary>
        /// 計費代碼
        /// </summary>
        /// <returns></returns>
        public ActionResult BaseCostCode() => View(new NormalModel(this));

        /// <summary>
        /// 字典表
        /// </summary>
        /// <returns></returns>
        public ActionResult BaseDictionary() => View(new NormalModel(this));

        /// <summary>
        /// 一般排車
        /// </summary>
        /// <returns></returns>
        public ActionResult NormalStandbyOrders() => View(new NormalModel(this));

        /// <summary>
        /// 訂單切割
        /// </summary>
        /// <returns></returns>
        public ActionResult NormalCutOrders() => View(new NormalModel(this));

        /// <summary>
        /// 訂單切割
        /// </summary>
        /// <returns></returns>
        public ActionResult NormalSignOrders() => View(new NormalModel(this));

        /// <summary>
        /// 訂單維護
        /// </summary>
        /// <returns></returns>
        public ActionResult NormalOrders() => View(new NormalModel(this));

        /// <summary>
        /// 到貨追蹤表
        /// </summary>
        /// <returns></returns>
        public ActionResult ReportTRPTrack() => View(new NormalModel(this));

        /// <summary>
        /// 車輛資料
        /// </summary>
        /// <returns></returns>
        public ActionResult BaseVehicle() => View(new NormalModel(this));
        
        /// <summary>
        /// 回單簽核表
        /// </summary>
        /// <returns></returns>
        public ActionResult ReportSDNReturnList() => View(new NormalModel(this));

        /// <summary>
        /// 請付款報表
        /// </summary>
        /// <returns></returns>
        public ActionResult ReportRequest() => View(new NormalModel(this));
        
        /// <summary>
        /// VLL裝載總表
        /// </summary>
        /// <returns></returns>
        public ActionResult ReportVLLRouteList() => View(new NormalModel(this));
        
        /// <summary>
        /// 簽單明細報表
        /// </summary>
        /// <returns></returns>
        public ActionResult ReportSignDetail() => View(new NormalModel(this));
        /// <summary>
        /// 配送異常表
        /// </summary>
        /// <returns></returns>
        public ActionResult ReportDeliveryAbnormal() => View(new NormalModel(this));
        /// <summary>
        /// 貨運公司主檔
        /// </summary>
        /// <returns></returns>
        public ActionResult BaseShippingCompany() => View(new NormalModel(this));
        /// <summary>
        /// 訂單匯入字典表維護
        /// </summary>
        /// <returns></returns>
        public ActionResult DataComparisonOrders() => View(new NormalModel(this));
        /// <summary>
        /// 裝載碼頭總表
        /// </summary>
        /// <returns></returns>
        public ActionResult VLLxDock() => View(new NormalModel(this));
        /// <summary>
        /// 排車一覽表
        /// </summary>
        /// <returns></returns>
        public ActionResult ReportRouteList() => View(new NormalModel(this));
    }
}