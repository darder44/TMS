using Bootstrap.Client.DataAccess;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace Bootstrap.Client.Models
{
    /// <summary>
    /// 
    /// </summary>
    public class HeaderBarModel : ModelBase
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="controller"></param>
        public HeaderBarModel(ControllerBase controller)
        {
            var user = UserHelper.RetrieveUserByUserName(controller.User.Identity.Name);
            if (user != null)
            {
                // set LogoutUrl
                var config = controller.HttpContext.RequestServices.GetRequiredService<IConfiguration>();
                var host = config.GetBootstrapAdminAuthenticationOptions().AuthHost;
                UriBuilder uriBuilder = null;

                DisplayName = user.DisplayName;
                UserName = user.UserName;
                SettingsUrl = DictHelper.RetrieveSettingsUrl(AppId);
                ProfilesUrl = DictHelper.RetrieveProfilesUrl(AppId);
                NotisUrl = DictHelper.RetrieveNotisUrl(AppId);

                uriBuilder = new UriBuilder(SettingsUrl);
                uriBuilder.Host = controller.Request.Host.Host;
                SettingsUrl = uriBuilder.ToString();

                uriBuilder = new UriBuilder(ProfilesUrl);
                uriBuilder.Host = controller.Request.Host.Host;
                ProfilesUrl = uriBuilder.ToString();

                uriBuilder = new UriBuilder(NotisUrl);
                uriBuilder.Host = controller.Request.Host.Host;
                NotisUrl = uriBuilder.ToString();

                uriBuilder = new UriBuilder(host);
                uriBuilder.Host = controller.Request.Host.Host;
                uriBuilder.Path = uriBuilder.Path == "/" ? CookieAuthenticationDefaults.LogoutPath.Value : $"{uriBuilder.Path.TrimEnd('/')}{CookieAuthenticationDefaults.LogoutPath.Value}";
                uriBuilder.Query = $"AppId={AppId}";
                LogoutUrl = uriBuilder.ToString();

                // set Icon
                uriBuilder = new UriBuilder(host);
                uriBuilder.Host = controller.Request.Host.Host;
                var icon = $"/{DictHelper.RetrieveIconFolderPath().Trim('~', '/')}/{user.Icon}";
                Icon = user.Icon.Contains("://", StringComparison.OrdinalIgnoreCase) ? user.Icon : (string.IsNullOrEmpty(config.GetValue("SimulateUserName", string.Empty)) ? $"{uriBuilder.ToString().TrimEnd('/')}{icon}" : "/images/admin.jpg");
                if (!string.IsNullOrEmpty(user.Css)) Theme = user.Css;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public string UserName { get; } = "";

        /// <summary>
        /// 
        /// </summary>
        public string DisplayName { get; } = "";

        /// <summary>
        /// 獲得/設置 用戶頭像地址
        /// </summary>
        public string Icon { get; } = "";

        /// <summary>
        /// 獲得/設置 設置網址
        /// </summary>
        public string SettingsUrl { get; } = "";

        /// <summary>
        /// 獲得/設置 個人中心網址
        /// </summary>
        public string ProfilesUrl { get; } = "";

        /// <summary>
        /// 獲得 退出登錄地址
        /// </summary>
        public string LogoutUrl { get; } = "";

        /// <summary>
        /// 
        /// </summary>
        public string NotisUrl { get; } = "";
    }
}
