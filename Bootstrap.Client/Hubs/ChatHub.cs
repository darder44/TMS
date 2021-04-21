using Microsoft.AspNetCore.SignalR;
using System;
using System.Threading.Tasks;
using System.Collections.Generic;


namespace Bootstrap.Client
{
    /// <summary>
    /// 
    /// </summary>
    public class ChatHub : Hub
    {
        /// <summary>
        /// 
        /// </summary>
        private readonly IUserConnectionManager _userConnectionManager;
        /// <summary>
        /// 
        /// </summary>
        public ChatHub(IUserConnectionManager userConnectionManager)
        {
            _userConnectionManager = userConnectionManager;
        }
        /// <summary>
        /// 
        /// </summary>
        public string GetConnectionId()
        {
            var httpContext = this.Context.GetHttpContext();
            var userId = this.Context.User.Identity.Name;
            _userConnectionManager.KeepUserConnection(userId, Context.ConnectionId);
            return Context.ConnectionId;
        }
        /// <summary>
        /// 
        /// </summary>
        public async override Task OnDisconnectedAsync(Exception exception)
        {
            //get the connectionId
            var connectionId = Context.ConnectionId;
            _userConnectionManager.RemoveUserConnection(connectionId);
            var value = await Task.FromResult(0);
        }
    }
}