using System;
using System.Collections.Generic;
using System.Text;

namespace aehyok.Lib.MetaData.Common
{
    /// <summary>
    /// 用户TOKEN验证实体
    /// </summary>
    /// 
    public class UserToken
    {
        /// <summary>
        /// GUID
        /// </summary>
        public string ID { get; set; }
        /// <summary>
        /// 用户名
        /// </summary>
        public string UserName { get; set; }

        /// <summary>
        /// 单位代码 CODE
        /// </summary>
        public string Dwdm { get; set; }

        /// <summary>
        /// TOKEN
        /// </summary>
        public string Token { get; set; }


    }
}
