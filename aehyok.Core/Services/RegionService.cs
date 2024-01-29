using aehyok.Core.Domains;
using aehyok.Core.Dtos;
using aehyok.Core.Services;
using aehyok.EntityFrameworkCore.Repository;
using aehyok.Infrastructure;
using aehyok.Excel.Export;
using aehyok.Infrastructure.Exceptions;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System.Dynamic;

namespace aehyok.Basic.Services
{
    public class RegionService(DbContext dbContext, IMapper mapper, ITemplateService templateService, IFileService fileService) : ServiceBase<Region>(dbContext, mapper), IRegionService, IScopedDependency
    {
        public override async Task<Region> InsertAsync(Region entity, CancellationToken cancellationToken = default)
        {
            if (string.IsNullOrWhiteSpace(entity.IdSequences))
            {
                if (entity.ParentId == 0)
                {
                    entity.IdSequences = $".{entity.Id}.";
                }
                else
                {
                    var parent = await this.GetAsync(a => a.Id == entity.ParentId);
                    if (parent != null)
                    {
                        entity.IdSequences = $"{parent.IdSequences}{entity.Id}.";
                        if (parent.Level == RegionLevel.自然村)
                        {
                            throw new ErrorCodeException(-1, "自然村不能创建下级行政区划");
                        }

                        entity.Level = parent.Level + 1;
                    }
                }
            }

            var exists = await this.GetAsync(a => a.Code == entity.Code);
            if (exists != null)
            {
                throw new ErrorCodeException(-1, $"行政区划代码【{entity.Code}】已存在");
            }

            await base.InsertAsync(entity, cancellationToken);

            return entity;
        }

        /// <summary>
        /// 根据行政区域等级设置行政区域名称
        /// </summary>
        /// <param name="dto"></param>
        /// <param name="region"></param>
        private static void SetExportRegionName(RegionExportDto dto, Region region)
        {
            if (region.Level == RegionLevel.省)
            {
                dto.Province = region.Name;
            }
            else if (region.Level == RegionLevel.市)
            {
                dto.City = region.Name;
            }
            else if (region.Level == RegionLevel.区县)
            {
                dto.County = region.Name;
            }
            else if (region.Level == RegionLevel.乡镇)
            {
                dto.Town = region.Name;
            }
            else if (region.Level == RegionLevel.行政村)
            {
                dto.Village = region.Name;
            }
        }

        public async Task<Core.Domains.File> ExportAsync(RegionExportQueryDto model)
        {
            var template = await templateService.GetAsync(item => item.Code == "RegionExport");
            if(template is null)
            {
                throw new ErrorCodeException(-1, "行政区域模版不存在");
            }

            if(template.ContentType != TemplateContentType.文件)
            {
                throw new ErrorCodeException(-1, "行政区域模版不正确");
            }

            // Content中则是上传文件的Url
            var templateFilePath = template.Content;

            if(templateFilePath.StartsWith("http"))
            {
                var fileInfo = await fileService.GetFileByUrlAsync(templateFilePath);
                templateFilePath = Path.Combine(App.GetTempPath(), $"{fileInfo.Id}{fileInfo.Extension}");

                // 获取文件内容
                var templateBytes = await fileService.GetContentAsync(template.Content);

                using var stream = System.IO.File.Create(templateFilePath);
                await stream.WriteAsync(templateBytes, 0, templateBytes.Length);
            }

            templateFilePath = Path.Combine(AppContext.BaseDirectory, templateFilePath);
            if(!System.IO.File.Exists(templateFilePath))
            {
                throw new ErrorCodeException(-1, "行政区域模版不存在");
            }

            // 根据参数查询出区域列表
            var regions = await this.GetListAsync(a => EF.Functions.Like(a.IdSequences, $"%{model.RegionId}%"));

            // 获取根节点
            var rootNode = regions.FirstOrDefault(a => a.Id == model.RegionId);

            var exportRegions = new List<RegionExportDto>();

            Action<RegionExportDto, long> fillExportRegions = null;

            fillExportRegions = (parent, parentRegionId) =>
            {
                foreach (var region in regions.Where(a => a.ParentId == parentRegionId))
                {
                    var t = parent.Clone();

                    t.ShortName = region.ShortName;
                    t.Code = region.Code;
                    t.Order = region.Order;
                    t.Id = region.Id;

                    SetExportRegionName(t, region);

                    exportRegions.Add(t);
                    fillExportRegions.Invoke(t, region.Id);
                }
            };

            var rootRegionDto = new RegionExportDto
            {
                ShortName = rootNode.ShortName,
                Code = rootNode.Code,
                Order = rootNode.Order,
            };
            SetExportRegionName(rootRegionDto, rootNode);


            if (rootNode.ParentId > 0)
            {
                var parent = await this.GetAsync(a => a.Id == rootNode.ParentId);
                if (parent is not null)
                {
                    SetExportRegionName(rootRegionDto, parent);
                }
            }

            fillExportRegions(rootRegionDto, rootNode.Id);

            if(model.Ids != null && model.Ids.Any())
            {
                exportRegions = exportRegions.Where(a => model.Ids.Contains(a.Id)).ToList();
            }

            exportRegions.ForEach(a => a.No = exportRegions.IndexOf(a) + 1);

            var exportData = new ExpandoObject();
            exportData.TryAdd("Regions", exportRegions);

            using var reportStream = EPPlusExtensions.GenerateExcelWithTemplate(templateFilePath, exportData);

            return await fileService.UploadAsync(reportStream, $"{rootNode.Name}行政区划_{DateTime.Now:yyyy-MM-dd}.xlsx");
        }
    }
}
