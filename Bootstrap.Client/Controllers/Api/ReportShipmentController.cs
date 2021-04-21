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
    ///<summary>
    ///
    ///</summary>
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class ReportShipmentController : ControllerBase
    {
        /// <summary>
        /// 
        /// </summary>
        [HttpGet]
        public QueryData<object> RetrieveData([FromQuery]QueryReportShipmentOption value)
        {
            return value.RetrieveData(BestHelper.GetFacility(User)); 
        }

        /// <summary>
        /// 根據路編取得貨主及對應報表名稱
        /// </summary>
        [HttpPost]
        public IEnumerable<BaseStorer> RetrievesByReportShipList([FromBody]IEnumerable<string> values)
        {            
            return BaseStorerHelper.RetrievesByReportShipList(values); 
        }
    }
}
