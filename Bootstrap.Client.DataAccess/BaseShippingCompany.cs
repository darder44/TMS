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
    [TableName("BaseShippingCompany")]
    [PrimaryKey("CompanyKey", AutoIncrement = false)]
    public class BaseShippingCompany
    {
        public string Id { get; set; } //前端呼叫新增編輯用
        public string CompanyKey { get; set; }
        public string FullName { get; set; }
        public string EngName { get; set; }
        public string ShortName { get; set; }
        public string Phone { get; set; }
        public string Contact { get; set; }
        public string Address { get; set; }
        public string Description { get; set; }
        public string AddWho { get; set; }
        public DateTime? AddDate { get; set; }
        public string EditWho { get; set; }
        public DateTime? EditDate { get; set; }


        //BaseShippingCompany 貨運公司主檔資料
        public virtual IEnumerable<BaseShippingCompany> Retrieves() => DbManager.Create("bestlogtms").Fetch<BaseShippingCompany>("SELECT * FROM BaseShippingCompany order by CompanyKey ");

        //查詢主鍵是否重覆
        public virtual BaseShippingCompany RetrieveByCompanyKey(string CompanyKey) => DbManager.Create("bestlogtms").SingleOrDefault<BaseShippingCompany>(
            "SELECT * " +
            "FROM BaseShippingCompany bc " +
            "WHERE bc.CompanyKey = @0 " 
            ,CompanyKey
        );


        /// <summary>
        /// 新增客戶資料
        /// </summary>
        /// <param name="value"></param>
        public virtual bool Save(BaseShippingCompany value)
        {
            if (value == null) throw new ArgumentNullException(nameof(value));
            bool ret = false;
            var db = DbManager.Create("bestlogtms");
            try
            {
                db.BeginTransaction();
                if (!db.Exists<BaseShippingCompany>("CompanyKey = @0", value.CompanyKey))
                {
                    db.Execute(
                        "INSERT INTO BaseShippingCompany " +
                        "(CompanyKey, FullName, EngName, ShortName, Phone, Contact " +
                        ",Address, Description, AddWho, AddDate, EditWho, EditDate " +
                        ")" +
                        "VALUES" +
                        "(@0, @1, @2, @3, @4, @5 " +
                        ",@6, @7, @8, @9, @10, @11 " +
                        ")",
                        value.CompanyKey, value.FullName, value.EngName, value.ShortName, value.Phone, value.Contact, 
                        value.Address, value.Description, value.AddWho, value.AddDate, value.EditWho, value.EditDate
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
        /// 更新資料
        /// </summary>
        /// <param name="value"></param>
        public virtual bool Update(BaseShippingCompany value)
        {            
            if (value == null) throw new ArgumentNullException(nameof(value));
            bool ret = false;
            var db = DbManager.Create("bestlogtms");            
            try
            {
                db.BeginTransaction();
                if (db.Exists<BaseShippingCompany>("CompanyKey = @0", value.CompanyKey))
                {
                    db.Update<BaseShippingCompany>(
                        "SET FullName = @1, EngName = @2, ShortName = @3, Phone = @4, Contact = @5 " +
                        ",Address = @6, Description = @7, EditWho = @8, EditDate = @9 where CompanyKey = @0",
                        value.CompanyKey, value.FullName, value.EngName, value.ShortName, value.Phone, value.Contact, 
                        value.Address, value.Description, value.EditWho, value.EditDate
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
                db.Delete<BaseShippingCompany>($"WHERE CompanyKey IN ({ids})");                
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

