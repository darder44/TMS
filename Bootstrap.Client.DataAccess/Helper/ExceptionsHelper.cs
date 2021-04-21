using Longbow.Cache;
using Longbow.Data;
using Longbow.Web.Mvc;
using PetaPoco;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.IO;
using System.Linq;

namespace Bootstrap.Client.DataAccess
{
    /// <summary>
    /// 
    /// </summary>
    public static class ExceptionsHelper
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="ex"></param>
        /// <param name="additionalInfo"></param>
        /// <returns></returns>
        public static void Log(Exception ex, NameValueCollection additionalInfo)
        {
            var ret = DbContextManager.Create<Exceptions>()?.Log(ex, additionalInfo) ?? false;
        }
    }
}
