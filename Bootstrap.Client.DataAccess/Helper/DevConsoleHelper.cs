using Longbow.Web.Mvc;
using Longbow.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Bootstrap.Client.DataAccess.Helper
{
    public class DevConsoleHelper
    {
        public static QueryData<object> Retrieves()
        {
            var data = DbContextManager.Create<DevConsole>().Retrieves();
            var ret = new QueryData<object>();
            ret.total = data.Count();
            ret.rows = data;
            return ret;
        }

        public static DevConsole Retrieve() => DbContextManager.Create<DevConsole>().Retrieve();
    }
}
