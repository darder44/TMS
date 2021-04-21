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
    public static class ReportRequestHelper
    {
        public static IEnumerable<ReportRequest> Retrieves(string storerkey, string facility, string doroutedates, string doroutedatee, string deliverydates, string deliverydatee, string reporttype) => DbContextManager.Create<ReportRequest>().Retrieves( storerkey,  facility,  doroutedates,  doroutedatee,  deliverydates,  deliverydatee, reporttype); 
    }
}
