using System.Linq;

namespace Bootstrap.Client.DataAccess
{
    /// <summary>
    /// 前台應用查詢類
    /// </summary>
    public class QueryAppOption
    {
        /// <summary>
        /// 應用操作 new 為新建 edit 為保存
        /// </summary>
        public string AppId { get; set; } = "edit";

        /// <summary>
        /// 應用名稱
        /// </summary>
        public string AppName { get; set; } = "";

        /// <summary>
        /// 應用編碼
        /// </summary>
        public string AppCode { get; set; } = "";

        /// <summary>
        /// 前台應用路徑
        /// </summary>
        public string AppUrl { get; set; } = "#";

        /// <summary>
        /// 前台應用標題
        /// </summary>
        public string AppTitle { get; set; } = "未設置";

        /// <summary>
        /// 前台應用頁腳
        /// </summary>
        public string AppFooter { get; set; } = "未設置";

        /// <summary>
        /// 前台應用圖標
        /// </summary>
        public string AppIcon { get; set; } = "/favicon.ico";

        /// <summary>
        /// 前台應用收藏圖標
        /// </summary>
        public string AppFavicon { get; set; } = "/favicon.png";

        /// <summary>
        /// 保存前台應用方法
        /// </summary>
        /// <returns></returns>
        public bool Save()
        {
            var ret = DictHelper.SaveAppSettings(this);
            return true;
        }

        /// <summary>
        /// 通過指定 AppKey 獲取前台應用配置信息
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public static QueryAppOption RetrieveByKey(string key)
        {
            var ret = new QueryAppOption() { AppCode = key };
            var dicts = DictHelper.RetrieveDicts();
            ret.AppName = dicts.FirstOrDefault(d => d.Category == "应用程序" && d.Code == key).Name ?? "";
            ret.AppUrl = dicts.FirstOrDefault(d => d.Category == "应用首页" && d.Name == key).Code ?? "";
            ret.AppTitle = dicts.FirstOrDefault(d => d.Category == ret.AppName && d.Name == "网站标题").Code ?? "";
            ret.AppFooter = dicts.FirstOrDefault(d => d.Category == ret.AppName && d.Name == "网站页脚").Code ?? "";
            ret.AppFavicon = dicts.FirstOrDefault(d => d.Category == ret.AppName && d.Name == "网站图标").Code ?? "";
            ret.AppIcon = dicts.FirstOrDefault(d => d.Category == ret.AppName && d.Name == "favicon").Code ?? "";
            return ret;
        }
    }
}
