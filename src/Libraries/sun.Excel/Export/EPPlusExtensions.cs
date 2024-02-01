using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sun.Excel.Export
{
    public class EPPlusExtensions
    {
        /// <summary>
        /// 生成 Excel
        /// </summary>
        /// <param name="savePath"></param>
        /// <param name="templatePath"></param>
        /// <param name="data"></param>
        /// <exception cref="FileNotFoundException"></exception>
        public static void GenerateExcelWithTemplate(string savePath, string templatePath, object data)
        {
            using var render = new ExcelTemplateRender(templatePath);
            render.Fill(data).SaveAs(savePath);
        }

        public static Stream GenerateExcelWithTemplate(string templatePath, object data)
        {
            using var render = new ExcelTemplateRender(templatePath);

            var stream = new MemoryStream();
            render.Fill(data).SaveAs(stream);

            return stream;
        }

        /// <summary>
        /// DataTable转Excel数据
        /// </summary>
        /// <param name="dataTables"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public static Stream DataTableToExcel(List<SheetDto> dataTables)
        {
            if (dataTables is null || dataTables.Count == 0) throw new Exception("数据表为空");
            using (ExcelPackage package = new ExcelPackage())
            {
                foreach (var item in dataTables)
                {
                    var dataTable = item.Data;
                    ExcelWorksheet worksheet = package.Workbook.Worksheets.Add(item.SheetName);

                    // 写入表头
                    for (int i = 0; i < dataTable.Columns.Count; i++)
                    {
                        var ColumnName = dataTable.Columns[i].ColumnName;
                        worksheet.Cells[1, i + 1].Value = ColumnName;
                        worksheet.Column(i + 1).Width = 60;// 单位:字符 7px=1字符
                        if (ColumnName.Contains("日期") || ColumnName.Contains("时间"))
                            worksheet.Column(i + 1).Width = 20;
                    }

                    // 写入数据行
                    for (int row = 0; row < dataTable.Rows.Count; row++)
                    {
                        worksheet.Row(row + 2).Height = 150;// 单位:磅 0.75px=1磅
                        for (int col = 0; col < dataTable.Columns.Count; col++)
                        {
                            var rowStr = (dataTable.Rows[row][col] ?? string.Empty).ToString();
                            #region 图片 九宫格
                            if (rowStr.StartsWith("[data:image]"))
                            {
                                var imgs = rowStr.Replace("[data:image]", "").Split(',', StringSplitOptions.RemoveEmptyEntries);
                                var imgsCount = imgs.Count();
                                // 图片大小px计算
                                var width = 420; // 图片宽度
                                var height = 200; // 图片高度
                                var columnOffset = 0; //横向偏移值
                                var rowOffset = 0; // 纵向偏移值
                                // 当前图片行数
                                var imagesPerRow = imgsCount > 4 ? 3
                                    : imgsCount > 2 ? 2
                                    : 1;
                                //计算图片大小
                                if (imagesPerRow == 1 && imgsCount == 2)
                                {
                                    width = width / 2;
                                }
                                else if (imagesPerRow == 2)
                                {
                                    width = width / 2;
                                    height = height / 2;
                                }
                                else if (imagesPerRow == 3 && imgsCount <= 6)
                                {
                                    width = width / 3;
                                    height = height / 2;
                                }
                                else if (imagesPerRow == 3)
                                {
                                    width = width / 3;
                                    height = height / 3;
                                }
                                var imgIndex = 1;
                                var thisImgRow = 1;
                                foreach (var img in imgs)
                                {
                                    if (imgIndex > 9) break;
                                    ReportHelper.InsertPicture(sheet: worksheet
                                        , name: Guid.NewGuid().ToString()
                                        , pictureBase64: img
                                        , row: row + 2 - 1
                                        , column: col + 1 - 1
                                        , width: width
                                        , height: height
                                        , rowOffset: rowOffset
                                        , columnOffset: columnOffset);
                                    columnOffset += width;
                                    imgIndex++;
                                    if (imagesPerRow > 1 && Math.Ceiling((double)imgIndex / imagesPerRow) > thisImgRow)
                                    {
                                        thisImgRow++;
                                        columnOffset = 0;
                                        rowOffset += height;
                                    }
                                }
                            }
                            #endregion
                            #region 视频
                            else if (rowStr.StartsWith("[data:video]"))
                            {
                                // 在单元格A1中插入视频链接
                                var videoLink = worksheet.Cells[row + 2, col + 1];
                                videoLink.Hyperlink = new ExcelHyperLink(rowStr.Replace("[data:video]", ""));
                                videoLink.Value = "点击观看视频"; // 这是链接的文本描述

                                // 设置单元格样式，使其看起来像链接
                                videoLink.Style.Font.UnderLine = true;
                                videoLink.Style.Font.Color.SetColor(System.Drawing.Color.Blue);
                            }
                            #endregion
                            // 普通数据
                            else
                                worksheet.Cells[row + 2, col + 1].Value = rowStr;
                        }
                    }
                }
                // 将Excel数据保存到MemoryStream
                var stream = new MemoryStream();
                package.SaveAs(stream);

                return stream;
            }
        }
    }
}
