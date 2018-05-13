using Ima.ImageOps;
using System;
using System.IO;
using System.Windows.Forms;

namespace Ima.Controls
{
    public partial class ThumbnailForm : Ima.Controls.PreviewToolForm
    {
        /// <summary>
        /// Constructor
        /// </summary>
        private ThumbnailForm(string title, ImageWrapper imagewrapper)
            : base(title, imagewrapper)
        {
            InitializeComponent();
            ((Button)this.AcceptButton).Visible = false;
            ((Button)this.CancelButton).Text = "&Close";
        }

        public static ThumbnailForm ShowView(Form owner, ImageWrapper image)
        {
            var form = new ThumbnailForm($"Thumbnail: {Path.GetFileName(image.Filename)}", image)
            {
                Owner = owner,
                TopMost = true
            };
            form.Show();

            return form;
        }

        /// <summary>
        /// If image is changed, this event allows to refresh view
        /// </summary>
        public override void OnOriginalBitmapChanged(object sender, ImageChangedEventArgs args)
        {
            double factor = Math.Max(1,
                Math.Max(((double)this.image.Width) / imageBox.Width,
                ((double)this.image.Height) / imageBox.Height));

            this.imageBox.Image = this.image.GetThumbnail((int)(this.image.Width / factor),
                (int)(this.image.Height / factor), true);
        }
    }
}
