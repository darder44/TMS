using Bootstrap.Client.DataAccess;
using Longbow.Web.SignalR;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Threading;
using Task = System.Threading.Tasks.Task;

namespace Bootstrap.Client
{
    /// <summary>
    /// 
    /// </summary>
    public static class ChatHubExtension
    {
        /// <summary>
        /// 推送 MessageBody 到客戶端方法擴展
        /// </summary>
        /// <param name="context"></param>
        /// <param name="user"></param>
        /// <param name="messageBody"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        public static Task SendMessageBody(this IHubContext<ChatHub> context, List<string> user, MessageBody messageBody, CancellationToken token = default) => context.Clients.Clients(user).SendAsync("chat", messageBody, token);
    }

}