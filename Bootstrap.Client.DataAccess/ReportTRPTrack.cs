using Bootstrap.Security;
using Bootstrap.Security.DataAccess;
using Longbow.Security.Cryptography;
using Longbow.Web.Mvc;
using PetaPoco;
using System;
using System.Collections.Generic;
using System.Linq;
using Bootstrap.Client.DataAccess.Helper;
using Newtonsoft.Json;
using Longbow.Data;


namespace Bootstrap.Client.DataAccess
{

    /// <summary>
    /// 到貨追蹤表
    /// </summary>

    public class ReportTRPTrack
    {
        /// <summary>
        /// OrderType
        /// </summary>
        public string OrderType { get; set; }
        /// <summary>
        /// OrderStatus
        /// </summary>
        public string OrderStatus { get; set; }
        /// <summary>
        /// AreaCode
        /// </summary>
        public string AreaCode { get; set; }
        /// <summary>
        /// VehicleKey
        /// </summary>
        public string VehicleKey { get; set; }
        /// <summary>
        /// Driver
        /// </summary>
        public string Driver { get; set; }
        /// <summary>
        /// RouteNo
        /// </summary>
        public string RouteNo { get; set; }
        /// <summary>
        /// OrderDate
        /// </summary>
        public DateTime? OrderDate { get; set; }
        /// <summary>
        /// DeliveryDate
        /// </summary>
        public DateTime? DeliveryDate { get; set; }
        /// <summary>
        /// DoRouteDate
        /// </summary>
        public DateTime? DoRouteDate { get; set; }
        /// <summary>
        /// SignDate
        /// </summary>
        public DateTime? SignDate { get; set; }
        /// <summary>
        /// StorerKey
        /// </summary>
        public string StorerKey { get; set; }
        /// <summary>
        /// WaveKey
        /// </summary>
        public string WaveKey { get; set; }
        /// <summary>
        /// TMSKey
        /// </summary>
        public string TMSKey { get; set; }
        /// <summary>
        /// ExternOrderKey
        /// </summary>
        public string ExternOrderKey { get; set; }
        /// <summary>
        /// ShortName
        /// </summary>
        public string ShortName { get; set; }
        /// <summary>
        /// City
        /// </summary>
        public string City { get; set; }
        /// <summary>
        /// PalletQty
        /// </summary>
        public double PalletQty { get; set; }
        /// <summary>
        /// CaseQty
        /// </summary>
        public double CaseQty { get; set; }
        /// <summary>
        /// OrderQty
        /// </summary>
        public int OrderQty { get; set; }
        /// <summary>
        /// TotalQty
        /// </summary>
        public int TotalQty { get; set; }
        /// <summary>
        /// ShipQty
        /// </summary>
        public int ShipQty { get; set; }
        /// <summary>
        /// ShipCaseQty
        /// </summary>
        public int ShipCaseQty { get; set; }
        /// <summary>
        /// TotalShipQty
        /// </summary>
        public int TotalShipQty { get; set; }
        /// <summary>
        /// SignQty
        /// </summary>
        public int SignQty { get; set; }
        /// <summary>
        /// Weight
        /// </summary>
        public decimal Weight { get; set; }
        /// <summary>
        /// Cube
        /// </summary>
        public decimal Cube { get; set; }
        /// <summary>
        /// Notes
        /// </summary>
        public string Notes { get; set; }
        /// <summary>
        /// OnTime
        /// </summary>
        public string OnTime { get; set; }
        /// <summary>
        /// Delay
        /// </summary>
        public string Delay { get; set; }
        /// <summary>
        /// 客編
        /// </summary>
        public string ConsigneeKey { get; set; }
        /// <summary>
        /// AddDate
        /// </summary>
        public DateTime AddDate { get; set; }
        public string SoldTo { get; set; }
        public string ShipTo { get; set; }
        public string SoldToName { get; set; }
        public string ShipToName { get; set; }

        public string StartAddress { get; set; }

        public string EndAddress { get; set; }




        public virtual IEnumerable<ReportTRPTrack> Retrieves(string SheetName, string storers, string ordertypes, string orderstatus, string consigneeKey, string waveKey, string tmskey, string externOrderKey, string areacodes, string routeno, string carleavedates, string carleavedatee, string deliverydates, string deliverydatee) => DbManager.Create("bestlogtms").FetchProc<ReportTRPTrack>(
            "SPReportTRPTrack", new
            {
                SheetName = SheetName,
                Warehouse = "BestLogWMS",
                StorerKey = storers,
                OrderType = ordertypes,
                OrderStatus = orderstatus,
                ConsigneeKey = consigneeKey,
                WaveKey = waveKey,
                TMSKey = tmskey,
                ExternOrderKey = externOrderKey,
                AreaCode = areacodes,
                RouteNo = routeno,
                CarLeaveDateS = carleavedates,
                CarLeaveDateE = carleavedatee,
                DeliveryDateS = deliverydates,
                DeliveryDateE = deliverydatee
            }
        );

    }
}
