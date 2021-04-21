using Bootstrap.Client.DataAccess;
using Bootstrap.Security.Mvc;

namespace Bootstrap.Client.Models
{
    /// <summary>
    /// ModelBase 基礎類
    /// </summary>
    public class ModelBase
    {
        /// <summary>
        /// 默認構造函數
        /// </summary>
        public ModelBase()
        {
            AppId = BootstrapAppContext.AppId;
            Title = DictHelper.RetrieveWebTitle(AppId);
            Footer = DictHelper.RetrieveWebFooter(AppId);
            Theme = DictHelper.RetrieveActiveTheme();
            IsDemo = DictHelper.RetrieveSystemModel();
        }

        /// <summary>
        /// 是否為演示系統
        /// </summary>
        public bool IsDemo { get; protected set; }

        /// <summary>
        /// 獲得 應用程序標識
        /// </summary>
        public string AppId { get; private set; }

        /// <summary>
        /// 獲取 網站標題
        /// </summary>
        public string Title { get; private set; }

        /// <summary>
        /// 獲取 網站頁腳
        /// </summary>
        public string Footer { get; private set; }

        /// <summary>
        /// 網站樣式全局設置
        /// </summary>
        public string Theme { get; protected set; }

        /// <summary>
        /// 是否顯示卡片標題
        /// </summary>
        public string ShowCardTitle { get; protected set; } = "";

        /// <summary>
        /// 是否收縮側邊欄
        /// </summary>
        public string ShowSideBar { get; protected set; } = "";
    }
}
