using sun.Basic.Domains;
using sun.Core;
using sun.Core.Dtos;
using sun.Core.Dtos.Query;
using sun.Core.Services;
using sun.Infrastructure;
using LinqKit;
using Microsoft.AspNetCore.Mvc;
using OfficeOpenXml;
using OfficeOpenXml.Style;
using X.PagedList;
using sun.Basic.Services;
using Microsoft.EntityFrameworkCore;
using sun.Core.Domains;

namespace sun.Basic.Api.Controllers
{
    /// <summary>
    /// 登录日志 
    /// </summary>
    public class TokenLogController(IUserTokenService userTokenService, IUserService userService, IUserRoleService userRoleService, IRegionService regionService) : BasicControllerBase
    {
        /// <summary>
        /// 获取登录日志列表
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpGet("list")]
        public async Task<IPagedList<UserTokenLogDto>> GetLogListAsync([FromQuery] UserTokenQueryDto model)
        {
            var currentUser = base.CurrentUser;
            var filter = PredicateBuilder.New<UserToken>(true);

            if (model.platformType > 0)
            {
                filter.And(a => a.PlatformType == model.platformType);
            }

            if (model.StartTime.HasValue)
            {
                filter.And(a => a.CreatedAt >= model.StartTime.Value);
            }

            if (model.EndTime.HasValue)
            {
                filter.And(a => a.CreatedAt <= model.EndTime.Value);
            }

            var regionFilter = PredicateBuilder.New<Region>(true);
            regionFilter.And(a => EF.Functions.Like(a.IdSequences, $"%{currentUser.RegionId}%"));


            var query = from ut in userTokenService.GetExpandable().Where(filter)
                        join u in userService.GetQueryable() on ut.UserId equals u.Id
                        join ur in userRoleService.GetQueryable() on ut.RoleId equals ur.RoleId
                        join re in regionService.GetQueryable().Where(regionFilter) on ur.RegionId equals re.Id
                        select new UserTokenLogDto
                        {
                            Id = ut.Id,
                            loginUser = u.RealName,
                            loginAt = ut.CreatedAt,
                            PlatformType = ut.PlatformType,
                            RoleName = ur.Role.Name,
                            RegionName = ur.Region.Name,
                            IpAddress = ut.IpAddress,
                            UserAgent = ut.UserAgent
                        };

            query.OrderByDescending(a => a.Id);

            return await query.ToPagedListAsync(model.Page, model.Limit);
        }


        public class Student
        {
            /// <summary>
            ///     姓名
            /// </summary>
            public string Name { get; set; }
            /// <summary>
            ///     年龄
            /// </summary>
            public int Age { get; set; }
        }


        [HttpGet("excel")]
        public async Task ExportHeaderAsByteArrayWithItems_Test()
        {
            using (var package = new ExcelPackage())
            {
                var sheet1 = package.Workbook.Worksheets.Add("Sheet1");

                string[] cols = { "商户名称", "订单号", "订单类型", "配送方式", "下单时间", "结算金额" };

                for (int c = 1; c <= cols.Length; c++)
                {
                    sheet1.Cells[1, c].Value = cols[c - 1];
                    sheet1.Cells[1, c].Style.Fill.PatternType = ExcelFillStyle.Solid;
                    sheet1.Cells[1, c].Style.Fill.BackgroundColor.SetColor(System.Drawing.Color.LightGray);
                    sheet1.Cells[1, c].Style.Font.Bold = true;
                    sheet1.Cells[1, c].Style.Font.Size = 12;
                    sheet1.Column(c).AutoFit();
                }
                sheet1.Row(1).Height = 30;
                sheet1.Row(1).Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                sheet1.Row(1).Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                sheet1.View.FreezePanes(2, 1);

                string fileName = "xxxxx.xlsx";
                
                string outputPath = Path.Combine(App.GetTempPath(), fileName);

                FileInfo outputFile = new FileInfo(outputPath);
                package.SaveAs(outputFile);
            }
        }
    }
}
