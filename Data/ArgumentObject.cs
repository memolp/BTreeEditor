/*
 * User: 覃贵锋
 * Date: 2021/11/5
 * Time: 11:04
 * 
 * 
 */
using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace BTreeEditor.Data
{
	/// <summary>
	/// 动态可扩展的参数对象
	/// </summary>
	[Serializable]
	public class ArgumentObject
	{
		/// <summary>
		/// 参数信息 
		/// </summary>
		private readonly List<ParamData> _arguments = new List<ParamData>();
		/// <summary>
		/// 提供设置和访问数据参数列表
		/// </summary>
		[Browsable(false)]
		public List<ParamData> Arguments { get {return _arguments;}}
		/// <summary>
		/// 创建动态参数对象
		/// </summary>
		/// <param name="args"></param>
		public ArgumentObject(List<ParamData> args)
		{
			foreach (ParamData element in args) 
			{
				_arguments.Add(element.Clone());
			}
		}
	}
}
