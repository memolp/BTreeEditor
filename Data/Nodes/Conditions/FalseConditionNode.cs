/*
 * User: 覃贵锋
 * Date: 2021/11/12
 * Time: 10:32
 * 
 * 
 */
using System;
using System.Drawing;

namespace BTreeEditor.Data.Nodes.Conditions
{
	/// <summary>
	/// Description of FalseConditionNode.
	/// </summary>
	[Serializable]
	public class FalseConditionNode : BTreeNode
	{
		/// <summary>
		/// 条件节点
		/// </summary>
		public FalseConditionNode()
		{
			this.NodeName = "假条件";
			this.ClassName = "Condition_False";
			this.NodeType = BTreeNodeType.FALSE_CONDITION;
			this.AcceptAction = false;
			this.AcceptComposite = false;
			this.AcceptCondition = false;
			this.AcceptDecoration = false;
			this.AcceptDeleted = true;
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
