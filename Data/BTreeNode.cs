/*
 * User: 覃贵锋
 * Date: 2021/11/9
 * Time: 10:53
 * 
 * 
 */
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using BTreeEditor.Data.Nodes;

namespace BTreeEditor.Data
{
	/// <summary>
	/// 行为树的节点数据
	/// </summary>
	[Serializable]
	public class BTreeNode
	{
		#region 静态属性区域
		internal static int DEFAULT_NODE_WIDTH = 120;
		internal static int DEFAULT_NODE_HEIGHT = 40;
		internal static int DEFAULT_V_PADDING = 20;
		internal static int DEFAULT_H_PADDING = 50;
		internal static int DEFAULT_INTERPOLATION = 48; 
		/// <summary>
		/// 激活情况下填充区域笔刷
		/// </summary>
		internal static Brush ACTIVE_FILL_BRUSH = Brushes.LemonChiffon;
		/// <summary>
		/// 绘制区域画笔
		/// </summary>
		internal static Pen ACTIVE_DRAW_PEN = Pens.LawnGreen;
		#endregion
		
		/// <summary>
		/// 节点的唯一ID
		/// </summary>
		[BrowsableAttribute(false)]
		public string NodeID { private set; get;}
		/// <summary>
		/// 节点的X坐标
		/// </summary>
		[BrowsableAttribute(false)]
		public int X {private set; get;}
		/// <summary>
		/// 节点的Y坐标
		/// </summary>
		[BrowsableAttribute(false)]
		public int Y {private set; get;}
		/// <summary>
		/// 节点的自身宽度
		/// </summary>
		[BrowsableAttribute(false)]
		public int Width {protected set; get;}
		/// <summary>
		/// 节点的自身高度
		/// </summary>
		[BrowsableAttribute(false)]
		public int Height {protected set; get;}
		/// <summary>
		/// 节点的布局高度 - 计算了子节点的高度，多个子节点会按排列计算，间隔由DEFAULT_V_PADDING设置
		/// </summary>
		[BrowsableAttribute(false)]
		public int LayoutHeight {private set; get;}
		/// <summary>
		/// 节点布局宽度 -计算子节点的宽度，多个子节点按最宽的计算，间隔由DEFAULT_H_PADDING设置
		/// </summary>
		[BrowsableAttribute(false)]
		public int LayoutWidth {private set; get;}
		/// <summary>
		/// 节点的子节点列表
		/// </summary>
		[BrowsableAttribute(false)]
		public List<BTreeNode> Nodes {protected set; get;}
		/// <summary>
		/// 返回节点的数量
		/// </summary>
		[BrowsableAttribute(false)]
		public int NodeCount { get {return Nodes.Count;}}
		/// <summary>
		/// 节点的类型定义
		/// </summary>
		public BTreeNodeType NodeType {protected set; get;}
		/// <summary>
		/// 节点名称
		/// </summary>
		public string NodeName {set; get;}
		/// <summary>
		/// 节点的类型名
		/// </summary>
		public string ClassName{protected set; get;}
		/// <summary>
		/// 父节点的执行
		/// </summary>
		[BrowsableAttribute(false)]
		public BTreeNode Parent {set; get;}
		/// <summary>
		/// 是否选中
		/// </summary>
		[BrowsableAttribute(false)]
		[NonSerialized]
		public bool Selected = false;
		/// <summary>
		/// 是否接受复合节点-选择、序列、分支、循环
		/// </summary>
		[BrowsableAttribute(false)]
		public bool AcceptComposite {set; get;}
		/// <summary>
		/// 是否接受装饰节点-前置条件、中断条件
		/// </summary>
		[BrowsableAttribute(false)]
		public bool AcceptDecoration {set; get;}
		/// <summary>
		/// 是否接受条件节点-简单条件、AND、OR
		/// </summary>
		[BrowsableAttribute(false)]
		public bool AcceptCondition {set; get;}
		/// <summary>
		/// 是否结束行为节点-action
		/// </summary>
		[BrowsableAttribute(false)]
		public bool AcceptAction {set; get;}
		/// <summary>
		/// 是否接受删除
		/// </summary>
		[BrowsableAttribute(false)]
		public bool AcceptDeleted {set; get;}
		/// <summary>
		/// 是否折叠
		/// </summary>
		[BrowsableAttribute(false)]
		private bool _expanded = true;
		/// <summary>
		/// 提供外部操作
		/// </summary>
		[BrowsableAttribute(false)]
		public bool Expanded 
		{
			get
			{
				return _expanded;
			}
			set
			{
				_expanded = value;
				this.PriUpdateVisible(_expanded);
				// 对于自己始终是可见的
				this.Visible = true; 
			}
		}
		/// <summary>
		/// 是否显示 - 折叠后需要设置此属性来屏蔽点击检测
		/// </summary>
		[BrowsableAttribute(false)]
		public bool Visible {set; get;}
		/// <summary>
		/// 基类默认构造函数
		/// </summary>
		public BTreeNode()
		{
			this.NodeID = Guid.NewGuid().ToString();
			this.X = 0;
			this.Y = 0;
			this.Width = DEFAULT_NODE_WIDTH;
			this.Height = DEFAULT_NODE_HEIGHT;
			this.LayoutHeight = DEFAULT_NODE_HEIGHT;
			this.NodeType = BTreeNodeType.ERROR;
			this.Nodes = new List<BTreeNode>();
			this.Expanded = true;
			this.Visible = true;
		}
		/// <summary>
		/// 内部执行节点布局更新
		/// </summary>
		/// <returns></returns>
		internal int PriUpdateLayout()
		{
			int h = 0;
			// 这个是自身的宽度 + 水平方向的间隔
			int w = this.Width + DEFAULT_H_PADDING;
			// 拿子节点中宽度最宽的那个。
			int max_child_width = 0;
			if(this.Expanded)
			{
				// 这里计算的结果没有进行
				foreach (BTreeNode node in this.Nodes) 
				{
					max_child_width = Math.Max(max_child_width, node.PriUpdateLayout());
					h += node.LayoutHeight + DEFAULT_V_PADDING;
				}
			}
			this.LayoutHeight = Math.Max(h -DEFAULT_V_PADDING, this.Height); // 减去最后一个额外加上去的间隔高度
			this.LayoutWidth = w + max_child_width; // 当前节点自身 + 子节点的总布局宽度 
			return this.LayoutWidth;
		}
		internal void PriUpdatePosition()
		{
			// 计算子节点的排布位置，子节点自己又会触发它的子节点，进行递归下去。
			int cx = this.X + this.Width + DEFAULT_H_PADDING;
			int sy = this.Y - this.LayoutHeight / 2;
			if(this.Expanded)
			{
				foreach (BTreeNode node in this.Nodes) 
				{
					node.X = cx;
					node.Y = sy + node.LayoutHeight / 2;
					sy += node.LayoutHeight + DEFAULT_V_PADDING;
					node.PriUpdatePosition(); // 递归更新内部的节点
				}
			}
		}
		/// <summary>
		/// 内部遍历更新是否可见
		/// </summary>
		/// <param name="v"></param>
		internal void PriUpdateVisible(bool v)
		{
			this.Visible = v;
			foreach (BTreeNode node in this.Nodes) 
			{
				node.PriUpdateVisible(v);
			}
		}
		/// <summary>
		/// 更新节点的布局-会同步修改子节点的布局
		/// 触发修改布局的包括设置位置，大小，增删节点
		/// </summary>
		public Size UpdateLayout(Size screenSize)
		{
			this.PriUpdateLayout();
			// 更新画布尺寸
			screenSize.Width = Math.Max(this.LayoutWidth + DEFAULT_H_PADDING * 4, screenSize.Width);
			screenSize.Height = Math.Max(this.LayoutHeight + DEFAULT_V_PADDING * 4, screenSize.Height);
			// 水平扩展树, 垂直居中
			this.X = DEFAULT_H_PADDING;
			this.Y = screenSize.Height / 2;
			
			// 更新节点的位置
			this.PriUpdatePosition();
			// 返回新的尺寸
			return screenSize;
		}
		/// <summary>
		/// 判断点是否在当前的节点内部
		/// </summary>
		/// <param name="cx"></param>
		/// <param name="cy"></param>
		/// <returns></returns>
		public bool Contains(int cx, int cy)
		{
			// 不可见不参与点击判断
			if(!this.Visible) return false;
			// 矩形区域判断
			if(this.X <= cx && this.Y <= cy &&
			   this.X + this.Width >= cx && this.Y + this.Height >= cy)
				return true;
			return false;
		}
		/// <summary>
		/// 属性改变
		/// </summary>
		/// <param name="attrName"></param>
		/// <param name="value"></param>
		public virtual void ChangedPropertyValue(string attrName, object value)
		{
			
		}
		/// <summary>
		/// 绘制图形
		/// </summary>
		/// <param name="g"></param>
		public virtual void DrawShape(Graphics g)
		{
			
		}
		/// <summary>
		/// 移除子节点
		/// </summary>
		/// <param name="node"></param>
		/// <returns></returns>
		public virtual bool RemoveChild(BTreeNode node)
		{
			return this.Nodes.Remove(node);
		}
		/// <summary>
		/// 添加子节点
		/// </summary>
		/// <param name="node"></param>
		public virtual bool AddChild(BTreeNode node)
		{
			this.Nodes.Add(node);
			node.Parent = this;
			return true;
		}
		/// <summary>
		/// 能否导出运行检查，返回不能导出的原因
		/// </summary>
		/// <returns></returns>
		public virtual String CanExportCheck()
		{
			return string.Empty;
		}
		/// <summary>
		/// 绘制节点名
		/// </summary>
		/// <param name="g"></param>
		public virtual void DrawLabel(Graphics g)
		{
			g.InterpolationMode = InterpolationMode.Low;
			g.SmoothingMode = SmoothingMode.HighSpeed;
			string label = this.NodeName;
			if(this.Expanded == false && this.NodeCount > 0)
			{
				label = "[+] " + this.NodeName;
			}
			// TODO 后续可以优化性能，不用每次都计算
			SizeF fx = g.MeasureString(label, SystemFonts.DefaultFont);
			float cx = (this.Width - fx.Width) /2 + this.X;
			float cy = (this.Height - fx.Height) / 2 + this.Y;
			g.DrawString(label, SystemFonts.DefaultFont, Brushes.Navy, cx, cy);
		}
		/// <summary>
		/// 绘制连线-具有贝塞尔曲线特性
		/// </summary>
		/// <param name="g"></param>
		public virtual void DrawConnection(Graphics g)
		{
			if(this.Parent == null) return;
			
			g.InterpolationMode = InterpolationMode.HighQualityBilinear;
			g.SmoothingMode = SmoothingMode.HighQuality;
			
			int sX = this.Parent.X + this.Parent.Width;
			int sY = this.Parent.Y + this.Parent.Height / 2;
			
			int eX = this.X;
			int eY = this.Y + this.Height / 2;
			
            PointF[] points = new PointF[DEFAULT_INTERPOLATION];
            for (int i = 0; i < DEFAULT_INTERPOLATION; i++)
            {
                float amount = i/(float) (DEFAULT_INTERPOLATION - 1);
               
                var lx = Lerp(sX, eX, amount);
                var d = Math.Min(Math.Abs(eX - sX), 100);
                var a = new PointF((float) Scale(amount, 0, 1, sX, sX + d),
                    sY);
                var b = new PointF((float) Scale(amount, 0, 1, eX-d, eX), eY);

                var bas = Sat(Scale(amount, 0.1, 0.9, 0, 1));       
                var cos = Math.Cos(bas*Math.PI);
                if (cos < 0)
                {
                    cos = -Math.Pow(-cos, 0.2);
                }
                else
                {
                    cos = Math.Pow(cos, 0.2);
                }
                amount = (float)cos * -0.5f + 0.5f;

                var f = Lerp(a, b, amount);
                points[i] = f;
            }

            g.DrawLines(Pens.Indigo, points);
		}
		
