using Bootstrap.Security;
using Bootstrap.Security.DataAccess;
using Longbow.Cache;
using Longbow.Web.Mvc;
using Longbow.Data;
using PetaPoco;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;


namespace Bootstrap.Client.DataAccess.Helper
{
    public static class ReportVLLRouteListHelper
    {
        public static IEnumerable<ReportVLLRouteList> Retrieves(string facility, string wavekey, string routenostart, string routenoend, string doroutedatestart, string doroutedateend) => DbContextManager.Create<ReportVLLRouteList>().Retrieves(facility, wavekey, routenostart, routenoend, doroutedatestart, doroutedateend);

        public static IEnumerable<ReportVLLRouteList> RetrievesDetail(string routeno) => DbContextManager.Create<ReportVLLRouteList>().RetrievesDetail(routeno);

        public static IEnumerable<ReportVLLRouteList> RetrievesExcel(string facility, string wavekey, string routenostart, string routenoend, string doroutedatestart, string doroutedateend) => DbContextManager.Create<ReportVLLRouteList>().RetrievesExcel(facility, wavekey, routenostart, routenoend, doroutedatestart, doroutedateend);

        public static IEnumerable<ReportVLLRouteList> SelectTMSKeyAndOrderTypeByRouteNo(IEnumerable<string> routelist) => DbContextManager.Create<ReportVLLRouteList>().SelectTMSKeyAndOrderTypeByRouteNo(routelist);

        //VLL裝載報表
        public static IEnumerable<ReportVLLRouteList> RetrieveByRouteNo(IEnumerable<string> RouteNos, string facility) => DbContextManager.Create<ReportVLLRouteList>().RetrieveByRouteNo(RouteNos, facility);
        //VLL裝載明細報表
        public static IEnumerable<ReportVLLRouteList> RetrieveDetailByRouteNo(IEnumerable<string> RouteNos, string facility) => DbContextManager.Create<ReportVLLRouteList>().RetrieveDetailByRouteNo(RouteNos, facility);
    }
}
