using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;

namespace Bootstrap.Client.Extensions
{
    /// <summary>
    /// 緩存配置類
    /// </summary>
    /// <typeparam name="TOptions"></typeparam>
    internal class SmtpConfigureOptions<TOptions> : ConfigureFromConfigurationOptions<TOptions> where TOptions : class
    {
        /// <summary>
        /// 構造函數
        /// </summary>
        /// <param name="configuration"></param>
        public SmtpConfigureOptions(IConfiguration configuration)
            : base(configuration.GetSection("SmtpClient"))
        {

        }
    }
}
