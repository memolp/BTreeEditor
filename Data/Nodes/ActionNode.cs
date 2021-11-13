/*
 * User: 覃贵锋
 * Date: 2021/10/27
 * Time: 16:45
 * 
 * 
 */
using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Design;
using System.Windows.Forms;
using System.Windows.Forms.Design;
using BTreeEditor.Data;
using BTreeEditor.Plugins;

namespace BTreeEditor.Data.Nodes
{
	/// <summary>
	/// 行为节点
	/// </summary>
	[Serializable]
	public class ActionNode : BTreeNode
	{
		/// <summary>
		/// 设置条件执行的方法名
		/// </summary>
		[TypeConverter(typeof(ActionConverter))]
		[DescriptionAttribute("条件检查方法名")]
		[CategoryAttribute("方法")]
		public string Method {set; get;}
		/// <summary>
		/// 当前方法的参数
		/// </summary>
		[TypeConverter(typeof(ArgumentObjectConverter))]
		[DescriptionAttribute("执行检测方法的参数列表")]
		[CategoryAttribute("参数")]
		public ArgumentObject Argument {set; get;}

		
		public ActionNode()
		{
			this.NodeName = "动作";
			this.ClassName = "Action";
			this.NodeType = BTreeNodeType.ACTION;
			this.AcceptAction = false;
			this.AcceptComposite = false;
			this.AcceptCondition = false;
			this.AcceptDecoration = true;
			this.AcceptDeleted = true;
		}
		
		public override bool AddChild(BTreeNode node)
		{
			// 行为增加前置和中断条件都只运行添加一个
			foreach (BTreeNode element in Nodes) 
			{
				if(element.NodeType == node.NodeType)
					return false;
			}
			return base.AddChild(node);
		}
		
		public override string CanExportCheck()
		{
			if(string.IsNullOrEmpty(this.Method))
				return "没有设置动作执行方法";
			return base.CanExportCheck();
		}
		
		/// <summary>
		/// 当前节点的属性发现改变后调用
		/// </summary>
		/// <param name="attrName"></param>
		/// <param name="value"></param>
		public override void ChangedPropertyValue(string attrName, object value)
		{
			// 只关注方法的改变
			if(attrName.Equals("Method"))
			{
				this.Method = (string)value;
				MethodData data = BTreeWorkspace.GetActionWithName(this.Method);
				if(data != null)
					this.Argument = new ArgumentObject(data.arguments);
			}
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
