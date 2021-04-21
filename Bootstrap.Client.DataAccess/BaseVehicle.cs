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
    [TableName("BaseVehicle")]
    public class BaseVehicle
    {
        public string Id { get; set; } //前端呼叫新增編輯用
        public string VehicleKey { get; set; }

        public string CompanyKey { get; set; }

        public string VehicleType { get; set; }

        public string LoadingWeight { get; set; }

        public string LoadingCube { get; set; }

        public string Driver { get; set; }

        public string DriverPhone { get; set; }

        public string Description { get; set; }

        public int LoadingPallet { get; set; }

        public float CarWeight { get; set; }

        public string BoxType { get; set; }

        public float CarHeight { get; set; }

        public string UnloadingType { get; set; }

        public string EmployType { get; set; }

        public string Receiver { get; set; }

        public string PND { get; set; }

        public string ApproveWho { get; set; }

        public DateTime? ApproveDate { get; set; }

        public string ConfirmWho { get; set; }

        public DateTime? ConfirmDate { get; set; }

        public string Active { get; set; }

        public string AddWho { get; set; }

        public DateTime? AddDate { get; set; }

        public string EditWho { get; set; }

        public DateTime? EditDate { get; set; }


        //查詢BaseVehicle 車輛資料
        public virtual IEnumerable<BaseVehicle> Retrieves() => DbManager.Create("bestlogtms").Fetch<BaseVehicle>(
            "SELECT * FROM BaseVehicle"
        );

        //新增時 查詢主鍵重覆
        public virtual BaseVehicle RetrieveByVehicleKeyAndDriver(string VehicleKey, string Driver) => DbManager.Create("bestlogtms").SingleOrDefault<BaseVehicle>(
            "SELECT * " +
            "FROM BaseVehicle bv " +
            "WHERE bv.VehicleKey = @0 and bv.Driver = @1 " 
            ,VehicleKey, Driver
        );

        /// <summary>
        /// 新增車輛資料
        /// </summary>
        /// <param name="value"></param>
        public virtual bool Save(BaseVehicle value)
        {
            if (value == null) throw new ArgumentNullException(nameof(value));
            bool ret = false;
            var db = DbManager.Create("bestlogtms");
            try
            {
                //設定統倉DC 儲存值
                var blPND = (value.PND == "true") ? "Y" : "N";
                db.BeginTransaction();
                if (!db.Exists<BaseVehicle>("VehicleKey = @0 and Driver = @1", value.VehicleKey, value.Driver))
                {
                    db.Execute(
                        "INSERT INTO BaseVehicle" +
                        "(VehicleKey, CompanyKey, VehicleType, LoadingWeight, LoadingCube, Driver, DriverPhone, Description, LoadingPallet, " +
                        "CarWeight, BoxType, CarHeight, UnloadingType, EmployType, Receiver, PND, AddWho, AddDate, EditWho, EditDate, Active) " +
                        "VALUES " +
                        "(@0, @1, @2, @3, @4, @5, @6, @7, @8, @9, @10, " +
                        "@11, @12, @13, @14, @15, @16, @17, @18, @19, @20)",
                        value.VehicleKey, value.CompanyKey, value.VehicleType, value.LoadingWeight, value.LoadingCube, value.Driver, value.DriverPhone, value.Description, value.LoadingPallet, 
                        value.CarWeight, value.BoxType, value.CarHeight, value.UnloadingType, value.EmployType, value.Receiver, blPND, value.AddWho, value.AddDate, value.EditWho, value.EditDate, value.Active
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
        public virtual bool Update(BaseVehicle value)
        {
            
            if (value == null) throw new ArgumentNullException(nameof(value));
            bool ret = false;
            var db = DbManager.Create("bestlogtms");            
            try
            {
                //設定統倉DC 儲存值
                var blPND = (value.PND == "true") ? "Y" : "N";
                db.BeginTransaction();
                if (db.Exists<BaseVehicle>("VehicleKey = @0 and Driver = @1", value.VehicleKey, value.Driver))
                {
                    db.Update<BaseVehicle>(                        
                        "SET CompanyKey = @0, VehicleType = @1, LoadingWeight = @2, " +
                        "LoadingCube = @3, DriverPhone = @4, Description = @5, LoadingPallet = @6, " +
                        "CarWeight = @7, BoxType = @8, CarHeight = @9, UnloadingType = @10, EmployType = @11, " +
                        "Receiver = @12, PND = @13, " +
                        "EditWho = @14, EditDate = @15 ,Active = @16 " +
                        "Where VehicleKey = @17 and Driver = @18 ",
                        value.CompanyKey, value.VehicleType, value.LoadingWeight, 
                        value.LoadingCube, value.DriverPhone, value.Description, value.LoadingPallet, 
                        value.CarWeight, value.BoxType, value.CarHeight, value.UnloadingType, value.EmployType, 
                        value.Receiver, blPND, value.EditWho, value.EditDate, value.Active, value.VehicleKey, value.Driver
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
        /// <param name=""></param>
        public virtual bool Delete(IEnumerable<BaseVehicle> values)
        {
            bool ret = false;
            var db = DbManager.Create("bestlogtms");
            try
            {
                db.BeginTransaction();
                foreach (var BV in values)
                {
                    db.Delete<BaseVehicle>("WHERE VehicleKey = @VehicleKey AND Driver = @Driver", BV);
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

