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
    public static class ReportShipmentHelper
    {
        /// <summary>
        /// 出貨單
        /// </summary>
        public static IEnumerable<ReportShipment> Retrieves(string storers, string routeno, string carleavedates, string carleavedatee, string deliverydates, string deliverydatee, string facility) 
        => DbContextManager.Create<ReportShipment>().Retrieves(storers, routeno, carleavedates, carleavedatee, deliverydates, deliverydatee, facility);
    
    }
}
