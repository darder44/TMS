using Bootstrap.Security.DataAccess;
using PetaPoco;
using System;
using System.Collections.Generic;
using Longbow.Web.Mvc;
using System.Linq;
using Longbow.Data;
using Bootstrap.Client.DataAccess.Helper;


namespace Bootstrap.Client.DataAccess
{
    /// <summary>
    /// 
    /// </summary>
    public class NormalOrders
    {
        public string StorerKey { get; set; }

        public string Facility { get; set; }

        public string TMSKey { get; set; }

        public string OriginalKey { get; set; }

        public string ExternOrderKey { get; set; }

        public string CustomerOrderKey { get; set; }

        public string OrderType { get; set; }

        public string ExternType { get; set; }

        public string OrderDate { get; set; }

        public string DeliveryDate { get; set; }

        public string ConsigneeKey { get; set; }

        public string ShortName { get; set; }

        public string PickUpConsigneeKey { get; set; }

        public string PickUpName { get; set; }

        public string PickUpAddress { get; set; }

        public string SoldTo { get; set; }

        public string SoldToName { get; set; }

        public string SoldToAddress { get; set; }

        public string ShipTo { get; set; }

        public string ShipToName { get; set; }

        public string ShipToAddress { get; set; }

        public string Zip { get; set; }

        public string Contact { get; set; }

        public string Phone { get; set; }

        public string Notes { get; set; }

        public double CaseQty { get; set; }

        public double PalletQty { get; set; }

        public double Cube { get; set; }

        public double Weight { get; set; }

        public string UrgentMark { get; set; }

        public string ReserveMark { get; set; }

        public string ColdMark { get; set; }

        public string Priority { get; set; }

        public string UpdateSource { get; set; }

        public string OriginalRouteNo { get; set; }

        public string InvoiceNo { get; set; }

        public string ToFacility { get; set; }

        public string ToWMS { get; set; }

        public DateTime? ConfirmDate { get; set; }

        public string ConfirmNotes { get; set; }

        public string ConfirmUser { get; set; }

        public DateTime? SdnSendDate { get; set; }

        public int OTQty { get; set; }

        public string OTConfirmUser { get; set; }

        public DateTime? OTConfirmDate { get; set; }

        public DateTime? OTPrintDate { get; set; }

        public int OTPrintTimes { get; set; }

        public string SdnNotes { get; set; }

        public string SdnBack { get; set; }

        public string InvBack { get; set; }

        public string CustHandle { get; set; }

        public string Status { get; set; }

        public DateTime AddDate { get; set; }

        public string AddWho { get; set; }

        public DateTime EditDate { get; set; }

        public string EditWho { get; set; }

        public string OrderLineNumber { get; set; }

        public string ExternLineNumber { get; set; }

        public string Sku { get; set; }

        public string Descr { get; set; }

        public int OrderQty { get; set; }

        public int ShipQty { get; set; }

        public int SignQty { get; set; }

        public double Pallet { get; set; }

        public double CaseCnt { get; set; }

        public double StdCube { get; set; }

        public double StdWeight { get; set; }

        public string RSCCode { get; set; }

        public string RBCCode { get; set; }

        public string RBCName { get; set; }


