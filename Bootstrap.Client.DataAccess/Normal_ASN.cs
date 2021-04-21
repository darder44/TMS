using Bootstrap.Security.DataAccess;
using PetaPoco;
using System;
using System.Collections.Generic;
using Longbow.Web.Mvc;
using System.Linq;

namespace Bootstrap.Client.DataAccess
{
    /// <summary>
    /// 
    /// </summary>
    public class Normal_ASN
    {
        /// <summary>
        /// ASNKey
        /// </summary>
        public string ASNKey { get; set; }
        /// <summary>
        /// ExternASNKey
        /// </summary>
        public string ExternASNKey { get; set; }
        /// <summary>
        /// StorerKey
        /// </summary>
        public string StorerKey { get; set; }
        /// <summary>
        /// ASNDate
        /// </summary>
        public DateTime? ASNDate { get; set; }
        /// <summary>
        /// SellersReference2
        /// </summary>
        public string SellersReference2 { get; set; }
        /// <summary>
        /// ASNType
        /// </summary>
        public string ASNType { get; set; }
        /// <summary>
        /// TransMethod
        /// </summary>
        public string TransMethod { get; set; }
        /// <summary>
        /// OrderQty
        /// </summary>
        public int OrderQty { get; set; }
        /// <summary>
        /// CaseQty
        /// </summary>
        public int CaseQty { get; set; }
        /// <summary>
        /// Status
        /// </summary>
        public string Status { get; set; }
        /// <summary>
        /// Notes
        /// </summary>
        public string Notes { get; set; }


        //ASN轉RC訂單 
        public virtual IEnumerable<Normal_ASN> RetrievesASNnotinReceipt()
        {
            string strSQL =  
                @"SELECT  
                    ASNKey = rtrim(a.ASNKey)
                    ,ExternASNKey = rtrim(a.ExternASNKey)
                    ,StorerKey = rtrim(a.StorerKey)
                    ,ASNDate = convert(char(10),a.ASNDate,111)
                    ,SellersReference2 = isnull(a.SellersReference2,'')
                    ,ASNType = rtrim(a.ASNType)
                    ,TransMethod = rtrim(a.TransMethod)
                    ,OrderQty = sum(ad.QtyOrdered)
                    ,Status = rtrim(a.Status)
                    ,Notes = isnull(convert(varchar(255),a.Notes),'')
                    ,CaseQty = sum(case when p.CaseCnt = 0 then 0 else ad.QtyOrdered / p.CaseCnt end)
                FROM ASN a  
                    join ASNDETAIL ad on a.ASNKey = ad.ASNKey
                    join SKU s on s.SKU = ad.SKU and s.StorerKey = ad.StorerKey    
                    join PACK p on p.PACKKey = s.PackKey  
                WHERE  rtrim(a.TransferRC) <> '1' and rtrim(a.Status) = '0'
                GROUP BY rtrim(a.ASNKey),rtrim(a.ExternASNKey),rtrim(a.StorerKey),convert(char(10),a.ASNDate,111),rtrim(a.TransMethod)
                    ,isnull(a.SellersReference2,''),rtrim(a.ASNType),rtrim(a.Status),isnull(convert(varchar(255),a.Notes),'')";

            return DbManager.Create("bestlogwms").Fetch<Normal_ASN>(strSQL);  
        }
    }
}
