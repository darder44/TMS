using Bootstrap.Client.DataAccess;
using Longbow.Web.Mvc;
using System.Linq;
using System;
using Bootstrap.Client.DataAccess.Helper;
using System.Collections.Generic;

namespace Bootstrap.Client.Query
{

    /// <summary>
    /// 
    /// </summary>
    public class QueryReportDeliveryAbnormalOption : CustomPaginationOption
    {
        /// <summary>
        /// 貨主代號
        /// </summary>
        public string StorerKey { get; set; }
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
        /// 品號
        /// </summary>
        public string Sku { get; set; }
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
        /// 到貨時間(起)
        /// </summary>
        public DateTime? DeliveryDateS { get; set; }
        /// <summary>
        /// 到貨時間(起)
        /// </summary>
        public DateTime? DeliveryDateE { get; set; }
        /// <summary>
        /// 簽單日期(起)
        /// </summary>
        public DateTime? SignDateS { get; set; }
        /// <summary>
        /// 簽單日期(起)
        /// </summary>
        public DateTime? SignDateE { get; set; }

        /// <summary>
        /// 查詢欄位
        /// </summary>
        private IEnumerable<ReportDeliveryAbnormal> InternalQueryData(IEnumerable<ReportDeliveryAbnormal> data, out int dataCount)
        {
            if (!string.IsNullOrEmpty(RSCCode))
            {
                data = data.Where( d => d.RSCCode.Contains(RSCCode) );
            }
            if (!string.IsNullOrEmpty(RBCCode))
            {
                data = data.Where( d => d.RBCCode.Contains(RBCCode) );
            }
            if (!string.IsNullOrEmpty(RBCName))
            {
                data = data.Where( d => d.RBCName.Contains(RBCName) );
            }
            if (!string.IsNullOrEmpty(VehicleKey))
            {
                data = data.Where( d => d.VehicleKey.Contains(VehicleKey) );
            }
            if (!string.IsNullOrEmpty(Consigneekey))
            {
                data = data.Where( d => d.Consigneekey.Contains(Consigneekey) );
            }
            if (!string.IsNullOrEmpty(TMSKey))
            {
                data = data.Where( d => d.TMSKey.Contains(TMSKey) );
            }
            if (!string.IsNullOrEmpty(Externorderkey))
            {
                data = data.Where( d => d.Externorderkey.Contains(Externorderkey) );
            }
            if (!string.IsNullOrEmpty(Sku))
            {
                data = data.Where( d => d.Sku.Contains(Sku) );
            }

            dataCount = data.Count();

            switch (Sort)
            {
                case "StorerKey":
                    data = Order == "asc" ? data.OrderBy(t => t.StorerKey) : data.OrderByDescending(t => t.StorerKey);
                    break;
                case "ShortName":
                    data = Order == "asc" ? data.OrderBy(t => t.ShortName) : data.OrderByDescending(t => t.ShortName);
                    break;
                case "VehicleKey":
                    data = Order == "asc" ? data.OrderBy(t => t.VehicleKey) : data.OrderByDescending(t => t.VehicleKey);
                    break;
                case "Consigneekey":
                    data = Order == "asc" ? data.OrderBy(t => t.Consigneekey) : data.OrderByDescending(t => t.Consigneekey);
                    break;
                case "Externorderkey":
                    data = Order == "asc" ? data.OrderBy(t => t.Externorderkey) : data.OrderByDescending(t => t.Externorderkey);
                    break;
                case "Orderdate":
                    data = Order == "asc" ? data.OrderBy(t => t.Orderdate) : data.OrderByDescending(t => t.Orderdate);
                    break;
                case "DeliveryDate":
                    data = Order == "asc" ? data.OrderBy(t => t.DeliveryDate) : data.OrderByDescending(t => t.DeliveryDate);
                    break;
                case "Sku":
                    data = Order == "asc" ? data.OrderBy(t => t.Sku) : data.OrderByDescending(t => t.Sku);
                    break;
                case "Descr":
                    data = Order == "asc" ? data.OrderBy(t => t.Descr) : data.OrderByDescending(t => t.Descr);
                    break;
                case "BarCode":
                    data = Order == "asc" ? data.OrderBy(t => t.BarCode) : data.OrderByDescending(t => t.BarCode);
                    break;
                case "OrderQty":
                    data = Order == "asc" ? data.OrderBy(t => t.OrderQty) : data.OrderByDescending(t => t.OrderQty);
                    break;
                case "CaseQty":
                    data = Order == "asc" ? data.OrderBy(t => t.CaseQty) : data.OrderByDescending(t => t.CaseQty);
                    break;
                case "ShipQty":
                    data = Order == "asc" ? data.OrderBy(t => t.ShipQty) : data.OrderByDescending(t => t.ShipQty);
                    break;
                case "ShipCaseQty":
                    data = Order == "asc" ? data.OrderBy(t => t.ShipCaseQty) : data.OrderByDescending(t => t.ShipCaseQty);
                    break;
                case "SignQty":
                    data = Order == "asc" ? data.OrderBy(t => t.SignQty) : data.OrderByDescending(t => t.SignQty);
                    break;
                case "SignCaseQty":
                    data = Order == "asc" ? data.OrderBy(t => t.SignCaseQty) : data.OrderByDescending(t => t.SignCaseQty);
                    break;
                case "ReturnQty":
                    data = Order == "asc" ? data.OrderBy(t => t.ReturnQty) : data.OrderByDescending(t => t.ReturnQty);
                    break;
                case "ReturnCaseQty":
                    data = Order == "asc" ? data.OrderBy(t => t.ReturnCaseQty) : data.OrderByDescending(t => t.ReturnCaseQty);
                    break;
                case "Uom":
                    data = Order == "asc" ? data.OrderBy(t => t.Uom) : data.OrderByDescending(t => t.Uom);
                    break;
                case "Casecnt":
                    data = Order == "asc" ? data.OrderBy(t => t.Casecnt) : data.OrderByDescending(t => t.Casecnt);
                    break;
                case "RSCCode":
                    data = Order == "asc" ? data.OrderBy(t => t.RSCCode) : data.OrderByDescending(t => t.RSCCode);
                    break;
                case "RBCCode":
                    data = Order == "asc" ? data.OrderBy(t => t.RBCCode) : data.OrderByDescending(t => t.RBCCode);
                    break;
                case "RBCName":
                    data = Order == "asc" ? data.OrderBy(t => t.RBCName) : data.OrderByDescending(t => t.RBCName);
                    break;
                case "CustHandle":
                    data = Order == "asc" ? data.OrderBy(t => t.CustHandle) : data.OrderByDescending(t => t.CustHandle);
                    break;
                case "TMSKey":
                    data = Order == "asc" ? data.OrderBy(t => t.TMSKey) : data.OrderByDescending(t => t.TMSKey);
                    break;
                case "ConfirmDate":
                    data = Order == "asc" ? data.OrderBy(t => t.ConfirmDate) : data.OrderByDescending(t => t.ConfirmDate);
                    break;
                case "ConfirmNotes":
                    data = Order == "asc" ? data.OrderBy(t => t.ConfirmNotes) : data.OrderByDescending(t => t.ConfirmNotes);
                    break;
            }
            if (Limit != 0) data = data.Skip(Offset).Take(Limit);
            return data;
        }

