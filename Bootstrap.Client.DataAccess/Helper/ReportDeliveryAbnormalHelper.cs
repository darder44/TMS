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
    public static class ReportDeliveryAbnormalHelper
    {
        /// <summary>
        /// 訂單配送異常
        /// </summary>
        public static IEnumerable<ReportDeliveryAbnormal> Retrieves(string storers, string confirmnotes, string signdates, string signdatee, string deliverydates, string deliverydatee) 
        => DbContextManager.Create<ReportDeliveryAbnormal>().Retrieves(storers, confirmnotes, signdates, signdatee, deliverydates, deliverydatee);

    }
}
