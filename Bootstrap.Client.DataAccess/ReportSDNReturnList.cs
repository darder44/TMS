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
    /// 回單檢核表
    /// </summary>
    public class ReportSDNReturnList
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
        /// 路編
        /// </summary>
        public string RouteNo { get; set; }
        /// <summary>
        /// 車號
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
        /// 貨主單號
        /// </summary>
        public string ExternOrderKey { get; set; }
        /// <summary>
        /// 訂單號碼
        /// </summary>
        public string TMSKey { get; set; }
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
        public DateTime? ScheduleDate { get; set; }
        /// <summary>
        /// 簽單確認時間
        /// </summary>
        public DateTime? SdnSendDate { get; set; }
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
        /// 簽單類別
        /// </summary>
        public string Status { get; set; }
        /// <summary>
        /// 維護者
        /// </summary>
        public string SdnSendWho { get; set; }
        /// <summary>
        /// 車次
        /// </summary>
        public string Sequence { get; set; }
        /// <summary>
        /// 來源倉別
        /// </summary>
        public string FromFacility { get; set; }
        /// <summary>
        /// 目的倉別
        /// </summary>
        public string ToFacility { get; set; }
        /// <summary>
        /// 來源地址
        /// </summary>
        public string StartAddress { get; set; }
        /// <summary>
        /// 目的地址
        /// </summary>
        public string EndAddress { get; set; }
        /// <summary>
        /// 司機
        /// </summary>
        public string Driver { get; set; }
        /// <summary>
        /// 新增人
        /// </summary>
        public DateTime? AddDate { get; set; }
        /// <summary>
        /// 新增時間
        /// </summary>
        public string AddWho { get; set; }
        /// <summary>
        /// 簽單備註
        /// </summary>
        public string SdnNotes { get; set; }

        public virtual IEnumerable<ReportSDNReturnList> Retrieves(string storers, string ordertypes, string areacodes, string companies, string facilies, string orderstatus, string tmskeys, string externorderkeys, string ordermethod, string confirmdates, string confirmdatee, string deliverydates, string deliverydatee) => DbManager.Create("bestlogtms").FetchProc<ReportSDNReturnList>(
            "SPReportSDNReturnList", new
            {
                StorerKey = storers,
                OrderType = ordertypes,
                AreaCode = areacodes,
                Company = companies,
                Facility = facilies,
                //RouteNo = routeno,
                ConfirmDateS = confirmdates,
                ConfirmDateE = confirmdatee,
                DeliveryDateS = deliverydates,
                DeliveryDateE = deliverydatee,
                ExternOrderKey = externorderkeys,
                TMSKey = tmskeys,
                OrderMethod = ordermethod,
                OrderStatus = orderstatus,
                Parameter = "Header"
            }
        );

        public virtual IEnumerable<ReportSDNReturnList> RetrieveDetail(string TMSKey, string ordermethod) => DbManager.Create("bestlogtms").FetchProc<ReportSDNReturnList>(
            "SPReportSDNReturnList", new
            {
                StorerKey = "",
                OrderType = "",
                AreaCode = "",
                Company = "",
                Facility = "",
                //RouteNo = "",
                ConfirmDateS = "",
                ConfirmDateE = "",
                DeliveryDateS = "",
                DeliveryDateE = "",
                ExternOrderKey = "",
                TMSKey = TMSKey,
                OrderMethod = ordermethod,
                OrderStatus = "",
                Parameter = "Detail"
            }
        );

        public virtual IEnumerable<ReportSDNReturnList> RetrieveExcelDetail(string TMSKey, string ordermethod)
        {
            string strOrderBy = "";
            if(ordermethod == "0") strOrderBy = "";
            else if(ordermethod == "1") strOrderBy = " rtrim(o.SdnSendWho),convert(char(19),o.SdnSendDate,121) ";
            else if(ordermethod == "2") strOrderBy = " rtrim(o.ExternOrderKey) ";
            else if(ordermethod == "3") strOrderBy = " rtrim(isnull(bc.channel,'')),rtrim(isnull(bc.ShortName,'')) ";
            strOrderBy = string.IsNullOrEmpty(strOrderBy) ? "" :  " Order by " + strOrderBy;
            string strSQL = 
                "Select  " +
                    "RouteNo = rtrim(ot.RouteNo)   " +
                    ",Sequence = ot.Sequence " +
                    ",TMSKey = rh.TMSKey " +
                    ",ExternOrderKey = rh.ExternOrderKey " +
                    ",Channel = rtrim(isnull(bc.channel,'''')) " +
			        ",ShortName = rtrim(isnull(bc.ShortName,'''')) " +
                    ",FromFacility = ot.FromFacility " +
                    ",ToFacility = ot.ToFacility " +
                    ",ScheduleDate = convert(char(10),rh.ScheduleDate,111) " +
                    ",StartAddress = ot.StartAddress " +
                    ",EndAddress = ot.EndAddress " +
                    ",Driver = ot.Driver " +
                    ",VehicleKey = ot.VehicleKey " +
                    ",Status = case when len(rtrim(ot.Tofacility)) = 0 then '直達' when len(rtrim(ot.ErrCode)) = 0 then '異常重組' else '轉運' end " +
                    ",AddDate = ot.AddDate " +
                    ",AddWho = ot.AddWho " +
                    ",SdnSendDate = convert(char(19),o.SdnSendDate,121) " +
			        ",SdnSendWho = rtrim(o.SdnSendWho) " +
                "from OrderTrack ot " +
                    "join RouteHeader rh on ot.TMSKey = rh.TMSKey and ot.RouteNo = rh.RouteNo " +
                    "join BestRoute br on rh.RouteNo = br.RouteNo " +
                    "join Orders o on ot.TMSKey = o.TMSKey " +
                    "join BaseCustomer bc on o.ConsigneeKey = bc.ConsigneeKey and o.StorerKey = bc.StorerKey " +
                    "left join BaseShippingCompany bsc on br.CompanyKey = bsc.CompanyKey " +
                "Where ot.TMSKey in (" + TMSKey + ") " +                
                strOrderBy ;
                //"Order by rh.ExternOrderKey,rtrim(ot.RouteNo) ";
            return DbManager.Create("bestlogtms").Fetch<ReportSDNReturnList>(strSQL);
        }

    }
}