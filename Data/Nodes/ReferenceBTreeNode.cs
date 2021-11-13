/*
 * User: 覃贵锋
 * Date: 2021/11/12
 * Time: 10:14
 * 
 * 
 */
using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Design;
using BTreeEditor.Plugins;

namespace BTreeEditor.Data.Nodes
{
	/// <summary>
	/// 子行为树
	/// </summary>
	[Serializable]
	public class ReferenceBTreeNode : BTreeNode
	{
		[Editor(typeof(FileNameEditor), typeof(UITypeEditor))]
		[DescriptionAttribute("关联子行为树")]
		[CategoryAttribute("行为树")]
		public string btreeFile {set; get;}
		
		public ReferenceBTreeNode()
		{
			this.NodeName = "子行为树";
			this.ClassName = "ReferencedBehavior";
			this.NodeType = BTreeNodeType.REFER_BTREE;
			this.AcceptAction = false;
			this.AcceptComposite = false;
			this.AcceptCondition = false;
			this.AcceptDecoration = false;
			this.AcceptDeleted = true;
		}
		
		public override string CanExportCheck()
		{
			if(string.IsNullOrEmpty(this.btreeFile))
				return "未设置子树路径";
			return base.CanExportCheck();
		}
		
		public override void DrawShape(Graphics g)
		{
			if(this.Selected)
			{
				this.DrawRectangle(g, ACTIVE_DRAW_PEN, ACTIVE_FILL_BRUSH);
			}else
			{
				this.DrawRectangle(g, Pens.MidnightBlue, Brushes.MediumTurquoise);
			}
		}
	}
}
