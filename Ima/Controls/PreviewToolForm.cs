using Ima.ImageOps;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace Ima.Controls
{
    /// <summary>
    /// Summary description for PreviewForm.
    /// </summary>
    public partial class PreviewToolForm : Ima.Controls.ToolForm
    {
        /// <summary>
        /// Default Width
        /// </summary>
        protected const int BASE_WIDTH = 240;

        /// <summary>
        /// Default Height
        /// </summary>
        protected const int BASE_HEIGHT = 180;

        /// <summary>
        /// Reference to the bitmap to modify
        /// </summary>
        protected ImageWrapper image;

        #region Constructors

        /// <summary>
        /// Constructor
        /// </summary>
        public PreviewToolForm()
            : this("Preview", null)
        {
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="title">Tool Window title (also used as internal name)</param>
        /// <param name="bitmap">Default Image Wrapper to use</param>
        public PreviewToolForm(string title, ImageWrapper imagewrapper)
            : this(title, BASE_WIDTH, BASE_HEIGHT, imagewrapper)
        {
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="title">Tool Window title (also used as name)</param>
        /// <param name="w">Tool Window Width</param>
        /// <param name="h">Tool Window Height</param>
        /// <param name="bitmap">Default Image Wrapper to use</param>
        public PreviewToolForm(string title, int w, int h, ImageWrapper imagewrapper)
        {
            InitializeComponent();
            this.Text = title;
            this.ClientSize = new Size(w + 2 * this.imageBox.Location.X,
                h + 2 * this.imageBox.Location.Y);

            this.imageBox.Size = new Size(w, h);

            if (imagewrapper != null)
            {
                this.image = imagewrapper;
                this.image.Changed += OnOriginalBitmapChanged;
            }
        }
        #endregion

        /// <summary>
        /// If image is changed, this event allows to refresh view
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
        public virtual void OnOriginalBitmapChanged(object sender, ImageChangedEventArgs args)
        {
            Rectangle rec = this.image.ActiveRegion;
            double factor = Math.Max(1,
                Math.Max(((double)rec.Width) / imageBox.Width,
                         ((double)rec.Height) / imageBox.Height));

            this.imageBox.Image = this.image.GetThumbnail((int)(this.image.Width / factor),
                                                                  (int)(this.image.Height / factor), true);
        }

        /// <summary>
        /// Nice clean-up
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void PreviewForm_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (this.image != null)
            {
                this.image.Changed -= OnOriginalBitmapChanged;
            }
        }

        /// <summary>
        /// Not-so-nice trick to generate the thumbnail in a lazy way
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected virtual void PreviewForm_VisibleChanged(object sender, System.EventArgs e)
        {
            if (!this.DesignMode)
            {
                if (this.Visible)
                {
                    OnOriginalBitmapChanged(this.image, null);
                }
            }
        }
    }
}