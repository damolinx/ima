using System;
using System.Drawing.Text;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Windows.Forms;

namespace Ima
{
	/// <summary>
	/// Summary description for NotificationComponent.
	/// </summary>
	public class NotificationComponent : System.Windows.Forms.UserControl, IStatusNotification
	{
		/// <summary> 
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		private int progressMin;
        private int progressMax;
        private int current;
		private Ima.Controls.ZoomControl zoomControl;
        private string msg;

		/// <summary>
		/// 
		/// </summary>
		public NotificationComponent()
		{
			// This call is required by the Windows.Forms Form Designer.
			InitializeComponent();

			// TODO: Add any initialization after the InitializeComponent call
			this.SetStyle(ControlStyles.ResizeRedraw, true);
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
            this.zoomControl = new Ima.Controls.ZoomControl();
            this.SuspendLayout();
            // 
            // zoomControl
            // 
            this.zoomControl.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.zoomControl.Location = new System.Drawing.Point(132, 0);
            this.zoomControl.Name = "zoomControl";
            this.zoomControl.Size = new System.Drawing.Size(380, 32);
            this.zoomControl.TabIndex = 0;
            this.zoomControl.Tooltip = "";
            this.zoomControl.ZoomMax = 10;
            this.zoomControl.ZoomMin = 0;
            this.zoomControl.ZoomValue = 0;
            // 
            // NotificationComponent
            // 
            this.Controls.Add(this.zoomControl);
            this.Name = "NotificationComponent";
            this.Size = new System.Drawing.Size(512, 32);
            this.Paint += this.NotificationComponent_Paint;
            this.ResumeLayout(false);

		}
		#endregion

		#region IStatusNotification Members

	    public void StatusMessage(string msg)
		{
			this.msg = msg;
			this.Invalidate();
		}

		public void StartProgress(int start, int end)
		{
			this.progressMin = start;
			this.progressMax = end;
			this.current     = 0;
			this.Invalidate(new Rectangle(0, 0, this.Width, 6));
		}

		public void EndProgress()
		{
			this.progressMin = 0;
			this.progressMax = 0;
			this.Invalidate(new Rectangle(0, 0, this.Width, 6));
		}

		public int Progress
		{
			get
			{
				return this.current;
			}
			set
			{
				this.current = value;
				this.Invalidate(new Rectangle(0, 0, this.Width, 6));
			}
		}

		public bool ZoomEnable
		{
			get
			{
				return this.zoomControl.Enabled;
			}
			set
			{
				this.zoomControl.Enabled = value;
				this.zoomControl.Visible = value;
				this.Invalidate();
			}
		}

		/// <summary>
		/// 
		/// </summary>
		public int ZoomValue
		{
			get
			{
				return this.zoomControl.ZoomValue;
			}
			set
			{
				this.zoomControl.ZoomValue = value;
			}
		}

		/// <summary>
		/// 
		/// </summary>
		public int ZoomMax
		{
			get
			{
				return this.zoomControl.ZoomMax;
			}
			set
			{
				this.zoomControl.ZoomMax = value;
			}
		}

		/// <summary>
		/// 
		/// </summary>
		public int ZoomMin
		{
			get
			{
				return this.zoomControl.ZoomMin;
			}
			set
			{
				this.zoomControl.ZoomMin = value;
			}
		}

		/// <summary>
		/// 
		/// </summary>
		public string ZoomMsg
		{
			get
			{
				return this.zoomControl.Tooltip;
			}
			set
			{
				this.zoomControl.Tooltip = value;
			}
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="e"></param>
		public void AddZoomEvent(EventHandler e)
		{
			this.zoomControl.AddZoomEvent(e);
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="e"></param>
		public void RemoveZoomEvent(EventHandler e)
		{
			this.zoomControl.RemoveZoomEvent(e);
		}		
		#endregion

		/// <summary>
		/// Specific Paint rutine
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void NotificationComponent_Paint(object sender, System.Windows.Forms.PaintEventArgs e)
		{
			Graphics gfx          = e.Graphics;
			gfx.TextRenderingHint = TextRenderingHint.ClearTypeGridFit;
			int adjWidth          = this.Width - ((this.zoomControl.Visible) ? this.zoomControl.Width : 0);

			//Draw message
			if (this.msg != string.Empty && e.ClipRectangle.Height > 5)
			{
				Font font   = new Font( "Arial", 8.25F);
				SizeF sizef = gfx.MeasureString(this.msg, font);
				gfx.DrawString(this.msg, font, SystemBrushes.ControlText, (adjWidth - sizef.Width) / 2.0f, font.Height);
			}
			
			//Draw Progress
			//if (this.progressMax != 0)
			//{
			//	gfx.DrawRectangle(SystemPens.ControlLight, adjWidth * 0.05f, 4, adjWidth * 0.90f, 2);
			//	gfx.DrawLine(SystemPens.Highlight, adjWidth * 0.05f, 5, (int)(0.95f * adjWidth * this.current / (this.progressMax - this.progressMin)), 5);
			//}
		}
	}
}
