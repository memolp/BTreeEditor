/*
 * User: 覃贵锋
 * Date: 2021/10/27
 * Time: 17:44
 * 
 * 
 */
using System;
using System.Drawing;
using System.Collections.Generic;
using System.Drawing.Drawing2D;
using System.Text;
using BTreeEditor.Data.Nodes;

namespace BTreeEditor.Data
{
	/// <summary>
	/// 可视化行为树数据
	/// </summary>
	[Serializable]
	public class BTreeVisualData
	{
		/// <summary>
		/// 文件完整路径
		/// </summary>
		[NonSerialized]
		public string FileName;
		/// <summary>
		/// 根节点
		/// </summary>
		public RootNode RootNode {private set; get;}
		/// <summary>
		/// 节点的ID
		/// </summary>
		private int _incNodeID = 0;
		/// <summary>
		/// 生成一个节点ID
		/// </summary>
		/// <returns></returns>
		public int NewNodeID()
		{
			return _incNodeID++;
		}
		/// <summary>
		/// 
		/// </summary>
		[NonSerialized]
		public bool NeedUpdateLayout = true;
		/// <summary>
		/// 创建可视化数据
		/// </summary>
		/// <param name="filename"></param>
		/// <param name="rootName"></param>
		public BTreeVisualData(string filename, string rootName="根节点")
		{
			this.FileName = filename;
			this.RootNode = new RootNode();
		}
		/// <summary>
		/// 计算绘图需要的大小
		/// </summary>
		/// <param name="width"></param>
		/// <param name="height"></param>
		/// <returns></returns>
		public Size CalcGraphBounds(int width, int height)
		{
			Size screen = new Size(width, height);
			if(NeedUpdateLayout)
			{
				screen = RootNode.UpdateLayout(screen);
				NeedUpdateLayout = false;
			}
			screen.Width = Math.Max(screen.Width, RootNode.LayoutWidth);
			screen.Height = Math.Max(screen.Height, RootNode.LayoutHeight);
			return screen;
		}
		
		public bool AddNode(BTreeNode node, BTreeNode parent)
		{
			if(parent.AddChild(node))
			{
				this.NeedUpdateLayout = true;
				return true;
			}
			return false;
		}

		public bool DelNode(BTreeNode node)
		{
			if(node.Parent.RemoveChild(node))
			{
				this.NeedUpdateLayout = true;
				return true;
			}
			return false;
		}
		/// <summary>
		/// 获取鼠标当前选中的节点
		/// </summary>
		/// <param name="mx"></param>
		/// <param name="my"></param>
		/// <returns></returns>
		public BTreeNode CurrentNode(int mx , int my)
		{
			return this.PriContainsHit(this.RootNode, mx, my);
		}
		/// <summary>
		/// 获取当前UID的节点
		/// </summary>
		/// <param name="nodeID"></param>
		/// <returns></returns>
		public BTreeNode CurrentNode(String nodeID)
		{
			return this.PriEquipWithID(this.RootNode, nodeID);
		}
		/// <summary>
		/// 导出前检查
		/// </summary>
		/// <param name="result"></param>
		/// <returns></returns>
		public bool CheckCanExport(ref StringBuilder result)
		{
			this.PriCheckCanExport(this.RootNode, ref result);
			if(result.Length > 0)
				return false;
			return true;
		}
		/// <summary>
		/// 内部递归获取不能导出的原因
		/// </summary>
		/// <param name="node"></param>
		/// <param name="result"></param>
		void PriCheckCanExport(BTreeNode node, ref StringBuilder result)
		{
			string res = node.CanExportCheck();
			if(!string.IsNullOrEmpty(res))
				result.Append(string.Format("{0}: {1}\r\n",node.NodeName , res));
			foreach (BTreeNode child in node.Nodes) 
			{
				this.PriCheckCanExport(child, ref result);
			}
		}
		
		/// <summary>
		/// 内部递归判断节点
		/// </summary>
		/// <param name="node"></param>
		/// <param name="cx"></param>
		/// <param name="cy"></param>
		/// <returns></returns>
		BTreeNode PriContainsHit(BTreeNode node, int cx, int cy)
		{
			// 矩形区域判断
			if(node.Contains(cx, cy))
				return node;
			BTreeNode res = null;
			foreach (BTreeNode child in node.Nodes) 
			{
				res = this.PriContainsHit(child, cx, cy);
				if( res != null) return res;
			}
			return res;
		}
		/// <summary>
		/// 内部递归判断ID是否相等
		/// </summary>
		/// <param name="node"></param>
		/// <param name="nodeID"></param>
		/// <returns></returns>
		BTreeNode PriEquipWithID(BTreeNode node, string nodeID)
		{
			// 矩形区域判断
			if(node.NodeID.Equals(nodeID))
				return node;
			BTreeNode res = null;
			foreach (BTreeNode child in node.Nodes) 
			{
				res = this.PriEquipWithID(child, nodeID);
				if( res != null) return res;
			}
			return res;
		}

		/// <summary>
		/// 内部递归更新全部的节点
		/// </summary>
		/// <param name="nodes"></param>
		/// <param name="g"></param>
		void PriUpdateDraw(List<BTreeNode> nodes, Graphics g)
		{
			foreach (BTreeNode node in nodes) 
			{
				this.PriDrawNode(node, g);
				if(node.NodeCount > 0 && node.Expanded)
				{
					this.PriUpdateDraw(node.Nodes, g);
				}
			}
		}
		/// <summary>
		/// 调用全部节点更新
		/// </summary>
		/// <param name="g"></param>
		public void Draw(Graphics g)
		{
			g.InterpolationMode = InterpolationMode.Low;
			g.SmoothingMode = SmoothingMode.HighSpeed;
			//g.DrawRectangle(Pens.AliceBlue, 0, 0, RootNode.LayoutWidth, RootNode.LayoutHeight);
			this.PriDrawNode(this.RootNode, g);
			if(this.RootNode.Expanded)
			{
				this.PriUpdateDraw(this.RootNode.Nodes, g);
			}
		}

		/// <summary>
		/// 内部绘制节点分类函数
		/// </summary>
		/// <param name="node"></param>
		/// <param name="g"></param>
		void PriDrawNode(BTreeNode node, Graphics g)
		{
			node.DrawShape(g);
			node.DrawLabel(g);
			node.DrawConnection(g);
		}
		
	}
}
