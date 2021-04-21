using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Bootstrap.Client.Controllers.Api
{
    /// <summary>
    /// 運維管理接口
    /// </summary>
    [Route("api/[controller]")]
    [Authorize]
    [ApiController]
    public class AdminController : ControllerBase
    {
        /// <summary>
        /// 更改系統運行模式 1 為演示模式 0 為正常模式
        /// </summary>
        [HttpGet]
        public bool Get([FromQuery]string authCode, string salt, int model)
        {
            var ret = false;
            // 檢查授權
            if (Longbow.Security.Cryptography.LgbCryptography.ComputeHash(authCode, salt) == "3lpCnRu7qqiAbIrx7gNRUB0mPXgJC3wGoyPJED3KeoA=")
            {
                using (var db = Longbow.Data.DbManager.Create("ba"))
                {
                    db.Execute("Update Dicts Set Code = @0 Where Category = @1 and Name = @2", model, "網站設置", "演示系統");
                }
                ret = true;
            }
            return ret;
        }
    }
}
