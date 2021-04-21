using System;
using System.Reflection;
using System.Linq;
using System.Collections.Generic;

namespace Bootstrap.Client.DataAccess.ShipListReport.Helper
{
    /// <summary>
    /// 
    /// </summary>
    public static class ShipListReportHelper
    {
        /// <summary>
        /// 取得貨主的ShipListReport類別
        /// </summary>
        /// <returns></returns>
        private static T GetShipListReportClass<T>(string storerkey)
        {
            var tofind = storerkey.ToLower().Trim();
            var targettype = typeof(T);
            var type = AppDomain.CurrentDomain.GetAssemblies()
                .SelectMany(s => s.GetTypes())
                .Where(p => targettype.IsAssignableFrom(p) && p.Name.ToLower().StartsWith(tofind) ).SingleOrDefault();
            if (type != null) targettype = type;
            var ret = (T)Activator.CreateInstance(targettype);            
            return ret;
        }

        /// <summary>
        /// 取得出貨單
        /// </summary>
        /// <returns></returns>
        public static object FetchShipListReport(IEnumerable<string> RouteNos, IEnumerable<string> TMSKeys, string ShipListReport)
        {
            var StorerKey = ShipListReport.ToLower().Replace("shiplist", "");
            var ret = GetShipListReportClass<ShipListReport>(StorerKey);
            MethodInfo method = ret.GetType().GetMethod("Fetch");
            MethodInfo generic = method.MakeGenericMethod(ret.GetType());
            var reports = generic.Invoke(ret, new object[] { RouteNos, TMSKeys, ShipListReport });
            return reports;
        }
    }
}
