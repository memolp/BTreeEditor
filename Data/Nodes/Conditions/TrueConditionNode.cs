/*
 * User: 覃贵锋
 * Date: 2021/11/12
 * Time: 10:31
 * 
 * 
 */
using System;
using System.Drawing;

namespace BTreeEditor.Data.Nodes.Conditions
{
	/// <summary>
	/// Description of TrueConditionNode.
	/// </summary>
	[Serializable]
	public class TrueConditionNode : BTreeNode
	{
		/// <summary>
		/// 条件节点
		/// </summary>
		public TrueConditionNode()
		{
			this.NodeName = "真条件";
			this.ClassName = "Condition_True";
			this.NodeType = BTreeNodeType.TRUE_CONDITION;
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
