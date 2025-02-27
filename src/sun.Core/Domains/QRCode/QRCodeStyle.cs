using sun.EntityFrameworkCore.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sun.Core.Domains.QrCode
{
    /// <summary>
    /// 二维码样式
    /// </summary>
    public class QRCodeStyle : AuditedEntity
    {
        /// <summary>
        /// 宽
        /// </summary>
        public int Width { get; set; }

        /// <summary>
        /// 高
        /// </summary>
        public int Height { get; set; }

        /// <summary>
        /// 上边距
        /// </summary>
        public int Top { get; set; }

        /// <summary>
        /// 左边距
        /// </summary>
        public int Left { get; set; }

        /// <summary>
        /// 边距
        /// </summary>
        public int Margin { get; set; }

        /// <summary>
        /// 背景颜色
        /// </summary>
        [MaxLength(64)]
        public string BackgroundColor { get; set; }

        /// <summary>
        /// 码点类型
        /// </summary>
        public QRCodeDotType DotType { get; set; }

        /// <summary>
        /// 码点颜色
        /// </summary>
        [MaxLength(64)]
        public string DotColor { get; set; }

        /// <summary>
        /// 码眼样式
        /// </summary>
        public QRCodeCornersDotType CornerDotType { get; set; }

        /// <summary>
        /// 码眼颜色
        /// </summary>
        [MaxLength(64)]
        public string CornerDotColor { get; set; }

        /// <summary>
        /// 码眼边框样式
        /// </summary>
        //public QRCodeCornersSquareType CornerSquareType { get; set; }

        /// <summary>
        /// 码眼边框颜色
        /// </summary>
        [MaxLength(64)]
        public string CornerSquareColor { get; set; }

        /// <summary>
        /// Logo 图片地址
        /// </summary>
        [MaxLength(256)]
        public string Logo { get; set; }
    }

    /// <summary>
    /// 二维码码点的类型
    /// </summary>
    public enum QRCodeDotType
    {
        /// <summary>
        /// 正方形
        /// </summary>
        Square = 0,
        /// <summary>
        /// 超圆角正方形（比一般的圆角幅度还要大）
        /// </summary>
        ExtraRounded,
        /// <summary>
        /// 实心圆
        /// </summary>
        Dots,
        /// <summary>
        /// 圆角正方形
        /// </summary>
        Rounded,
        Classy,
        ClassyRounded
    }

    /// <summary>
    /// 码眼的两种类型
    /// </summary>
    public enum QRCodeCornersDotType
    {
        /// <summary>
        /// 实心圆
        /// </summary>
        Dot = 0,
        /// <summary>
        /// 实心正方形
        /// </summary>
        Square
    }
}
