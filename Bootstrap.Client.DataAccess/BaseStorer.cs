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
    [TableName("BaseStorer")]
    public class BaseStorer
    {
        public string Id { get; set; }
        public string StorerKey { get; set; }

        public string Facility { get; set; }

        public string ChineseName { get; set; }

        public string EnglishName { get; set; }

        public string ShortName { get; set; }

        public string Phone { get; set; }

        public string Contact { get; set; }

        public string Address { get; set; }

        public string Description { get; set; }

        public string ShipListReport { get; set; }

        public string StorerStatus { get; set; }

        public string AddWho { get; set; }
        
        public DateTime? AddDate { get; set; }
        
        public string EditWho { get; set; }

        public DateTime? EditDate { get; set; }

        public IEnumerable<BaseStorer> RetrievesByReportShipList(IEnumerable<string> values)
        {
            var routes = string.Join(",", values.Select(p => string.Format("'{0}'", p.ToString())).ToArray());
            return DbManager.Create("bestlogtms").Fetch<BaseStorer>(
                "SELECT DISTINCT bs.ShipListReport, bs.StorerKey FROM BaseStorer bs " +
                "JOIN RouteHeader rh ON bs.StorerKey = rh.StorerKey " +	            	            
                $"WHERE rh.RouteNO IN ({routes})" 
            );
        }
        public virtual IEnumerable<BaseStorer> Retrieves() => DbManager.Create("bestlogtms").Fetch<BaseStorer>("select * from BaseStorer order by StorerKey");
        
        //查詢主鍵是否重覆
        public virtual BaseStorer RetrieveByStorerKey(string StorerKey) => DbManager.Create("bestlogtms").SingleOrDefault<BaseStorer>(
            "SELECT * " +
            "FROM BaseStorer bs " +
            "WHERE bs.StorerKey = @0 " 
            ,StorerKey
        );
        /// <summary>
        /// 新增貨主資料
        /// </summary>
        /// <param name="value"></param>

        public virtual bool Save(BaseStorer value)
        {
            if (value == null) throw new ArgumentNullException(nameof(value));
            bool ret = false;
            var db = DbManager.Create("bestlogtms");
            try
            {
                db.BeginTransaction();
                if (!db.Exists<BaseStorer>("Storerkey = @0 AND Facility = @1", value.StorerKey, value.Facility))
                {
                    db.Execute(
                        "INSERT INTO BaseStorer(" +
                        "StorerKey, Facility, ChineseName, EnglishName, ShortName, Phone," +
                        "Contact, Address, Description, ShipListReport, StorerStatus, " +
                        "AddWho, AddDate, EditWho, EditDate " +
                        ")" +
                        "VALUES( " +
                        "@0, @1, @2, @3, @4, @5, " +
                        "@6, @7, @8, @9, @10, " +
                        "@11, @12, @13, @14 " +
                        ")",
                        value.StorerKey, value.Facility, value.ChineseName, value.EnglishName, value.ShortName, value.Phone, 
                        value.Contact, value.Address, value.Description, value.ShipListReport, value.StorerStatus, 
                        value.AddWho, value.AddDate, value.EditWho, value.EditDate
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
        /// 編輯貨主
        /// </summary>
        /// <param name="value"></param>
        public virtual bool Update(BaseStorer value)
        {            
            if (value == null) throw new ArgumentNullException(nameof(value));
            bool ret = false;
            var db = DbManager.Create("bestlogtms");            
            try
            {                
                db.BeginTransaction();
                if (db.Exists<BaseStorer>("Storerkey = @0 AND Facility = @1", value.StorerKey, value.Facility))
                {
                    db.Update<BaseStorer>(
                        "SET " + 
                        "StorerKey = @StorerKey, Facility = @Facility, ChineseName = @ChineseName, EnglishName = @EnglishName, ShortName = @ShortName, Phone = @Phone, " +
                        "Contact = @Contact , Address = @Address, Description = @Description, ShipListReport = @ShipListReport, StorerStatus = @StorerStatus, " +
                        "EditWho = @EditWho, EditDate = @EditDate WHERE StorerKey = @StorerKey AND Facility = @Facility ",
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

        public virtual bool Delete(IEnumerable<BaseStorer> Values)
        {
            bool ret = false;
            var db = DbManager.Create("bestlogtms");
            try
            {
                db.BeginTransaction();
                foreach (var BS in Values)
                {
                    db.Delete<BaseStorer>("WHERE StorerKey = @StorerKey AND Facility = @Facility", BS);
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
