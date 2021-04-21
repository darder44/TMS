using Microsoft.AspNetCore.Mvc;

namespace Bootstrap.Client.Models
{
    /// <summary>
    /// Mail Model
    /// </summary>
    public class MailModel : NavigatorBarModel
    {
        /// <summary>
        /// 構造函數
        /// </summary>
        public MailModel(ControllerBase controller) : base(controller)
        {

        }

        /// <summary>
        /// 獲得執行結果
        /// </summary>
        public string Result { get; set; } = "";
    }
}
