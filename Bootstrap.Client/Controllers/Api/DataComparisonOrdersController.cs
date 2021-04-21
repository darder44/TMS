using Bootstrap.Client.Query;
using Bootstrap.Client.DataAccess;
using Longbow.Web.Mvc;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Bootstrap.Client.DataAccess.Helper;
using System.Collections.Generic;

namespace Bootstrap.Client.Controllers.Api
{
    ///<summary>
    ///訂單匯入Controller
    ///</summary>
    [Route("api/[controller]")]
    [ApiController]
    public class DataComparisonOrdersController : ControllerBase
    {
        ///<summary>
        ///
        ///</summary>
        [HttpGet]
        public QueryData<object> Get([FromQuery]QueryDataComparisonOrdersOption value)
        {
            return value.RetrieveData();
        }

        /// <summary>
        /// 保存
        /// </summary>
        /// <param name="value"></param>
        [HttpPost]
        [ButtonAuthorize(Url = "~/TMS/DataComparisonOrders", Auth = "add,edit")]
        public bool Post([FromBody]DataComparisonOrders value)
        {   
            if (string.IsNullOrEmpty(value.Id)) 
            {
                return DataComparisonOrdersHelper.Save(value);
            } 
            else
            {
                return DataComparisonOrdersHelper.Update(value);
            }
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="values"></param>
        [HttpDelete]
        [ButtonAuthorize(Url = "~/TMS/DataComparisonOrders", Auth = "del")]
        public bool Delete([FromBody]IEnumerable<string> values)
        {
            return DataComparisonOrdersHelper.Delete(values);
        }
    }
}