        public virtual IEnumerable<NormalOrders> Retrieves(string storers, string ordertypes, string tmskey, string externorderkey, string orderdates, string orderdatee, string deliverydates, string deliverydatee, string adddates, string adddatee, string facility){
            string strSQL = 
            "select " +
                "StorerKey = rtrim(o.StorerKey) " +
                ",Facility = rtrim(o.Facility) " +
                ",TMSKey = o.TMSKey " +
                ",ExternOrderKey = rtrim(o.ExternOrderKey) " +
                ",CustomerOrderKey = rtrim(o.CustomerOrderKey) " +
                ",OrderType = rtrim(o.OrderType) " +
                ",OrderDate = convert(char(10),o.OrderDate,111) " +
                ",DeliveryDate = convert(char(10),o.DeliveryDate,111) " +
                ",AddDate = convert(char(10),o.AddDate,111) " +
                ",ConsigneeKey = rtrim(o.ConsigneeKey) " +
                ",ShortName = rtrim(o.ShortName) " +
                ",PickUpConsigneeKey = rtrim(o.PickUpConsigneeKey) " +
                ",PickUpName = rtrim(o.PickUpName) " +
                ",PickUpAddress = rtrim(o.PickUpAddress) " +
                ",ShipToAddress = rtrim(o.ShipToAddress) " +
                ",Zip = rtrim(o.Zip) " +
                ",Contact = rtrim(o.Contact) " +
                ",Phone = rtrim(o.Phone) " +
                ",Notes = rtrim(o.Notes) " +
                ",Status = o.Status " +
            "from Orders o " + 
            "where o.Status <> 'D' ";

            //貨主
            if(storers.Length != 0) strSQL += " and o.StorerKey in (" + storers + ")";

            //訂單類別
            if(ordertypes.Length != 0) strSQL += " and o.OrderType in (" + ordertypes + ")";

            //訂單號碼
            if(tmskey.Length != 0) strSQL += " and o.TMSKey = '" + tmskey + "'";

            //貨主單號
            if(externorderkey.Length != 0) strSQL += " and o.ExternOrderKey = '" + externorderkey + "'";

            //訂單日期
            if(orderdates.Length != 0 && orderdatee.Length != 0) strSQL += " and convert(char(8),o.OrderDate,112) between '" + orderdates + "' and '" + orderdatee + "'";
            if(orderdates.Length != 0 && orderdatee.Length == 0) strSQL += " and convert(char(8),o.OrderDate,112) = '" + orderdates + "'";
            if(orderdates.Length == 0 && orderdatee.Length != 0) strSQL += " and convert(char(8),o.OrderDate,112) = '" + orderdatee + "'";

            //到貨日期
            if(deliverydates.Length != 0 && deliverydatee.Length != 0) strSQL += " and convert(char(8),o.DeliveryDate,112) between '" + deliverydates + "' and '" + deliverydatee + "'";
            if(deliverydates.Length != 0 && deliverydatee.Length == 0) strSQL += " and convert(char(8),o.DeliveryDate,112) = '" + deliverydates + "'";
            if(deliverydates.Length == 0 && deliverydatee.Length != 0) strSQL += " and convert(char(8),o.DeliveryDate,112) = '" + deliverydatee + "'";

            //轉入日期
            if(adddates.Length != 0 && adddatee.Length != 0) strSQL += " and convert(char(8),o.AddDate,112) between '" + adddates + "' and '" + adddatee + "'";
            if(adddates.Length != 0 && adddatee.Length == 0) strSQL += " and convert(char(8),o.AddDate,112) = '" + adddates + "'";
            if(adddates.Length == 0 && adddatee.Length != 0) strSQL += " and convert(char(8),o.AddDate,112) = '" + adddatee + "'";

            //倉別
            //if(facility.Length != 0) strSQL += " and o.ToFacility = '" + facility + "'";

            return DbManager.Create("bestlogtms").Fetch<NormalOrders>(strSQL);
        }

        public virtual IEnumerable<NormalOrders> RetrieveDetailByTMSKey(string TMSKey, string facility) => DbManager.Create("bestlogtms").Fetch<NormalOrders>(
            "select " +
                "OrderLineNumber = rtrim(od.OrderLineNumber) " +
                ",Sku = rtrim(od.Sku) " +
                ",Descr = rtrim(od.Descr) " +
                ",OrderQty = od.OrderQty " +
                ",CaseQty = od.CaseQty " +
                ",PalletQty = od.PalletQty " +
                ",Cube = od.Cube " +
                ",Weight = od.Weight " +
            "from OrderDetail od " +
            "where od.TMSKey = @0 "
            ,TMSKey
        );

