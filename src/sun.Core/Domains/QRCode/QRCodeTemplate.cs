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
    /// 二维码模板
    /// </summary>
    public class QRCodeTemplate : AuditedEntity
    {
        /// <summary>
        /// 区域 Id
        /// </summary>
        public long RegionId { get; set; }

        /// <summary>
        /// 类别代码，字典项
        /// </summary>
        [MaxLength(256)]
        public string TypeCode { get; set; }

        /// <summary>
        /// 名称
        /// </summary>
        [MaxLength(256)]
        public string Name { get; set; }

        /// <summary>
        /// 宽度
        /// </summary>
        public int Width { get; set; }

        /// <summary>
        /// 高度
        /// </summary>
        public int Height { get; set; }

        /// <summary>
        /// 背景图，如果内容是链接，背景就是图片，否则就是颜色（十六进制颜色代码#FFB6C1）
        /// </summary>
        [MaxLength(512)]
        public string Background { get; set; }

        /// <summary>
        /// 是否默认
        /// </summary>
        public bool IsDefault { get; set; }

        /// <summary>
        /// 预览效果（图片url）
        /// </summary>
        [MaxLength(512)]
        public string Preview { get; set; }

        /// <summary>
        /// 模板中二维码样式 Id
        /// </summary>
        public long QRCodeStyleId { get; set; }

        /// <summary>
        /// 区域
        /// </summary>
        [ForeignKey(nameof(RegionId))]
        public virtual Region Region { get; set; }

        /// <summary>
        /// 二维码样式
        /// </summary>
        [ForeignKey(nameof(QRCodeStyleId))]
        public virtual QRCodeStyle QRCodeStyle { get; set; }

        /// <summary>
        /// 文字块
        /// </summary>
        public virtual List<QRCodeTemplateText> TextBlocks { get; set; }
    }
}
