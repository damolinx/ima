using Ima.ImageOps;
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
        public BasicFilterWindow(ImageWrapper image, FilterBase filter) 
            : this(image, filter, BASE_WIDTH, BASE_HEIGHT)
        {
            //
            // TODO: Add constructor logic here
            //
        }

        /// <summary>
        /// 
        /// </summary>
        public BasicFilterWindow(ImageWrapper image, FilterBase filter, int w, int h) 
            : base(filter.Name, w, h, image)
        {
            this.filter = filter;
            InitializeComponent();
            this.lblHelp.Text = filter.Description;
        }
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

        #endregion

        #region Designer generated code
        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.lblHelp = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.previewBox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.imageBox)).BeginInit();
            this.SuspendLayout();
            // 
            // lblHelp
            // 
            this.lblHelp.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblHelp.Location = new System.Drawing.Point(9, 454);
            this.lblHelp.Name = "lblHelp";
            this.lblHelp.Size = new System.Drawing.Size(982, 112);
            this.lblHelp.TabIndex = 6;
            // 
            // BasicFilterWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(168F, 168F);
            this.ClientSize = new System.Drawing.Size(1000, 636);
            this.Controls.Add(this.lblHelp);
            this.Name = "BasicFilterWindow";
            this.Controls.SetChildIndex(this.lblHelp, 0);
            this.Controls.SetChildIndex(this.previewBox, 0);
            this.Controls.SetChildIndex(this.imageBox, 0);
            ((System.ComponentModel.ISupportInitialize)(this.previewBox)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.imageBox)).EndInit();
            this.ResumeLayout(false);

        }
        #endregion

        #region Events
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected override void Apply()
        {
            Cursor cursor = Cursor.Current;
            Cursor.Current = Cursors.WaitCursor;

            unsafe
            {
                if (this.filter.Direct)
                {
                    this.image.Apply(filter.Name, new ImageDirectFilter(this.filter.Filter));
                }
                else
                {
                    this.image.Apply(filter.Name, filter.Border, new ImageIndirectFilter(this.filter.Filter));
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
                if (this.Visible)
                {
                    if (this.image != null)
                    {
                        OnOriginalBitmapChanged(this.image, null);

                        ///Apply Filter
                        Cursor cursor = Cursor.Current;
                        Cursor.Current = Cursors.WaitCursor;
                        unsafe
                        {
                            if (this.filter.Direct)
                            {
                                this.preview.Apply(filter.Name, new ImageDirectFilter(this.filter.Filter));
                            }
                            else
                            {
                                this.preview.Apply(filter.Name, filter.Border, new ImageIndirectFilter(this.filter.Filter));
                            }
                        }
                        Cursor.Current = cursor;
                    }
                }
            }
        }

        #endregion
    }
}

