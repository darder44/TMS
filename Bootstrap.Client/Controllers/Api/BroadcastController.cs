using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.SignalR;
using Longbow.Web.SignalR;
using Bootstrap.Client.DataAccess;

namespace Bootstrap.Client.Controllers.Api
{
    /// <summary>
    /// 廣播
    /// </summary>
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class BroadcastController : ControllerBase
    {
        /// <summary>
        /// 
        /// </summary>
        private readonly IUserConnectionManager _userConnectionManager;
        /// <summary>
        /// 
        /// </summary>
        private readonly IHubContext<ChatHub> _ChatHubContext;
        /// <summary>
        /// 
        /// </summary>
        private readonly IHubContext<SignalRHub> _SignalRHubContext;
        /// <summary>
        /// 
        /// </summary>
        public BroadcastController(IUserConnectionManager userConnectionManager, IHubContext<ChatHub> chatHubContext, IHubContext<SignalRHub> signalRHubContext)
        {
            _userConnectionManager = userConnectionManager;
            _ChatHubContext = chatHubContext;
            _SignalRHubContext = signalRHubContext;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IEnumerable<string> GetConnections()
        {
            IEnumerable<string> conn = new List<string>();
            if (_userConnectionManager != null)
            {
                conn = _userConnectionManager.GetConnections();
            }
            return conn;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public bool Broadcast([FromBody] MessageBody Message)
        {
            _SignalRHubContext.SendMessageBody(Message);
            return true;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public bool Send([FromBody] NotiUser User)
        {
            var users = _userConnectionManager.GetUserConnections(User.UserName);
            if (users != null && users.Count > 0)
            {
                _ChatHubContext.SendMessageBody(users, new MessageBody() { Category = "Chat", Message = User.Message });
            }
            return true;
        }
    }
}
