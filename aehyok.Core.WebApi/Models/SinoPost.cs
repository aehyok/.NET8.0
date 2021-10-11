using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace aehyok.Core.WebApi.Models
{
    public class SinoPost
    {
        /// <summary>
        /// 此岗位的角色列表
        /// </summary>
        /// <summary>
        /// 是否默认岗位
        /// </summary>

        public bool IsDefaultPost { get; set; }
        /// <summary>
        /// 岗位ID
        /// </summary>

        public string PostId { get; set; }
        /// <summary>
        /// 岗位名称
        /// </summary>

        public string PostName { get; set; }
        /// <summary>
        /// 岗位所在的单位ID
        /// </summary>

        public string PostDwId { get; set; }
        /// <summary>
        /// 岗位权限所在单位代码
        /// </summary>

        public string PostDWDM { get; set; }
        /// <summary>
        /// 岗位所在的单位名称
        /// </summary>

        public string PostDWMC { get; set; }
        /// <summary>
        /// 权限所在单位的GUID
        /// </summary>

        public string PostDWGUID { get; set; }
        /// <summary>
        /// 安全级别
        /// </summary>

        public int SecretLevel { get; set; }
        /// <summary>
        /// 岗位描述
        /// </summary>

        public string PostDescript { get; set; }


        public int DisplayOrder { get; set; }


        public string SystemId { get; set; }

        public SinoPost()
        {
            //
            // TODO: 在此处添加构造函数逻辑
            //
        }

        public SinoPost(string _gwmc, string _gwid, string _gwdwid, string _dwmc, string _dwdm, string _gwms, int _secretLevel, bool _sfmr)
        {
            this.PostName = _gwmc;
            this.PostId = _gwid;
            this.PostDwId = _gwdwid;
            this.PostDWMC = _dwmc;
            this.PostDWDM = _dwdm;
            this.PostDescript = _gwms;
            this.IsDefaultPost = _sfmr;
            this.SecretLevel = _secretLevel;
            this.DisplayOrder = 0;
        }
        //新添加
        public SinoPost(string _gwmc, string _gwid, string _gwdwid, string _dwmc, string _dwdm, string _gwms, int _secretLevel, bool _sfmr, int _order)
        {
            this.PostName = _gwmc;
            this.PostId = _gwid;
            this.PostDwId = _gwdwid;
            this.PostDWMC = _dwmc;
            this.PostDWDM = _dwdm;
            this.PostDescript = _gwms;
            this.IsDefaultPost = _sfmr;
            this.SecretLevel = _secretLevel;
            this.DisplayOrder = (_order == 0) ? 100 : _order;
        }
    }
}
