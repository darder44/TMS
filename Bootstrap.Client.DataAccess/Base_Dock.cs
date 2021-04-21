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
    [TableName("BaseDock")]
    public class Base_Dock
    {
        public string Id { get; set; }

        public string Dock { get; set; }

        public string Notes { get; set; }

        public string AddWho { get; set; }

        public DateTime? AddDate { get; set; }

        public string EditWho { get; set; }

        public DateTime? EditDate { get; set; }

        public virtual IEnumerable<Base_Dock> Retrieves() => DbManager.Create("bestlogwms").Fetch<Base_Dock>("SELECT * FROM BaseDock");

        public virtual bool Save(Base_Dock value) 
        {
            bool ret = false;
            if (string.IsNullOrEmpty(value.Dock)) return ret;
            var db = DbManager.Create("bestlogwms");
            try
            {
                db.BeginTransaction();
                if (!db.Exists<Base_Dock>("Dock = @0", value.Dock.Trim()))
                {
                    db.Execute("INSERT INTO BaseDock(Dock, Notes, AddWho, AddDate, EditWho, EditDate) VALUES(@Dock, @Notes, @AddWho, @AddDate, @EditWho, @EditDate)", value);
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

        public virtual bool Update(Base_Dock value)
        {
            bool ret = false;
            if (string.IsNullOrEmpty(value.Dock)) return ret;
            var db = DbManager.Create("bestlogwms");
            try
            {
                db.BeginTransaction();
                if (db.Exists<Base_Dock>("Dock = @0", value.Dock.Trim()))
                {
                    db.Execute("UPDATE BaseDock SET Notes = @Notes, EditWho = @EditWho, EditDate = @EditDate WHERE Dock = @Dock", value);
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
                db.Delete<Base_Dock>($"WHERE Dock IN ({ids})");
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
