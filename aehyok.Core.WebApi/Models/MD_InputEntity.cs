using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace aehyok.Core.WebApi.Models
{
	public class MD_InputEntity
	{
		/// <summary>
		/// 录入模型名称
		/// </summary>
		
		public string InputModelName { get; set; }

		/// <summary>
		/// 录入模型实体数据字典
		/// </summary>
		
		public Dictionary<string, string> InputData { get; set; }

		/// <summary>
		/// 是否为新数据
		/// </summary>
		
		public bool IsNewData { get; set; }

		/// <summary>
		/// 是否为新流程
		/// </summary>
		
		public bool IsNewFlow { get; set; }

		/// <summary>
		/// 子模型实体数据列表
		/// </summary>
		
		public Dictionary<string, string> ChildInputData { get; set; }

		public MD_InputEntity() { }
		public MD_InputEntity(string inputModelName)
		{
			InputModelName = inputModelName;
			InputData = new Dictionary<string, string>();
		}

		/// <summary>
		/// 深复制该对象
		/// </summary>
		/// <returns></returns>
		public MD_InputEntity DeepClone()
		{
			MD_InputEntity ret = new MD_InputEntity();
			ret.InputModelName = this.InputModelName;
			ret.IsNewData = this.IsNewData;
			ret.IsNewFlow = this.IsNewFlow;
			if (this.ChildInputData != null)
			{
				Dictionary<string, string> cInputData = new Dictionary<string, string>();
				foreach (var input in this.ChildInputData)
				{
					cInputData.Add(input.Key, input.Value);
				}
				ret.ChildInputData = cInputData;
			}

			if (this.InputData != null)
			{
				Dictionary<string, string> inputData = new Dictionary<string, string>();
				foreach (var input in this.InputData)
				{
					inputData.Add(input.Key, input.Value);
				}
				ret.InputData = inputData;
			}

			return ret;
		}
	}
}
