/*
 * User: 覃贵锋
 * Date: 2021/10/27
 * Time: 17:45
 * 
 * 
 */
using System;
using System.Drawing;
using System.Windows.Forms;

namespace BTreeEditor.Data.Nodes
{
	/// <summary>
	/// Description of RootNode.
	/// </summary>
	[Serializable]
	public class RootNode : BTreeNode
	{
		public RootNode()
		{
			this.NodeName = "根节点";
			this.ClassName = "Root";
			this.NodeType = BTreeNodeType.ROOT;
			this.AcceptAction = false;
			this.AcceptCondition = false;
			this.AcceptComposite = true;
			this.AcceptDecoration = false;
			this.AcceptDeleted = false;
		}
		
		public override bool AddChild(BTreeNode node)
		{
			if(this.NodeCount <= 0)
			{
				this.AcceptComposite = false;
				return base.AddChild(node);
			}
			return false;
		}
		
		public override bool RemoveChild(BTreeNode node)
		{
			if(base.RemoveChild(node))
			{
				this.AcceptComposite = true;
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
				g.FillEllipse(ACTIVE_FILL_BRUSH, this.X, this.Y, this.Width, this.Height);
				g.DrawEllipse(ACTIVE_DRAW_PEN, this.X, this.Y, this.Width, this.Height);
			}else
			{
				g.FillEllipse(Brushes.DeepPink, this.X, this.Y, this.Width, this.Height);
				g.DrawEllipse(Pens.MidnightBlue, this.X, this.Y, this.Width, this.Height);
			}
		}
	}
}
