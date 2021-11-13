/*
 * User: 覃贵锋
 * Date: 2021/11/13
 * Time: 10:22
 * 
 * 
 */
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using BTreeEditor.Data;

namespace BTreeEditor
{
	/// <summary>
	/// Description of MainForm.
	/// </summary>
	public partial class MainForm : Form
	{
		
		public MainForm()
		{
			//
			// The InitializeComponent() call is required for Windows Forms designer support.
			//
			InitializeComponent();
			
			//
			// TODO: Add constructor code after the InitializeComponent() call.
			//
			BTreeVisualData data = new BTreeVisualData("1.btree");
			this.bTreeControl.SetBTreeData(data);
			
			this.SizeChanged += (object sender, EventArgs e) => this.bTreeControl.OnClientSizeChanged(this.ClientSize);
		}
	}
}
