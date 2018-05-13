using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Windows.Forms;

namespace Ima.Controls
{
	/// <summary>
	/// Summary description for ZoomControl.
	/// </summary>
	public class ZoomControl : System.Windows.Forms.UserControl
	{
		private System.Windows.Forms.ImageList imageList;
		private System.Windows.Forms.TrackBar trackBar;
		private System.Windows.Forms.Label lblMinus;
		private System.Windows.Forms.Label lblPlus;
		private System.Windows.Forms.ToolTip toolTip;
		private System.ComponentModel.IContainer components;

		/// <summary>
		/// 
		/// </summary>
		public ZoomControl()
		{
			// This call is required by the Windows.Forms Form Designer.
			InitializeComponent();

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
			System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(ZoomControl));
			this.trackBar = new System.Windows.Forms.TrackBar();
			this.lblMinus = new System.Windows.Forms.Label();
			this.imageList = new System.Windows.Forms.ImageList(this.components);
			this.lblPlus = new System.Windows.Forms.Label();
			this.toolTip = new System.Windows.Forms.ToolTip(this.components);
			((System.ComponentModel.ISupportInitialize)(this.trackBar)).BeginInit();
			this.SuspendLayout();
			// 
			// trackBar
			// 
			this.trackBar.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
				| System.Windows.Forms.AnchorStyles.Right)));
			this.trackBar.Location = new System.Drawing.Point(16, 0);
			this.trackBar.Name = "trackBar";
			this.trackBar.Size = new System.Drawing.Size(368, 45);
			this.trackBar.TabIndex = 0;
			this.trackBar.TickFrequency = 0;
			// 
			// lblMinus
			// 
			this.lblMinus.Dock = System.Windows.Forms.DockStyle.Left;
			this.lblMinus.ImageIndex = 1;
			this.lblMinus.ImageList = this.imageList;
			this.lblMinus.Location = new System.Drawing.Point(0, 0);
			this.lblMinus.Name = "lblMinus";
			this.lblMinus.Size = new System.Drawing.Size(24, 26);
			this.lblMinus.TabIndex = 1;
			// 
			// imageList
			// 
			this.imageList.ColorDepth = System.Windows.Forms.ColorDepth.Depth32Bit;
			this.imageList.ImageSize = new System.Drawing.Size(16, 16);
			this.imageList.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList.ImageStream")));
			this.imageList.TransparentColor = System.Drawing.Color.White;
			// 
			// lblPlus
			// 
			this.lblPlus.Dock = System.Windows.Forms.DockStyle.Right;
			this.lblPlus.ImageIndex = 0;
			this.lblPlus.ImageList = this.imageList;
			this.lblPlus.Location = new System.Drawing.Point(376, 0);
			this.lblPlus.Name = "lblPlus";
			this.lblPlus.Size = new System.Drawing.Size(24, 26);
			this.lblPlus.TabIndex = 2;
			// 
			// ZoomControl
			// 
			this.Controls.Add(this.lblPlus);
			this.Controls.Add(this.lblMinus);
			this.Controls.Add(this.trackBar);
			this.Name = "ZoomControl";
			this.Size = new System.Drawing.Size(400, 26);
			this.EnabledChanged += new System.EventHandler(this.ZoomControl_EnabledChanged);
			((System.ComponentModel.ISupportInitialize)(this.trackBar)).EndInit();
			this.ResumeLayout(false);

		}
		#endregion

		/// <summary>
		/// 
		/// </summary>
		public string Tooltip
		{
			get
			{
				return this.toolTip.GetToolTip(this.trackBar);
			}
			set
			{
				this.toolTip.SetToolTip(this.trackBar, value);
			}
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="e"></param>
		public void AddZoomEvent(EventHandler e)
		{
			this.trackBar.ValueChanged+= e;
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="e"></param>
		public void RemoveZoomEvent(EventHandler e)
		{
			this.trackBar.ValueChanged-= e;
		}

		/// <summary>
		/// Current Zoom Value
		/// </summary>
		public int ZoomValue
		{
			get
			{
				return this.trackBar.Value;
			}
			set
			{
				this.trackBar.Value = value;
			}
		}

		/// <summary>
		/// Minimum Scale Value
		/// </summary>
		public int ZoomMin
		{
			get
			{
				return this.trackBar.Minimum;
			}
			set
			{
				this.trackBar.Minimum = value;
				this.trackBar.TickFrequency = (this.trackBar.Maximum - this.trackBar.Minimum) / 2;
			}
		}
		
		/// <summary>
		/// Maximum Scale Value
		/// </summary>
		public int ZoomMax
		{
			get
			{
				return this.trackBar.Maximum;
			}
			set
			{
				this.trackBar.Maximum = value;				
				this.trackBar.TickFrequency = (this.trackBar.Maximum - this.trackBar.Minimum) / 2;
			}
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void ZoomControl_EnabledChanged(object sender, System.EventArgs e)
		{
			this.trackBar.Enabled = this.Enabled;
			this.lblMinus.Enabled = this.Enabled;
			this.lblPlus.Enabled  = this.Enabled;
			this.toolTip.Active   = this.Enabled;
		}
	}
}
