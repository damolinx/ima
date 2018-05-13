using Ima.ImageOps;
using System;
using System.Windows.Forms;

namespace Ima.Controls
{
    /// <summary>
    /// 
    /// </summary>
    partial class ThumbnailForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            ((System.ComponentModel.ISupportInitialize)(this.imageBox)).BeginInit();
            this.SuspendLayout();
            // 
            // imageBox
            // 
            this.imageBox.Size = new System.Drawing.Size(918, 455);
            // 
            // ThumbnailForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(168F, 168F);
            this.ClientSize = new System.Drawing.Size(936, 536);
            this.Name = "ThumbnailForm";
            ((System.ComponentModel.ISupportInitialize)(this.imageBox)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
    }
}
