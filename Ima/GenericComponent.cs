using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Windows.Forms;

namespace Ima
{
	/// <summary>
	/// Summary description for GenericComponent.
	/// </summary>
	public class GenericComponent : System.Windows.Forms.UserControl
	{
		protected System.Windows.Forms.Panel     panel;
		protected System.Windows.Forms.ImageList imageList;
		protected System.Windows.Forms.ToolBar   toolBar;
		protected System.Windows.Forms.Panel     bottomPanel;
		private System.ComponentModel.IContainer components;

		public GenericComponent()
		{
			// This call is required by the Windows.Forms Form Designer.
			InitializeComponent();

			// TODO: Add any initialization after the InitializeComponent call

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

		#region Component Designer generated code
		/// <summary> 
		/// Required method for Designer support - do not modify 
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.components = new System.ComponentModel.Container();
			this.panel = new System.Windows.Forms.Panel();
			this.imageList = new System.Windows.Forms.ImageList(this.components);
			this.toolBar = new System.Windows.Forms.ToolBar();
			this.bottomPanel = new System.Windows.Forms.Panel();
			this.bottomPanel.SuspendLayout();
			this.SuspendLayout();
			// 
			// panel
			// 
			this.panel.Dock = System.Windows.Forms.DockStyle.Fill;
			this.panel.Location = new System.Drawing.Point(0, 0);
			this.panel.Name = "panel";
			this.panel.Size = new System.Drawing.Size(400, 296);
			this.panel.TabIndex = 0;
			// 
			// imageList
			// 
			this.imageList.ColorDepth = System.Windows.Forms.ColorDepth.Depth32Bit;
			this.imageList.ImageSize = new System.Drawing.Size(32, 32);
			this.imageList.TransparentColor = System.Drawing.Color.Transparent;
			// 
			// toolBar
			// 
			this.toolBar.Appearance = System.Windows.Forms.ToolBarAppearance.Flat;
			this.toolBar.Divider = false;
			this.toolBar.Dock = System.Windows.Forms.DockStyle.Fill;
			this.toolBar.DropDownArrows = true;
			this.toolBar.ImageList = this.imageList;
			this.toolBar.Location = new System.Drawing.Point(0, 0);
			this.toolBar.Name = "toolBar";
			this.toolBar.ShowToolTips = true;
			this.toolBar.Size = new System.Drawing.Size(400, 40);
			this.toolBar.TabIndex = 1;
			// 
			// bottomPanel
			// 
			this.bottomPanel.Controls.Add(this.toolBar);
			this.bottomPanel.Dock = System.Windows.Forms.DockStyle.Bottom;
			this.bottomPanel.Location = new System.Drawing.Point(0, 296);
			this.bottomPanel.Name = "bottomPanel";
			this.bottomPanel.Size = new System.Drawing.Size(400, 48);
			this.bottomPanel.TabIndex = 2;
			// 
			// GenericComponent
			// 
			this.Controls.Add(this.panel);
			this.Controls.Add(this.bottomPanel);
			this.Name = "GenericComponent";
			this.Size = new System.Drawing.Size(400, 344);
			this.bottomPanel.ResumeLayout(false);
			this.ResumeLayout(false);

		}
		#endregion


	}
}
