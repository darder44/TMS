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
    /// NormalRouteList
    /// </summary>
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class NormalRouteListController : ControllerBase
    {
        ///<summary>
        ///
        ///</summary>
        [HttpGet]
        public QueryData<object> RetrieveData([FromQuery]QueryNormalRouteListOption value)
        {
            return value.RetrieveData(BestHelper.GetFacility(User)); 
        }

        ///<summary>
        ///
        ///</summary>
        [HttpGet]
        public IEnumerable<NormalRouteList> RetrieveDetailByRouteNo([FromQuery]string RouteNo)
        {
            return NormalRouteListHelper.RetrieveDetailByRouteNo(RouteNo.Trim(), BestHelper.GetFacility(User));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="values"></param>
        [HttpPost]
        public bool CheckBeforeCarLeave([FromBody]IEnumerable<string> values)
        {
            return NormalRouteListHelper.CheckBeforeCarLeave(values, BestHelper.GetFacility(User));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="values"></param>
        [HttpPost]
        public bool CheckCarLeave([FromBody]IEnumerable<string> values)
        {
            return NormalRouteListHelper.CheckCarLeave(values, BestHelper.GetFacility(User));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="values"></param>
        [HttpPost]
        public bool CarLeave([FromBody]IEnumerable<string> values)
        {
            return NormalRouteListHelper.CarLeave(values, User.Identity.Name);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="values"></param>
        [HttpPost]
        public bool ReturnWMS([FromBody]IEnumerable<string> values)
        {
            return NormalRouteListHelper.ReturnWMS(values, User.Identity.Name);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="values"></param>
        [HttpDelete]
        public bool DeleteRoutes([FromBody]IEnumerable<string> values)
        {
            return NormalRouteListHelper.DeleteRoutes(values, User.Identity.Name, RoleHelper.RetrievesByUserName(User.Identity.Name));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="values"></param>
        [HttpPost]  
        public bool UpdateRouteNo([FromBody]UpdateRouteNoBody values)
        {
            return NormalRouteListHelper.UpdateRouteNo(values, User.Identity.Name);
        }
    }
}
