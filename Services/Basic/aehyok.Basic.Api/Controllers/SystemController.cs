using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace aehyok.Basic.Api.Controllers
{
    /// <summary>
    /// 系统服务
    /// </summary>
    public class SystemController(ILogger<SystemController> logger) : BasicControllerBase
    {
        /// <summary>
        /// 获取系统服务列表
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public dynamic GetListAsync()
        {
            string systemdPath = "/usr/lib/systemd/system";

            string[] serviceFiles = [];
            try
            {
                Console.WriteLine($"An error occurred: {systemdPath}");
                logger.LogInformation($"An error occurred: {systemdPath}");
                // 检查目录是否存在
                if (Directory.Exists(systemdPath))
                {
                    // 获取目录下的所有文件
                    serviceFiles = Directory.GetFiles(systemdPath).Where(item => item.Contains("aehyok")).ToArray();
                    logger.LogInformation($"{serviceFiles.Length}");
                    Console.WriteLine($"An error occurred: {serviceFiles}");
                    logger.LogInformation($"An error occurred: {serviceFiles}");

                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
                logger.LogInformation($"An error occurred: {ex.Message}");
            }
            return serviceFiles;
        }
    }
}
