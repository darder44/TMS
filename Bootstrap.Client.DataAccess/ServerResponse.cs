using Bootstrap.Security.DataAccess;
using PetaPoco;
using System;
using System.Collections.Generic;
using Longbow.Web.Mvc;
using System.Linq;
using Newtonsoft.Json;

namespace Bootstrap.Client.DataAccess
{
    /// <summary>
    /// 伺服器交易回傳類
    /// </summary>
    public class ServerResponse
    {
        /// <summary>
        /// 成功/失敗
        /// </summary>
        public bool success { get; set; } = false;

        /// <summary>
        /// 訊息
        /// </summary>
        public string message { get; set; }
    }
}
