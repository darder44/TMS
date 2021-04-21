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
    public static class ReportGoodDistributionHelper
    {
        public static IEnumerable<ReportGoodDistribution> Retrieves(string deliverydates, string deliverydatee, string doroutedates , string doroutedatee) => DbContextManager.Create<ReportGoodDistribution>().Retrieves( deliverydates, deliverydatee, doroutedates, doroutedatee);
    }
}
