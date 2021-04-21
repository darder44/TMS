
using Bootstrap.Client.DataAccess;
using Longbow.Web.Mvc;
using System.Linq;
using System;
using Bootstrap.Client.DataAccess.Helper;
using System.Collections.Generic;

namespace Bootstrap.Client.Query.ExportExcelBody
{
    /// <summary>
    /// 
    /// </summary>
    public class BootstrapSheet
    {
        /// <summary>
        /// Sheet名稱
        /// </summary>
        public string SheetName { get; set; }
        /// <summary>
        /// 排序欄位名稱
        /// </summary>
        public string sortName { get; set; }
        /// <summary>
        /// 欄位定義
        /// </summary>
        public List<ColumnMapping> Columns { get; set; }
    }

    /// <summary>
    /// 
    /// </summary>
    public class ExportExcelBody<T>
    {
        /// <summary>
        /// Excel檔案名稱
        /// </summary>
        public string FileName { get; set; }
        /// <summary>
        /// 查詢條件
        /// </summary>
        public T Query { get; set; }
        /// <summary>
        /// 分頁
        /// </summary>
        public IEnumerable<BootstrapSheet> Sheets { get; set; }
    }
}