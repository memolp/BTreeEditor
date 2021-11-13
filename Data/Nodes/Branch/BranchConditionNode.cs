/*
 * User: 覃贵锋
 * Date: 2021/11/10
 * Time: 16:20
 * 
 * 
 */
using System;
using System.Drawing;

namespace BTreeEditor.Data.Nodes.Branch
{
	/// <summary>
	/// 分支节点的条件节点
	/// </summary>
	[Serializable]
	public class BranchConditionNode : BTreeNode
	{
		public BranchConditionNode()
		{
			this.NodeName = "条件";
			this.ClassName = "Branch_Condition";
			this.NodeType = BTreeNodeType.IFELSE_CONDITION;
			this.Width = DEFAULT_NODE_HEIGHT * 2;
			this.Height = DEFAULT_NODE_HEIGHT;
			this.AcceptAction = false;
			this.AcceptComposite = false;
			this.AcceptCondition = true;
			this.AcceptDecoration = false;
			this.AcceptDeleted = false;
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
				this.DrawEllipse(g, ACTIVE_DRAW_PEN, ACTIVE_FILL_BRUSH);
			}else
			{
				this.DrawEllipse(g, Pens.MidnightBlue, Brushes.MediumOrchid);
			}
		}
	}
}
