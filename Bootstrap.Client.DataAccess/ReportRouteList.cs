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
    public class ReportRouteList
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
        /// 運送區域
        /// </summary>
        public string AreaDescription { get; set; }
        /// <summary>
        /// 暫存區
        /// </summary>
        public string DockNo { get; set; }
        /// <summary>
        /// 縣市別
        /// </summary>
        public string City { get; set; }
        /// <summary>
        /// 貨運公司
        /// </summary>
        public string CompanyName { get; set; }
        /// <summary>
        /// 車號
        /// </summary>
        public string VehicleKey { get; set; }
        /// <summary>
        /// 車次
        /// </summary>
        public string DriveTimes { get; set; }
        /// <summary>
        /// 一單多車
        /// </summary>
        public string MultiVehicle { get; set; }
        /// <summary>
        /// 司機
        /// </summary>
        public string Driver { get; set; }
        /// <summary>
        /// 可載重量
        /// </summary>
        public string LoadingWeight { get; set; }
        /// <summary>
        /// 可載材積
        /// </summary>
        public string LoadingCube { get; set; }
        /// <summary>
        /// 路線編號
        /// </summary>
        public string RouteNo { get; set; }
        /// <summary>
        /// 運送點數
        /// </summary>
        public string CountOrders { get; set; }
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
        /// 貨運公司代碼
        /// </summary>
        public string CompanyKey { get; set; }
        /// <summary>
        /// 備註
        /// </summary>
        public string Notes { get; set; }
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
        public string DeliveryDateS { get; set; }
        /// <summary>
        /// 到貨時間(訖)
        /// </summary>
        public string DeliveryDateE { get; set; }
        /// <summary>
        /// 出車時間(起)
        /// </summary>
        public string DoRouteDateS { get; set; }
        /// <summary>
        /// 出車時間(迄)
        /// </summary>
        public string DoRouteDateE { get; set; }
        /// <summary>
        /// 品號
        /// </summary>
        public string Sku { get; set; }
        /// <summary>
        /// 倉別
        /// </summary>
        public string WareHouse { get; set; }
        /// <summary>
        /// 品名
        /// </summary>
        public string Descr { get; set; }
        /// <summary>
        /// 出車日期
        /// </summary>
        public string DoRouteDate { get; set; }
    

        public virtual IEnumerable<ReportRouteList> Retrieves( string deliverydates, string deliverydatee, string doroutedates , string doroutedatee) => DbManager.Create("bestlogtms").FetchProc<ReportRouteList>(
            "SPReportRouteList", new
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