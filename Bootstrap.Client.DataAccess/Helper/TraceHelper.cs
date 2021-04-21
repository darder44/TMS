using Longbow.Web;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;

namespace Bootstrap.Client.DataAccess
{
    /// <summary>
    /// 訪問網頁跟蹤幫助類
    /// </summary>
    public static class TraceHelper
    {
        /// <summary>
        /// 保存訪問歷史記錄
        /// </summary>
        /// <param name="context"></param>
        /// <param name="user"></param>
        public static void Save(HttpContext context, OnlineUser user)
        {
            if (context.User.Identity.IsAuthenticated)
            {
                var client = context.RequestServices.GetRequiredService<TraceHttpClient>();
                client.Post(context, user);
            }
        }
    }
}
