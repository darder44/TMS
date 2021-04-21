using Bootstrap.Security;
using Bootstrap.Security.DataAccess;
using Longbow.Security.Cryptography;
using Longbow.Web.Mvc;
using PetaPoco;
using System;
using System.Collections.Generic;
using System.Linq;
using Bootstrap.Client.DataAccess.Helper;
//using Bootstrap.Client.DataAccess.BestApp.Helper;
//using Bootstrap.Client.DataAccess.BestApp;
using Newtonsoft.Json;
using Longbow.Data;


namespace Bootstrap.Client.DataAccess
{
    /// <summary>
    /// 待排車訂單 BestLogTMS Orders
    /// </summary>
    public class NormalStandbyOrders
    {

        /// <summary>
        /// 貨主
        /// </summary>
        public string StorerKey { get; set; }
        /// <summary>
        /// 訂單號碼
        /// </summary>
        public string TMSKey { get; set; }
        /// <summary>
        /// 原始訂單號碼(切單用)
        /// </summary>
        public string OriginalKey { get; set; }
        /// <summary>
        /// 貨主單號
        /// </summary>
        public string ExternOrderKey { get; set; }
        /// <summary>
        /// 採購單號
        /// </summary>
        public string CustomerOrderKey { get; set; }
        /// <summary>
        /// 訂單類別
        /// </summary>
        public string OrderType { get; set; }
        /// <summary>
        /// 訂單日期
        /// </summary>
        public DateTime OrderDate { get; set; }
        /// <summary>
        /// 到貨日期
        /// </summary>
        public DateTime DeliveryDate { get; set; }
        /// <summary>
        /// 客戶編號
        /// </summary>
        public string ConsigneeKey { get; set; }
        /// <summary>
        /// 提貨客編
        /// </summary>
        public string PickUpConsigneeKey { get; set; }
        /// <summary>
        /// 提貨名稱
        /// </summary>
        public string PickUpName { get; set; }
        /// <summary>
        /// 提貨地址
        /// </summary>
        public string PickUpAddress { get; set; }
        /// <summary>
        /// 客戶簡稱
        /// </summary>
        public string ShortName { get; set; }
        /// <summary>
        /// SoldTo
        /// </summary>
        public string SoldTo { get; set; }
        /// <summary>
        /// SoldToName
        /// </summary>
        public string SoldToName { get; set; }
        /// <summary>
        /// SoldToAddress
        /// </summary>
        public string SoldToAddress { get; set; }
        /// <summary>
        /// ShipTo
        /// </summary>
        public string ShipTo { get; set; }
        /// <summary>
        /// ShipToName
        /// </summary>
        public string ShipToName { get; set; }
        /// <summary>
        /// ShipToAddress
        /// </summary>
        public string ShipToAddress { get; set; }
        /// <summary>
        /// 郵遞區號
        /// </summary>
        public string Zip { get; set; }
        /// <summary>
        /// 區碼
        /// </summary>
        public string AreaCode { get; set; }
        /// <summary>
        /// 聯絡人
        /// </summary>
        public string Contact { get; set; }
        /// <summary>
        /// 電話
        /// </summary>
        public string Phone { get; set; }
        /// <summary>
        /// 備註
        /// </summary>
        public string Notes { get; set; }
        /// <summary>
        /// 訂單箱數
        /// </summary>
        public double CaseQty { get; set; }
        /// <summary>
        /// 訂單板數
        /// </summary>
        public double PalletQty { get; set; }
        /// <summary>
        /// 訂單重量
        /// </summary>
        public double Weight { get; set; }
        /// <summary>
        /// 訂單材積
        /// </summary>
        public double Cube { get; set; }
        /// <summary>
        /// UrgentMark
        /// </summary>
        public string UrgentMark { get; set; }
        /// <summary>
        /// ReserveMark
        /// </summary>
        public string ReserveMark { get; set; }
        /// <summary>
        /// ColdMark
        /// </summary>
        public string ColdMark { get; set; }
        /// <summary>
        /// 原路編(轉運用)
        /// </summary>
        public string OriginalRouteNo { get; set; }
        /// <summary>
        /// ToFacility(轉運用)
        /// </summary>
        public string ToFacility { get; set; }
        /// <summary>
        /// Facility(訂單最初所屬倉別)
        /// </summary>
        public string Facility { get; set; }
        /// <summary>
        /// 訂單狀態(0有異動資料等待確認，1待排，2已排，3轉運，4已出車，5正常，6異常，7未出，8保留，9刪單)
        /// </summary>
        public string Status { get; set; }
        /// <summary>
        /// 更新時間
        /// </summary>
        public DateTime EditDate { get; set; }
        /// <summary>
        /// 更新人
        /// </summary>
        public string EditWho { get; set; }
        /// <summary>
        /// 維護用
        /// </summary>
        [ResultColumn]
        public bool IsTemp { get; set; } = false;
        /// <summary>
        /// 待排車新增欄位 WaveKey
        /// </summary>
        public string WaveKey { get; set; }
        /// <summary>
        /// 待排車新增欄位 倉庫狀態AllocateStatus
        /// </summary>
        public string AllocateStatus { get; set; }

