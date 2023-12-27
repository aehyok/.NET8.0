using aehyok.Basic.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace aehyok.Basic.Api.Controllers
{
    public class FileController(IFileService fileService, ILogger<FileController> logger) : BasicControllerBase
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
