/*
 * User: 覃贵锋
 * Date: 2021/11/13
 * Time: 10:22
 * 
 * 
 */
using System;
using System.Windows.Forms;

namespace BTreeEditor
{
	/// <summary>
	/// Class with program entry point.
	/// </summary>
	internal sealed class Program
	{
		/// <summary>
		/// Program entry point.
		/// </summary>
		[STAThread]
		private static void Main(string[] args)
		{
			Application.EnableVisualStyles();
			Application.SetCompatibleTextRenderingDefault(false);
			Application.Run(new MainForm());
		}
		
	}
}
