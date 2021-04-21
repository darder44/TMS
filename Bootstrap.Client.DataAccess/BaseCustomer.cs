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
    [TableName("BaseCustomer")]
    public class BaseCustomer
    {
        public string Id { get; set; } //前端呼叫新增編輯用
        
        public string StorerKey { get; set; }

        public string ConsigneeKey { get; set; }

        public string AreaCode { get; set; }

        public string Zip { get; set; }

        public string FullName { get; set; }

        public string ShortName { get; set; }

        public string Contact { get; set; }

        public string Phone { get; set; }

        public string ShipToAddress { get; set; }

        public string VehicleType { get; set; }

        public string DemandCode1 { get; set; }

        public string DemandCode2 { get; set; }

        public string ChannelType { get; set; }

        public string PickTool { get; set; }

        public string Channel { get; set; }

        public int? CodeDate1 { get; set; }

        public int? CodeDate2 { get; set; }

        public string Notes { get; set; }

        public string Fax { get; set; }

        public string PalletType { get; set; }

        public string PalletSpec { get; set; }

        public string Penalties { get; set; }

        public string DC { get; set; }

        public string UpdateSource { get; set; }

        public string AddWho { get; set; }

        public DateTime? Adddate { get; set; }

        public string EditWho { get; set; }

        public DateTime? EditDate { get; set; }

        public string CustGroup { get; set; }

        public string CustomerType { get; set; }

        public string InvoiceAddress { get; set; }

        public string OrderGroup { get; set; }

        public string CustomerEAN { get; set; }

        public string SalesOffice { get; set; }

        public string SoldTo { get; set; }

        public string SoldToName { get; set; }

        public string SoldToAddress { get; set; }

        public string ShipTo { get; set; }

        public string ShipToName { get; set; }

        public string SN { get; set; }

        public string PaperSize { get; set; }

        //查詢BaseCustomer 客戶主檔資料
        public virtual IEnumerable<BaseCustomer> Retrieves() => DbManager.Create("bestlogtms").Fetch<BaseCustomer>("SELECT * FROM BaseCustomer");

        //查詢主鍵是否重覆
        public virtual BaseCustomer RetrieveByStorerKeyAndConsigneeKey(string StorerKey, string ConsigneeKey) => DbManager.Create("bestlogtms").SingleOrDefault<BaseCustomer>(
            "SELECT * " +
            "FROM BaseCustomer bc " +
            "WHERE bc.StorerKey = @0 and bc.ConsigneeKey = @1 " 
            ,StorerKey ,ConsigneeKey 
        );

        /// <summary>
        /// 新增客戶資料
        /// </summary>
        /// <param name="value"></param>
        public virtual bool Save(BaseCustomer value)
        {
            if (value == null) throw new ArgumentNullException(nameof(value));
            bool ret = false;
            var db = DbManager.Create("bestlogtms");
            try
            {
                //設定統倉DC 儲存值
                var blDC = (value.DC == "true") ? "Y" : "N";
                db.BeginTransaction();
                if (!db.Exists<BaseCustomer>("ConsigneeKey = @0", value.ConsigneeKey))
                {
                    db.Execute(
                        "insert into BaseCustomer " +
                        "(StorerKey, ConsigneeKey, AreaCode, Zip, FullName, ShortName, Contact, Phone, ShipToAddress, VehicleType, DemandCode1," +
                        " DemandCode2, ChannelType, PickTool, Channel, CodeDate1, CodeDate2, Notes, Fax, PalletType, PalletSpec," +
                        " Penalties, DC, UpdateSource, AddWho, Adddate, EditWho, EditDate, CustGroup, CustomerType, InvoiceAddress," +
                        " OrderGroup, CustomerEAN, SalesOffice, SoldTo, ShipTo, SN, PaperSize, SoldToName, SoldToAddress, ShipToName)" +
                        "VALUES" +
                        "(@0, @1, @2, @3, @4, @5, @6, @7, @8, @9, @10," +
                        " @11, @12, @13, @14, @15, @16, @17, @18, @19, @20," +
                        " @21, @22, @23, @24, @25, @26, @27, @28," +
                        " @29, @30, @31, @32, @33, @34, @35, " +
                        " @36, @37, @38, @39, @40 "  +
                        ")",
                        value.StorerKey, value.ConsigneeKey, value.AreaCode, value.Zip, value.FullName, value.ShortName, value.Contact, value.Phone, value.ShipToAddress, value.VehicleType, value.DemandCode1,
                        value.DemandCode2, value.ChannelType, value.PickTool, value.Channel, value.CodeDate1, value.CodeDate2, value.Notes, value.Fax, value.PalletType, value.PalletSpec, 
                        value.Penalties, blDC, value.UpdateSource, value.AddWho, value.Adddate, value.EditWho, value.EditDate, value.CustGroup, value.CustomerType, value.InvoiceAddress, 
                        value.OrderGroup, value.CustomerEAN, value.SalesOffice, value.SoldTo, value.ShipTo, value.SN, value.PaperSize,
                        value.SoldToName, value.SoldToAddress, value.ShipToName
                    );
                    db.CompleteTransaction();
                    ret = true;
                }else {
                    ret = false;
                }
            }
            catch (Exception ex)
            {
                db.AbortTransaction();
                throw ex;
            }
            return ret;
        }

        /// <summary>
        /// 更新儲位資料
        /// </summary>
        /// <param name="value"></param>
        public virtual bool Update(BaseCustomer value)
        {
            
            if (value == null) throw new ArgumentNullException(nameof(value));
            bool ret = false;
            var db = DbManager.Create("bestlogtms");            
            try
            {
                //設定統倉DC 儲存值
                var blDC = (value.DC == "true") ? "Y" : "N";
                db.BeginTransaction();
                if (db.Exists<BaseCustomer>("ConsigneeKey = @0", value.ConsigneeKey))
                {
                    db.Execute(
                        "UPDATE BaseCustomer SET " + 
                        "AreaCode = @2, Zip = @3, FullName = @4, ShortName = @5, Contact = @6, Phone = @7, ShipToAddress = @8, " + 
                        "VehicleType = @9, DemandCode1 = @10, DemandCode2 = @11, ChannelType = @12, PickTool = @13, " + 
                        "Channel = @14, CodeDate1 = @15, CodeDate2 = @16, Notes = @17, Fax = @18, PalletType = @19, PalletSpec = @20, Penalties = @21, " + 
                        "DC = @22, UpdateSource = @23, EditWho = @24, EditDate = @25, CustGroup = @26, CustomerType = @27, InvoiceAddress = @28, " + 
                        "OrderGroup = @29, CustomerEAN = @30, SalesOffice = @31, SoldTo = @32, ShipTo = @33, SN = @34, PaperSize = @35, SoldToName = @36, SoldToAddress = @37, ShipToName = @38 where StorerKey = @0 AND ConsigneeKey = @1",
                        value.StorerKey, value.ConsigneeKey, value.AreaCode, value.Zip, value.FullName, value.ShortName, value.Contact, value.Phone, value.ShipToAddress,
                        value.VehicleType, value.DemandCode1, value.DemandCode2, value.ChannelType, value.PickTool, 
                        value.Channel, value.CodeDate1, value.CodeDate2, value.Notes, value.Fax, value.PalletType, value.PalletSpec, value.Penalties, 
                        blDC, value.UpdateSource, value.EditWho, value.EditDate, value.CustGroup, value.CustomerType, value.InvoiceAddress, 
                        value.OrderGroup, value.CustomerEAN, value.SalesOffice, value.SoldTo, value.ShipTo, value.SN, value.PaperSize, value.SoldToName, value.SoldToAddress, value.ShipToName
                    );
                }                
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

        /// <summary>
        /// 刪除
        /// </summary>
        /// <param name="values"></param>
        public virtual bool Delete(IEnumerable<BaseCustomer> values)
        {
            bool ret = false;
            var db = DbManager.Create("bestlogtms");
            try
            {
                db.BeginTransaction();
                foreach (var bc in values)
                {
                    db.Execute("DELETE FROM BaseCustomer WHERE StorerKey = @0 AND ConsigneeKey = @1", bc.StorerKey, bc.ConsigneeKey);
                }                
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

