using Bootstrap.Security.DataAccess;
using PetaPoco;
using System;
using System.Collections.Generic;
using Longbow.Web.Mvc;
using System.Linq;
using Newtonsoft.Json;
using Longbow.Data;

namespace Bootstrap.Client.DataAccess
{
    /// <summary>
    /// 
    /// </summary>
    [TableName("BaseFacility")]
    [PrimaryKey("ID", AutoIncrement = true)]
    public class BaseFacility
    {
        public string ID { get; set; }

        public string Facility { get; set; }
        
        public string Description { get; set; }

        public string Phone { get; set; }

        public string Address { get; set; }

        public string AddWho { get; set; }

        public DateTime? Adddate { get; set; }

        public string EditWho { get; set; }

        public DateTime? EditDate { get; set; }

        public string Type { get; set; }

        //查詢BaseFacility 轉運
        public virtual IEnumerable<BaseFacility> Retrieves() => DbManager.Create("bestlogtms").Fetch<BaseFacility>("SELECT * FROM BaseFacility");

        /// <summary>
        /// 新增轉運站
        /// </summary>
        /// <param name="value"></param>
        public virtual bool Save(BaseFacility value)
        {
            if (value == null) throw new ArgumentNullException(nameof(value));
            bool ret = false;
            var db = DbManager.Create("bestlogtms");            
            try
            {
                db.BeginTransaction();
                if (!db.Exists<BaseFacility>("ID = @0", value.ID))
                {
                    db.Insert(value);    
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
        /// 編輯轉運站
        /// </summary>
        /// <param name="value"></param>
        public virtual bool Update(BaseFacility value)
        {            
            if (value == null) throw new ArgumentNullException(nameof(value));
            bool ret = false;
            var db = DbManager.Create("bestlogtms");            
            try
            {                
                db.BeginTransaction();
                if (db.Exists<BaseFacility>("ID = @0", value.ID))
                {
                    db.Update<BaseFacility>(
                        "SET Facility = @Facility, Description = @Description,Phone = @Phone, Address = @Address, " +
                        "EditWho = @EditWho, EditDate = @EditDate, Type = @Type WHERE ID = @ID",
                        value
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
        public virtual bool Delete(IEnumerable<string> values)
        {
            bool ret = false;
            var ids = string.Join(",", values.Select(p => string.Format("'{0}'", p)));
            var db = DbManager.Create("bestlogtms");
            try
            {                
                db.BeginTransaction();
                db.Delete<BaseFacility>($"WHERE ID IN ({ids})");                
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

