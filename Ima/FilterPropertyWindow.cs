using Ima.ImageOps;
using System;
using System.Drawing;
using System.Reflection;
using System.Windows.Forms;

namespace Ima
{
    /// <summary>
    /// Summary description for FilterPropertyWindow.
    /// </summary>
    public class FilterPropertyWindow : Ima.Controls.ActivePreviewWindow
    {
        private TrackBar trackBar;
        private NumericUpDown udText;
        private GroupBox groupBox;
        private Label lblHelp;
        private FilterBase filter;
        private PropertyInfo property;

        public FilterPropertyWindow(ImageWrapper image, FilterBase filter, string propertyName, int min, int max)
            : base(filter.Name, BASE_WIDTH, BASE_HEIGHT, image)
        {
            this.filter = filter;
            this.property = filter.GetType().GetProperty(propertyName);

            InitializeComponent();

            this.groupBox.Text = propertyName;
            this.lblHelp.Text = "TODO: add description";
            this.udText.Minimum = this.trackBar.Minimum = min;
            this.udText.Maximum = this.trackBar.Maximum = max;
            this.udText.Value = this.trackBar.Value = this.PropertyValue;
            this.trackBar.TickFrequency = (max - min) / 10;

            ///Add Events after adjustment
            this.udText.ValueChanged += this.udText_ValueChanged;
            this.trackBar.ValueChanged += this.trackBar_ValueChanged;
        }

        public int PropertyValue
        {
            get { return (int)this.property.GetValue(this.filter); }
            set { this.property.SetValue(this.filter, value); }
        }


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
            this.previewBox.Location = new System.Drawing.Point(548, 41);
            this.previewBox.Size = new System.Drawing.Size(432, 304);
            // 
            // imageBox
            // 
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
            this.groupBox.Size = new System.Drawing.Size(972, 211);
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
            this.lblHelp.Size = new System.Drawing.Size(943, 89);
            this.lblHelp.TabIndex = 3;
            // 
            // udText
            // 
            this.udText.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.udText.Location = new System.Drawing.Point(842, 27);
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
            this.trackBar.Size = new System.Drawing.Size(813, 80);
            this.trackBar.TabIndex = 1;
            // 
            // FilterPropertyWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(168F, 168F);
            this.ClientSize = new System.Drawing.Size(1000, 636);
            this.Controls.Add(this.groupBox);
            this.Name = "FilterPropertyWindow";
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

        private void udText_ValueChanged(object sender, System.EventArgs e)
        {
            this.PropertyValue = this.trackBar.Value = (int)udText.Value;
            OnOriginalBitmapChanged(this.image, null);
            unsafe
            {
                if (this.filter.Direct)
                {
                    this.preview.Apply(this.filter.Name, new ImageDirectFilter(this.filter.Filter));
                }
                else
                {
                    this.preview.Apply(this.filter.Name, filter.Border, new ImageIndirectFilter(this.filter.Filter));
                }
            }
        }

        private void trackBar_ValueChanged(object sender, System.EventArgs e)
        {
            this.udText.Value = trackBar.Value;
        }

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

        protected void previewBox_Click(object sender, System.EventArgs e)
        {
            this.udText_ValueChanged(this.udText, EventArgs.Empty);
        }

        protected override void PreviewForm_VisibleChanged(object sender, System.EventArgs e)
        {
            if (!this.DesignMode)
            {
                if (this.Visible)
                {
                    OnOriginalBitmapChanged(this.image, null);
                    udText_ValueChanged(udText, EventArgs.Empty);
                }
            }
        }
    }
}