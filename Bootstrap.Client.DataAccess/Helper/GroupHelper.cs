using Bootstrap.Security;
using Bootstrap.Security.DataAccess;
using Longbow.Cache;
using Longbow.Data;
using System.Collections.Generic;

namespace Bootstrap.Client.DataAccess
{
    /// <summary>
    /// 
    /// </summary>
    public static class GroupHelper
    {
        /// <summary>
        /// 
        /// </summary>
        public const string RetrieveGroupsDataKey = "GroupHelper-RetrieveGroups";
        /// <summary>
        /// 
        /// </summary>
        public const string RetrieveGroupsByUserIdDataKey = "GroupHelper-RetrieveGroupsByUserId";
        /// <summary>
        /// 
        /// </summary>
        public const string RetrieveGroupsByRoleIdDataKey = "GroupHelper-RetrieveGroupsByRoleId";
        /// <summary>
        /// 
        /// </summary>
        public const string RetrieveGroupsByUserNameDataKey = DbHelper.RetrieveGroupsByUserNameDataKey;

        /// <summary>
        /// 查詢所有群組信息
        /// </summary>
        /// <returns></returns>
        public static IEnumerable<Group> Retrieves() => CacheManager.GetOrAdd(RetrieveGroupsDataKey, key => DbContextManager.Create<Group>().Retrieves());

        /// <summary>
        /// 刪除群組信息
        /// </summary>
        /// <param name="values"></param>
        public static bool Delete(IEnumerable<string> values)
        {
            var ret = DbContextManager.Create<Group>().Delete(values);
            if (ret) CacheCleanUtility.ClearCache(groupIds: values);
            return ret;
        }

        /// <summary>
        /// 保存新建/更新的群組信息
        /// </summary>
        /// <param name="p"></param>
        /// <returns></returns>
        public static bool Save(Group p)
        {
            var ret = DbContextManager.Create<Group>().Save(p);
            if (ret) CacheCleanUtility.ClearCache(groupIds: string.IsNullOrEmpty(p.Id) ? new List<string>() : new List<string>() { p.Id });
            return ret;
        }
        /// <summary>
        /// 根據用戶查詢部門信息
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public static IEnumerable<Group> RetrievesByUserId(string userId) => CacheManager.GetOrAdd(string.Format("{0}-{1}", RetrieveGroupsByUserIdDataKey, userId), k => DbContextManager.Create<Group>().RetrievesByUserId(userId), RetrieveGroupsByUserIdDataKey);

        /// <summary>
        /// 保存用戶部門關係
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="groupIds"></param>
        /// <returns></returns>
        public static bool SaveByUserId(string userId, IEnumerable<string> groupIds)
        {
            var ret = DbContextManager.Create<Group>().SaveByUserId(userId, groupIds);
            if (ret) CacheCleanUtility.ClearCache(groupIds: groupIds, userIds: new List<string>() { userId });
            return ret;
        }

        /// <summary>
        /// 根據角色ID指派部門
        /// </summary>
        /// <param name="roleId"></param>
        /// <returns></returns>
        public static IEnumerable<Group> RetrievesByRoleId(string roleId) => CacheManager.GetOrAdd(string.Format("{0}-{1}", RetrieveGroupsByRoleIdDataKey, roleId), key => DbContextManager.Create<Group>().RetrievesByRoleId(roleId), RetrieveGroupsByRoleIdDataKey);

        /// <summary>
        /// 根據角色ID以及選定的部門ID，保到角色部門表
        /// </summary>
        /// <param name="roleId"></param>
        /// <param name="groupIds"></param>
        /// <returns></returns>
        public static bool SaveByRoleId(string roleId, IEnumerable<string> groupIds)
        {
            var ret = DbContextManager.Create<Group>().SaveByRoleId(roleId, groupIds);
            if (ret) CacheCleanUtility.ClearCache(groupIds: groupIds, roleIds: new List<string>() { roleId });
            return ret;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="userName"></param>
        /// <returns></returns>
        public static IEnumerable<BootstrapGroup> RetrievesByUserName(string userName) => CacheManager.GetOrAdd(string.Format("{0}-{1}", RetrieveGroupsByUserNameDataKey, userName), r => DbContextManager.Create<Group>().RetrievesByUserName(userName), RetrieveGroupsByUserNameDataKey);
    }
}
