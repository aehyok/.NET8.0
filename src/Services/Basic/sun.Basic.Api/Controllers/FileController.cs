using sun.Basic.Services;
using sun.Core.Domains;
using sun.Core.Dtos;
using sun.Core.Dtos.Query;
using sun.Core.Services;
using sun.EntityFrameworkCore.Repository;
using sun.Infrastructure.Models;
using LinqKit;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using X.PagedList;
using X.PagedList.EF;
using File = sun.Core.Domains.File;
namespace sun.Basic.Api.Controllers
{
    /// <summary>
    /// 文件管理
    /// </summary>
    /// <param name="fileService"></param>
    public class FileController(IFileService fileService) : BasicControllerBase
    {
        /// <summary>
        /// 文件列表
        /// </summary>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        [HttpGet]
        public async Task<IPagedList<FileDto>> GetListAsync([FromQuery]FileQueryDto model)
        {
            var filter = PredicateBuilder.New<File>(true);
            if(!string.IsNullOrEmpty(model.Keyword))
            {
                filter = filter.And(a => a.Name.Contains(model.Keyword));
            }

            if(model.FileType > 0)
            {
                filter = filter.And(a => a.Type == model.FileType);
            }

            var list =await fileService.GetPagedListAsync<FileDto>(filter, model.Page, model.Limit);   //.ToPagedListAsync<FileDto>(model.Page, model.Limit);

            return list;
        }

        /// <summary>
        /// 上传文件
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public async Task<File> PostAsync(IFormFile file)
        {
            using var stream = file.OpenReadStream();
            return await fileService.UploadAsync(stream, file.FileName);
        }
    }
}
