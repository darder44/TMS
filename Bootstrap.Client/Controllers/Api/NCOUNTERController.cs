using Bootstrap.Client.Query;
using Bootstrap.Client.DataAccess;
using Bootstrap.Client.DataAccess.Helper;
using Bootstrap.Security;
using Longbow.Web.Mvc;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;

namespace Bootstrap.Client.Controllers.Api
{
    /// <summary>
    /// 
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class NCOUNTERController : ControllerBase
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="keyname"></param>
        /// <returns></returns>
        [HttpGet]
        public NCOUNTER Get([FromQuery]string keyname)
        {
            return NCOUNTERHelper.RetrievesBykeyname(keyname);
        }
    }
}