        //抓取待排車訂單
        public virtual IEnumerable<NormalStandbyOrders> RetrievesByOrders(string facility) => DbManager.Create("bestlogtms").FetchProc<NormalStandbyOrders>(
            "SPNormalStandbyOrders", new { ToFacility = facility, Type = "StandbyOrders", Warehouse = "BestLogWMS"}//此參數放User所屬倉別(權限控制)，可抓到所屬倉別訂單資料，代空表示抓所有資料
        );

        //組成路編後抓取被組成訂單的路線編號
        public virtual string SelectRouteNo(IEnumerable<NormalStandbyOrders> SelectOrders)
        {
            string strRouteNo = "";
            var db = DbManager.Create("bestlogtms");
            try
            {
                var orderlist = "(" + string.Join(",", SelectOrders.Select(p => string.Format("'{0}'", p.TMSKey)).ToArray()) + ")";

                var routeno = db.Fetch<string>(
                    "select top 1 RouteNo from RouteHeader where TMSKey in " + orderlist + " Order by RouteNo desc "
                );
                strRouteNo = routeno.Count == 0 ? string.Empty : routeno.FirstOrDefault();
            }
            catch(Exception ex)
            {
                throw ex;
            }
            return strRouteNo;
        }

        //抓取比對資料(目前客戶主檔)
        public virtual NormalStandbyOrders RetrievesCustomerByConsigneeKeyAndStorerKey(string storerkey, string consigneekey, string tmskey, string facility)
        {
            var sql = new Sql("select " +
                "StorerKey = RTRIM(isnull(bc.StorerKey,'')) " +
                ",TMSKey = RTRIM(isnull(o.TMSKey,''))" +
                ",ExternOrderKey = rtrim(o.ExternOrderKey) " + 
                ",ConsigneeKey = RTRIM(isnull(bc.ConsigneeKey,'')) " +
                ",ShortName = RTRIM(isnull(bc.ShortName,'')) " +
                ",ShipToAddress = RTRIM(isnull(bc.ShipToAddress,'')) " +
                ",Zip = RTRIM(isnull(bc.Zip,'')) " +
                ",Contact = RTRIM(isnull(bc.Contact,'')) " +
                ",Phone = RTRIM(isnull(bc.Phone,'')) " +
            "from Orders o " +
                "left join BaseCustomer bc on o.ConsigneeKey = bc.ConsigneeKey and o.StorerKey = bc.StorerKey "
            );
            sql.Where("o.Status = '0' AND o.StorerKey = @0 AND o.ConsigneeKey = @1 AND o.TMSKey = @2", storerkey, consigneekey, tmskey);
            if (!string.IsNullOrEmpty(facility))
            {
                sql.Where("o.ToFacility = @0", facility);
            }
            return DbManager.Create("bestlogtms").SingleOrDefault<NormalStandbyOrders>(sql);
        }

        //抓取比對資料(Orders)
        public virtual IEnumerable<NormalStandbyOrders> RetrievesCustomerTemp(string facility)
        {
            var sql = new Sql("select " +
                "StorerKey = RTRIM(isnull(o.StorerKey,'')) " +
                ",TMSKey = RTRIM(isnull(o.TMSKey,''))" +
                ",ExternOrderKey = rtrim(o.ExternOrderKey) " + 
                ",ConsigneeKey = RTRIM(isnull(o.ConsigneeKey,'')) " +
                ",ShortName = RTRIM(isnull(o.ShortName,'')) " +
                ",ShipToAddress = RTRIM(isnull(o.ShipToAddress,'')) " +
                ",Zip = RTRIM(isnull(o.Zip,'')) " +
                ",Contact = RTRIM(isnull(o.Contact,'')) " +
                ",Phone = RTRIM(isnull(o.Phone,'')) " +
            "from Orders o " +
                "left join BaseCustomer bc on o.ConsigneeKey = bc.ConsigneeKey and o.StorerKey = bc.StorerKey ");
            sql.Where("o.Status = '0'");
            if (!string.IsNullOrEmpty(facility))
            {
                sql.Where("o.ToFacility = @0", facility);
            }
            return DbManager.Create("bestlogtms").Fetch<NormalStandbyOrders>(sql);
        }


