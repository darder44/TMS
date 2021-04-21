
using PetaPoco;
using System;
using System.Collections.Generic;
using Longbow.Web.Mvc;
using System.Linq;

namespace Bootstrap.Client.DataAccess.ShipListReport
{
    /// <summary>
    /// 
    /// </summary>
    public class LIAH01ShipListReport : ShipListReport
    {
        public string OrderKey { get; set; }
        public string ExternOrderKey { get; set; }
        public string DeliveryDate { get; set; }
        public string company { get; set; }
        public string shippingNumber { get; set; }
        public string shippingDocumentType { get; set; }
        public string orderDocumentTypeDescription { get; set; }
        public string categoryCode15 { get; set; }
        public string year { get; set; }
        public string month { get; set; }
        public string day { get; set; }
        public string soldToAddressNumber { get; set; }
        public string soldToAddressDescription { get; set; }
        public string phoneAreaCode { get; set; }
        public string phoneNumber { get; set; }
        public string customerOrderNumber { get; set; }
        public string shipToAddressLine1 { get; set; }
        public string shipToAddressDescription { get; set; }
        public string shipToAddressLine2 { get; set; }
        public string salesmanDescription { get; set; }
        public string orderDocumentType { get; set; }
        public string transferToAddressDescription { get; set; }
        public string shipToAddressNumber { get; set; }
        public string relatedOrderType { get; set; }
        public string relatedOrderNumber { get; set; }
        public string remark { get; set; }
        public string carNumber { get; set; }
        public string carSequence { get; set; }
        public string userId { get; set; }
        public string runTime { get; set; }
        public string promisedDeliveryDate { get; set; }
        public string location { get; set; }
        public string LineNumber { get; set; }
        public string ExternLineNumber { get; set; }
        public string itemNumber { get; set; }
        public string itemDescription { get; set; }
        public string shippingQty { get; set; }
        public string unitDescription { get; set; }
        public double unitPrice { get; set; }
        public double extendedPrice { get; set; }
        public string cubicMeter { get; set; }
        public string cartons { get; set; }
        public double taxableAmount { get; set; }
        public double tax { get; set; }
        public double totalAmount { get; set; }
        public string freight { get; set; }
        public string capacity { get; set; }
        public string isPriceVisiabled { get; set; }
        public string taxRate { get; set; }
        public string unitOfMeasure { get; set; }
        public string TotalPrice { get; set; }
        public string VehicleKey { get; set; }
        public string Driver { get; set; }
        public string Lottable03 { get; set; }
        public string RouteNo { get; set; }
        public string DriverPhone { get; set; }
        public string Phone { get; set; }
        public string ShippingEA { get; set; }
        public string BUSR1 { get; set; }
        public double SumCSQty { get; set; }
    }
}
