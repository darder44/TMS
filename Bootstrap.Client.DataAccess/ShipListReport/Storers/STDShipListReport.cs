
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
    public class STDShipListReport : ShipListReport
    {        
        public string StorerKey { get; set; }
        public string StorerName { get; set; }
        public string RouteNo { get; set; }
        public string DoRouteDate { get; set; }
        public string DeliveryDate { get; set; }
        public string ExternOrderKey { get; set; }
        public string TMSKey { get; set; }
        public string CustomerOrderKey { get; set; }
        public string OrderType { get; set; }
        public string FullName { get; set; }
        public string Address { get; set; }
        public string ConsigneeKey { get; set; }
        public string Phone { get; set; }
        public string Notes { get; set; }
        public string Driver { get; set; }
        public string VehicleKey { get; set; }
        public string ExternLineNumber { get; set; }
        public string Sku { get; set; }
        public string Susr3 { get; set; }
        public string Descr { get; set; }
        public string Lottable06 { get; set; }
        public string ShipCaseQty { get; set; }
        public string Busr3 { get; set; }
        public string ShipQty { get; set; }
        public string TotalShipQty { get; set; }
        public string Busr1 { get; set; }
        public string OrderQty { get; set; }
        public string Cube { get; set; }
        public string Weight { get; set; }
        public string ShipToName { get; set; }
        public string ContactPhone { get; set; }
        public string Contact { get; set; }
        public string CaseCnt { get; set; }
    } 
}
