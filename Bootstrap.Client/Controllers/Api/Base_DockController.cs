using Bootstrap.Client.Query;
using Bootstrap.Client.DataAccess;
using Longbow.Web.Mvc;
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
    /// 碼頭主檔Controller
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class Base_DockController : ControllerBase
    {
        ///<summary>
        ///
        ///</summary>
        [HttpGet]
        public QueryData<object> Get([FromQuery]QueryBase_DockOption value)
        {
            return value.RetrieveData();
        }

        /// <summary>
        /// 新建/更新
        /// </summary>
        /// <param name="value"></param>
        [HttpPost]
        [ButtonAuthorize(Url = "~/WMS/Base_Dock", Auth = "add,edit")]
        public bool Save([FromBody]Base_Dock value)
        {
            value.EditDate = DateTime.Now;
            value.EditWho = User.Identity.Name;
            if (string.IsNullOrEmpty(value.Id)) 
            {
                value.AddDate = value.EditDate;
                value.AddWho = value.EditWho;
                return Base_DockHelper.Save(value);
            } 
            else
            {
                return Base_DockHelper.Update(value);
            }
        }

        /// <summary>
        /// 刪除
        /// </summary>
        /// <param name="values"></param>
        [HttpDelete]
        [ButtonAuthorize(Url = "~/WMS/Base_Dock", Auth = "del")]
        public bool Delete([FromBody]IEnumerable<string> values)
        {
            return Base_DockHelper.Delete(values);
        }
    }
}
