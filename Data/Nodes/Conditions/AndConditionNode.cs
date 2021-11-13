/*
 * User: 覃贵锋
 * Date: 2021/11/10
 * Time: 16:48
 * 
 * 
 */
using System;
using System.Drawing;

namespace BTreeEditor.Data.Nodes.Conditions
{
	/// <summary>
	/// 并集，全部为true则为true
	/// </summary>
	[Serializable]
	public class AndConditionNode : BTreeNode
	{
		public AndConditionNode()
		{
			this.NodeName = "并集";
			this.ClassName = "Condition_And";
			this.NodeType = BTreeNodeType.AND_CONDITION;
			this.AcceptAction = false;
			this.AcceptComposite = false;
			this.AcceptCondition = true;
			this.AcceptDecoration = false;
			this.AcceptDeleted = true;
		}
		
		public override string CanExportCheck()
		{
			if(this.NodeCount == 0)
				return "未设置子节点";
			if(this.NodeCount < 2)
				return "子节点数量至少2个";
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
