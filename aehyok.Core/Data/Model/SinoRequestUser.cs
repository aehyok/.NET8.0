using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace aehyok.Core.Data.Model
{
	public class SinoRequestUser
	{
		/// <summary>
		/// 用户基本信息
		/// </summary>
		public SinoUserBaseInfo BaseInfo { get; set; }

        /// <summary>
        /// 岗位基本信息
        /// </summary>
        public SinoPost SinoPost { get; set; }

        /// <summary>
        /// 用户当前所在的系统Id
        /// </summary>
        public string SystemId { get; set; }
	}
}
