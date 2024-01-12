using aehyok.Infrastructure.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace aehyok.Infrastructure.Options
{
    /// <summary>
    /// 配置文件中的存储配置
    /// </summary>
    public class StorageOptions : IOptions
    {
        /// <summary>
        /// 节点名称
        /// </summary>
        public string SectionName => "Storage";

        /// <summary>
        /// 本地存储保存路径
        /// </summary>
        public string Path { get; set; }

        /// <summary>
        /// 临时文件存储路径
        /// </summary>
        public string TempPath { get; set; }

        /// <summary>
        /// 存储类型
        /// </summary>
        public FileStorageType Type { get; set; } = FileStorageType.Local;
    }
}
