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
    public static class ReportSignDetailHelper
    {
        public static IEnumerable<ReportSignDetail> Retrieves(string storers, string ordertypes, string deliverydates, string deliverydatee) 
            => DbContextManager.Create<ReportSignDetail>().Retrieves(storers, ordertypes, deliverydates, deliverydatee);
    }
}
