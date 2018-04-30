using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Imaging;
using System.Windows.Forms;

namespace Ima.Controls
{
	public class ActivePreviewWindow : Ima.Controls.PreviewForm
	{
		/// <summary>
		/// Working Image
		/// </summary>
		protected ImageWrapper preview;

		protected System.Windows.Forms.PictureBox previewBox;
		protected System.Windows.Forms.Button btnApply;
		protected System.Windows.Forms.Button btnClose;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label label2;
		private System.ComponentModel.IContainer components = null;


		#region Constructors/Destructors
		public ActivePreviewWindow() : this (string.Empty, null)
		{
		}

		public ActivePreviewWindow(string title, ImageWrapper bitmap) : this(title, BASE_WIDTH, BASE_HEIGHT, bitmap)
		{
		}

		public ActivePreviewWindow(string title, int w, int h, ImageWrapper bitmap) : base(title, w, h, bitmap)
		{
			// This call is required by the Windows Form Designer.
			InitializeComponent();
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
			this.previewBox = new System.Windows.Forms.PictureBox();
			this.btnApply = new System.Windows.Forms.Button();
			this.btnClose = new System.Windows.Forms.Button();
			this.label1 = new System.Windows.Forms.Label();
			this.label2 = new System.Windows.Forms.Label();
			this.SuspendLayout();
			// 
			// imageBox
			// 
			this.imageBox.BackColor = System.Drawing.SystemColors.AppWorkspace;
			this.imageBox.Location = new System.Drawing.Point(5, 24);
			this.imageBox.Name = "imageBox";
			// 
			// previewBox
			// 
			this.previewBox.BackColor = System.Drawing.SystemColors.AppWorkspace;
			this.previewBox.Location = new System.Drawing.Point(256, 24);
			this.previewBox.Name = "previewBox";
			this.previewBox.Size = new System.Drawing.Size(240, 180);
			this.previewBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
			this.previewBox.TabIndex = 1;
			this.previewBox.TabStop = false;
			// 
			// btnApply
			// 
			this.btnApply.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.btnApply.Location = new System.Drawing.Point(344, 320);
			this.btnApply.Name = "btnApply";
			this.btnApply.TabIndex = 2;
			this.btnApply.Text = "&Apply";
			this.btnApply.Click += new System.EventHandler(this.btnApply_Click);
			// 
			// btnClose
			// 
			this.btnClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.btnClose.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.btnClose.Location = new System.Drawing.Point(424, 320);
			this.btnClose.Name = "btnClose";
			this.btnClose.TabIndex = 3;
			this.btnClose.Text = "&Close";
			this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
			// 
			// label1
			// 
			this.label1.Location = new System.Drawing.Point(8, 0);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(240, 23);
			this.label1.TabIndex = 4;
			this.label1.Text = "Original";
			this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// label2
			// 
			this.label2.Location = new System.Drawing.Point(256, 0);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(240, 23);
			this.label2.TabIndex = 5;
			this.label2.Text = "Preview";
			this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// ActivePreviewWindow
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.CancelButton = this.btnClose;
			this.ClientSize = new System.Drawing.Size(504, 350);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.btnClose);
			this.Controls.Add(this.btnApply);
			this.Controls.Add(this.previewBox);
			this.Controls.Add(this.label1);
			this.Name = "ActivePreviewWindow";
			this.Controls.SetChildIndex(this.label1, 0);
			this.Controls.SetChildIndex(this.previewBox, 0);
			this.Controls.SetChildIndex(this.btnApply, 0);
			this.Controls.SetChildIndex(this.btnClose, 0);
			this.Controls.SetChildIndex(this.imageBox, 0);
			this.Controls.SetChildIndex(this.label2, 0);
			this.ResumeLayout(false);

		}
		#endregion

		#region Events

		/// <summary>
		/// 
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		protected virtual void btnClose_Click(object sender, System.EventArgs e)
		{
			this.Close();
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		protected virtual void btnApply_Click(object sender, System.EventArgs e)
		{
			this.Close();
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="args"></param>
		public override void OnOriginalBitmapChanged(object sender, ImageChangedEventArgs args)
		{
			Rectangle rec = this.image.ActiveRegion;

			double factor = Math.Max(1.0,
				Math.Max(((double)rec.Width) / imageBox.Width,
				((double)rec.Height) / imageBox.Height));

			this.preview = new ImageWrapper("Preview: " + System.IO.Path.GetExtension(this.image.Filename),
				(Bitmap)this.image.GetThumbnail(
				(int)(rec.Width  / factor),
				(int)(rec.Height / factor), true));
			this.preview.Changed += new ImageChangedEventHandler(OnPreviewBitmapChanged);

			this.imageBox.Image   = new Bitmap(this.preview.Bitmap);
			this.previewBox.Image = this.preview.Bitmap;
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="args"></param>
		public virtual void OnPreviewBitmapChanged(object sender, ImageChangedEventArgs args)
		{
			this.previewBox.Image = this.preview.Bitmap;
		}
		#endregion

	}
}