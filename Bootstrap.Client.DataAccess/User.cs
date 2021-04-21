using System.Collections.Generic;
using System;
using Bootstrap.Security;
using Bootstrap.Security.DataAccess;
using PetaPoco;

namespace Bootstrap.Client.DataAccess
{
    /// <summary>
    /// 用戶表實體類
    /// </summary>
    [TableName("Users")]
    public class User : BootstrapUser
    {
        /// <summary>
        /// 獲得/設置 用戶主鍵ID
        /// </summary>
        public string Id { get; set; }
        /// <summary>
        /// 獲取/設置 密碼
        /// </summary>
        public string Password { get; set; }

        /// <summary>
        /// 獲取/設置 密碼鹽
        /// </summary>
        public string PassSalt { get; set; }

        /// <summary>
        /// 獲取/設置 角色用戶關聯狀態 checked 標示已經關聯 '' 標示未關聯
        /// </summary>
        [ResultColumn]
        public string Checked { get; set; }

        /// <summary>
        /// 獲得/設置 用戶註冊時間
        /// </summary>
        public DateTime RegisterTime { get; set; }

        /// <summary>
        /// 獲得/設置 用戶被批覆時間
        /// </summary>
        public DateTime? ApprovedTime { get; set; }

        /// <summary>
        /// 獲得/設置 用戶批覆人
        /// </summary>
        public string ApprovedBy { get; set; }

        /// <summary>
        /// 獲得/設置 用戶的申請理由
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// 獲得/設置 用戶當前狀態 0 表示管理員註冊用戶 1 表示用戶註冊 2 表示更改密碼 3 表示更改個人皮膚 4 表示更改顯示名稱 5 批覆新用戶註冊操作
        /// </summary>
        [ResultColumn]
        public UserStates UserStatus { get; set; }

        /// <summary>
        /// 獲得/設置 通知描述 2分鐘內為剛剛
        /// </summary>
        [ResultColumn]
        public string Period { get; set; }

        /// <summary>
        /// 獲得/設置 新密碼
        /// </summary>
        [ResultColumn]
        public string NewPassword { get; set; }

        /// <summary>
        /// 獲得/設置 是否重置密碼
        /// </summary>
        [ResultColumn]
        public int IsReset { get; set; }

        /// <summary>
        /// 查詢所有用戶
        /// </summary>
        /// <returns></returns>
        public virtual IEnumerable<User> Retrieves() => DbManager.Create().Fetch<User>("select u.ID, u.UserName, u.DisplayName,   ru.IsReset from Users u left join (select 1 as IsReset, UserName from ResetUsers group by UserName) ru on u.UserName = ru.UserName Where ApprovedTime is not null");

        /// <summary>
        /// 
        /// </summary>
        /// <param name="userName"></param>
        /// <returns></returns>
        public virtual BootstrapUser? RetrieveUserByUserName(string userName) => DbHelper.RetrieveUserByUserName(userName);
    }

    /// <summary>
    /// 
    /// </summary>
    public enum UserStates
    {
        /// <summary>
        /// 
        /// </summary>
        ChangePassword,
        /// <summary>
        /// 
        /// </summary>
        ChangeTheme,
        /// <summary>
        /// 
        /// </summary>
        ChangeDisplayName,
        /// <summary>
        /// 
        /// </summary>
        ApproveUser,
        /// <summary>
        /// 
        /// </summary>
        RejectUser,
        /// <summary>
        /// 
        /// </summary>
        SaveApp
    }
}
