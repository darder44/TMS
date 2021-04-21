using Bootstrap.Client.DataAccess;
using Bootstrap.Security;
using Longbow.Tasks;
using Microsoft.Extensions.Hosting;
using System.Linq;
using System.Threading;
using Task = System.Threading.Tasks.Task;

namespace Microsoft.Extensions.DependencyInjection
{
    /// <summary>
    /// 後台任務擴展方法
    /// </summary>
    internal static class TasksExtensions
    {
        /// <summary>
        /// 添加示例後台任務
        /// </summary>
        /// <param name="services"></param>
        public static IServiceCollection AddBootstrapAdminBackgroundTask(this IServiceCollection services)
        {
            services.AddTaskServices(builder => builder.AddFileStorage());
            services.AddHostedService<BootstrapAdminBackgroundServices>();
            return services;
        }
    }

    /// <summary>
    /// 後台任務服務類
    /// </summary>
    internal class BootstrapAdminBackgroundServices : BackgroundService
    {
        /// <summary>
        /// 運行任務
        /// </summary>
        /// <param name="stoppingToken"></param>
        /// <returns></returns>
        protected override Task ExecuteAsync(CancellationToken stoppingToken) => Task.Run(() =>
        {
            // 真實任務負責批次寫入數據執行腳本到日誌中
            TaskServicesManager.GetOrAdd<DBLogTask>("SQL日誌", TriggerBuilder.Build(Cron.Minutely()));
        });
    }
}
