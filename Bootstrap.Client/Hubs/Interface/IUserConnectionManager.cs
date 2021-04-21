using System.Collections.Generic;

namespace Bootstrap.Client
{
    /// <summary>
    /// 
    /// </summary>
    public interface IUserConnectionManager
    {
        /// <summary>
        /// 
        /// </summary>
        void KeepUserConnection(string userId, string connectionId);
        /// <summary>
        /// 
        /// </summary>
        void RemoveUserConnection(string connectionId);
        /// <summary>
        /// 
        /// </summary>
        List<string> GetUserConnections(string userId);
        /// <summary>
        /// 
        /// </summary>
        IEnumerable<string> GetConnections();
    }
}