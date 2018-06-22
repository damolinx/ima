using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;

namespace Ima.ImageOps
{
    #region Delegates

    /// <summary>
    /// Function prototype to obtain  a pixel
    /// </summary>
    unsafe public delegate PixelData* PixelGet(int x, int y);

    /// <summary>
    /// Notification function for an image change
    /// </summary>
    public delegate void ImageChangedEventHandler(object sender, ImageChangedEventArgs e);

    /// <summary>
    /// Direct Filter
    /// </summary>
    unsafe public delegate void ImageDirectFilter(PixelData* pPixelData);

    /// <summary>
    /// Indirect Filter
    /// </summary>
    unsafe public delegate void ImageIndirectFilter(int x, int y, PixelGet getPixel, PixelData* pPixelTarget, int x0, int y0, int x1, int y1);

    #endregion

    /// <summary>
    /// Summary description for ImageWrapper.
    /// </summary>
    public class ImageWrapper
    {
        #region Members
        /// <summary>
        /// Internal Bitmap
        /// </summary>
        private Bitmap image;

        /// <summary>
        /// Original Format
        /// </summary>
        private ImageFormat format;

        /// <summary>
        /// Active Bitmap Region to work on
        /// </summary>
        private Rectangle activeRegion = Rectangle.Empty;

        #endregion

