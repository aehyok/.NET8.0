using aehyok.Infrastructure.Dtos;
using aehyok.Infrastructure.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace aehyok.Core.Dtos
{
    public class FileDto : AuditedDtoBase
    {
        /// <summary>
        /// 文件名
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 文件大小（字节）1024字节=1KB
        /// </summary>
        public Int64 Size { get; set; }

        /// <summary>
        /// 所在目录
        /// </summary>
        public string Path { get; set; }

        /// <summary>
        /// 文件Url路径
        /// </summary>
        public string Url { get; set; }

        /// <summary>
        /// 文件类型
        /// </summary>
        public FileType FileType { get; set; }
    }
}
