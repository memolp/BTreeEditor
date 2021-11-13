/*
 * User: 覃贵锋
 * Date: 2021/10/27
 * Time: 16:33
 * 
 * 
 */
using System;
using System.ComponentModel;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using BTreeEditor.Data;
using BTreeEditor.Data.Nodes;
using BTreeEditor.Data.Nodes.Conditions;

namespace BTreeEditor
{
	/// <summary>
	/// 行为树编辑器主模块
	/// </summary>
	public partial class BTreeControl : UserControl
	{
		/// <summary>
		/// 行为节点选中事件
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="target"></param>
		public delegate void OnNodeSelectedEventHandler(object sender, BTreeNode target);
		/// <summary>
		/// 监听行为节点选中事件
		/// </summary>
		public OnNodeSelectedEventHandler OnNodeSelectedEvent;
		/// <summary>
		/// 行为树数据
		/// </summary>
		BTreeVisualData _btreeData = null;
		/// <summary>
		/// 当前选中的节点
		/// </summary>
		BTreeNode _selectedNode = null;
		
		private float _scaleVal = 1.0f;
		/// <summary>
		/// 
		/// </summary>
		public float scaleVal 
		{
			set
			{
				_scaleVal = value;
				this.Invalidate();
			}
			get
			{
				return _scaleVal;
			}
		}
		/// <summary>
		/// 行为树编辑器
		/// </summary>
		public BTreeControl()
		{
			InitializeComponent();
			SetStyle(ControlStyles.SupportsTransparentBackColor, true);
			this.Paint += this.OnPaintEvent;
		}
		/// <summary>
		/// 外部更改大小
		/// </summary>
		/// <param name="size"></param>
		public void OnClientSizeChanged(Size size)
		{
			this.Width = Math.Max(this.Width, size.Width);
			this.Height = Math.Max(this.Height, size.Height);
			this._btreeData.NeedUpdateLayout = true;
			this.Invalidate();
		}
		/// <summary>
		/// 设置行为树是数据
		/// </summary>
		/// <param name="data"></param>
		public void SetBTreeData(BTreeVisualData data)
		{
			_btreeData = data;
			if(_btreeData != null)
			{
				_btreeData.NeedUpdateLayout = true;
				this.Invalidate();
			}
		}
		/// <summary>
		/// 设置获取选中的节点
		/// </summary>
		protected BTreeNode SelectedNode
		{
			get
			{
				return _selectedNode;
			}
			set
			{
				if(_selectedNode != null)
					_selectedNode.Selected = false;
				_selectedNode = value;
				_selectedNode.Selected = true;
				if(OnNodeSelectedEvent != null)
				{
					OnNodeSelectedEvent(this, value);
				}
			}
		}
		/// <summary>
		/// 绘图事件
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		void OnPaintEvent(object sender, PaintEventArgs e)
		{
			// 必须设置行为树数据对象
			if(_btreeData == null) return;
			// 缩放功能不完整，不开放
			//e.Graphics.ScaleTransform(_scaleVal, _scaleVal);
			// 计算整个行为树数据占用的大小
			Size size = _btreeData.CalcGraphBounds(this.Width, this.Height);
			this.Width = Math.Max(this.Width, size.Width);
			this.Height = Math.Max(this.Height, size.Height);
			//e.Graphics.DrawRectangle(Pens.Navy, 0, 0, this.Width - 1, this.Height - 1);
			// 调用绘制
			_btreeData.Draw(e.Graphics);
		}
		
		void RePaintCanvas()
		{
			this.Invalidate();
		}
		
		/// <summary>
		/// 鼠标点击事件
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		void OnMouseClickEvent(object sender, MouseEventArgs e)
		{
			//判空
			if(_btreeData == null) return;
			
			if(e.Button == MouseButtons.Left || e.Button == MouseButtons.Right)
			{
				// 缩放功能不完整
				//BTreeNode node = _btreeData.CurrentNode((int)(e.Location.X / _scaleVal), (int)(e.Location.Y / scaleVal));
				BTreeNode node = _btreeData.CurrentNode(e.Location.X, e.Location.Y);
				if(node != null)
				{
					if(_selectedNode != node)
					{
						this.SelectedNode = node;
						this.RePaintCanvas();
					}
				}else
				{
					return;
				}
			}
			
			if(e.Button == MouseButtons.Right)
			{
				this.ShowContextMenu(PointToScreen(e.Location));
			}
		}
		
