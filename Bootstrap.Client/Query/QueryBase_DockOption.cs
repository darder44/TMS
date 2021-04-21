using Bootstrap.Client.DataAccess;
using Longbow.Web.Mvc;
using System.Linq;
using System;
using Bootstrap.Client.DataAccess.Helper;

namespace Bootstrap.Client.Query
{
    /// <summary>
    /// 
    /// </summary>
    public class QueryBase_DockOption : CustomPaginationOption
    {
        /// <summary>
        /// 
        /// </summary>
        public string Dock { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string Notes { get; set; }

        /// <summary>
        /// 取得篩選條件資料
        /// </summary>

        //輸入條件查詢
        public QueryData<object> RetrieveData()
        {
            var data = Base_DockHelper.Retrieves();

            if (!string.IsNullOrEmpty(Dock))
            {
                data = data.Where(t => t.Dock.Contains(Dock));
            }

            if (!string.IsNullOrEmpty(Notes))
            {
                data = data.Where(t => t.Notes.Contains(Notes));
            }         

            var ret = new QueryData<object>();
            ret.total = data.Count();

            //資料排序
            switch (Sort)
            {
                case "Dock":
                    data = Order == "asc" ? data.OrderBy(t => t.Dock) : data.OrderByDescending(t => t.Dock);
                    break;         
                case "Notes":
                    data = Order == "asc" ? data.OrderBy(t => t.Notes) : data.OrderByDescending(t => t.Notes);
                    break;              
            }
            if (Limit != 0) data = data.Skip(Offset).Take(Limit);
            //重新查詢欄位資料
            ret.rows = data.Select(u => new
            {
                Id = u.Dock,
                u.Dock,
                u.Notes
            });
            return ret;
        }
    }
}
