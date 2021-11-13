/*
 * User: 覃贵锋
 * Date: 2021/10/27
 * Time: 17:25
 * 
 * 
 */
using System;
using System.Drawing;

using System.Windows.Forms;

namespace BTreeEditor.Data.Nodes
{
	/// <summary>
	/// Description of SelectorNode.
	/// </summary>
	[Serializable]
	public class SelectorNode : BTreeNode
	{
		public SelectorNode()
		{
			this.NodeName = "选择";
			this.ClassName = "Selector";
			this.NodeType = BTreeNodeType.SELECTOR;
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
				this.DrawRoundRectangle(g, Pens.MidnightBlue, Brushes.DarkSlateGray, this.Height/2f);
			}
		}
	}
}
