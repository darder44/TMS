using Bootstrap.Security;
using Bootstrap.Security.DataAccess;
using Longbow.Web.Mvc;
using PetaPoco;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using Longbow.Data;
using Longbow.Cache;
using Bootstrap.Client.DataAccess;


namespace Bootstrap.Client.DataAccess.Helper
{
    public class UpdateRouteNoBody
        {
            public string RouteNo { get; set; }
            public string DoRouteDate { get; set; }
            public string DockNo { get; set; }
            public string ExpectDate { get; set; }
            public string ExpectTime { get; set; }
            public BaseVehicle Driver { get; set; }
        }
    public static class NormalRouteListHelper
    {

        

        public static IEnumerable<NormalRouteList> Retrieves(string facility) => DbContextManager.Create<NormalRouteList>().Retrieves(facility);

        public static IEnumerable<NormalRouteList> RetrieveDetailByRouteNo(string RouteNo, string facility) => DbContextManager.Create<NormalRouteList>().RetrieveDetailByRouteNo(RouteNo, facility);

        public static bool CheckBeforeCarLeave(IEnumerable<string> Routes, string facility) => DbContextManager.Create<NormalRouteList>().CheckBeforeCarLeave(Routes, facility);
        
        public static bool CheckCarLeave(IEnumerable<string> Routes, string facility) => DbContextManager.Create<NormalRouteList>().CheckCarLeave(Routes, facility);

        public static bool CarLeave(IEnumerable<string> Routes, string UserName) 
        {
            var ret = DbContextManager.Create<NormalRouteList>().CarLeave(Routes, UserName);
            if (ret) CacheHelper.ClearAll();
            return ret;
        }

        public static bool ReturnWMS(IEnumerable<string> Routes, string UserName) 
        {
            var ret = DbContextManager.Create<NormalRouteList>().ReturnWMS(Routes, UserName);
            if (ret) CacheHelper.ClearAll();
            return ret;
        }

        public static bool UpdateRouteNo(UpdateRouteNoBody UpdateData, string UserName) 
        {
            var ret = DbContextManager.Create<NormalRouteList>().UpdateRouteNo(UpdateData, UserName);
            if (ret) CacheHelper.ClearAll();
            return ret;
        }

        public static bool DeleteRoutes(IEnumerable<string> Routes, string UserName, IEnumerable<string> Role) 
        {
            var ret = DbContextManager.Create<NormalRouteList>().DeleteRoutes(Routes, UserName, Role);
            if (ret) CacheHelper.ClearAll();
            return ret;
        }
    }
}
