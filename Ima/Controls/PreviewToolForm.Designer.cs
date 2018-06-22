using Ima.ImageOps;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace Ima.Controls
{
    /// <summary>
    /// Summary description for PreviewForm.
    /// </summary>
    partial class PreviewToolForm
    {
        protected PictureBox imageBox;

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.imageBox = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.imageBox)).BeginInit();
            this.SuspendLayout();
            // 
            // imageBox
            // 
            this.imageBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.imageBox.Location = new System.Drawing.Point(9, 8);
            this.imageBox.Name = "imageBox";
            this.imageBox.Size = new System.Drawing.Size(915, 455);
            this.imageBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.imageBox.TabIndex = 0;
            this.imageBox.TabStop = false;
            // 
            // PreviewToolForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(168F, 168F);
            this.ClientSize = new System.Drawing.Size(936, 536);
            this.Controls.Add(this.imageBox);
            this.Name = "PreviewToolForm";
            this.Closing += this.PreviewForm_Closing;
            this.VisibleChanged += this.PreviewForm_VisibleChanged;
            this.Controls.SetChildIndex(this.imageBox, 0);
            ((System.ComponentModel.ISupportInitialize)(this.imageBox)).EndInit();
            this.ResumeLayout(false);

        }
        #endregion
    }
}