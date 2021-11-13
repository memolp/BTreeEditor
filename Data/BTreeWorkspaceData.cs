/*
 * User: 覃贵锋
 * Date: 2021/11/4
 * Time: 15:40
 * 
 * 行为树工作空间的配置数据，用于配置行为树条件节点和行动节点可以进行选择的操作。以及记录当前工程的一些信息。
 */
using System;
using System.Collections.Generic;


namespace BTreeEditor.Data
{
	/// <summary>
	/// 行为树工作空间的配置数据
	/// </summary>
	[Serializable]
	public class BTreeWorkspaceData
	{
		/// <summary>
		/// 所有当前工程行为树存放目录
		/// </summary>
		public const string WorkRootName = "behavior";
		/// <summary>
		/// 所有行为树导出存放的目录
		/// </summary>
		public const string ExportRootName = "exported";
		/// <summary>
		/// 工程的完整路径
		/// </summary>
		public string WorkPath {set; get;}
		/// <summary>
		/// 记录当前工程可用的条件列表
		/// </summary>
		public List<MethodData> Conditions {set; get;}
		/// <summary>
		/// 记录当前工程可用的动作列表
		/// </summary>
		public List<MethodData> Actions {set; get;}
		/// <summary>
		/// 当前工作区的行为树数据列表-只记录文件名
		/// </summary>
		public List<string> btreeDataList {set; get;}
		/// <summary>
		/// 行为树工作区
		/// </summary>
		public BTreeWorkspaceData()
		{
			this.Conditions = new List<MethodData>();
			this.Actions = new List<MethodData>();
			this.btreeDataList = new List<string>();
		}

		
	}
}
