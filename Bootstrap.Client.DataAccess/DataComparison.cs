using Microsoft.AspNetCore.Http;
using System;
using System.Text;
using System.Data;
using System.Collections.Generic;
using ExcelDataReader;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Web;
using System.Linq;
using System.Globalization;

namespace Bootstrap.Client.DataAccess
{
    /// <summary>
    /// 資料比對
    /// </summary>
    public static class DataComparison
    {
        /// <summary>
        /// 日期轉換yyyyMMdd格式
        /// </summary>
        public static string DateTimeConvert(DateTime? dt, string format = "")
        {
            return string.Format(string.IsNullOrEmpty(format) ? "{0:yyyyMMdd}" : format, dt);
        }

        /// <summary>
        /// 日期轉換yyyyMMdd格式
        /// </summary>
        public static DateTime? DateTimeConvert(string sDate)
        {
            DateTime? date = null;
            DateTime cdate = DateTime.Now;
            if (DateTime.TryParseExact(sDate, "yyyyMMdd", CultureInfo.InvariantCulture, DateTimeStyles.None, out cdate)) 
            {
                date = cdate;
            }
            else
            {
                date = Convert.ToDateTime(sDate);
            }
            return date;
        }

        /// <summary>
        /// 日期轉換yyyyMMdd格式
        /// </summary>
        public static bool DateTimeCompare(DateTime? source, DateTime? target)
        {
            return DateTimeConvert(source) == DateTimeConvert(target);
        }

        /// <summary>
        /// 日期轉換yyyyMMdd格式
        /// </summary>
        public static bool DateTimeLessThan(DateTime? source, DateTime? dest)
        {
            var sourceDT = DateTime.Parse(DateTimeConvert(source, "{0:yyyy/MM/dd}"));
            var destDT = DateTime.Parse(DateTimeConvert(dest, "{0:yyyy/MM/dd}"));
            return sourceDT <= destDT;
        }

        /// <summary>
        /// 日期轉換yyyyMMdd格式
        /// </summary>
        public static bool DateTimeGreaterThan(DateTime? source, DateTime? dest)
        {
            var sourceDT = DateTime.Parse(DateTimeConvert(source, "{0:yyyy/MM/dd}"));
            var destDT = DateTime.Parse(DateTimeConvert(dest, "{0:yyyy/MM/dd}"));
            return sourceDT >= destDT;
        }

        /// <summary>
        /// Excel轉換成對應類別
        /// </summary>
        public static IEnumerable<T> ExcelConvert<T>(IFormFile file, object value = null)
        {
            IEnumerable<T> ret = null;
            string str_json = "";
            int appendCount = 0;
            try
            {
                var str_value = JsonConvert.SerializeObject(value);
                var obj = JsonConvert.DeserializeObject<JObject>(str_value);
                var stream = file.OpenReadStream();
                var reader = ExcelReaderFactory.CreateReader(stream);
                var dt = reader.AsDataSet();
                if (dt.Tables.Count == 0) return null;
                var table = dt.Tables[0];
                if (table.Rows.Count < 2) return null;
                if (table.Columns.Count == 0) return null;
                StringBuilder resultsb = new StringBuilder();
                resultsb.Append("[");
                DataRow fieldRow = table.Rows[0];
                for(var rowidx = 1; rowidx < table.Rows.Count; rowidx ++)
                {
                    DataRow row = table.Rows[rowidx];
                    StringBuilder sb = new StringBuilder();
                    var comma = rowidx == 1 ? "" : ",";
                    var columnIdx = 0;
                    sb.Append(comma + "{");
                    foreach (DataColumn column in table.Columns)
                    {   
                        var field = fieldRow[column].ToString();
                        if (string.IsNullOrEmpty(field)) continue;
                        var currentField = field;
                        if (obj != null)
                        {
                            currentField = "";
                            foreach(var token in obj)
                            {
                                var key = token.Key.Trim();
                                var val = token.Value.ToString().Trim();
                                if (val == field)
                                {
                                    currentField = key;
                                    break;
                                }
                            }
                        }
                        if (string.IsNullOrEmpty(currentField)) continue;
                        string str = row[column].ToString();
                        str = string.IsNullOrEmpty(str) ? "null" : '"' + HttpUtility.JavaScriptStringEncode(str) + '"';
                        comma = columnIdx == 0 ? "" : ",";
                        sb.Append(comma + '"'+ currentField + '"' + ":" + str);
                        columnIdx += 1;
                        appendCount += 1;
                    }
                    sb.Append("}");
                    resultsb.Append(sb);
                }
                if (appendCount == 0) return ret; 
                resultsb.Append("]");
                str_json = resultsb.ToString();
                ret = JsonConvert.DeserializeObject<IEnumerable<T>>(str_json);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return ret;
        }

    }
}
