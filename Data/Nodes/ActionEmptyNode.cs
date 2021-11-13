/*
 * User: 覃贵锋
 * Date: 2021/11/12
 * Time: 10:30
 * 
 * 
 */
using System;
using System.Drawing;

namespace BTreeEditor.Data.Nodes
{
	/// <summary>
	/// Description of ActionEmptyNode.
	/// </summary>
	[Serializable]
	public class ActionEmptyNode : BTreeNode
	{
		public ActionEmptyNode()
		{
			this.NodeName = "空动作";
			this.ClassName = "Action_Empty";
			this.NodeType = BTreeNodeType.ACTION_EMPTY;
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
				this.DrawEightRadiusRectangle(g, ACTIVE_DRAW_PEN, ACTIVE_FILL_BRUSH, 10f);
			}else
			{
				this.DrawEightRadiusRectangle(g, Pens.MidnightBlue, Brushes.SandyBrown, 10f);
			}
		}
	}
}
