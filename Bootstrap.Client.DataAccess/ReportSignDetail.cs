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
    /// 簽單明細報表
    /// </summary>

    public class ReportSignDetail
    {
        /// <summary>
        /// 路線編號
        /// </summary>
        public string RouteNo { get; set; }
        /// <summary>
        /// 車號
        /// </summary>
        public string VehicleKey { get; set; }
        /// <summary>
        /// 司機
        /// </summary>
        public string Driver { get; set; }
        /// <summary>
        /// 貨運公司
        /// </summary>
        public string ShippingCompany { get; set; }
        /// <summary>
        /// 出車日期
        /// </summary>
        public string DoRouteDate { get; set; }
        /// <summary>
        /// 貨主
        /// </summary>
        public string StorerKey { get; set; }
        /// <summary>
        /// 貨主名稱
        /// </summary>
        public string StorerName { get; set; }
        /// <summary>
        /// 備註
        /// </summary>
        public string Notes { get; set; }
        /// <summary>
        /// 客戶編號
        /// </summary>
        public string ConsigneeKey { get; set; }
        /// <summary>
        /// 客戶名稱
        /// </summary>
        public string ShortName { get; set; }
        /// <summary>
        /// 郵遞區號
        /// </summary>
        public string Zip { get; set; }
        /// <summary>
        /// 區碼
        /// </summary>
        public string AreaCode { get; set; }
        /// <summary>
        /// 送貨地址
        /// </summary>
        public string ShipToAddress { get; set; }
        /// <summary>
        /// 訂單類別
        /// </summary>
        public string OrderType { get; set; }
        /// <summary>
        /// 訂單日期
        /// </summary>
        public string OrderDate { get; set; }
        /// <summary>
        /// 到貨日期
        /// </summary>
        public string DeliveryDate { get; set; }
        /// <summary>
        /// 貨主單號
        /// </summary>
        public string ExternOrderKey { get; set; }
        /// <summary>
        /// 簽單日期
        /// </summary>
        public string ConFirmDate { get; set; }
        /// <summary>
        /// 狀態
        /// </summary>
        public string ConfirmNotes { get; set; }
        /// <summary>
        /// 客戶驗收單號
        /// </summary>
        public string CustomerOrderKey { get; set; }
        /// <summary>
        /// 簽單回傳日期
        /// </summary>
        public string SdnSendDate { get; set; }
        /// <summary>
        /// 客戶回覆處理方式
        /// </summary>
        public string CustHandle { get; set; }
        /// <summary>
        /// 簽單備註
        /// </summary>
        public string SDNNotes { get; set; }
        /// <summary>
        /// 發票回收
        /// </summary>
        public string InvBack { get; set; }
        /// <summary>
        /// 簽單已回
        /// </summary>
        public string SDNBack { get; set; }
        /// <summary>
        /// 訂單號碼
        /// </summary>
        public string TMSKey { get; set; }
        /// <summary>
        /// 項次
        /// </summary>
        public string OrderLineNumber { get; set; }
        /// <summary>
        /// 品號
        /// </summary>
        public string Sku { get; set; }
        /// <summary>
        /// 品名
        /// </summary>
        public string Descr { get; set; }
        /// <summary>
        /// 單位
        /// </summary>
        public string Uom { get; set; }
        /// <summary>
        /// 訂單量
        /// </summary>
        public string OrderQty { get; set; }
        /// <summary>
        /// 訂單箱數
        /// </summary>
        public string CaseQty { get; set; }
        /// <summary>
        /// 送貨量
        /// </summary>
        public string ShipQty { get; set; }
        /// <summary>
        /// 送貨箱數
        /// </summary>
        public string ShipCaseQty { get; set; }
        /// <summary>
        /// 簽單量
        /// </summary>
        public string SignQty { get; set; }
        /// <summary>
        /// 簽單箱數
        /// </summary>
        public string SignCaseQty { get; set; }
        /// <summary>
        /// 箱包轉換率
        /// </summary>
        public string CaseCnt { get; set; }
        /// <summary>
        /// 單位材積
        /// </summary>
        public string STDCUBE { get; set; }
        /// <summary>
        /// 單位重量
        /// </summary>
        public string STDGROSSWGT { get; set; }
        /// <summary>
        /// 訂單總材積
        /// </summary>
        public string Cube { get; set; }
        /// <summary>
        /// 送貨總材積
        /// </summary>
        public string ShipCube { get; set; }
        /// <summary>
        /// 簽單總材積
        /// </summary>
        public string SignCube { get; set; }
        /// <summary>
        /// 訂單總重
        /// </summary>
        public string Weight { get; set; }
        /// <summary>
        /// 送貨總重
        /// </summary>
        public string ShipWeight { get; set; }
        /// <summary>
        /// 簽單總重
        /// </summary>
        public string SignWeighr { get; set; }
        /// <summary>
        /// 異常碼
        /// </summary>
        public string RSC { get; set; }
        /// <summary>
        /// 責屬碼
        /// </summary>
        public string RBC { get; set; }

        public virtual IEnumerable<ReportSignDetail> Retrieves(string storers, string ordertypes, string deliverydates, string deliverydatee) => DbManager.Create("bestlogtms").FetchProc<ReportSignDetail>(
            "SPReportSignDetail", new
            {
                Warehouse = "BestLogWMS",
                StorerKey = storers,
                OrderType = ordertypes,
                DeliveryDateS = deliverydates,
                DeliveryDateE = deliverydatee
            }
        );

    }
}
