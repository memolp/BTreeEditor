/*
 * User: 覃贵锋
 * Date: 2021/10/27
 * Time: 17:24
 * 
 * 
 */
using System;
using System.Drawing;
using System.Windows.Forms;
using BTreeEditor.Data.Nodes.Branch;

namespace BTreeEditor.Data.Nodes
{
	/// <summary>
	/// Description of IfElseNode.
	/// </summary>
	[Serializable]
	public class IfElseNode : BTreeNode
	{
		public BranchConditionNode ConditionNode {set;get;}
		public BranchTrueNode TrueNode {set; get;}
		public BranchFalseNode FalseNode {set;get;}
		
		public IfElseNode()
		{
			this.NodeName = "分支选择";
			this.ClassName = "IfElse";
			this.NodeType = BTreeNodeType.IFELSE;
			this.AcceptAction = false;
			this.AcceptComposite = false;
			this.AcceptCondition = false;
			this.AcceptDecoration = false;
			this.AcceptDeleted = true;
			// 分支节点默认创建好条件-真假分支。
			this.ConditionNode = new BranchConditionNode();
			this.TrueNode = new BranchTrueNode();
			this.FalseNode = new BranchFalseNode();
			this.AddChild(this.ConditionNode);
			this.AddChild(this.TrueNode);
			this.AddChild(this.FalseNode);
		}
		
		public override void DrawShape(Graphics g)
		{
			if(this.Selected)
			{
				this.DrawDiamond(g, ACTIVE_DRAW_PEN, ACTIVE_FILL_BRUSH);
			}else
			{
				this.DrawDiamond(g, Pens.MidnightBlue, Brushes.RoyalBlue);
			}
		}
	}
}
