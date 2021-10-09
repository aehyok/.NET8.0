using System;
using System.Collections.Generic;
using System.Text;
using X.PagedList;

namespace aehyok.Core.Data
{
    /// <summary>
    /// 分页返回JSON结构
    /// </summary>
    public class PagedJsonResultModel
    {
        public PagedJsonResultModel()
        {
        }

        public PagedJsonResultModel(IPagedList list)
        {
            this.Page = list.PageNumber;
            this.Pages = list.PageCount;
            this.Total = list.TotalItemCount;
            this.Limit = list.PageSize;
            this.Docs = list;
        }

        /// <summary>
        /// 当前页码
        /// </summary>
        public int Page { get; set; }

        /// <summary>
        /// 总页数
        /// </summary>
        public int Pages { get; set; }

        /// <summary>
        /// 总记录数
        /// </summary>
        public int Total { get; set; }

        /// <summary>
        /// 每页记录条数
        /// </summary>
        public int Limit { get; set; }

        /// <summary>
        /// 分页数据
        /// </summary>
        public dynamic Docs { get; set; }
    }
}
