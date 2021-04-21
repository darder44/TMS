using Bootstrap.Security.DataAccess;
using PetaPoco;
using System;
using System.Collections.Generic;
using Longbow.Web.Mvc;
using System.Linq;

namespace Bootstrap.Client.DataAccess
{
    /// <summary>
    /// 
    /// </summary>
    [TableName("VLLxDock")]
    public class VLLxDock
    {
        //public string Id { get; set; } //前端呼叫新增編輯用
        public string RouteNo { get; set; }
        public string Dock { get; set; }
        public string Notes { get; set; }
        public string Driver { get; set; }
        public string VehicleKey { get; set; }
        public string CaseQty { get; set; }
        public string Cube { get; set; }
        public string Weight { get; set; }
        public string PalletQty { get; set; }
        public string ExpectTime { get; set; }
        public string Stop { get; set; }
        public string C_Company { get; set; }
        public string Facility { get; set; }
        public DateTime? ExpectDate { get; set; }
        public string SoldTo { get; set; }
        public string ShipTo { get; set; }
        public string SoldToName { get; set; }
        public string ShipToName { get; set; }

        //查詢資料
        public virtual IEnumerable<VLLxDock> Retrieves(string Facility) => DbManager.Create("bestlogtms").Fetch<VLLxDock>(
            "SELECT " +
                "RouteNo = rh.RouteNo, " +
                "Dock = rtrim(isnull(dw.Dock,'')) , " +
                "Notes = rtrim(isnull(bd.Notes,'')), " +
                "Driver = br.Driver, " +
                "VehicleKey = br.VehicleKey, " +
                "CaseQty = SUM(rh.CaseQty), " +
                "Cube = SUM(round(rh.Cube,3)), " +
                "Weight = SUM(round(rh.Weight,3)), " +
                "PalletQty = SUM(round(rh.PalletQty,3)), " +
                "ExpectTime = ISNULL(br.ExpectTime,''), " +
                "Stop = case when ow.stop = '99' then '' else ow.stop end , " +
                "C_Company = ISNULL(ow.C_Company,''),  " +
                "ExpectDate = br.ExpectDate, " +
                "SoldTo = RTRIM(o.SoldTo), " +
                "ShipTo = RTRIM(o.ShipTo), " +
                "SoldToName = RTRIM(o.SoldToName), " +
                "ShipToName = RTRIM(o.ShipToName) " +
            " FROM Orders o  " +  
                "join RouteHeader rh on o.TMSKey = rh.TMSKey " +  
                "join BestRoute br on rh.RouteNo = br.RouteNo " +  
                "join BestLogWMS..ORDERS ow on o.UpdateSource = ow.OrderKey " +  
                "left join BestLogWMS..DockxWave  dw on dw.WaveKey = ow.stop " +  
                "left join BestLogWMS..BaseDock bd on dw.Dock = bd.Dock " + 
            "WHERE " +
                "o.facility = @0 " +
            "GROUP BY " +
                "rh.RouteNo,dw.Dock,bd.Notes, BR.Driver, BR.VehicleKey,ExpectTime,case when ow.stop = '99' then '' else ow.stop end,ISNULL(OW.C_Company,''), br.ExpectDate   " +  
                ", o.SoldTo, o.ShipTo, o.SoldToName, o.ShipToName " +
            "ORDER BY " +
                "ISNULL(br.ExpectDate,''),ISNULL(C_Company,''),rh.RouteNo,case when ow.stop = '99' then '' else ow.stop end " , Facility
        );
    }

}

