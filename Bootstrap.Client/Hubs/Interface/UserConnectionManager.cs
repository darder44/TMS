using System.Collections.Generic;
using System.Linq;

namespace Bootstrap.Client
{
    /// <summary>
    /// 
    /// </summary>
    public class UserConnectionManager : IUserConnectionManager
    {
        /// <summary>
        /// 
        /// </summary>
        private static Dictionary<string, List<string>> userConnectionMap = new Dictionary<string, List<string>>();
        /// <summary>
        /// 
        /// </summary>
        private static string userConnectionMapLocker = string.Empty;
        /// <summary>
        /// 
        /// </summary>
        public void KeepUserConnection(string userId, string connectionId)
        {   
            if(string.IsNullOrEmpty(userId) || string.IsNullOrEmpty(connectionId)) return;
            lock (userConnectionMapLocker)
            {
                if (!userConnectionMap.ContainsKey(userId))
                {
                    userConnectionMap[userId] = new List<string>();
                }
                userConnectionMap[userId].Add(connectionId);
            }
        }
        /// <summary>
        /// 
        /// </summary>
        public void RemoveUserConnection(string connectionId)
        {
            //Remove the connectionId of user 
            lock (userConnectionMapLocker)
            {
                foreach (var userId in userConnectionMap.Keys)
                {
                    if (userConnectionMap.ContainsKey(userId))
                    {
                        if (userConnectionMap[userId].Contains(connectionId))
                        {
                            userConnectionMap[userId].Remove(connectionId);
                            break;
                        }
                    }
                }
            }
        }
        /// <summary>
        /// 
        /// </summary>
        public List<string> GetUserConnections(string userId)
        {
            var conn = new List<string>();
            if (!string.IsNullOrEmpty(userId))
            {
                lock (userConnectionMapLocker)
                {
                    conn = userConnectionMap[userId];
                }
            }
            return conn;
        }
        /// <summary>
        /// 
        /// </summary>
        public IEnumerable<string> GetConnections()
        {
            IEnumerable<string> conn = new List<string>();
            lock (userConnectionMapLocker)
            {
                conn = userConnectionMap.Keys.ToList();
            }
            return conn;
        }
    }
    /// <summary>
    /// 
    /// </summary>
    public class NotiUser
    {
        /// <summary>
        /// 
        /// </summary>
        public string UserName { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string Message { get; set; }
    }
}