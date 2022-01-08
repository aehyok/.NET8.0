using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace aehyok.Core.Data.Model
{
	public class TransmitUserInfo
	{
		/// <summary>
		/// 用户ID
		/// </summary>
		
		public string UserId { get; set; }

		/// <summary>
		/// 岗位ID
		/// </summary>
		
		public string PostId { get; set; }

		/// <summary>
		/// 系统ID
		/// </summary>
		
		public string SystemId { get; set; }


		public TransmitUserInfo(string userId, string postId, string systemId)
		{
			this.UserId = userId;
			this.PostId = postId;
			this.SystemId = systemId;
		}

		public TransmitUserInfo(SinoRequestUser user)
		{
			if (user != null)
			{
				if (user.BaseInfo != null)
				{
					this.UserId = user.BaseInfo.UserId;
				}
				if (user.SinoPost != null)
				{
					this.PostId = user.SinoPost.PostId;
				}
				this.SystemId = user.SystemId;
			}
		}

		public TransmitUserInfo()
		{
		}
	}
}