        public virtual IEnumerable<NormalOrders> RetrievesExcel(string facility)
        {
            var strSQL = new Sql(
            "select " +
                "StorerKey = rtrim(o.StorerKey) " +
                ",Facility = rtrim(o.Facility) " +
                ",TMSKey = o.TMSKey " +
                ",ExternOrderKey = rtrim(o.ExternOrderKey) " +
                ",CustomerOrderKey = rtrim(o.CustomerOrderKey) " +
                ",OrderType = rtrim(o.OrderType) " +
                ",OrderDate = convert(char(10),o.OrderDate,111) " +
                ",DeliveryDate = convert(char(10),o.DeliveryDate,111) " +
                ",ConsigneeKey = rtrim(o.ConsigneeKey) " +
                ",ShortName = rtrim(o.ShortName) " +
                ",PickUpConsigneeKey = rtrim(o.PickUpConsigneeKey) " +
                ",PickUpName = rtrim(o.PickUpName) " +
                ",PickUpAddress = rtrim(o.PickUpAddress) " +
                ",ShipToAddress = rtrim(o.ShipToAddress) " +
                ",Zip = rtrim(o.Zip) " +
                ",Contact = rtrim(o.Contact) " +
                ",Phone = rtrim(o.Phone) " +
                ",Notes = rtrim(o.Notes) " +
                ",AddDate = convert(char(10),o.AddDate,111) " +
                ",OrderLineNumber = rtrim(od.OrderLineNumber) " +
                ",Sku = rtrim(od.Sku) " +
                ",Descr = rtrim(od.Descr) " +
                ",OrderQty = od.OrderQty " +
                ",CaseQty = od.CaseQty " +
                ",Cube = od.Cube " +
                ",Weight = od.Weight " +
            "from Orders o " + 
            "join OrderDetail od on o.TMSKey = od.TMSKey ");

            if (!string.IsNullOrEmpty(facility))
            {
                strSQL.Where("o.ToFacility = @0", facility);
            }
            
            return DbManager.Create("bestlogtms").Fetch<NormalOrders>(strSQL);
        }
            

        public virtual NormalOrders RetrieveSkuByStorerKey(string Sku, string StorerKey, string facility) => DbManager.Create("bestlogtms").SingleOrDefault<NormalOrders>(
            "Select " +  
                "Descr = rtrim(s.Descr) " + 
                ",CaseCnt = p.CaseCnt " + 
                ",Pallet = p.Pallet " + 
                ",StdCube = s.stdcube " + 
                ",StdWeight = s.stdgrosswgt " + 
            "From BestLogWMS..Sku s " +
            "join BestLogWMS..Pack p on s.PackKey = p.PackKey " +
            "WHERE s.Sku = @0 and s.StorerKey = @1 " 
            ,Sku,StorerKey
        );

        public virtual NormalOrders RetrieveConsigneeKeyByStorerKeyAndConsigneeKey(string ConsigneeKey, string StorerKey, string facility) => DbManager.Create("bestlogtms").SingleOrDefault<NormalOrders>(
            "select " +
                "ShortName = rtrim(ShortName) " +
                ",ShipToAddress = rtrim(ShipToAddress) " +
                ",Zip = rtrim(Zip) " +
                ",Contact = rtrim(Contact) " +
                ",Phone = rtrim(Phone) " +
            "from BaseCustomer " +
            "where ConsigneeKey = @0 and StorerKey = @1 "
            ,ConsigneeKey,StorerKey
        );

        public virtual NormalOrders RetrieveExternOrderKey(string ExternOrderKey, string StorerKey)
        {
            var sql = new Sql("select top 1 ExternOrderKey from Orders ");
            sql.Where("ExternOrderKey = @0 and StorerKey = @1", ExternOrderKey, StorerKey);
            return DbManager.Create("bestlogtms").SingleOrDefault<NormalOrders>(sql);
        }

