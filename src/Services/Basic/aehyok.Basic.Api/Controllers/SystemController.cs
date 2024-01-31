using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Renci.SshNet;
using ConnectionInfo = Renci.SshNet.ConnectionInfo;

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
                string privateKeyFilePath = @"C:\Users\Administrator\.ssh\id_rsa";

                //var pk = new PrivateKeyFile(yourkey);

                //var keyFiles = new[] { privateKeyFilePath };

                //var methods = new List<AuthenticationMethod>();
                //methods.Add(new PrivateKeyAuthenticationMethod(userName, keyFiles));

                //var connectionInfo = new ConnectionInfo(host, userName, new PrivateKeyAuthenticationMethod(userName, methods.ToArray()));
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
