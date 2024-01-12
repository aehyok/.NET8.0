using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace aehyok.Infrastructure.Enums
{
    /// <summary>
    /// 文件存储类型
    /// </summary>
    public enum FileStorageType
    {
        Local = 0,  //本地存储
        Aliyun = 1,
        Huawei = 2,
        Tencent = 3,
        Aws = 4
    }

    /// <summary>
    /// 文件类型
    /// </summary>
    public enum FileType
    {
        其他 = 0,
        图片,
        文档,
        视频,
        音频
    }

    public enum FileState
    {
        正常 = 0,
        转码中 = 1,
        已禁用 = 2,
    }
}
