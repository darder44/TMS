using Bootstrap.Client.Query;
using Bootstrap.Client.DataAccess;
using Longbow.Web.Mvc;
using Longbow.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using Bootstrap.Client.DataAccess.Helper;

namespace Bootstrap.Client.Controllers.Api
{
    /// <summary>
    /// 郵遞區號Controller
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class BaseZipController : ControllerBase
    {
        ///<summary>
        ///
        ///</summary>
        [HttpGet] 
        public IEnumerable<BaseZip> Get([FromQuery]QueryBaseCustomerOption value)
        {
            return BaseZipHelper.GetZipInfo();
        }
    }
}
