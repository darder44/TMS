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
    /// NormalSignOrders
    /// </summary>
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class NormalSignOrdersController : ControllerBase
    {
        /// <summary>
        /// 取得簽單維護資料
        /// </summary>
        [HttpGet]
        public QueryData<object> RetrieveOrders([FromQuery]QueryNormalSignOrdersOption value)
        {
            return value.RetrieveOrders(BestHelper.GetFacility(User)); 
        }

        /// <summary>
        /// 取得簽單維護明細
        /// </summary>
        [HttpGet]
        public IEnumerable<NormalSignOrders> RetrieveDetailByTMSKey([FromQuery]string TMSKey)
        {
            return NormalSignOrdersHelper.RetrieveDetailByTMSKey(TMSKey.Trim(), BestHelper.GetFacility(User));
        }

        /// <summary>
        /// 取得簽單明細
        /// </summary>
        [HttpGet]
        public IEnumerable<NormalSignOrders> RetrieveOrderTrackByTMSKey([FromQuery]string TMSKey)
        {
            return NormalSignOrdersHelper.RetrieveOrderTrackByTMSKey(TMSKey.Trim(), BestHelper.GetFacility(User));
        }

        ///<summary>
        ///
        ///</summary>
        [HttpGet]
        public NormalSignOrders RetrieveCostCode([FromQuery]string StorerKey, string CostCode)
        {
            return NormalSignOrdersHelper.RetrieveCostCode(StorerKey, CostCode);
        }

        /// <summary>
        /// 取得運費維護資料
        /// </summary>
        [HttpPost]
        public NormalShippingCostResult RetrievesShippingCost([FromBody]NormalSignOrders value)
        {       
            var ShippingCost = NormalSignOrdersHelper.RetrievesShippingCost(value.TMSKey, BestHelper.GetFacility(User),User.Identity.Name);
            //if (ShippingCost.Count() == 0) return null;
            return new NormalShippingCostResult()
            {
                ShippingCost = ShippingCost,
                RouteData = NormalSignOrdersHelper.RetrievesRouteData(value.TMSKey, BestHelper.GetFacility(User)),
                SignOrderData = NormalSignOrdersHelper.RetrieveSignOrderDataByTMSKey(value.TMSKey)
            };
        }

        // /// <summary>
        // /// 取得簽單運費維護資料明細
        // /// </summary>
        // [HttpPost]
        // public NormalShippingCostResultDetail RetrievesShippingCostDetail([FromBody]NormalSignOrders value)
        // {                        
        //     return new NormalShippingCostResultDetail()
        //     {
        //         OrderData = NormalSignOrdersHelper.RetrievesOrderData(value.TMSKey, BestHelper.GetFacility(User)),
        //         DeliveryDatexAddressData = NormalSignOrdersHelper.RetrievesDeliveryDatexAddressData(value.ShipToAddress, value.DeliveryDate.ToString("yyyy/MM/dd"), BestHelper.GetFacility(User)),
        //         RouteNoxStorerKeyData = NormalSignOrdersHelper.RetrievesRouteNoxStorerKeyData(value.RouteNo, value.StorerKey, BestHelper.GetFacility(User))
        //     };
        // }



        /// <summary>
        /// 
        /// </summary>
        /// <param name="value"></param>
        [HttpPost]
        public bool SignOrders([FromBody]NormalSignOrdersBody value)
        {
            return NormalSignOrdersHelper.SignOrders(value, User.Identity.Name,RoleHelper.RetrievesByUserName( User.Identity.Name));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="value"></param>
        [HttpPost]
        public string CheckSignOrders([FromBody]NormalSignOrdersCheckBody value)
        {
            return NormalSignOrdersHelper.CheckSignOrders(value, User.Identity.Name);
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="value"></param>
        [HttpPost]
        public bool NormalOrders([FromBody]IEnumerable<NormalSignOrders> value)
        {
            return NormalSignOrdersHelper.NormalOrders(value, User.Identity.Name, RoleHelper.RetrievesByUserName(User.Identity.Name));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="value"></param>
        [HttpPost]
        public bool CheckOrderStatus([FromBody]IEnumerable<NormalSignOrders> value)
        {
            return NormalSignOrdersHelper.CheckOrderStatus(value, User.Identity.Name, RoleHelper.RetrievesByUserName(User.Identity.Name));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="value"></param>
        [HttpPost]
        public bool UnShippedOrders([FromBody]NormalUnShippedOrdersBody value)
        {
            return NormalSignOrdersHelper.UnShippedOrders(value, User.Identity.Name, RoleHelper.RetrievesByUserName(User.Identity.Name));
        }

        /// <summary>
        /// 出車異常重組
        /// </summary>
        /// <param name="value"></param>
        [HttpPost]
        public string BreakRoute([FromBody]NormalDeliveryAbnormalModal value)
        {
            return NormalSignOrdersHelper.BreakRoute(value, User.Identity.Name, RoleHelper.RetrievesByUserName(User.Identity.Name));
        } 
        /// <summary>
        /// 清空簽單資料
        /// </summary>
        /// <param name="value"></param>
        [HttpPost]
        public string ClearSignOrders([FromBody]IEnumerable<NormalSignOrders> value)
        {
            return NormalSignOrdersHelper.ClearSignOrders(value, User.Identity.Name,RoleHelper.RetrievesByUserName(User.Identity.Name));
        }

        
        /// <summary>
        /// 清空簽單資料
        /// </summary>
        /// <param name="value"></param>
        [HttpPost]
        public string Calculate([FromBody]IEnumerable<NormalSignOrders> value)
        {
            return NormalSignOrdersHelper.Calculate(value, User.Identity.Name,RoleHelper.RetrievesByUserName(User.Identity.Name));
        }

        /// <summary>
        /// 檢查簽單是否已回傳
        /// </summary>
        /// <param name="value"></param>
        [HttpPost]
        public string CheckSdnBack([FromBody]CheckSdnBackBody value)
        {
            return NormalSignOrdersHelper.CheckSdnBack(value.TMSKey, RoleHelper.RetrievesByUserName(User.Identity.Name),UserHelper.RetrieveUserByUserName(value.SdnSendWho));
        }

        /// <summary>
        /// 保存計費資料
        /// </summary>
        /// <param name="value"></param>
        [HttpPost]
        public bool SaveCostData([FromBody]NormalCostDataBody value)
        {
            return NormalSignOrdersHelper.SaveCostData(value, User.Identity.Name);
        }

        /// <summary>
        /// 
        /// </summary>
        [HttpGet]
        public NormalSignOrders RetriveVehicleKeyByRouteNo([FromQuery]string RouteNo)
        {
            return NormalSignOrdersHelper.RetriveVehicleKeyByRouteNo(RouteNo);
        }

        /// <summary>
        /// 
        /// </summary>
        [HttpGet]
        public IEnumerable<NormalSignOrders> RetriveVehicleKeyByTMSKey([FromQuery]string TMSKey)
        {
            return NormalSignOrdersHelper.RetriveVehicleKeyByTMSKey(TMSKey);
        }

        /// <summary>
        /// 
        /// </summary>
        [HttpGet]
        public bool CheckCostCode([FromQuery]string CostCode, string StorerKey)
        {
            return NormalSignOrdersHelper.CheckCostCode(CostCode,StorerKey);
        }

        ///<summary>
        /// 
        ///</summary>
        public IEnumerable<NormalSignOrders> RetrieveDriver([FromQuery]string VehicleKey)
        {
            return NormalSignOrdersHelper.RetrieveDriver(VehicleKey);
        }

        ///<summary>
        /// 
        ///</summary>
        public IEnumerable<NormalSignOrders> RetrieveReceiver([FromQuery]string VehicleKey)
        {
            return NormalSignOrdersHelper.RetrieveReceiver(VehicleKey);
        }

        /// <summary>
        /// 
        /// </summary>
        [HttpGet]
        public string CheckVehicleKey([FromQuery]string VehicleKey, string Driver, string Receiver)
        {
            return NormalSignOrdersHelper.CheckVehicleKey(VehicleKey,Driver,Receiver);
        }
    }
}