        public ImageWrapper(string filename)
        {
            this.Filename = filename;
            var stream = new FileStream(filename, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
            this.Bitmap = (Bitmap)Image.FromStream(stream);
        }

        public ImageWrapper(string filename, Bitmap bitmap)
        {
            this.Filename = filename;
            this.Bitmap = bitmap;
        }

        /// <summary>
        /// Event fired when any of the layers changed
        /// </summary>
        public event ImageChangedEventHandler Changed;

        protected virtual void OnChanged(ImageChangedEventArgs e)
        {
            Changed?.Invoke(this, e);
        }

        #region Properties

        /// <summary>
        /// Returns internal image size
        /// </summary>
        /// <returns>Size</returns>
        public Size Size
        {
            get
            {
                return this.image.Size;
            }
        }

        /// <summary>
        /// Returns internal image Width
        /// </summary>
        /// <returns>Width</returns>
        public int Width
        {
            get
            {
                return this.image.Width;
            }
        }

        /// <summary>
        /// Returns internal image Height
        /// </summary>
        /// <returns>Height</returns>
        public int Height
        {
            get
            {
                return this.image.Height;
            }
        }

        public Point PixelSize
        {
            get
            {
                var unit = GraphicsUnit.Pixel;
                var bounds = this.image.GetBounds(ref unit);
                return new Point((int)bounds.Width, (int)bounds.Height);
            }
        }

        /// <summary>
        /// Sets current active region
        /// </summary>
        public Rectangle ActiveRegion
        {
            get
            {
                return this.activeRegion;
            }
            set
            {
                this.activeRegion = (value != Rectangle.Empty) ? value :
                    new Rectangle(0, 0, this.image.Width, this.image.Height);
            }
        }

        public string Filename
        {
            get; set;
        }

        /// <summary>
        /// Bitmap
        /// </summary>
        public Bitmap Bitmap
        {
            get
            {
                return this.image;
            }

            set
            {
                Bitmap clone = null;
                if (this.image != null)
                    clone = new Bitmap(this.image);

                this.image = value;
                this.format = this.image.RawFormat;
                this.ActiveRegion = Rectangle.Empty;

                if (clone != null)
                    OnChanged(new ImageChangedEventArgs(clone));
            }
        }
        #endregion

        #region Basic Operations

        /// <summary>
        /// 
        /// </summary>
        /// <param name="format"></param>
        public void Convert(ImageFormat format)
        {
            try
            {
                System.IO.Stream stream = new MemoryStream();
                this.image.Save(stream, format);
                this.Bitmap = new Bitmap(stream);
            }
            catch (Exception e)
            {
                Console.Out.WriteLine(e.Message);
                Console.Out.WriteLine(e.TargetSite);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="type"></param>
        public void Flip(RotateFlipType type)
        {
            if (type != RotateFlipType.RotateNoneFlipNone)
            {
                Bitmap clone = new Bitmap(this.image);
                ///TODO: The RotateFlip transformation converts bitmaps to memoryBmp?
                this.image.RotateFlip(type);
                Convert(this.format);
                OnChanged(new ImageChangedEventArgs("Flip", clone));
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="x_ratio"></param>
        /// <param name="y_ratio"></param>
        public void Resize(double x_ratio, double y_ratio)
        {
            int width = (int)(x_ratio * this.Width);
            int height = (int)(y_ratio * this.Height);
            if (width != 0 && height != 0)
            {
                ///http://www.thecodeproject.com/cs/media/bitmapmanip.asp				
                Bitmap resized = new Bitmap(width, height, PixelFormat.Format32bppArgb);
                Graphics graphics = Graphics.FromImage(resized);

                ///High-quality resizing
                graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
                graphics.ScaleTransform((float)x_ratio, (float)y_ratio);

                Rectangle drawRect = new Rectangle(0, 0, this.image.Size.Width, this.image.Size.Height);
                graphics.DrawImage(this.image, drawRect, drawRect, GraphicsUnit.Pixel);
                graphics.Dispose();

                Bitmap clone = new Bitmap(this.image);
                this.Bitmap = resized;
                Convert(this.format);
                OnChanged(new ImageChangedEventArgs((x_ratio == y_ratio) ? "Resize" : "Stretch", clone));
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="cropRec"></param>
        public void Crop(Rectangle cropRec)
        {
            if (cropRec.Width != 0 && cropRec.Height != 0)
            {
                ///http://www.thecodeproject.com/cs/media/bitmapmanip.asp				
                Bitmap cropped = new Bitmap(cropRec.Width, cropRec.Height, PixelFormat.Format32bppArgb);
                Graphics graphics = Graphics.FromImage(cropped);


                Rectangle drawRect = new Rectangle(0, 0, cropRec.Width, cropRec.Height);
                graphics.DrawImage(this.image, drawRect, cropRec, GraphicsUnit.Pixel);
                graphics.Dispose();

                Bitmap clone = new Bitmap(this.image);
                this.Bitmap = cropped;
                Convert(this.format);
                OnChanged(new ImageChangedEventArgs("Crop", clone));
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="w"></param>
        /// <param name="h"></param>
        /// <returns></returns>
        public Image GetThumbnail(int w, int h, bool activeOnly)
        {

            Bitmap preview = new Bitmap(w, h);
            Graphics gfx = Graphics.FromImage(preview);
            gfx.Clear(Color.Transparent);
            gfx.PixelOffsetMode = PixelOffsetMode.HighQuality;
            gfx.InterpolationMode = InterpolationMode.HighQualityBicubic;

            Rectangle rec = (activeOnly) ? this.ActiveRegion : new Rectangle(0, 0, this.Bitmap.Width, this.Bitmap.Height);

            gfx.DrawImage(this.image, new Rectangle(0, 0, w, h), rec, GraphicsUnit.Pixel);
            return preview;
        }

        /// <summary>
        /// Saves internal image
        /// </summary>
        public void Save()
        {
            this.Save(this.Filename, this.format);
        }

        /// <summary>
        /// Saves internal image
        /// </summary>
        public void Save(string filename, ImageFormat format)
        {
            var stream = new FileStream(filename, FileMode.OpenOrCreate, FileAccess.Write, FileShare.ReadWrite);
            this.Filename = filename;
            this.image.Save(stream, format);
            stream.Close();
        }

        #endregion

        #region Filter's Apply

        /// <summary>
        /// Lock a layer for unsafe access
        /// </summary>
        /// <param name="bitmap"></param>
        /// <param name="mode"></param>
        /// <param name="width"></param>
        /// <param name="bd"></param>
        private BitmapData Lock(ref Bitmap bitmap, ImageLockMode mode, out int width)
        {
            GraphicsUnit unit = GraphicsUnit.Pixel;
            RectangleF boundsF = bitmap.GetBounds(ref unit);
            Rectangle bounds = new Rectangle((int)boundsF.X,
                (int)boundsF.Y,
                (int)boundsF.Width,
                (int)boundsF.Height);
            unsafe
            {
                width = (int)boundsF.Width * sizeof(PixelData);
            }
            if (width % 4 != 0)
            {
                width = 4 * (bitmap.Width / 4 + 1);
            }
            return bitmap.LockBits(bounds, ImageLockMode.ReadWrite, PixelFormat.Format32bppArgb);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="bitmap"></param>
        /// <param name="bd"></param>
        private void Unlock(ref Bitmap bitmap, ref BitmapData bd)
        {
            bitmap.UnlockBits(bd);
            bd = null;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="filter"></param>
        public void Apply(string name, ImageDirectFilter filter)
        {
            int width;
            BitmapData bd;
            Bitmap original;

            original = new Bitmap(this.image);
            bd = Lock(ref this.image, ImageLockMode.ReadWrite, out width);
            unsafe
            {
                PixelData* pPixel;
                for (int j = this.activeRegion.Top; j < this.activeRegion.Bottom; j++)
                {
                    pPixel = (PixelData*)(((Byte*)bd.Scan0.ToPointer()) + j * width + this.activeRegion.Left * sizeof(PixelData));
                    for (int i = this.activeRegion.Left; i < this.activeRegion.Right; i++, pPixel++)
                    {
                        filter(pPixel);
                    }
                }
            }
            this.image.UnlockBits(bd);
            OnChanged(new ImageChangedEventArgs(name, original));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="layer"></param>
        /// <param name="indirectFilter"></param>
        public void Apply(string name, int border, ImageIndirectFilter filter)
        {
            int width;
            BitmapData bdCurrent, bdOriginal;
            Bitmap original;
            IndirectFilterGetDelegate getDelegateParent;
            PixelGet getDelegate;
            original = new Bitmap(this.image);
            bdOriginal = Lock(ref original, ImageLockMode.ReadWrite, out width);
            bdCurrent = Lock(ref this.image, ImageLockMode.ReadWrite, out width);
            getDelegateParent = new IndirectFilterGetDelegate(width, original, bdOriginal);
            int min_i = Math.Max(border, this.activeRegion.Left);
            int max_i = Math.Min(this.activeRegion.Right, this.activeRegion.Right - border);
            int min_j = Math.Max(border, this.activeRegion.Top);
            int max_j = Math.Min(this.activeRegion.Bottom, this.activeRegion.Bottom - border);
            unsafe
            {
                PixelData* pPixel;
                getDelegate = new PixelGet(getDelegateParent.GetPixel);

                for (int j = min_j; j < max_j; j++)
                {
                    pPixel = (PixelData*)(((byte*)bdCurrent.Scan0.ToPointer()) + j * width + this.activeRegion.Left * sizeof(PixelData));
                    for (int i = min_i; i < max_i; i++, pPixel++)
                    {
                        filter(i, j, getDelegate, pPixel, this.activeRegion.Left, this.activeRegion.Top, this.activeRegion.Right - 1, this.activeRegion.Bottom - 1);
                    }
                }
            }
            this.image.UnlockBits(bdCurrent);
            original.UnlockBits(bdOriginal);
            OnChanged(new ImageChangedEventArgs(name, original));
        }

        #endregion
    }
}