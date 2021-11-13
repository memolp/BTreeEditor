/*
 * User: 覃贵锋
 * Date: 2021/10/27
 * Time: 17:24
 * 
 * 
 */
using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace BTreeEditor.Data.Nodes
{
	/// <summary>
	/// Description of LoopNode.
	/// </summary>
	[Serializable]
	public class LoopNode : BTreeNode
	{
		/// <summary>
		/// 循环次数
		/// </summary>
		[DescriptionAttribute("循环次数")]
		[CategoryAttribute("参数")]
		public int LoopCount {set; get;}
		
		public LoopNode()
		{
			this.NodeName = "循环计数";
			this.ClassName = "DecoratorLoop";
			this.NodeType = BTreeNodeType.LOOP;
			this.AcceptAction = true;
			this.AcceptComposite = true;
			this.AcceptCondition = true;
			this.AcceptDeleted = true;
			this.AcceptDecoration = false;
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
				g.FillEllipse(ACTIVE_FILL_BRUSH, this.X, this.Y, this.Width, this.Height);
				g.DrawEllipse(ACTIVE_DRAW_PEN, this.X, this.Y, this.Width, this.Height);
			}else
			{
				g.FillEllipse(Brushes.Teal, this.X, this.Y, this.Width, this.Height);
				g.DrawEllipse(Pens.MidnightBlue, this.X, this.Y, this.Width, this.Height);
			}
		}
	}
}
