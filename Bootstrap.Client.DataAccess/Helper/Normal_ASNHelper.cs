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
    public static class Normal_ASNHelper
    { 
        public static IEnumerable<Normal_ASN> RetrievesASNnotinReceipt() => DbContextManager.Create<Normal_ASN>().RetrievesASNnotinReceipt();
    }
}