        //抓取保留訂單
        public virtual IEnumerable<NormalStandbyOrders> RetrievesReturnOrders(string facility) => DbManager.Create("bestlogtms").FetchProc<NormalStandbyOrders>(
            "SPNormalStandbyOrders", new { ToFacility = facility ,Type = "ReserveOrders", Warehouse = "BestLogWMS"}//此參數放User所屬倉別(權限控制)，可抓到所屬倉別訂單資料，代空表示抓所有資料
        );


        //保存主檔異動資料
        public virtual ServerResponse SaveCustomer(NormalStandbyOrders value,string UserName, string facility)
        {
            ServerResponse res = new ServerResponse();
            var db = DbManager.Create("bestlogtms"); 
            try
            {
                db.BeginTransaction();
                //判斷是否已經被維護至待排車
                NormalStandbyOrders order = db.SingleOrDefault<NormalStandbyOrders>("SELECT EditWho, EditDate FROM Orders WHERE Status = '1' AND TMSKey = @0 AND Consigneekey = @1", value.TMSKey, value.ConsigneeKey);
                if (order != null)
                {
                    res.success = false;
                    res.message = $"此筆訂單已被維護至待排車，訂單號碼：{value.TMSKey}、維護人：{order.EditWho?.Trim()}、維護時間：{DataComparison.DateTimeConvert(order.EditDate, "{0:yyyy/MM/dd HH:mm:ss}")}";
                    return res;
                }
                db.ExecuteNonQueryProc(
                    "SPNormalOrderImport" , new { Warehouse = "BestLogWMS", Type = "Update", Facility = facility, TMSKey = value.TMSKey, Consigneekey = value.ConsigneeKey, ShortName = value.ShortName
                                                , Zip = value.Zip, Contact = value.Contact, Phone = value.Phone, ShipToAddress = value.ShipToAddress, User = UserName}
                );

                db.CompleteTransaction();
                res.success = true;
            }
            catch (Exception ex)
            {
                db.AbortTransaction();
                res.success = false;
                res.message = ex.Message;
            }
            return res;
        }


        //異動轉入
        public virtual int OrderImport(string UserName, string facility)
        {            
            var ret = 0;
            var db = DbManager.Create("bestlogtms"); 
            try
            {
                db.BeginTransaction();                
                ret = db.ExecuteScalarProc<int>("SPNormalOrderImport", new { Warehouse = "BestLogWMS", Type = "Insert", Facility = facility, TMSKey = "", Consigneekey = "", ShortName = ""
                                                , Zip = "", Contact = "", Phone ="", ShipToAddress = "", User = UserName});
                db.CompleteTransaction();
            }
            catch (Exception ex)
            {
                db.AbortTransaction();
                throw ex;
            }
            return ret;
        }

        //保存訂單
        public virtual bool ReserveOrders(IEnumerable<NormalStandbyOrders> SelectedOrders,string UserName)
        {
            bool ret = false;
            var db = DbManager.Create("bestlogtms"); 
            try
            {
                db.BeginTransaction();

                var orderlist = "(" + string.Join(",", SelectedOrders.Select(p => string.Format("'{0}'", p.TMSKey)).ToArray()) + ")";
                db.Execute(
                    "Update Orders set Status = '8' where TMSKey in " + orderlist + "and rtrim(Status) = '1'"
                );

                db.CompleteTransaction();
                ret = true;
            }
            catch (Exception ex)
            {
                db.AbortTransaction();
                throw ex;
            }
            return ret;
        }

        //將保留訂單返回待排車
        public virtual bool ReturnOrder(IEnumerable<NormalStandbyOrders> SelectedOrders,string UserName)
        {
            bool ret = false;
            var db = DbManager.Create("bestlogtms"); 
            try
            {
                db.BeginTransaction();

                var orderlist = "(" + string.Join(",", SelectedOrders.Select(p => string.Format("'{0}'", p.TMSKey)).ToArray()) + ")";
                db.Execute(
                    "Update Orders set Status = '1' where status = '8' and TMSKey in " + orderlist
                );

                db.CompleteTransaction();
                ret = true;
            }
            catch (Exception ex)
            {
                db.AbortTransaction();
                throw ex;
            }
            return ret;
        }

