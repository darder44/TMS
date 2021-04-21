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
    [TableName("BaseCostCode")]
    public class BaseCostCode
    {
        public string Id { get; set; } //前端呼叫新增編輯用

        public string Storerkey { get; set; }

        public string CostCode { get; set; }

        public string CostName { get; set; }

        public string UOM { get; set; }

        public float Receivable { get; set; }

        public float Payable { get; set; }

        public string AreaStart { get; set; }

        public string AreaEnd { get; set; }

        public string Notes { get; set; }

        public string CostKind { get; set; }

        public string ARnoDistribution { get; set; }

        public string APnoDistribution { get; set; }

        public string MinimumCharge { get; set; }

        public string AddWho { get; set; }

        public DateTime? AddDate { get; set; }

        public string EditWho { get; set; }

        public DateTime? EditDate { get; set; }

        public string Facility { get; set; }


        //查詢BaseCostCode 計費代碼主檔資料
        public virtual IEnumerable<BaseCostCode> Retrieves() => DbManager.Create("bestlogtms").Fetch<BaseCostCode>("SELECT * FROM BaseCostCode");

        public virtual IEnumerable<BaseCostCode> RetrieveByStorerkeyAndCostCode(string Storerkey, string CostCode) 
        {
            var sql = new Sql("SELECT * FROM BaseCostCode");
            sql.Where("Storerkey = @0", Storerkey);
            if (!string.IsNullOrEmpty(CostCode))
            {
                sql.Where("CostCode = @0", CostCode);
            }
            return DbManager.Create("bestlogtms").Fetch<BaseCostCode>(sql);
        }

        public virtual IEnumerable<BaseCostCode> RetrieveCostCodeByStorerkeyAndCostKind(string Storerkey, string CostKind) 
        {

            string strSQL = 
                $@"select distinct CostCode from BaseCostCode where StorerKey = '{Storerkey}' and CostKind like '%{CostKind}%'";
            
            return DbManager.Create("bestlogtms").Fetch<BaseCostCode>(strSQL);
        }

        public virtual IEnumerable<BaseCostCode> RetrieveByStorerkeyAndCostKind(string Storerkey ) 
        {
            string strSQL = 
                $@"select distinct CostKind from BaseCostCode where StorerKey = '{Storerkey}'";
            return DbManager.Create("bestlogtms").Fetch<BaseCostCode>(strSQL);
        }

        /// <summary>
        /// 新增計費代碼
        /// </summary>
        /// <param name="value"></param>
        public virtual bool Save(BaseCostCode value)
        {
            if (value == null) throw new ArgumentNullException(nameof(value));
            bool ret = false;
            var db = DbManager.Create("bestlogtms");
            try
            {
                db.BeginTransaction();
                if (!db.Exists<BaseCostCode>("Storerkey = @0 AND CostCode = @1 AND Facility = @2", value.Storerkey, value.CostCode, value.Facility))
                {
                    db.Execute(
                        "INSERT INTO BaseCostCode(" +
                        "Storerkey, CostCode, CostName, UOM, Receivable, Payable, AreaStart, AreaEnd, Notes, CostKind, " +
                        "ARnoDistribution, APnoDistribution, MinimumCharge, Facility, AddWho, AddDate, EditWho, EditDate " +
                        ")" +
                        "VALUES( " +
                        "@0, @1, @2, @3, @4, @5, @6, @7, @8, @9, " +
                        "@10, @11, @12, @13, @14, @15, @16, @17 " +
                        ")",
                        value.Storerkey, value.CostCode, value.CostName, value.UOM, value.Receivable, value.Payable, value.AreaStart, value.AreaEnd, value.Notes, value.CostKind,
                        value.ARnoDistribution, value.APnoDistribution, value.MinimumCharge, value.Facility, value.AddWho, value.AddDate, value.EditWho, value.EditDate
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
        /// 更新計費代碼資料
        /// </summary>
        /// <param name="value"></param>
        public virtual bool Update(BaseCostCode value)
        {
            if (value == null) throw new ArgumentNullException(nameof(value));
            bool ret = false;
            var db = DbManager.Create("bestlogtms");            
            try
            {
                db.BeginTransaction();
                if (db.Exists<BaseCostCode>("Storerkey = @0 AND CostCode = @1 AND Facility = @2", value.Storerkey, value.CostCode, value.Facility))
                {
                    db.Update<BaseCostCode>(                        
                        "SET CostName = @CostName, UOM = @UOM, Receivable = @Receivable, Payable = @Payable, AreaStart = @AreaStart, AreaEnd = @AreaEnd, Notes = @Notes, CostKind = @CostKind, " +
                        "ARnoDistribution = @ARnoDistribution, APnoDistribution = @APnoDistribution, MinimumCharge = @MinimumCharge, Facility = @Facility, EditWho = @EditWho, EditDate = @EditDate WHERE CostCode = @CostCode AND Storerkey = @Storerkey AND Facility = @Facility",
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
        /// <param name="CostCode"></param>
        public virtual bool Delete(IEnumerable<BaseCostCode> Values)
        {
            bool ret = false;
            var db = DbManager.Create("bestlogtms");
            try
            {
                db.BeginTransaction();
                foreach (var BC in Values)
                {
                    db.Delete<BaseCostCode>($"WHERE CostCode = @0 AND Storerkey = @1 AND Facility = @2 ", BC.CostCode, BC.Storerkey, BC.Facility);                
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

