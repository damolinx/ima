using Ima.ImageOps;
using System;
using System.Drawing;

namespace Ima.Controls
{
    public partial class ActivePreviewWindow : Ima.Controls.PreviewToolForm
    {
        /// <summary>
        /// Working Image
        /// </summary>
        protected ImageWrapper preview;

        public ActivePreviewWindow()
            : this(string.Empty, null)
        {
        }

        public ActivePreviewWindow(string title, ImageWrapper bitmap)
            : this(title, BASE_WIDTH, BASE_HEIGHT, bitmap)
        {
        }

        public ActivePreviewWindow(string title, int w, int h, ImageWrapper bitmap)
            : base(title, w, h, bitmap)
        {
            // This call is required by the Windows Form Designer.
            InitializeComponent();
        }

        public override void OnOriginalBitmapChanged(object sender, ImageChangedEventArgs args)
        {
            Rectangle rec = this.image.ActiveRegion;

            double factor = Math.Max(1.0,
                Math.Max(((double)rec.Width) / imageBox.Width,
                ((double)rec.Height) / imageBox.Height));

            this.preview = new ImageWrapper("Preview: " + System.IO.Path.GetExtension(this.image.Filename),
                (Bitmap)this.image.GetThumbnail(
                (int)(rec.Width / factor),
                (int)(rec.Height / factor), true));
            this.preview.Changed += OnPreviewBitmapChanged;

            this.imageBox.Image = new Bitmap(this.preview.Bitmap);
            this.previewBox.Image = this.preview.Bitmap;
        }

        public virtual void OnPreviewBitmapChanged(object sender, ImageChangedEventArgs args)
        {
            this.previewBox.Image = this.preview.Bitmap;
        }
    }
}