using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Text.Json;

namespace Bootstrap.Client.Extensions
{
    /// <summary>
    /// SmtpExtensions 擴展類
    /// </summary>
    internal static class SmtpExtensions
    {
        /// <summary>
        /// 格式化
        /// </summary>
        /// <param name="message"></param>
        /// <param name="webRootPath"></param>
        /// <returns></returns>
        public static string FormatHealths(this string message, string webRootPath)
        {
            var cate = new Dictionary<string, string>()
            {
                ["db"] = "數據庫",
                ["file"] = "組件文件",
                ["mem"] = "內存",
                ["Gitee"] = "Gitee 接口",
                ["gc"] = "垃圾回收器",
                ["dotnet-runtime"] = "運行時",
                ["environment"] = "環境變量"
            };
            var state = new Dictionary<string, string>()
            {
                { "0", "不健康" },
                { "1", "亞健康" },
                { "2", "健康" }
            };
            var styleFile = System.IO.File.ReadAllText(Path.Combine(webRootPath, "html\\healths.html".ReplaceOSPlatformPath()));
            var itemTemplate = System.IO.File.ReadAllText(Path.Combine(webRootPath, "html\\item.html".ReplaceOSPlatformPath()));
            var trTemplate = "<tr><td>{0}</td><td>{1}</td></tr>";

            var sb = new StringBuilder();
            sb.Append(styleFile);
            var root = JsonDocument.Parse(message).RootElement;
            var items = root.GetProperty("Keys").EnumerateArray();
            foreach (var item in items)
            {
                var itemName = item.GetString();

                // 通過 itemName 讀取檢查項明細內容
                var data = root.GetProperty("Report").GetProperty("Entries").GetProperty(itemName);
                var rowData = new StringBuilder();
                foreach (var row in data.GetProperty("Data").EnumerateObject())
                {
                    rowData.AppendFormat(trTemplate, row.Name, row.Value.GetRawText().Replace("\\r\\n", "<br>").Replace("\\n", "<br>"));
                }
                sb.Append(string.Format(itemTemplate, cate[itemName], data.GetProperty("Duration").GetString(), state[data.GetProperty("Status").GetRawText()], rowData.ToString()));
            }
            return sb.ToString();
        }
    }
}
