using System;
using System.Collections.Generic;

namespace aehyok.Core.EntityFrameCore.MySql.Models
{
    public partial class MdParameter
    {
        /// <summary>
        /// GUID
        /// </summary>
        public string Id { get; set; } = null!;
        /// <summary>
        /// 参数名称
        /// </summary>
        public string? Pname { get; set; }
        /// <summary>
        /// 参数值
        /// </summary>
        public string? Pdata { get; set; }
    }
}
