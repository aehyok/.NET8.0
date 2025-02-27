using sun.EntityFrameworkCore.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sun.Basic.Domains
{
    /// <summary>
    /// 公众号文章列表
    /// </summary>
    public class WeChatBlog : AuditedEntity
    {
        /// <summary>
        /// 微信公众号aid
        /// </summary>
        public string AId { get; set; }
        /// <summary>
        /// 公众号文章
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// 封面图
        /// </summary>
        public string Cover { get; set; }
        /// <summary>
        /// 摘要
        /// </summary>
        public string Digest { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public long Create_Time { get; set; }

        /// <summary>
        /// 修改时间
        /// </summary>
        public long Update_Time { get; set; }

        /// <summary>
        /// 文章详情链接
        /// </summary>
        public string Link { get; set; }

        /// <summary>
        /// 公众号发布内容类型
        /// </summary>
        public int ItemShowType { get; set; }

        /// <summary>
        /// 作者
        /// </summary>
        public string AuthorName { get; set; }
    }
}
