using System;
using System.Drawing;
using System.Windows.Forms;

namespace Ima
{
	/// <summary>
	/// Summary description for FlipRotateWindow.
	/// </summary>
	public class FlipRotateWindow : Ima.Controls.ActivePreviewWindow
	{
		private System.Windows.Forms.RadioButton rbNormal;
		private System.Windows.Forms.RadioButton rbFlipHorizontal;
		private System.Windows.Forms.RadioButton rbFlipVertical;
		private System.Windows.Forms.GroupBox gbFlipRotate;
		private System.Windows.Forms.RadioButton rbCompound;
		private System.Windows.Forms.ComboBox cbCompound;
		private System.Windows.Forms.NumericUpDown udAngle;
		private System.Windows.Forms.Button btnOrigin;
		private System.Windows.Forms.Button btnFillcolor;
		private System.Windows.Forms.Label lblFillColor;
		private System.Windows.Forms.Label lblDegrees;
		private System.Windows.Forms.RadioButton rbRotate;

		#region Constructor
		public FlipRotateWindow(ImageWrapper bitmap) : this(BASE_WIDTH, BASE_HEIGHT, bitmap)
		{
			//
			// TODO: Add constructor logic here
			//
		}

		public FlipRotateWindow(int w, int h, ImageWrapper bitmap) : base("Flip/Rotate", w, h, bitmap)
		{
			InitializeComponent();
			//
			// TODO: Add constructor logic here
			//
		}

		#endregion

