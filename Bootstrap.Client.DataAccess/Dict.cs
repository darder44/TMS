﻿using Bootstrap.Security;
using Bootstrap.Security.DataAccess;
using Longbow;
using PetaPoco;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Bootstrap.Client.DataAccess
{
    /// <summary>
    /// 字典表實體類
    /// </summary>
    [TableName("Dicts")]
    public class Dict : BootstrapDict
    {
        /// <summary>
        /// 刪除字典中的數據
        /// </summary>
        /// <param name="value">需要刪除的IDs</param>
        /// <returns></returns>
        public virtual bool Delete(IEnumerable<string> value)
        {
            if (!value.Any()) return true;
            var ids = string.Join(",", value);
            string sql = $"where ID in ({ids})";
            using var db = DbManager.Create();
            db.Delete<BootstrapDict>(sql);
            return true;
        }

        /// <summary>
        /// 保存新建/更新的字典信息
        /// </summary>
        /// <param name="dict"></param>
        /// <returns></returns>
        public virtual bool Save(BootstrapDict dict)
        {
            if (dict.Category.Length > 50) dict.Category = dict.Category.Substring(0, 50);
            if (dict.Name.Length > 50) dict.Name = dict.Name.Substring(0, 50);
            if (dict.Code.Length > 2000) dict.Code = dict.Code.Substring(0, 2000);

            using var db = DbManager.Create();
            db.Save(dict);
            return true;
        }

        /// <summary>
        /// 保存網站個性化設置
        /// </summary>
        /// <param name="dicts"></param>
        /// <returns></returns>
        public virtual bool SaveSettings(IEnumerable<BootstrapDict> dicts)
        {
            using var db = DbManager.Create();
            dicts.ToList().ForEach(dict => db.Update<Dict>("set Code = @Code where Category = @Category and Name = @Name", dict));
            return true;
        }

        /// <summary>
        /// 獲取字典分類名稱
        /// </summary>
        /// <returns></returns>
        public virtual IEnumerable<string> RetrieveCategories() => DictHelper.RetrieveDicts().OrderBy(d => d.Category).Select(d => d.Category).Distinct();

        /// <summary>
        /// 獲取系統網站標題
        /// </summary>
        /// <param name="appId"></param>
        /// <returns></returns>
        public virtual string RetrieveWebTitle(string appId)
        {
            // 優先查找配置的應用程序網站標題
            var code = DbHelper.RetrieveTitle(appId);
            if (code == "网站标题未设置") code = DictHelper.RetrieveDicts().FirstOrDefault(d => d.Name == "網站標題" && d.Category == "网站设置" && d.Define == 0)?.Code ?? "後台管理系統";
            return code;
        }

        /// <summary>
        /// 獲取系統網站頁腳
        /// </summary>
        /// <param name="appId"></param>
        /// <returns></returns>
        public virtual string RetrieveWebFooter(string appId)
        {
            // 優先查找配置的應用程序網站標題
            var code = DbHelper.RetrieveFooter(appId);
            if (code == "网站页脚未设置") code = DictHelper.RetrieveDicts().FirstOrDefault(d => d.Name == "網站頁腳" && d.Category == "网站设置" && d.Define == 0)?.Code ?? "2020 © 佰事達後台管理系統";
            return code;
        }

        /// <summary>
        /// 獲得系統設置地址
        /// </summary>
        /// <returns></returns>
        public virtual string RetrieveSettingsUrl(string appId) => RetrieveFullUrl(appId, DbHelper.RetrieveSettingsUrl);

        /// <summary>
        /// 獲得系統個人中心地址
        /// </summary>
        /// <returns></returns>
        public virtual string RetrieveProfilesUrl(string appId) => RetrieveFullUrl(appId, DbHelper.RetrieveProfilesUrl);

        /// <summary>
        /// 獲得系統通知地址地址
        /// </summary>
        /// <returns></returns>
        public virtual string RetrieveNotisUrl(string appId) => RetrieveFullUrl(appId, DbHelper.RetrieveNotisUrl);

        /// <summary>
        /// 支持絕對路徑
        /// </summary>
        /// <param name="appId"></param>
        /// <param name="func"></param>
        /// <returns></returns>
        protected virtual string RetrieveFullUrl(string appId, Func<string, string> func)
        {
            // https://gitee.com/LongbowEnterprise/BootstrapAdmin/issues/I1DIKG
            // 相對路徑時拼接後台地址
            // 絕對路徑時直接使用

            var url = func(appId);
            if (!url.StartsWith("http", StringComparison.OrdinalIgnoreCase)) url = $"{RetrieveAdminPath()}{url}";
            return url;
        }

        private string RetrieveAdminPath() => DictHelper.RetrieveDicts().FirstOrDefault(d => d.Category == "网站设置" && d.Name == "後台地址" && d.Define == 0)?.Code ?? "";

        /// <summary>
        /// 獲得系統中配置的可以使用的網站樣式
        /// </summary>
        /// <returns></returns>
        public virtual IEnumerable<BootstrapDict> RetrieveThemes() => DictHelper.RetrieveDicts().Where(d => d.Category == "网站样式");

        /// <summary>
        /// 獲得網站設置中的當前樣式
        /// </summary>
        /// <returns></returns>
        public virtual string RetrieveActiveTheme()
        {
            var theme = DictHelper.RetrieveDicts().FirstOrDefault(d => d.Name == "使用樣式" && d.Category == "网站设置" && d.Define == 0);
            return theme == null ? string.Empty : (theme.Code.Equals("site.css", StringComparison.OrdinalIgnoreCase) ? string.Empty : theme.Code);
        }

        /// <summary>
        /// 獲取頭像路徑
        /// </summary>
        /// <returns></returns>
        public virtual string? RetrieveIconFolderPath() => DictHelper.RetrieveDicts().FirstOrDefault(d => d.Name == "頭像路徑" && d.Category == "头像地址" && d.Define == 0)?.Code;

        /// <summary>
        /// 獲得默認的前台首頁地址，默認為~/Home/Index
        /// </summary>
        /// <param name="userName">登錄用戶名</param>
        /// <param name="appId">默認應用程序編碼</param>
        /// <returns></returns>
        public virtual string RetrieveHomeUrl(string? userName, string appId)
        {
            // https://gitee.com/LongbowEnterprise/dashboard/issues?id=IS0WK
            // https://gitee.com/LongbowEnterprise/dashboard/issues?id=I17SD0
            var url = "~/Home/Index";
            var dicts = DictHelper.RetrieveDicts();

            if (appId.IsNullOrEmpty())
            {
                var defaultUrl = dicts.FirstOrDefault(d => d.Name == "前台首頁" && d.Category == "网站设置" && d.Define == 0)?.Code;
                if (!string.IsNullOrEmpty(defaultUrl)) url = defaultUrl;
            }
            else if (appId.Equals("BA", StringComparison.OrdinalIgnoreCase))
            {
                // 使用配置項設置是否啟用默認第一個應用是默認應用
                var defaultApp = (dicts.FirstOrDefault(d => d.Name == "默認應用程序" && d.Category == "网站设置" && d.Define == 0)?.Code ?? "0") == "1";
                if (defaultApp)
                {
                    var app = AppHelper.RetrievesByUserName(userName).FirstOrDefault(key => !key.Equals("BA", StringComparison.OrdinalIgnoreCase)) ?? "";
                    if (!string.IsNullOrEmpty(app))
                    {
                        // 指定應用程序的首頁
                        var appUrl = RetrieveDefaultHomeUrlByApp(dicts, app);
                        if (!string.IsNullOrEmpty(appUrl)) url = appUrl;
                    }
                }
            }
            else
            {
                // 指定應用程序的首頁
                var appUrl = RetrieveDefaultHomeUrlByApp(dicts, appId);
                if (!string.IsNullOrEmpty(appUrl)) url = appUrl;
            }
            return url;
        }

        /// <summary>
        /// 通過 appId 獲取應用首頁配置值
        /// </summary>
        /// <param name="dicts"></param>
        /// <param name="appId"></param>
        /// <returns></returns>
        protected virtual string RetrieveDefaultHomeUrlByApp(IEnumerable<BootstrapDict> dicts, string appId)
        {
            return dicts.FirstOrDefault(d => d.Name.Equals(appId, StringComparison.OrdinalIgnoreCase) && d.Category == "应用首页" && d.Define == 0)?.Code ?? "";
        }

        /// <summary>
        /// 獲得字典表中配置的所有應用程序
        /// </summary>
        /// <returns></returns>
        public virtual IEnumerable<KeyValuePair<string, string>> RetrieveApps() => DictHelper.RetrieveDicts().Where(d => d.Category == "应用程序" && d.Define == 0).Select(d => new KeyValuePair<string, string>(d.Code, d.Name)).OrderBy(d => d.Key);

        /// <summary>
        /// 通過數據庫獲得所有字典表配置信息，緩存Key=DictHelper-RetrieveDicts
        /// </summary>
        /// <returns></returns>
        public virtual IEnumerable<BootstrapDict> RetrieveDicts() => DbHelper.RetrieveDicts();

        /// <summary>
        /// 程序異常時長 默認1月
        /// </summary>
        /// <returns></returns>
        public int RetrieveExceptionsLogPeriod() => LgbConvert.ReadValue(DictHelper.RetrieveDicts().FirstOrDefault(d => d.Category == "网站设置" && d.Name == "程序異常保留時長" && d.Define == 0)?.Code, 1);

        /// <summary>
        /// 操作日誌時長 默認12月
        /// </summary>
        /// <returns></returns>
        public int RetrieveLogsPeriod() => LgbConvert.ReadValue(DictHelper.RetrieveDicts().FirstOrDefault(d => d.Category == "网站设置" && d.Name == "操作日誌保留時長" && d.Define == 0)?.Code, 12);

        /// <summary>
        /// 登錄日誌時長 默認12月
        /// </summary>
        /// <returns></returns>
        public int RetrieveLoginLogsPeriod() => LgbConvert.ReadValue(DictHelper.RetrieveDicts().FirstOrDefault(d => d.Category == "网站设置" && d.Name == "登錄日誌保留時長" && d.Define == 0)?.Code, 12);

        /// <summary>
        /// Cookie保存時長 默認7天
        /// </summary>
        /// <returns></returns>
        public int RetrieveCookieExpiresPeriod() => LgbConvert.ReadValue(DictHelper.RetrieveDicts().FirstOrDefault(d => d.Category == "网站设置" && d.Name == "Cookie保留時長" && d.Define == 0)?.Code, 7);

        /// <summary>
        /// 獲得 IP地理位置
        /// </summary>
        /// <returns></returns>
        public string RetrieveLocaleIPSvr() => DictHelper.RetrieveDicts().FirstOrDefault(d => d.Category == "网站设置" && d.Name == "IP地理位置接口" && d.Define == 0)?.Code ?? string.Empty;

        /// <summary>
        /// 獲得 IP請求緩存時長配置值
        /// </summary>
        /// <returns></returns>
        public int RetrieveLocaleIPSvrCachePeriod()
        {
            var period = DictHelper.RetrieveDicts().FirstOrDefault(d => d.Category == "网站设置" && d.Name == "IP請求緩存時長" && d.Define == 0)?.Code;
            var ret = 10;
            if (!string.IsNullOrEmpty(period) && int.TryParse(period, out var svrPeriod)) ret = svrPeriod;
            return ret;
        }

        /// <summary>
        /// 獲得 項目是否獲取登錄地點 默認為false
        /// </summary>
        /// <param name="ipSvr">服務提供名稱</param>
        /// <returns></returns>
        public string? RetrieveLocaleIPSvrUrl(string ipSvr) => DictHelper.RetrieveDicts().FirstOrDefault(d => d.Category == "地理位置" && d.Name == ipSvr && d.Define == 0)?.Code;

        /// <summary>
        /// 獲得 訪問日誌保留時長 默認為1個月
        /// </summary>
        /// <returns></returns>
        public int RetrieveAccessLogPeriod() => LgbConvert.ReadValue(DictHelper.RetrieveDicts().FirstOrDefault(d => d.Category == "网站设置" && d.Name == "訪問日誌保留時長" && d.Define == 0)?.Code, 1);

        /// <summary>
        /// 獲得 是否為演示系統 默認為 false 不是演示系統
        /// </summary>
        /// <returns></returns>
        public bool RetrieveSystemModel() => LgbConvert.ReadValue(DictHelper.RetrieveDicts().FirstOrDefault(d => d.Category == "网站设置" && d.Name == "演示系統" && d.Define == 0)?.Code, "0") == "1";

        /// <summary>
        /// 獲得 是否為演示系統 默認為 false 不是演示系統
        /// </summary>
        /// <returns></returns>
        public bool UpdateSystemModel(bool isDemo)
        {
            var dict = DictHelper.RetrieveDicts().FirstOrDefault(d => d.Category == "网站设置" && d.Name == "演示系統" && d.Define == 0);
            dict.Code = isDemo ? "1" : "0";
            return Save(dict);
        }

        /// <summary>
        /// 獲得 驗證碼圖床地址
        /// </summary>
        /// <returns></returns>
        public string RetrieveImagesLibUrl() => DictHelper.RetrieveDicts().FirstOrDefault(d => d.Category == "网站设置" && d.Name == "驗證碼圖床" && d.Define == 0)?.Code ?? "http://images.sdgxgz.com/";

        /// <summary>
        /// 獲得 數據庫標題是否顯示
        /// </summary>
        /// <returns></returns>
        public bool RetrieveCardTitleStatus() => (DictHelper.RetrieveDicts().FirstOrDefault(d => d.Category == "网站设置" && d.Name == "卡片標題狀態" && d.Define == 0)?.Code ?? "1") == "1";

        /// <summary>
        /// 獲得 是否顯示側邊欄 為真時顯示
        /// </summary>
        /// <returns></returns>
        public bool RetrieveSidebarStatus() => (DictHelper.RetrieveDicts().FirstOrDefault(d => d.Category == "网站设置" && d.Name == "側邊欄狀態" && d.Define == 0)?.Code ?? "1") == "1";

        /// <summary>
        /// 獲得是否允許短信驗證碼登錄
        /// </summary>
        /// <returns></returns>
        public bool RetrieveMobileLogin() => (DictHelper.RetrieveDicts().FirstOrDefault(d => d.Category == "网站设置" && d.Name == "短信驗證碼登錄" && d.Define == 0)?.Code ?? "1") == "1";

        /// <summary>
        /// 獲得是否允許 OAuth 認證登錄
        /// </summary>
        /// <returns></returns>
        public bool RetrieveOAuthLogin() => (DictHelper.RetrieveDicts().FirstOrDefault(d => d.Category == "网站设置" && d.Name == "OAuth 認證登錄" && d.Define == 0)?.Code ?? "1") == "1";

        /// <summary>
        /// 獲得自動鎖屏時長 默認 30 秒
        /// </summary>
        /// <returns></returns>
        public int RetrieveAutoLockScreenPeriod() => LgbConvert.ReadValue(DictHelper.RetrieveDicts().FirstOrDefault(d => d.Category == "网站设置" && d.Name == "自動鎖屏時長" && d.Define == 0)?.Code, 30);

        /// <summary>
        /// 獲得自動鎖屏是否開啟 默認關閉
        /// </summary>
        /// <returns></returns>
        public bool RetrieveAutoLockScreen() => (DictHelper.RetrieveDicts().FirstOrDefault(d => d.Category == "网站设置" && d.Name == "自動鎖屏" && d.Define == 0)?.Code ?? "0") == "1";

        /// <summary>
        /// 獲得默認應用是否開啟 默認關閉
        /// </summary>
        /// <returns></returns>
        public bool RetrieveDefaultApp() => (DictHelper.RetrieveDicts().FirstOrDefault(d => d.Category == "网站设置" && d.Name == "默認應用程序" && d.Define == 0)?.Code ?? "0") == "1";

        /// <summary>
        /// 獲得是否開啟 Blazor 功能 默認關閉
        /// </summary>
        /// <returns></returns>
        public bool RetrieveEnableBlazor() => (DictHelper.RetrieveDicts().FirstOrDefault(d => d.Category == "网站设置" && d.Name == "Blazor" && d.Define == 0)?.Code ?? "0") == "1";

        /// <summary>
        /// 獲得是否開啟 固定表頭 功能 默認開啟
        /// </summary>
        /// <returns></returns>
        public bool RetrieveFixedTableHeader() => (DictHelper.RetrieveDicts().FirstOrDefault(d => d.Category == "网站设置" && d.Name == "固定表頭" && d.Define == 0)?.Code ?? "1") == "1";

        /// <summary>
        /// 獲得字典表地理位置配置信息集合
        /// </summary>
        /// <returns></returns>
        public IEnumerable<BootstrapDict> RetireveLocators() => DictHelper.RetrieveDicts().Where(d => d.Category == "地理位置服务");

        /// <summary>
        /// 獲得個人中心地址
        /// </summary>
        /// <returns></returns>
        public string RetrievePathBase() => DictHelper.RetrieveDicts().FirstOrDefault(d => d.Category == "网站设置" && d.Name == "後台地址" && d.Define == 0)?.Code ?? "";

        /// <summary>
        /// 獲得字典表健康檢查是否開啟
        /// </summary>
        /// <returns></returns>
        public bool RetrieveHealth() => (DictHelper.RetrieveDicts().FirstOrDefault(d => d.Category == "网站设置" && d.Name == "健康檢查" && d.Define == 0)?.Code ?? "0") == "1";

        /// <summary>
        /// 獲得字典表登錄界面數據
        /// </summary>
        /// <returns></returns>
        public IEnumerable<BootstrapDict> RetrieveLogins() => DictHelper.RetrieveDicts().Where(d => d.Category == "系统首页" && d.Define == 1);

        /// <summary>
        /// 獲得使用中的登錄視圖名稱
        /// </summary>
        /// <returns></returns>
        public string? RetrieveLoginView() => DictHelper.RetrieveDicts().FirstOrDefault(d => d.Category == "网站设置" && d.Name == "登錄界面" && d.Define == 1)?.Code;
    }
}
