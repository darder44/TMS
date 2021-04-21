using System.Collections.Generic;
using Longbow.Web.Mvc;
using System.Linq;
using System;
using Bootstrap.Client.DataAccess.Helper;
using Bootstrap.Client.DataAccess;

namespace Bootstrap.Client.Query
{
    /// <summary>
    /// 
    /// </summary>
    public class QueryReportSignDetailOption : CustomPaginationOption
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
        /// <summary>
        /// 
        /// </summary>
        public DateTime? OrderDate_Start { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public DateTime? OrderDate_End { get; set; }
        /// <summary>
        /// 出車日期(起)
        /// </summary>
        public DateTime? DoRouteDate_Start { get; set; }
        /// <summary>
        /// 出車日期(迄)
        /// </summary>
        public DateTime? DoRouteDate_End { get; set; }
        /// <summary>
        /// 到貨日期(起)
        /// </summary>
        public DateTime? DeliveryDate_Start { get; set; }
        /// <summary>
        /// 到貨日期(迄)
        /// </summary>
        public DateTime? DeliveryDate_End { get; set; }
        /// <summary>
        /// 訂單類別查詢
        /// </summary>
        public string OrderTypes { get; set; }
        /// <summary>
        /// 貨主查詢
        /// </summary>
        public string StorerKeys { get; set; }

        /// <summary>
        /// 查詢欄位
        /// </summary>
        private IEnumerable<ReportSignDetail> InternalQueryData(IEnumerable<ReportSignDetail> data, out int dataCount)
        {
            if (!string.IsNullOrEmpty(RouteNo))
            {
                data = data.Where( d => d.RouteNo.Contains(RouteNo) );
            }
            if (!string.IsNullOrEmpty(VehicleKey))
            {
                data = data.Where( d => d.VehicleKey.Contains(VehicleKey) );
            }
            if (!string.IsNullOrEmpty(ConsigneeKey))
            {
                data = data.Where( d => d.ConsigneeKey.Contains(ConsigneeKey) );
            }
            if (!string.IsNullOrEmpty(TMSKey))
            {
                data = data.Where( d => d.TMSKey.Contains(TMSKey) );
            }
            if (!string.IsNullOrEmpty(ExternOrderKey))
            {
                data = data.Where( d => d.ExternOrderKey.Contains(ExternOrderKey) );
            }
            if (!string.IsNullOrEmpty(Sku))
            {
                data = data.Where( d => d.Sku.Contains(Sku) );
            }
            if (OrderDate_Start != null)
            {   
                if (OrderDate_End != null)
                {
                    data = data.Where(t => DataComparison.DateTimeConvert(t.OrderDate) >= OrderDate_Start);
                } 
                else
                {
                    data = data.Where(t => DataComparison.DateTimeConvert(t.OrderDate) == OrderDate_Start);
                }
            }
            if (OrderDate_End != null)
            {
                if (OrderDate_Start != null)
                {
                    data = data.Where(t => DataComparison.DateTimeConvert(t.OrderDate) <= OrderDate_End);
                }
                else
                {
                    data = data.Where(t => DataComparison.DateTimeConvert(t.OrderDate) == OrderDate_End);
                }
            }
            //出車日期
            if (DoRouteDate_Start != null)
            {
                if (DoRouteDate_End != null)
                {
                    data = data.Where(t => DataComparison.DateTimeConvert(t.DoRouteDate) >= DoRouteDate_Start);
                } 
                else
                {
                    data = data.Where(t => DataComparison.DateTimeConvert(t.DoRouteDate) == DoRouteDate_Start);
                }
            }
            if (DoRouteDate_End != null)
            {
                if (DoRouteDate_Start != null)
                {
                    data = data.Where(t => DataComparison.DateTimeConvert(t.DoRouteDate) <= DoRouteDate_End);
                }
                else
                {
                    data = data.Where(t => DataComparison.DateTimeConvert(t.DoRouteDate) == DoRouteDate_End);
                }
            }
            
            dataCount = data.Count();

            switch (Sort)
            {
                case "RouteNo":
                    data = Order == "asc" ? data.OrderBy(t => t.RouteNo) : data.OrderByDescending(t => t.RouteNo);
                break;
                case "VehicleKey":
                    data = Order == "asc" ? data.OrderBy(t => t.VehicleKey) : data.OrderByDescending(t => t.VehicleKey);
                break;
                case "Driver":
                    data = Order == "asc" ? data.OrderBy(t => t.Driver) : data.OrderByDescending(t => t.Driver);
                break;
                case "ShippingCompany":
                    data = Order == "asc" ? data.OrderBy(t => t.ShippingCompany) : data.OrderByDescending(t => t.ShippingCompany);
                break;
                case "DoRouteDate":
                    data = Order == "asc" ? data.OrderBy(t => t.DoRouteDate) : data.OrderByDescending(t => t.DoRouteDate);
                break;
                case "StorerKey":
                    data = Order == "asc" ? data.OrderBy(t => t.StorerKey) : data.OrderByDescending(t => t.StorerKey);
                break;
                case "StorerName":
                    data = Order == "asc" ? data.OrderBy(t => t.StorerName) : data.OrderByDescending(t => t.StorerName);
                break;
                case "Notes":
                    data = Order == "asc" ? data.OrderBy(t => t.Notes) : data.OrderByDescending(t => t.Notes);
                break;
                case "ConsigneeKey":
                    data = Order == "asc" ? data.OrderBy(t => t.ConsigneeKey) : data.OrderByDescending(t => t.ConsigneeKey);
                break;
                case "ShortName":
                    data = Order == "asc" ? data.OrderBy(t => t.ShortName) : data.OrderByDescending(t => t.ShortName);
                break;
                case "Zip":
                    data = Order == "asc" ? data.OrderBy(t => t.Zip) : data.OrderByDescending(t => t.Zip);
                break;
                case "AreaCode":
                    data = Order == "asc" ? data.OrderBy(t => t.AreaCode) : data.OrderByDescending(t => t.AreaCode);
                break;
                case "ShipToAddress":
                    data = Order == "asc" ? data.OrderBy(t => t.ShipToAddress) : data.OrderByDescending(t => t.ShipToAddress);
                break;
                case "OrderType":
                    data = Order == "asc" ? data.OrderBy(t => t.OrderType) : data.OrderByDescending(t => t.OrderType);
                break;
                case "OrderDate":
                    data = Order == "asc" ? data.OrderBy(t => t.OrderDate) : data.OrderByDescending(t => t.OrderDate);
                break;
                case "DeliveryDate":
                    data = Order == "asc" ? data.OrderBy(t => t.DeliveryDate) : data.OrderByDescending(t => t.DeliveryDate);
                break;
                case "ExternOrderKeyExternOrderKey":
                    data = Order == "asc" ? data.OrderBy(t => t.ExternOrderKey) : data.OrderByDescending(t => t.ExternOrderKey);
                break;
                case "ConFirmDate":
                    data = Order == "asc" ? data.OrderBy(t => t.ConFirmDate) : data.OrderByDescending(t => t.ConFirmDate);
                break;
                case "ConfirmNotes":
                    data = Order == "asc" ? data.OrderBy(t => t.ConfirmNotes) : data.OrderByDescending(t => t.ConfirmNotes);
                break;
                case "CustomerOrderKey":
                    data = Order == "asc" ? data.OrderBy(t => t.CustomerOrderKey) : data.OrderByDescending(t => t.CustomerOrderKey);
                break;
                case "SdnSendDate":
                    data = Order == "asc" ? data.OrderBy(t => t.SdnSendDate) : data.OrderByDescending(t => t.SdnSendDate);
                break;
                case "CustHandle":
                    data = Order == "asc" ? data.OrderBy(t => t.CustHandle) : data.OrderByDescending(t => t.CustHandle);
                break;
                case "SDNNotes":
                    data = Order == "asc" ? data.OrderBy(t => t.SDNNotes) : data.OrderByDescending(t => t.SDNNotes);
                break;
                case "InvBack":
                    data = Order == "asc" ? data.OrderBy(t => t.InvBack) : data.OrderByDescending(t => t.InvBack);
                break;
                case "SDNBack":
                    data = Order == "asc" ? data.OrderBy(t => t.SDNBack) : data.OrderByDescending(t => t.SDNBack);
                break;
                case "TMSKey":
                    data = Order == "asc" ? data.OrderBy(t => t.TMSKey) : data.OrderByDescending(t => t.TMSKey);
                break;
                case "OrderLineNumber":
                    data = Order == "asc" ? data.OrderBy(t => t.OrderLineNumber) : data.OrderByDescending(t => t.OrderLineNumber);
                break;
                case "Sku":
                    data = Order == "asc" ? data.OrderBy(t => t.Sku) : data.OrderByDescending(t => t.Sku);
                break;
                case "Descr":
                    data = Order == "asc" ? data.OrderBy(t => t.Descr) : data.OrderByDescending(t => t.Descr);
                break;
                case "Uom":
                    data = Order == "asc" ? data.OrderBy(t => t.Uom) : data.OrderByDescending(t => t.Uom);
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
                case "CaseCnt":
                    data = Order == "asc" ? data.OrderBy(t => t.CaseCnt) : data.OrderByDescending(t => t.CaseCnt);
                break;
                case "STDCUBE":
                    data = Order == "asc" ? data.OrderBy(t => t.STDCUBE) : data.OrderByDescending(t => t.STDCUBE);
                break;
                case "STDGROSSWGT":
                    data = Order == "asc" ? data.OrderBy(t => t.STDGROSSWGT) : data.OrderByDescending(t => t.STDGROSSWGT);
                break;
                case "Cube":
                    data = Order == "asc" ? data.OrderBy(t => t.Cube) : data.OrderByDescending(t => t.Cube);
                break;
                case "ShipCube":
                    data = Order == "asc" ? data.OrderBy(t => t.ShipCube) : data.OrderByDescending(t => t.ShipCube);
                break;
                case "SignCube":
                    data = Order == "asc" ? data.OrderBy(t => t.SignCube) : data.OrderByDescending(t => t.SignCube);
                break;
                case "ShipWeight":
                    data = Order == "asc" ? data.OrderBy(t => t.ShipWeight) : data.OrderByDescending(t => t.ShipWeight);
                break;
                case "SignWeighr":
                    data = Order == "asc" ? data.OrderBy(t => t.SignWeighr) : data.OrderByDescending(t => t.SignWeighr);
                break;
                case "RSC":
                    data = Order == "asc" ? data.OrderBy(t => t.RSC) : data.OrderByDescending(t => t.RSC);
                break;
                case "RBC":
                    data = Order == "asc" ? data.OrderBy(t => t.RBC) : data.OrderByDescending(t => t.RBC);
                break;
            }
            if (Limit != 0) data = data.Skip(Offset).Take(Limit);
            return data;
        }

        /// <summary>
        /// 
        /// </summary>
        public QueryData<object> RetrieveData()
        {            
            var storers = string.IsNullOrEmpty(StorerKeys) ? "" : string.Join(",", StorerKeys.Split(",").Select(p => string.Format("'{0}'", p)).ToArray());
            var ordertypes = string.IsNullOrEmpty(OrderTypes) ? "" : string.Join(",", OrderTypes.Split(",").Select(p => string.Format("'{0}'", p)).ToArray());
            var deliverydates = DeliveryDate_Start.HasValue ? DataComparison.DateTimeConvert(DeliveryDate_Start) : "";
            var deliverydatee = DeliveryDate_End.HasValue ? DataComparison.DateTimeConvert(DeliveryDate_End) : "";

            var dataCount = 0;
            var data = InternalQueryData(ReportSignDetailHelper.Retrieves(storers, ordertypes, deliverydates, deliverydatee), out dataCount);
              
            var ret = new QueryData<object>();
            ret.total = dataCount;

            ret.rows = data.Select(u => new
            {
                u.RouteNo,
                u.VehicleKey,
                u.Driver,
                u.ShippingCompany,
                u.DoRouteDate,
                u.StorerKey,
                u.StorerName,
                u.Zip,
                u.Notes,
                u.ConsigneeKey,
                u.ShortName,
                u.AreaCode,
                u.ShipToAddress,
                u.OrderType,
                u.OrderDate,
                u.DeliveryDate,
                u.ExternOrderKey,
                u.ConFirmDate,
                u.ConfirmNotes,
                u.CustomerOrderKey,
                u.SdnSendDate,
                u.CustHandle,
                u.SDNNotes,
                u.InvBack,
                u.SDNBack,
                u.TMSKey,
                u.OrderLineNumber,
                u.Sku,
                u.Descr,
                u.Uom,
                u.OrderQty,
                u.CaseQty,
                u.ShipQty,
                u.ShipCaseQty,
                u.SignQty,
                u.SignCaseQty,
                u.CaseCnt,
                u.STDCUBE,
                u.STDGROSSWGT,
                u.Cube,
                u.ShipCube,
                u.SignCube,
                u.Weight,
                u.ShipWeight,
                u.SignWeighr,
                u.RSC,
                u.RBC
            });
            return ret;
        }

        /// <summary>
        /// 
        /// </summary>  
        public IEnumerable<ReportSignDetail> RetrievesExcel()
        {
            var storers = string.IsNullOrEmpty(StorerKeys) ? "" : string.Join(",", StorerKeys.Split(",").Select(p => string.Format("'{0}'", p)).ToArray());
            var ordertypes = string.IsNullOrEmpty(OrderTypes) ? "" : string.Join(",", OrderTypes.Split(",").Select(p => string.Format("'{0}'", p)).ToArray());
            var deliverydates = DeliveryDate_Start.HasValue ? DataComparison.DateTimeConvert(DeliveryDate_Start) : "";
            var deliverydatee = DeliveryDate_End.HasValue ? DataComparison.DateTimeConvert(DeliveryDate_End) : "";

            var dataCount = 0;
            var data = InternalQueryData(ReportSignDetailHelper.Retrieves(storers, ordertypes, deliverydates, deliverydatee), out dataCount);
            
            return data;
        }
    }
}
