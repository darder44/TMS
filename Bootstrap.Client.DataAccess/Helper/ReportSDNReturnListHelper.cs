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
    public static class ReportSDNReturnListHelper
    {
        public static IEnumerable<ReportSDNReturnList> Retrieves(string storers, string ordertypes, string areacodes, string companies, string facilies, string orderstatus,string tmskeys, string externorderkeys, string ordermethod, string confirmdates, string confirmdatee, string deliverydates, string deliverydatee) => DbContextManager.Create<ReportSDNReturnList>().Retrieves(storers, ordertypes, areacodes, companies, facilies, orderstatus, tmskeys, externorderkeys, ordermethod, confirmdates, confirmdatee, deliverydates, deliverydatee); 
        public static IEnumerable<ReportSDNReturnList> RetrieveDetail(string TMSKey, string ordermethod) => DbContextManager.Create<ReportSDNReturnList>().RetrieveDetail(TMSKey,ordermethod);
        public static IEnumerable<ReportSDNReturnList> RetrieveExcelDetail(string TMSKey, string ordermethod) => DbContextManager.Create<ReportSDNReturnList>().RetrieveExcelDetail(TMSKey, ordermethod); 
    }
}
