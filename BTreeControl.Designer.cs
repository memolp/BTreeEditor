/*
 * User: 覃贵锋
 * Date: 2021/10/27
 * Time: 16:33
 * 
 * 
 */
namespace BTreeEditor
{
	partial class BTreeControl
	{
		/// <summary>
		/// Designer variable used to keep track of non-visual components.
		/// </summary>
		private System.ComponentModel.IContainer components = null;
		private System.Windows.Forms.ContextMenuStrip mContextMenu;
		
		/// <summary>
		/// Disposes resources used by the control.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose(bool disposing)
		{
			if (disposing) {
				if (components != null) {
					components.Dispose();
				}
			}
			base.Dispose(disposing);
		}
		
		/// <summary>
		/// This method is required for Windows Forms designer support.
		/// Do not change the method contents inside the source code editor. The Forms designer might
		/// not be able to load this method if it was changed manually.
		/// </summary>
		private void InitializeComponent()
		{
			this.components = new System.ComponentModel.Container();
			this.mContextMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
			this.SuspendLayout();
			// 
			// mContextMenu
			// 
			this.mContextMenu.Name = "mContextMenu";
			this.mContextMenu.Size = new System.Drawing.Size(61, 4);
			// 
			// BTreeControl
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.BackColor = System.Drawing.Color.Transparent;
			this.DoubleBuffered = true;
			this.Name = "BTreeControl";
			this.Size = new System.Drawing.Size(511, 340);
			this.MouseClick += new System.Windows.Forms.MouseEventHandler(this.OnMouseClickEvent);
			this.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.OnMouseDBClickEvent);
			this.PreviewKeyDown += new System.Windows.Forms.PreviewKeyDownEventHandler(this.OnKeyPreDownEvent);
			this.ResumeLayout(false);

		}
	}
}
