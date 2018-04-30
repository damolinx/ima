using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace Ima
{
	public class BasicFilterWindow : Ima.Controls.ActivePreviewWindow
	{

		/// <summary>
		/// Filter
		/// </summary>
		private FilterBase filter;
		private System.Windows.Forms.Label lblHelp;

		private System.ComponentModel.IContainer components = null;

		#region Constructor & Destructor

		/// <summary>
		/// 
		/// </summary>
		/// <param name="filter"></param>
		public BasicFilterWindow(ImageWrapper image, FilterBase filter) : this(image, filter, BASE_WIDTH, BASE_HEIGHT)
	{
		//
		// TODO: Add constructor logic here
		//
	}

		/// <summary>
		/// 
		/// </summary>
		public BasicFilterWindow(ImageWrapper image, FilterBase filter, int w, int h) : base(filter.Name, w, h, image)
	{
		this.filter = filter;
		InitializeComponent();
		this.lblHelp.Text = filter.Description;
	}
		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		protected override void Dispose( bool disposing )
		{
			if( disposing )
			{
				if (components != null) 
				{
					components.Dispose();
				}
			}
			base.Dispose( disposing );
		}

		#endregion

		#region Designer generated code
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.lblHelp = new System.Windows.Forms.Label();
			this.SuspendLayout();
			// 
			// previewBox
			// 
			this.previewBox.Name = "previewBox";
			// 
			// btnApply
			// 
			this.btnApply.Location = new System.Drawing.Point(344, 272);
			this.btnApply.Name = "btnApply";
			// 
			// btnClose
			// 
			this.btnClose.Location = new System.Drawing.Point(424, 272);
			this.btnClose.Name = "btnClose";
			// 
			// imageBox
			// 
			this.imageBox.Name = "imageBox";
			// 
			// lblHelp
			// 
			this.lblHelp.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
			this.lblHelp.Location = new System.Drawing.Point(8, 216);
			this.lblHelp.Name = "lblHelp";
			this.lblHelp.Size = new System.Drawing.Size(488, 48);
			this.lblHelp.TabIndex = 6;
			// 
			// BasicFilterWindow
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.ClientSize = new System.Drawing.Size(504, 302);
			this.Controls.Add(this.lblHelp);
			this.Name = "BasicFilterWindow";
			this.Controls.SetChildIndex(this.lblHelp, 0);
			this.Controls.SetChildIndex(this.previewBox, 0);
			this.Controls.SetChildIndex(this.btnApply, 0);
			this.Controls.SetChildIndex(this.btnClose, 0);
			this.Controls.SetChildIndex(this.imageBox, 0);
			this.ResumeLayout(false);

		}
		#endregion

		#region Events
		/// <summary>
		/// 
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		protected override void btnApply_Click(object sender, System.EventArgs e)
		{
			Cursor cursor = Cursor.Current;
			Cursor.Current = Cursors.WaitCursor;
			unsafe
			{
				if (this.filter.Direct)
				{
					this.image.Apply(filter.Name, new ImageDirectFilter(this.filter.filter));
				}
				else
				{
					this.image.Apply(filter.Name, filter.Border, new ImageIndirectFilter(this.filter.filter));
				}
			}
			Cursor.Current = cursor;
			this.Close();
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		protected override void PreviewForm_VisibleChanged(object sender, System.EventArgs e)
		{
			if (!this.DesignMode)
			{
				if (this.Visible && firstTime)
				{
					firstTime = false;
					OnOriginalBitmapChanged(this.image, null);

					///Apply Filter
					Cursor cursor = Cursor.Current;
					Cursor.Current = Cursors.WaitCursor;
					unsafe
					{
						if (this.filter.Direct)
						{
							this.preview.Apply(filter.Name, new ImageDirectFilter(this.filter.filter));
						}
						else
						{
							this.preview.Apply(filter.Name, filter.Border, new ImageIndirectFilter(this.filter.filter));
						}
					}
					Cursor.Current = cursor;

					
				}
			}
		}

		#endregion
	}
}

