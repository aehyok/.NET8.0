using sun.EntityFrameworkCore.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sun.Core.Domains.QrCode
{
    /// <summary>
    /// 二维码模板文字块
    /// </summary>
    public class QRCodeTemplateText : AuditedEntity
    {
        /// <summary>
        /// 模板 Id
        /// </summary>
        public long QRCodeTemplateId { get; set; }

        /// <summary>
        /// 宽度
        /// </summary>
        public int Width { get; set; }

        /// <summary>
        /// 高度
        /// </summary>
        public int Height { get; set; }

        /// <summary>
        /// 距离顶部像素
        /// </summary>
        public int Top { get; set; }

        /// <summary>
        /// 距离左边像素
        /// </summary>
        public int Left { get; set; }

        /// <summary>
        /// 文字大小
        /// </summary>
        public int FontSize { get; set; }

        /// <summary>
        /// 加粗
        /// </summary>
        public bool TextBold { get; set; }

        /// <summary>
        /// 文字对齐方式
        /// </summary>
        public TextAlign TextAlign { get; set; }

        /// <summary>
        /// 文字内容
        /// </summary>
        [MaxLength(1024)]
        public string TextContent { get; set; }

        /// <summary>
        /// 文字颜色
        /// </summary>
        [MaxLength(64)]
        public string TextColor { get; set; }

        /// <summary>
        /// 背景颜色
        /// </summary>
        [MaxLength(64)]
        public string BackgroundColor { get; set; }

        /// <summary>
        /// 模板
        /// </summary>
        [ForeignKey(nameof(QRCodeTemplateId))]
        public virtual QRCodeTemplate QRCodeTemplate { get; set; }
    }

    public enum TextAlign
    {
        Left = 0,
        Center = 1,
        Right = 2,
    }
}