        /// <summary>
        /// 查詢
        /// </summary>
        public QueryData<object> RetrieveData()
        {
            var storers = string.IsNullOrEmpty(StorerKey) ? "" : string.Join(",", StorerKey.Split(",").Select(p => string.Format("'{0}'", p)).ToArray());
            var confirmnotes = string.IsNullOrEmpty(ConfirmNotes) ? "" : string.Join(",", ConfirmNotes.Split(",").Select(p => string.Format("'{0}'", p)).ToArray());
            var signdates = SignDateS.HasValue ? DataComparison.DateTimeConvert(SignDateS) : "";
            var signdatee = SignDateE.HasValue ? DataComparison.DateTimeConvert(SignDateE) : "";
            var deliverydates = DeliveryDateS.HasValue ? DataComparison.DateTimeConvert(DeliveryDateS) : "";
            var deliverydatee = DeliveryDateE.HasValue ? DataComparison.DateTimeConvert(DeliveryDateE) : "";

            var dataCount = 0;
            var data = InternalQueryData(ReportDeliveryAbnormalHelper.Retrieves(storers, confirmnotes, signdates, signdatee, deliverydates, deliverydatee), out dataCount);    
            var ret = new QueryData<object>();
            ret.total = dataCount;        
            //重新查詢欄位資料
            ret.rows = data.Select(u => new
            {
                u.StorerKey,
                u.ShortName,
                u.VehicleKey,
                u.Consigneekey,
                u.Externorderkey,
                u.Orderdate,
                u.DeliveryDate,
                u.Sku,
                u.Descr,
                u.BarCode,
                u.OrderQty,
                u.CaseQty,
                u.ShipQty,
                u.ShipCaseQty,
                u.SignQty,
                u.SignCaseQty,
                u.ReturnQty,
                u.ReturnCaseQty,
                u.Uom,
                u.Casecnt,
                u.RSCCode,
                u.RBCCode,
                u.RBCName,
                u.CustHandle,
                u.TMSKey,
                u.ConfirmDate,
                u.ConfirmNotes
            });
            return ret;
        }

        /// <summary>
        /// 
        /// </summary>  
        public IEnumerable<ReportDeliveryAbnormal> RetrievesExcel()
        {
            var storers = string.IsNullOrEmpty(StorerKey) ? "" : string.Join(",", StorerKey.Split(",").Select(p => string.Format("'{0}'", p)).ToArray());
            var confirmnotes = string.IsNullOrEmpty(ConfirmNotes) ? "" : string.Join(",", ConfirmNotes.Split(",").Select(p => string.Format("'{0}'", p)).ToArray());
            var signdates = SignDateS.HasValue ? DataComparison.DateTimeConvert(SignDateS) : "";
            var signdatee = SignDateE.HasValue ? DataComparison.DateTimeConvert(SignDateE) : "";
            var deliverydates = DeliveryDateS.HasValue ? DataComparison.DateTimeConvert(DeliveryDateS) : "";
            var deliverydatee = DeliveryDateE.HasValue ? DataComparison.DateTimeConvert(DeliveryDateE) : "";

            var dataCount = 0;
            var data = InternalQueryData(ReportDeliveryAbnormalHelper.Retrieves(storers, confirmnotes, signdates, signdatee, deliverydates, deliverydatee), out dataCount);
            return data;
        }
    }
}
