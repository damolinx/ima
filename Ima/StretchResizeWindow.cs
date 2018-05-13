using Ima.ImageOps;
using System;
using System.Drawing;

namespace Ima
{
	/// <summary>
	/// Summary description for StretchResizeWindow.
	/// </summary>
	public class StretchResizeWindow : Ima.Controls.ActivePreviewWindow
	{
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.NumericUpDown udVerticalSize;
		private System.Windows.Forms.NumericUpDown udHorizontalSize;
		private System.Windows.Forms.CheckBox cbRatio;
		private System.Windows.Forms.Label lblOriginal;
		private System.Windows.Forms.Label lblNew;
		private System.Windows.Forms.GroupBox groupBox1;
		private System.Windows.Forms.Label label5;
		private System.Windows.Forms.Label label6;
		private System.Windows.Forms.GroupBox groupBox3;
		private System.Windows.Forms.Label label4;

		public StretchResizeWindow(ImageWrapper bitmap) 
            : this(BASE_WIDTH, BASE_HEIGHT, bitmap)
		{
		}

		public StretchResizeWindow(int w, int h, ImageWrapper bitmap) 
            : base("Stretch/Resize", w, h, bitmap)
		{
			InitializeComponent();
			this.lblNew.Text = this.lblOriginal.Text = this.image.Width + " x " + this.image.Height + " pixels";
		}
		