        public virtual ServerResponse SaveHeaderDetail(NormalOrders header, IEnumerable<NormalOrders> detail, string UserName, string Facility)
        {
            
            var res = new ServerResponse();
            
            var db = DbManager.Create("bestlogtms");
            try
            {
                db.BeginTransaction();
                //取得到貨客戶主檔
                var customer = db.SingleOrDefault<BaseCustomer>("SELECT * FROM BaseCustomer WHERE StorerKey = @0 AND ConsigneeKey = @1", header.StorerKey, header.ConsigneeKey.Trim()); 
                if (customer == null)
                {
                    res.message = "客戶主檔不存在";
                    return res;
                }

                // 20201124 Added by Andy.
                // 20201130 Modified by Andy. 新增貨主代號判斷
                // 檢查ExternOrderKey是否存在 存在則無法新增
                var externorderkey = db.SingleOrDefault<string>("SELECT ExternOrderKey FROM Orders WHERE ExternOrderKey = @0 AND StorerKey = @1", header.ExternOrderKey, header.StorerKey);

                //Insert master
                var orders = db.SingleOrDefault<NormalOrders>("SELECT TMSKey FROM Orders WHERE TMSKey = @0", header.TMSKey);
                var headers = string.Format(
                        //StorerKey,TMSKey,OriginalKey,ExternOrderKey,OrderType,ExternType,OrderDate,DeliveryDate,ConsigneeKey,ShortName
                        "('{0}', '{1}', '{2}', '{3}', '{4}', '{5}', convert(char(10),'{6}',111), convert(char(10),'{7}',111), '{8}', '{9}' " +
                        //,ShipToName,ShipToAddress,Notes,CaseQty,PalletQty,Weight,Cube,UpdateSource,ToFacility,Facility,AddDate,AddWho,EditDate,EditWho
                        ", '{10}', '{11}', '{12}', '{13}', '{14}', '{15}', '{16}', '{17}', '{18}', '{19}', getdate(), '{20}', getdate(), '{21}' " +
                        //PickUpConsigneeKey,PickUpName,PickUpAddress,Zip,Contact,Phone,CustomerOrderKey
                        ", '{22}', '{23}', '{24}', '{25}', '{26}', '{27}', '{28}')"
                        , header.StorerKey, header.TMSKey, header.TMSKey, header.ExternOrderKey, header.OrderType, "", header.OrderDate, header.DeliveryDate, customer.ConsigneeKey, customer.ShortName
                        , header.ShortName, customer.ShipToAddress, header.Notes, 0, 0, 0, 0, "手開單", Facility, Facility, UserName, UserName
                        , header.PickUpConsigneeKey, header.PickUpName, header.PickUpAddress, customer.Zip, customer.Contact, customer.Phone, header.CustomerOrderKey
                        );
                if (orders == null)
                {
                    if (!string.IsNullOrEmpty(externorderkey))
                    {
                        res.message = "貨主單號已存在";
                        return res;
                    }
                    string strSQL = "insert into Orders(StorerKey,TMSKey,OriginalKey,ExternOrderKey,OrderType " +
                        ",ExternType,OrderDate,DeliveryDate,ConsigneeKey,ShortName,ShipToName,ShipToAddress,Notes " +
                        ",CaseQty,PalletQty,Weight,Cube,UpdateSource,ToFacility,Facility,AddDate,AddWho,EditDate,EditWho " +
                        ",PickUpConsigneeKey,PickUpName,PickUpAddress,Zip,Contact,Phone,CustomerOrderKey) " + 
                        "VALUES " + headers;
                    db.Execute(strSQL);

                    //Insert newdetail
                    if (detail.Count() > 0)
                    {
                        foreach(var p in detail)
                        {
                            db.Execute(
                                " insert into OrderDetail (StorerKey,TMSKey,OrderLineNumber,ExternOrderKey " +
                                ",Sku,OrderQty,UpdateSource,OriginalKey,Notes)  " +
                                " VALUES  (@0, @1, @2, @3, @4, @5, @6, @7, @8) "
                                ,header.StorerKey, header.TMSKey, p.OrderLineNumber, header.ExternOrderKey
                                , p.Sku, p.OrderQty, "手開單", header.TMSKey,""
                            );
                        }
                    }
                    //更新訂單明細
                    db.Execute(
                        "Update OrderDetail set " +
                            "CaseQty = case when p.CaseCnt = 0 then 0 else od.OrderQty / p.CaseCnt end " +
                            ",PalletQty = case when p.Pallet = 0 then 0 else od.OrderQty / p.Pallet end " +
                            ",Cube = od.OrderQty * s.StdCube " +
                            ",Weight = od.OrderQty * s.StdgrossWGT " +
                            ",CaseCnt = p.CaseCnt " +
                            ",Pallet = p.Pallet " +
                            ",StdCube = s.StdCube " +
                            ",StdWeight = StdgrossWGT " +
                            ",Descr = rtrim(s.Descr) " +
                        "from OrderDetail od " +
                            "join BestLogWMS..Sku s on od.Sku = s.Sku and od.StorerKey = s.StorerKey " +
                            "join BestLogWMS..Pack p on s.PackKey = p.PackKey " +
                        "where od.TMSKey = @0  "
                        ,header.TMSKey
                    );
                    //更新訂單表頭
                    db.Execute(
                        "if object_id('tempdb..#temp') is not null drop table #temp ;" +
                        "select " +
                            "TMSKey = od.TMSKey " +
                            ",CaseQty = sum(od.CaseQty) " +
                            ",PalletQty = sum(od.PalletQty) " +
                            ",Cube = sum(od.Cube) " +
                            ",Weight = sum(od.Weight) " +
                        "into #temp " +
                        "from OrderDetail od " +
                        "where od.TMSKey = @0 " +
                        "group by od.TMSKey ;" +
                        "Update Orders set " +
                            "CaseQty = t.CaseQty " +
                            ",PalletQty = t.PalletQty " +
                            ",Cube = t.Cube " +
                            ",Weight = t.Weight " +
                        "from Orders o " +
                            "join #temp t on o.TMSKey = t.TMSKey " +
                        "where o.TMSKey = @0 "
                        ,header.TMSKey
                    );
                }
                else if (detail.Count() > 0)
                {
                    var sql = new Sql("UPDATE ORDERS");
                    sql.Set(
                        "ExternOrderKey = @0, OrderType = @1, OrderDate = @2, " +
                        "DeliveryDate = @3, ConsigneeKey = @4, ShortName = @5, ShipToName = @6, ShipToAddress = @7, " +
                        "Notes = @8, Facility = @9, EditDate = @10, EditWho = @11, " +
                        "PickUpConsigneeKey = @12, PickUpName = @13, Zip = @14, Contact = @15, Phone = @16, CustomerOrderKey = @17",
                        header.ExternOrderKey, header.OrderType, header.OrderDate,
                        header.DeliveryDate, customer.ConsigneeKey, customer.ShortName, customer.ShortName, customer.ShipToAddress,
                        header.Notes, Facility, DateTime.Now, UserName, header.PickUpConsigneeKey, header.PickUpName, customer.Zip, customer.Contact, customer.Phone, header.CustomerOrderKey
                    );
                    sql.Where("TMSKey = @0", header.TMSKey);
                    db.Execute(sql);
                                    
                    //20201019 Added by Andy 新增修改到貨地址 for LIAH01
                    //TODO 不正規，日後需改成對應各個用戶需求來更新CustOrders及CustOrderDetail
                    if (header.StorerKey.ToUpper().Trim() == "LIAH01")
                    {                        
                        //CustOrders_LIAH01 shipToAddressLine1 欄位型態: VARCHAR(100)
                        var address = db.SingleOrDefault<string>($"SELECT SUBSTRING(CONVERT(VARCHAR(100), '{customer.ShipToAddress}'), 1, 100)").TrimEnd();
                        sql = new Sql("UPDATE BestLogWMS..CustOrders_LIAH01");
                        sql.Set("shipToAddressNumber = @0", customer.ConsigneeKey);
                        sql.Set("shipToAddressLine1 = @0", address);
                        sql.Set("shipToAddressDescription = @0", customer.ShortName);
                        sql.Set("phoneAreaCode = @0", "");
                        sql.Set("phoneNumber = @0", customer.Phone);
                        sql.Set("EditWho = @0", UserName);
                        sql.Set("EditDate = @0", DateTime.Now);
                        sql.From("BestLogWMS..CustOrders_LIAH01 co");
                        sql.InnerJoin("Orders o").On("o.UpdateSource = co.OrderKey");
                        sql.Where("o.StorerKey = @0 AND o.ExternOrderKey = @1 AND o.TMSKey = @2", header.StorerKey, header.ExternOrderKey, header.TMSKey);
                        db.Execute(sql);
                    }                    

                    //訂單修改
                    foreach (var d in detail)
                    {
                        db.Execute("UPDATE ORDERDETAIL SET " + 
                                    "Sku = @2 " + 
                                    ",OrderQty = @3 " +
                                    ",CaseQty = case when CaseCnt = 0 then 0 else @3 / CaseCnt end " +
                                    ",PalletQty = case when Pallet = 0 then 0 else @3 / Pallet end " +
                                    ",Cube = @3 * StdCube " +
                                    ",Weight = @3 * Stdweight " +
                                    "WHERE TMSKey = @0 AND OrderLineNumber = @1",
                                    header.TMSKey, d.OrderLineNumber, d.Sku,d.OrderQty
                                    );
                    }

                    //更新訂單表頭
                    db.Execute(
                        "if object_id('tempdb..#temp') is not null drop table #temp ;" +
                        "select " +
                            "TMSKey = od.TMSKey " +
                            ",CaseQty = sum(od.CaseQty) " +
                            ",PalletQty = sum(od.PalletQty) " +
                            ",Cube = sum(od.Cube) " +
                            ",Weight = sum(od.Weight) " +
                        "into #temp " +
                        "from OrderDetail od " +
                        "where od.TMSKey = @0 " +
                        "group by od.TMSKey ;" +
                        "Update Orders set " +
                            "CaseQty = t.CaseQty " +
                            ",PalletQty = t.PalletQty " +
                            ",Cube = t.Cube " +
                            ",Weight = t.Weight " +
                        "from Orders o " +
                            "join #temp t on o.TMSKey = t.TMSKey " +
                        "where o.TMSKey = @0 "
                        ,header.TMSKey
                    );
                }

                db.CompleteTransaction();
                res.success = true;
                res.message = "";
            }
            catch (Exception ex)
            {
                db.AbortTransaction();
                res.success = false;
                res.message = ex.Message;
            }
            
            return res;
            
        }