		#region Windows Form Designer generated code
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.gbFlipRotate = new System.Windows.Forms.GroupBox();
			this.lblFillColor = new System.Windows.Forms.Label();
			this.btnFillcolor = new System.Windows.Forms.Button();
			this.lblDegrees = new System.Windows.Forms.Label();
			this.udAngle = new System.Windows.Forms.NumericUpDown();
			this.cbCompound = new System.Windows.Forms.ComboBox();
			this.rbCompound = new System.Windows.Forms.RadioButton();
			this.rbNormal = new System.Windows.Forms.RadioButton();
			this.rbFlipHorizontal = new System.Windows.Forms.RadioButton();
			this.rbFlipVertical = new System.Windows.Forms.RadioButton();
			this.rbRotate = new System.Windows.Forms.RadioButton();
			this.btnOrigin = new System.Windows.Forms.Button();
			this.gbFlipRotate.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.udAngle)).BeginInit();
			this.SuspendLayout();
			// 
			// previewBox
			// 
			this.previewBox.Name = "previewBox";
			// 
			// btnApply
			// 
			this.btnApply.Location = new System.Drawing.Point(342, 360);
			this.btnApply.Name = "btnApply";
			this.btnApply.TabIndex = 1;
			// 
			// btnClose
			// 
			this.btnClose.Location = new System.Drawing.Point(422, 360);
			this.btnClose.Name = "btnClose";
			this.btnClose.TabIndex = 2;
			// 
			// imageBox
			// 
			this.imageBox.Name = "imageBox";
			// 
			// gbFlipRotate
			// 
			this.gbFlipRotate.Controls.Add(this.lblFillColor);
			this.gbFlipRotate.Controls.Add(this.btnFillcolor);
			this.gbFlipRotate.Controls.Add(this.lblDegrees);
			this.gbFlipRotate.Controls.Add(this.udAngle);
			this.gbFlipRotate.Controls.Add(this.cbCompound);
			this.gbFlipRotate.Controls.Add(this.rbCompound);
			this.gbFlipRotate.Controls.Add(this.rbNormal);
			this.gbFlipRotate.Controls.Add(this.rbFlipHorizontal);
			this.gbFlipRotate.Controls.Add(this.rbFlipVertical);
			this.gbFlipRotate.Controls.Add(this.rbRotate);
			this.gbFlipRotate.Location = new System.Drawing.Point(5, 208);
			this.gbFlipRotate.Name = "gbFlipRotate";
			this.gbFlipRotate.Size = new System.Drawing.Size(491, 144);
			this.gbFlipRotate.TabIndex = 0;
			this.gbFlipRotate.TabStop = false;
			// 
			// lblFillColor
			// 
			this.lblFillColor.Enabled = false;
			this.lblFillColor.Location = new System.Drawing.Point(384, 80);
			this.lblFillColor.Name = "lblFillColor";
			this.lblFillColor.Size = new System.Drawing.Size(96, 23);
			this.lblFillColor.TabIndex = 9;
			this.lblFillColor.Text = "Fill Color";
			this.lblFillColor.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// btnFillcolor
			// 
			this.btnFillcolor.BackColor = System.Drawing.Color.White;
			this.btnFillcolor.Enabled = false;
			this.btnFillcolor.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
			this.btnFillcolor.Location = new System.Drawing.Point(320, 80);
			this.btnFillcolor.Name = "btnFillcolor";
			this.btnFillcolor.Size = new System.Drawing.Size(56, 23);
			this.btnFillcolor.TabIndex = 8;
			this.btnFillcolor.Click += new System.EventHandler(this.btnFillcolor_Click);
			// 
			// lblDegrees
			// 
			this.lblDegrees.Enabled = false;
			this.lblDegrees.Location = new System.Drawing.Point(384, 48);
			this.lblDegrees.Name = "lblDegrees";
			this.lblDegrees.Size = new System.Drawing.Size(96, 23);
			this.lblDegrees.TabIndex = 7;
			this.lblDegrees.Text = "Degrees";
			this.lblDegrees.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// udAngle
			// 
			this.udAngle.Enabled = false;
			this.udAngle.Location = new System.Drawing.Point(320, 48);
			this.udAngle.Maximum = new System.Decimal(new int[] {
																	359,
																	0,
																	0,
																	0});
			this.udAngle.Minimum = new System.Decimal(new int[] {
																	359,
																	0,
																	0,
																	-2147483648});
			this.udAngle.Name = "udAngle";
			this.udAngle.Size = new System.Drawing.Size(56, 20);
			this.udAngle.TabIndex = 6;
			this.udAngle.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			this.udAngle.ValueChanged += new System.EventHandler(this.udAngle_ValueChanged);
			// 
			// cbCompound
			// 
			this.cbCompound.Enabled = false;
			this.cbCompound.Items.AddRange(new object[] {
															System.Drawing.RotateFlipType.RotateNoneFlipNone,
															System.Drawing.RotateFlipType.Rotate180FlipY,
															System.Drawing.RotateFlipType.Rotate270FlipX,
															System.Drawing.RotateFlipType.Rotate270FlipXY,
															System.Drawing.RotateFlipType.Rotate270FlipY,
															System.Drawing.RotateFlipType.Rotate270FlipY,
															System.Drawing.RotateFlipType.Rotate90FlipXY,
															System.Drawing.RotateFlipType.Rotate270FlipX,
															System.Drawing.RotateFlipType.RotateNoneFlipXY});
			this.cbCompound.Location = new System.Drawing.Point(120, 112);
			this.cbCompound.Name = "cbCompound";
			this.cbCompound.Size = new System.Drawing.Size(136, 21);
			this.cbCompound.TabIndex = 4;
			this.cbCompound.SelectedIndexChanged += new System.EventHandler(this.cbCompound_SelectedIndexChanged);
			// 
			// rbCompound
			// 
			this.rbCompound.Location = new System.Drawing.Point(24, 112);
			this.rbCompound.Name = "rbCompound";
			this.rbCompound.TabIndex = 3;
			this.rbCompound.Text = "&Compound";
			this.rbCompound.CheckedChanged += new System.EventHandler(this.rbCompound_CheckedChanged);
			// 
			// rbNormal
			// 
			this.rbNormal.Checked = true;
			this.rbNormal.Location = new System.Drawing.Point(24, 16);
			this.rbNormal.Name = "rbNormal";
			this.rbNormal.Size = new System.Drawing.Size(120, 24);
			this.rbNormal.TabIndex = 0;
			this.rbNormal.TabStop = true;
			this.rbNormal.Text = "&No change";
			this.rbNormal.CheckedChanged += new System.EventHandler(this.rbNormal_CheckedChanged);
			// 
			// rbFlipHorizontal
			// 
			this.rbFlipHorizontal.Location = new System.Drawing.Point(24, 48);
			this.rbFlipHorizontal.Name = "rbFlipHorizontal";
			this.rbFlipHorizontal.Size = new System.Drawing.Size(120, 24);
			this.rbFlipHorizontal.TabIndex = 1;
			this.rbFlipHorizontal.Text = "Flip &Horizontal";
			this.rbFlipHorizontal.CheckedChanged += new System.EventHandler(this.rbFlipHorizontal_CheckedChanged);
			// 
			// rbFlipVertical
			// 
			this.rbFlipVertical.Location = new System.Drawing.Point(24, 80);
			this.rbFlipVertical.Name = "rbFlipVertical";
			this.rbFlipVertical.Size = new System.Drawing.Size(120, 24);
			this.rbFlipVertical.TabIndex = 2;
			this.rbFlipVertical.Text = "Flip &Vertical";
			this.rbFlipVertical.CheckedChanged += new System.EventHandler(this.rbFlipVertical_CheckedChanged);
			// 
			// rbRotate
			// 
			this.rbRotate.Location = new System.Drawing.Point(280, 16);
			this.rbRotate.Name = "rbRotate";
			this.rbRotate.Size = new System.Drawing.Size(200, 24);
			this.rbRotate.TabIndex = 5;
			this.rbRotate.Text = "Arbitrary &Rotate by:";
			this.rbRotate.CheckedChanged += new System.EventHandler(this.rbRotate_CheckedChanged);
			// 
			// btnOrigin
			// 
			this.btnOrigin.Cursor = System.Windows.Forms.Cursors.Cross;
			this.btnOrigin.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
			this.btnOrigin.Location = new System.Drawing.Point(0, 0);
			this.btnOrigin.Name = "btnOrigin";
			this.btnOrigin.Size = new System.Drawing.Size(8, 8);
			this.btnOrigin.TabIndex = 0;
			this.btnOrigin.Visible = false;
			this.btnOrigin.VisibleChanged += new System.EventHandler(this.btnOrigin_VisibleChanged);
			this.btnOrigin.MouseUp += new System.Windows.Forms.MouseEventHandler(this.btnOrigin_MouseUp);
			this.btnOrigin.MouseMove += new System.Windows.Forms.MouseEventHandler(this.btnOrigin_MouseMove);
			// 
			// FlipRotateWindow
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.ClientSize = new System.Drawing.Size(502, 390);
			this.Controls.Add(this.btnOrigin);
			this.Controls.Add(this.gbFlipRotate);
			this.Name = "FlipRotateWindow";
			this.Controls.SetChildIndex(this.gbFlipRotate, 0);
			this.Controls.SetChildIndex(this.previewBox, 0);
			this.Controls.SetChildIndex(this.btnApply, 0);
			this.Controls.SetChildIndex(this.btnClose, 0);
			this.Controls.SetChildIndex(this.imageBox, 0);
			this.Controls.SetChildIndex(this.btnOrigin, 0);
			this.gbFlipRotate.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.udAngle)).EndInit();
			this.ResumeLayout(false);

		}
		#endregion


		/// <summary>
		/// 
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void rbNormal_CheckedChanged(object sender, System.EventArgs e)
		{
			OnOriginalBitmapChanged(this.image, null);
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void rbFlipHorizontal_CheckedChanged(object sender, System.EventArgs e)
		{
			OnOriginalBitmapChanged(this.image, null);
			this.preview.Flip(RotateFlipType.RotateNoneFlipX);
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void rbFlipVertical_CheckedChanged(object sender, System.EventArgs e)
		{
			OnOriginalBitmapChanged(this.image, null);
			this.preview.Flip(RotateFlipType.RotateNoneFlipY);		
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void rbRotate_CheckedChanged(object sender, System.EventArgs e)
		{
			OnOriginalBitmapChanged(this.image, null);
			udAngle.Enabled         = rbRotate.Checked;
			btnOrigin.Visible       = rbRotate.Checked;
			btnFillcolor.Enabled    = rbRotate.Checked;
			lblDegrees.Enabled      = rbRotate.Checked;
			lblFillColor.Enabled    = rbRotate.Checked;
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void rbCompound_CheckedChanged(object sender, System.EventArgs e)
		{
			OnOriginalBitmapChanged(this.image, null);
			cbCompound.Enabled = rbCompound.Checked;
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		protected override void btnApply_Click(object sender, System.EventArgs e)
		{
			if (rbFlipHorizontal.Checked)
			{
				this.image.Flip(RotateFlipType.RotateNoneFlipX);
			}
			else if (rbFlipVertical.Checked)
			{
				this.image.Flip(RotateFlipType.RotateNoneFlipY);
			}
			else if (rbRotate.Checked)
			{
				double dx = this.btnOrigin.Location.X - this.previewBox.Location.X - this.previewBox.Width  / 2.0 + this.preview.Width  / 2.0;
				double dy = this.btnOrigin.Location.Y - this.previewBox.Location.Y - this.previewBox.Height / 2.0 + this.preview.Height / 2.0;
				double sx = this.image.Width / this.preview.Width;
				double sy = this.image.Height / this.preview.Height;
				FilterRotate filter = new FilterRotate((double)this.udAngle.Value, new Point((int)(sx * dx), (int)(sy * dy)));
				filter.Fillcolor = btnFillcolor.BackColor;
				unsafe
				{
					this.image.Apply(filter.Name, filter.Border, new ImageIndirectFilter(filter.filter));
				}
			}
			else if (rbCompound.Checked)
			{
				this.image.Flip((RotateFlipType)this.cbCompound.SelectedItem);
			}
			btnClose_Click(sender, e);
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		protected override void btnClose_Click(object sender, System.EventArgs e)
		{
			this.Close();
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void udAngle_ValueChanged(object sender, System.EventArgs e)
		{
			OnOriginalBitmapChanged(this.image, null);
			int dx = this.btnOrigin.Location.X - this.previewBox.Location.X - this.previewBox.Width / 2;
			int dy = this.btnOrigin.Location.Y - this.previewBox.Location.Y - this.previewBox.Height/ 2;
			FilterRotate filter = new FilterRotate((double)this.udAngle.Value, new Point(dx + this.preview.Width / 2, dy +  this.preview.Height / 2));
			filter.Fillcolor = btnFillcolor.BackColor;
			unsafe
			{
				this.preview.Apply(filter.Name, filter.Border, new ImageIndirectFilter(filter.filter));
			}
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void cbCompound_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			OnOriginalBitmapChanged(this.image, null);
			this.preview.Flip((RotateFlipType)this.cbCompound.SelectedItem);
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void btnOrigin_VisibleChanged(object sender, System.EventArgs e)
		{
			if (btnOrigin.Visible)
			{
				btnOrigin.Location = new Point((int)(this.previewBox.Location.X + this.previewBox.Width / 2.0),
					                           (int)(this.previewBox.Location.Y + this.previewBox.Height / 2.0));
			}
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void btnOrigin_MouseMove(object sender, System.Windows.Forms.MouseEventArgs e)
		{
			if (e.Button == MouseButtons.Left)
			{
				Point point = this.PointToClient(btnOrigin.PointToScreen(new Point(e.X, e.Y)));
				point.X -= btnOrigin.Width/2;
				point.Y -= btnOrigin.Height/2;

				if (point.X >= this.previewBox.Location.X && point.Y >= this.previewBox.Location.Y
					&& (point.X + btnOrigin.Width) <= (this.previewBox.Location.X + this.previewBox.Width)
					&& (point.Y + btnOrigin.Height) <= (this.previewBox.Location.Y + this.previewBox.Height))
				{
					btnOrigin.Location = point;
				}
			}
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void btnOrigin_MouseUp(object sender, System.Windows.Forms.MouseEventArgs e)
		{
			if (e.Button == MouseButtons.Right)
			{
				btnOrigin.Location = new Point((int)(this.previewBox.Location.X + this.previewBox.Width / 2.0),
					                           (int)(this.previewBox.Location.Y + this.previewBox.Height / 2.0));
			}
			else
			{
				udAngle.Value = 0;
			}
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void btnFillcolor_Click(object sender, System.EventArgs e)
		{
			ColorDialog dialog = new ColorDialog();
			dialog.Color = btnFillcolor.BackColor;
			if (dialog.ShowDialog(this) == DialogResult.OK)
			{
				btnFillcolor.BackColor = dialog.Color;
				udAngle_ValueChanged(udAngle, e);
			}
		}
	}
}