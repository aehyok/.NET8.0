using sun.EntityFrameworkCore.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sun.Basic.Domains
{
    public class WeChatUrlToBlog: AuditedEntity
    {
        public string SourceUrl { get; set; }

        /// <summary>
        /// Url来源分类
        /// </summary>
        public SourceUrlType SourceUrlType { get; set; }

        /// <summary>
        /// 含有html标签的微信公众号文章
        /// </summary>
        public string SourceContent { get; set; }

        /// <summary>
        /// Gemini提取微信公众号文章
        /// </summary>
        public string GeminiContent { get; set; }

        /// <summary>
        /// 重写内容
        /// </summary>
        public string ReWriteContent { get; set; }

        /// <summary>
        /// 将重写内容转换为新的html风格
        /// </summary>
        public string ConvertContentToHtml { get; set; }

        /// <summary>
        /// 转换为微信公众号文章格式的html排版
        /// </summary>
        public string ConvertWeChatHtml { get; set; }

        /// <summary>
        /// 生成的文章标题
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// 摘要
        /// </summary>
        public string Digest { get; set; }

        /// <summary>
        /// 生成的封面图Id
        /// </summary>
        public long CoverImageId { get; set; }

        /// <summary>
        /// 上传封面图返回Id
        /// </summary>
        public string MediaId { get; set; }

        /// <summary>
        /// 封面图Url
        /// </summary>
        public string MediaUrl { get; set; }

        /// <summary>
        /// base64 封面图片
        /// </summary>
        public string CoverImageBase64 { get; set; }

        /// <summary>
        /// 封面图文件名
        /// </summary>
        public string CoverImageUrl { get; set; }
    }

    public enum SourceUrlType
    {
        WeChat = 1,
    }
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

        /// <summary>
        /// CookieType类型
        /// </summary>
        public CookieType CookieType { get; set; }
    }

    public enum CookieType
    {
        单次拉取Cookie = 1,
        微信公众号后台管理批量拉取Cookie = 2
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
