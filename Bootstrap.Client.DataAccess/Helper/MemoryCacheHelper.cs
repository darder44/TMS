using System;
using System.IO;
using Microsoft.Extensions.Caching.Memory;

namespace Bootstrap.Client.DataAccess.Helper
{
    /// <summary>
    /// 下載快取
    /// </summary>
    public class DownloadCache
    {
        /// <summary>
        /// 
        /// </summary>
        public MemoryStream mem { get; set; }

        /// <summary>
        /// 檔案名稱
        /// </summary>
        public string fileName { get; set; }
    }

    /// <summary>
    /// 
    /// </summary>
    public static class MemoryCacheHelper
    {
        /// <summary>
        /// IMemoryCache擴充功能
        /// </summary>
        public static string SetDownloadCache(this IMemoryCache cache, DownloadCache downloadCache)
        {
            if (downloadCache == null) return string.Empty;
            //產生取檔Token
            var token = Guid.NewGuid();
            //存入Cache待下載(限一分鐘內有效)
            cache.Set<DownloadCache>(token.ToString(), downloadCache, new MemoryCacheEntryOptions(){ SlidingExpiration = TimeSpan.FromSeconds(10) }); 
            return token.ToString();
        }
    }
}