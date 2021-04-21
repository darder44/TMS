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
    public static class VLLxDockHelper
    {
        public static IEnumerable<VLLxDock> Retrieves(String Facility) => DbContextManager.Create<VLLxDock>().Retrieves(Facility); 
    }
}
