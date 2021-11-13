/*
 * User: 覃贵锋
 * Date: 2021/11/13
 * Time: 10:22
 * 
 * 
 */
namespace BTreeEditor
{
	partial class MainForm
	{
		/// <summary>
		/// Designer variable used to keep track of non-visual components.
		/// </summary>
		private System.ComponentModel.IContainer components = null;
		private BTreeEditor.BTreeControl bTreeControl;
		
		/// <summary>
		/// Disposes resources used by the form.
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
			this.bTreeControl = new BTreeEditor.BTreeControl();
			this.SuspendLayout();
			// 
			// bTreeControl
			// 
			this.bTreeControl.BackColor = System.Drawing.Color.Transparent;
			this.bTreeControl.Dock = System.Windows.Forms.DockStyle.Fill;
			this.bTreeControl.Location = new System.Drawing.Point(0, 0);
			this.bTreeControl.Name = "bTreeControl";
			this.bTreeControl.scaleVal = 1F;
			this.bTreeControl.Size = new System.Drawing.Size(581, 386);
			this.bTreeControl.TabIndex = 0;
			// 
			// MainForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(581, 386);
			this.Controls.Add(this.bTreeControl);
			this.Name = "MainForm";
			this.Text = "BTreeEditor";
			this.ResumeLayout(false);

		}
	}
}
