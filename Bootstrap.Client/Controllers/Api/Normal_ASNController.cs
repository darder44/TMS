using Bootstrap.Client.Query;
using Bootstrap.Client.DataAccess;
using Longbow.Web.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using Bootstrap.Client.DataAccess.Helper;
using Microsoft.AspNetCore.Http;

namespace Bootstrap.Client.Controllers.Api
{
    ///<summary>
    ///ASNController
    ///</summary>
    [Route("api/[controller]")]
    [ApiController]
    public class Normal_ASNController : ControllerBase
    {
        ///<summary>
        ///
        ///</summary>
        [HttpGet]
        public QueryData<object> Get([FromQuery]QueryNormal_ASNOption value)
        {
            return value.RetrieveData(); 
        }
    }
}
