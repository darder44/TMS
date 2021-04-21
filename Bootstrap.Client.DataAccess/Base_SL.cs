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
    [TableName("SL")]
    public class Base_SL
    {
        public string StorageLocation { get; set; }

        public string SLType { get; set; }

        public string AddWho { get; set; }

        public DateTime AddDate { get; set; }

        public string EditWho { get; set; }

        public DateTime EditDate { get; set; }

        public virtual IEnumerable<Base_SL> Retrieves() => DbManager.Create("bestlogwms").Fetch<Base_SL>("SELECT * FROM SL");

        public virtual IEnumerable<Base_SL> RetrieveByStorerkey(string StorerKey) => DbManager.Create("bestlogwms").Fetch<Base_SL>("SELECT * FROM SL WHERE SLType = @0", StorerKey);

        public virtual bool Save(Base_SL sl) 
        {
            bool ret = false;
            if(string.IsNullOrEmpty(sl.StorageLocation)) return ret;
            var db = DbManager.Create("bestlogwms");
            try
            {
                db.BeginTransaction();
                if (!db.Exists<Base_SL>("StorageLocation = @0", sl.StorageLocation.Trim()))
                {
                    db.Execute("INSERT INTO SL(StorageLocation, SLType) VALUES(@0, @1)", sl.StorageLocation.Trim(), sl.SLType.Trim());
                    ret = true;
                }
                else
                {
                    ret = false;
                }
                db.CompleteTransaction();
            }
            catch (Exception ex)
            {
                ret = false;
                db.AbortTransaction();
                throw ex;
            }
            return ret;
        } 

        public virtual bool Delete(IEnumerable<string> values) 
        {
            bool ret = false;
            var ids = string.Join(",", values.Select(p => string.Format("'{0}'", p)));
            var db = DbManager.Create("bestlogwms");
            try
            {
                db.BeginTransaction();
                db.Delete<Base_SL>($"where StorageLocation in ({ids})");
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
