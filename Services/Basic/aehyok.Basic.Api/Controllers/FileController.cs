using aehyok.Basic.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace aehyok.Basic.Api.Controllers
{
    /// <summary>
    /// 文件管理
    /// </summary>
    /// <param name="fileService"></param>
    public class FileController(IFileService fileService) : BasicControllerBase
    {
        /// <summary>
        /// 上传文件
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public async Task<Domains.File> PostAsync(IFormFile file)
        {
            using var stream = file.OpenReadStream();
            return await fileService.UploadAsync(stream, file.FileName);
        }
    }
}
