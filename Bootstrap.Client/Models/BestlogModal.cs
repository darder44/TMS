using Bootstrap.Client.DataAccess;
using Bootstrap.Client.DataAccess.Helper;
using Bootstrap.Security;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Bootstrap.Client.Models
{
    /// <summary>
    /// 自定義modal
    /// </summary>
    public class BestlogModal : NavigatorBarModel
    {

        /// <summary>
        /// 自定義
        /// </summary>
        public BestlogModal(ControllerBase controller) : base(controller)
        {
            var Console = BaseDictionaryHelper.Retrieves().Where(d => d.CodeName == "BestAPP");
            GoogleMapAPI = "https://maps.googleapis.com/maps/api/js?key=" + Console.Where(d => d.Description == "GoogleAPIKey").SingleOrDefault().Code;
            GoogleServerKey = Console.Where(d => d.Description == "GoogleServerKey").SingleOrDefault().Code;
            GoogleSenderID = Console.Where(d => d.Description == "GoogleSenderID").SingleOrDefault().Code;
        }

        /// <summary>
        /// Google map API
        /// </summary>
        public string GoogleMapAPI { get; protected set; }
        /// <summary>
        /// Google server key
        /// </summary>
        public string GoogleServerKey {get; protected set;}
        /// <summary>
        /// Google SenderID
        /// </summary>
        public string GoogleSenderID {get; protected set;}

    }

    
}
