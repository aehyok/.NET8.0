using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace aehyok.Core.Data.Model
{
    public class SinoUserBaseInfo
    {
        /// <summary>
        /// 用户类型    值为：1:“海关用户”　或	2:“缉私局用户”
        /// </summary>
        public int UserType = 2;

        /// <summary>
        /// 用户登录名
        /// </summary>
        
        virtual public string LoginName { get; set; }

        /// <summary>
        /// 用户ID
        /// </summary>		
        
        virtual public string UserId { get; set; }

        /// <summary>
        /// 用户名称
        /// </summary>
        
        virtual public string UserName { get; set; }

        /// <summary>
        /// 用户的GUID
        /// </summary>
        
        virtual public string UserGuid { get; set; }

        /// <summary>
        /// 用户的海关关号
        /// </summary>
        
        virtual public string UserCustomsNo { get; set; }

        /// <summary>
        /// 单位ID
        /// </summary>
        
        virtual public string DwId { get; set; }

        /// <summary>
        /// 单位名称
        /// </summary>
        
        virtual public string DwName { get; set; }

        /// <summary>
        /// 单位代码
        /// </summary>
        
        virtual public string DwCode { get; set; }

        /// <summary>
        /// 单位的GUID
        /// </summary>
        
        virtual public string DwGuid { get; set; }

        /// <summary>
        /// 安全级别
        /// </summary>
        
        public int SecretLevel = 0;

        /// <summary>
        /// 默认岗位ID
        /// </summary>
        
        virtual public string DefaultPostId { get; set; }

        /// <summary>
        /// 性别
        /// </summary>
        
        virtual public string Sex { get; set; }

        ///// <summary>
        ///// 用户具有岗位集
        ///// </summary>
        
        //virtual public List<SinoPost> Posts { get; set; }

        ///// <summary>
        ///// 用户默认岗位
        ///// </summary>
        
        //virtual public SinoPost DefaultPost { get; set; }

        ///// <summary>
        ///// 用户当前使用的岗位
        ///// </summary>
        
        //virtual public SinoPost CurrentPost { get; set; }
    }
}
