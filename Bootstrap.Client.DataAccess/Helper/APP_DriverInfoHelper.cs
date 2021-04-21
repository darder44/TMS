using Bootstrap.Security;
using Bootstrap.Security.DataAccess;
using Longbow.Cache;
using Longbow.Web.Mvc;
using PetaPoco;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using Longbow.Data;

namespace Bootstrap.Client.DataAccess.Helper
{
    public static class APP_DriverInfoHelper
    {
        public static IEnumerable<APP_DriverInfo> Retrieves() => DbContextManager.Create<APP_DriverInfo>().Retrieves();
        public static APP_DriverInfo RetrieveDriverByID(string VEHICLE_ID_NO) => DbContextManager.Create<APP_DriverInfo>().RetrieveDriverByID(VEHICLE_ID_NO);
        public static IEnumerable<APP_DriverInfo> RetrievesDrivers(IEnumerable<string> VEHICLE_ID_NOs) => DbContextManager.Create<APP_DriverInfo>().RetrievesDrivers(VEHICLE_ID_NOs);

        private static bool UserChecker(APP_DriverInfo user)
        {  
            if (user.Driver_Phone?.Length > 18) user.Driver_Phone = user.Driver_Phone.Substring(0, 18);
            if (user.Password?.Length > 60) user.Password = user.Password.Substring(0, 60);
            var pattern = @"^[a-zA-Z0-9_@.]*$";
            return user.Driver_Phone.IsNullOrEmpty() || Regex.IsMatch(user.Driver_Phone, pattern);
        }

        public static bool Authenticate(string phone, string password, Action<LoginUser> config)
        {
            if (!UserChecker(new APP_DriverInfo { Driver_Phone = phone, Password = password })) return false;
            var loginUser = new LoginUser
            {
                UserName = phone,        
                LoginTime = DateTime.Now,
                Result = "登入失敗"
            };
            config(loginUser);
            var ret = string.IsNullOrEmpty(phone) ? false : DbContextManager.Create<APP_DriverInfo>().Authenticate(phone, password);
            if (ret)
            {
                DbContextManager.Create<APP_DriverInfo>().UpdateDriverStatus(phone, "1");
                loginUser.Result = "登入成功";
            } 
            return ret;
        }

        public static int Register(APP_DriverInfo driverInfo, string username)
        {
            if (!UserChecker(new APP_DriverInfo { Driver_Phone = driverInfo.Driver_Phone, Password = driverInfo.Password })) return 0;
            return string.IsNullOrEmpty(driverInfo.Driver_Phone) ? 0 : DbContextManager.Create<APP_DriverInfo>().Register(driverInfo, username);
        }
      
        public static QueryData<object> RetrievesByPhone(string phone)
        {
            var data = APP_DriverInfo.RetrievesByPhone(phone);
            var ret = new QueryData<object>();
            ret.total = data.Count();
            ret.rows = data;            
            return ret;
        }

        public static QueryData<object> ChangePassword(string phone, string pwd, string newpwd, string confirmpwd)
        {
            var data = APP_DriverInfo.ChangePassword(phone, pwd, newpwd, confirmpwd);
            if (data == null) return null;
            var ret = new QueryData<object>();
            ret.total = data.Count();
            ret.rows = data;
            return ret;
        }

        public static bool RegisterToken(string phone, string pushtoken, string platform)
        {            
            return APP_DriverInfo.RegisterToken(phone, pushtoken, platform);
        }

        /// <summary>
        /// 保存新建/更新的司機
        /// </summary>
        /// <param name="p"></param>
        /// <returns></returns>
        public static bool Save(APP_DriverInfo driverInfo, string username)
        {
            var ret = DbContextManager.Create<APP_DriverInfo>().Save(driverInfo, username);
            return ret;
        }

        /// <summary>
        /// 刪除司機
        /// </summary>
        /// <param name="value"></param>
        public static bool Delete(IEnumerable<string> value)
        {     
            var ret = DbContextManager.Create<APP_DriverInfo>().Delete(value);
            return ret;
        }
    }
}
