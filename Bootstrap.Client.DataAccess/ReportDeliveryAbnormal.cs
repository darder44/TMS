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
    /// 訂單配送異常表
    /// </summary>
    public class ReportDeliveryAbnormal
    {
        /// <summary>
        /// 貨主代號
        /// </summary>
        public string StorerKey { get; set; }
        /// <summary>
        /// 客戶名稱
        /// </summary>
        public string ShortName { get; set; }
        /// <summary>
        /// 配送車號
        /// </summary>
        public string VehicleKey { get; set; }
        /// <summary>
        /// 客戶編號
        /// </summary>
        public string Consigneekey { get; set; }
        /// <summary>
        /// 訂單號碼
        /// </summary>
        public string Externorderkey { get; set; }
        /// <summary>
        /// 訂單日期
        /// </summary>
        public DateTime? Orderdate { get; set; }
        /// <summary>
        /// 到貨日期
        /// </summary>
        public DateTime? DeliveryDate { get; set; }
        /// <summary>
        /// 品號
        /// </summary>
        public string Sku { get; set; }
        /// <summary>
        /// 品名
        /// </summary>
        public string Descr { get; set; }
        /// <summary>
        /// 條碼
        /// </summary>
        public string BarCode { get; set; }
        /// <summary>
        /// 訂單數量
        /// </summary>
        public string OrderQty { get; set; }
        /// <summary>
        /// 訂單箱數
        /// </summary>
        public string CaseQty { get; set; }
        /// <summary>
        /// 出貨數量
        /// </summary>
        public string ShipQty { get; set; }
        /// <summary>
        /// 出貨箱數
        /// </summary>
        public string ShipCaseQty { get; set; }
        /// <summary>
        /// 簽收數量
        /// </summary>
        public string SignQty { get; set; }
        /// <summary>
        /// 簽收箱數
        /// </summary>
        public string SignCaseQty { get; set; }
        /// <summary>
        /// 退回數量
        /// </summary>
        public string ReturnQty { get; set; }
        /// <summary>
        /// 退回箱數
        /// </summary>
        public string ReturnCaseQty { get; set; }
        /// <summary>
        /// 單位
        /// </summary>
        public string Uom { get; set; }        
        /// <summary>
        /// 箱入數
        /// </summary>
        public string Casecnt { get; set; }    
        /// <summary>
        /// 異常原因
        /// </summary>
        public string RSCCode { get; set; }    
        /// <summary>
        /// 責屬
        /// </summary>
        public string RBCCode { get; set; }    
        /// <summary>
        /// 責屬人
        /// </summary>
        public string RBCName { get; set; }   
        /// <summary>
        /// 客戶回覆處理方式
        /// </summary>
        public string CustHandle { get; set; }            
        /// <summary>
        /// TMS 單號
        /// </summary>
        public string TMSKey { get; set; }           
        /// <summary>
        /// 簽單日期
        /// </summary>
        public DateTime? ConfirmDate { get; set; }           
        /// <summary>
        /// 簽單備註
        /// </summary>
        public string ConfirmNotes { get; set; }
        /// <summary>
        /// RI單號(聯華用)
        /// </summary>
        public string ShippingNumber { get; set; }     

        /// <summary>
        /// 異常訂單
        /// </summary>
        public virtual IEnumerable<ReportDeliveryAbnormal> Retrieves(string storers, string confirmnotes, string signdates, string signdatee, string deliverydates, string deliverydatee) => DbManager.Create("bestlogtms").FetchProc<ReportDeliveryAbnormal>(
            "SPReportDeliveryAbnormal", new
            {
                Warehouse = "BestLogWMS",
                StorerKey = storers,
                ConfirmNotes = confirmnotes,
                SignDateS = signdates,
                signdatee = signdatee,
                DeliveryDateS = deliverydates,
                DeliveryDateE = deliverydatee
            }
        );
    }
}