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
    /// VLL裝載總表
    /// </summary>
    public class ReportVLLRouteList
    {
        /// <summary>
        /// 路線編號
        /// </summary>
        public string RouteNo { get; set; }
        /// <summary>
        /// 出車日期
        /// </summary>
        public string ExpectDate { get; set; }
        /// <summary>
        /// 列印次數
        /// </summary>
        public string VLListCount { get; set; }
        /// <summary>
        /// 列印時間
        /// </summary>
        public string VLListPrintDate { get; set; }
        /// <summary>
        /// 車牌號碼
        /// </summary>
        public string VehicleKey { get; set; }
        /// <summary>
        /// 駕駛人
        /// </summary>
        public string Driver { get; set; }
        /// <summary>
        /// 車次
        /// </summary>
        public string DriveTimes { get; set; }
        /// <summary>
        /// 運輸公司
        /// </summary>
        public string CompanyKey { get; set; }
        /// <summary>
        /// 排車個數
        /// </summary>
        public string OrderQty { get; set; }
        /// <summary>
        /// 揀貨個數
        /// </summary>
        public string ShipQty { get; set; }
        /// <summary>
        /// 揀貨箱數
        /// </summary>
        public string ShipCaseQty { get; set; }
        /// <summary>
        /// 揀貨總個數
        /// </summary>
        public string TotalShipQty { get; set; }
        /// <summary>
        /// 排車材積
        /// </summary>
        public string Cube { get; set; }
        /// <summary>
        /// 排車重量
        /// </summary>
        public string Weight { get; set; }
        /// <summary>
        /// 到貨時間(起)
        /// </summary>
        public DateTime? DeliveryDate_Start { get; set; }
        /// <summary>
        /// 到貨時間(訖)
        /// </summary>
        public DateTime? DeliveryDate_end { get; set; }
        /// <summary>
        /// 路線編號(起)
        /// </summary>
        public string RouteNo_Start { get; set; }
        /// <summary>
        /// 路線編號(迄)
        /// </summary>
        public string RouteNo_end { get; set; }

    //報表列印
        /// <summary>
        /// 
        /// </summary>
        public string DoRouteDate { get; set; } 
        /// <summary>
        /// 
        /// </summary>
        public string ExternOrderKey { get; set; }       
        /// <summary>
        /// 
        /// </summary>
        public string ConsigneeKey { get; set; }       
        /// <summary>
        /// 
        /// </summary>
        public string CustName { get; set; }       
        /// <summary>
        /// 
        /// </summary>
        public string ShipToAddress { get; set; }       
        /// <summary>
        /// 
        /// </summary>
        public string Notes { get; set; }       
        /// <summary>
        /// 
        /// </summary>
        public string PalletQty { get; set; }  
        /// <summary>
        /// 
        /// </summary>
        public string CaseQty { get; set; } 
        /// <summary>
        /// 
        /// </summary>
        public string TotalQty { get; set; } 
        /// <summary>
        /// 
        /// </summary>
        public string OrderDate { get; set; } 
        /// <summary>
        /// 
        /// </summary>
        public string ExpectTime { get; set; } 
        /// <summary>
        /// 
        /// </summary>
        public string DockNo { get; set; } 
        /// <summary>
        /// 
        /// </summary>
        public string TMSKey { get; set; } 
        /// <summary>
        /// 
        /// </summary>
        public string AddWho { get; set; } 
        /// <summary>
        /// 
        /// </summary>
        public string OrderType { get; set; } 
        /// <summary>
        /// 
        /// </summary>
        public string OTQty { get; set; } 
        /// <summary>
        /// 
        /// </summary>
        public string Facility { get; set; } 
        /// <summary>
        /// 
        /// </summary>
        public string StorerKey { get; set; } 
        /// <summary>
        /// 
        /// </summary>
        public string UpdateSource { get; set; } 
        /// <summary>
        /// 
        /// </summary>
        public string WaveKey { get; set; } 
        /// <summary>
        /// 
        /// </summary>
        public string Sku { get; set; } 
        /// <summary>
        /// 
        /// </summary>
        public string DESCR { get; set; } 
        /// <summary>
        /// 
        /// </summary>
        public string Lotable03 { get; set; } 
        /// <summary>
        /// 
        /// </summary>
        public string Lotable04 { get; set; } 
        /// <summary>
        /// 
        /// </summary>
        public string Lotable05 { get; set; } 

        public virtual IEnumerable<ReportVLLRouteList> Retrieves(string facility, string wavekey, string routenostart, string routenoend, string doroutedatestart, string doroutedateend){

            return DbManager.Create("bestlogtms").FetchProc<ReportVLLRouteList>(
                "SPReportVLLRouteList", new{ Facility = facility
                                            , WaveKey = wavekey
                                            , RouteNoS = routenostart
                                            , RouteNoE = routenoend
                                            , DoRouteDateS = doroutedatestart
                                            , DoRouteDateE = doroutedateend
                                            , RouteNo = ""
                                            , Type = "Header"
                });

        }

        public virtual IEnumerable<ReportVLLRouteList> RetrievesDetail( string routeno){

            return DbManager.Create("bestlogtms").FetchProc<ReportVLLRouteList>(
                "SPReportVLLRouteList", new{ Facility = ""
                                            , WaveKey = ""
                                            , RouteNoS = ""
                                            , RouteNoE = ""
                                            , DoRouteDateS = ""
                                            , DoRouteDateE = ""
                                            , RouteNo = routeno
                                            , Type = "Detail"
                });

        }

        public virtual IEnumerable<ReportVLLRouteList> RetrievesExcel(string facility, string wavekey, string routenostart, string routenoend, string doroutedatestart, string doroutedateend){

            return DbManager.Create("bestlogtms").FetchProc<ReportVLLRouteList>(
                "SPReportVLLRouteList", new{ Facility = facility
                                            , WaveKey = wavekey
                                            , RouteNoS = routenostart
                                            , RouteNoE = routenoend
                                            , DoRouteDateS = doroutedatestart
                                            , DoRouteDateE = doroutedateend
                                            , RouteNo = ""
                                            , Type = "Excel"
                });

        }

        public virtual IEnumerable<ReportVLLRouteList> RetrieveByRouteNo(IEnumerable<string> RouteNos, string facility)
        {

            //20200929  改為BY送貨客編彙總列印
            var db = DbManager.Create("bestlogtms");
            var routeno = string.Join(",", RouteNos.Select(p => string.Format("'{0}'", p.ToString())).ToArray());
            db.Execute($"UPDATE BestRoute SET VLListCount = ISNULL(VLListCount, 0) + 1, VLListPrintDate = GETDATE() WHERE RouteNo IN ({routeno})");
            string strSQL = 
                "select " +
                "Distinct " +
                "DoRouteDate = convert(char(10),br.DoRouteDate,111) " +
                ",RouteNo = rtrim(br.RouteNo) " +
                ",VehicleKey = rtrim(br.VehicleKey) " +
                ",DriveTimes = br.DriveTimes " +
                ",Driver = rtrim(br.Driver) " +
                ",CompanyKey = rtrim(bv.CompanyKey) " +
                ",ConsigneeKey = rtrim(rh.ConsigneeKey) " +
                ",CustName = rtrim(bs.ChineseName) + '_' + rtrim(o.ShortName) " +
                ",ShipToAddress = rtrim(o.ShipToAddress) " +
                ",Notes = rtrim(o.Notes) " +
                ",PalletQty = sum(case when p.Pallet = 0 then 0 else floor(od.OrderQty / p.Pallet) end) " +
                ",CaseQty = sum(case when p.CaseCnt = 0 then 0 else floor(od.OrderQty / p.CaseCnt) end) " +
                ",OrderQty = sum(case when p.CaseCnt = 0 then od.OrderQty else od.OrderQty - floor(od.OrderQty / p.CaseCnt) * p.CaseCnt end) " +
                ",TotalQty = sum(od.OrderQty) " +
                ",Cube = Round(sum(od.OrderQty * od.StdCube), 3) " +
                ",Weight = ROUND(sum(od.OrderQty * od.StdWeight), 3) " +
                ",OrderDate = convert(char(10),o.OrderDate,111) " +
                ",ExpectDate = convert(char(10),ExpectDate,111) " +
                ",ExpectTime = br.ExpectTime " +
                ",DockNo = rtrim(br.DockNo) " +
                ",VLListCount = br.VLListCount " +
                ",VLListPrintDate = convert(varchar, br.VLListPrintDate, 120) " +
                ",AddWho = rtrim(br.AddWho) " +
                ",OrderType = rtrim(o.OrderType) " +
                ",OTQty = rh.OTQty " +
                ",Facility = case when rtrim(o.Facility) = 'HCDC' then '新澄倉' when rtrim(o.Facility) = 'DYDC' then '大園倉' else '觀音倉' end " +
                "from BestRoute br " +
                "join RouteHeader rh on br.RouteNo = rh.RouteNo " +
                "join Orders o on rh.TMSKey = o.TMSKey " +
                "join OrderDetail od on o.TMSKey = od.TMSKey " +
                "join BaseCustomer bc on o.ConsigneeKey = bc.ConsigneeKey and o.StorerKey = bc.StorerKey " +
                "join BaseVehicle bv on br.VehicleKey = bv.VehicleKey AND br.Driver = bv.Driver " +
                "join BaseStorer bs on o.StorerKey = bs.StorerKey " +
                "join BestLogWMS..Sku s on s.Sku = od.Sku and s.StorerKey = od.StorerKey " +
                "join BestLogWMS..Pack p on s.PackKey = p.PackKey " +
                "where rtrim(br.RouteNo) in (" + routeno + ") " +
                "group by convert(char(10),br.DoRouteDate,111) "+ 
                " ,rtrim(br.RouteNo)  ,rtrim(br.VehicleKey)  "+
                ",br.DriveTimes  ,rtrim(br.Driver)  ,rtrim(bv.CompanyKey) ,rtrim(rh.ConsigneeKey)  "+
                " ,rtrim(bs.ChineseName) + '_' + rtrim(o.ShortName)  "+
                " ,rtrim(o.ShipToAddress)  ,rtrim(o.Notes)  ,convert(char(10),o.OrderDate,111)   ,convert(char(10),ExpectDate,111)  "+
                " ,br.ExpectTime, rtrim(br.DockNo), br.VLListCount, convert(varchar, br.VLListPrintDate, 120) " + //,rh.TMSKey  ,rtrim(rh.ExternOrderKey)  
                " ,rtrim(br.AddWho)  "+
                " ,rtrim(o.OrderType)  ,rh.OTQty  ,case when rtrim(o.Facility) = 'HCDC' then '新澄倉' when rtrim(o.Facility) = 'DYDC' then '大園倉'  else '觀音倉' end  ";
            return db.Fetch<ReportVLLRouteList>(strSQL);
        }

        public virtual IEnumerable<ReportVLLRouteList> RetrieveDetailByRouteNo(IEnumerable<string> RouteNos, string facility)
        {
            var db = DbManager.Create("bestlogtms");
            var routeno = string.Join(",", RouteNos.Select(p => string.Format("'{0}'", p.ToString())).ToArray());
            db.Execute($"UPDATE BestRoute SET VLListCount = ISNULL(VLListCount, 0) + 1, VLListPrintDate = GETDATE() WHERE RouteNo IN ({routeno})");
            string strSQL = 
                " select " +
                " Distinct " +
                " DoRouteDate = convert(char(10),br.DoRouteDate,111) " + 
                " ,RouteNo = rtrim(br.RouteNo) " +
                " ,VehicleKey = rtrim(br.VehicleKey) " +
                " ,DriveTimes = br.DriveTimes " +
                " ,Driver = rtrim(br.Driver) " +
                " ,CompanyKey = rtrim(bv.CompanyKey) " +
                " ,ConsigneeKey = rtrim(rh.ConsigneeKey)  " +
                " ,CustName = rtrim(bs.ChineseName) +'_'+rtrim(o.ShortName)  " +
                " ,Sku = RTRIM(RD.Sku) " +
                " ,DESCR = RTRIM(S.DESCR) " +
                " ,ShipToAddress = rtrim(o.ShipToAddress)  " +
                " ,Notes = rtrim(o.Notes)  " +
                " ,PalletQty = sum(case when p.Pallet = 0 then 0 else floor(pd.qty / p.Pallet) end)  " +
                " ,CaseQty = sum(case when p.CaseCnt = 0 then 0 else floor(pd.qty / p.CaseCnt) end)  " +
                " ,OrderQty = sum(case when p.CaseCnt = 0 then pd.qty else pd.qty - floor(pd.qty / p.CaseCnt) * p.CaseCnt end)  " +
                " ,TotalQty = sum(pd.qty)  " +
                " ,Cube = Round(sum(pd.qty * od.StdCube), 3)  " +
                " ,Weight = ROUND(sum(pd.qty * od.StdWeight), 3)  " +
                " ,OrderDate = convert(char(10),o.OrderDate,111)  " +
                " ,ExpectDate = convert(char(10),ExpectDate,111)  " +
                " ,ExpectTime = br.ExpectTime  " +
                " ,DockNo = rtrim(br.DockNo)  " +
                " ,VLListCount = br.VLListCount  " +
                " ,VLListPrintDate = convert(varchar, br.VLListPrintDate, 120)  " +
                " ,AddWho = rtrim(br.AddWho)  " +
                " ,OrderType = rtrim(o.OrderType)  " +
                " ,OTQty = rh.OTQty  " +
                " ,Facility = case when rtrim(o.Facility) = 'HCDC' then '新澄倉' when rtrim(o.Facility) = 'DYDC' then '大園倉' else '觀音倉' end " +
                " ,WaveKey= rtrim(ow.Stop) " +
                " ,ExternOrderKey = rtrim(ow.ExternOrderKey) " +
                " ,Lotable03 = rtrim(lta.Lottable03) " +
                " ,Lotable04 = convert(char(10),lta.Lottable04,111) " +
                " ,Lotable05 = convert(char(10),lta.Lottable05,111) " +
                " from BestRoute br  " +
                " join RouteHeader rh on br.RouteNo = rh.RouteNo  " +
                " join RouteDetail rd on rh.TMSKey = rd.TMSKey  " +
                " join Orders o on rh.TMSKey = o.TMSKey  " +
                " join OrderDetail od on o.TMSKey = od.TMSKey and rd.OrderLineNumber = od.OrderLineNumber  " +
                " join BaseCustomer bc on o.ConsigneeKey = bc.ConsigneeKey and o.StorerKey = bc.StorerKey  " +
                " join BaseVehicle bv on br.VehicleKey = bv.VehicleKey AND br.Driver = bv.Driver " +
                " join BaseStorer bs on o.StorerKey = bs.StorerKey  " +
                " join BestLogWMS..ORDERS ow on o.UpdateSource = ow.OrderKey " +
                " join BestLogWMS..ORDERDETAIL odw on ow.OrderKey = odw.OrderKey and od.OrderLineNumber = odw.OrderLineNumber  " +
                " join BestLogWMS..PICKDETAIL pd on odw.OrderKey = pd.OrderKey and odw.OrderLineNumber = pd.OrderLineNumber " +
                " join BestLogWMS..LOTATTRIBUTE lta on pd.lot = lta.lot " +
                " join BestLogWMS..Sku s on pd.Sku = S.Sku and s.StorerKey = pd.StorerKey " +
                " join BestLogWMS..Pack p on s.PackKey = p.PackKey " +
                $" where rtrim(br.RouteNo) in ({routeno})  " +
                " group by RTRIM(S.DESCR),rtrim(ow.ExternOrderKey),rtrim(ow.Stop),rtrim(lta.Lottable03),convert(char(10),lta.Lottable04,111),convert(char(10),lta.Lottable05,111),RTRIM(RD.Sku),convert(char(10),br.DoRouteDate,111)  " +
                " ,rtrim(br.RouteNo)  ,rtrim(br.VehicleKey)  " +
                " ,br.DriveTimes  ,rtrim(br.Driver)  ,rtrim(bv.CompanyKey) ,rtrim(rh.ConsigneeKey)  " +
                " ,rtrim(bs.ChineseName) +'_'+rtrim(o.ShortName)  " +
                " ,rtrim(o.ShipToAddress)  ,rtrim(o.Notes)  ,convert(char(10),o.OrderDate,111)   ,convert(char(10),ExpectDate,111)  " +
                " ,br.ExpectTime  ,rtrim(br.DockNo)  ,br.VLListCount  , convert(varchar, br.VLListPrintDate, 120) " +
                " ,rtrim(br.AddWho)  " +
                " ,rtrim(o.OrderType)  ,rh.OTQty  ,case when rtrim(o.Facility) = 'HCDC' then '新澄倉' when rtrim(o.Facility) = 'DYDC' then '大園倉'  else '觀音倉' end ";
            return db.Fetch<ReportVLLRouteList>(strSQL);
        }

        public virtual IEnumerable<ReportVLLRouteList> SelectTMSKeyAndOrderTypeByRouteNo(IEnumerable<string> routelist)
        {
            var routes = string.Join(",", routelist.Select(p => string.Format("'{0}'", p)));
            string strSQL = 
                "select distinct " +
                    "TMSKey = rh.TMSKey " +
                    ",OrderType = rh.OrderType " +
                    ",StorerKey = rh.StorerKey " +
                    ",UpdateSource = case when tmso.OrderType in ('R','RC','A2B') and tmso.StorerKey = 'LIAH01' then '手開單' else ISNULL(wmso.UpdateSource, '') end " +
                "from RouteHeader rh " +
                "left join Orders tmso on tmso.TMSKey = rh.TMSKey " + 
                "left join BestLogWMS..Orders wmso on wmso.OrderKey = tmso.UpdateSource " +
                "where rh.RouteNo in (" + routes + ")";
            return DbManager.Create("bestlogtms").Fetch<ReportVLLRouteList>(strSQL);
        }

    }
}