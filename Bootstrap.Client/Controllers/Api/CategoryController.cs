using Bootstrap.Client.DataAccess;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using Bootstrap.Client.DataAccess.Helper;

namespace Bootstrap.Client.Controllers.Api
{
    /// <summary>
    /// 數據字典分類
    /// </summary>
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        /// <summary>
        /// 獲取字典表中所有Category數據
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [AllowAnonymous]
        public IEnumerable<string> RetrieveDictCategorys()
        {
            return DictHelper.RetrieveCategories();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IEnumerable<string> RetrieveMenus()
        {
            return MenuHelper.RetrieveAllMenus(User.Identity.Name).OrderBy(m => m.Name).Select(m => m.Name);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IEnumerable<string> RetrieveParentMenus()
        {
            return MenuHelper.RetrieveMenus(User.Identity.Name).Where(m => m.Menus.Count() > 0).OrderBy(m => m.Name).Select(m => m.Name);
        }

        /// <summary>
        /// 根據Category取得Code
        /// </summary>
        [HttpGet]
        public IEnumerable<string> RetrieveCodes([FromQuery] string Category)
        {
            return DictHelper.RetrieveDicts().Where( d => d.Category == Category).OrderBy( d => d.Code).Select( d => d.Code);
        }
    }
}
