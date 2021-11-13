/*
 * User: 覃贵锋
 * Date: 2021/11/5
 * Time: 10:19
 * 
 * 
 */
using System;
using System.ComponentModel;
using System.Collections.Generic;
using BTreeEditor.Data;

namespace BTreeEditor.Plugins
{
	/// <summary>
	/// Description of ConditionConverter.
	/// </summary>
	public class ConditionConverter : StringConverter
	{
		/// <summary>
		/// 表示此对象支持可以从列表中选取的一组标准值
		/// </summary>
		/// <param name="context"></param>
		/// <returns></returns>
		public override bool GetStandardValuesSupported(ITypeDescriptorContext context)
        {
            return true;
        }
		/// <summary>
		/// 覆盖 GetStandardValues 方法并返回填充了标准值的 StandardValuesCollection。
		/// 创建 StandardValuesCollection 的方法之一是在构造函数中提供一个值数组
		/// </summary>
		/// <param name="context"></param>
		/// <returns></returns>
        public override StandardValuesCollection GetStandardValues(ITypeDescriptorContext context)
        {
        	List<MethodData> conditions = BTreeWorkspace.CurrentWorkspaceData.Conditions;
        	string[] conds = new string[conditions.Count];
        	for(int i=0;i<conditions.Count; i++)
        	{
        		conds[i] = conditions[i].methodName;
        	}
        	return new StandardValuesCollection(conds);
        }
		/// <summary>
		/// 如果希望用户能够键入下拉列表中没有包含的值，请覆盖 GetStandardValuesExclusive 方法并返回 false。
		/// 这从根本上将下拉列表样式变成了组合框样式。
		/// </summary>
		/// <param name="context"></param>
		/// <returns></returns>
        public override bool GetStandardValuesExclusive(ITypeDescriptorContext context)
        {
            return true;
        }
	}
}
