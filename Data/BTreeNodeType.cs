/*
 * User: 覃贵锋
 * Date: 2021/11/9
 * Time: 11:31
 * 
 * 
 */
using System;

namespace BTreeEditor.Data
{
	/// <summary>
	/// 行为树节点类型定义
	/// </summary>
	public enum BTreeNodeType
	{
		ERROR,
		
		// ---- 动作行为 ----
		/// <summary>
		/// 行为动作节点
		/// </summary>
		ACTION,
		/// <summary>
		/// 空行为节点
		/// </summary>
		ACTION_EMPTY,
		
		// --- 条件 ----
		/// <summary>
		/// 添加节点
		/// </summary>
		CONDITION = 10,
		/// <summary>
		/// 真条件节点 - 始终为真
		/// </summary>
		TRUE_CONDITION,
		/// <summary>
		/// 假条件 - 始终为假
		/// </summary>
		FALSE_CONDITION,
		/// <summary>
		/// 并条件组-里面包含其他的条件节点（and操作）
		/// </summary>
		AND_CONDITION,
		/// <summary>
		/// 或条件组-里面包含其他的条件节点（or操作）
		/// </summary>
		OR_CONDITION,
		
		// ---- 装饰节点 ----
		/// <summary>
		/// 前置条件 
		/// </summary>
		PRE_CONDITION = 30,
		/// <summary>
		/// 中断条件
		/// </summary>
		BREAK_CONDITION,
		// ---- 组合复合节点 -------
		/// <summary>
		/// 分支节点
		/// </summary>
		IFELSE = 50,
		/// <summary>
		/// 循环次数节点
		/// </summary>
		LOOP,
		/// <summary>
		/// 选择节点
		/// </summary>
		SELECTOR,
		/// <summary>
		/// 顺序节点
		/// </summary>
		SEQUENCE,
		
		// ----- 特殊 -----
		/// <summary>
		/// 根节点
		/// </summary>
		ROOT = 60,
		/// <summary>
		/// 子行为树节点
		/// </summary>
		REFER_BTREE,
		
		// ---- 不参与导出的节点 ---
		/// <summary>
		/// 分支节点的条件节点根
		/// </summary>
		IFELSE_CONDITION = 80,
		/// <summary>
		/// 分支真条件节点根
		/// </summary>
		IFELSE_TRUENODE,
		/// <summary>
		/// 分支假条件节点根
		/// </summary>
		IFELSE_FALSENODE,
	}
}
