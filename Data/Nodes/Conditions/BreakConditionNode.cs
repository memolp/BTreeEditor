/*
 * User: 覃贵锋
 * Date: 2021/11/11
 * Time: 9:29
 * 
 * 
 */
using System;
using System.Drawing;

namespace BTreeEditor.Data.Nodes.Conditions
{
	/// <summary>
	/// Description of BreakConditionNode.
	/// </summary>
	[Serializable]
	public class BreakConditionNode : BTreeNode
	{
		/// <summary>
		/// 前置条件节点
		/// </summary>
		public BreakConditionNode()
		{
			this.NodeName = "中断条件";
			this.ClassName = "Break_Condition";
			this.NodeType = BTreeNodeType.BREAK_CONDITION;
			this.AcceptAction = false;
			this.AcceptComposite = false;
			this.AcceptCondition = true;
			this.AcceptDecoration = false;
			this.AcceptDeleted = true;
		}
		
		public override bool AddChild(BTreeNode node)
		{
			if(this.NodeCount <= 0)
			{
				this.AcceptAction = false;
				return base.AddChild(node);
			}
			return false;
		}
		
		public override bool RemoveChild(BTreeNode node)
		{
			if(base.RemoveChild(node))
			{
				this.AcceptAction = true;
				return true;
			}
			return false;
		}
		
		public override string CanExportCheck()
		{
			if(this.NodeCount == 0)
				return "未设置子节点";
			return base.CanExportCheck();
		}

		public override void DrawShape(Graphics g)
		{
			if(this.Selected)
			{
				this.DrawRoundRectangle(g, ACTIVE_DRAW_PEN, ACTIVE_FILL_BRUSH, 10f);
			}else
			{
				this.DrawRoundRectangle(g, Pens.MidnightBlue, Brushes.MediumOrchid, 10f);
			}
		}
	}
}
