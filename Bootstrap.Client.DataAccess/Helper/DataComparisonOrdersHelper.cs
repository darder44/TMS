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
    public static class DataComparisonOrdersHelper
    {
      public static IEnumerable<DataComparisonOrders> Retrieves() => DbContextManager.Create<DataComparisonOrders>().Retrives();
      public static DataComparisonOrders RetrieveByStorerKey(string StorerKey) => DbContextManager.Create<DataComparisonOrders>().RetriveByStorerKey(StorerKey);
      public static bool Save(DataComparisonOrders value) => DbContextManager.Create<DataComparisonOrders>().Save(value);
      public static bool Update(DataComparisonOrders value) => DbContextManager.Create<DataComparisonOrders>().Update(value);
      public static bool Delete(IEnumerable<string> values) => DbContextManager.Create<DataComparisonOrders>().Delete(values);
    }
}
