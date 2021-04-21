using Bootstrap.Security;
using Bootstrap.Security.DataAccess;
using Longbow.Security.Cryptography;
using Longbow.Web.Mvc;
using PetaPoco;
using System;
using System.Collections.Generic;
using System.Linq;
using Longbow.Data;

namespace Bootstrap.Client.DataAccess
{
    /// <summary>

    /// </summary>
    [TableName("BaseZip")]
    public class BaseZip
    {
        /// <summary>
        /// 
        /// </summary>
        public string ZIP { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string AreaCode { get; set; } 
        /// <summary>
        /// 
        /// </summary>
        public int Description { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int City { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string Area { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string Dcode { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string CityCode { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string ZIPINFO { get; set; } //alley


        public virtual IEnumerable<BaseZip> GetZipInfo() //alley
        => DbManager.Create("bestlogtms").Fetch<BaseZip>("SELECT ZIP + ' ' + DESCRIPTION AS ZIPINFO, ZIP " +
                                                     "From BaseZip");

        public virtual IEnumerable<BaseZip> GetAreaCode() //Andy
        => DbManager.Create("bestlogtms").Fetch<BaseZip>("SELECT DISTINCT AreaCode FROM BaseZip ");
    }
}
