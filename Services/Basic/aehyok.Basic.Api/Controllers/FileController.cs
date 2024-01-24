using aehyok.Basic.Services;
using aehyok.Core.Domains;
using aehyok.Core.Dtos;
using aehyok.Core.Services;
using aehyok.EntityFrameworkCore.Repository;
using aehyok.Infrastructure.Models;
using LinqKit;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using X.PagedList;
using X.PagedList.EF;
using File = aehyok.Core.Domains.File;
namespace aehyok.Basic.Api.Controllers
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
        public async Task<IPagedList<FileDto>> GetListAsync([FromQuery]PagedQueryModelBase model)
        {
            var filter = PredicateBuilder.New<File>(true);
            if(!string.IsNullOrEmpty(model.Keyword))
            {
                filter = filter.And(a => a.Name.Contains(model.Keyword));
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
