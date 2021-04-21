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
    /// 出貨單
    /// </summary>

    public class ReportShipment
    {
        /// <summary>
        /// 路線編號
        /// </summary>
        public string RouteNo { get; set; }
        /// <summary>
        /// 出車日期
        /// </summary>
        public DateTime? DoRouteDate { get; set; }
        /// <summary>
        /// 到貨日期
        /// </summary>
        public DateTime? DeliveryDate { get; set; }
        /// <summary>
        /// 司機
        /// </summary>
        public string Driver { get; set; }
        /// <summary>
        /// 車號
        /// </summary>
        public string VehicleKey { get; set; }
        /// <summary>
        /// 出貨箱數
        /// </summary>
        public string ShipCaseQty { get; set; }
        /// <summary>
        /// 出貨個數
        /// </summary>
        public string ShipQty { get; set; }
        /// <summary>
        /// 訂單數量
        /// </summary>
        public string OrderQty { get; set; }
        /// <summary>
        /// 材積
        /// </summary>
        public string Cube { get; set; }
        /// <summary>
        /// 重量
        /// </summary>
        public string Weight { get; set; }
    

        public virtual IEnumerable<ReportShipment> Retrieves(string storers, string routeno, string carleavedates, string carleavedatee, string deliverydates, string deliverydatee, string facility) => DbManager.Create("bestlogtms").FetchProc<ReportShipment>(
            "SPReportShipment", new
            {
                Warehouse = "BestLogWMS",
                StorerKey = storers,
                RouteNo = routeno,
                CarLeaveDateS = carleavedates,
                CarLeaveDateE = carleavedatee,
                DeliveryDateS = deliverydates,
                DeliveryDateE = deliverydatee
            }
        );

            /*public virtual IEnumerable<ReportShipment> Retrieves() => DbManager.Create("bestlogtms").Fetch<ReportShipment>(  
            "select " +
                "StorerKey = rtrim(o.StorerKey)" +
	            ",RouteNo = rtrim(br.RouteNo) " +
	            ",DoRouteDate =  br.ExpectDate " +
	            ",VLListCount = br.VLListCount " +  
	            ",VLListPrintDate = case when VLListPrintDate is null then '' else convert(char(19),VLListPrintDate,120) end " + 
	            ",FullName = rtrim(bc.FullName) " +
	            ",Address = rtrim(o.ShipToAddress) " +
			    ",ConsigneeKey = rtrim(o.ConsigneeKey) " +
			    ",Phone = rtrim(bc.Phone) " +
			    ",Notes = rtrim(o.Notes) " +
            "From BestRoute br " +
	            "join RouteHeader rh on br.RouteNo = rh.RouteNo " +  
	            "join RouteDetail rd on rh.TMSKey = rd.TMSKey " +   
	            "join Orders o on rd.TMSKey = o.TMSKey " +
	            "join BaseVehicle bv on br.VehicleKey = bv.VehicleKey " +
	            "join BaseCustomer bc on o.ConsigneeKey = bc.ConsigneeKey and o.StorerKey = bc.StorerKey " +   
                "join BestLogWMS..Sku s on s.sku = rd.sku and s.storerkey = rd.storerkey " + 
                "join BestLogWMS..Pack p on s.packkey = p.packkey " + 
                "group by rtrim(o.StorerKey),rtrim(br.RouteNo),br.ExpectDate,br.VLListCount,rtrim(br.VehicleKey),rtrim(bc.FullName),br.DriveTimes,rtrim(br.Driver) " +
                ",rtrim(o.ShipToAddress), rtrim(o.ConsigneeKey), rtrim(bc.Phone), rtrim(o.Notes) " +
	            ",case when VLListPrintDate is null then '' else convert(char(19),VLListPrintDate,120) end,rtrim(bv.CompanyKey) "
            );*/
    }
}