		#region Windows Form Designer generated code
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
            this.udVerticalSize = new System.Windows.Forms.NumericUpDown();
            this.udHorizontalSize = new System.Windows.Forms.NumericUpDown();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.cbRatio = new System.Windows.Forms.CheckBox();
            this.lblOriginal = new System.Windows.Forms.Label();
            this.lblNew = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            ((System.ComponentModel.ISupportInitialize)(this.previewBox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.imageBox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.udVerticalSize)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.udHorizontalSize)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.SuspendLayout();
            // 
            // udVerticalSize
            // 
            this.udVerticalSize.Location = new System.Drawing.Point(158, 27);
            this.udVerticalSize.Maximum = new decimal(new int[] {
            100000,
            0,
            0,
            0});
            this.udVerticalSize.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.udVerticalSize.Name = "udVerticalSize";
            this.udVerticalSize.Size = new System.Drawing.Size(130, 29);
            this.udVerticalSize.TabIndex = 1;
            this.udVerticalSize.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.udVerticalSize.ThousandsSeparator = true;
            this.udVerticalSize.Value = new decimal(new int[] {
            100,
            0,
            0,
            0});
            this.udVerticalSize.ValueChanged += new System.EventHandler(this.udVerticalSize_ValueChanged);
            // 
            // udHorizontalSize
            // 
            this.udHorizontalSize.Location = new System.Drawing.Point(158, 68);
            this.udHorizontalSize.Maximum = new decimal(new int[] {
            100000,
            0,
            0,
            0});
            this.udHorizontalSize.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.udHorizontalSize.Name = "udHorizontalSize";
            this.udHorizontalSize.Size = new System.Drawing.Size(130, 29);
            this.udHorizontalSize.TabIndex = 4;
            this.udHorizontalSize.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.udHorizontalSize.ThousandsSeparator = true;
            this.udHorizontalSize.Value = new decimal(new int[] {
            100,
            0,
            0,
            0});
            this.udHorizontalSize.ValueChanged += new System.EventHandler(this.udHorizontalSize_ValueChanged);
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(29, 27);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(101, 27);
            this.label1.TabIndex = 0;
            this.label1.Text = "Vertical";
            // 
            // label2
            // 
            this.label2.Location = new System.Drawing.Point(29, 68);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(101, 27);
            this.label2.TabIndex = 3;
            this.label2.Text = "Horizontal";
            // 
            // label3
            // 
            this.label3.Location = new System.Drawing.Point(302, 68);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(29, 27);
            this.label3.TabIndex = 5;
            this.label3.Text = "%";
            // 
            // label4
            // 
            this.label4.Location = new System.Drawing.Point(302, 27);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(29, 27);
            this.label4.TabIndex = 2;
            this.label4.Text = "%";
            // 
            // cbRatio
            // 
            this.cbRatio.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.cbRatio.Checked = true;
            this.cbRatio.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbRatio.Location = new System.Drawing.Point(29, 122);
            this.cbRatio.Name = "cbRatio";
            this.cbRatio.Size = new System.Drawing.Size(259, 34);
            this.cbRatio.TabIndex = 6;
            this.cbRatio.Text = "Mantain Aspect &Ratio";
            // 
            // lblOriginal
            // 
            this.lblOriginal.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblOriginal.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.lblOriginal.Location = new System.Drawing.Point(202, 41);
            this.lblOriginal.Name = "lblOriginal";
            this.lblOriginal.Size = new System.Drawing.Size(187, 27);
            this.lblOriginal.TabIndex = 1;
            this.lblOriginal.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblNew
            // 
            this.lblNew.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblNew.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.lblNew.Location = new System.Drawing.Point(202, 95);
            this.lblNew.Name = "lblNew";
            this.lblNew.Size = new System.Drawing.Size(187, 27);
            this.lblNew.TabIndex = 3;
            this.lblNew.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.udHorizontalSize);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.udVerticalSize);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.cbRatio);
            this.groupBox1.Location = new System.Drawing.Point(9, 445);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(480, 162);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            // 
            // label5
            // 
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.label5.Location = new System.Drawing.Point(58, 95);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(122, 27);
            this.label5.TabIndex = 2;
            this.label5.Text = "Resized:";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label6
            // 
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.label6.Location = new System.Drawing.Point(58, 41);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(122, 27);
            this.label6.TabIndex = 0;
            this.label6.Text = "Original:";
            this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // groupBox3
            // 
            this.groupBox3.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox3.Controls.Add(this.lblNew);
            this.groupBox3.Controls.Add(this.label5);
            this.groupBox3.Controls.Add(this.label6);
            this.groupBox3.Controls.Add(this.lblOriginal);
            this.groupBox3.Location = new System.Drawing.Point(511, 445);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(480, 162);
            this.groupBox3.TabIndex = 1;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Dimensions";
            // 
            // StretchResizeWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(168F, 168F);
            this.ClientSize = new System.Drawing.Size(1000, 686);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.groupBox3);
            this.Name = "StretchResizeWindow";
            this.Controls.SetChildIndex(this.imageBox, 0);
            this.Controls.SetChildIndex(this.previewBox, 0);
            this.Controls.SetChildIndex(this.groupBox3, 0);
            this.Controls.SetChildIndex(this.groupBox1, 0);
            ((System.ComponentModel.ISupportInitialize)(this.previewBox)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.imageBox)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.udVerticalSize)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.udHorizontalSize)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox3.ResumeLayout(false);
            this.ResumeLayout(false);

		}
		#endregion

		/// <summary>
		/// 
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void udVerticalSize_ValueChanged(object sender, System.EventArgs e)
		{
			double f1, f2;
			if (cbRatio.Checked)
			{
				udHorizontalSize.Value = udVerticalSize.Value;
				f2 = f1 = (double)(udVerticalSize.Value) / 100d;
			}
			else
			{
				f1 = (double)(udVerticalSize.Value) / 100d;
				f2 = (double)(udHorizontalSize.Value) / 100d;
			}
			this.previewBox.Image = this.image.GetThumbnail((int)(f1 * this.preview.Width), (int)(f2 * this.preview.Height), false);
			this.lblNew.Text = (int)(f1 * this.image.Width) + " x " + (int)(f2 * this.image.Height) + " pixels";
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void udHorizontalSize_ValueChanged(object sender, System.EventArgs e)
		{
			double f1, f2;
			if (cbRatio.Checked)
			{
				udVerticalSize.Value = udHorizontalSize.Value;
				f2 = f1 = (double)(udHorizontalSize.Value) / 100d;
			}
			else
			{
				f1 = (double)(udVerticalSize.Value) / 100d;
				f2 = (double)(udHorizontalSize.Value) / 100d;
			}
			this.previewBox.Image = this.image.GetThumbnail((int)(f1 * this.preview.Width), (int)(f2 * this.preview.Height), false);
			this.lblNew.Text = (int)(f1 * this.image.Width) + " x " + (int)(f2 * this.image.Height) + " pixels";
		}

		protected override void Apply()
		{
			double f1, f2;
			f1 = (double)(udVerticalSize.Value)	  / 100d;
			f2 = (double)(udHorizontalSize.Value) / 100d;
			this.image.Resize(f1, f2);
            this.Close();
		}
	}
}