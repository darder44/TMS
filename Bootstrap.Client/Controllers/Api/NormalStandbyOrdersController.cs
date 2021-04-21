using Bootstrap.Client.Query;
using Bootstrap.Client.DataAccess;
using Longbow.Web.Mvc;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
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
    public class NormalStandbyOrdersController : ControllerBase
    {        
        ///<summary>
        ///
        ///</summary>
        [HttpGet]
        public QueryData<object> RetrieveOrders([FromQuery]QueryNormalStandbyOrdersOption value)
        {
            return value.RetrieveOrders(BestHelper.GetFacility(User));
        }

        /// <summary>
        /// 
        /// </summary>
        [HttpGet]
        public QueryData<object> RetrievesCustomerTemp([FromQuery]QueryNormalStandbyOrdersOption value)
        {
            return value.RetrievesCustomerTemp(BestHelper.GetFacility(User));
        }

        /// <summary>
        /// 
        /// </summary>
        [HttpGet]
        public QueryData<object> RetrievesReturnOrders([FromQuery]QueryNormalStandbyOrdersOption value)
        {
            return value.RetrievesReturnOrders(BestHelper.GetFacility(User));
        }

        /// <summary>
        /// 
        /// </summary>
        [HttpGet]
        public NormalStandbyOrders RetrievesCustomerByConsigneeKeyAndStorerKey([FromQuery] string storerkey, string consigneekey, string tmskey)
        {
            return NormalStandbyOrdersHelper.RetrievesCustomerByConsigneeKeyAndStorerKey(storerkey, consigneekey, tmskey, BestHelper.GetFacility(User));
        }

        /// <summary>
        /// 
        /// </summary>
        [HttpPost]
        public ServerResponse SaveCustomer([FromBody]NormalStandbyOrders value)
        {
            return NormalStandbyOrdersHelper.SaveCustomer(value,User.Identity.Name, BestHelper.GetFacility(User));
        } 

        /// <summary>
        /// 取得選取訂單
        /// </summary>
        [HttpPost]
        [ButtonAuthorize(Url = "~/TMS/NormalStandbyOrders", Auth = "NormalStandbyOrders")]
        public IEnumerable<NormalStandbyOrders> RetrieveSelectedOrders([FromBody]IEnumerable<string> TMSKey)
        {
            return NormalStandbyOrdersHelper.RetrieveSelectedOrders(TMSKey, BestHelper.GetFacility(User));
        }

        /// <summary>
        /// 新增路編
        /// </summary>
        [HttpPost]
        [ButtonAuthorize(Url = "~/TMS/NormalStandbyOrders", Auth = "NormalStandbyOrders")]
        public CreateNewRouteResponse CreateNewRoute([FromBody]NormalStandbyOrdersModal modal)
        {
            return NormalStandbyOrdersHelper.CreateNewRoute(modal.Driver, modal.SelectedOrders, User.Identity.Name, modal.DoRouteDate, modal.Dock, modal.ExpectDate, modal.ExpectTime, modal.Transfer, modal.ToFacility);
        } 

        /// <summary>
        /// 取得建立的路線編號
        /// </summary>
        [HttpPost]
        [ButtonAuthorize(Url = "~/TMS/NormalStandbyOrders", Auth = "NormalStandbyOrders")]
        public string SelectRouteNo([FromBody]IEnumerable<NormalStandbyOrders> value)
        {
            return NormalStandbyOrdersHelper.SelectRouteNo(value);
        } 

        /// <summary>
        /// 
        /// </summary>
        [HttpPost]
        [ButtonAuthorize(Url = "~/TMS/NormalStandbyOrders", Auth = "NormalStandbyOrders")]
        public int OrderImport()
        {
            return NormalStandbyOrdersHelper.OrderImport(User.Identity.Name, BestHelper.GetFacility(User));
        }

        /// <summary>
        /// 
        /// </summary>
        [HttpPost]
        [ButtonAuthorize(Url = "~/TMS/NormalStandbyOrders", Auth = "reserveorders")]
        public bool ReserveOrders([FromBody]IEnumerable<NormalStandbyOrders> value)
        {
            return NormalStandbyOrdersHelper.ReserveOrders(value,User.Identity.Name);
        }

        /// <summary>
        /// 
        /// </summary>
        [HttpPost]
        [ButtonAuthorize(Url = "~/TMS/NormalStandbyOrders", Auth = "returnorder")]
        public bool ReturnOrder([FromBody]IEnumerable<NormalStandbyOrders> value)
        {
            return NormalStandbyOrdersHelper.ReturnOrder(value,User.Identity.Name);
        }

        /// <summary>
        /// 
        /// </summary>
        [HttpPost]
        [ButtonAuthorize(Url = "~/TMS/NormalStandbyOrders", Auth = "updateskudata")]
        public bool UpdateSkuData([FromBody]IEnumerable<NormalStandbyOrders> value)
        {
            return NormalStandbyOrdersHelper.UpdateSkuData(value,User.Identity.Name);
        }

    }
}
