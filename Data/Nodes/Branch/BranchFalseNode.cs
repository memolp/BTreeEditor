/*
 * User: 覃贵锋
 * Date: 2021/11/10
 * Time: 16:23
 * 
 * 
 */
using System;
using System.Drawing;

namespace BTreeEditor.Data.Nodes.Branch
{
	/// <summary>
	/// 分支节点的False情况下执行的分支
	/// </summary>
	[Serializable]
	public class BranchFalseNode : BTreeNode
	{
		public BranchFalseNode()
		{
			this.NodeName = "假分支";
			this.ClassName = "Branch_False";
			this.NodeType = BTreeNodeType.IFELSE_TRUENODE;
			this.Width = DEFAULT_NODE_HEIGHT * 2;
			this.AcceptAction = true;
			this.AcceptComposite = true;
			this.AcceptCondition = true;
			this.AcceptDecoration = false;
			this.AcceptDeleted = false;
		}
		
		public override bool AddChild(BTreeNode node)
		{
			if(this.NodeCount <= 0)
			{
				this.AcceptAction = false;
				this.AcceptComposite = false;
				this.AcceptCondition = false;
				return base.AddChild(node);
			}
			return false;
		}
		
		public override bool RemoveChild(BTreeNode node)
		{
			if(base.RemoveChild(node))
			{
				this.AcceptAction = true;
				this.AcceptComposite = true;
				this.AcceptCondition = true;
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
				this.DrawEllipse(g, Pens.MidnightBlue, Brushes.SlateGray);
			}
		}
	}
}
