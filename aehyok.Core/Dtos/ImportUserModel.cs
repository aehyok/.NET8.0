using aehyok.Infrastructure.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace aehyok.Core.Dtos
{
    public class ImportUserModel
    {
        /// <summary>
        /// 用户类型
        /// </summary>
        public UserType UserType { get; set; }

        /// <summary>
        /// 导入文件 Url
        /// </summary>
        public string Url { get; set; }
    }
}
