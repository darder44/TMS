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
    /// 請付款報表
    /// </summary>
    public class ReportRequest
    {
        /// <summary>
        /// 貨主代號
        /// </summary>
        public string StorerKey { get; set; }
        public string CostKind { get; set; }
        public string DeliveryDate { get; set; }
        public string VehicleKey { get; set; }
        public string AreaStart { get; set; }
        public string AreaEnd { get; set; }
        public string CustName { get; set; }
        public string ExternOrderKey { get; set; }
        public double ChargeQty { get; set; }
        public string Uom { get; set; }
        public double Receivable { get; set; }
        public double SumReceivable { get; set; }
        public string Channel { get; set; }
        public string RouteNo { get; set; }
        public string CostName { get; set; }
        public string Sequence { get; set; }
        public string DoRouteDate { get; set; }
        public string OrderDate { get; set; }
        public string SignDate { get; set; }
        public string Driver { get; set; }
        public string Receiver { get; set; }
        public string Code { get; set; }
        public double Payable { get; set; }
        public double Premium { get; set; }
        public string Notes { get; set; }
        public double StdReceivable { get; set; }
        public double StdPayable { get; set; }
        public string Facility { get; set; }
        public double ARDistribution { get; set; }
        public double SumPayable { get; set; }
        public double SumPremium { get; set; }
        public string department { get; set; }
        public string TMSKey { get; set; }
        public string ShipToAddress { get; set; }
        public string CostStatus { get; set; }
        public string ConsigneeKey { get; set; }
        public string Reason { get; set; }
        public string CostCode { get; set; }
        public string SdnSendDate { get; set; }
        public string SdnSendWho { get; set; }
        public string CheckDate { get; set; }
        public string ConfirmNotes { get; set; }
        public string SdnNotes { get; set; }
        public string CostDate { get; set; }
        public string APnoDistribution { get; set; }
        public string ChannelCode { get; set; }


        public virtual IEnumerable<ReportRequest> Retrieves(string storerkey, string facility, string doroutedates, string doroutedatee, string deliverydates, string deliverydatee, string reporttype)
        {
            var strreporttype = string.IsNullOrEmpty(reporttype) ? "CostDetail" : reporttype.ToString().Trim();

            return DbManager.Create("bestlogtms").FetchProc<ReportRequest>(
            "SPReportRequest", new
            {
                StorerKey = storerkey,
                Facility = facility,
                DoRouteDateS = doroutedates,
                DoRouteDateE = doroutedatee,
                DeliveryDateS = deliverydates,
                DeliveryDateE = deliverydatee,
                ReportType = strreporttype
            }
            );
        }

    }
}