		void OnMouseDBClickEvent(object sender, MouseEventArgs e)
		{
			//判空
			if(_btreeData == null) return;
			if(e.Button == MouseButtons.Left)
			{
				// 缩放功能不完整
				//BTreeNode node = _btreeData.CurrentNode((int)(e.Location.X / _scaleVal), (int)(e.Location.Y / scaleVal));
				BTreeNode node = _btreeData.CurrentNode(e.Location.X, e.Location.Y);
				if(node != null )
				{
					bool need_repaint = false;
					// 没有子节点的不折叠
					if(node.NodeCount > 0)
					{
						node.Expanded = !node.Expanded;
						_btreeData.NeedUpdateLayout = true;
						need_repaint = true;
					}
					if(_selectedNode != node)
					{
						this.SelectedNode = node;
						need_repaint = true;
					}
					if(need_repaint)
						this.RePaintCanvas();
				}
			}
		}
		
		
		void OnMenuCreateBTreeNode(BTreeNodeType type)
		{
			if(_btreeData == null)
			{
				MessageBox.Show("请先创建行为树", "错误");
				return;
			}
			
			if(_selectedNode == null)
			{
				MessageBox.Show("未选择节点", "错误");
				return;
			}
			
			BTreeNode node = CreateNodeByType(type);
			if(_btreeData.AddNode(node, _selectedNode))
			{
				this.RePaintCanvas();
			}else
			{
				MessageBox.Show(string.Format("无法将 {0} 节点添加至当前 {1} 节点上。", node.NodeName, _selectedNode.NodeName),"错误");
			}
		}
		
		void OnMenuRemoveNodeEvent()
		{
			if(_btreeData == null)
			{
				MessageBox.Show("请先创建行为树", "错误");
				return;
			}
			
			if(_selectedNode == null)
			{
				MessageBox.Show("未选择节点", "错误");
				return;
			}
			
			if(_btreeData.DelNode(_selectedNode))
			{
				this.RePaintCanvas();
			}
			
		}
		
		void OnMenuMoveUpEvent()
		{
			if(_btreeData == null)
			{
				MessageBox.Show("请先创建行为树", "错误");
				return;
			}
			
			if(_selectedNode == null)
			{
				MessageBox.Show("未选择节点", "错误");
				return;
			}
			
			int idx = _selectedNode.Parent.Nodes.IndexOf(_selectedNode);
			if(idx > 0)
			{
				_selectedNode.Parent.Nodes.Remove(_selectedNode);
				_selectedNode.Parent.Nodes.Insert(idx-1, _selectedNode);
				_btreeData.NeedUpdateLayout = true;
				this.RePaintCanvas();
			}
			
		}
		
		void OnMenuMoveDownEvent()
		{
			if(_btreeData == null)
			{
				MessageBox.Show("请先创建行为树", "错误");
				return;
			}
			
			if(_selectedNode == null)
			{
				MessageBox.Show("未选择节点", "错误");
				return;
			}
			
			int idx = _selectedNode.Parent.Nodes.IndexOf(_selectedNode);
			if(idx < _selectedNode.Parent.NodeCount - 1)
			{
				_selectedNode.Parent.Nodes.Remove(_selectedNode);
				_selectedNode.Parent.Nodes.Insert(idx+1, _selectedNode);
				_btreeData.NeedUpdateLayout = true;
				this.RePaintCanvas();
			}
			
		}
		
