using Ima.ImageOps;
using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Imaging;
using System.Windows.Forms;

namespace Ima.Controls
{
    partial class ActivePreviewWindow
    {
        protected System.Windows.Forms.PictureBox previewBox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.ComponentModel.IContainer components = null;

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
            this.previewBox = new System.Windows.Forms.PictureBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.imageBox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.previewBox)).BeginInit();
            this.SuspendLayout();
            // 
            // imageBox
            // 
            this.imageBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)));
            this.imageBox.BackColor = System.Drawing.SystemColors.AppWorkspace;
            this.imageBox.Location = new System.Drawing.Point(9, 41);
            this.imageBox.Size = new System.Drawing.Size(480, 400);
            // 
            // previewBox
            // 
            this.previewBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.previewBox.BackColor = System.Drawing.SystemColors.AppWorkspace;
            this.previewBox.Location = new System.Drawing.Point(511, 41);
            this.previewBox.Name = "previewBox";
            this.previewBox.Size = new System.Drawing.Size(480, 400);
            this.previewBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.previewBox.TabIndex = 1;
            this.previewBox.TabStop = false;
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(14, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(475, 39);
            this.label1.TabIndex = 4;
            this.label1.Text = "Original";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label2
            // 
            this.label2.Location = new System.Drawing.Point(511, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(480, 39);
            this.label2.TabIndex = 5;
            this.label2.Text = "Preview";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // ActivePreviewWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(168F, 168F);
            this.ClientSize = new System.Drawing.Size(1000, 536);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.previewBox);
            this.Controls.Add(this.label1);
            this.Name = "ActivePreviewWindow";
            this.Controls.SetChildIndex(this.label1, 0);
            this.Controls.SetChildIndex(this.previewBox, 0);
            this.Controls.SetChildIndex(this.imageBox, 0);
            this.Controls.SetChildIndex(this.label2, 0);
            ((System.ComponentModel.ISupportInitialize)(this.imageBox)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.previewBox)).EndInit();
            this.ResumeLayout(false);

        }
        #endregion

    }
}