/*
 * User: 覃贵锋
 * Date: 2021/11/4
 * Time: 15:49
 * 
 * 
 */
using System;

namespace BTreeEditor.Data
{
	/// <summary>
	/// 参数数据定义ParamData.
	/// 支持序列化存储
	/// </summary>
	[Serializable]
	public class ParamData
	{
		/// <summary>
		/// 参数名
		/// </summary>
		public string paramName {set; get;}
		/// <summary>
		/// 参数值
		/// </summary>
		public object paramValue {set; get;}
		/// <summary>
		/// 参数描述
		/// </summary>
		public string paramDesc {set; get;}
		/// <summary>
		/// Tips
		/// </summary>
		public string toolTips {set; get;}
		/// <summary>
		/// 参数的类型
		/// </summary>
		public EnumParamType paramType {set; get;}
		/// <summary>
		/// 进行参数数据克隆
		/// 注意: paramValue 只能是基础数据类型，如果是复杂类型这里并没有实现递归拷贝。
		/// </summary>
		/// <returns></returns>
		public ParamData Clone()
		{
			ParamData data = new ParamData();
			data.paramDesc = this.paramDesc;
			data.paramValue = this.paramValue;
			data.paramName = this.paramName;
			data.paramType = this.paramType;
			data.toolTips = this.toolTips;
			return data;
		}
	}
}
