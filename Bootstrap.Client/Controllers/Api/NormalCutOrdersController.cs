using Bootstrap.Client.Query;
using Bootstrap.Client.DataAccess;
using Longbow.Web.Mvc;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using Bootstrap.Client.DataAccess.Helper;

namespace Bootstrap.Client.Controllers.Api
{   
    /// <summary>
    /// NormalCutOrders
    /// </summary>
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class NormalCutOrdersController : ControllerBase
    {
        /// <summary>
        /// 
        /// </summary>
        [HttpGet]
        public QueryData<object> RetrieveOrders([FromQuery]QueryNormalCutOrdersOption value)
        {
            return value.RetrieveOrders(BestHelper.GetFacility(User)); 
        }

        /// <summary>
        /// 
        /// </summary>
        [HttpGet]
        public IEnumerable<NormalCutOrders> RetrieveDetailByTMSKey([FromQuery]string TMSKey)
        {
            return NormalCutOrdersHelper.RetrieveDetailByTMSKey(TMSKey.Trim());
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="value"></param>
        [HttpPost]
        public bool CutOrders([FromBody]NormalCutOrdersBody value)
        {
            return NormalCutOrdersHelper.CutOrders(value, User.Identity.Name);
        }

        ///<summary>
        ///
        ///</summary>
        [HttpGet]
        public NormalCutOrders RetrieveSkuByTMSKey([FromQuery]double qty, string tmskey, string sku, string orderlinenumber)
        {
            return NormalCutOrdersHelper.RetrieveSkuByTMSKey(qty, tmskey, sku, orderlinenumber);
        }
    }
}
