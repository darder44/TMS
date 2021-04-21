using Bootstrap.Security;
using Bootstrap.Security.DataAccess;
using Longbow.Security.Cryptography;
using PetaPoco;
using System;
using System.Collections.Generic;
using System.Linq;
using Longbow.Data;

namespace Bootstrap.Client.DataAccess
{
    /// <summary>
    /// [BestAPP].[dbo].[APP_DriverInfo]
    /// </summary>
    [TableName("APP_DriverInfo")]
    [PrimaryKey("Driver_Phone")]
    public class APP_DriverInfo
    {
        /// <summary>
        /// 司機手機 即帳號
        /// </summary>
        public string Driver_Phone { get; set; }
        /// <summary>
        /// 密碼
        /// </summary>
        public string Password { get; set; }
        /// <summary>
        /// 密碼Salt
        /// </summary>
        public string PassSalt { get; set; }
        /// <summary>
        /// 車號
        /// </summary>
        public string VEHICLE_ID_NO { get; set; }
        /// <summary>
        /// 信箱
        /// </summary>
        public string Email { get; set; }
        /// <summary>
        /// 群組
        /// </summary>
        public string WorkGroup { get; set; }
        /// <summary>
        /// 司機名稱
        /// </summary>
        public string Driver { get; set; }
        /// <summary>
        /// 推播Token
        /// </summary>
        public string PushToken { get; set; }
        /// <summary>
        /// 狀態
        /// </summary>
        public string Status { get; set; }
        /// <summary>
        /// 誰新增的
        /// </summary>
        public string AddWho { get; set; }
        /// <summary>
        /// 新增時間
        /// </summary>
        public DateTime? AddDate { get; set; }
        /// <summary>
        /// 誰修改的
        /// </summary>
        public string EditWho { get; set; }
        /// <summary>
        /// 修改時間
        /// </summary>
        public DateTime? EditDate { get; set; }    
        /// <summary>
        /// 登入平台
        /// </summary>
        public string Platform { get; set; }  

        public virtual IEnumerable<APP_DriverInfo> Retrieves() => DbManager.Create("bestapp").Fetch<APP_DriverInfo>("SELECT * FROM APP_DriverInfo");

        public virtual APP_DriverInfo RetrieveDriverByID(string VEHICLE_ID_NO)
        {
            return DbManager.Create("bestapp").SingleOrDefault<APP_DriverInfo>("SELECT * FROM APP_DriverInfo WHERE VEHICLE_ID_NO =@0 AND Status = '1'", VEHICLE_ID_NO);             
        }
        
        public virtual IEnumerable<APP_DriverInfo> RetrievesDrivers(IEnumerable<string> VEHICLE_ID_NOs)
        {
            if (VEHICLE_ID_NOs.Count() < 1)
            {
                List<APP_DriverInfo> list = new List<APP_DriverInfo>();
                return list;
            }
            var sql = new Sql("SELECT * FROM APP_DriverInfo");         
            var Ids = string.Join(",", VEHICLE_ID_NOs.Select(p => string.Format("'{0}'", p.ToString())).ToArray());
            sql.Where($"VEHICLE_ID_NO IN ({Ids})");
            return DbManager.Create("bestapp").Fetch<APP_DriverInfo>(sql);
        }

        public static IEnumerable<APP_DriverInfo> Retrieves(IEnumerable<string> SelectedIds)
        {
            var sql = new Sql("select * from APP_DriverInfo");
            var Ids = string.Join(",", SelectedIds.Select(p => string.Format("'{0}'", p.ToString())).ToArray());
            sql.Where($"VEHICLE_ID_NO IN ({Ids})");
            return DbManager.Create("bestapp").Fetch<APP_DriverInfo>(sql);
        }

        /// <summary>
        /// 註冊司機
        /// </summary>
        /// <param name="driverInfo"></param>
        /// <param name="username"></param>
        /// <returns></returns>
        public virtual int Register(APP_DriverInfo driverInfo, string username)
        {
            var ret = 0;            
            var db = DbManager.Create("bestapp");
            try
            {
                db.BeginTransaction();
                var pas = driverInfo.Password;
                if (string.IsNullOrEmpty(pas)) { pas = "123789"; };
                driverInfo.PassSalt = LgbCryptography.GenerateSalt();
                driverInfo.Password = LgbCryptography.ComputeHash(pas, driverInfo.PassSalt);
                if (!db.Exists<APP_DriverInfo>("Driver_Phone = @0", driverInfo.Driver_Phone))
                {
                    driverInfo.AddDate = DateTime.Now;
                    driverInfo.AddWho = username;
                    driverInfo.EditDate = DateTime.Now;
                    driverInfo.EditWho = username;   
                    driverInfo.Status = "0";
                    db.Execute("Insert into APP_DriverInfo (Driver_Phone, Driver, VEHICLE_ID_NO, Email, WorkGroup, Status, Password, PassSalt, AddWho, AddDate, EditWho, EditDate) values(@0, @1, @2, @3, @4, @5, @6, @7, @8, @9, @10, @11)",
                        driverInfo.Driver_Phone, driverInfo.Driver, driverInfo.VEHICLE_ID_NO, driverInfo.Email, driverInfo.WorkGroup, driverInfo.Status, driverInfo.Password, driverInfo.PassSalt, driverInfo.AddWho, driverInfo.AddDate, driverInfo.EditWho, driverInfo.EditDate);
                    ret = 1;
                }
                else
                {
                    ret = 2;
                }
                db.CompleteTransaction();
            }
            catch (Exception ex)
            {
                db.AbortTransaction();
                throw ex;
            }
            return ret;
        }

