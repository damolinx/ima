using System;
using System.IO;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Collections;

namespace Ima
{
	#region Structs
	/// <summary>
	/// Defines the RGB memory structure for a bitmap
	/// </summary>
	public struct PixelData
	{
		public byte blue;
		public byte green;
		public byte red;
		public byte alpha;
	};
	#endregion

	#region Delegates
	
	/// <summary>
	/// Function prototype to obtain  a pixel
	/// </summary>
	unsafe public delegate PixelData * PixelGet(int x, int y);

	/// <summary>
	/// Notification function for an image change
	/// </summary>
	public delegate void ImageChangedEventHandler(object sender, ImageChangedEventArgs e);

	/// <summary>
	/// Direct Filter
	/// </summary>
	unsafe public delegate void ImageDirectFilter(PixelData * pPixelData);

	/// <summary>
	/// Indirect Filter
	/// </summary>
	unsafe public delegate void ImageIndirectFilter(int x, int y, PixelGet getPixel , PixelData * pPixelTarget, int x0, int y0, int x1, int y1);

	#endregion

	#region Support Classes

	/// <summary>
	/// Event Class
	/// </summary>
	public class ImageChangedEventArgs : EventArgs
	{
		/// <summary>
		/// 
		/// </summary>
		private Bitmap bitmap;

		/// <summary>
		/// 
		/// </summary>
		private string name;
		

		/// <summary>
		/// 
		/// </summary>
		/// <param name="old"></param>
		public ImageChangedEventArgs(Bitmap old)
		{
			this.bitmap = old;
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="name"></param>
		/// <param name="old"></param>
		public ImageChangedEventArgs(String name, Bitmap old)
		{
			this.name   = name;
			this.bitmap = old;
		}
		/// <summary>
		/// Previous Bitmap
		/// </summary>
		public Bitmap Bitmap
		{
			get
			{
				return this.bitmap;
			}
		}

		/// <summary>
		/// Action name
		/// </summary>
		public string Name
		{
			get
			{
				return this.name;
			}
		}
	}

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
		/// Associated filename
		/// </summary>
		private string filename;
		/// <summary>
		/// Active Bitmap Region to work on
		/// </summary>
		private Rectangle activeRegion = Rectangle.Empty;

		#endregion

