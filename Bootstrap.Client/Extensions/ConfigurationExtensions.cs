using Microsoft.AspNetCore;
using Bootstrap.Client.Extensions;

namespace Microsoft.Extensions.Configuration
{
    /// <summary>
    /// IConfiguration 擴展類
    /// </summary>
    public static class ConfigurationExtensions
    {
        /// <summary>
        /// 獲得 配置文件中 BootstrapAdminAuthenticationOptions 實例
        /// </summary>
        /// <param name="configuration"></param>
        /// <returns></returns>
        public static BootstrapAdminAuthenticationOptions GetBootstrapAdminAuthenticationOptions(this IConfiguration configuration) => configuration.GetSection<BootstrapAdminAuthenticationOptions>().Get<BootstrapAdminAuthenticationOptions>();
    }
}
