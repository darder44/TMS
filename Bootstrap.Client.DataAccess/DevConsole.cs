using Bootstrap.Security;
using Bootstrap.Security.DataAccess;
using Longbow.Security.Cryptography;
using PetaPoco;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Bootstrap.Client.DataAccess
{
    /// <summary>
    /// [BestAPP].[dbo].[DevConsole]
    /// </summary>
    [TableName("DevConsole")]
    public class DevConsole
    {
        /// <summary>
        /// GoogleAPIKey
        /// </summary>
        public string GoogleAPIKey { get; set; }
        /// <summary>
        /// GoogleServerKey
        /// </summary>
        public string GoogleServerKey { get; set; }
        /// <summary>
        /// GoogleSenderID
        /// </summary>
        public string GoogleSenderID { get; set; }

        public virtual IEnumerable<DevConsole> Retrieves()
        {
            var ret = DbManager.Create("bestapp").Fetch<DevConsole>("select * from DevConsole");
            return ret;
        }

        public virtual DevConsole Retrieve()
        {
            return DbManager.Create("bestapp").SingleOrDefault<DevConsole>("select * from DevConsole");            
        }
    }
}
