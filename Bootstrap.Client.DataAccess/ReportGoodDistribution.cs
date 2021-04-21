using Bootstrap.Security.DataAccess;
using PetaPoco;
using System;
using System.Collections.Generic;
using Longbow.Web.Mvc;
using System.Linq;
using Longbow.Data;

namespace Bootstrap.Client.DataAccess
{
    /// <summary>
    /// 排車一覽表
    /// </summary>
    public class ReportGoodDistribution
    {
        /// <summary>
        /// 到貨日期
        /// </summary>
        public string DeliveryDate { get; set; }
        /// <summary>
        /// 區碼
        /// </summary>
        public string AreaCode { get; set; }
        /// <summary>
        /// 區域
        /// </summary>
        public string AreaDescription { get; set; }
        /// <summary>
        /// 車號
        /// </summary>
        public string VehicleKey { get; set; }
        /// <summary>
        /// 路線編號
        /// </summary>
        public string RouteNo { get; set; }
        /// <summary>
        /// 品號
        /// </summary>
        public string Sku { get; set; }
        /// <summary>
        /// 品名
        /// </summary>
        public string Descr { get; set; }
        /// <summary>
        /// 運送箱數
        /// </summary>
        public string ShipCaseQty { get; set; }
        /// <summary>
        /// 運送個數
        /// </summary>
        public string ShipQty { get; set; }
        /// <summary>
        /// 運送板數
        /// </summary>
        public string ShipPalletQty { get; set; }
        /// <summary>
        /// 運送重量
        /// </summary>
        public string ShipWeight { get; set; }
        /// <summary>
        /// 運送材積
        /// </summary>
        public string ShipCube { get; set; }
        /// <summary>
        /// 預計報到時間
        /// </summary>
        public string ExpectDate { get; set; }
        /// <summary>
        /// 貨主
        /// </summary>
        public string StorerKey { get; set; }
        /// <summary>
        /// 到貨時間(起)
        /// </summary>
        public DateTime? DeliveryDateS { get; set; }
        /// <summary>
        /// 到貨時間(訖)
        /// </summary>
        public DateTime? DeliveryDateE { get; set; }
        /// <summary>
        /// 出車時間(起)
        /// </summary>
        public DateTime? DoRouteDateS { get; set; }
        /// <summary>
        /// 出車時間(迄)
        /// </summary>
        public DateTime? DoRouteDateE { get; set; }
        /// <summary>
        /// 倉別
        /// </summary>
        public string WareHouse { get; set; }
        /// <summary>
        /// 出車日期
        /// </summary>
        public string DoRouteDate { get; set; }
    

        public virtual IEnumerable<ReportGoodDistribution> Retrieves( string deliverydates, string deliverydatee, string doroutedates , string doroutedatee) => DbManager.Create("bestlogtms").FetchProc<ReportGoodDistribution>(
            "SPReportGoodDistribution", new
            {   
                WareHouse = "BestLogWMS",
                DoRouteDateS = doroutedates,
                DoRouteDateE = doroutedatee,
                DeliveryDateS = deliverydates,
                DeliveryDateE = deliverydatee
            }
        );
    }
}