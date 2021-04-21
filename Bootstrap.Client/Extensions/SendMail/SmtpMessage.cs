namespace Bootstrap.Client.Extensions
{
    /// <summary>
    /// SmtpMessage 郵件實體類
    /// </summary>
    internal class SmtpMessage
    {
        /// <summary>
        /// 獲得/設置 郵件標題
        /// </summary>
        public string Title { get; set; } = "";

        /// <summary>
        /// 獲得/設置 郵件正文
        /// </summary>
        public string Message { get; set; } = "";
    }
}