        public virtual ServerResponse DeleteOrders(IEnumerable<string> TMSKey, string UserName)
        {
            var res = new ServerResponse();
            
            var db = DbManager.Create("bestlogtms");  
            var tmskey = string.Join(",", TMSKey.Select(p => string.Format("'{0}'", p.ToString())).ToArray());
            try
            {
                db.BeginTransaction(); 

                //檢查訂單狀態是否在待確認、待排車、保留
                var ordercheck = db.Fetch<string>($"SELECT TMSKey FROM Orders WHERE TMSKey IN ({tmskey}) and Status IN ('0', '1', '8') ");
                if(ordercheck.Count() == 0)
                {
                    res.message = "請確認訂單在以下狀態：待確認、待排車、保留訂單";
                    return res;
                }
                
                //檢查訂單是否有被配貨
                var pickdetailcount = db.SingleOrDefault<int>(
                    $@"
                        SELECT
                        COUNT(wpd.OrderKey)
                        FROM
                        Orders o (NOLOCK)
                        JOIN BestLogWMS..ORDERS wo (NOLOCK) ON wo.OrderKey = o.UpdateSource
                        JOIN BestLogWMS..ORDERDETAIL wod (NOLOCK) ON wod.OrderKey = wo.OrderKey
                        LEFT JOIN BestLogWMS..PICKDETAIL wpd (NOLOCK) ON wpd.OrderKey = wod.OrderKey AND wpd.OrderLineNumber = wod.OrderLineNumber
                        WHERE o.TMSKey IN ({tmskey})
                    "
                );

                if (pickdetailcount > 0)
                {
                    res.message = "有訂單已配貨\n請通知相關人員確認後再刪除";
                    return res;
                }

                //將訂單狀態更新為D(刪除)
                db.Execute($"Update Orders set Status = 'D', EditWho = @0, EditDate = GETDATE() where TMSKey in ({tmskey})", UserName);

                //將WMS訂單同步更新
                //P.S: WMS Orders 已有Update trigger更新EditWho跟EditDate 所以無法更新UserName進去
                db.Execute(
                    "Update BestLogWMS..Orders set Type = '刪單' " + 
                    "from BestLogWMS..Orders os " +
                    "join Orders o on os.OrderKey = o.UpdateSource " +
                    $"where o.TMSKey in ({tmskey})"
                    );

                db.CompleteTransaction();
                res.success = true;
                res.message = "";
            }
            catch (Exception ex)
            {
                db.AbortTransaction();
                throw ex;
            }
            return res;
        }


        //ASN轉RC
        public virtual ServerResponse ASNtoRC(string asnkey, string storerkey, string consigneekey, string warehouse, string facility, string user)
        {
            ServerResponse res = new ServerResponse();
            var db = DbManager.Create("bestlogtms"); 
            try
            {
                db.BeginTransaction();
                
                //取得已轉RC單的ASNKey
                var asnkeys = db.Fetch<string>($"SELECT RTRIM(ASNKey) FROM BestLogWMS..ASN WHERE TransferRC = '1' AND ASNKey IN ({asnkey})");

                if (asnkeys.Count() > 0)
                {
                    db.AbortTransaction();
                    res.success = false;    
                    res.message = $"已有ASN轉RC的單號，入庫通知單號：{string.Join(",", asnkeys)}";
                    return res;
                }

                db.ExecuteNonQueryProc(
                    "SPNormalASNToRC", new 
                    { 
                        ASNKey = asnkey,
                        StorerKey = storerkey,
                        ConsigneeKey = consigneekey,
                        WareHouse = "BestLogWMS",
                        Facility = facility, 
                        User = user
                    }                
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

    }    
}