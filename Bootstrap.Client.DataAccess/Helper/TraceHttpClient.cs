using Longbow.Web;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Specialized;
using System.Net.Http;

namespace Bootstrap.Client.DataAccess
{
    /// <summary>
    /// Bootstrap TraceHttpClient 操作類
    /// </summary>
    public class TraceHttpClient
    {
        HttpClient _client;

        /// <summary>
        /// 構造函數
        /// </summary>
        /// <param name="client"></param>
        public TraceHttpClient(HttpClient client) => _client = client;

        /// <summary>
        /// 提交數據到後台訪問網頁接口
        /// </summary>
        /// <param name="context"></param>
        /// <param name="user"></param>
        public void Post(HttpContext context, OnlineUser user)
        {
            System.Threading.Tasks.Task.Run(() =>
            {
                // 調用 後台跟蹤接口
                // http://localhost:8080/api/Traces
                if (_client.BaseAddress == null) return;
                user.RequestUrl = context.Request.AbsoluteUrl();
                if (user.RequestUrl.Contains("ChatHub")) return;
                try
                {
                    //var t = _client.PostAsJsonAsync("", user, context.RequestAborted);
                    //t.Wait(2000);
                    _client.PostAsJsonAsync("", user);
                }
                catch (Exception ex)
                {
                    ex.Log(new NameValueCollection() { ["RequestUrl"] = _client.BaseAddress.AbsoluteUri });
                }
            });
        }
    }
}
