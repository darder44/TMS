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
    public static class ReportTRPTrackHelper
    {
        public static IEnumerable<ReportTRPTrack> Retrieves(string SheetName, string storers, string ordertypes, string orderstatus, string consigneeKey, string waveKey, string tmskey, string externOrderKey, string areacodes, string routeno, string carleavedates, string carleavedatee, string deliverydates, string deliverydatee) 
            => DbContextManager.Create<ReportTRPTrack>().Retrieves(SheetName, storers, ordertypes, orderstatus, consigneeKey, waveKey, tmskey, externOrderKey, areacodes, routeno, carleavedates, carleavedatee, deliverydates, deliverydatee);

    }
}
