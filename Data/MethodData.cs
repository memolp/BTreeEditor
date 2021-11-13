/*
 * User: 覃贵锋
 * Date: 2021/11/4
 * Time: 15:44
 * 
 * 
 */
using System;
using System.Collections.Generic;

namespace BTreeEditor.Data
{
	/// <summary>
	/// Description of MethodData.
	/// </summary>
	[Serializable]
	public class MethodData
	{
		/// <summary>
		/// 方法名称
		/// </summary>
		public string methodName {set; get;}
		/// <summary>
		/// 方法的描述
		/// </summary>
		public string methodDesc {set; get;}
		/// <summary>
		/// tips文本
		/// </summary>
		public string toolTips {set; get;}
		/// <summary>
		/// 方法的参数
		/// </summary>
		public List<ParamData> arguments {set; get;}
		
		public MethodData()
		{
			this.arguments = new List<ParamData>();
		}
	}
}
