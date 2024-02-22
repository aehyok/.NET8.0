using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Renci.SshNet;
using System.Dynamic;
using ConnectionInfo = Renci.SshNet.ConnectionInfo;

namespace sun.Basic.Api.Controllers
{
    /// <summary>
    /// 系统服务
    /// </summary>
    public class SystemController(ILogger<SystemController> logger, IConfiguration configuration) : BasicControllerBase
    {

        /// <summary>
        /// 获取配置json字符串
        /// </summary>
        /// <returns></returns>
        [HttpGet("config")]
        public dynamic GetConfigJson()
        {
            dynamic result = new ExpandoObject();
            
            var connString = configuration.GetValue<string>("Host");

            var test = configuration.GetValue<string>("Test");
            result.connString = connString;
            result.test = test;
            return result;
        }

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
                    serviceFiles = Directory.GetFiles(systemdPath).Where(item => item.Contains("sun")).ToArray();
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

        /// <summary>
        /// 测试ssh
        /// </summary>
        /// <returns></returns> 
        [HttpPost]
        public  dynamic PostAsync()
        {
            var result = string.Empty;
            try
            {
                string host = "121.37.222.1";
                string userName = "root";
                string password = "sunlight2010!";

                using(var client = new SshClient(host, userName, password))
                {
                    client.Connect();
                    var output = client.RunCommand("docker ps");
                    return output.Result;
                }
            }
            catch(Exception ex)
            {

            }
            return result ;
        }
    }
}