        //更新箱板材重
        public virtual bool UpdateSkuData(IEnumerable<NormalStandbyOrders> SelectedOrders,string UserName)
        {
            bool ret = false;
            var db = DbManager.Create("bestlogtms"); 
            string strSQL = "";
            try
            {
                db.BeginTransaction();

                var orderlist = "(" + string.Join(",", SelectedOrders.Select(p => string.Format("'{0}'", p.TMSKey)).ToArray()) + ")";

                strSQL = 
                    $@"update OrderDetail set 
                        CaseCnt = p.CaseCnt
                        ,Pallet = p.Pallet
                        ,PalletTi = p.PalletTi
                        ,PalletHi = p.PalletHi
                        ,StdCube = s.StdCube
                        ,StdWeight = s.StdGrossWgt
                    from OrderDetail od 
                        join Orders o on o.TMSKey = od.TMSKey
                        join BestLogWMS..Sku s on od.Sku = s.Sku and od.StorerKey = s.StorerKey
                        join BestLogWMS..Pack p on s.PackKey = p.PackKey
                    where od.TMSKey in {orderlist} and rtrim(o.Status) = '1' ";
                db.Execute(strSQL);

                db.CompleteTransaction();
                ret = true;
            }
            catch (Exception ex)
            {
                db.AbortTransaction();
                throw ex;
            }
            return ret;
        }

