using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace aehyok.Core.Data.Model
{
	public enum MDType_RefDownloadMode
	{
		/// <summary>
		/// 一次性下载
		/// </summary>
		[EnumMember]
		FullDownload = 1,
		/// <summary>
		/// 分级下载
		/// </summary>
		[EnumMember]
		LevelDownload = 2,
	}
}