        /// <summary>
        /// 修改司機資料
        /// </summary>
        /// <param name="driverInfo"></param>
        /// <param name="username"></param>
        /// <returns></returns>
        public virtual bool Save(APP_DriverInfo driverInfo, string username)
        {
            var ret = false;            
            var db = DbManager.Create("bestapp");
            try
            {
                db.BeginTransaction();
                var pas = driverInfo.Password;
                if (string.IsNullOrEmpty(pas)) { pas = "123789"; };
                driverInfo.PassSalt = LgbCryptography.GenerateSalt();
                driverInfo.Password = LgbCryptography.ComputeHash(pas, driverInfo.PassSalt);
                if (!db.Exists<APP_DriverInfo>("Driver_Phone = @0", driverInfo.Driver_Phone))
                {
                    driverInfo.AddDate = DateTime.Now;
                    driverInfo.AddWho = username;
                    driverInfo.EditDate = DateTime.Now;
                    driverInfo.EditWho = username;   
                    driverInfo.Status = "0";
                    db.Execute("Insert into APP_DriverInfo (Driver_Phone, Driver, VEHICLE_ID_NO, Email, WorkGroup, Status, Password, PassSalt, AddWho, AddDate, EditWho, EditDate) values(@0, @1, @2, @3, @4, @5, @6, @7, @8, @9, @10, @11)",
                        driverInfo.Driver_Phone, driverInfo.Driver, driverInfo.VEHICLE_ID_NO, driverInfo.Email, driverInfo.WorkGroup, driverInfo.Status, driverInfo.Password, driverInfo.PassSalt, driverInfo.AddWho, driverInfo.AddDate, driverInfo.EditWho, driverInfo.EditDate);
                }
                else
                {
                    db.Execute("Update APP_DriverInfo Set Driver = @1, VEHICLE_ID_NO = @2, Email = @3, WorkGroup = @4, Password = @5, PassSalt = @6, EditWho = @7, EditDate = @8 where Driver_Phone = @0",
                        driverInfo.Driver_Phone, driverInfo.Driver, driverInfo.VEHICLE_ID_NO, driverInfo.Email, driverInfo.WorkGroup, driverInfo.Password, driverInfo.PassSalt, username, DateTime.Now);
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
        /// 刪除司機
        /// </summary>
        /// <param name="value"></param>
        public virtual bool Delete(IEnumerable<string> value)
        {
            if (!value.Any()) return true;
            bool ret = false;
            var db = DbManager.Create("bestapp");
            try
            {
                var ids = string.Join(",", value.Select(p => string.Format("'{0}'", p.ToString())).ToArray());
                db.BeginTransaction();  
                db.Delete<APP_DriverInfo>($"Where Driver_Phone in ({ids})");
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

        public virtual bool Authenticate(string phone, string password)
        {
            var user = DbManager.Create("bestapp").SingleOrDefault<APP_DriverInfo>("select Password, PassSalt from APP_DriverInfo where Driver_Phone = @0", phone);
            return user != null && !string.IsNullOrEmpty(user.PassSalt) && user.Password == LgbCryptography.ComputeHash(password, user.PassSalt);                
        }

        public virtual bool UpdateDriverStatus(string phone, string status)
        {
            return DbManager.Create("bestapp").Update<APP_DriverInfo>("set Status = @1, EditWho = 'Server', EditDate = @2 where Driver_Phone = @0", phone, status, DateTime.Now) == 1;
        }

        public static IEnumerable<APP_DriverInfo> RetrievesByPhone(string phone)
        {
            var ret = DbManager.Create("bestapp").Fetch<APP_DriverInfo>("SELECT * " +
                                                                        "FROM APP_DriverInfo " +
                                                                        "WHERE Driver_Phone = @0", phone);
            return ret;
        }

        public static IEnumerable<APP_DriverInfo> ChangePassword(string phone, string pwd, string newpwd, string confirmpwd)
        {
            if (newpwd != confirmpwd) return null;
            var Authenticated = false;
            var user = DbManager.Create("bestapp").SingleOrDefault<APP_DriverInfo>("select Password, PassSalt from APP_DriverInfo where Driver_Phone = @0", phone);
            Authenticated = user != null && !string.IsNullOrEmpty(user.PassSalt) && user.Password == LgbCryptography.ComputeHash(pwd, user.PassSalt);
            if (!Authenticated) return null;
            var currentpassword = LgbCryptography.ComputeHash(newpwd, user.PassSalt);
            Authenticated = DbManager.Create("bestapp").Update<APP_DriverInfo>("set Password = @1 where Driver_Phone = @0", phone, currentpassword) == 1;
            if (!Authenticated) return null;
            var ret = DbManager.Create("bestapp").Fetch<APP_DriverInfo>("select * from APP_DriverInfo where Driver_Phone = @0", phone);
            return ret;
        }

        public static bool RegisterToken(string phone, string pushtoken, string platform)
        {
            var sql = new Sql("");
            sql.Set("PushToken = @0", pushtoken);
            sql.Set("EditWho = @0", "Server");
            sql.Set("EditDate = @0", DateTime.Now);
            if(!String.IsNullOrEmpty(platform)) sql.Set("Platform = @0", platform);
            sql.Where("Driver_Phone = @0", phone);
            return DbManager.Create("bestapp").Update<APP_DriverInfo>(sql) == 1;
        }
    }
}