        //組成路編
        public virtual CreateNewRouteResponse CreateNewRoute(BaseVehicle Driver, IEnumerable<NormalStandbyOrders> SelectedOrders,string UserName,string DoRouteDate,string Dock,string ExpectDate,string ExpectTime,bool Transfer,string ToFacility) 
        {
            CreateNewRouteResponse res = new CreateNewRouteResponse();
            res.success = false;
            var db = DbManager.Create("bestlogtms"); 
            try
            {
                db.BeginTransaction();

                var orderlist = "(" + string.Join(",", SelectedOrders.Select(p => string.Format("'{0}'", p.TMSKey)).ToArray()) + ")";
                
                //檢查是否已被排車
                var CheckRepeat = db.Fetch<string>(
                    "select TMSKey from Orders " +
                    "where Status not in  ('1','3') and TMSKey in  " + orderlist 
                );

                if(CheckRepeat.Count > 0)
                {
                    res.message = "派車失敗，此趟次有已排車訂單，請重新整理後再繼續排車作業";
                    return res;
                }                


                //取RouteNo
                var data = db.Fetch<string>(
                        "select isnull(max(right(rtrim(RouteNo),3)),0) + 1 from BestRoute WHERE substring(RouteNo,2,6) = substring(convert(char(8),dateadd(day,0,@0),112),3,6) ",DoRouteDate.ToString().Trim());
                var routedate = db.Fetch<string>(
                        "select substring(convert(char(8),dateadd(day,0,@0),112),3,6) ",DoRouteDate.ToString().Trim());

                String strRouteNo = "F" + routedate[0].ToString().Trim() + data[0].ToString().Trim().PadLeft(4,'0');
                String strDriver = Driver.Driver.Trim();
                String strVehicleKey = Driver.VehicleKey.Trim();
                String strReceiver = Driver.Receiver.Trim();
                String strDriverPhone = Driver.DriverPhone.Trim();
                String strCompanyKey = Driver.CompanyKey.Trim();


                //DriveTimes
                data = db.Fetch<string>(
                        "Select Isnull(Max(DriveTimes) + 1,1) From BestRoute Where Convert(varchar(8),DeliveryDate,112) = convert(char(8),@0,112) and VehicleKey = @1"
                        ,DoRouteDate.ToString().Trim(), strVehicleKey);
                String strDriverTimes = data[0].ToString().Trim();



                //計算勾選訂單的箱板材重
                var DeliveryCase = db.Fetch<string>(
                        "select DeliveryCase = sum(o.CaseQty) " +
                                                "from ORDERS o " +
                                                "where o.TMSKey in " + orderlist
                );
                var DeliveryCube = db.Fetch<string>(
                        "select DeliveryCube = sum(o.Cube) " +
                                                "from ORDERS o " +
                                                "where o.TMSKey in " + orderlist
                );
                var DeliveryWeight = db.Fetch<string>(
                        "select DeliveryWeight = sum(o.Weight) " +
                                                "from ORDERS o " +
                                                "where o.TMSKey in " + orderlist
                );
                var DeliveryPallet = db.Fetch<string>(
                        "select DeliveryPallet = sum(o.PalletQty) " +
                                                "from ORDERS o " +
                                                "where o.TMSKey in " + orderlist
                );

                double dobDeliveryCase = Convert.ToDouble(DeliveryCase[0].ToString().Trim());
                double dobDeliveryCube = Convert.ToDouble(DeliveryCube[0].ToString().Trim());
                double dobDeliveryWeight = Convert.ToDouble(DeliveryWeight[0].ToString().Trim());
                double dobDeliveryPallet = Convert.ToDouble(DeliveryPallet[0].ToString().Trim());

                //Insert BestRoute 
                db.Execute(
                    "Insert into BestRoute (" + 
                    "DoRouteDate,RouteNo,CaseQty,PalletQTY,Weight,Cube,DockNo,ExpectDate,ExpectTime,Receiver,Driver, " +
                    "DriverPhone,Notes,CompanyKey,VehicleKey,DriveTimes,AddDate,AddWho)" +
                    "Values( @0, @1, @2, @3, @4, @5, @6, @7, @8, @9, @10, " +
                    "@11, '', @12, @13, @14, getdate(), @15 )" 
                    ,DoRouteDate,strRouteNo,dobDeliveryCase,dobDeliveryPallet,dobDeliveryWeight,dobDeliveryCube,Dock,ExpectDate,ExpectTime,strReceiver,strDriver
                    ,strDriverPhone,strCompanyKey,strVehicleKey,strDriverTimes,UserName
                );

                //Insert RouteHeader
                db.Execute( 
                    "Insert into RouteHeader ( " +
                    "Facility,RouteNo,StorerKey,TMSKey,OriginalKey,ExternOrderKey " +
                    ",OrderType,ExternType,OrderDate,DeliveryDate,ConsigneeKey " +
                    ",ShortName,SoldTo, SoldToName, SoldToAddress " + 
                    ",ShipTo,ShipToName, ShipToAddress " + 
                    ",InvoiceNo,Notes,CaseQty,PalletQty " +
                    ",Weight,Cube,VehicleKey,Driver,UrgentMark,ReserveMark " +
                    ",ColdMark,ScheduleDate,CustomerOrderKey )" +
                    "select " +
                    "o.ToFacility, @0, o.storerkey, o.TMSKey, o.OriginalKey, o.ExternOrderKey " +
                    ",o.OrderType, o.ExternType, o.OrderDate, o.DeliveryDate, o.ConsigneeKey " +
                    ",o.ShortName,o.SoldTo, o.SoldToName, o.SoldToAddress " + 
                    ",o.ShipTo,o.ShipToName, o.ShipToAddress " + 
                    ",o.InvoiceNo, o.Notes, o.CaseQty, o.PalletQty " +
                    ", o.Weight, o.Cube, @1, @2, UrgentMark, ReserveMark " +
                    ",ColdMark, @3, CustomerOrderKey " +
                    "from Orders o where TMSKey in " + orderlist
                    , strRouteNo, strVehicleKey, strDriver, DoRouteDate
                );

                //Insert RouteDetail
                db.Execute(
                    "insert into RouteDetail (RouteNo,StorerKey,TMSKey,OrderLineNumber,ExternOrderKey,ExternLineNumber,Sku,OrderQty,CaseQty,PalletQty,Weight,Cube) " + 
                    "select  " +
                    "@0,od.StorerKey,od.TMSKey,od.OrderLineNumber,od.ExternOrderKey,od.ExternLineNumber,od.Sku,od.OrderQty,od.CaseQty,od.PalletQty,od.Weight,od.Cube " +
                    "from OrderDetail od " +
                    "where od.TMSKey in  " + orderlist 
                    ,strRouteNo

                );
                
                //有勾選轉運，將轉運目的地更新至o.ToFacility
                string strFacility = "";
                if(Transfer) strFacility = ToFacility; 
                else strFacility = "";

                //變更狀態
                db.Execute(
                    "update Orders set " + 
                        "Status = '2' " + //已排，若為轉運則在出車後變更為3，非轉運則變更為4，OriginalRouteNo改由出車確認時更新(因路編刪除會衝突)
                        ",ToFacility = @0 " + 
                        ",EditDate = GETDATE() " + 
                        ",EditWho = @1 " + 
                    "where TMSKey in " + orderlist  
                    ,strFacility,UserName
                );

                //20201012 Modified by Andy. 建立路編後直接取得路編，不用再call一次api(SelectRouteNo)去取得路編
                res.routeno = strRouteNo;

                db.CompleteTransaction();
                res.success = true;
            }
            catch (Exception ex)
            {
                db.AbortTransaction();
                throw ex;
            }
            return res;
        }

        
        

    }
}
