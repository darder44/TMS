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
    /// 訂單切割
    /// </summary>

    public class NormalCutOrders
    {
        /// <summary>
        /// Storerkey
        /// </summary>
        public string StorerKey { get; set; }
        /// <summary>
        /// TMSKey
        /// </summary>
        public string TMSKey { get; set; }
        /// <summary>
        /// ExternOrderkey
        /// </summary>
        public string ExternOrderKey { get; set; }
        /// <summary>
        /// OrderDate
        /// </summary>
        public DateTime? OrderDate { get; set; }
        /// <summary>
        /// DeliveryDate
        /// </summary>
        public DateTime? DeliveryDate { get; set; }
        /// <summary>
        /// ConsigneeKey
        /// </summary>
        public string ConsigneeKey { get; set; }
        /// <summary>
        /// ShortName
        /// </summary>
        public string ShortName { get; set; }
        /// <summary>
        /// ShipToAddress
        /// </summary>
        public string ShipToAddress { get; set; }
        /// <summary>
        /// Zip
        /// </summary>
        public string Zip { get; set; }
        /// <summary>
        /// OrderQty
        /// </summary>
        public double OrderQty { get; set; }
        /// <summary>
        /// CaseQty
        /// </summary>
        public double CaseQty { get; set; }
        /// <summary>
        /// PalletQty
        /// </summary>
        public double PalletQty { get; set; }
        /// <summary>
        /// Weight
        /// </summary>
        public double Weight { get; set; }
        /// <summary>
        /// Cube
        /// </summary>
        public double Cube { get; set; }
        /// <summary>
        /// Notes
        /// </summary>
        public string Notes { get; set; }
        /// <summary>
        /// OrderLineNumber
        /// </summary>
        public string OrderLineNumber { get; set; }
        /// <summary>
        /// Descr
        /// </summary>
        public string Descr { get; set; }
        /// <summary>
        /// CaseCnt
        /// </summary>
        public double CaseCnt { get; set; }
        /// <summary>
        /// Pallet
        /// </summary>
        public double Pallet { get; set; }
        /// <summary>
        /// Sku
        /// </summary>
        public string Sku { get; set; }
        /// <summary>
        /// StdCube
        /// </summary>
        public string StdCube { get; set; }
        /// <summary>
        /// StdWeight
        /// </summary>
        public string StdWeight { get; set; }
        /// <summary>
        /// CutPallet
        /// </summary>
        public string CutPallet { get; set; }
        /// <summary>
        /// CutCube
        /// </summary>
        public string CutCube { get; set; }
        /// <summary>
        /// CutWeight
        /// </summary>
        public string CutWeight { get; set; }


        //由所屬倉別設定，目前預設BestLogWMS
        string strDBName = "BestLogWMS";

        public virtual IEnumerable<NormalCutOrders> RetrieveOrders(string facility)
        {
            string strSQL = "";
            strSQL = 
            "select " +
                "StorerKey = rtrim(StorerKey) " +
                ",TMSKey = TMSKey " +
                ",ExternOrderKey = rtrim(ExternOrderKey) " +
                ",OrderDate = convert(char(10),OrderDate,111) " +
                ",DeliveryDate = convert(char(10),DeliveryDate,111) " +
                ",ConsigneeKey = rtrim(ConsigneeKey) " +
                ",ShortName = rtrim(ShortName) " +
                ",ShipToAddress = rtrim(ShipToAddress) " +
                ",Zip = rtrim(Zip) " +
                ",CaseQty = CaseQty " +
                ",PalletQty = PalletQty " +
                ",Cube = Cube " +
                ",Weight = Weight " +
                ",Notes = rtrim(Notes) " +
            "from " +
                "Orders  " +
            "where Status = '1' " ;
            //CT單也可切單
            //and left(TMSKey,2) <> 'CT'

            if(!string.IsNullOrEmpty(facility))
            {
                strSQL = strSQL + " and ToFacility = '" + facility + "' ";
            }



            return DbManager.Create("bestlogtms").Fetch<NormalCutOrders>(strSQL);
        }




        public virtual IEnumerable<NormalCutOrders> RetrieveDetailByTMSKey(string TMSKey){
            string strSQL = 
                "select " +
                    "TMSKey = od.TMSKey " +
                    ",OrderLineNumber = od.OrderLineNumber" +
                    ",Sku = rtrim(od.Sku) " +
                    ",Descr = rtrim(od.Descr) " +
                    ",CaseCnt = od.CaseCnt " +
                    ",Pallet = od.Pallet " +
                    ",OrderQty = od.OrderQty " +
                    ",CaseQty = od.CaseQty " +
                    ",PalletQTY = ROUND(od.PalletQty, 3) " +
                    ",Cube = ROUND(od.Cube, 3) " +
                    ",Weight = ROUND(od.Weight, 3) " +
                    ",StdCube = ROUND(od.StdCube, 3) " +
                    ",StdWeight = ROUND(od.StdWeight, 3) " +
                "from " +
                    "Orders o " +
                    "join OrderDetail od on o.TMSKey = od.TMSKey " +
                "where o.Status = '1'  and o.TMSKey = '" + TMSKey + "' " +
                "order by od.OrderLineNumber ";
            return DbManager.Create("bestlogtms").Fetch<NormalCutOrders>(strSQL);
        } 

        public virtual NormalCutOrders RetrieveSkuByTMSKey(double qty, string tmskey, string sku, string orderlinenumber){
            string strSQL = 
            "Select " +  
                "CutPallet = round(case when Pallet = 0 then 0 else '" + qty + "' * CaseCnt / Pallet end,3) " + 
                ",CutCube = round('" + qty + "' * CaseCnt * StdCube,3) " + 
                ",CutWeight = round('" + qty + "' * CaseCnt * StdWeight,3) " + 
            "From OrderDetail " +
            "WHERE TMSKey = '" + tmskey + "' and Sku = '" + sku + "' and OrderLineNumber = '" + orderlinenumber + "' ";

            return DbManager.Create("bestlogtms").SingleOrDefault<NormalCutOrders>(strSQL);
        } 

        public virtual bool CutOrders(NormalCutOrdersBody value, string UserName) {
            bool ret = false;
            var db = DbManager.Create("bestlogtms"); 
            try
            {
                db.BeginTransaction();             
                //取CT單號
                var nc = db.FetchProc<NCOUNTER>("SPNCOUNTER", new { keyname = "CutOrderKey" }).SingleOrDefault();
                var strNewCutOrderKey = string.Format("{0}{1:D8}", "CT", nc.keycount);
                
                var newdetails = string.Join(",", value.NewDetails.Select(p => string.Format(
                            //StorerKey,TMSKey,OrderLineNumber,ExternOrderKey,ExternLineNumber,Sku
                            "('{0}', '{1}', '{2}', '{3}', '{4}', '{5}' " +
                            //,OrderQty,CaseQty,PalletQty,Cube,Weight,UpdateSource,OriginalKey
                            ", '{6}', '{7}', '{8}', '{9}', '{10}', '{11}','{12}')"
                            ,"", strNewCutOrderKey, p.OrderLineNumber,"","",p.Sku
                            , 0, p.CaseQty, 0, 0, 0,"",value.TMSKey
                            )).ToArray()
                );

                //Insert Orders (CT單)
                db.Execute(
                    "Insert into Orders(StorerKey,TMSKey,OriginalKey,ExternOrderKey,CustomerOrderKey,OrderType,ExternType,OrderDate,DeliveryDate,ConsigneeKey,ShortName,SoldTo,SoldToName,SoldToAddress,ShipTo,ShipToName,ShipToAddress,Zip,Contact,Phone,Notes,CaseQty,PalletQty,Cube,Weight,UrgentMark,ReserveMark,ColdMark,Priority,UpdateSource,OriginalRouteNo,ToFacility,Facility,Status,AddDate  ,AddWho,EditDate ,EditWho) " +
                    "select             StorerKey,    @1,OriginalKey,ExternOrderKey,CustomerOrderKey,OrderType,ExternType,OrderDate,DeliveryDate,ConsigneeKey,ShortName,SoldTo,SoldToName,SoldToAddress,ShipTo,ShipToName,ShipToAddress,Zip,Contact,Phone,Notes,      0,        0,   0,     0,UrgentMark,ReserveMark,ColdMark,Priority,UpdateSource,OriginalRouteNo,ToFacility,Facility,Status,GETDATE(),    @2,GETDATE(),     @3  " +
                    "from Orders where TMSKey = @0"
                    ,value.TMSKey,strNewCutOrderKey,UserName,UserName
                );

                //Insert OrderDetail (CT單)
                db.Execute(
                    "Insert into OrderDetail (StorerKey,TMSKey,OrderLineNumber,ExternOrderKey,ExternLineNumber,Sku,OrderQty,CaseQty,PalletQty,Cube,Weight,UpdateSource,OriginalKey) " +
                    "values  " + newdetails
                );

                
                //Update OrderDetail (CT單)
                db.ExecuteNonQueryProc(
                    "SPNormalUpdateCutOrders" , new { TMSKey = value.TMSKey, CutOrderKey = strNewCutOrderKey } 
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
    }
}
