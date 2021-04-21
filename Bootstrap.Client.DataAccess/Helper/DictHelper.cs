using Bootstrap.Security;
using Bootstrap.Security.DataAccess;
using Longbow.Cache;
using Longbow.Security.Cryptography;
using Longbow.Web;
using Longbow.Data;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Bootstrap.Client.DataAccess
{
    /// <summary>
    /// 字典配置數據操作幫助類
    /// </summary>
    public static class DictHelper
    {
        /// <summary>
        /// 緩存索引，BootstrapAdmin後台清理緩存時使用
        /// </summary>
        public const string RetrieveDictsDataKey = DbHelper.RetrieveDictsDataKey;

        /// <summary>
        /// 緩存索引，字典分類數據緩存鍵值 值為 DictHelper-RetrieveDictsCategory
        /// </summary>
        public const string RetrieveCategoryDataKey = "DictHelper-RetrieveDictsCategory";

        /// <summary>
        /// 獲得所有字典項配置數據集合方法 內部使用了緩存，緩存值 BootstrapMenu-RetrieveMenus
        /// </summary>
        /// <returns></returns>
        public static IEnumerable<BootstrapDict> RetrieveDicts() => CacheManager.GetOrAdd(RetrieveDictsDataKey, key => DbContextManager.Create<Dict>()?.RetrieveDicts()) ?? new BootstrapDict[0];

        private static IEnumerable<BootstrapDict> RetrieveProtectedDicts() => RetrieveDicts().Where(d => d.Define == 0 || d.Category == "测试平台");

        /// <summary>
        /// 獲取網站 favicon 圖標
        /// </summary>
        /// <param name="appId"></param>
        /// <returns></returns>
        public static string RetrieveWebIcon(string appId)
        {
            // 獲取應用程序 logo
            var ret = "~/favicon.ico";
            var ditcs = RetrieveDicts();
            var platName = ditcs.FirstOrDefault(d => d.Category == "应用程序" && d.Code == appId)?.Name;
            if (!string.IsNullOrEmpty(platName))
            {
                var pathBase = ditcs.FirstOrDefault(d => d.Category == "应用首页" && d.Name == appId)?.Code;
                if (!string.IsNullOrEmpty(pathBase))
                {
                    var favIcon = ditcs.FirstOrDefault(d => d.Category == platName && d.Name == "favicon")?.Code;
                    if (!string.IsNullOrEmpty(favIcon)) ret = $"{pathBase}{favIcon}";
                }
            }
            return ret;
        }

        /// <summary>
        /// 獲取網站 logo 小圖標
        /// </summary>
        /// <param name="appId"></param>
        /// <returns></returns>
        public static string RetrieveWebLogo(string appId)
        {
            // 獲取應用程序 logo
            var ret = "~/favicon.png";
            var ditcs = RetrieveDicts();
            var platName = ditcs.FirstOrDefault(d => d.Category == "应用程序" && d.Code == appId)?.Name;
            if (!string.IsNullOrEmpty(platName))
            {
                var pathBase = ditcs.FirstOrDefault(d => d.Category == "应用首页" && d.Name == appId)?.Code;
                if (!string.IsNullOrEmpty(pathBase))
                {
                    var favIcon = ditcs.FirstOrDefault(d => d.Category == platName && d.Name == "网站图标")?.Code;
                    if (!string.IsNullOrEmpty(favIcon)) ret = $"{pathBase}{favIcon}";
                }
            }
            return ret;
        }

        /// <summary>
        /// 刪除字典中的數據
        /// </summary>
        /// <param name="value">需要刪除的IDs</param>
        /// <returns></returns>
        public static bool Delete(IEnumerable<string> value)
        {
            if (!value.Any()) return true;

            // 禁止刪除系統數據與測試平台數據
            if (RetrieveSystemModel() && RetrieveProtectedDicts().Any(d => value.Any(v => v == d.Id))) return true;
            var ret = DbContextManager.Create<Dict>()?.Delete(value) ?? false;
            if (ret) CacheCleanUtility.ClearCache(dictIds: value);
            return ret;
        }

        /// <summary>
        /// 保存新建/更新的字典信息
        /// </summary>
        /// <param name="p"></param>
        /// <returns></returns>
        public static bool Save(BootstrapDict p)
        {
            if (RetrieveSystemModel() && !string.IsNullOrEmpty(p.Id) && RetrieveProtectedDicts().Any(m => m.Id == p.Id)) return true;

            var ret = DbContextManager.Create<Dict>()?.Save(p) ?? false;
            if (ret) CacheCleanUtility.ClearCache(dictIds: new List<string>());
            return ret;
        }

        /// <summary>
        /// 配置 IP 地理位置查詢配置項 注入方法調用此方法
        /// </summary>
        /// <param name="op"></param>
        public static void ConfigIPLocator(IPLocatorOption op)
        {
            var name = RetrieveLocaleIPSvr();
            if (!string.IsNullOrEmpty(name) && !name.Equals("None", StringComparison.OrdinalIgnoreCase))
            {
                var url = RetrieveLocaleIPSvrUrl(name);
                op.LocatorName = name;
                op.Url = string.IsNullOrEmpty(url) ? string.Empty : $"{url}{op.IP}";
                op.Period = RetrieveLocaleIPSvrCachePeriod() * 60 * 1000;
            }
        }

        /// <summary>
        /// 保存網站個性化設置
        /// </summary>
        /// <param name="dicts"></param>
        /// <returns></returns>
        public static bool SaveSettings(IEnumerable<BootstrapDict> dicts)
        {
            var ret = DbContextManager.Create<Dict>()?.SaveSettings(dicts) ?? false;
            if (ret) CacheCleanUtility.ClearCache(dictIds: new List<string>());
            return ret;
        }

        /// <summary>
        /// 保存網站UI設置
        /// </summary>
        /// <param name="items"></param>
        /// <returns></returns>
        public static bool SaveUISettings(IEnumerable<BootstrapDict> items)
        {
            var cache = new Dictionary<string, string>()
            {
                ["SaveWebTitle"] = "網站標題",
                ["SaveWebFooter"] = "網站頁腳",
                ["SaveTheme"] = "使用樣式",
                ["ShowCardTitle"] = "卡片標題狀態",
                ["ShowSideBar"] = "側邊欄狀態",
                ["FixedTableHeader"] = "固定表頭",
                ["OAuth"] = "OAuth 認證登錄",
                ["SMS"] = "短信驗證碼登錄",
                ["AutoLock"] = "自動鎖屏",
                ["AutoLockPeriod"] = "自動鎖屏時長",
                ["DefaultApp"] = "默認應用程序",
                ["Blazor"] = "Blazor",
                ["IPLocator"] = "IP地理位置接口",
                ["ErrLog"] = "程序異常保留時長",
                ["OpLog"] = "操作日誌保留時長",
                ["LogLog"] = "登錄日誌保留時長",
                ["TraceLog"] = "訪問日誌保留時長",
                ["CookiePeriod"] = "Cookie保留時長",
                ["IPCachePeriod"] = "IP請求緩存時長",
                ["AppPath"] = "後台地址",
                ["EnableHealth"] = "健康檢查",
                ["Login"] = "登錄界面"
            };
            var ret = SaveSettings(items.Where(i => cache.Any(c => c.Key == i.Name)).Select(i => new BootstrapDict()
            {
                Category = "网站设置",
                Name = cache[i.Name],

                // 後台網站配置不能以 / 號結尾
                Code = i.Name == "AppPath" ? i.Code.TrimEnd('/') : i.Code
            }));
            return ret;
        }

        /// <summary>
        /// 獲取字典分類名稱
        /// </summary>
        /// <returns></returns>
        public static IEnumerable<string> RetrieveCategories() => CacheManager.GetOrAdd(RetrieveCategoryDataKey, key => DbContextManager.Create<Dict>()?.RetrieveCategories()) ?? new string[0];

        /// <summary>
        /// 獲取站點 Title 配置信息
        /// </summary>
        /// <param name="appId"></param>
        /// <returns></returns>
        public static string RetrieveWebTitle(string appId) => DbContextManager.Create<Dict>()?.RetrieveWebTitle(appId) ?? string.Empty;

        /// <summary>
        /// 獲取站點 Footer 配置信息
        /// </summary>
        /// <param name="appId"></param>
        /// <returns></returns>
        public static string RetrieveWebFooter(string appId) => DbContextManager.Create<Dict>()?.RetrieveWebFooter(appId) ?? string.Empty;

        /// <summary>
        /// 獲得系統中配置的可以使用的網站樣式
        /// </summary>
        /// <returns></returns>
        public static IEnumerable<BootstrapDict> RetrieveThemes() => DbContextManager.Create<Dict>()?.RetrieveThemes() ?? new BootstrapDict[0];

        /// <summary>
        /// 獲得網站設置中的當前樣式
        /// </summary>
        /// <returns></returns>
        public static string RetrieveActiveTheme() => DbContextManager.Create<Dict>()?.RetrieveActiveTheme() ?? string.Empty;

        /// <summary>
        /// 獲取頭像路徑
        /// </summary>
        /// <returns></returns>
        public static string RetrieveIconFolderPath() => DbContextManager.Create<Dict>()?.RetrieveIconFolderPath() ?? "~/images/uploader/";

        /// <summary>
        /// 獲得默認的前台首頁地址，默認為 ~/Home/Index
        /// </summary>
        /// <param name="userName">登錄用戶名</param>
        /// <param name="appId">默認應用程序編碼</param>
        /// <returns></returns>
        public static string RetrieveHomeUrl(string? userName, string appId) => DbContextManager.Create<Dict>()?.RetrieveHomeUrl(userName, appId) ?? "~/Home/Index";

        /// <summary>
        /// 獲取所有應用程序數據方法
        /// </summary>
        /// <returns></returns>
        public static IEnumerable<KeyValuePair<string, string>> RetrieveApps() => DbContextManager.Create<Dict>()?.RetrieveApps() ?? new KeyValuePair<string, string>[0];

        /// <summary>
        /// 程序異常時長 默認 1 個月
        /// </summary>
        /// <returns></returns>
        public static int RetrieveExceptionsLogPeriod() => DbContextManager.Create<Dict>()?.RetrieveExceptionsLogPeriod() ?? 1;

        /// <summary>
        /// 獲得操作日誌保留時長 默認 12 個月
        /// </summary>
        /// <returns></returns>
        public static int RetrieveLogsPeriod() => DbContextManager.Create<Dict>()?.RetrieveLogsPeriod() ?? 12;

        /// <summary>
        /// 獲得登錄日誌保留時長 默認 12 個月
        /// </summary>
        /// <returns></returns>
        public static int RetrieveLoginLogsPeriod() => DbContextManager.Create<Dict>()?.RetrieveLoginLogsPeriod() ?? 12;

        /// <summary>
        /// 獲取登錄認證Cookie保留時長 默認 7 天
        /// </summary>
        /// <returns></returns>
        public static int RetrieveCookieExpiresPeriod() => DbContextManager.Create<Dict>()?.RetrieveCookieExpiresPeriod() ?? 7;

        /// <summary>
        /// 獲取 IP 地址位置查詢服務名稱
        /// </summary>
        /// <returns></returns>
        public static string RetrieveLocaleIPSvr() => DbContextManager.Create<Dict>()?.RetrieveLocaleIPSvr() ?? string.Empty;

        /// <summary>
        /// 通過 IP 地理位置查詢服務名稱獲得請求地址方法
        /// </summary>
        /// <param name="ipSvr">ip地址請求服務名稱</param>
        /// <returns></returns>
        public static string RetrieveLocaleIPSvrUrl(string ipSvr) => DbContextManager.Create<Dict>()?.RetrieveLocaleIPSvrUrl(ipSvr) ?? string.Empty;

        /// <summary>
        /// 獲取 IP 地理位置查詢服務緩存時長
        /// </summary>
        /// <returns></returns>
        public static int RetrieveLocaleIPSvrCachePeriod() => DbContextManager.Create<Dict>()?.RetrieveLocaleIPSvrCachePeriod() ?? 10;

        /// <summary>
        /// 訪問日誌保留時長 默認 1 個月
        /// </summary>
        /// <returns></returns>
        public static int RetrieveAccessLogPeriod() => DbContextManager.Create<Dict>()?.RetrieveAccessLogPeriod() ?? 1;

        /// <summary>
        /// 獲得 是否為演示系統 默認為 false 不是演示系統
        /// </summary>
        /// <returns></returns>
        public static bool RetrieveSystemModel() => DbContextManager.Create<Dict>()?.RetrieveSystemModel() ?? true;

        /// <summary>
        /// 設置 系統是否為演示系統 默認為 false 不是演示系統
        /// </summary>
        /// <returns></returns>
        public static bool UpdateSystemModel(bool isDemo, string authKey)
        {
            var ret = false;
            // 檢查授權碼
            // 請求者提供 秘鑰與結果 服務器端通過算法比對結果
            if (LgbCryptography.ComputeHash(authKey, RetrieveAuthorSalt()) == RetrieveAuthorHash())
            {
                ret = DbContextManager.Create<Dict>()?.UpdateSystemModel(isDemo) ?? false;
            }
            return ret;
        }

        /// <summary>
        /// 獲得 字典表中配置的授權鹽值
        /// </summary>
        /// <returns></returns>
        public static string RetrieveAuthorSalt() => RetrieveDicts().FirstOrDefault(d => d.Category == "网站设置" && d.Name == "授權鹽值")?.Code ?? "";

        /// <summary>
        /// 獲得 字典表中配置的哈希值
        /// </summary>
        /// <returns></returns>
        public static string RetrieveAuthorHash() => RetrieveDicts().FirstOrDefault(d => d.Category == "网站设置" && d.Name == "哈希結果")?.Code ?? "";

        /// <summary>
        /// 獲得驗證碼圖床地址
        /// </summary>
        /// <returns></returns>
        public static string RetrieveImagesLibUrl() => DbContextManager.Create<Dict>()?.RetrieveImagesLibUrl() ?? string.Empty;

        /// <summary>
        /// 獲得數據區卡片標題是否顯示
        /// </summary>
        /// <returns></returns>
        public static bool RetrieveCardTitleStatus() => DbContextManager.Create<Dict>()?.RetrieveCardTitleStatus() ?? true;

        /// <summary>
        /// 獲得側邊欄狀態 為真時顯示
        /// </summary>
        /// <returns></returns>
        public static bool RetrieveSidebarStatus() => DbContextManager.Create<Dict>()?.RetrieveSidebarStatus() ?? true;

        /// <summary>
        /// 獲得系統設置地址
        /// </summary>
        /// <param name="appId">App 應用ID 默認為 0 表示後台管理程序</param>
        /// <returns></returns>
        public static string RetrieveSettingsUrl(string appId) => DbContextManager.Create<Dict>()?.RetrieveSettingsUrl(appId) ?? "";

        /// <summary>
        /// 獲得系統個人中心地址
        /// </summary>
        /// <param name="appId">App 應用ID 默認為 0 表示後台管理程序</param>
        /// <returns></returns>
        public static string RetrieveProfilesUrl(string appId) => DbContextManager.Create<Dict>()?.RetrieveProfilesUrl(appId) ?? "";

        /// <summary>
        /// 獲得系統通知地址地址
        /// </summary>
        /// <param name="appId">App 應用ID 默認為 0 表示後台管理程序</param>
        /// <returns></returns>
        public static string RetrieveNotisUrl(string appId) => DbContextManager.Create<Dict>()?.RetrieveNotisUrl(appId) ?? "";

        /// <summary>
        /// 獲得是否允許短信驗證碼登錄
        /// </summary>
        /// <returns></returns>
        public static bool RetrieveMobileLogin() => DbContextManager.Create<Dict>()?.RetrieveMobileLogin() ?? false;

        /// <summary>
        /// 獲得是否允許 OAuth 認證登錄
        /// </summary>
        /// <returns></returns>
        public static bool RetrieveOAuthLogin() => DbContextManager.Create<Dict>()?.RetrieveOAuthLogin() ?? false;

        /// <summary>
        /// 獲得自動鎖屏時長 默認 30 秒
        /// </summary>
        /// <returns></returns>
        public static int RetrieveAutoLockScreenPeriod() => DbContextManager.Create<Dict>()?.RetrieveAutoLockScreenPeriod() ?? 30;

        /// <summary>
        /// 獲得自動鎖屏 默認關閉
        /// </summary>
        /// <returns></returns>
        public static bool RetrieveAutoLockScreen() => DbContextManager.Create<Dict>()?.RetrieveAutoLockScreen() ?? false;

        /// <summary>
        /// 獲得是否開啟默認應用功能 默認關閉
        /// </summary>
        /// <returns></returns>
        public static bool RetrieveDefaultApp() => DbContextManager.Create<Dict>()?.RetrieveDefaultApp() ?? false;

        /// <summary>
        /// 獲得是否開啟 Blazor 功能 默認關閉
        /// </summary>
        /// <returns></returns>
        public static bool RetrieveEnableBlazor() => DbContextManager.Create<Dict>()?.RetrieveEnableBlazor() ?? false;

        /// <summary>
        /// 獲得是否開啟 固定表頭 默認開啟
        /// </summary>
        /// <returns></returns>
        public static bool RetrieveFixedTableHeader() => DbContextManager.Create<Dict>()?.RetrieveFixedTableHeader() ?? false;

        /// <summary>
        /// 獲得字典表地理位置配置信息集合
        /// </summary>
        /// <returns></returns>
        public static IEnumerable<BootstrapDict> RetireveLocators() => DbContextManager.Create<Dict>()?.RetireveLocators() ?? new BootstrapDict[0];

        /// <summary>
        /// 獲得個人中心地址
        /// </summary>
        /// <returns></returns>
        public static string RetrievePathBase() => DbContextManager.Create<Dict>()?.RetrievePathBase() ?? "";

        /// <summary>
        /// 獲得字典表健康檢查是否開啟
        /// </summary>
        /// <returns></returns>
        public static bool RetrieveHealth() => DbContextManager.Create<Dict>()?.RetrieveHealth() ?? true;

        /// <summary>
        /// 獲得登錄界面數據
        /// </summary>
        /// <returns></returns>
        public static IEnumerable<BootstrapDict> RetrieveLogins() => DbContextManager.Create<Dict>()?.RetrieveLogins() ?? new BootstrapDict[0];

        /// <summary>
        /// 獲得使用中的登錄視圖名稱
        /// </summary>
        /// <returns></returns>
        public static string RetrieveLoginView() => DbContextManager.Create<Dict>()?.RetrieveLoginView() ?? "Login";

        /// <summary>
        /// 保存前台應用配置信息
        /// </summary>
        /// <param name="option"></param>
        /// <returns></returns>
        public static bool SaveAppSettings(QueryAppOption option)
        {
            bool update = option.AppId == "edit";

            // dict define == 1 時為新建前台應用
            bool ret;

            // 前台網站配置地址 不允許以 / 結尾
            option.AppUrl = option.AppUrl.TrimEnd('/');
            if (update)
            {
                // Update
                ret = SaveSettings(new BootstrapDict[] {
                    new BootstrapDict()
                    {
                        Category = option.AppName,
                        Name = "网站标题",
                        Code = option.AppTitle,
                        Define = 1
                    },
                    new BootstrapDict()
                    {
                        Category = option.AppName,
                        Name = "网站页脚",
                        Code = option.AppFooter,
                        Define = 1
                    },
                    new BootstrapDict()
                    {
                        Category = "应用首页",
                        Name = option.AppCode,
                        Code = option.AppUrl,
                        Define = 0
                    },
                    new BootstrapDict()
                    {
                        Category = option.AppName,
                        Name = "网站图标",
                        Code = option.AppFavicon,
                        Define = 1
                    },
                    new BootstrapDict()
                    {
                        Category = option.AppName,
                        Name = "favicon",
                        Code = option.AppIcon,
                        Define = 1
                    }
                });
            }
            else
            {
                ret = Save(new BootstrapDict()
                {
                    Category = "应用程序",
                    Name = option.AppName,
                    Code = option.AppCode,
                    Define = 0
                });
                if (ret) ret = Save(new BootstrapDict()
                {
                    Category = "应用首页",
                    Name = option.AppCode,
                    Code = option.AppUrl,
                    Define = 0
                });
                if (ret) ret = Save(new BootstrapDict()
                {
                    Category = option.AppName,
                    Name = "网站标题",
                    Code = option.AppTitle,
                    Define = 1
                });
                if (ret) ret = Save(new BootstrapDict()
                {
                    Category = option.AppName,
                    Name = "网站页脚",
                    Code = option.AppFooter,
                    Define = 1
                });
                if (ret) ret = Save(new BootstrapDict()
                {
                    Category = option.AppName,
                    Name = "个人中心地址",
                    Code = "/Admin/Profiles",
                    Define = 1
                });
                if (ret) ret = Save(new BootstrapDict()
                {
                    Category = option.AppName,
                    Name = "系统设置地址",
                    Code = "/Admin/Index",
                    Define = 1
                });
                if (ret) ret = Save(new BootstrapDict()
                {
                    Category = option.AppName,
                    Name = "系统通知地址",
                    Code = "/Admin/Notifications",
                    Define = 1
                });
                if (ret) ret = Save(new BootstrapDict()
                {
                    Category = option.AppName,
                    Name = "网站图标",
                    Code = option.AppFavicon,
                    Define = 1
                });
                if (ret) ret = Save(new BootstrapDict()
                {
                    Category = option.AppName,
                    Name = "favicon",
                    Code = option.AppIcon,
                    Define = 1
                });
            }
            return ret;
        }

        /// <summary>
        /// 刪除指定前台應用
        /// </summary>
        /// <param name="dict"></param>
        /// <returns></returns>
        public static bool DeleteApp(BootstrapDict dict)
        {
            var ids = new List<string>();
            ids.AddRange(RetrieveDicts().Where(d => d.Category == "应用程序" && d.Name == dict.Name && d.Code == dict.Code).Select(d => d.Id ?? ""));
            ids.AddRange(RetrieveDicts().Where(d => d.Category == "应用首页" && d.Name == dict.Code).Select(d => d.Id ?? ""));
            ids.AddRange(RetrieveDicts().Where(d => d.Category == dict.Name).Select(d => d.Id ?? ""));

            return Delete(ids);
        }
    }
}
