using System.Collections.Generic;

namespace Bootstrap.Client.Extensions
{
    /// <summary>
    /// SmtpOption 配置類
    /// </summary>
    public class SmtpOption
    {
        /// <summary>
        /// 獲得/設置 主機地址
        /// </summary>
        public string Host { get; set; } = "";

        /// <summary>
        /// 獲得/設置 郵箱密碼
        /// </summary>
        public string Password { get; set; } = "";

        /// <summary>
        /// 獲得/設置 發件人地址
        /// </summary>
        public string From { get; set; } = "";

        /// <summary>
        /// 獲得/設置 收件人地址
        /// </summary>
        public string To { get; set; } = "";

        /// <summary>
        /// 獲得/設置 端口
        /// </summary>
        public int Port { get; set; }

        /// <summary>
        /// 獲得/設置 是否啟用 SSL
        /// </summary>
        public bool EnableSsl { get; set; }

        /// <summary>
        /// 獲得/設置 郵件發送人顯示名稱
        /// </summary>
        public string DisplayName { get; set; } = "";

        /// <summary>
        /// 獲得/設置 郵件發送超時時間 默認 100000 毫秒
        /// </summary>
        public int Timeout { get; set; } = 100000;

        /// <summary>
        /// 獲得/設置 郵件黑名單
        /// </summary>
        public ICollection<string> BlackList { get; set; } = new HashSet<string>();
    }
}
