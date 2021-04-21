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
    /// 訂單公版匯入
    /// </summary>
    [TableName("DataComparisonOrders")]
    public class DataComparisonOrders
    {
        public string Id { get; set; }
        /// <summary>
        /// StorerKey
        /// </summary>
        public string StorerKey { get; set; }
        /// <summary>
        /// InsertType
        /// </summary>
        public string InsertType { get; set; }
        /// <summary>
        /// Description
        /// </summary>
        public string Description { get; set; }
        /// <summary>
        /// Facility
        /// </summary>
        public string Facility { get; set; }
        /// <summary>
        /// ExternOrderKey
        /// </summary>
        public string ExternOrderKey { get; set; }
        /// <summary>
        /// CustomerOrderKey
        /// </summary>
        public string CustomerOrderKey { get; set; }
        /// <summary>
        /// OrderType
        /// </summary>
        public string OrderType { get; set; }
        /// <summary>
        /// ExternType
        /// </summary>
        public string ExternType { get; set; }
        /// <summary>
        /// OrderDate
        /// </summary>
        public string OrderDate { get; set; }
        /// <summary>
        /// DeliveryDate
        /// </summary>
        public string DeliveryDate { get; set; }
        /// <summary>
        /// ConsigneeKey
        /// </summary>
        public string ConsigneeKey { get; set; }
        /// <summary>
        /// ShortName
        /// </summary>
        public string ShortName { get; set; }
        /// <summary>
        /// PickUpConsigneeKey
        /// </summary>
        public string PickUpConsigneeKey { get; set; }
        /// <summary>
        /// PickUpName
        /// </summary>
        public string PickUpName { get; set; }
        /// <summary>
        /// PickUpAddress
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
        /// ShipToAddress
        /// </summary>
        public string ShipToAddress { get; set; }
        /// <summary>
        /// Zip
        /// </summary>
        public string Zip { get; set; }
        /// <summary>
        /// Contact
        /// </summary>
        public string Contact { get; set; }
        /// <summary>
        /// Phone
        /// </summary>
        public string Phone { get; set; }
        /// <summary>
        /// Notes
        /// </summary>
        public string Notes { get; set; }
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
        /// InvoiceNo
        /// </summary>
        public string InvoiceNo { get; set; }
        /// <summary>
        /// OTQty
        /// </summary>
        public string OTQty { get; set; }
        /// <summary>
        /// ExternLineNumber
        /// </summary>
        public string ExternLineNumber { get; set; }
        /// <summary>
        /// Sku
        /// </summary>
        public string Sku { get; set; }
        /// <summary>
        /// OrderQty
        /// </summary>
        public string OrderQty { get; set; }




        public virtual IEnumerable<DataComparisonOrders> Retrives() => DbManager.Create("bestlogtms").Fetch<DataComparisonOrders>("SELECT * FROM DataComparisonOrders");
        public virtual DataComparisonOrders RetriveByStorerKey(string StorerKey) => DbManager.Create("bestlogtms").FirstOrDefault<DataComparisonOrders>("SELECT * FROM DataComparisonOrders WHERE StorerKey = @0", StorerKey);

        public virtual bool Save(DataComparisonOrders value) 
        {
            bool ret = false;
            if(string.IsNullOrEmpty(value.StorerKey)) return ret;
            var db = DbManager.Create("bestlogtms");
            if (db.Exists<DataComparisonOrders>("StorerKey = @0", value.StorerKey.Trim())) return ret;
            try
            {
                db.BeginTransaction();
                db.Execute(
                    "insert into DataComparisonOrders(" + 
                    "StorerKey,InsertType,Description,Facility,ExternOrderKey,CustomerOrderKey,OrderType,ExternType,OrderDate" + 
                    ",DeliveryDate,ConsigneeKey,ShortName,PickUpConsigneeKey,PickUpName,PickUpAddress,SoldTo,SoldToName,SoldToAddress" + 
                    ",ShipTo,ShipToName,ShipToAddress,Zip,Contact,Phone,Notes,UrgentMark,ReserveMark" + 
                    ",ColdMark,InvoiceNo,OTQty,ExternLineNumber,Sku,OrderQty) " + 
                    "values(@0, @1, @2, @3, @4, @5, @6, @7, @8" + 
                    ", @9, @10, @11, @12, @13, @14, @15, @16, @17" + 
                    ", @18, @19, @20, @21, @22, @23, @24, @25, @26" + 
                    ", @27, @28, @29, @30, @31, @32)"
                    ,value.StorerKey,"NormalOrder","一般訂單匯入",value.Facility,value.ExternOrderKey,value.CustomerOrderKey,value.OrderType,value.ExternType,value.OrderDate
                    ,value.DeliveryDate,value.ConsigneeKey,value.ShortName,value.PickUpConsigneeKey,value.PickUpName,value.PickUpAddress,value.SoldTo,value.SoldToName,value.SoldToAddress
                    ,value.ShipTo,value.ShipToName,value.ShipToAddress,value.Zip,value.Contact,value.Phone,value.Notes,value.UrgentMark,value.ReserveMark
                    ,value.ColdMark,value.InvoiceNo,value.OTQty,value.ExternLineNumber,value.Sku,value.OrderQty
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

        public virtual bool Update(DataComparisonOrders value) 
        {
            bool ret = false;
            if(string.IsNullOrEmpty(value.StorerKey)) return ret;
            var db = DbManager.Create("bestlogtms");
            if (!db.Exists<DataComparisonOrders>("StorerKey = @0", value.StorerKey.Trim())) return ret;
            try
            {
                db.BeginTransaction();
                db.Execute(
                    "UPDATE DataComparisonOrders SET Facility = @2,ExternOrderKey = @3,CustomerOrderKey = @4,OrderType = @5,ExternType = @6,OrderDate = @7 " +
                    ",DeliveryDate = @8,ConsigneeKey = @9,ShortName = @10,PickUpConsigneeKey = @11,PickUpName = @12,PickUpAddress = @13,SoldTo = @14,SoldToName = @15,SoldToAddress = @16 " +
                    ",ShipTo = @17,ShipToName = @18,ShipToAddress = @19,Zip = @20,Contact = @21,Phone = @22,Notes = @23,UrgentMark = @24,ReserveMark = @25 " +
                    ",ColdMark = @26,InvoiceNo = @27,OTQty = @28,ExternLineNumber = @29,Sku = @30,OrderQty = @31 where StorerKey = @0 and InsertType = @1 "
                    ,value.StorerKey,"NormalOrder",value.Facility,value.ExternOrderKey,value.CustomerOrderKey,value.OrderType,value.ExternType,value.OrderDate
                    ,value.DeliveryDate,value.ConsigneeKey,value.ShortName,value.PickUpConsigneeKey,value.PickUpName,value.PickUpAddress,value.SoldTo,value.SoldToName,value.SoldToAddress
                    ,value.ShipTo,value.ShipToName,value.ShipToAddress,value.Zip,value.Contact,value.Phone,value.Notes,value.UrgentMark,value.ReserveMark
                    ,value.ColdMark,value.InvoiceNo,value.OTQty,value.ExternLineNumber,value.Sku,value.OrderQty
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

        public virtual bool Delete(IEnumerable<string> values) 
        {
            var ret = false;
            var db = DbManager.Create("bestlogtms");
            try
            {
                var keys = string.Join(",", values.Select(p => string.Format("'{0}'", p.ToString())).ToArray());
                db.BeginTransaction();
                db.Delete<DataComparisonOrders>($"where StorerKey in ({keys})");
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
