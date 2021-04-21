using Bootstrap.Security;
using Bootstrap.Security.DataAccess;
using PetaPoco;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Bootstrap.Client.DataAccess
{
    /// <summary>
    /// 
    /// </summary>
    [TableName("Groups")]
    public class Group : BootstrapGroup
    {
        /// <summary>
        /// 獲得/設置 群組描述
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// 獲取/設置 用戶群組關聯狀態 checked 標示已經關聯 '' 標示未關聯
        /// </summary>
        [ResultColumn]
        public string Checked { get; set; }

        /// <summary>
        /// 查詢所有群組信息
        /// </summary>
        /// <returns></returns>
        public virtual IEnumerable<Group> Retrieves() => DbManager.Create().Fetch<Group>();

        /// <summary>
        /// 刪除群組信息
        /// </summary>
        /// <param name="value"></param>
        public virtual bool Delete(IEnumerable<string> value)
        {
            bool ret = false;
            var ids = string.Join(",", value);
            var db = DbManager.Create();
            try
            {
                db.BeginTransaction();
                db.Execute($"delete from UserGroup where GroupID in ({ids})");
                db.Execute($"delete from RoleGroup where GroupID in ({ids})");
                db.Delete<Group>($"where ID in ({ids})");
                db.CompleteTransaction();
                ret = true;
            }
            catch (Exception e)
            {
                db.AbortTransaction();
                throw e;
            }
            return ret;
        }

        /// <summary>
        /// 保存新建/更新的群組信息
        /// </summary>
        /// <param name="p"></param>
        /// <returns></returns>
        public virtual bool Save(Group p)
        {
            DbManager.Create().Save(p);
            return !p.Id.IsNullOrEmpty();
        }

        /// <summary>
        /// 根據用戶查詢部門信息
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public virtual IEnumerable<Group> RetrievesByUserId(string userId)
        {
            var db = DbManager.Create();
            return db.Fetch<Group>($"select g.ID, g.GroupCode, g.GroupName, g.Description, case ug.GroupID when g.ID then 'checked' else '' end Checked from {db.Provider.EscapeSqlIdentifier("Groups")} g left join UserGroup ug on g.ID = ug.GroupID and UserID = @0", userId);
        }

        /// <summary>
        /// 根據角色ID指派部門
        /// </summary>
        /// <param name="roleId"></param>
        /// <returns></returns>
        public virtual IEnumerable<Group> RetrievesByRoleId(string roleId)
        {
            var db = DbManager.Create();
            return db.Fetch<Group>($"select g.ID, g.GroupCode, g.GroupName, g.Description, case rg.GroupID when g.ID then 'checked' else '' end Checked from {db.Provider.EscapeSqlIdentifier("Groups")} g left join RoleGroup rg on g.ID = rg.GroupID and RoleID = @0", roleId);
        }

        /// <summary>
        /// 保存用戶部門關係
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="groupIds"></param>
        /// <returns></returns>
        public virtual bool SaveByUserId(string userId, IEnumerable<string> groupIds)
        {
            var ret = false;
            var db = DbManager.Create();
            try
            {
                db.BeginTransaction();
                //刪除用戶部門表中該用戶所有的部門關係
                db.Execute("delete from UserGroup where UserID = @0", userId);
                db.InsertBatch("UserGroup", groupIds.Select(g => new { UserID = userId, GroupID = g }));
                db.CompleteTransaction();
                ret = true;
            }
            catch (Exception ex)
            {
                db.AbortTransaction();
                throw ex;
            }
            return ret;
        }

        /// <summary>
        /// 根據角色ID以及選定的部門ID，保到角色部門表
        /// </summary>
        /// <param name="roleId"></param>
        /// <param name="groupIds"></param>
        /// <returns></returns>
        public virtual bool SaveByRoleId(string roleId, IEnumerable<string> groupIds)
        {
            bool ret = false;
            var db = DbManager.Create();
            try
            {
                db.BeginTransaction();
                //刪除角色部門表該角色所有的部門
                db.Execute("delete from RoleGroup where RoleID = @0", roleId);
                db.InsertBatch("RoleGroup", groupIds.Select(g => new { RoleID = roleId, GroupID = g }));
                db.CompleteTransaction();
                ret = true;
            }
            catch (Exception ex)
            {
                db.AbortTransaction();
                throw ex;
            }
            return ret;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="userName"></param>
        /// <returns></returns>
        public virtual IEnumerable<BootstrapGroup> RetrievesByUserName(string userName) => DbHelper.RetrieveGroupsByUserName(userName);
    }
}
