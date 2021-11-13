/*
 * User: 覃贵锋
 * Date: 2021/10/27
 * Time: 17:26
 * 
 * 
 */
using System;
using System.Drawing;
using System.Collections.Generic;
using System.Windows.Forms;

namespace BTreeEditor.Data.Nodes
{
	/// <summary>
	/// Description of SequenceNode.
	/// </summary>
	[Serializable]
	public class SequenceNode : BTreeNode
	{
		
		public SequenceNode()
		{
			this.NodeName = "序列";
			this.ClassName = "Sequence";
			this.NodeType = BTreeNodeType.SEQUENCE;
			this.AcceptAction = true;
			this.AcceptComposite = true;
			this.AcceptCondition = true;
			this.AcceptDeleted = true;
			this.AcceptDecoration = false;
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
				this.DrawRoundRectangle(g, ACTIVE_DRAW_PEN, ACTIVE_FILL_BRUSH, this.Height/2f);
			}else
			{
				this.DrawRoundRectangle(g, Pens.MidnightBlue, Brushes.DarkSlateBlue, this.Height/2f);
			}
		}
	}
}
