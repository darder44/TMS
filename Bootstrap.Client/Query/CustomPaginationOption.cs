using Bootstrap.Client.DataAccess;
using Longbow.Web.Mvc;
using System.Linq;
using System;
using Bootstrap.Client.DataAccess.Helper;

namespace Bootstrap.Client.Query
{
    /// <summary>
    /// 分页配置项类
    /// </summary>
    public class CustomPaginationOption
    {
        /// <summary>
        /// 获得/设置 每页显示行数
        /// </summary>
        public int Limit { get; set; }
        /// <summary>
        /// 获得/设置 当前数据偏移量 Offset / Limit 的页码 第一页换算方法为 Offset / Limit + 1
        /// </summary>
        public int Offset { get; set; }
        /// <summary>
        /// 获得/设置 排序列名称
        /// </summary>
        public string? Sort { get; set; }
        /// <summary>
        /// 获得/设置 排序方式 asc/desc
        /// </summary>
        public string? Order { get; set; }
        /// <summary>
        /// 获得/设置 搜索内容
        /// </summary>
        public string? Search { get; set; }
        /// <summary>
        /// 获得 当前页码，内部自动通过Limit Offset属性计算获得
        /// </summary>
        public int PageIndex { get; }
    }
}