		void OnKeyPreDownEvent(object sender, PreviewKeyDownEventArgs e)
		{
			// 安全检查
			if(_btreeData == null || _selectedNode == null) return;
			// 防空
			if(e.Alt == true || e.Control == true || e.Shift == true ) return;
			if(e.KeyCode == Keys.Delete)
			{
				if(_selectedNode.AcceptDeleted)
					this.OnMenuRemoveNodeEvent();
			}
			
	
		}
		/// <summary>
		/// 显示右键菜单
		/// </summary>
		/// <param name="pt"></param>
		void ShowContextMenu(Point pt)
		{
			// 判空
			if(_selectedNode == null) return;
			
			ToolStripDropDownItem item = null;
			ToolStripItem subItem = null;
			
			mContextMenu.Items.Clear();
			mContextMenu.Items.Add(_selectedNode.NodeName);
			if (_selectedNode.AcceptComposite) 
			{
				
				item = (ToolStripDropDownItem)mContextMenu.Items.Add("添加组合节点");
				subItem = item.DropDownItems.Add("顺序节点", null, (object s, EventArgs e)=> this.OnMenuCreateBTreeNode(BTreeNodeType.SEQUENCE));
				subItem.ToolTipText = "按顺序从上到下依次执行，遇到False则返回。";
				
				subItem = item.DropDownItems.Add("选择节点", null, (object s, EventArgs e)=> this.OnMenuCreateBTreeNode(BTreeNodeType.SELECTOR));
				subItem.ToolTipText = "按顺序从上到下依次执行，遇到True则返回。";
				
				subItem = item.DropDownItems.Add("分支节点", null, (object s, EventArgs e)=> this.OnMenuCreateBTreeNode(BTreeNodeType.IFELSE));
				subItem.ToolTipText = "执行条件判断，条件为True走真节点，为False走假节点。";
				
				subItem = item.DropDownItems.Add("计数节点", null, (object s, EventArgs e)=> this.OnMenuCreateBTreeNode(BTreeNodeType.LOOP));
				subItem.ToolTipText = "循环次数，直到次数结束或者执行返回False。";
			}
			if(_selectedNode.AcceptDecoration)
			{
				item = (ToolStripDropDownItem)mContextMenu.Items.Add("添加装饰节点");
				subItem = item.DropDownItems.Add("前置条件", null, (object s, EventArgs e)=> this.OnMenuCreateBTreeNode(BTreeNodeType.PRE_CONDITION));
				subItem.ToolTipText = "前置条件结果为True时才会进行动作执行";
				
				subItem = item.DropDownItems.Add("中断条件", null, (object s, EventArgs e)=> this.OnMenuCreateBTreeNode(BTreeNodeType.BREAK_CONDITION));
				subItem.ToolTipText = "中断条件结果为True时动作将结束";
			}
			if(_selectedNode.AcceptCondition)
			{
				item = (ToolStripDropDownItem)mContextMenu.Items.Add("添加条件节点");
				subItem = item.DropDownItems.Add("条件", null, (object s, EventArgs e)=> this.OnMenuCreateBTreeNode(BTreeNodeType.CONDITION));
				subItem.ToolTipText = "条件节点执行条件判断并返回True或者False。";
				
				subItem = item.DropDownItems.Add("真条件", null, (object s, EventArgs e)=> this.OnMenuCreateBTreeNode(BTreeNodeType.TRUE_CONDITION));
				subItem.ToolTipText = "该条件节点执行永远返回True。";
				
				subItem = item.DropDownItems.Add("假条件", null, (object s, EventArgs e)=> this.OnMenuCreateBTreeNode(BTreeNodeType.FALSE_CONDITION));
				subItem.ToolTipText = "该条件节点执行永远返回False。";
				
				subItem = item.DropDownItems.Add("并集条件", null, (object s, EventArgs e)=> this.OnMenuCreateBTreeNode(BTreeNodeType.AND_CONDITION));
				subItem.ToolTipText = "并集条件，全部条件都为True才为True，否则为False";
				
				subItem = item.DropDownItems.Add("或集条件", null, (object s, EventArgs e)=> this.OnMenuCreateBTreeNode(BTreeNodeType.OR_CONDITION));
				subItem.ToolTipText = "或集条件，所有条件中只要有True则返回True，全部False才返回False";
			}
			
			if(_selectedNode.AcceptAction)
			{
				item = (ToolStripDropDownItem)mContextMenu.Items.Add("添加动作节点");
				subItem = item.DropDownItems.Add("动作节点", null, (object s, EventArgs e)=> this.OnMenuCreateBTreeNode(BTreeNodeType.ACTION));
				subItem.ToolTipText = "动作执行节点，执行后返回执行的状态情况。";
				
				subItem = item.DropDownItems.Add("空动作", null, (object s, EventArgs e)=> this.OnMenuCreateBTreeNode(BTreeNodeType.ACTION_EMPTY));
				subItem.ToolTipText = "空动作执行始终返回成功。";
				
				subItem = item.DropDownItems.Add("子树", null, (object s, EventArgs e)=> this.OnMenuCreateBTreeNode(BTreeNodeType.REFER_BTREE));
				subItem.ToolTipText = "添加子行为树。";
			}
			if(_selectedNode.AcceptDeleted)
			{
				mContextMenu.Items.Add(new ToolStripSeparator());
				mContextMenu.Items.Add("上移节点", null, (object s, EventArgs e)=> this.OnMenuMoveUpEvent());
				mContextMenu.Items.Add("下移节点", null, (object s, EventArgs e)=> this.OnMenuMoveDownEvent());
				mContextMenu.Items.Add(new ToolStripSeparator());
				subItem = mContextMenu.Items.Add("删除节点", null, (object s, EventArgs e)=> this.OnMenuRemoveNodeEvent());
				subItem.ToolTipText = "删除节点及子节点";
			}
			mContextMenu.Show(pt);
		}
		/// <summary>
		/// 根据指定的类型创建节点
		/// </summary>
		/// <param name="type"></param>
		/// <returns></returns>
		internal static BTreeNode CreateNodeByType(BTreeNodeType type)
		{
			switch (type) 
			{
				case BTreeNodeType.ACTION:
					return new ActionNode();
				case BTreeNodeType.ACTION_EMPTY:
					return new ActionEmptyNode();
				case BTreeNodeType.CONDITION:
					return new ConditionNode();
				case BTreeNodeType.IFELSE:
					return new IfElseNode();
				case BTreeNodeType.LOOP:
					return new LoopNode();
				case BTreeNodeType.SELECTOR:
					return new SelectorNode();
				case BTreeNodeType.SEQUENCE:
					return new SequenceNode();
				case BTreeNodeType.AND_CONDITION:
					return new AndConditionNode();
				case BTreeNodeType.OR_CONDITION:
					return new OrConditionNode();
				case BTreeNodeType.PRE_CONDITION:
					return new PreConditionNode();
				case BTreeNodeType.BREAK_CONDITION:
					return new BreakConditionNode();
				case BTreeNodeType.TRUE_CONDITION:
					return new TrueConditionNode();
				case BTreeNodeType.FALSE_CONDITION:
					return new FalseConditionNode();
				case BTreeNodeType.REFER_BTREE:
					return new ReferenceBTreeNode();
				default:
					return new BTreeNode();
			}
		}
	}
}
