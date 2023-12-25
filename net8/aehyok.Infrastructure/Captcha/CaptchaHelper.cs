using SkiaSharp;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace aehyok.Infrastructure.Captcha
{
    public class CaptchaHelper
    {
        /// <summary>
        /// 生成验证码图片
        /// </summary>
        /// <param name="width"></param>
        /// <param name="height"></param>
        /// <param name="captchaCode"></param>
        /// <returns></returns>
        public static byte[] GenerateCaptchaImage(int width, int height, string captchaCode)
        {
            using var image = new SKBitmap(width, height, SKColorType.Rgba8888, SKAlphaType.Premul);

            // 创建画布
            using var canvas = new SKCanvas(image);

            // 填充白色
            canvas.DrawColor(SKColors.White);

            var random = new Random();

            // 绘制背景噪音线
            using var lineStyle = new SKPaint();
            lineStyle.IsAntialias = true;
            for (var i = 0; i < (width * height * 0.01); i++)
            {
                lineStyle.Color = new SKColor(Convert.ToUInt32(random.Next(Int32.MaxValue)));
                canvas.DrawLine(random.Next(0, width), random.Next(0, height), random.Next(0, width), random.Next(0, height), lineStyle);
            }

            // 绘制验证码
            using var typeface = SKTypeface.FromFile(Path.Combine(AppContext.BaseDirectory, "Fonts/SpecialElite-Regular.ttf"));
            using var textFont = new SKFont(typeface);
            using var textStyle = new SKPaint(textFont);
            var fontSize = GetFontSize(width, captchaCode.Length);
            textStyle.TextSize = fontSize;
            textStyle.IsAntialias = true;

            var xSpace = (width - 10f) / captchaCode.Length;

            for (var i = 0; i < captchaCode.Length; i++)
            {
                textStyle.Color = GetRandomDeepColor();

                var textBounds = new SKRect();
                textStyle.MeasureText(captchaCode[i].ToString(), ref textBounds);

                float x = xSpace * i + 5 + (xSpace - textBounds.Width) / 2;
                float maxY = height + textBounds.Top;
                if (maxY < 0) maxY = 0;
                float y = random.Next(5, Convert.ToInt32(maxY - 5));

                var rotateDegrees = random.Next(0, 25);

                // 以文字中心为原点将画布旋转随机角度
                canvas.RotateDegrees(rotateDegrees % 2 == 0 ? rotateDegrees : -rotateDegrees, x + textBounds.Width / 2, y - textBounds.Height / 2);
                canvas.DrawText(captchaCode[i].ToString(), x, y - textBounds.Top, textStyle);

                // 将画布旋转到正常角度
                canvas.RotateDegrees(rotateDegrees % 2 == 0 ? -rotateDegrees : rotateDegrees, x + textBounds.Width / 2, y - textBounds.Height / 2);
            }

            using var pointStyle = new SKPaint();
            pointStyle.IsAntialias = true;
            pointStyle.StrokeWidth = 2;
            for (var i = 0; i < (width * height * 0.04); i++)
            {
                pointStyle.Color = new SKColor(Convert.ToUInt32(random.Next(Int32.MaxValue)));
                canvas.DrawPoint(random.Next(width), random.Next(height), pointStyle);
            }

            return image.Encode(SKEncodedImageFormat.Jpeg, 80).ToArray();
        }

        /// <summary>
        /// 根据验证码图片尺寸计算验证码文字大小
        /// </summary>
        /// <param name="imageWidth"></param>
        /// <param name="captchaLength"></param>
        /// <returns></returns>
        private static int GetFontSize(int imageWidth, int captchaLength)
        {
            var averageSize = imageWidth / captchaLength;
            return Convert.ToInt32(averageSize);
        }

        /// <summary>
        /// 生成随机颜色
        /// </summary>
        /// <returns></returns>
        private static SKColor GetRandomDeepColor()
        {
            var random = new Random();
            return new SKColor((byte)random.Next(160), (byte)random.Next(100), (byte)random.Next(160), byte.MaxValue);
        }
    }
}
