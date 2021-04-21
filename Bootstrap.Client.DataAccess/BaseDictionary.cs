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
    [TableName("BaseDictionary")]
    public class BaseDictionary
    {
        public string Id { get; set; } //前端呼叫新增編輯用
        public string CodeName { get; set; }
        public string Code { get; set; }
        public string Description { get; set; }
        public string Facility { get; set; }
        public DateTime? AddDate { get; set; }
        public string AddWho { get; set; }
        public DateTime? EditDate { get; set; }
        public string EditWho { get; set; }

        //查詢字典表  
        public virtual IEnumerable<BaseDictionary> Retrieves() => DbManager.Create("BestLogCommon").Fetch<BaseDictionary>("SELECT * FROM BaseDictionary order by CodeName,Code");

        /// <summary>
        /// 新增轉運站
        /// </summary>
        /// <param name="value"></param>
        public virtual bool Save(BaseDictionary value)
        {
            if (value == null) throw new ArgumentNullException(nameof(value));
            bool ret = false;
            var db = DbManager.Create("BestLogCommon");            
            try
            {
                db.BeginTransaction();
                if (!db.Exists<BaseDictionary>("CodeName = @0 AND Code = @1", value.CodeName, value.Code))
                {
                    db.Execute(
                        "INSERT INTO BaseDictionary " +
                        "(CodeName, Code, Description, Facility, AddWho, AddDate) " +
                        "VALUES (@CodeName, @Code, @Description, @Facility, @AddWho, @AddDate)",
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
        /// 編輯轉運站
        /// </summary>
        /// <param name="value"></param>
        public virtual bool Update(BaseDictionary value)
        {            
            if (value == null) throw new ArgumentNullException(nameof(value));
            bool ret = false;
            var db = DbManager.Create("BestLogCommon");            
            try
            {                
                db.BeginTransaction();
                if (db.Exists<BaseDictionary>("CodeName = @0 AND Code = @1", value.CodeName, value.Code))
                {
                    db.Update<BaseDictionary>(
                        "SET Facility = @Facility, Description = @Description, " +
                        "EditWho = @EditWho, EditDate = @EditDate WHERE CodeName = @CodeName AND Code = @Code",
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
        public virtual bool Delete(IEnumerable<BaseDictionary> values)
        {
            bool ret = false;
            var db = DbManager.Create("BestLogCommon");
            try
            {                
                db.BeginTransaction();
                foreach (var bd in values)
                {
                    db.Execute("DELETE FROM BaseDictionary WHERE CodeName = @0 AND Code = @1", bd.CodeName, bd.Code);
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

