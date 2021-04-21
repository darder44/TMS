using Bootstrap.Client.Query;
using Bootstrap.Client.DataAccess;
using Longbow.Web;
using Longbow.Web.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace Bootstrap.Client.Controllers.Api
{
    /// <summary>
    /// 操作日誌控制器
    /// </summary>
    [Route("api/[controller]")]
    [Authorize]
    [ApiController]
    public class LogsController : ControllerBase
    {
        /// <summary>
        /// 操作日誌記錄方法
        /// </summary>
        /// <param name="ipLocator"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        [HttpPost]
        public bool Post([FromServices]IIPLocatorProvider ipLocator, [FromBody]Log value)
        {
            value.UserAgent = Request.Headers["User-Agent"];
            var agent = new UserAgent(value.UserAgent);
            value.Ip = HttpContext.Connection.RemoteIpAddress.ToIPv4String();
            value.Browser = $"{agent.Browser?.Name} {agent.Browser?.Version}";
            value.OS = $"{agent.OS?.Name} {agent.OS?.Version}";
            value.City = ipLocator.Locate(value.Ip);
            value.UserName = User.Identity.Name ?? string.Empty;
            return LogHelper.Save(value);
        }
    }
}
