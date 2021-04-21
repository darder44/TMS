
using PetaPoco;
using System;
using System.Collections.Generic;
using Longbow.Web.Mvc;
using System.Linq;

namespace Bootstrap.Client.DataAccess.ShipListReport
{
    /// <summary>
    /// 
    /// </summary>
    public class ShipListReport
    {        
        public virtual IEnumerable<T> Fetch<T>(IEnumerable<string> RouteNos, IEnumerable<string> TMSKeys, string ShipListReport)
        {
            var routenos = string.Join(",", RouteNos.Select(p => string.Format("'{0}'", p)));
            var tmskeys = string.Join(",", TMSKeys.Select(p => string.Format("'{0}'", p)));
            return DbManager.Create("bestlogtms").FetchProc<T>(
                "SPReportShipList", new
                {
                    Warehouse = "BestLogWMS",
                    RouteNo = routenos,
                    TMSKey = tmskeys,
                    ShipListReport = ShipListReport
                }
            );
        }        
    } 
}