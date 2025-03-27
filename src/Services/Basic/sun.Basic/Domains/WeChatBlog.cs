using sun.EntityFrameworkCore.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sun.Basic.Domains
{
    public class WeChatConfig: AuditedEntity
    {
        /// <summary>
        /// 微信公众号的Cookie
        /// </summary>
        public string Cookie { get; set; }

        /// <summary>
        /// FakeId
        /// </summary>
        public string FakeId { get; set; }

        /// <summary>
        /// Token
        /// </summary>
        public string Token { get; set; }
    }

    /// <summary>
    /// 微信公众号列表
    /// </summary>
    public class WeChatOfficialAccount: AuditedEntity
    {
        /// <summary>
        /// 微信公众号名称
        /// </summary>
        public string Name { get; set; }
    }

    /// <summary>
    /// 公众号文章列表
    /// </summary>
    public class WeChatBlog : AuditedEntity
    {
        public long WeChatOfficialAccountId { get; set; }

        public virtual WeChatOfficialAccount WeChatOfficialAccount { get; set; }

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

        /// <summary>
        /// 是否使用AI重写
        /// </summary>
        public bool IsAIReWrite {get;set;}
    }

    public class WeChatBlogDetail : AuditedEntity
    {
        /// <summary>
        /// news 图文消息
        /// newspic图片消息
        /// 不填默认为图文消息
        /// </summary>
        public string ArticleType { get; set; }

        /// <summary>
        /// 标题
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// 作者
        /// </summary>
        public string Author { get; set; }

        /// <summary>
        /// 封面
        /// </summary>
        public string Cover { get; set; }

        /// <summary>
        /// 内容
        /// </summary>
        public string Content { get; set; }

        /// <summary>
        /// 摘要
        /// </summary>
        public string Digest { get; set; }
    }
}
