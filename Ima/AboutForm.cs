using System.Threading;

namespace Ima
{

    public class AboutForm : System.Windows.Forms.Form
    {
        private static AboutForm _instance;

        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.Label lblApplication;
        private System.Windows.Forms.Label lblVersion;
        private System.Windows.Forms.TextBox txtGeneral;
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Constructor
        /// </summary>
        private AboutForm()
        {
            // This call is required by the Windows Form Designer.
            InitializeComponent();
        }

        public static AboutForm Instance => LazyInitializer.EnsureInitialized(ref _instance, () => new AboutForm());

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (components != null)
                {
                    components.Dispose();
                }
            }
            base.Dispose(disposing);
        }

        #region Designer generated code
        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.lblApplication = new System.Windows.Forms.Label();
            this.btnOK = new System.Windows.Forms.Button();
            this.lblVersion = new System.Windows.Forms.Label();
            this.txtGeneral = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // lblApplication
            // 
            this.lblApplication.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblApplication.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblApplication.Location = new System.Drawing.Point(12, 27);
            this.lblApplication.Name = "lblApplication";
            this.lblApplication.Size = new System.Drawing.Size(586, 41);
            this.lblApplication.TabIndex = 3;
            this.lblApplication.Text = "Application_Name";
            // 
            // btnOK
            // 
            this.btnOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnOK.BackColor = System.Drawing.SystemColors.Control;
            this.btnOK.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnOK.Location = new System.Drawing.Point(468, 361);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(130, 41);
            this.btnOK.TabIndex = 0;
            this.btnOK.Text = "OK";
            this.btnOK.UseVisualStyleBackColor = false;
            // 
            // lblVersion
            // 
            this.lblVersion.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblVersion.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblVersion.Location = new System.Drawing.Point(12, 54);
            this.lblVersion.Name = "lblVersion";
            this.lblVersion.Size = new System.Drawing.Size(586, 41);
            this.lblVersion.TabIndex = 2;
            this.lblVersion.Text = "Application_Version";
            // 
            // txtGeneral
            // 
            this.txtGeneral.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtGeneral.Location = new System.Drawing.Point(12, 95);
            this.txtGeneral.Multiline = true;
            this.txtGeneral.Name = "txtGeneral";
            this.txtGeneral.ReadOnly = true;
            this.txtGeneral.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.txtGeneral.Size = new System.Drawing.Size(586, 253);
            this.txtGeneral.TabIndex = 1;
            // 
            // AboutForm
            // 
            this.AcceptButton = this.btnOK;
            this.AutoScaleDimensions = new System.Drawing.SizeF(168F, 168F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.CancelButton = this.btnOK;
            this.ClientSize = new System.Drawing.Size(616, 416);
            this.Controls.Add(this.txtGeneral);
            this.Controls.Add(this.lblVersion);
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.lblApplication);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "AboutForm";
            this.ShowInTaskbar = false;
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "About";
            this.Closed += new System.EventHandler(this.AboutForm_Closed);
            this.Load += new System.EventHandler(this.AboutForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }
        #endregion

        /// <summary>
        /// Sets relevant contents
        /// </summary>
        private void AboutForm_Load(object sender, System.EventArgs e)
        {
            this.lblApplication.Text = Program.APPLICATION_NAME;
            this.lblVersion.Text = "Version " + Program.APPLICATION_VERSION;
        }

        /// <summary>
        /// Deals with instance disposition
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void AboutForm_Closed(object sender, System.EventArgs e)
        {
            AboutForm._instance.Dispose();
            AboutForm._instance = null;
        }
    }
}

