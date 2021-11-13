/*
 * User: 覃贵锋
 * Date: 2021/10/27
 * Time: 17:23
 * 
 * 
 */
using System;
using System.ComponentModel;
using System.Drawing;
using System.Collections.Generic;
using System.Drawing.Design;
using System.Windows.Forms;
using System.Windows.Forms.Design;
using BTreeEditor.Data;
using BTreeEditor.Plugins;

namespace BTreeEditor.Data.Nodes
{
	/// <summary>
	/// Description of ConditionNode.
	/// </summary>
	[Serializable]
	public class ConditionNode : BTreeNode
	{
		/// <summary>
		/// 设置条件执行的方法名
		/// </summary>
		[TypeConverter(typeof(ConditionConverter))]
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

		/// <summary>
		/// 条件节点
		/// </summary>
		public ConditionNode()
		{
			this.NodeName = "条件";
			this.ClassName = "Condition";
			this.NodeType = BTreeNodeType.CONDITION;
			this.AcceptAction = false;
			this.AcceptComposite = false;
			this.AcceptCondition = false;
			this.AcceptDecoration = false;
			this.AcceptDeleted = true;
		}
		
		public override string CanExportCheck()
		{
			if(string.IsNullOrEmpty(this.Method))
				return "没有设置条件检查方法";
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
				MethodData data = BTreeWorkspace.GetConditionWithName(this.Method);
				if(data != null)
				{
					this.Argument = new ArgumentObject(data.arguments);
				}
			}
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
