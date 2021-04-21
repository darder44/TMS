using Bootstrap.Client.Query;
using Bootstrap.Client.DataAccess;
//using Bootstrap.DataAccess.BestApp;
//using Bootstrap.DataAccess.BestApp.Helper;
using Longbow.Web.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using Bootstrap.Client.DataAccess.Helper;
using Longbow.Data;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Bootstrap.Client.Controllers.Api
{
    ///<summary>
    ///司機清單Controller
    ///</summary>
    [Route("api/[controller]")]
    [ApiController]
    public class APP_DriverInfoController : ControllerBase
    {
        ///<summary>
        ///取得司機清單
        ///</summary>
        [HttpGet]
        public QueryData<object> Get([FromQuery]QueryAPP_DriverInfoOption value)
        {
            return value.RetrieveData();
        }

        /// <summary>
        /// 儲存司機資料
        /// </summary>
        /// <param name="value"></param>
        [HttpPost]
        [ButtonAuthorize(Url = "~/TMS/DriverInfo", Auth = "add,edit")]
        public bool Post([FromBody]APP_DriverInfo value)
        {            
            return APP_DriverInfoHelper.Save(value, User.Identity.Name);
        }

        /// <summary>
        /// 刪除司機
        /// </summary>
        /// <param name="value"></param>
        [HttpDelete]
        [ButtonAuthorize(Url = "~/TMS/DriverInfo", Auth = "del")]
        public bool Delete([FromBody]IEnumerable<string> value)
        {
            return APP_DriverInfoHelper.Delete(value);
        } 
    }
}
