using Bootstrap.Security;
using Bootstrap.Security.DataAccess;
using Longbow.Cache;
using Longbow.Web.Mvc;
using PetaPoco;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace Bootstrap.Client.DataAccess.Helper
{
    /// <summary>
    /// 緩存Helper
    /// </summary>
    public static class CacheHelper
    {
        /// <summary>
        /// 清除指定緩存
        /// </summary>
        public static bool Clear(string cacheKey)
        {
            CacheCleanUtility.ClearCache(cacheKey: cacheKey);
            return true;
        }

        /// <summary>
        /// 清除所有碼頭緩存
        /// </summary>
        public static void ClearBestDock()
        {
            CacheCleanUtility.ClearCache(cacheKey: $"{Base_DockHelper.RetrieveDocksDataKey}*");     
        }

        /// <summary>
        /// 清除所有倉別緩存
        /// </summary>
        public static void ClearBestSL()
        {
            CacheCleanUtility.ClearCache(cacheKey: $"{Base_SLHelper.RetrieveSLsDataKey}*");           
            CacheCleanUtility.ClearCache(cacheKey: $"{Base_SLHelper.RetrieveByStorerkeyDataKey}*");
        }

        /// <summary>
        /// 清除所有客戶主檔緩存
        /// </summary>
        public static void ClearBestStorerKey()
        {
            CacheCleanUtility.ClearCache(cacheKey: $"{BaseStorerHelper.RetrievesStorersDataKey}*");
        }

        /// <summary>
        /// 清除所有計費代碼緩存
        /// </summary>
        public static void ClearBestCostCode()
        {
            CacheCleanUtility.ClearCache(cacheKey: $"{Base_SLHelper.RetrieveSLsDataKey}*");
            CacheCleanUtility.ClearCache(cacheKey: $"{BaseStorerHelper.RetrievesStorersDataKey}*");                       
            CacheCleanUtility.ClearCache(cacheKey: $"{BaseCostCodeHelper.RetrievesCostCodeDataKey}*");
        }

        /// <summary>
        /// 清除所有客戶主檔緩存
        /// </summary>
        public static void ClearBaseCustomer()
        {
            CacheCleanUtility.ClearCache(cacheKey: $"{BaseCustomerHelper.RetrievesCustomerDataKey}*");
        }

        /// <summary>
        /// 清除所有字典表緩存
        /// </summary>
        public static void ClearBestDictionary()
        {
            CacheCleanUtility.ClearCache(cacheKey: $"{BaseDictionaryHelper.RetrievesDictionaryDataKey}*");
        }

        /// <summary>
        /// 清除所有轉運站緩存
        /// </summary>
        public static void ClearBestFacility()
        {
            CacheCleanUtility.ClearCache(cacheKey: $"{BaseFacilityHelper.RetrievesFacilityDataKey}*");
        }

        /// <summary>
        /// 清除所有貨運公司緩存
        /// </summary>
        public static void ClearBaseShippingCompany()
        {
            CacheCleanUtility.ClearCache(cacheKey: $"{BaseShippingCompanyHelper.RetrievesShippingCompanyDataKey}*");
        }

        /// <summary>
        /// 清除所有車輛資料緩存
        /// </summary>
        public static void ClearBaseVehicle()
        {
            CacheCleanUtility.ClearCache(cacheKey: $"{BaseVehicleHelper.RetrievesVehicleDataKey}*");
        }

        /// <summary>
        /// 清除所有車種緩存
        /// </summary>
        public static void ClearBaseVehicleType()
        {
            CacheCleanUtility.ClearCache(cacheKey: $"{BaseVehicleTypeHelper.RetrievesVehicleTypeDataKey}*");
        }

        /// <summary>
        /// 清除所有郵遞區號緩存
        /// </summary>
        public static void ClearBaseZipInfo()
        {
            CacheCleanUtility.ClearCache(cacheKey: $"{BaseZipHelper.RetrievesZipInfoDataKey}*");
        }

        /// <summary>
        /// 清除所有區域代碼緩存
        /// </summary>
        public static void ClearBaseZipAreaCode()
        {
            CacheCleanUtility.ClearCache(cacheKey: $"{BaseZipHelper.RetrievesAreaCodeDataKey}*");
        }

        public static bool ClearBaseCache()
        {
            ClearBestDock();
            ClearBestSL();
            ClearBestStorerKey();
            ClearBestCostCode();
            ClearBaseCustomer();
            ClearBestDictionary();
            ClearBestFacility();
            ClearBaseShippingCompany();
            ClearBaseVehicle();
            ClearBaseVehicleType();
            ClearBaseZipInfo();
            ClearBaseZipAreaCode();
            return true;
        }
        
        /// <summary>
        /// 清除所有緩存
        /// </summary>
        public static bool ClearAll()
        {
            
            ClearBaseCache();      
            return true;
        }
    }
}
