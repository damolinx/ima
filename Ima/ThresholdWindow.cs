using System;
using System.Drawing;
using System.Windows.Forms;

namespace Ima
{
	/// <summary>
	/// Summary description for ThresholdWindow.
	/// </summary>
	public class ThresholdWindow : Ima.Controls.ActivePreviewWindow
	{
		private System.Windows.Forms.TrackBar trackBar;
		private System.Windows.Forms.NumericUpDown udText;
		private System.Windows.Forms.GroupBox groupBox;
		private System.Windows.Forms.Label lblHelp;

		/// <summary>
		/// 
		/// </summary>
		private ThresholdFilterBase filter;

		#region Constructors

		/// <summary>
		/// 
		/// </summary>
		/// <param name="filter"></param>
		public ThresholdWindow(ImageWrapper image, ThresholdFilterBase filter)
            : this(image, filter, BASE_WIDTH, BASE_HEIGHT)
		{
			//
			// TODO: Add constructor logic here
			//
		}

		/// <summary>
		/// 
		/// </summary>
		public ThresholdWindow(ImageWrapper image, ThresholdFilterBase filter, int w, int h) : base(filter.Name, w, h, image)
		{
			this.filter = filter;
			InitializeComponent();
			//
			// TODO: Add constructor logic here
			//
			this.groupBox.Text  = this.filter.Property;
			this.lblHelp.Text   = "TODO: add description";
			this.udText.Minimum = this.trackBar.Minimum = this.filter.Minimum;
			this.udText.Maximum = this.trackBar.Maximum = this.filter.Maximum;
			this.udText.Value   = this.trackBar.Value   = this.filter.Threshold;
			this.trackBar.TickFrequency = (this.filter.Maximum - this.filter.Minimum) / 10;

			///Add Events after adjustment
			this.udText.ValueChanged += new System.EventHandler(this.udText_ValueChanged);
			this.trackBar.ValueChanged += new System.EventHandler(this.trackBar_ValueChanged);
		}

		#endregion

		#region Windows Form Designer generated code
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
            this.groupBox = new System.Windows.Forms.GroupBox();
            this.lblHelp = new System.Windows.Forms.Label();
            this.udText = new System.Windows.Forms.NumericUpDown();
            this.trackBar = new System.Windows.Forms.TrackBar();
            ((System.ComponentModel.ISupportInitialize)(this.previewBox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.imageBox)).BeginInit();
            this.groupBox.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.udText)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar)).BeginInit();
            this.SuspendLayout();
            // 
            // previewBox
            // 
            this.previewBox.Location = new System.Drawing.Point(461, 41);
            this.previewBox.Size = new System.Drawing.Size(432, 304);
            // 
            // btnApply
            // 
            this.btnApply.Location = new System.Drawing.Point(625, 454);
            this.btnApply.Size = new System.Drawing.Size(135, 39);
            // 
            // btnClose
            // 
            this.btnClose.Location = new System.Drawing.Point(769, 454);
            this.btnClose.Size = new System.Drawing.Size(135, 39);
            // 
            // imageBox
            // 
            this.imageBox.Location = new System.Drawing.Point(9, 41);
            this.imageBox.Size = new System.Drawing.Size(432, 304);
            // 
            // groupBox
            // 
            this.groupBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox.Controls.Add(this.lblHelp);
            this.groupBox.Controls.Add(this.udText);
            this.groupBox.Controls.Add(this.trackBar);
            this.groupBox.Location = new System.Drawing.Point(14, 352);
            this.groupBox.Name = "groupBox";
            this.groupBox.Size = new System.Drawing.Size(885, 89);
            this.groupBox.TabIndex = 0;
            this.groupBox.TabStop = false;
            // 
            // lblHelp
            // 
            this.lblHelp.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblHelp.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblHelp.Location = new System.Drawing.Point(14, 108);
            this.lblHelp.Name = "lblHelp";
            this.lblHelp.Size = new System.Drawing.Size(856, 0);
            this.lblHelp.TabIndex = 3;
            // 
            // udText
            // 
            this.udText.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.udText.Location = new System.Drawing.Point(755, 27);
            this.udText.Name = "udText";
            this.udText.Size = new System.Drawing.Size(101, 29);
            this.udText.TabIndex = 2;
            this.udText.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // trackBar
            // 
            this.trackBar.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.trackBar.Location = new System.Drawing.Point(14, 27);
            this.trackBar.Name = "trackBar";
            this.trackBar.Size = new System.Drawing.Size(726, 80);
            this.trackBar.TabIndex = 1;
            // 
            // ThresholdWindow
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(9, 22);
            this.ClientSize = new System.Drawing.Size(913, 505);
            this.Controls.Add(this.groupBox);
            this.Name = "ThresholdWindow";
            this.Controls.SetChildIndex(this.btnApply, 0);
            this.Controls.SetChildIndex(this.btnClose, 0);
            this.Controls.SetChildIndex(this.imageBox, 0);
            this.Controls.SetChildIndex(this.groupBox, 0);
            this.Controls.SetChildIndex(this.previewBox, 0);
            ((System.ComponentModel.ISupportInitialize)(this.previewBox)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.imageBox)).EndInit();
            this.groupBox.ResumeLayout(false);
            this.groupBox.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.udText)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar)).EndInit();
            this.ResumeLayout(false);

		}
		#endregion
	
		/// <summary>
		/// 
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private  void udText_ValueChanged(object sender, System.EventArgs e)
		{
			this.filter.Threshold = this.trackBar.Value = (int) udText.Value;
			OnOriginalBitmapChanged(this.image, null);
			unsafe
			{
				if (this.filter.Direct)
				{
					this.preview.Apply(this.filter.Name, new ImageDirectFilter(this.filter.filter));
				}
				else
				{
					this.preview.Apply(this.filter.Name, filter.Border, new ImageIndirectFilter(this.filter.filter));
				}
			}
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void trackBar_ValueChanged(object sender, System.EventArgs e)
		{
			this.udText.Value = trackBar.Value;
		}

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
		protected  void previewBox_Click(object sender, System.EventArgs e)
		{
			if (Configuration.Instance.GetProperty(this.Name + Configuration.SUFFIX_PREVIEW_USE_THUMBNAIL, Configuration.VALUE_ON).Equals(Configuration.VALUE_ON))		
			{
				Configuration.Instance.SetProperty(this.Name + Configuration.SUFFIX_PREVIEW_USE_THUMBNAIL, Configuration.VALUE_OFF);
			}
			else
			{
				Configuration.Instance.SetProperty(this.Name + Configuration.SUFFIX_PREVIEW_USE_THUMBNAIL, Configuration.VALUE_ON);
			}
			this.udText_ValueChanged(this.udText, EventArgs.Empty);
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
					udText_ValueChanged(udText, EventArgs.Empty);
				}
			}
		}
	}
}