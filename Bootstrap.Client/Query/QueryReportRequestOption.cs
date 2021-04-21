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
    public class QueryReportRequestOption : CustomPaginationOption
    {
        /// <summary>
        /// 查詢貨主
        /// </summary>
        public string StorerKey { get; set; }

        /// <summary>
        /// 查詢倉別
        /// </summary>
        public string Facility { get; set; }
        /// <summary>
        /// 出車時間(起)
        /// </summary>
        public DateTime? DoRouteDateS { get; set; }
        /// <summary>
        /// 出車時間(訖)
        /// </summary>
        public DateTime? DoRouteDateE { get; set; }
        /// <summary>
        /// 到貨時間(起)
        /// </summary>
        public DateTime? DeliveryDateS { get; set; }
        /// <summary>
        /// 到貨時間(訖)
        /// </summary>
        public DateTime? DeliveryDateE { get; set; }        

        /// <summary>
        /// 查詢欄位
        /// </summary>
        private IEnumerable<ReportRequest> InternalQueryData(IEnumerable<ReportRequest> data, out int dataCount)
        {
            dataCount = data.Count();

            switch (Sort)
            {
                case "CostStatus":
                    data = Order == "asc" ? data.OrderBy(t => t.CostStatus) : data.OrderByDescending(t => t.CostStatus);
                break;
                case "StorerKey":
                    data = Order == "asc" ? data.OrderBy(t => t.StorerKey) : data.OrderByDescending(t => t.StorerKey);
                break;
                case "SignDate":
                    data = Order == "asc" ? data.OrderBy(t => t.SignDate) : data.OrderByDescending(t => t.SignDate);
                break;
                case "DoRouteDate":
                    data = Order == "asc" ? data.OrderBy(t => t.DoRouteDate) : data.OrderByDescending(t => t.DoRouteDate);
                break;
                case "OrderDate":
                    data = Order == "asc" ? data.OrderBy(t => t.OrderDate) : data.OrderByDescending(t => t.OrderDate);
                break;
                case "DeliveryDate":
                    data = Order == "asc" ? data.OrderBy(t => t.DeliveryDate) : data.OrderByDescending(t => t.DeliveryDate);
                break;
                case "VehicleKey":
                    data = Order == "asc" ? data.OrderBy(t => t.VehicleKey) : data.OrderByDescending(t => t.VehicleKey);
                break;
                case "Driver":
                    data = Order == "asc" ? data.OrderBy(t => t.Driver) : data.OrderByDescending(t => t.Driver);
                break;
                case "Receiver":
                    data = Order == "asc" ? data.OrderBy(t => t.Receiver) : data.OrderByDescending(t => t.Receiver);
                break;
                case "RouteNo":
                    data = Order == "asc" ? data.OrderBy(t => t.RouteNo) : data.OrderByDescending(t => t.RouteNo);
                break;
                case "ExternOrderKey":
                    data = Order == "asc" ? data.OrderBy(t => t.ExternOrderKey) : data.OrderByDescending(t => t.ExternOrderKey);
                break;
                case "TMSKey":
                    data = Order == "asc" ? data.OrderBy(t => t.TMSKey) : data.OrderByDescending(t => t.TMSKey);
                break;
                case "ConsigneeKey":
                    data = Order == "asc" ? data.OrderBy(t => t.ConsigneeKey) : data.OrderByDescending(t => t.ConsigneeKey);
                break;
                case "CustName":
                    data = Order == "asc" ? data.OrderBy(t => t.CustName) : data.OrderByDescending(t => t.CustName);
                break;
                case "Uom":
                    data = Order == "asc" ? data.OrderBy(t => t.Uom) : data.OrderByDescending(t => t.Uom);
                break;
                case "ChargeQty":
                    data = Order == "asc" ? data.OrderBy(t => t.ChargeQty) : data.OrderByDescending(t => t.ChargeQty);
                break;
                case "Receivable":
                    data = Order == "asc" ? data.OrderBy(t => t.Receivable) : data.OrderByDescending(t => t.Receivable);
                break;
                case "Payable":
                    data = Order == "asc" ? data.OrderBy(t => t.Payable) : data.OrderByDescending(t => t.Payable);
                break;
                case "Premium":
                    data = Order == "asc" ? data.OrderBy(t => t.Premium) : data.OrderByDescending(t => t.Premium);
                break;
                case "CostName":
                    data = Order == "asc" ? data.OrderBy(t => t.CostName) : data.OrderByDescending(t => t.CostName);
                break;
                case "Reason":
                    data = Order == "asc" ? data.OrderBy(t => t.Reason) : data.OrderByDescending(t => t.Reason);
                break;
                case "SumReceivable":
                    data = Order == "asc" ? data.OrderBy(t => t.SumReceivable) : data.OrderByDescending(t => t.SumReceivable);
                break;
                case "SumPayable":
                    data = Order == "asc" ? data.OrderBy(t => t.SumPayable) : data.OrderByDescending(t => t.SumPayable);
                break;
                case "SumPremium":
                    data = Order == "asc" ? data.OrderBy(t => t.SumPremium) : data.OrderByDescending(t => t.SumPremium);
                break;
                case "AreaStart":
                    data = Order == "asc" ? data.OrderBy(t => t.AreaStart) : data.OrderByDescending(t => t.AreaStart);
                break;
                case "AreaEnd":
                    data = Order == "asc" ? data.OrderBy(t => t.AreaEnd) : data.OrderByDescending(t => t.AreaEnd);
                break;
                case "Notes":
                    data = Order == "asc" ? data.OrderBy(t => t.Notes) : data.OrderByDescending(t => t.Notes);
                break;
                case "CostKind":
                    data = Order == "asc" ? data.OrderBy(t => t.CostKind) : data.OrderByDescending(t => t.CostKind);
                break;
                case "CostCode":
                    data = Order == "asc" ? data.OrderBy(t => t.CostCode) : data.OrderByDescending(t => t.CostCode);
                break;
                case "SdnSendDate":
                    data = Order == "asc" ? data.OrderBy(t => t.SdnSendDate) : data.OrderByDescending(t => t.SdnSendDate);
                break;
                case "SdnSendWho":
                    data = Order == "asc" ? data.OrderBy(t => t.SdnSendWho) : data.OrderByDescending(t => t.SdnSendWho);
                break;
                case "CheckDate":
                    data = Order == "asc" ? data.OrderBy(t => t.CheckDate) : data.OrderByDescending(t => t.CheckDate);
                break;
                case "ConfirmNotes":
                    data = Order == "asc" ? data.OrderBy(t => t.ConfirmNotes) : data.OrderByDescending(t => t.ConfirmNotes);
                break;
                case "SdnNotes":
                    data = Order == "asc" ? data.OrderBy(t => t.SdnNotes) : data.OrderByDescending(t => t.SdnNotes);
                break;
                case "CostDate":
                    data = Order == "asc" ? data.OrderBy(t => t.CostDate) : data.OrderByDescending(t => t.CostDate);
                break;
                case "APnoDistribution":
                    data = Order == "asc" ? data.OrderBy(t => t.APnoDistribution) : data.OrderByDescending(t => t.APnoDistribution);
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
            var storerkey = string.IsNullOrEmpty(StorerKey) ? "" : StorerKey;
            var facility = string.IsNullOrEmpty(Facility) ? "" : Facility;
            var doroutedates = DoRouteDateS.HasValue ? DataComparison.DateTimeConvert(DoRouteDateS) : "";
            var doroutedatee = DoRouteDateE.HasValue ? DataComparison.DateTimeConvert(DoRouteDateE) : "";
            var deliverydates = DeliveryDateS.HasValue ? DataComparison.DateTimeConvert(DeliveryDateS) : "";
            var deliverydatee = DeliveryDateE.HasValue ? DataComparison.DateTimeConvert(DeliveryDateE) : "";

            var dataCount = 0;
            var data = InternalQueryData(ReportRequestHelper.Retrieves(storerkey,  facility,  doroutedates,  doroutedatee,  deliverydates,  deliverydatee, ""), out dataCount);


            var ret = new QueryData<object>();
            ret.total = dataCount;
            var users = UserHelper.Retrieves();
            var newrows = data.GroupJoin(users, d => string.IsNullOrEmpty(d.SdnSendWho) ? "" : d.SdnSendWho.ToUpper().Trim(), u => u.UserName.ToUpper().Trim(), (d, u) => new
            {
                data = d,
                user = u
            }).SelectMany(d => d.user.DefaultIfEmpty(), (d, u) => new
            {
                d.data.CostStatus,
                d.data.StorerKey,
                d.data.SignDate,
                d.data.DoRouteDate,
                d.data.OrderDate,
                d.data.DeliveryDate,
                d.data.VehicleKey,
                d.data.Driver,
                d.data.Receiver,
                d.data.RouteNo,
                d.data.ExternOrderKey,
                d.data.TMSKey,
                d.data.ConsigneeKey,
                d.data.CustName,
                d.data.Uom,
                d.data.ChargeQty,
                d.data.Receivable,
                d.data.Payable,
                d.data.Premium,
                d.data.CostName,
                d.data.Reason,
                d.data.SumReceivable,
                d.data.SumPayable,
                d.data.SumPremium,
                d.data.AreaStart,
                d.data.AreaEnd,
                d.data.Notes,
                d.data.CostKind,
                d.data.CostCode,
                d.data.SdnSendDate,
                d.data.SdnSendWho,
                d.data.CheckDate,
                d.data.ConfirmNotes,
                d.data.SdnNotes,
                d.data.CostDate,
                d.data.APnoDistribution,
                DisplayName = (u != null) ? u.DisplayName : ""
            });
            //重新查詢欄位資料
            ret.rows = newrows;  
            return ret;
        }

        /// <summary>
        /// 
        /// </summary>  
        public IEnumerable<ReportRequest> RetrievesExcel(string reporttype)
        {
            var storerkey = string.IsNullOrEmpty(StorerKey) ? "" : StorerKey;
            var facility = string.IsNullOrEmpty(Facility) ? "" : Facility;
            var doroutedates = DoRouteDateS.HasValue ? DataComparison.DateTimeConvert(DoRouteDateS) : "";
            var doroutedatee = DoRouteDateE.HasValue ? DataComparison.DateTimeConvert(DoRouteDateE) : "";
            var deliverydates = DeliveryDateS.HasValue ? DataComparison.DateTimeConvert(DeliveryDateS) : "";
            var deliverydatee = DeliveryDateE.HasValue ? DataComparison.DateTimeConvert(DeliveryDateE) : "";
            

            var dataCount = 0;
            var data = InternalQueryData(ReportRequestHelper.Retrieves(storerkey,  facility,  doroutedates,  doroutedatee,  deliverydates,  deliverydatee, reporttype), out dataCount);
            return data;
        }
    }
}
