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
    /// 
    /// </summary>
    [TableName("STORER")]
    public class Storer
    {
        /// <summary>
        /// Id
        /// </summary>
        public string Id { get; set; }

        public string StorerKey { get; set; }

        public string type { get; set; }

        public string Company { get; set; }

        public string VAT { get; set; }

        public string Address1 { get; set; }

        public string Address2 { get; set; }

        public string Address3 { get; set; }

        public string Address4 { get; set; }

        public string City { get; set; }

        public string State { get; set; }

        public string Zip { get; set; }

        public string Country { get; set; }

        public string ISOCntryCode { get; set; }

        public string Contact1 { get; set; }

        public string Contact2 { get; set; }

        public string Phone1 { get; set; }

        public string Phone2 { get; set; }

        public string Fax1 { get; set; }

        public string Fax2 { get; set; }

        public string Email1 { get; set; }

        public string Email2 { get; set; }

        public string B_contact1 { get; set; }

        public string B_Contact2 { get; set; }

        public string B_Company { get; set; }

        public string B_Address1 { get; set; }

        public string B_Address2 { get; set; }

        public string B_Address3 { get; set; }

        public string B_Address4 { get; set; }

        public string B_City { get; set; }

        public string B_State { get; set; }

        public string B_Zip { get; set; }

        public string B_Country { get; set; }

        public string B_ISOCntryCode { get; set; }

        public string B_Phone1 { get; set; }

        public string B_Phone2 { get; set; }

        public string B_Fax1 { get; set; }

        public string B_Fax2 { get; set; }

        public string Notes1 { get; set; }

        public string Notes2 { get; set; }

        public string CreditLimit { get; set; }

        public string CartonGroup { get; set; }

        public string PickCode { get; set; }

        public string CreatePATaskOnRFReceipt { get; set; }

        public string CalculatePutAwayLocation { get; set; }

        public string Status { get; set; }

        public DateTime? AddDate { get; set; }

        public string AddWho { get; set; }

        public DateTime? EditDate { get; set; }

        public string EditWho { get; set; }

        public string TrafficCop { get; set; }

        public string ArchiveCop { get; set; }

        public string SkuRotat01 { get; set; }

        public string SkuRotat02 { get; set; }

        public string SkuRotat03 { get; set; }

        public string SkuRotat04 { get; set; }

        public string SkuRotat05 { get; set; }

        public string DefaultRotation { get; set; }

        public string AllocParm { get; set; }

        public string Putawaystrag { get; set; }

        public string ChargeBilling { get; set; }

        public string SkuASNChargeCode { get; set; }

        public string SkuSOChargeCode { get; set; }

        public string SkuTransportChargeCode { get; set; }

        public decimal? ASNMinCharge { get; set; }

        public decimal? SOMinCharge { get; set; }

        public decimal? ASNDocCharge { get; set; }

        public decimal? SODocCharge { get; set; }

        public string StorageCalc { get; set; }

        public string StorageBaseOn { get; set; }

        public decimal? StorageFixedRate { get; set; }

        public DateTime? StorageLastChargeDate { get; set; }

        public string GLDistributionKey { get; set; }

        public virtual IEnumerable<Storer> Retrieves() => DbManager.Create("bestlogwms").Fetch<Storer>("SELECT * FROM Storer");

        public virtual bool Save(Storer storer) 
        {
            bool ret = false;
            if(string.IsNullOrEmpty(storer.StorerKey)) return ret;
            var db = DbManager.Create("bestlogwms");
            if (db.Exists<Storer>("StorerKey = @0", storer.StorerKey)) return ret;
            try
            {
                db.BeginTransaction();
                db.Execute("INSERT INTO STORER(StorerKey, type, Company, VAT, Address1, Address2, Address3, Address4, City, State, Zip, Country, ISOCntryCode, Contact1, Phone1, Fax1, Email1, Contact2, Phone2, Fax2, Email2) values (@0, @1, @2, @3, @4, @5, @6, @7, @8, @9, @10, @11, @12, @13, @14, @15, @16, @17, @18, @19, @20)", 
                            storer.StorerKey.Trim(), "1", storer.Company, storer.VAT, storer.Address1, storer.Address2, storer.Address3, storer.Address4, storer.City, storer.State, storer.Zip, storer.Country, storer.ISOCntryCode, storer.Contact1, storer.Phone1, storer.Fax1, storer.Email1, storer.Contact2, storer.Phone2, storer.Fax2, storer.Email2);
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

        public virtual bool Update(Storer storer) 
        {
            bool ret = false;
            if(string.IsNullOrEmpty(storer.StorerKey)) return ret;
            var db = DbManager.Create("bestlogwms");
            if (!db.Exists<Storer>("StorerKey = @0", storer.StorerKey)) return ret;
            try
            {
                db.BeginTransaction();
                db.Execute("UPDATE STORER SET type = @1, Company = @2, VAT = @3, Address1 = @4, Address2 = @5, Address3 = @6, Address4 = @7, City = @8, State = @9, Zip = @10, Country = @11, ISOCntryCode = @12, Contact1 = @13, Phone1 = @14, Fax1 = @15, Email1 = @16, Contact2 = @17, Phone2 = @18, Fax2 = @19, Email2 = @20 WHERE StorerKey = @0", 
                            storer.StorerKey.Trim(), storer.type, storer.Company, storer.VAT, storer.Address1, storer.Address2, storer.Address3, storer.Address4, storer.City, storer.State, storer.Zip, storer.Country, storer.ISOCntryCode, storer.Contact1, storer.Phone1, storer.Fax1, storer.Email1, storer.Contact2, storer.Phone2, storer.Fax2, storer.Email2);
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

        public virtual bool Delete(Storer storer) 
        {
            return DbManager.Create("bestlogwms").Execute("DELETE FROM STORER WHERE StorerKey = @0", storer.StorerKey.Trim()) == 1;
        }
    }
}
