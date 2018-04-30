using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace Ima
{

	public class AboutForm : System.Windows.Forms.Form
	{
		private static AboutForm instance;

		private System.Windows.Forms.Button btnOK;
		private System.Windows.Forms.Label lblApplication;
		private System.Windows.Forms.Label lblVersion;
		private System.Windows.Forms.PictureBox pictureBox;
		private System.Windows.Forms.TextBox txtGeneral;
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		/// Constructor
		/// </summary>
		private AboutForm()
		{
			// This call is required by the Windows Form Designer.
			InitializeComponent();

			// TODO: Add any initialization after the InitializeComponent call
		}

		/// <summary>
		/// 
		/// </summary>
		/// <returns></returns>
		public static AboutForm Instance()
		{
			if (AboutForm.instance == null)
			{
				AboutForm.instance = new AboutForm();
			}
			return AboutForm.instance;
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

		#region Designer generated code
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(AboutForm));
			this.lblApplication = new System.Windows.Forms.Label();
			this.btnOK = new System.Windows.Forms.Button();
			this.lblVersion = new System.Windows.Forms.Label();
			this.pictureBox = new System.Windows.Forms.PictureBox();
			this.txtGeneral = new System.Windows.Forms.TextBox();
			this.SuspendLayout();
			// 
			// lblApplication
			// 
			this.lblApplication.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
				| System.Windows.Forms.AnchorStyles.Right)));
			this.lblApplication.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.lblApplication.Location = new System.Drawing.Point(112, 16);
			this.lblApplication.Name = "lblApplication";
			this.lblApplication.Size = new System.Drawing.Size(272, 24);
			this.lblApplication.TabIndex = 3;
			this.lblApplication.Text = "Application_Name";
			// 
			// btnOK
			// 
			this.btnOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.btnOK.BackColor = System.Drawing.SystemColors.Control;
			this.btnOK.DialogResult = System.Windows.Forms.DialogResult.OK;
			this.btnOK.Location = new System.Drawing.Point(320, 184);
			this.btnOK.Name = "btnOK";
			this.btnOK.Size = new System.Drawing.Size(72, 24);
			this.btnOK.TabIndex = 0;
			this.btnOK.Text = "OK";
			// 
			// lblVersion
			// 
			this.lblVersion.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
				| System.Windows.Forms.AnchorStyles.Right)));
			this.lblVersion.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.lblVersion.Location = new System.Drawing.Point(112, 32);
			this.lblVersion.Name = "lblVersion";
			this.lblVersion.Size = new System.Drawing.Size(272, 24);
			this.lblVersion.TabIndex = 2;
			this.lblVersion.Text = "Application_Version";
			// 
			// pictureBox
			// 
			this.pictureBox.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
			this.pictureBox.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox.Image")));
			this.pictureBox.Location = new System.Drawing.Point(10, 8);
			this.pictureBox.Name = "pictureBox";
			this.pictureBox.Size = new System.Drawing.Size(94, 200);
			this.pictureBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
			this.pictureBox.TabIndex = 3;
			this.pictureBox.TabStop = false;
			// 
			// txtGeneral
			// 
			this.txtGeneral.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
				| System.Windows.Forms.AnchorStyles.Left) 
				| System.Windows.Forms.AnchorStyles.Right)));
			this.txtGeneral.Location = new System.Drawing.Point(112, 56);
			this.txtGeneral.Multiline = true;
			this.txtGeneral.Name = "txtGeneral";
			this.txtGeneral.ReadOnly = true;
			this.txtGeneral.ScrollBars = System.Windows.Forms.ScrollBars.Both;
			this.txtGeneral.Size = new System.Drawing.Size(280, 120);
			this.txtGeneral.TabIndex = 1;
			this.txtGeneral.Text = "";
			// 
			// AboutForm
			// 
			this.AcceptButton = this.btnOK;
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.BackColor = System.Drawing.SystemColors.Control;
			this.CancelButton = this.btnOK;
			this.ClientSize = new System.Drawing.Size(402, 216);
			this.Controls.Add(this.txtGeneral);
			this.Controls.Add(this.pictureBox);
			this.Controls.Add(this.lblVersion);
			this.Controls.Add(this.btnOK);
			this.Controls.Add(this.lblApplication);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "AboutForm";
			this.ShowInTaskbar = false;
			this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "About";
			this.Load += new System.EventHandler(this.AboutForm_Load);
			this.Closed += new System.EventHandler(this.AboutForm_Closed);
			this.ResumeLayout(false);

		}
		#endregion

		/// <summary>
		/// Sets relevant contents
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void AboutForm_Load(object sender, System.EventArgs e)
		{
			this.Text                = "About " + Configuration.APPLICATION_NAME;
			this.lblApplication.Text = Configuration.APPLICATION_NAME;
			this.lblVersion.Text     = "Version " + Configuration.APPLICATION_VERSION;
		}

		/// <summary>
		/// Deals with instance disposition
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void AboutForm_Closed(object sender, System.EventArgs e)
		{
			AboutForm.instance.Dispose();
			AboutForm.instance = null;
		}
	}
}

