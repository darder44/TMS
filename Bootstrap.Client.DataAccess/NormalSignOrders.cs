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
    /// 簽單維護
    /// </summary>

    public class NormalSignOrders
    {
        /// <summary>
        /// 貨主
        /// </summary>
        public string StorerKey { get; set; }
        /// <summary>
        /// 路線編號
        /// </summary>
        public string RouteNo { get; set; }
        /// <summary>
        /// 所屬倉別
        /// </summary>
        public string Facility { get; set; }
        /// <summary>
        /// 訂單號碼
        /// </summary>
        public string TMSKey { get; set; }
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
        /// 出車日期
        /// </summary>
        public DateTime DoRouteDate { get; set; }
        /// <summary>
        /// 客戶編號
        /// </summary>
        public string ConsigneeKey { get; set; }
        /// <summary>
        /// 客戶簡稱
        /// </summary>
        public string ShortName { get; set; }
        /// <summary>
        /// 提貨客戶編號
        /// </summary>
        public string PickUpConsigneeKey { get; set; }
        /// <summary>
        /// 提貨客戶名稱
        /// </summary>
        public string PickUpName { get; set; }
        /// <summary>
        /// 提貨客戶地址
        /// </summary>
        public string PickUpAddress { get; set; }
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
        /// 到貨地址
        /// </summary>
        public string ShipToAddress { get; set; }
        /// <summary>
        /// 郵遞區號
        /// </summary>
        public string Zip { get; set; }
        /// <summary>
        /// 聯絡人
        /// </summary>
        public string Contact { get; set; }
        /// <summary>
        /// 電話
        /// </summary>
        public string Phone { get; set; }
        /// <summary>
        /// 急單
        /// </summary>
        public string UrgentMark { get; set; }
        /// <summary>
        /// 專車
        /// </summary>
        public string ReserveMark { get; set; }
        /// <summary>
        /// 冷藏
        /// </summary>
        public string ColdMark { get; set; }
        /// <summary>
        /// 簽單狀態
        /// </summary>
        public string ConfirmNotes { get; set; }
        /// <summary>
        /// 簽單日期
        /// </summary>
        public DateTime? ConfirmDate { get; set; }
        /// <summary>
        /// 維護人
        /// </summary>
        public string ConfirmUser { get; set; }
        /// <summary>
        /// 簽單備註
        /// </summary>
        public string SdnNotes { get; set; }
        /// <summary>
        /// 簽單回傳
        /// </summary>
        public string SdnBack { get; set; }
        /// <summary>
        /// 簽單回傳時間
        /// </summary>
        public string SdnSendDate { get; set; }
        /// <summary>
        /// 簽單回傳者
        /// </summary>
        public string SdnSendWho { get; set; }
        /// <summary>
        /// 客戶回覆處理方式
        /// </summary>
        public string CustHandle { get; set; }
        /// <summary>
        /// 發票回傳
        /// </summary>
        public string InvBack { get; set; }
        /// <summary>
        /// 項次
        /// </summary>
        public string OrderLineNumber { get; set; }
        /// <summary>
        /// 品號
        /// </summary>
        public string Sku { get; set; }
        /// <summary>
        /// 品名
        /// </summary>
        public string Descr { get; set; }
        /// <summary>
        /// 訂單個數
        /// </summary>
        public string OrderQty { get; set; }
        /// <summary>
        /// 訂單箱數
        /// </summary>
        public string CaseQty { get; set; }
        /// <summary>
        /// 出貨個數
        /// </summary>
        public string ShipQty { get; set; }
        /// <summary>
        /// 出貨箱數
        /// </summary>
        public string ShipCaseQty { get; set; }
        /// <summary>
        /// 簽收個數
        /// </summary>
        public string SignQty { get; set; }
        /// <summary>
        /// 訂單箱數
        /// </summary>
        public string SignCaseQty { get; set; }
        /// <summary>
        /// 箱入數
        /// </summary>
        public string CaseCnt { get; set; }
        /// <summary>
        /// 板入數
        /// </summary>
        public string Pallet { get; set; }
        /// <summary>
        /// 訂單材積
        /// </summary>
        public string Cube { get; set; }
        /// <summary>
        /// 訂單重量
        /// </summary>
        public string Weight { get; set; }
        /// <summary>
        /// 單位材積
        /// </summary>
        public string StdCube { get; set; }
        /// <summary>
        /// 單位重量
        /// </summary>
        public string StdWeight { get; set; }
        /// <summary>
        /// 異常原因
        /// </summary>
        public string RSCCode { get; set; }
        /// <summary>
        /// 責任歸屬
        /// </summary>
        public string RBCCode { get; set; }
        /// <summary>
        /// 責屬人
        /// </summary>
        public string RBCName { get; set; }
        /// <summary>
        /// 運費維護狀態
        /// </summary>
        public string CostStatus { get; set; }
        /// <summary>
        /// 趟次
        /// </summary>
        public string Sequence { get; set; }
        /// <summary>
        /// 起始位置
        /// </summary>
        public string StartAddress { get; set; }
        /// <summary>
        /// 目的位置
        /// </summary>
        public string EndAddress { get; set; }
        /// <summary>
        /// 司機
        /// </summary>
        public string Driver { get; set; }
        /// <summary>
        /// 車號
        /// </summary>
        public string VehicleKey { get; set; }
        /// <summary>
        /// 更新時間
        /// </summary>
        public DateTime EditDate { get; set; }
        /// <summary>
        /// 更新人
        /// </summary>
        public string EditWho { get; set; }
        /// <summary>
        /// Uom
        /// </summary>
        public string Uom { get; set; }
        /// <summary>
        /// ChargeQty
        /// </summary>
        public string ChargeQty { get; set; }
        /// <summary>
        /// Receivable
        /// </summary>
        public string Receivable { get; set; }
        /// <summary>
        /// Payable
        /// </summary>
        public string Payable { get; set; }
        /// <summary>
        /// Premium
        /// </summary>
        public string Premium { get; set; }
        /// <summary>
        /// Reason
        /// </summary>
        public string Reason { get; set; }
        /// <summary>
        /// SumReceivable
        /// </summary>
        public string SumReceivable { get; set; }
        /// <summary>
        /// SumPayable
        /// </summary>
        public string SumPayable { get; set; }
        /// <summary>
        /// AreaStart
        /// </summary>
        public string AreaStart { get; set; }
        /// <summary>
        /// AreaEnd
        /// </summary>
        public string AreaEnd { get; set; }
        /// <summary>
        /// Notes
        /// </summary>
        public string Notes { get; set; }
        /// <summary>
        /// CostKind
        /// </summary>
        public string CostKind { get; set; }
        /// <summary>
        /// CostCode
        /// </summary>
        public string CostCode { get; set; }
        /// <summary>
        /// STDReceivable
        /// </summary>
        public string STDReceivable { get; set; }
        /// <summary>
        /// STDPayable
        /// </summary>
        public string STDPayable { get; set; }
        /// <summary>
        /// AddDate
        /// </summary>
        public DateTime AddDate { get; set; }
        /// <summary>
        /// AddWho
        /// </summary>
        public string AddWho { get; set; }
        /// <summary>
        /// ARDistribution
        /// </summary>
        public string ARDistribution { get; set; }
        /// <summary>
        /// APAdjustment
        /// </summary>
        public string APAdjustment { get; set; }
        /// <summary>
        /// Receiver
        /// </summary>
        public string Receiver { get; set; }
        /// <summary>
        /// CompanyName
        /// </summary>
        public string CompanyName { get; set; }
        /// <summary>
        /// OTQty
        /// </summary>
        public string OTQty { get; set; }

        /// <summary>
        /// SummaryType
        /// </summary>
        public string SummaryType { get; set; }


        public virtual IEnumerable<NormalSignOrders> RetrieveOrders(string facility, string storers, string tmskey, string routeno, string externorderkey, string sdnback, string signstatus, string orderdates, string orderdatee, string doroutedates, string doroutedatee, string deliverydates, string deliverydatee) => DbManager.Create("bestlogtms").FetchProc<NormalSignOrders>(
            "SPNormalSignOrders", new { Facility = "", Type = "Header",StorerKey = storers, TMSKey = tmskey, RouteNo = routeno, ExternOrderKey = externorderkey, SdnBack = sdnback, SignStatus = signstatus, OrderDateS = orderdates, OrderDateE = orderdatee, DoRouteDateS = doroutedates, DoRouteDateE = doroutedatee, DeliveryDateS = deliverydates, DeliveryDateE = deliverydatee }//此參數放User所屬倉別(權限控制)，可抓到所屬倉別訂單資料，代空表示抓所有資料
            //"SPNormalSignOrders", new { Facility = "", Type = "Header", TMSKey = tmskey}//此參數放User所屬倉別(權限控制)，可抓到所屬倉別訂單資料，代空表示抓所有資料
        );

        public virtual IEnumerable<NormalSignOrders> RetrieveDetailByTMSKey(string TMSKey, string facility) => DbManager.Create("bestlogtms").FetchProc<NormalSignOrders>(
            "SPNormalSignOrders", new { Facility = "", Type = "Detail",StorerKey = "", TMSKey = TMSKey, RouteNo = "", ExternOrderKey = "", SdnBack = "", SignStatus = "", OrderDateS = "", OrderDateE = "", DoRouteDateS = "", DoRouteDateE = "", DeliveryDateS = "", DeliveryDateE = "" }//此參數放User所屬倉別(權限控制)，可抓到所屬倉別訂單資料，代空表示抓所有資料
        );

        public virtual IEnumerable<NormalSignOrders> RetrieveOrderTrackByTMSKey(string TMSKey, string facility) => DbManager.Create("bestlogtms").FetchProc<NormalSignOrders>(
            "SPNormalSignOrders", new { Facility = "", Type = "OrderTrack",StorerKey = "", TMSKey = TMSKey, RouteNo = "", ExternOrderKey = "", SdnBack = "", SignStatus = "", OrderDateS = "", OrderDateE = "", DoRouteDateS = "", DoRouteDateE = "", DeliveryDateS = "", DeliveryDateE = "" }//此參數放User所屬倉別(權限控制)，可抓到所屬倉別訂單資料，代空表示抓所有資料
        );


        public virtual IEnumerable<NormalSignOrders> RetrieveSignOrderDataByTMSKey(string TMSKey) => DbManager.Create("bestlogtms").FetchProc<NormalSignOrders>(
            "SPNormalSignOrderData", new { TMSKey = TMSKey, WareHouse = "BestLogWMS" }
        );

        public virtual IEnumerable<NormalSignOrders> RetrievesRouteData(string TMSKey, string facility) 
        {
            string strSQL = 
                "select " +
                    "RouteNo = rtrim(rh.RouteNo) " +
                    ",StorerKey = rtrim(rh.StorerKey) " +
                    ",TMSKey = rh.TMSKey " +
                    ",ExternOrderKey = rtrim(o.ExternOrderKey) " +
                    ",OrderType = rtrim(o.OrderType) " +
                    ",OrderDate = CONVERT(char(10),o.OrderDate,111) " +
                    ",DoRouteDate = convert(char(10),br.DoRouteDate,111) " +
                    ",DeliveryDate = CONVERT(char(10),o.DeliveryDate,111) " +
                    ",CompanyName = rtrim(bsc.FullName) " +
                    ",VehicleKey = rtrim(br.VehicleKey) " +
                    ",Driver = rtrim(br.Driver) " +
                    ",StartAddress = rtrim(ot.StartAddress) " +
                    ",EndAddress = rtrim(ot.EndAddress) " +
                    ",Zip = rtrim(o.Zip) " +
                    ",ConfirmNotes = rtrim(o.ConfirmNotes)" +
                "from Orders o  " +
                    "join OrderTrack ot on o.TMSKey = ot.TMSKey " +
                    "join RouteHeader rh on o.TMSKey = rh.TMSKey and rh.RouteNo = ot.RouteNo and rh.ConfirmNotes <> '異常重組'" +
                    "join BestRoute br on rh.RouteNo = br.RouteNo " +
                    "join BaseShippingCompany bsc on bsc.CompanyKey = br.CompanyKey " + 
                "where o.TMSKey = '" + TMSKey + "' ";

            return DbManager.Create("bestlogtms").Fetch<NormalSignOrders>(strSQL);
        }

        public virtual IEnumerable<NormalSignOrders> RetrievesShippingCost(string TMSKey, string facility, string User)
        {
            string strSQL = "";
            bool blCheckCostData = true;
            var db = DbManager.Create("bestlogtms");

            //先查詢此單是否已有計費資料
            strSQL = 
                $@"select * from CostDetail where TMSKey = '{TMSKey}'";
            var CheckCostData = db.Fetch<string>(strSQL);
            if (CheckCostData.Count > 0) blCheckCostData = false;

            //已有計費資料則不再執行計費程式
            if(blCheckCostData)
            {
                db.ExecuteNonQueryProc( "SPNormalCost", new { TMSKey = TMSKey, WareHouse = "BestLogWMS", User = User});
            }

            strSQL = 
                "SELECT RouteNo,Uom,ChargeQty,Receivable,Payable,Premium,Reason,SumReceivable,SumPayable,AreaStart,AreaEnd " + 
                ",Notes,TMSKey,CostKind,CostCode,STDReceivable,STDPayable,ARDistribution,VehicleKey,Receiver,Driver FROM CostDetail " + 
                "where TMSKey = '" + TMSKey + "' ";
            
            var CostData = DbManager.Create("bestlogtms").Fetch<NormalSignOrders>(strSQL);

            //已有計費資料則不刪除計費資料
            if(blCheckCostData)
            {
                strSQL = 
                    $@"Delete from CostDetail where TMSKey = '{TMSKey}' ";
                db.Execute(strSQL);
            }
            
            return CostData;

        }


        public virtual NormalSignOrders RetrieveCostCode(string StorerKey, string CostCode){
            string strSQL = 
                "select " +
                "Uom = Uom " + 
                ",Receivable = Receivable " + 
                ",Payable = Payable " + 
                ",STDReceivable = Receivable " + 
                ",STDPayable = Payable " + 
                ",AreaStart = AreaStart " + 
                ",AreaEnd = AreaEnd " +
                ",CostKind = CostKind " +  
                "from BaseCostCode " + 
                "where StorerKey = '" + StorerKey + "' and CostCode = '" + CostCode + "' ";

            return DbManager.Create("bestlogtms").SingleOrDefault<NormalSignOrders>(strSQL);
        } 

        public virtual NormalSignOrders RetriveVehicleKeyByRouteNo(string RouteNo){

            string strSQL = 
                $@"select top 1 VehicleKey,Driver,Receiver from BestRoute where RouteNo = '{RouteNo}'";

            var VehicleKey = DbManager.Create("bestlogtms").Fetch<string>(strSQL);

            string strVehicleKey = VehicleKey[0].ToString();

            return DbManager.Create("bestlogtms").SingleOrDefault<NormalSignOrders>(strSQL);
        }

        public virtual IEnumerable<NormalSignOrders> RetriveVehicleKeyByTMSKey(string TMSKey){

            string strSQL = 
                $@"select distinct VehicleKey from OrderTrack where TMSKey = '{TMSKey}' and len(rtrim(ErrCode)) = 0 ";

            return DbManager.Create("bestlogtms").Fetch<NormalSignOrders>(strSQL);
        } 

        public virtual IEnumerable<NormalSignOrders> RetrieveDriver(string VehicleKey) 
        {

            string strSQL = 
                $@"select distinct Driver from BaseVehicle where VehicleKey = '{VehicleKey}' ";
            
            return DbManager.Create("bestlogtms").Fetch<NormalSignOrders>(strSQL);
        }

        public virtual IEnumerable<NormalSignOrders> RetrieveReceiver(string VehicleKey) 
        {

            string strSQL = 
                $@"select distinct Receiver from BaseVehicle where VehicleKey = '{VehicleKey}' ";
            
            return DbManager.Create("bestlogtms").Fetch<NormalSignOrders>(strSQL);
        }

        public virtual bool CheckCostCode(string CostCode, string StorerKey){
            var ret = false;
            string strSQL = 
                $@"select CostCode from BaseCostCode where CostCode = '{CostCode}' and StorerKey = '{StorerKey}'";

            var CheckCostCode = DbManager.Create("bestlogtms").Fetch<string>(strSQL);

            if(CheckCostCode.Count() > 0) ret = true;
            

            return ret;
        }

        public virtual string CheckVehicleKey(string VehicleKey, string Driver, string Receiver){

            var db = DbManager.Create("bestlogtms");
            var ret = "1";
            bool blCheckVehicleKey = true;
            bool blCheckDriver = true;
            bool blCheckReceiver = true;

            //檢查車號是否存在
            string strSQL = 
                $@"select VehicleKey from BaseVehicle where VehicleKey = '{VehicleKey}'";
            
            var DataCount = db.Fetch<string>(strSQL);
            if(DataCount.Count == 0){ 
                blCheckVehicleKey = false;
            }
            //車號存在才繼續檢查
            else{

                //檢查司機是否屬於此車號
                strSQL = 
                    $@"select Driver from BaseVehicle where VehicleKey = '{VehicleKey}' and Driver = '{Driver}'";
                
                DataCount = db.Fetch<string>(strSQL);
                if(DataCount.Count == 0) blCheckDriver = false;

                if(!string.IsNullOrEmpty(Receiver))
                {
                    if(Receiver.ToString().Trim() != "")
                    {
                        //檢查請款人是否屬於此車號，有輸入請款人才檢查
                        strSQL = 
                            $@"select Receiver from BaseVehicle where VehicleKey = '{VehicleKey}' and Receiver = '{Receiver}'";
                        
                        DataCount = db.Fetch<string>(strSQL);
                        if(DataCount.Count == 0) blCheckReceiver = false;
                    }
                }
            }

            if(!blCheckVehicleKey) ret = "2"; //車號不存在
            else if(!blCheckDriver && blCheckReceiver) ret = "3"; //司機不屬於此車號
            else if(blCheckDriver && !blCheckReceiver) ret = "4"; //請款人不屬於此車號
            else if(!blCheckDriver && !blCheckReceiver) ret = "5"; //司機與請款人皆不屬於此車號



            return ret;
        } 

        public virtual bool SignOrders(NormalSignOrdersBody value, string UserName,IEnumerable<string> Role) {
            bool ret = false;
            var db = DbManager.Create("bestlogtms"); 
            
            try
            {
                db.BeginTransaction();
                var Header = value.Header;
                string strConfirmNotes = "";
                double dobSignQty = 0;
                double dobShipQty = 0;
                string strSQL = "";
                string strStatus = value.SignOrderStatus.ToString();
                string strRSCCode = "";
                string strRBCCode = "";
                string strRBCName = "";
                string strSdnSendDate = value.SdnSendDate;

                bool blRole = Role.Contains("POD");
                bool blSdnBack = false;

                //20201201 Terry 確認此張訂單是否已經回傳過(只記錄第一次簽單維護時間)
                strSQL = 
                    $@"select * from Orders where TMSKey = '{Header.TMSKey}' and SdnSendDate is not null";

                var CheckSdnSendDate = db.Fetch<string>(strSQL);
                if(CheckSdnSendDate.Count > 0) blSdnBack = true;

                
                
                if(strStatus == "5") strConfirmNotes = "正常訂單";
                else if(strStatus == "6") strConfirmNotes = "異常訂單";
                else if(strStatus == "7") strConfirmNotes = "未出訂單";

                //抓取未出訂單RSCCode、RBCCode、RBCName
                if(strStatus == "7")
                {
                    foreach(var d in value.Details)
                    {
                        if(!string.IsNullOrEmpty(d.RSCCode) && !string.IsNullOrEmpty(d.RBCCode))
                        {
                            strRSCCode = d.RSCCode;
                            strRBCCode = d.RBCCode;
                            strRBCName = d.RBCName;
                        }
                    }
                }

                //更新簽單明細
                foreach(var d in value.Details)
                {
                    // 20200819 Modified by Andy. 無異常原因維護，則簽收個數設定為出貨個數
                    // 20201201 Edit by Terry 加未出訂單條件
                    if (string.IsNullOrEmpty(d.RSCCode) && string.IsNullOrEmpty(d.RBCCode) && strStatus != "7")
                    {
                        d.SignQty = d.ShipQty;
                    }
                    dobSignQty += Convert.ToDouble(d.SignQty);
                    dobShipQty += Convert.ToDouble(d.ShipQty);
                    db.Execute(
                        "Update OrderDetail set SignQty = @2, RSCCode = @3, RBCCode = @4, RBCName = @5 " + 
                        "where TMSKey = @0 and OrderLineNumber = @1"
                        , Header.TMSKey, d.OrderLineNumber, d.SignQty, d.RSCCode, d.RBCCode, d.RBCName
                    );

                    db.Execute(
                        "Update RouteDetail set SignQty = @3, RSCCode = @4, RBCCode = @5, RBCName = @6 " + 
                        "from RouteHeader rh join RouteDetail rd on rh.TMSKey = rd.TMSKey and rh.RouteNo = rd.RouteNo " +
                        "where rd.TMSKey = @0 and rd.OrderLineNumber = @1 and rd.RouteNo = @2 and rh.ConfirmNotes <> '異常重組'"
                        , Header.TMSKey, d.OrderLineNumber, Header.RouteNo, d.SignQty, d.RSCCode, d.RBCCode, d.RBCName
                    );
                }

                // 20201201 Add by Terry 未出訂單 一次更新所有異常原因及責屬
                if(strStatus == "7") 
                {
                    db.Execute("update OrderDetail set RSCCode = @1, RBCCode = @2, RBCName = @3 where TMSKey = @0",Header.TMSKey,strRSCCode,strRBCCode,strRBCName);
                    db.Execute("update RouteDetail set RSCCode = @1, RBCCode = @2, RBCName = @3 where TMSKey = @0",Header.TMSKey,strRSCCode,strRBCCode,strRBCName);
                }

                //更新簽單表頭
                strSQL = 
                    $@"Update Orders set ConfirmNotes = '{strConfirmNotes}', CustomerOrderKey = '{Header.CustomerOrderKey}'
                    , SdnNotes = '{Header.SdnNotes}',InvBack = '{Header.InvBack}', CustHandle = '{Header.CustHandle}' ";

                    if(blRole) strSQL += $@", Status = '{value.SignOrderStatus}', SdnBack = '1', SdnSendWho = '{UserName}' ";
                    //20201201 Terry 只有第一次維護才紀錄維護時間
                    //20201215 Terry 新增簽單回傳時間修改功能
                    if(blRole && !string.IsNullOrEmpty(strSdnSendDate)) strSQL += $@", SdnSendDate = '{strSdnSendDate}'";
                    else if(!blSdnBack && blRole) strSQL += $@", SdnSendDate = getdate() ";

                    if(!blRole) strSQL += $@",ConfirmDate = GETDATE(),ConfirmUser = '{UserName}' ";
                
                strSQL += $@"where TMSKey = '{Header.TMSKey}' ";

                db.Execute(strSQL);

                strSQL = 
                    $@"Update RouteHeader set ConfirmNotes = '{strConfirmNotes}', CustomerOrderKey = '{Header.CustomerOrderKey}'
                    , SdnNotes = '{Header.SdnNotes}',InvBack = '{Header.InvBack}', CustHandle = '{Header.CustHandle}' ";

                    if(blRole) strSQL += $@", SdnBack = '1', SdnSendWho = '{UserName}' ";
                    //20201201 Terry 只有第一次維護才紀錄維護時間
                    //20201215 Terry 新增簽單回傳時間修改功能
                    if(blRole && !string.IsNullOrEmpty(strSdnSendDate)) strSQL += $@", SdnSendDate = '{strSdnSendDate}'";
                    else if(!blSdnBack && blRole) strSQL += $@", SdnSendDate = getdate() ";

                    if(!blRole) strSQL += $@",ConfirmDate = GETDATE(),ConfirmUser = '{UserName}' ";
                
                strSQL += $@"where TMSKey = '{Header.TMSKey}' AND RouteNo = '{Header.RouteNo}' and ConfirmNotes <> '異常重組' ";

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

        //檢查簽單是否已回傳
        public virtual string CheckSdnBack(string TMSKey,IEnumerable<string> Role, BootstrapUser User) {
            var db = DbManager.Create("bestlogtms"); 
            string result = "0";
            string strSQL = "";
            bool blRole = Role.Contains("POD");
            
            try
            {
                //非簽單行政，已維護簽單不可修改
                strSQL = 
                    $@"select TMSKey from Orders where TMSKey = '{TMSKey}' and SdnBack = '1' ";

                var CheckSdnBack = db.Fetch<string>(strSQL);

                if(CheckSdnBack.Count != 0 && !blRole)
                {
                    //沒通過,回傳簽單回傳者
                    result = (User == null) ? "找不到使用者" : User.DisplayName;
                }
                else
                {
                    //通過,回傳1
                    result = "1";
                }


            }
            catch (Exception ex)
            {
                throw ex;
            }

            return result;
        

        }

        //檢查簽單維護
        public virtual string CheckSignOrders(NormalSignOrdersCheckBody value, string UserName) {
            bool ret = false;
            var db = DbManager.Create("bestlogtms"); 
            string result = "0";
            
            try
            {
                var signorderstatus = value.SignOrderStatus;
                string strStatus = "0";
                double dobSignQty = 0;
                double dobShipQty = 0;
                bool blRSCCode = false; 
                bool blRBCCode = false;
                // bool blQtyCheck = true; 

                //更新簽單明細
                foreach(var d in value.Details)
                {
                    // 無異常原因維護，並且不是維護成未出訂單，則簽收個數設定為出貨個數
                    if (string.IsNullOrEmpty(d.RSCCode) && string.IsNullOrEmpty(d.RBCCode) && signorderstatus.ToString() != "7" )
                    {
                        d.SignQty = d.ShipQty;
                    }
                    dobSignQty += Convert.ToDouble(d.SignQty);
                    dobShipQty += Convert.ToDouble(d.ShipQty);
                    if (d.RSCCode.ToString().Trim() != "") blRSCCode = true; //存在異常原因
                    if (d.RBCCode.ToString().Trim() != "") blRBCCode = true; //存在責任歸屬

                    // //檢查是否存在某個細項的簽單量與出貨量有同時為0的情況，若存在則不可維護正常訂單
                    // if (Convert.ToInt32(d.SignQty) == 0 && Convert.ToInt32(d.ShipQty) == 0 && Convert.ToInt32(d.OrderQty) != 0)
                    // {
                    //     blQtyCheck = false;
                    // }
                }

                // if(dobSignQty == dobShipQty && dobShipQty != 0 && blQtyCheck) strStatus = "5"; //可維護成正常訂單
                // //else if (dobSignQty == 0 && dobShipQty == 0) strStatus = "7"; //可維護成未出訂單
                // else strStatus = "6";

                // if(signorderstatus.ToString() == "5" && strStatus == "5" && !blRSCCode && !blRBCCode ) ret = true;
                // else if(signorderstatus.ToString() != "5" && blRSCCode && blRBCCode ) ret = true;
                // else ret = false;

                if(dobSignQty == dobShipQty && !blRBCCode && !blRSCCode ) strStatus = "5"; //可維護成正常訂單(出貨總量等於簽收總量、沒有異常原因與責屬)
                else if (dobSignQty == 0 && blRBCCode && blRSCCode) strStatus = "7"; //可維護成未出訂單(出貨總量與簽收總量皆為0、有異常原因及責屬)
                else if(blRBCCode && blRSCCode) strStatus = "6";

                if(signorderstatus.ToString() == strStatus) ret = true;
                else if (signorderstatus.ToString() == "6" && strStatus != "5") ret = true;
                else ret = false;

                if(ret) result = "1";
                else result = signorderstatus;


            }
            catch (Exception ex)
            {
                db.AbortTransaction();
                throw ex;
            }

            return result;
        

        }

        //批次維護正常訂單前檢查是否已經維護
        public virtual bool CheckOrderStatus(IEnumerable<NormalSignOrders> value, string UserName,IEnumerable<string> Role) {
            bool ret = false;
            var db = DbManager.Create("bestlogtms"); 
            string strSQL = "";
            
            try
            {
                var orderlist = "(" + string.Join(",",value.Select(p =>string.Format("'{0}'",p.TMSKey)).ToArray() ) + ")";

                //20201201 Terry 確認此批訂單是否已經回傳過
                strSQL = 
                    $@"select * from Orders where TMSKey in {orderlist} and SdnSendDate is not null";
                
                var CheckSdnSendDate = db.Fetch<string>(strSQL);
                if(CheckSdnSendDate.Count > 0) ret = true;

            }
            catch (Exception ex)
            {
                throw ex;
            }
            return ret;
        }

        //批次維護正常訂單
        public virtual bool NormalOrders(IEnumerable<NormalSignOrders> value, string UserName,IEnumerable<string> Role) {
            bool ret = false;
            var db = DbManager.Create("bestlogtms"); 
            string strSQL = "";
            bool blSdnBack = false;
            
            try
            {
                db.BeginTransaction();
                var orderlist = "(" + string.Join(",",value.Select(p =>string.Format("'{0}'",p.TMSKey)).ToArray() ) + ")";

                //20201201 Terry 確認此張訂單是否已經回傳過(只記錄第一次簽單維護時間)
                strSQL = 
                    $@"select * from Orders where TMSKey in {orderlist} and SdnSendDate is not null";

                var CheckSdnSendDate = db.Fetch<string>(strSQL);
                if(CheckSdnSendDate.Count > 0) blSdnBack = true;

                //檢查選取訂單內是否有異常訂單或未出訂單
                strSQL = 
                    "select distinct TMSKey from Orders where Status not in ('4','5') and TMSKey in " + orderlist;
                var CheckSignOrders = db.Fetch<string>(strSQL);

                if(CheckSignOrders.Count > 0)
                {
                    ret = false;
                    return ret;
                }

                bool blRole = Role.Contains("POD");

                //更新Header
                strSQL = 
                    $@"Update Orders set Status = '5', ConfirmNotes = '正常訂單'
                        , SdnNotes = '快速簽單確認', InvBack = 'N' ";

                if(blRole) strSQL += $@", SdnBack = '1', SdnSendWho = '{UserName}' ";
                //20201201 Terry 只有第一次維護才紀錄維護時間
                if(!blSdnBack && blRole) strSQL += $@", SdnSendDate = getdate() ";
                if(!blRole) strSQL += $@", ConfirmDate = getdate(), ConfirmUser = '{UserName}'  ";

                strSQL += $@"where TMSKey in {orderlist} ";
                
                db.Execute(strSQL);

                strSQL = 
                    $@"Update RouteHeader set ConfirmNotes = '正常訂單'
                    , SdnNotes = '快速簽單確認',InvBack = 'N' ";

                    if(blRole) strSQL += $@", SdnBack = '1', SdnSendWho = '{UserName}' ";
                    //20201201 Terry 只有第一次維護才紀錄維護時間
                    if(!blSdnBack && blRole) strSQL += $@", SdnSendDate = getdate() ";
                    if(!blRole) strSQL += $@",ConfirmDate = GETDATE(),ConfirmUser = '{UserName}' ";
                
                strSQL += $@"where TMSKey in {orderlist}  and ConfirmNotes <> '異常重組' ";

                db.Execute(strSQL);

                //更新Detail
                db.Execute(
                    "Update OrderDetail set SignQty = ShipQty " + 
                    ", RSCCode = '' " + 
                    ", RBCCode = '' " + 
                    ", RBCName = '' " + 
                    "where TMSKey in " + orderlist
                );

                db.Execute(
                        $@"Update RouteDetail 
                            set SignQty = ShipQty
                            , RSCCode = ''
                            , RBCCode = ''
                            , RBCName = '' 
                        from RouteHeader rh 
                            join RouteDetail rd on rh.TMSKey = rd.TMSKey and rh.RouteNo = rd.RouteNo 
                        where rd.TMSKey in {orderlist} and rh.ConfirmNotes <> '異常重組' "
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

        //批次維護未出訂單
        public virtual bool UnShippedOrders(NormalUnShippedOrdersBody value, string UserName, IEnumerable<string> Role) {
            
            bool ret = false;
            var db = DbManager.Create("bestlogtms"); 
            
            try
            {
                db.BeginTransaction();

                var orderlist = "(" + string.Join(",",value.selections.Select(p =>string.Format("'{0}'",p.TMSKey)).ToArray() ) + ")";

                //檢查選取訂單內是否有正常訂單或已維護好的異常訂單
                string strSQL = 
                    "select * from OrderDetail where ShipQty <> 0 and TMSKey in " + orderlist;
                var CheckSignOrders = db.Fetch<string>(strSQL);

                if(CheckSignOrders.Count > 0)
                {
                    ret = false;
                    return ret;
                }

                bool blRole = Role.Contains("POD");

                //更新Header
                strSQL = 
                    $@"Update Orders set Status = '7', ConfirmNotes = '未出訂單'
                        , SdnNotes = '快速簽單確認', InvBack = 'N' ";

                if(blRole) strSQL += $@", SdnBack = '1', SdnSendDate = getdate(), SdnSendWho = '{UserName}' ";
                if(!blRole) strSQL += $@", ConfirmDate = getdate(), ConfirmUser = '{UserName}'  ";

                strSQL += $@"where TMSKey in {orderlist} ";
                
                db.Execute(strSQL);

                //更新Detail
                db.Execute(
                    "Update OrderDetail set SignQty = ShipQty " + 
                    ", RSCCode = @0 " + 
                    ", RBCCode = @1 " + 
                    ", RBCName = @2 " + 
                    "where TMSKey in " + orderlist
                    , value.rsccode, value.rbccode, value.rbcname
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


        //出車異常打散
        public virtual string BreakRoute(NormalDeliveryAbnormalModal value, string UserName, IEnumerable<string> Role) {
            
            string ret = "0";
            var db = DbManager.Create("bestlogtms"); 
            string strSQL = "";
            var blRole = Role.Contains("TMS_Supervisor");
            
            try
            {
                db.BeginTransaction();

                var routelist = "(" + string.Join(",",value.Selects.Select(p =>string.Format("'{0}'",p.RouteNo)).ToArray() ) + ")";
                string strErrorCode = value.ErrorCode;

                //檢查簽單是否已維護
                strSQL = 
                    "select * " + 
                    "from Orders o " + 
                        "join RouteHeader rh on o.TMSKey = rh.TMSKey and rh.ConfirmNotes <> '異常重組' " + 
                    "where rtrim(o.status) in ('5','6','7') " + 
                        "and o.SdnBack = '1' " + 
                        "and rh.RouteNo in " + routelist ;
                var CheckStatus = db.Fetch<string>(strSQL);
                if(CheckStatus.Count > 0)
                {
                    ret = "1";
                    return ret;
                }

                //檢查是否已有棧板資料
                strSQL = 
                    $@"select CheckNo from PalletHead where CheckNo in {routelist}";
                
                var CheckPalletData = db.Fetch<string>(strSQL);

                if(CheckPalletData.Count > 0)
                {
                    ret = "2";
                    return ret;
                }

                //每個排車者只能重組自己建立的路編
                strSQL = 
                    "select DISTINCT AddWho " +
                        "from BestRoute " + 
                        "where RouteNo in " + routelist;
                var CreateUser = db.Fetch<string>(strSQL);

                if(CreateUser.Count != 1 && !blRole) //20200928 Modified by Andy. 必須要等於1 如果回傳為0也不能繼續
                {
                    ret = "3";
                    return ret;
                }
                string strCreateUser = CreateUser.FirstOrDefault(); //20200928 Modified by Andy. 改為Linq抓取值
                if((string.IsNullOrEmpty(strCreateUser) || strCreateUser.Trim().ToUpper() != UserName.Trim().ToUpper()) && !blRole)
                {
                    ret = "3";
                    return ret;
                }

                //將訂單狀態更新為待排車
                strSQL = 
                    "update orders set " + 
                        "Status = case when o.Facility = ot.FromFacility then '1' else '3' end " + 
                        ",ToFacility = ot.FromFacility " + 
                        ", ConfirmNotes = '' " + 
                        ", ConfirmDate = null " + 
                        ", ConfirmUser = '' " + 
                        ", SdnBack = '0' " + 
                        ", SdnNotes = '' " + 
                        ", InvBack = 'N' " +
                        //", SdnSendDate = null " + //簽單回傳日期不清空
                        ", SdnSendWho = '' " +
                    "from Orders o " + 
                        "join OrderTrack ot on o.TMSKey = ot.TMSKey " +
                    "where ot.RouteNo in " + routelist ;
                db.Execute(strSQL);

                //更新OrderTrack
                strSQL = 
                    "Update OrderTrack set ErrCode = '" + strErrorCode + "' " + 
                    "where RouteNo in " + routelist;
                db.Execute(strSQL);

                //更新路編
                strSQL = 
                    "Update RouteHeader set ConfirmUser = '" + UserName + "', ConfirmNotes = '異常重組', ConfirmDate = getdate() " + 
                    "where RouteNo in " + routelist;
                db.Execute(strSQL);


                //跑迴圈更新OriginalRouteNo
                strSQL = 
                    "select distinct TMSKey from RouteHeader where RouteNo in " + routelist;
                var tmskeys = db.Fetch<NormalSignOrders>(strSQL);

                foreach(var p in tmskeys)
                {
                    strSQL = 
                        "select Sequence = MAX(Sequence)  " + 
                        "from BestLogTMS..OrderTrack " +
                        "where ErrCode = '' and TMSKey = '" + p.TMSKey + "' " +
                        "GROUP BY TMSKey " ;
                        
                    var LastRouteNo = db.Fetch<int>(strSQL);

                    string strLastRouteNo = "";
                    if(LastRouteNo.Count != 0)
                    {
                        strLastRouteNo = LastRouteNo[0].ToString();
                    
                    

                        strSQL = 
                            "update Orders set OriginalRouteNo = ot.RouteNo " +
                            "from Orders o " + 
                                "join OrderTrack ot on o.TMSKey = ot.TMSKey " + 
                            "where o.TMSKey = '" + p.TMSKey + "' and ot.Sequence = '" + strLastRouteNo + "'";
                        db.Execute(strSQL);
                    }
                    else
                    {
                        //抓不到上一筆OrderTrack，表示此單沒轉運，若打散則直接回到最一開始的未排車狀態
                        strSQL = 
                                "update orders set " + 
                                    "Status = '1' " + 
                                    ",ToFacility = Facility " + 
                                "from Orders  " + 
                                "where TMSKey = '" + p.TMSKey + "' ";
                        db.Execute(strSQL);
                    }

                    //清空計費資料
                    strSQL = "delete from CostDetail where TMSKey = '" + p.TMSKey + "'";
                    db.Execute(strSQL);

                    //20201105，改為打散時才清空
                    //清空簽單明細資料
                    strSQL = 
                        "update OrderDetail set " +
                            "SignQty = 0 " + 
                            ",RSCCode = '' " + 
                            ",RBCCode = '' " + 
                            ",RBCName = '' " + 
                        "where TMSKey = '" + p.TMSKey + "'";
                    db.Execute(strSQL);
                }

                

                db.CompleteTransaction();
                ret = "0";

            }
            catch (Exception ex)
            {
                db.AbortTransaction();
                throw ex;
            }
            
            return ret;
            
        }

        //清空簽單資料
        public virtual string ClearSignOrders(IEnumerable<NormalSignOrders> value, string UserName, IEnumerable<string> Role) {
            
            var db = DbManager.Create("bestlogtms"); 
            string strSQL = "";
            string result = "0";
            //簽單主管可清空所有簽單
            bool blRoleSupervisor = Role.Contains("POD_Supervisor");
            
            try
            {
                db.BeginTransaction();

                var tmskeys = "(" + string.Join(",",value.Select(p =>string.Format("'{0}'",p.TMSKey)).ToArray() ) + ")";

                //每個維護人只能清空自己維護的簽單
                strSQL = 
                    "select distinct SdnSendWho " +
                    "from Orders " + 
                    "where TMSKey in " + tmskeys;
                var CreateUser = db.Fetch<string>(strSQL);

                if(CreateUser.Count != 1 && !blRoleSupervisor) //必須要等於1 如果回傳為0也不能繼續
                {
                    result = "2";
                    return result;
                }
                string strCreateUser = CreateUser.FirstOrDefault(); //Linq抓取值
                if(!string.IsNullOrEmpty(strCreateUser) && strCreateUser.Trim().ToUpper() != UserName.Trim().ToUpper() && !blRoleSupervisor)
                {
                    result = "2";
                    return result;
                }

                //清空計費資料
                strSQL = "delete from CostDetail where TMSKey in " + tmskeys;
                db.Execute(strSQL);

                //將簽單資料清空
                //Orders
                strSQL = 
                    "update Orders set " +
                        "Status = '4' " + 
                        ",ConfirmNotes = '' " + 
                        ",CostDate = null " + 
                        ",CostWho = '' " + 
                        ",SdnBack = '0' " +
                        // ",SdnSendDate = null " + //Edit by Terry 20201201 不更改維護時間
                         ",SdnSendWho = '' " +
                        ",ClearWho = '" + UserName + "'" +
                        ",ClearDate = getdate() " +
                    "where TMSKey in " + tmskeys ;
                db.Execute(strSQL);

                //20201105，改為打散時才清空
                //清空明細資料
                // //OrderDetail
                // strSQL = 
                //     "update OrderDetail set " +
                //         "SignQty = 0 " + 
                //         ",RSCCode = '' " + 
                //         ",RBCCode = '' " + 
                //         ",RBCName = '' " + 
                //     "where TMSKey in " + tmskeys ;
                // db.Execute(strSQL);


                db.CompleteTransaction();
                result = "1";

            }
            catch (Exception ex)
            {
                db.AbortTransaction();
                throw ex;
            }

            
            
            return result;
            
        }

        //重新計費
        public virtual string Calculate(IEnumerable<NormalSignOrders> value, string UserName, IEnumerable<string> Role) {
            
            var db = DbManager.Create("bestlogtms"); 
            string strSQL = "";
            string result = "0";
            //簽單主管可重算所有計費
            bool blRoleSupervisor = Role.Contains("POD_Supervisor");
            
            try
            {
                db.BeginTransaction();

                var tmskeys = "(" + string.Join(",",value.Select(p =>string.Format("'{0}'",p.TMSKey)).ToArray() ) + ")";

                //先檢查是否有未維護簽單
                strSQL = 
                    $@"select * from Orders where TMSKey in {tmskeys} and SdnBack = 0";
                
                var CheckSdnBack = db.Fetch<string>(strSQL);
                if(CheckSdnBack.Count > 0)
                {
                    result = "3";
                    return result;
                }

                //每個維護人只能重算自己的簽單
                strSQL = 
                    "select distinct SdnSendWho " +
                    "from Orders " + 
                    "where TMSKey in " + tmskeys;
                var CreateUser = db.Fetch<string>(strSQL);

                if(CreateUser.Count != 1 && !blRoleSupervisor) //必須要等於1 如果回傳為0也不能繼續
                {
                    result = "2";
                    return result;
                }
                string strCreateUser = CreateUser.FirstOrDefault(); //Linq抓取值
                if(!string.IsNullOrEmpty(strCreateUser) && strCreateUser.Trim().ToUpper() != UserName.Trim().ToUpper() && !blRoleSupervisor)
                {
                    result = "2";
                    return result;
                }

                //清空計費資料
                strSQL = "delete from CostDetail where TMSKey in " + tmskeys;
                db.Execute(strSQL);

                //重新計費
                foreach(var d in value)
                {
                    db.ExecuteNonQueryProc("SPNormalCost" , new { TMSKey = d.TMSKey, WareHouse = "BestLogWMS", User = UserName });
                }


                db.CompleteTransaction();
                result = "1";

            }
            catch (Exception ex)
            {
                db.AbortTransaction();
                throw ex;
            }

            
            
            return result;
            
        }

        //保存計費資料
        public virtual bool SaveCostData(NormalCostDataBody value, string UserName) {
            
            bool ret = false;
            var db = DbManager.Create("bestlogtms"); 
            string strSQL = "";
            
            try
            {
                db.BeginTransaction();

                string strTMSKey = value.TMSKey;
                //先刪除此單原有計費資料
                strSQL = 
                    "delete from CostDetail where TMSKey = '" + strTMSKey + "'";
                db.Execute(strSQL);


                foreach(var p in value.CostData)
                {

                    var costdata = string.Format("('{0}', '{1}', '{2}', '{3}', '{4}'" + 
                                            ", '{5}', '{6}', '{7}', '{8}', '{9}'" + 
                                            ", '{10}', '{11}', '{12}', '{13}', '{14}'" + 
                                            ", '{15}', '{16}', getdate(), '{17}', '{18}'" + 
                                            ", '{19}', '{20}')"
                                            , p.RouteNo, p.Uom, p.ChargeQty, p.Receivable, p.Payable
                                            ,p.Premium, p.Reason, p.SumReceivable, p.SumPayable, p.AreaStart
                                            , p.AreaEnd, p.Notes, p.TMSKey, p.CostKind, p.CostCode
                                            , p.STDReceivable,p.STDPayable,UserName, p.VehicleKey
                                            , p.Receiver,p.Driver );

                    strSQL = 
                        "insert into CostDetail (" + 
                            "RouteNo,Uom,ChargeQty,Receivable,Payable " +
                            ",Premium,Reason,SumReceivable,SumPayable,AreaStart " +
                            ",AreaEnd,Notes,TMSKey,CostKind,CostCode " +
                            ",STDReceivable,STDPayable,AddDate,AddWho,VehicleKey " +
                            ",Receiver,Driver ) " +
                        "values " + costdata ;

                    db.Execute(strSQL);
                        
                }

                strSQL = 
                    "update Orders set CostWho = '" + UserName + "', CostDate = getdate() where TMSKey = '" + strTMSKey + "' ";
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
    }
}


//         public virtual NormalSignOrders RetrievesOrderData(string TMSKey, string facility)
//         {
//             // string strSQL = 
//             //     "select " +
//             //         "ShipQty = sum(od.ShipQty) " +
//             //         ",ShipCaseQty = sum(case when od.CaseCnt = 0 then 0 else od.ShipQty / od.CaseCnt end) " +
//             //         ",Cube = round(sum(od.ShipQty * od.StdCube),5) " +
//             //         ",Weight = round(sum(od.ShipQty * od.StdWeight),5) " +
//             //         ",OTQty = o.OTQty " +
//             //     "from OrderDetail od  " +
//             //         "join Orders o on o.TMSKey = od.TMSKey " +
//             //     "where o.TMSKey = '" + TMSKey + "' " +
//             //     "group by o.OTQty,o.TMSKey " ;

//             //更改出貨量抓取欄位
//             string strSQL = 
//                 $@"select 
//                     ShipQty = sum(od.ShipQty) 
//                     ,ShipCaseQty = sum(case when od.CaseCnt = 0 then 0 else od.ShipQty / od.CaseCnt end) 
//                     ,Cube = round(sum(od.ShipQty * od.StdCube),5) 
//                     ,Weight = round(sum(od.ShipQty * od.StdWeight),5) 
//                     ,OTQty = o.OTQty 
//                 from OrderDetail od  
//                     join Orders o on o.TMSKey = od.TMSKey 
//                 where o.TMSKey = '{TMSKey}' 
//                 group by o.OTQty,o.TMSKey " ;
            

//             return DbManager.Create("bestlogtms").SingleOrDefault<NormalSignOrders>(strSQL);
//         }

//         public virtual NormalSignOrders RetrievesDeliveryDatexAddressData(string ShipToAddress, string DeliveryDate, string facility) 
//         {
//             // string strSQL = 
//             //     "select " +
//             //         "ShipQty = sum(od.ShipQty) " +
//             //         ",ShipCaseQty = sum(case when od.CaseCnt = 0 then 0 else od.ShipQty / od.CaseCnt end) " +
//             //         ",Cube = round(sum(od.ShipQty * od.StdCube),5) " +
//             //         ",Weight = round(sum(od.ShipQty * od.StdWeight),5) " +
//             //         ",OTQty = 0 " +
//             //     "from Orders o  " +
//             //         "join OrderDetail od on o.TMSKey = od.TMSKey " +
//             //         "join RouteHeader rh on o.TMSKey = rh.TMSKey and rh.ConfirmNotes <> '異常重組' " +
//             //         "join BaseCustomer bc on o.ConsigneeKey = bc.ConsigneeKey and o.StorerKey = bc.StorerKey " +
//             //     "where o.ShipToAddress = '" + ShipToAddress + "' and convert(char(10),o.DeliveryDate,111) = '" + DeliveryDate + "' ";

//             //更改出貨量抓取欄位
//             string strSQL = 
//                 $@"select 
//                     ShipQty = sum(od.ShipQty) 
//                     ,ShipCaseQty = sum(case when od.CaseCnt = 0 then 0 else od.ShipQty / od.CaseCnt end) 
//                     ,Cube = round(sum(od.ShipQty * od.StdCube),5) 
//                     ,Weight = round(sum(od.ShipQty * od.StdWeight),5) 
//                     ,OTQty = 0 
//                 from Orders o  
//                     join OrderDetail od on o.TMSKey = od.TMSKey 
//                     join RouteHeader rh on o.TMSKey = rh.TMSKey and rh.ConfirmNotes <> '異常重組' 
//                     join BaseCustomer bc on o.ConsigneeKey = bc.ConsigneeKey and o.StorerKey = bc.StorerKey 
//                 where o.ShipToAddress = '{ShipToAddress}' and convert(char(10),o.DeliveryDate,111) = '{DeliveryDate}' ";
//             var ret = DbManager.Create("bestlogtms").Fetch<NormalSignOrders>(strSQL);
//             if (ret.Count == 0) return null;
//             return ret.First();
//             //",OTQty = isnull((select sum(isnull(o.OtQty,0)) from Orders os join RouteHeader rhs on os.TMSKey = rhs.TMSKey where rhs.RouteNo = rh.RouteNo and os.TMSKey = o.TMSKey),0) " +
//         }

//         public virtual NormalSignOrders RetrievesRouteNoxStorerKeyData(string StorerKey, string RouteNo, string facility)
//         {
//             // string strSQL =
//             //     "select " +
//             //         "ShipQty = sum(od.ShipQty) " +
//             //         ",ShipCaseQty = sum(case when od.CaseCnt = 0 then 0 else od.ShipQty / od.CaseCnt end) " +
//             //         ",Cube = round(sum(od.ShipQty * od.StdCube),5) " +
//             //         ",Weight = round(sum(od.ShipQty * od.StdWeight),5) " +
//             //         ",OTQty = isnull((select sum(isnull(os.OtQty,0)) from Orders os join RouteHeader rhs on os.TMSKey = rhs.TMSKey where rhs.RouteNo = rh.RouteNo ),0) " +
//             //     "from OrderDetail od  " +
//             //         "join Orders o on o.TMSKey = od.TMSKey " +
//             //         "join RouteHeader rh on o.TMSKey = rh.TMSKey and rh.ConfirmNotes <> '異常重組' " +
//             //     "where o.StorerKey = '" + StorerKey + "' and rh.RouteNo = '" + RouteNo + "' " + 
//             //     "group by rh.RouteNo ";

//             //更改出貨量抓取欄位
//             string strSQL =
//                 $@"select 
//                     ShipQty = sum(od.ShipQty) 
//                     ,ShipCaseQty = sum(case when od.CaseCnt = 0 then 0 else od.ShipQty / od.CaseCnt end) 
//                     ,Cube = round(sum(od.ShipQty * od.StdCube),5) 
//                     ,Weight = round(sum(od.ShipQty * od.StdWeight),5) 
//                     ,OTQty = isnull((select sum(isnull(os.OtQty,0)) from Orders os join RouteHeader rhs on os.TMSKey = rhs.TMSKey where rhs.RouteNo = rh.RouteNo ),0) 
//                 from OrderDetail od  
//                     join Orders o on o.TMSKey = od.TMSKey 
//                     join RouteHeader rh on o.TMSKey = rh.TMSKey and rh.ConfirmNotes <> '異常重組' 
//                 where o.StorerKey = '{StorerKey}' and rh.RouteNo = '{RouteNo}' 
//                 group by rh.RouteNo ";
//             return DbManager.Create("bestlogtms").SingleOrDefault<NormalSignOrders>(strSQL);
//         }