		 /// <summary>
        /// 绘制椭圆
        /// </summary>
        /// <param name="g"></param>
        /// <param name="p"></param>
        /// <param name="b"></param>
        internal void DrawEllipse(Graphics g, Pen p, Brush b)
        {
        	g.InterpolationMode = InterpolationMode.HighQualityBilinear;
			g.SmoothingMode = SmoothingMode.HighQuality;
        	g.FillEllipse(b, this.X, this.Y, this.Width, this.Height);
			g.DrawEllipse(p, this.X, this.Y, this.Width, this.Height);
        }
        /// <summary>
        /// 画矩形
        /// </summary>
        /// <param name="g"></param>
        /// <param name="p"></param>
        /// <param name="b"></param>
        internal void DrawRectangle(Graphics g, Pen p, Brush b)
        {
        	g.InterpolationMode = InterpolationMode.Low;
			g.SmoothingMode = SmoothingMode.HighSpeed;
        	g.FillRectangle(b, this.X, this.Y, this.Width, this.Height);
			g.DrawRectangle(p, this.X, this.Y, this.Width, this.Height);
        }
        /// <summary>
        /// 绘制钻石形状
        /// </summary>
        /// <param name="g"></param>
        /// <param name="p"></param>
        /// <param name="b"></param>
        internal void DrawDiamond(Graphics g, Pen p, Brush b)
        {
        	g.InterpolationMode = InterpolationMode.Low;
			g.SmoothingMode = SmoothingMode.HighSpeed;
        	float cw = this.Width / 2.0f;
        	float ch = this.Height / 2.0f;
        	float cx = this.X;
        	float cy = this.Y;
        	PointF[] points = new PointF[]
        	{
        		new PointF(cx + cw, cy),
        		new PointF(cx + this.Width, cy + ch),
        		new PointF(cx + cw, cy + this.Height),
        		new PointF(cx, cy + ch),
        	};
        	g.FillPolygon(b, points);
        	g.DrawPolygon(p, points);
        }
        /// <summary>
        /// 绘制八角矩形-长方八角
        /// </summary>
        /// <param name="g"></param>
        /// <param name="p"></param>
        /// <param name="b"></param>
        /// <param name="radius"></param>
        internal void DrawEightRadiusRectangle(Graphics g, Pen p, Brush b, float radius)
        {
        	g.InterpolationMode = InterpolationMode.Low;
			g.SmoothingMode = SmoothingMode.HighSpeed;
        	using (GraphicsPath path = new GraphicsPath()) 
        	{
        		//左边
        		path.AddLine(this.X, this.Y + this.Height - radius, this.X, this.Y + radius);
        		path.AddLine(this.X, this.Y + radius, this.X , this.Y + radius);
        		//上边
        		path.AddLine(this.X + radius, this.Y, this.X + this.Width - radius, this.Y);
        		path.AddLine(this.X + Width - radius, this.Y, this.X + this.Width, this.Y + radius);
        		//右边
        		path.AddLine(this.X + this.Width, this.Y + radius, this.X + this.Width, this.Y + this.Height - radius);
        		path.AddLine(this.X + this.Width, this.Y + this.Height - radius, this.X + this.Width - radius, this.Y + this.Height);
        		// 下边 - 左下角
        		path.AddLine(this.X + this.Width - radius, this.Y + this.Height , this.X + radius, this.Y + this.Height);
        		path.AddLine(this.X + radius, this.Y + this.Height, this.X, this.Y + this.Height - radius);
        		
        		g.FillPath(b, path);
        		g.DrawPath(p, path);
        	}
        }
        /// <summary>
        /// 绘制圆角矩形
        /// </summary>
        /// <param name="g"></param>
        /// <param name="p"></param>
        /// <param name="b"></param>
        /// <param name="radius"></param>
        internal void DrawRoundRectangle(Graphics g, Pen p, Brush b, float radius)
        {
        	g.InterpolationMode = InterpolationMode.HighQualityBilinear;
			g.SmoothingMode = SmoothingMode.HighQuality;
        	//AddArc 绘制的是矩形的内切椭圆，注意是内切。不是外切
        	using (GraphicsPath path = new GraphicsPath()) 
        	{
        		//左边 - 左上角
        		path.AddLine(this.X, this.Y + this.Height - radius, this.X, this.Y + radius);
        		path.AddArc(this.X, this.Y, 2f * radius, 2f * radius, 180f, 90f);
        		//上边- 右上角
        		path.AddLine(this.X + radius, this.Y, this.X + this.Width - radius, this.Y);
        		path.AddArc(this.X + this.Width - 2f*radius, this.Y, 2f * radius, 2f * radius, -90f, 90f);
        		//右边 - 右下角
        		path.AddLine(this.X + this.Width, this.Y + radius, this.X + this.Width, this.Y + this.Height - radius);
        		path.AddArc(this.X + this.Width - 2f*radius, this.Y + this.Height - 2f*radius, 2f * radius, 2f * radius, 0f, 90f);
        		// 下边 - 左下角
        		path.AddLine(this.X + this.Width - radius, this.Y + this.Height , this.X + radius, this.Y + this.Height);
        		path.AddArc(this.X , this.Y + this.Height - 2f*radius, 2f * radius, 2f * radius, 90f, 90f);
        		
        		g.FillPath(b, path);
        		g.DrawPath(p, path);
        	}
        }
		
		internal static double Sat(double x)
        {
            if (x < 0) return 0;
            if (x > 1) return 1;
            return x;
        }

        internal static double Scale(double x, double a, double b, double c, double d)
        {
            double s = (x - a)/(b - a);
            return s*(d - c) + c;
        }

        internal static float Lerp(float a, float b, float amount)
        {
            return a*(1f - amount) + b*amount;
        }

        internal static PointF Lerp(PointF a, PointF b, float amount)
        {
            PointF result = new PointF();

            result.X = a.X*(1f - amount) + b.X*amount;
            result.Y = a.Y*(1f - amount) + b.Y*amount;

            return result;
        }
       
	}
}
