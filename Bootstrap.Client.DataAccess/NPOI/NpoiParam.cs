using NPOI.SS.UserModel;
using System.Collections.Generic;

namespace Bootstrap.Client.DataAccess.Helper
{
    public enum NpoiDataType
    {
        String = 0,
        Int = 1,
        Double = 2,
        Date = 3
    }

    public class FontStyle
    {
        public string FontName { get; set; }
        public short? FontHeightInPoints { get; set; }

        public FontStyle()
        {
            FontName = null;
            FontHeightInPoints = null;
        }
    }

    public class ColumnMapping
    {
        public string field { get; set; }
        /// <summary>
        /// 若以範本初始化 Excel 則此欄可不填
        /// </summary>
        public string title { get; set; }
        public NpoiDataType dataType { get; set; }
        /// <summary>
        /// 如果是 String 則這個欄位不生效
        /// </summary>
        public string dataFormat { get; set; }
    }

    public class NpoiParam<T>
    {
        private int? _RowStartFrom;
        private int? _ColumnStartFrom;
        private bool? _ShowHeader;
        private bool? _IsAutoFit;
        private FontStyle _fontStyle;

        /// <summary>
        /// 資料
        /// </summary>
        public IEnumerable<T> Data { get; set; }
        /// <summary>
        /// 原始的 Sheet Name
        /// </summary>
        public string OriginSheetName { get; set; }
        /// <summary>
        /// 上一個最後一筆
        /// </summary>
        public int LastTo { get; set; }
        /// <summary>
        /// 欲新增(或已存在)的 Sheet Name
        /// </summary>
        public string SheetName { get; set; }
        /// <summary>
        /// 與 Excel 檔間的欄位對應
        /// </summary>
        public List<ColumnMapping> ColumnMapping { get; set; }
        /// <summary>
        /// 預設從第 1 行開始塞資料 ( 第 0 行為標題欄位 )
        /// </summary>
        public int RowStartFrom
        {
            get { return _RowStartFrom ?? 1; }
            set { _RowStartFrom = value; }
        }
        /// <summary>
        /// 預設從第 0 欄開始塞資料
        /// </summary>
        public int ColumnStartFrom
        {
            get { return _ColumnStartFrom ?? 0; }
            set { _ColumnStartFrom = value; }
        }
        /// <summary>
        /// 是否excel要畫表頭 (預設畫表頭 = true)
        /// </summary>
        public bool ShowHeader
        {
            get { return _ShowHeader ?? true; }
            set { _ShowHeader = value; }
        }
        /// <summary>
        /// 是否自動調整欄寬 (預設不自動調整欄寬 = false)
        /// </summary>
        public bool IsAutoFit
        {
            get { return _IsAutoFit ?? false; }
            set { _IsAutoFit = value; }
        }

        /// <summary>
        /// 自己決定文字預設格式
        /// </summary>
        public FontStyle FontStyle
        {
            get { return _fontStyle ?? new FontStyle(); }
            set { _fontStyle = value; }
        }
    }
}
