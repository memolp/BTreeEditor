/*
 * User: 覃贵锋
 * Date: 2021/11/5
 * Time: 14:53
 * 
 * 
 */
using System;
using BTreeEditor.Data;

namespace BTreeEditor
{
	/// <summary>
	/// 行为树工作空间
	/// </summary>
	public static class BTreeWorkspace
	{
		/// <summary>
		/// 行为树工作空间是数据
		/// </summary>
		public static BTreeWorkspaceData CurrentWorkspaceData {set; get;}
		/// <summary>
		/// 通过方法名获取执行方法
		/// </summary>
		/// <param name="name"></param>
		/// <returns></returns>
		public static MethodData GetActionWithName(string name)
		{
			foreach (MethodData data in BTreeWorkspace.CurrentWorkspaceData.Actions) 
			{
				if(data.methodName == name)
				{
					return data;
				}
			}
			return null;
		}
		/// <summary>
		/// 通过方法名获取条件方法
		/// </summary>
		/// <param name="name"></param>
		/// <returns></returns>
		public static MethodData GetConditionWithName(string name)
		{
			foreach (MethodData node in BTreeWorkspace.CurrentWorkspaceData.Conditions) 
			{
				if(node.methodName == name)
				{
					return node;
				}
			}
			return null;
		}
	}
}