		#region Constructors
		/// <summary>
		/// 
		/// </summary>
		/// <param name="filename"></param>
		public ImageWrapper(string filename)
		{
			this.filename     = filename;
			FileStream stream = new FileStream(filename, FileMode.Open, FileAccess.ReadWrite, FileShare.ReadWrite);
			this.Bitmap       = (Bitmap)Image.FromStream(stream);
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="filename"></param>
		/// <param name="bitmap"></param>
		public ImageWrapper(string filename, Bitmap bitmap)
		{
			this.filename     = filename;
			this.Bitmap       = bitmap;
		}

		#endregion

		#region Event Handling
		
		/// <summary>
		/// Event fired when any of the layers changed
		/// </summary>
		public event ImageChangedEventHandler Changed;
		
		protected virtual void OnChanged(ImageChangedEventArgs e) 
		{
			if (Changed != null)
				Changed(this, e);
		}
		#endregion

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

		/// <summary>
		/// 
		/// </summary>
		public Point PixelSize
		{
			get
			{
				GraphicsUnit unit = GraphicsUnit.Pixel;
				RectangleF bounds = this.image.GetBounds(ref unit);
				return new Point((int) bounds.Width, (int) bounds.Height);
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

		/// <summary>
		/// 
		/// </summary>
		public string Filename
		{
			get
			{
				return this.filename;
			}

			set
			{
				this.filename = value;
			}
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
					clone      = new Bitmap(this.image);

				this.image        = value;
				this.format       = this.image.RawFormat;
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
			catch(Exception e)
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
			int width  = (int)(x_ratio * this.Width);
			int height = (int)(y_ratio * this.Height);
			if (width != 0 && height != 0)
			{
				///http://www.thecodeproject.com/cs/media/bitmapmanip.asp				
				Bitmap resized   = new Bitmap(width, height, PixelFormat.Format32bppArgb);
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
				OnChanged(new ImageChangedEventArgs((x_ratio == y_ratio) ? "Resize": "Stretch" , clone));
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
				this.Bitmap  = cropped;
				Convert(this.format);
				OnChanged(new ImageChangedEventArgs( "Crop" , clone));
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
			gfx.PixelOffsetMode   = PixelOffsetMode.HighQuality;
			gfx.InterpolationMode = InterpolationMode.HighQualityBicubic;

			Rectangle rec = (activeOnly) ? this.ActiveRegion : new Rectangle(0, 0, this.Bitmap.Width, this.Bitmap.Height);

			gfx.DrawImage(this.image, new Rectangle(0, 0, w, h), rec, GraphicsUnit.Pixel);
			return preview;
		}

		/// <summary>
		/// Saves internal image as a Bitmap
		/// </summary>
		public void Save()
		{
			this.Save(this.filename, this.format);
		}		

		/// <summary>
		/// Saves internal image
		/// </summary>
		/// <param name="filename">string.Empty to use internal filename</param>
		/// <param name="format">Image Format</param>
		public void Save(string filename, ImageFormat format)
		{
			FileStream stream = new FileStream(filename, FileMode.OpenOrCreate, FileAccess.Write, FileShare.ReadWrite);
			this.filename = filename;
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
			Rectangle bounds = new Rectangle((int) boundsF.X,
				(int) boundsF.Y,
				(int) boundsF.Width,
				(int) boundsF.Height);
			unsafe
			{
				width = (int) boundsF.Width * sizeof(PixelData);
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
			bd       = Lock(ref this.image, ImageLockMode.ReadWrite, out width);
			unsafe
			{
				PixelData * pPixel;
				for(int j = this.activeRegion.Top; j < this.activeRegion.Bottom; j++)
				{
					pPixel = (PixelData*) (((Byte*)bd.Scan0.ToPointer()) + j * width + this.activeRegion.Left * sizeof(PixelData));
					for(int i = this.activeRegion.Left; i < this.activeRegion.Right; i++, pPixel++)
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
			original   = new Bitmap(this.image);
			bdOriginal = Lock(ref original, ImageLockMode.ReadWrite, out width);
			bdCurrent  = Lock(ref this.image, ImageLockMode.ReadWrite, out width);
			getDelegateParent = new IndirectFilterGetDelegate(width, original, bdOriginal);
			int min_i = Math.Max(border, this.activeRegion.Left);
			int max_i = Math.Min(this.activeRegion.Right, this.activeRegion.Right - border);
			int min_j = Math.Max(border, this.activeRegion.Top);
			int max_j = Math.Min(this.activeRegion.Bottom, this.activeRegion.Bottom - border);
			unsafe
			{
				PixelData * pPixel;
				getDelegate = new PixelGet(getDelegateParent.getPixel);

				for(int j = min_j; j < max_j; j++)
				{
					pPixel = (PixelData*) (((byte*)bdCurrent.Scan0.ToPointer()) + j * width  + this.activeRegion.Left * sizeof(PixelData));
					for(int i = min_i; i < max_i; i++, pPixel++)
					{
						filter(i, j, getDelegate, pPixel, this.activeRegion.Left, this.activeRegion.Top, this.activeRegion.Right-1, this.activeRegion.Bottom-1);
					}
				}
			}
			this.image.UnlockBits(bdCurrent);
			original.UnlockBits(bdOriginal);
			OnChanged(new ImageChangedEventArgs(name, original));
		}

		#endregion
	}

	/// <summary>
	/// IndirectFilterGetDelegate
	/// </summary>
	class IndirectFilterGetDelegate
	{
		Bitmap bitmap;
		BitmapData bitmapData;
		int width;
		
		/// <summary>
		/// 
		/// </summary>
		/// <param name="bitmap"></param>
		/// <param name="bitmapData"></param>
		public IndirectFilterGetDelegate(int width, Bitmap bitmap, BitmapData bitmapData)
		{
			this.bitmap = bitmap;
			this.bitmapData = bitmapData;
			this.width = width;
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="bd"></param>
		/// <param name="x"></param>
		/// <param name="y"></param>
		/// <returns></returns>
		public unsafe PixelData * getPixel(int x, int y)
		{
			return ((PixelData*) ((Byte*)bitmapData.Scan0.ToPointer() + y * width + x * sizeof(PixelData)));
		}
	}

	/// <summary>
	/// 
	/// </summary>
	public abstract class FilterBase
	{
		protected string name = string.Empty;
		protected string desc = string.Empty;
		protected bool direct = true;
		protected int  border = 0;

		/// <summary>
		/// 
		/// </summary>
		/// <param name="name"></param>
		public FilterBase (string name)
		{
			this.name = name;
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="pPixel"></param>
		public unsafe virtual void filter(PixelData * pPixel){}
		
		/// <summary>
		/// 
		/// </summary>
		/// <param name="x"></param>
		/// <param name="y"></param>
		/// <param name="getPixel"></param>
		/// <param name="pPixel"></param>
		/// <param name="x0"></param>
		/// <param name="y0"></param>
		/// <param name="x1"></param>
		/// <param name="y1"></param>
		public unsafe virtual void filter(int x, int y, PixelGet getPixel , PixelData * pPixel, int x0, int y0, int x1, int y1){}

		#region Property
		/// <summary>
		/// 
		/// </summary>
		public string Name
		{
			get
			{
				return this.name;
			}
		}

		/// <summary>
		/// 
		/// </summary>
		public string Description
		{
			get
			{
				return this.desc;
			}
		}

		/// <summary>
		/// 
		/// </summary>
		public bool Direct
		{
			get
			{
				return this.direct;
			}
		}

		/// <summary>
		/// 
		/// </summary>
		public int Border
		{
			get
			{
				return this.border;
			}
		}
		#endregion
	}

	public abstract class ThresholdFilterBase : FilterBase
	{
		protected int threshold = 0;
		protected int min_threshold = 0;
		protected int max_threshold = 0;
		protected string property = string.Empty;

		public ThresholdFilterBase(string name) : base(name)
		{
		}

		#region Property
		
		/// <summary>
		/// 
		/// </summary>
		public virtual int Threshold
		{
			get
			{
				return this.threshold;
			}

			set
			{
				this.threshold = (value >= this.min_threshold) ? ((value <= this.max_threshold) ?  value : this.max_threshold) : this.min_threshold;
			}
		}

		/// <summary>
		/// 
		/// </summary>
		public int Minimum
		{
			get
			{
				return this.min_threshold;
			}
		}

		/// <summary>
		/// 
		/// </summary>
		public int Maximum
		{
			get
			{
				return this.max_threshold;
			}
		}
		
		/// <summary>
		/// 
		/// </summary>
		public string Property
		{
			get
			{
				return this.property;
			}
		}
		#endregion
	}


	#region Pixel Based Filters
	public class FilterInvert : FilterBase
	{
		public FilterInvert() : base("Invert") { this.direct = true; }
		public unsafe override void filter(PixelData * pPixel)
		{
			pPixel->red   = (byte)(255 - pPixel->red);
			pPixel->green = (byte)(255 - pPixel->green);
			pPixel->blue  = (byte)(255 - pPixel->blue);
		}			
	}

	public class FilterRed : FilterBase
	{
		public FilterRed() : base("Red") { this.direct = true; }

		public unsafe override void filter(PixelData * pPixel)
		{
			pPixel->red   = (byte)(0.299 * pPixel->red + 0.587 * pPixel->green + 0.114 * pPixel->blue);
			pPixel->green = 0;
			pPixel->blue  = 0;
		}			
	}

	public class FilterGreen : FilterBase
	{
		public FilterGreen() : base("Green") { this.direct = true; }

		public unsafe override void filter(PixelData * pPixel)
		{
			pPixel->green = (byte)(0.299 * pPixel->red + 0.587 * pPixel->green + 0.114 * pPixel->blue);
			pPixel->red   = 0;
			pPixel->blue  = 0;
		}			
	}

	public class FilterBlue : FilterBase
	{
		public FilterBlue() : base("Blue") { this.direct = true; }

		public unsafe override void filter(PixelData * pPixel)
		{
			pPixel->blue  = (byte)(0.299 * pPixel->red + 0.587 * pPixel->green + 0.114 * pPixel->blue);
			pPixel->red   = 0;
			pPixel->green = 0;
		}			
	}

	public class FilterSepia : FilterBase
	{
		public FilterSepia() : base("Sepia") { this.direct = true; }

		public unsafe override void filter(PixelData * pPixel)
		{
			double gray   = (0.299 * pPixel->red + 0.587 * pPixel->green + 0.114 * pPixel->blue);
			pPixel->red   = (byte)Math.Min(255, gray + 40);
			pPixel->green = (byte)Math.Min(255, gray + 26);
			pPixel->blue  = (byte)gray;
		}			
	}

	public class FilterGrayAdjusted : FilterBase
	{
		public FilterGrayAdjusted() : base("GrayScale") { this.direct = true; }
		public unsafe override void filter(PixelData * pPixel)
		{
			byte gray = (byte)(0.299 * pPixel->red + 0.587 * pPixel->green + 0.114 * pPixel->blue);
			pPixel->red   = gray;
			pPixel->green = gray;
			pPixel->blue  = gray;
		}			
	}
	public class FilterRedChannel : FilterBase
	{
		public FilterRedChannel() : base("Red Channel") { this.direct = true; }

		public unsafe override void filter(PixelData * pPixel)
		{
			pPixel->green = 0;
			pPixel->blue  = 0;
		}			
	}

	public class FilterGreenChannel : FilterBase
	{
		public FilterGreenChannel() : base("Green Channel") { this.direct = true; }

		public unsafe override void filter(PixelData * pPixel)
		{
			pPixel->red   = 0;
			pPixel->blue  = 0;
		}			
	}

	public class FilterBlueChannel : FilterBase
	{
		public FilterBlueChannel() : base("Blue Channel") { this.direct = true; }

		public unsafe override void filter(PixelData * pPixel)
		{
			pPixel->red   = 0;
			pPixel->green = 0;
		}			
	}

	public class FilterMean : FilterBase
	{
		/// <summary>
		/// Working array
		/// </summary>
		byte[] colors = new byte[5];

		/// <summary>
		/// 
		/// </summary>
		public FilterMean() : base("Mean") 
		{ 
			this.direct = false; 
			this.border = 1;
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="x"></param>
		/// <param name="y"></param>
		/// <param name="getPixel"></param>
		/// <param name="pPixel"></param>
		/// <param name="x0"></param>
		/// <param name="y0"></param>
		/// <param name="x1"></param>
		/// <param name="y1"></param>
		public unsafe override void filter(int x, int y, PixelGet getPixel , PixelData * pPixel, int x0, int y0, int x1, int y1)
		{
			PixelData * pPixelClone1, pPixelClone2, pPixelClone3, pPixelClone4;
			pPixelClone1 = getPixel(x,   y+1);
			pPixelClone2 = getPixel(x-1, y);
			pPixelClone3 = getPixel(x+1, y);
			pPixelClone4 = getPixel(x,   y-1);

			colors[0] = pPixelClone1->red;
			colors[1] = pPixelClone2->red;
			colors[2] = pPixelClone3->red;
			colors[3] = pPixelClone4->red;
			colors[4] = pPixel->red;
			Array.Sort(colors);
			pPixel->red = colors[2];

			colors[0] = pPixelClone1->green;
			colors[1] = pPixelClone2->green;
			colors[2] = pPixelClone3->green;
			colors[3] = pPixelClone4->green;
			colors[4] = pPixel->green;
			Array.Sort(colors);
			pPixel->green = colors[2];

			colors[0] = pPixelClone1->blue;
			colors[1] = pPixelClone2->blue;
			colors[2] = pPixelClone3->blue;
			colors[3] = pPixelClone4->blue;
			colors[4] = pPixel->blue;
			Array.Sort(colors);
			pPixel->blue = colors[2];
			
		}
	}

	public class FilterBlackWhite : ThresholdFilterBase
	{
		public FilterBlackWhite() : base("Black/White") 
		{
			this.max_threshold = 255;
			this.min_threshold = 0;
			this.threshold     = 127;
			this.property      = "B/W Threshold";
			this.direct        = true; 
		}

		public unsafe override void filter(PixelData * pPixel)
		{
			int distance = (255 - pPixel->red) * (255 - pPixel->red) + (255 - pPixel->green) * (255 - pPixel->green) + (255 - pPixel->blue) * (255 - pPixel->blue);
			pPixel->red  = pPixel->green = pPixel->blue = (distance <= (this.threshold * this.threshold)) ? (byte)255 : (byte)0;
		}			
	}

	public class FilterBrightness : ThresholdFilterBase
	{
		public FilterBrightness() : base("Brightness") 
		{
			this.max_threshold = 255;
			this.min_threshold = -255;
			this.property = "Brightness";
			this.direct = true; 
		}

		public unsafe override void filter(PixelData * pPixel)
		{
			int tmp;
			tmp           = pPixel->red + this.threshold;
			pPixel->red   = (byte)((tmp >= 0) ? ((tmp <= 255) ? tmp : 255) : 0); 

			tmp           = pPixel->green + this.threshold;
			pPixel->green = (byte)((tmp >= 0) ? ((tmp <= 255) ? tmp : 255) : 0); 

			tmp           = pPixel->blue + this.threshold;
			pPixel->blue  = (byte)((tmp >= 0) ? ((tmp <= 255) ? tmp : 255) : 0);
		}			
	}

	public class FilterContrast : ThresholdFilterBase
	{
		double fpContrast;
		public FilterContrast() : base("Contrast") 
		{
			fpContrast         = 0.0;
			this.max_threshold = 100;
			this.min_threshold = -100;
			this.property      = "Contrast";
			this.direct = true; 
		}

		/// <summary>
		/// 
		/// </summary>
		public override int Threshold
		{
			get
			{
				return this.threshold;
			}

			set
			{
				this.threshold = (value >= this.min_threshold) ? ((value <= this.max_threshold) ?  value : this.max_threshold) : this.min_threshold;
				this.fpContrast = (this.threshold + 100.0) / 100.0;
				this.fpContrast *= this.fpContrast;
			}
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="pPixel"></param>
		public unsafe override void filter(PixelData * pPixel)
		{
			double pixel;

			/* red */
			pixel = pPixel->red * 1/255.0;
			pixel -= 0.5;
			pixel *= this.fpContrast;
			pixel += 0.5;
			pixel *= 255.0;
			pPixel->red = (byte)((pixel >= 0) ? ((pixel <= 255) ? pixel : 255) : 0);

			/* green */
			pixel = pPixel->green * 1/255.0;
			pixel -= 0.5;
			pixel *= this.fpContrast;
			pixel += 0.5;
			pixel *= 255.0;
			pPixel->green = (byte)((pixel >= 0) ? ((pixel <= 255) ? pixel : 255) : 0);

			/* blue */
			pixel = pPixel->blue * 1/255.0;
			pixel -= 0.5;
			pixel *= this.fpContrast;
			pixel += 0.5;
			pixel *= 255.0;
			pPixel->blue = (byte)((pixel >= 0) ? ((pixel <= 255) ? pixel : 255) : 0);
		}			
	}

	public class FilterGamma : ThresholdFilterBase
	{
		byte[] redGamma   = new byte[256];
		byte[] greenGamma = new byte[256];
		byte[] blueGamma  = new byte[256];
		
		public FilterGamma() : base("Gamma") 
		{
			this.max_threshold = 100;
			this.min_threshold = 1;
			this.threshold     = 20;
			this.property      = "Gamma correction";
			this.direct        = true; 
		}

		/// <summary>
		/// 
		/// </summary>
		public override int Threshold
		{
			get
			{
				return this.threshold;
			}

			set
			{
				this.threshold = (value >= this.min_threshold) ? ((value <= this.max_threshold) ?  value : this.max_threshold) : this.min_threshold;
				double fpGamma = this.threshold / 20.0;

				if (this.threshold == 0)
				{
					for (int i = 0; i < 256; ++i)
					{
						redGamma[i] = greenGamma[i] = blueGamma[i] = (byte)i;
					}
				}
				else
				{
					///http://www.teamten.com/lawrence/graphics/gamma/
					for (int i = 0; i < 256; ++i)
					{
						redGamma[i] = greenGamma[i] = blueGamma[i] = 
							(byte)Math.Min( 255.0, 
										   (255.0 * Math.Pow(i/255.0, 1.0/fpGamma)) + 0.5);
					}
				}
			}
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="pPixel"></param>
		public unsafe override void filter(PixelData * pPixel)
		{
			pPixel->red   = redGamma[pPixel->red];
			pPixel->green = redGamma[pPixel->green];
			pPixel->blue  = redGamma[pPixel->blue];
		}			
	}

	public class FilterNoise : ThresholdFilterBase
	{
		Random rand;
		byte red = 255, green = 255, blue = 255;

		public FilterNoise() : base("Random Noise")
		{
			this.direct = true; 
			rand = new Random();
			this.min_threshold = 0;
			this.max_threshold = 100;
			this.property = "Noise Density";
		}

		public FilterNoise(int seed) : base("Random Noise") 
		{
			this.direct = true; 
			rand = new Random(seed);
			this.min_threshold = 0;
			this.max_threshold = 100;
			this.property = "Noise Density";
		}

		public void setNoiseColor(byte R, byte G, byte B)
		{
			this.red   = R;
			this.green = G;
			this.blue  = B;
		}

		public unsafe override void filter(PixelData * pPixel)
		{
			if (rand.Next(this.min_threshold, this.max_threshold) <= this.threshold)
			{
				pPixel->red   = red;
				pPixel->green = green;
				pPixel->blue  = blue;
			}
		}			
	}

	public class FilterPosterize : ThresholdFilterBase
	{
		byte[] lookup = new byte[256];
		public FilterPosterize() : base("Posterize") 
		{
			this.max_threshold = 8;
			this.min_threshold = 0;
			this.property      = "Number of colors (2^n)";
			this.direct        = true; 
			this.Threshold     = 4;
		}

		/// <summary>
		/// 
		/// </summary>
		public override int Threshold
		{
			get
			{
				return this.threshold;
			}

			set
			{
				this.threshold = (value >= this.min_threshold) ? ((value <= this.max_threshold) ?  value : this.max_threshold) : this.min_threshold;
				int pow = (int)Math.Pow(2, this.threshold);
				for(int i = 0; i < this.lookup.Length; i++)
				{
					this.lookup[i] = (byte)(i - (i % pow));
				}
			}
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="pPixel"></param>
		public unsafe override void filter(PixelData * pPixel)
		{
			pPixel->red   = this.lookup[pPixel->red];
			pPixel->green = this.lookup[pPixel->green];
			pPixel->blue  = this.lookup[pPixel->blue];
		}
	}

	public class FilterSolarize : ThresholdFilterBase
	{
		int pow;
		public FilterSolarize() : base("Solarize") 
		{
			this.max_threshold = 8;
			this.min_threshold = 0;
			this.property      = "Number of colors (2^n)";
			this.direct        = true;
			this.Threshold     = 8;
		}

		/// <summary>
		/// 
		/// </summary>
		public override int Threshold
		{
			get
			{
				return this.threshold;
			}

			set
			{
				this.threshold = (value >= this.min_threshold) ? ((value <= this.max_threshold) ?  value : this.max_threshold) : this.min_threshold;
				this.pow = (int)Math.Pow(2, this.threshold);
			}
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="pPixel"></param>
		public unsafe override void filter(PixelData * pPixel)
		{
			pPixel->red   = (byte)((pPixel->red > this.pow) ? (255 - pPixel->red) : pPixel->red);
			pPixel->green = (byte)((pPixel->green> this.pow) ? (255 - pPixel->green) : pPixel->green);
			pPixel->blue  = (byte)((pPixel->blue > this.pow) ? (255 - pPixel->blue) : pPixel->blue);
		}
	}


	#endregion

	#region Kernel Based Filters

	public abstract class KernelFilterBase : ThresholdFilterBase
	{
		int weight;
		int offset;
		int[] matrix;

		public KernelFilterBase(int size, string name) : base(name)
		{
			this.direct        = false; 
			this.property      = "Matrix (2n + 1)";
			this.threshold     = size;
			this.min_threshold = 0;
			this.max_threshold = 5;
			this.border        = (this.threshold - 1) / 2;
			this.offset        = 0;
			this.weight        = 1;
		}

		#region Properties
		public int[] Matrix
		{
			get
			{
				return this.matrix;
			}
			set
			{
				this.matrix = value;
				this.Threshold = (matrix == null) ? 0 : matrix.Length;
			}
		}
		public int Weight
		{
			get
			{
				return this.weight;
			}
			set
			{
				this.weight = value;
			}
		}
		public int Offset
		{
			get
			{
				return this.offset;
			}
			set
			{
				this.offset = value;
			}
		}
		#endregion
		/// <summary>
		/// 
		/// </summary>
		/// <param name="x"></param>
		/// <param name="y"></param>
		/// <param name="getPixel"></param>
		/// <param name="pPixel"></param>
		/// <param name="matrix">0-8 matrix, 9 weight, 10 offset</param>
		protected unsafe void applyConvolution3x3(int x, int y, PixelGet getPixel , PixelData * pPixel)
		{
			PixelData * pPixelClone1, pPixelClone2, pPixelClone3, pPixelClone4, pPixelClone5, 
				pPixelClone6, pPixelClone7, pPixelClone8, pPixelClone9;

			pPixelClone1 = getPixel(x-1, y+1);
			pPixelClone2 = getPixel(x,   y+1);
			pPixelClone3 = getPixel(x+1, y+1);
			pPixelClone4 = getPixel(x-1, y);
			pPixelClone5 = getPixel(x,   y);
			pPixelClone6 = getPixel(x+1, y);
			pPixelClone7 = getPixel(x-1, y-1);
			pPixelClone8 = getPixel(x,   y-1);
			pPixelClone9 = getPixel(x+1, y-1);

			int tmp = offset + (matrix[0] * pPixelClone1->red + matrix[1] * pPixelClone2->red + matrix[2] * pPixelClone3->red
				+ matrix[3] * pPixelClone4->red + matrix[4] * pPixelClone5->red + matrix[5] * pPixelClone6->red
				+ matrix[6] * pPixelClone7->red + matrix[7] * pPixelClone8->red + matrix[8] * pPixelClone9->red) / weight;

			pPixel->red = (byte)((tmp >= 0) ? ((tmp <= 255) ? tmp : 255) : 0);

			tmp = offset + (matrix[0] * pPixelClone1->green + matrix[1] * pPixelClone2->green + matrix[2] * pPixelClone3->green
				+ matrix[3] * pPixelClone4->green + matrix[4] * pPixelClone5->green + matrix[5] * pPixelClone6->green
				+ matrix[6] * pPixelClone7->green + matrix[7] * pPixelClone8->green + matrix[8] * pPixelClone9->green) / weight;

			pPixel->green = (byte)((tmp >= 0) ? ((tmp <= 255) ? tmp : 255) : 0);

			tmp = offset + (matrix[0] * pPixelClone1->blue + matrix[1] * pPixelClone2->blue + matrix[2] * pPixelClone3->blue
				+ matrix[3] * pPixelClone4->blue + matrix[4] * pPixelClone5->blue + matrix[5] * pPixelClone6->blue
				+ matrix[6] * pPixelClone7->blue + matrix[7] * pPixelClone8->blue + matrix[8] * pPixelClone9->blue) / weight;

			pPixel->blue = (byte)((tmp >= 0) ? ((tmp <= 255) ? tmp : 255) : 0);
		}
	}

	public class FilterMatrixBlur : KernelFilterBase
	{
		public FilterMatrixBlur() : base(3, "Blur")
		{
			this.Weight = 16;
			this.Offset = 0;
			this.Matrix = new int[]{ 1, 2, 1, 2, 4, 2, 1, 2, 1};
		}	
		
		public unsafe override void filter(int x, int y, PixelGet getPixel , PixelData * pPixel, int x0, int y0, int x1, int y1)
		{
			this.applyConvolution3x3(x, y, getPixel, pPixel);
		}
	}

	public class FilterMatrixSharpen : KernelFilterBase
	{
		public FilterMatrixSharpen() : base(3, "Sharpen")
		{
			this.Weight = 1;
			this.Offset = 0;
			this.Matrix = new int[]{ 0, -1, 0, -1, 5, -1, 0, -1, 0};
		}
		
		public unsafe override void filter(int x, int y, PixelGet getPixel , PixelData * pPixel, int x0, int y0, int x1, int y1)
		{
			this.applyConvolution3x3(x, y, getPixel, pPixel);
		}
	}

	public class FilterMatrixBW : KernelFilterBase
	{
		public FilterMatrixBW() : base(3, "Black/White Edge Detect")
		{
			this.Weight = 1;
			this.Offset = 0;
			this.Matrix = new int[]{0};
			this.property      = "Color threshold";
			this.min_threshold = 0;
			this.max_threshold = 255;
			this.threshold     = 20;
		}
		
		public unsafe override void filter(int x, int y, PixelGet getPixel , PixelData * pPixel, int x0, int y0, int x1, int y1)
		{
			PixelData * localPixel  = pPixel;
			PixelData * localPixel1 = getPixel(x+1, y);
			PixelData * localPixel2 = getPixel(x, y+1);
	
			if (((localPixel->red - localPixel1->red) * (localPixel->red - localPixel1->red)) +
				((localPixel->green - localPixel1->green) * (localPixel->green - localPixel1->green)) +
				((localPixel->blue - localPixel1->blue) * (localPixel->blue - localPixel1->blue)) >= (this.threshold * this.threshold)
				||
				
				((localPixel->red - localPixel2->red) * (localPixel->red - localPixel2->red)) +
				((localPixel->green - localPixel2->green) * (localPixel->green - localPixel2->green)) +
				((localPixel->blue - localPixel2->blue) * (localPixel->blue - localPixel2->blue)) >= (this.threshold * this.threshold))
			{
				pPixel->red		= (byte)0;
				pPixel->green	= (byte)0;
				pPixel->blue	= (byte)0;
			}
			else
			{
				pPixel->red		= (byte)255;
				pPixel->green	= (byte)255;
				pPixel->blue	= (byte)255;
			}
		}
	}

	public class FilterMatrixSobelED : KernelFilterBase
	{
		protected int[] mat1 = new int[]{ 1, 2,  1, 0, 0,  0, -1, -2, -1};
	    protected int[] mat2 = new int[]{ 1, 0, -1, 2, 0, -2,  1,  0, -1};

		public FilterMatrixSobelED() : base(3, "Sobel Edge Detect")
		{
			this.Weight = 13;
			this.Offset = 0;
			this.Matrix = null;
		}
		
		public unsafe override void filter(int x, int y, PixelGet getPixel , PixelData * pPixel, int x0, int y0, int x1, int y1)
		{
			PixelData pixel1 = new PixelData();
			PixelData pixel2 = new PixelData();

			this.Matrix = mat1;
			this.applyConvolution3x3(x, y, getPixel, &pixel1);

			this.Matrix = mat2;
			this.applyConvolution3x3(x, y, getPixel, &pixel2);

			pPixel->red = (byte)Math.Sqrt(pixel1.red * pixel1.red + pixel2.red * pixel2.red);
			pPixel->green = (byte)Math.Sqrt(pixel1.green * pixel1.green + pixel2.green * pixel2.green);
			pPixel->blue = (byte)Math.Sqrt(pixel1.blue * pixel1.blue + pixel2.blue * pixel2.blue);
		}
	}

	public class FilterMatrixPrewittED : KernelFilterBase
	{
		protected int[] mat1 = new int[]{ 1, 1,  1, 0, 0,  0, -1, -1, -1};
		protected int[] mat2 = new int[]{ 1, 0, -1, 1, 0, -1,  1,  0, -1};

		public FilterMatrixPrewittED() : base(3, "Prewitt Edge Detect")
		{
			this.Weight = 13;
			this.Offset = 0;
			this.Matrix = null;
		}
		
		public unsafe override void filter(int x, int y, PixelGet getPixel , PixelData * pPixel, int x0, int y0, int x1, int y1)
		{
			PixelData pixel1 = new PixelData();
			PixelData pixel2 = new PixelData();

			this.Matrix = mat1;
			this.applyConvolution3x3(x, y, getPixel, &pixel1);

			this.Matrix = mat2;
			this.applyConvolution3x3(x, y, getPixel, &pixel2);

			pPixel->red = (byte)Math.Sqrt(pixel1.red * pixel1.red + pixel2.red * pixel2.red);
			pPixel->green = (byte)Math.Sqrt(pixel1.green * pixel1.green + pixel2.green * pixel2.green);
			pPixel->blue = (byte)Math.Sqrt(pixel1.blue * pixel1.blue + pixel2.blue * pixel2.blue);
		}
	}

	public class FilterMatrixKirshED : KernelFilterBase
	{
		int[] mat1 = new int[]{ 5, -3, -3,  5, -3, -3,  5, -3, -3};
		int[] mat2 = new int[]{ 5,  5,  5, -3, -3, -3, -3, -3, -3};

		public FilterMatrixKirshED() : base(3, "Kirsh Edge Detect")
		{
			this.Weight = 13;
			this.Offset = 0;
			this.Matrix = null;
		}
		
		public unsafe override void filter(int x, int y, PixelGet getPixel , PixelData * pPixel, int x0, int y0, int x1, int y1)
		{
			PixelData pixel1 = new PixelData();
			PixelData pixel2 = new PixelData();

			this.Matrix = mat1;
			this.applyConvolution3x3(x, y, getPixel, &pixel1);

			this.Matrix = mat2;
			this.applyConvolution3x3(x, y, getPixel, &pixel2);

			pPixel->red = (byte)Math.Sqrt(pixel1.red * pixel1.red + pixel2.red * pixel2.red);
			pPixel->green = (byte)Math.Sqrt(pixel1.green * pixel1.green + pixel2.green * pixel2.green);
			pPixel->blue = (byte)Math.Sqrt(pixel1.blue * pixel1.blue + pixel2.blue * pixel2.blue);
		}
	}
	#endregion

	#region Displacement Filters
	public class FilterFrostedGlass : ThresholdFilterBase
	{
		Random rand;

		public FilterFrostedGlass() : base("Frosted Glass")
		{
			this.direct = false; 
			rand = new Random();
			this.min_threshold = 0;
			this.max_threshold = 10;
			this.threshold = 1;
			this.property = "Frost thickness";
		}

		public FilterFrostedGlass(int seed) : base("Frosted Glass") 
		{
			this.direct = false; 
			rand = new Random(seed);
			this.min_threshold = 0;
			this.max_threshold = 10;
			this.threshold = 1;
			this.property = "Frost thickness";
		}

		public unsafe override void filter(int x, int y, PixelGet getPixel, PixelData * pPixel, int x0, int y0, int x1, int y1)
		{
			int rand_x = Math.Min(Math.Max(x0, x + rand.Next(-this.threshold, this.threshold)), x1);
			int rand_y = Math.Min(Math.Max(y0, y + rand.Next(-this.threshold, this.threshold)), y1);

			PixelData * pRandPixel = getPixel(rand_x, rand_y);

			pPixel->red   = pRandPixel->red;
			pPixel->green = pRandPixel->green;
			pPixel->blue  = pRandPixel->blue;
		}
	}

	public class FilterRotate : ThresholdFilterBase
	{
		Color color;
		Point center;
		double angle;
		
		public FilterRotate(double angle, Point center) : base("Rotation")
		{
			this.direct        = false; 
			this.min_threshold = 0;
			this.max_threshold = 359;
			this.threshold     = 0;
			this.property      = "Angle";
			this.center        = center;
			this.angle         = 2.0 * Math.PI * angle / 360.0;
			this.color         = Color.White;
		}

		public FilterRotate() : base("Rotation")
		{
			this.direct        = false; 
			this.min_threshold = 0;
			this.max_threshold = 359;
			this.threshold     = 0;
			this.property      = "Angle";
			this.center        = new Point(0,0);
			this.angle         = 0.0;
			this.color         = Color.White;
		}

		public Color Fillcolor
		{
			get
			{
				return this.color;
			}
			set
			{
				this.color = value;
			}
		}

		public unsafe override void filter(int x, int y, PixelGet getPixel, PixelData * pPixel, int x0, int y0, int x1, int y1)
		{
			int rot_x = (int)((x - center.X) * Math.Cos(angle) - (y - center.Y) * Math.Sin(angle) + center.X);
			int rot_y = (int)((x - center.X) * Math.Sin(angle) + (y - center.Y) * Math.Cos(angle) + center.Y);

			if (rot_x >= x0 && rot_x <= x1 && rot_y >= y0 && rot_y <= y1)
			{
				PixelData * pRotPixel = getPixel(rot_x, rot_y);
				pPixel->red   = pRotPixel->red;
				pPixel->green = pRotPixel->green;
				pPixel->blue  = pRotPixel->blue;
			}
			else
			{
				pPixel->red   = color.R;
				pPixel->green = color.G;
				pPixel->blue  = color.B;
			}
		}
	}

	#endregion

	#region Drawing Techniques Filters
	public class FilterPencil : ThresholdFilterBase
	{

		double ratio;

		public FilterPencil() : base("Pencil") 
		{
			this.max_threshold = 100;
			this.min_threshold = 0;
			this.property      = "Pen Strenght";
			this.direct        = false;
			this.border        = 1;
			this.Threshold     = 50;
		}

		/// <summary>
		/// 
		/// </summary>
		public override int Threshold
		{
			get
			{
				return this.threshold;
			}

			set
			{
				this.threshold = (value >= this.min_threshold) ? ((value <= this.max_threshold) ?  value : this.max_threshold) : this.min_threshold;
				this.ratio     = this.threshold  / 100.0;
			}
		}

		public unsafe override void filter(int x, int y, PixelGet getPixel , PixelData * pPixel, int x0, int y0, int x1, int y1)
		{
			PixelData*[] pPixels;
			double mask, max_diff, max_temp, grayscaled;

			grayscaled = 0.299 * pPixel->red + 0.587 * pPixel->green + 0.114 * pPixel->blue;

			pPixels    = new PixelData*[8];
			pPixels[0] = getPixel(x-1, y+1);
			pPixels[1] = getPixel(x,   y+1);
			pPixels[2] = getPixel(x+1, y+1);
			pPixels[3] = getPixel(x-1, y);
			pPixels[4] = getPixel(x+1, y);
			pPixels[5] = getPixel(x-1, y-1);
			pPixels[6] = getPixel(x,   y-1);
			pPixels[7] = getPixel(x+1, y-1);

			///Red
			max_diff = 0;

			

			foreach(PixelData * pCurrent in pPixels)
			{
				max_temp = Math.Abs((0.299 * pCurrent->red + 0.587 * pCurrent->green + 0.114 * pCurrent->blue) - grayscaled);
				if (max_temp > max_diff)
				{
					max_diff = max_temp;
				}
			}
			mask     = 1.0 / (max_diff / Math.Sqrt(grayscaled + 1.0) / 3.0 + 1.0);
			pPixel->red = pPixel->green = pPixel->blue = (byte)(grayscaled + this.ratio * ((255 - grayscaled) * mask - max_diff * grayscaled / 100.0));
		}
	}
	
	public class FilterColoredPencil : ThresholdFilterBase
	{

		double ratio;

		public FilterColoredPencil() : base("Colored Pencil") 
		{
			this.max_threshold = 100;
			this.min_threshold = 0;
			this.property      = "Pen Strenght";
			this.direct        = false;
			this.border        = 1;
			this.Threshold     = 50;
		}

		/// <summary>
		/// 
		/// </summary>
		public override int Threshold
		{
			get
			{
				return this.threshold;
			}

			set
			{
				this.threshold = (value >= this.min_threshold) ? ((value <= this.max_threshold) ?  value : this.max_threshold) : this.min_threshold;
				this.ratio     = this.threshold  / 100.0;
			}
		}

		public unsafe override void filter(int x, int y, PixelGet getPixel , PixelData * pPixel, int x0, int y0, int x1, int y1)
		{
			PixelData*[] pPixels;
			double mask;
			int   max_diff, max_temp;

			pPixels    = new PixelData*[8];
			pPixels[0] = getPixel(x-1, y+1);
			pPixels[1] = getPixel(x,   y+1);
			pPixels[2] = getPixel(x+1, y+1);
			pPixels[3] = getPixel(x-1, y);
			pPixels[4] = getPixel(x+1, y);
			pPixels[5] = getPixel(x-1, y-1);
			pPixels[6] = getPixel(x,   y-1);
			pPixels[7] = getPixel(x+1, y-1);

			///Red
			max_diff = 0;
			foreach(PixelData * pCurrent in pPixels)
			{
				max_temp = Math.Abs(pCurrent->red - pPixel->red);
				if (max_temp > max_diff)
				{
					max_diff = max_temp;
				}
			}
			mask     = 1.0 / (max_diff / Math.Sqrt(pPixel->red + 1.0) / 3.0 + 1.0);
			pPixel->red += (byte)(this.ratio * ((255 - pPixel->red) * mask - max_diff * pPixel->red / 100.0));

			///Green
			max_diff = 0;
			foreach(PixelData * pCurrent in pPixels)
			{
				max_temp = Math.Abs(pCurrent->green - pPixel->green);
				if (max_temp > max_diff)
				{
					max_diff = max_temp;
				}
			}
			mask     = 1.0 / (max_diff / Math.Sqrt(pPixel->green + 1.0) / 3.0 + 1.0);
			pPixel->green += (byte)(this.ratio * ((255 - pPixel->green) * mask - max_diff * pPixel->green / 100.0));

			///Blue
			max_diff = 0;
			foreach(PixelData * pCurrent in pPixels)
			{
				max_temp = Math.Abs(pCurrent->blue - pPixel->blue);
				if (max_temp > max_diff)
				{
					max_diff = max_temp;
				}
			}
			mask     = 1.0 / (max_diff / Math.Sqrt(pPixel->blue + 1.0) / 3.0 + 1.0);
			pPixel->blue += (byte)(this.ratio * ((255 - pPixel->blue) * mask - max_diff * pPixel->blue / 100.0));
		}
	}

	public class FilterOil : ThresholdFilterBase
	{
		public FilterOil() : base("Oil") 
		{
			this.max_threshold = 5;
			this.min_threshold = 1;
			this.property      = "Brush Size";
			this.direct        = false;
			this.border        = 1;
			this.Threshold     = 3;
		}

		/// <summary>
		/// 
		/// </summary>
		public override int Threshold
		{
			get
			{
				return border;
			}

			set
			{
				this.threshold = (value >= this.min_threshold) ? ((value <= this.max_threshold) ?  value : this.max_threshold) : this.min_threshold;
				this.border    = value;
				this.threshold = 2 * this.threshold + 1;
			}
		}

		private unsafe int MostFrequent(PixelData*[] color)
		{
			int[] freq = new int[color.Length];

			foreach(PixelData * pPixel in color)
			{
				for(int i = 0; i < color.Length; i++)
				{
					if (pPixel->blue == color[i]->blue && pPixel->green == color[i]->green && pPixel->red == color[i]->red)
					{
						freq[i]++;
						break;
					}
				}
			}

			int maxIndex = 0;

			for(int i = 1; i < color.Length; i++)
			{
				if  (freq[i] > freq[maxIndex])
				{
					maxIndex = i;
				}
			}
			return maxIndex;
		}


		public unsafe override void filter(int x, int y, PixelGet getPixel , PixelData * pPixel, int x0, int y0, int x1, int y1)
		{
			PixelData*[] pixels = new PixelData*[this.threshold * this.threshold];

			int f1 = (int)(0.5 * (this.threshold - 1));

			for(int i = 0; i < this.threshold; i++)
			{
				for(int j = 0; j < this.threshold; j++)
				{
					int xi = x - f1 + i;
					int yi = y - f1 + j;
					pixels[i * this.threshold + j] = getPixel( (xi < 0) ? 0 : ((xi > x1) ? x1 : xi), 
						(yi < 0) ? 0 : ((yi > y1) ? y1 : yi));
				}
			}
			
			int index     = MostFrequent(pixels);
			pPixel->red   = pixels[index]->red;
			pPixel->green = pixels[index]->green;
			pPixel->blue  = pixels[index]->blue;
		}

	}
	#endregion

	#region Filter Based tools
	public class HistogramRetriever : FilterBase
	{
		int[] red = new int[256];
		int[] green = new int[256];
		int[] blue = new int[256];
		int[] gray = new int[256];

		public HistogramRetriever() : base("Histogram Retriever") 
		{
			this.direct = true;
			red.SetValue(0, 0, 255);
			green.SetValue(0, 0, 255);
			blue.SetValue(0, 0, 255);
			gray.SetValue(0, 0, 255);
		}
		public unsafe override void filter(PixelData * pPixel)
		{
			byte gray = (byte)(0.299 * pPixel->red + 0.587 * pPixel->green + 0.114 * pPixel->blue);
			this.red[pPixel->red]++;
			this.green[pPixel->green]++;
			this.blue[pPixel->blue]++;
			this.gray[gray]++;
		}			
	}

	#endregion


	public class ImageUtilities
	{
		private ImageUtilities()
		{
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="path"></param>
		/// <param name="type"></param>
		/// <returns></returns>
		public static bool RotateFlipImage(string path, RotateFlipType type)
		{
			Image image       = null;
			FileStream stream = null;
			try
			{
				stream  = new FileStream(path, FileMode.Open, FileAccess.ReadWrite, FileShare.Read);
				image   = Image.FromStream(stream, false);
				stream.Close();
				image.RotateFlip(type);
				image.Save(path);
				return true;
			}
			catch(Exception e)
			{
				Console.WriteLine(e.Message + "\n" + e.StackTrace);
			}
			finally
			{
				if (image != null)
					image.Dispose();
				if (stream != null)
					stream.Close();
			}
			return false;
		}
	}
}