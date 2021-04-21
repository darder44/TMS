using Bootstrap.Security;
using Bootstrap.Security.DataAccess;
using Longbow.Cache;
using Longbow.Data;
using Longbow.Web.Mvc;
using PetaPoco;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace Bootstrap.Client.DataAccess.Helper
{
    public class BaseZipHelper
    {
        /// <summary>
        /// 所有郵遞區號緩存數據鍵值
        /// </summary>
        public const string RetrievesZipInfoDataKey = "BaseZipHelper-GetZipInfo";

        public static IEnumerable<BaseZip> GetZipInfo() => CacheManager.GetOrAdd(RetrievesZipInfoDataKey, key => DbContextManager.Create<BaseZip>().GetZipInfo());     
        
        /// <summary>
        /// 所有區域代碼緩存數據鍵值
        /// </summary>
        public const string RetrievesAreaCodeDataKey = "BaseZipHelper-GetAreaCode";
        public static IEnumerable<BaseZip> GetAreaCode() => CacheManager.GetOrAdd(RetrievesAreaCodeDataKey, key => DbContextManager.Create<BaseZip>().GetAreaCode());     
    }
}