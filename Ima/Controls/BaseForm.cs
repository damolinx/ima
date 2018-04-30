using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;

namespace Ima.Controls
{
	/// <summary>
	/// Summary description for BaseForm.
	/// </summary>
	public class BaseForm : System.Windows.Forms.Form
	{
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		public BaseForm()
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();

			//
			// TODO: Add any constructor code after InitializeComponent call
			//
		}

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		protected override void Dispose( bool disposing )
		{
			if( disposing )
			{
				if(components != null)
				{
					components.Dispose();
				}
			}
			base.Dispose( disposing );
		}

		#region Windows Form Designer generated code
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			// 
			// BaseForm
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.ClientSize = new System.Drawing.Size(292, 266);
			this.Name = "BaseForm";
			this.Text = "BaseForm";
			this.Closing += new System.ComponentModel.CancelEventHandler(this.BaseForm_Closing);
			this.Load += new System.EventHandler(this.BaseForm_Load);

		}
		#endregion

		/// <summary>
		/// Restores Form properties
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void BaseForm_Load(object sender, System.EventArgs e)
		{
			if (!this.DesignMode)
			{
				Configuration.Instance.RestoreLocation(this);
				Configuration.Instance.RestoreSize(this);
			}		
		}

		/// <summary>
		/// Saves form proeprties
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void BaseForm_Closing(object sender, System.ComponentModel.CancelEventArgs e)
		{
			Configuration.Instance.SaveLocation(this);
			Configuration.Instance.SaveSize(this);
		}
	}
}
