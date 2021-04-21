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
    public class QueryReportSDNReturnListOption : CustomPaginationOption
    {
        /// <summary>
        /// 貨主代號
        /// </summary>
        public string StorerKey { get; set; }
        /// <summary>
        /// 通路別
        /// </summary>
        public string Channel { get; set; }
        /// <summary>
        /// 二次路編
        /// </summary>
        public string RouteNo { get; set; }
        /// <summary>
        /// 一次路編
        /// </summary>
        public string OriginalRouteNo { get; set; }
        /// <summary>
        /// 二次車號
        /// </summary>
        public string VehicleKey { get; set; }
        /// <summary>
        /// 客戶全名
        /// </summary>
        public string FullName { get; set; }
        /// <summary>
        /// 客戶簡稱
        /// </summary>
        public string ShortName { get; set; }
        /// <summary>
        /// 到貨日
        /// </summary>
        public DateTime? DeliveryDate { get; set; }
        /// <summary>
        /// 訂單類別
        /// </summary>
        public string OrderType { get; set; }
        /// <summary>
        /// 訂單號碼
        /// </summary>
        public string ExternOrderKey { get; set; }
        /// <summary>
        /// 驗收單號
        /// </summary>
        public string Customerorderkey1 { get; set; }
        /// <summary>
        /// 異常狀況
        /// </summary>
        public string SignStatus { get; set; }
        /// <summary>
        /// 簽單狀態
        /// </summary>
        public string ConfirmNotes { get; set; }
        /// <summary>
        /// 採購單號
        /// </summary>
        public string Customerorderkey { get; set; }
        /// <summary>
        /// 發票退回
        /// </summary>
        public string Invback { get; set; }
        /// <summary>
        /// 預定寄送日
        /// </summary>
        public DateTime? Date { get; set; }
        /// <summary>
        /// 簽單確認時間
        /// </summary>
        public DateTime? ScheduleDate { get; set; }
        /// <summary>
        /// 區碼
        /// </summary>
        public string AreaCode { get; set; }
        /// <summary>
        /// 貨運公司
        /// </summary>
        public string Company { get; set; }
        /// <summary>
        /// 配送倉別
        /// </summary>
        public string Facility { get; set; }
        /// <summary>
        /// 簽單狀態
        /// </summary>
        public string Status { get; set; }
        /// <summary>
        /// 維護者
        /// </summary>
        public string ConfirmUser { get; set; }
        /// <summary>
        /// 維護日期(起)
        /// </summary>
        public DateTime? ConfirmDateS { get; set; }
        /// <summary>
        /// 維護日期(迄)
        /// </summary>
        public DateTime? ConfirmDateE { get; set; }
        /// <summary>
        /// 到貨時間(起)
        /// </summary>
        public DateTime? DeliveryDateS { get; set; }
        /// <summary>
        /// 到貨時間(訖)
        /// </summary>
        public DateTime? DeliveryDateE { get; set; }
        /// <summary>
        /// 查詢貨主
        /// </summary>
        public string StorerKeys { get; set; }
        /// <summary>
        /// 查詢單別
        /// </summary>
        public string OrderTypes { get; set; }
        /// <summary>
        /// 排序方式
        /// </summary>
        public string OrderMethod { get; set; }
        /// <summary>
        /// 查詢訂單號碼
        /// </summary>
        public string TMSKeys { get; set; }
        /// <summary>
        /// 查詢貨主單號
        /// </summary>
        public string ExternOrderKeys { get; set; }
        /// <summary>
        /// 查詢區碼
        /// </summary>
        public string AreaCodes { get; set; }
        /// <summary>
        /// 查詢貨運公司
        /// </summary>
        public string Companies { get; set; }
        /// <summary>
        /// 查詢倉別
        /// </summary>
        public string Facilities { get; set; }
        /// <summary>
        /// 查詢簽單狀態
        /// </summary>
        public string OrderStatus { get; set; }

        /// <summary>
        /// 查詢欄位
        /// </summary>
        private IEnumerable<ReportSDNReturnList> InternalQueryData(IEnumerable<ReportSDNReturnList> data, out int dataCount)
        {
            dataCount = data.Count();

            switch (Sort)
            {
                case "StorerKey":
                    data = Order == "asc" ? data.OrderBy(t => t.StorerKey) : data.OrderByDescending(t => t.StorerKey);
                    break;
                case "Channel":
                    data = Order == "asc" ? data.OrderBy(t => t.Channel) : data.OrderByDescending(t => t.Channel);
                    break;
                case "RouteNo":
                    data = Order == "asc" ? data.OrderBy(t => t.RouteNo) : data.OrderByDescending(t => t.RouteNo);
                    break;
                case "VehicleKey":
                    data = Order == "asc" ? data.OrderBy(t => t.VehicleKey) : data.OrderByDescending(t => t.VehicleKey);
                    break;
                case "FullName":
                    data = Order == "asc" ? data.OrderBy(t => t.FullName) : data.OrderByDescending(t => t.FullName);
                    break;
                case "ShortName":
                    data = Order == "asc" ? data.OrderBy(t => t.ShortName) : data.OrderByDescending(t => t.ShortName);
                    break;
                case "DeliveryDate":
                    data = Order == "asc" ? data.OrderBy(t => t.DeliveryDate) : data.OrderByDescending(t => t.DeliveryDate);
                    break;
                case "OrderType":
                    data = Order == "asc" ? data.OrderBy(t => t.OrderType) : data.OrderByDescending(t => t.OrderType);
                    break;    
                case "ExternOrderKey":
                    data = Order == "asc" ? data.OrderBy(t => t.ExternOrderKey) : data.OrderByDescending(t => t.ExternOrderKey);
                    break;
                case "SignStatus":
                    data = Order == "asc" ? data.OrderBy(t => t.SignStatus) : data.OrderByDescending(t => t.SignStatus);
                    break; 
                case "ConfirmNotes":
                    data = Order == "asc" ? data.OrderBy(t => t.ConfirmNotes) : data.OrderByDescending(t => t.ConfirmNotes);
                    break;    
                case "Customerorderkey":
                    data = Order == "asc" ? data.OrderBy(t => t.Customerorderkey) : data.OrderByDescending(t => t.Customerorderkey);
                    break;    
                case "Invback":
                    data = Order == "asc" ? data.OrderBy(t => t.Invback) : data.OrderByDescending(t => t.Invback);
                    break;    
                case "ScheduleDate":
                    data = Order == "asc" ? data.OrderBy(t => t.ScheduleDate) : data.OrderByDescending(t => t.ScheduleDate);
                    break;
                case "SdnSendDate":
                    data = Order == "asc" ? data.OrderBy(t => t.SdnSendDate) : data.OrderByDescending(t => t.SdnSendDate);
                    break;
                case "AreaCode":
                    data = Order == "asc" ? data.OrderBy(t => t.AreaCode) : data.OrderByDescending(t => t.AreaCode);
                    break;  
                case "Company":
                    data = Order == "asc" ? data.OrderBy(t => t.Company) : data.OrderByDescending(t => t.Company);
                    break;  
                case "Facility":
                    data = Order == "asc" ? data.OrderBy(t => t.Facility) : data.OrderByDescending(t => t.Facility);
                    break;           
                case "Status":
                    data = Order == "asc" ? data.OrderBy(t => t.Status) : data.OrderByDescending(t => t.Status);
                    break; 
                case "SdnSendWho":
                    data = Order == "asc" ? data.OrderBy(t => t.SdnSendWho) : data.OrderByDescending(t => t.SdnSendWho);
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
            var routeno = string.IsNullOrEmpty(RouteNo) ? "" : RouteNo;
            var tmskeys = TMSKeys;
            var externorderkeys = ExternOrderKeys;
            var ordermethod = OrderMethod;
            var areacodes = string.IsNullOrEmpty(AreaCodes) ? "" : string.Join(",", AreaCodes.Split(",").Select(p => string.Format("'{0}'", p)).ToArray());
            var companies = string.IsNullOrEmpty(Companies) ? "" : string.Join(",", Companies.Split(",").Select(p => string.Format("'{0}'", p)).ToArray());
            var ordertypes = string.IsNullOrEmpty(OrderTypes) ? "" : string.Join(",", OrderTypes.Split(",").Select(p => string.Format("'{0}'", p)).ToArray());
            var facilies = string.IsNullOrEmpty(Facilities) ? "" : string.Join(",", Facilities.Split(",").Select(p => string.Format("'{0}'", p)).ToArray());
            var orderstatus = string.IsNullOrEmpty(OrderStatus) ? "" : string.Join(",", OrderStatus.Split(",").Select(p => string.Format("'{0}'", p)).ToArray());
            var confirmdates = ConfirmDateS.HasValue ? DataComparison.DateTimeConvert(ConfirmDateS) : "";
            var confirmdatee = ConfirmDateE.HasValue ? DataComparison.DateTimeConvert(ConfirmDateE) : "";
            var deliverydates = DeliveryDateS.HasValue ? DataComparison.DateTimeConvert(DeliveryDateS) : "";
            var deliverydatee = DeliveryDateE.HasValue ? DataComparison.DateTimeConvert(DeliveryDateE) : "";

            var dataCount = 0;
            var data = InternalQueryData(ReportSDNReturnListHelper.Retrieves(storers, ordertypes, areacodes, companies, facilies, orderstatus, tmskeys, externorderkeys, ordermethod, confirmdates, confirmdatee, deliverydates, deliverydatee), out dataCount);
            /*if (!string.IsNullOrEmpty(StorerKey))
            {
                data = data.Where(t => t.StorerKey.Contains(StorerKey.Trim().ToUpper()));
            }
            // 維護時間(起訖)
            if (ConfirmDate_Start != null && ConfirmDate_End != null)
            {
                data = data.Where(t => t.ScheduleDate >= ConfirmDate_Start && t.ScheduleDate <= ConfirmDate_End);
            }
            else if (ConfirmDate_Start != null )
            {
                data = data.Where(t => t.ScheduleDate >= ConfirmDate_Start);
            }
            else if (ConfirmDate_End != null)
            {
                data = data.Where(t => t.ScheduleDate <= ConfirmDate_End);
            }
            // 到貨時間(起訖)
            if (DeliveryDate_Start != null && DeliveryDate_End != null)
            {
                data = data.Where(t => t.DeliveryDate >= DeliveryDate_Start && t.DeliveryDate <= DeliveryDate_End);
            }
            else if (DeliveryDate_Start != null )
            {
                data = data.Where(t => t.DeliveryDate >= DeliveryDate_Start);
            }
            else if (DeliveryDate_End != null)
            {
                data = data.Where(t => t.DeliveryDate <= DeliveryDate_End);
            }

            if (!string.IsNullOrEmpty(AreaCode))
            {
                data = data.Where(t => t.AreaCode.Contains(AreaCode.Trim().ToUpper()));
            }
            if (!string.IsNullOrEmpty(Company))
            {
                data = data.Where(t => t.Company.Contains(Company.Trim().ToUpper()));
            }
            if (!string.IsNullOrEmpty(OrderType))
            {
                data = data.Where(t => t.OrderType.Contains(OrderType.Trim().ToUpper()));
            }
            if (!string.IsNullOrEmpty(Facility))
            {
                data = data.Where(t => t.Facility.Contains(Facility.Trim().ToUpper()));
            }
            if (!string.IsNullOrEmpty(Status))
            {
                data = data.Where(t => t.Status.Contains(Status.Trim().ToUpper()));
            }*/

            var ret = new QueryData<object>();
            ret.total = dataCount;
            var users = UserHelper.Retrieves();
            var newrows = data.GroupJoin(users, d => string.IsNullOrEmpty(d.SdnSendWho) ? "" : d.SdnSendWho.ToUpper().Trim(), u => u.UserName.ToUpper().Trim(), (d, u) => new
            {
                data = d,
                user = u
            }).SelectMany(d => d.user.DefaultIfEmpty(), (d, u) => new
            {
                d.data.StorerKey,
                d.data.Channel,
                //d.data.RouteNo,
                //d.VehicleKey,
                d.data.FullName,
                d.data.ShortName,
                d.data.DeliveryDate,
                d.data.OrderType,
                d.data.ExternOrderKey,
                d.data.TMSKey,
                d.data.SignStatus,
                d.data.ConfirmNotes,
                d.data.Customerorderkey,
                d.data.Invback,
                //d.data.ScheduleDate,
                d.data.SdnSendDate,
                d.data.AreaCode,
                d.data.Company,
                d.data.Facility,
                d.data.SdnNotes,
                d.data.Status,
                d.data.SdnSendWho,
                DisplayName = (u != null) ? u.DisplayName : ""
            });
            //重新查詢欄位資料
            ret.rows = newrows;  
            return ret;
        }

        /// <summary>
        /// 
        /// </summary>  
        public IEnumerable<ReportSDNReturnList> RetrievesExcel()
        {
            var storers = string.IsNullOrEmpty(StorerKeys) ? "" : string.Join(",", StorerKeys.Split(",").Select(p => string.Format("'{0}'", p)).ToArray());
            //var routeno = string.IsNullOrEmpty(RouteNo) ? "" : RouteNo;
            var areacodes = string.IsNullOrEmpty(AreaCodes) ? "" : string.Join(",", AreaCodes.Split(",").Select(p => string.Format("'{0}'", p)).ToArray());
            var companies = string.IsNullOrEmpty(Companies) ? "" : string.Join(",", Companies.Split(",").Select(p => string.Format("'{0}'", p)).ToArray());
            var ordertypes = string.IsNullOrEmpty(OrderTypes) ? "" : string.Join(",", OrderTypes.Split(",").Select(p => string.Format("'{0}'", p)).ToArray());
            var facilies = string.IsNullOrEmpty(Facilities) ? "" : string.Join(",", Facilities.Split(",").Select(p => string.Format("'{0}'", p)).ToArray());
            var orderstatus = string.IsNullOrEmpty(OrderStatus) ? "" : string.Join(",", OrderStatus.Split(",").Select(p => string.Format("'{0}'", p)).ToArray());
            var confirmdates = ConfirmDateS.HasValue ? DataComparison.DateTimeConvert(ConfirmDateS) : "";
            var confirmdatee = ConfirmDateE.HasValue ? DataComparison.DateTimeConvert(ConfirmDateE) : "";
            var deliverydates = DeliveryDateS.HasValue ? DataComparison.DateTimeConvert(DeliveryDateS) : "";
            var deliverydatee = DeliveryDateE.HasValue ? DataComparison.DateTimeConvert(DeliveryDateE) : "";
            var tmskeys = TMSKeys;
            var externorderkeys = ExternOrderKeys;
            var ordermethod = OrderMethod;
            

            var dataCount = 0;
            var data = InternalQueryData(ReportSDNReturnListHelper.Retrieves(storers, ordertypes, areacodes, companies, facilies, orderstatus, tmskeys, externorderkeys, ordermethod, confirmdates, confirmdatee, deliverydates, deliverydatee), out dataCount);
            return data;
        }

        /// <summary>
        /// 
        /// </summary>  
        public IEnumerable<ReportSDNReturnList> RetrievesExcelDetail(string tmskeys)
        {
            var ordermethod = OrderMethod;
            
            var dataCount = 0;
            var data = InternalQueryData(ReportSDNReturnListHelper.RetrieveExcelDetail(tmskeys,ordermethod), out dataCount);
            return data;
        }
    }
}
