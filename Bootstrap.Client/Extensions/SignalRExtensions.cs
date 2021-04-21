using Bootstrap.Client.DataAccess;
using Longbow.Web.SignalR;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Data.Common;
using System.Threading;
using Task = System.Threading.Tasks.Task;

namespace Bootstrap.Client
{
    /// <summary>
    /// SignalR 操作擴展類
    /// </summary>
    public static class SignalRExtensions
    {
        /// <summary>
        /// 推送異常信息到客戶端方法擴展
        /// </summary>
        /// <param name="context"></param>
        /// <param name="ex"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        public static async Task SendMessageBody(this IHubContext<SignalRHub> context, Exception ex, CancellationToken token = default)
        {
            var category = "App";
            if (ex.GetType().IsSubclassOf(typeof(DbException))) category = "DB";
            await context.SendMessageBody(new MessageBody() { Category = category, Message = ex.Message }, token);
        }

        /// <summary>
        /// 推送 MessageBody 到客戶端方法擴展
        /// </summary>
        /// <param name="context"></param>
        /// <param name="messageBody"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        public static Task SendMessageBody(this IHubContext<SignalRHub> context, MessageBody messageBody, CancellationToken token = default) => context.Clients.All.SendAsync("rev", messageBody, token);
    }
}
