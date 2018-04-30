using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Data;
using System.Windows.Forms;

namespace Ima.Controls
{
	/// <summary>
	/// Delegate to reduce painting overhead
	/// </summary>
	public delegate void ImageControlPaintDelegate(System.Windows.Forms.PaintEventArgs e);

	/// <summary>
	/// Delegate to report selection changes
	/// </summary>
	public delegate void SelectionChangedEventHandler(object sender, Rectangle rec);

	/// <summary>
	/// Summary description for ImageControl.
	/// </summary>
	public class ImageControl : System.Windows.Forms.UserControl
	{
		/// <summary>
		/// Paint Mode
		/// </summary>
		public enum PaintStyle
		{
			PAINT_UNKNOWN,
			PAINT_PIXELATED,
			PAINT_HIGHQUALITY
		}

		/// <summary> 
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;
		/// <summary>
		/// Current Bitmap
		/// </summary>
		private Image image = null;
		/// <summary>
		/// Zoom
		/// </summary>
		private double zoom;
		/// <summary>
		/// Active selection. Rectangle Empty if none
		/// </summary>
		private ImageControl.Selection selection;
		/// <summary>
		/// Method use to paint control
		/// </summary>
		private ImageControlPaintDelegate paintDelegate;

		private PaintStyle paintStyle;

		#region Constructor
		/// <summary>
		/// Constructor
		/// </summary>
		public ImageControl()
		{
			// This call is required by the Windows.Forms Form Designer.
			InitializeComponent();

			// TODO: Add any initialization after the InitializeComponent call
			this.Zoom            = 1.0;
			this.PaintMode       = PaintStyle.PAINT_HIGHQUALITY;
			this.SelectionActive = false;
			//
			// Flickering reduction / Paint behavior control
			//
			this.SetStyle(ControlStyles.DoubleBuffer, true);
			this.SetStyle(ControlStyles.AllPaintingInWmPaint, true);
			this.SetStyle(ControlStyles.UserPaint, true);
		}

		/// <summary> 
		/// Clean up any resources being used.
		/// </summary>
		protected override void Dispose( bool disposing )
		{
			if( disposing )
			{
				if(components != null)
				{
					components.Dispose();
				}
			}
			base.Dispose( disposing );
		}
		#endregion

		#region Component Designer generated code
		/// <summary> 
		/// Required method for Designer support - do not modify 
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			// 
			// ImageControl
			// 
			this.BackColor = System.Drawing.SystemColors.AppWorkspace;
			this.Name = "ImageControl";
			this.Paint += new System.Windows.Forms.PaintEventHandler(this.ImageControl_Paint);
		}
		#endregion

		#region Properties
		/// <summary>
		/// Set current image
		/// </summary>
		public Image Image
		{
			get
			{
				return this.image;
			}
			set
			{
				this.image  = value;
				if (this.image != null)
				{
					this.Width  = (int)(this.image.Width  * this.zoom);
					this.Height = (int)(this.image.Height * this.zoom);
				}
			}
		}

		/// <summary>
		/// 
		/// </summary>
		public double Zoom
		{
			get
			{
				return this.zoom;
			}
			set
			{
				if (value != this.zoom)
				{
					this.zoom   = value;
					if (this.image != null)
					{
						Region region = new Region(this.ClientRectangle);
						this.Width  = (int)(this.image.Width  * this.zoom);
						this.Height = (int)(this.image.Height * this.zoom);
						region.Union(this.ClientRectangle);
						this.Invalidate(region); //TODO is it worth it to refresh max rectangle?
					}
				}
			}
		}

		/// <summary>
		/// 
		/// </summary>
		public bool SelectionActive
		{
			get
			{
				return this.selection != null;
			}
			set
			{
				bool exists = this.selection != null;

				if (value)
				{
					if (!exists)
						this.selection = new ImageControl.Selection(this);
					this.selection.Start();
				}
				else if (exists)
				{
					
					this.selection.End();
					this.selection = null;
					this.Invalidate();
				}
			}
		}

		/// <summary>
		/// 
		/// </summary>
		public ImageControl.PaintStyle PaintMode
		{
			set
			{
				if (value == this.paintStyle)
					return;
				else
					this.paintStyle = value;

				if (value == PaintStyle.PAINT_HIGHQUALITY)
				{

					this.paintDelegate = new ImageControlPaintDelegate(this.paintHighQuality);
				}
				else if (value == PaintStyle.PAINT_PIXELATED)
				{
					this.paintDelegate = new ImageControlPaintDelegate(this.paintPixelated);
				}
				else
				{
					this.paintDelegate = null;
				}
			}
			get
			{
				return this.paintStyle;
			}
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="gfx"></param>
		private void paintPixelated(System.Windows.Forms.PaintEventArgs e)
		{
			Rectangle destination;
			e.Graphics.PixelOffsetMode   = PixelOffsetMode.HighQuality;

			if (this.zoom < 1.0)	//It doesn't make sense to pixelate an scaled down picture
			{
				e.Graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
				destination                  = e.ClipRectangle; 
			}
			else
			{
				e.Graphics.InterpolationMode = InterpolationMode.NearestNeighbor;
				int iZoom                    = (int)this.zoom;
				destination                  = new Rectangle(
					e.ClipRectangle.X - (e.ClipRectangle.X % iZoom),
					e.ClipRectangle.Y - (e.ClipRectangle.Y % iZoom),
					e.ClipRectangle.Right  + (iZoom - (e.ClipRectangle.Right % iZoom)),
					e.ClipRectangle.Bottom + (iZoom - (e.ClipRectangle.Bottom % iZoom)));
			}

			e.Graphics.DrawImage(
				this.image,  
				destination,
				(int)(destination.X      / this.zoom), 
				(int)(destination.Y      / this.zoom), 
				(int)(destination.Width  / this.zoom), 
				(int)(destination.Height / this.zoom),
				GraphicsUnit.Pixel);
		}
		#endregion

		#region Events
	
		/// <summary>
		/// 
		/// </summary>
		public event SelectionChangedEventHandler SelectionChanged;

		/// <summary>
		/// 
		/// </summary>
		/// <param name="e"></param>
		protected virtual void OnSelectionChanged(Rectangle rec) 
		{
			if (SelectionChanged != null)
				SelectionChanged(this, rec);
		}

		#endregion

		/// <summary>
		/// 
		/// </summary>
		/// <param name="gfx"></param>
		private void paintHighQuality(System.Windows.Forms.PaintEventArgs e)
		{
			e.Graphics.InterpolationMode = InterpolationMode.Bicubic;	//TODO  adaptative with size or zoom
			e.Graphics.PixelOffsetMode   = PixelOffsetMode.HighQuality;
			e.Graphics.DrawImage(
				this.image,  
				e.ClipRectangle,
				(int)(e.ClipRectangle.X      / this.zoom), 
				(int)(e.ClipRectangle.Y      / this.zoom), 
				(int)(e.ClipRectangle.Width  / this.zoom), 
				(int)(e.ClipRectangle.Height / this.zoom),
				GraphicsUnit.Pixel);
		}


		/// <summary>
		/// 
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></para
		private void ImageControl_Paint(object sender, System.Windows.Forms.PaintEventArgs e)
		{
			if (this.image != null)
			{
				this.paintDelegate(e);
			}
		}

		/// <summary>
		/// Class to isolate Selection behavior
		/// </summary>
		private class Selection
		{
			double zoom          = 1.0;
			bool shadeNonActive  = true;
			Point start          = new Point(0, 0);
			Point end            = new Point(0, 0);
			Cursor cursor        = null;
			ImageControl control = null;
			Color color          = Color.FromArgb(64, SystemColors.Highlight);

			#region Constructor/Dispose
			/// <summary>
			/// Constructor
			/// </summary>
			/// <param name="control"></param>
			public Selection(ImageControl control)
			{
				this.control = control;
			}

			#endregion

			/// <summary>
			/// 
			/// </summary>
			public void Start()
			{
				this.start.X = this.end.X = this.start.Y = this.end.Y = 0;
				
				this.cursor				= Cursor.Current;
				this.control.Cursor		= Cursors.Cross;
				this.control.Paint     +=new PaintEventHandler(Selection_OnPaint);
				this.control.MouseDown +=new MouseEventHandler(Selection_MouseDown);
				this.control.MouseUp   +=new MouseEventHandler(Selection_MouseUp);
				this.control.MouseMove +=new MouseEventHandler(Selection_MouseMove);
			}

			/// <summary>
			/// 
			/// </summary>
			public void End()
			{
				this.control.MouseMove -=new MouseEventHandler(Selection_MouseMove);
				this.control.MouseUp   -=new MouseEventHandler(Selection_MouseUp);
				this.control.MouseDown -=new MouseEventHandler(Selection_MouseDown);
				this.control.Paint	   -=new PaintEventHandler(Selection_OnPaint);
				this.control.Cursor		= this.cursor;
				this.cursor				= null;
				this.control.OnSelectionChanged(Rectangle.Empty);
			}

			/// <summary>
			/// Evaluates if there is a selection
			/// </summary>
			/// <returns></returns>
			public bool Empty()
			{
				return this.start.X == this.end.X && this.start.Y == this.end.Y;
			}
			

			/// <summary>
			/// 
			/// </summary>
			/// <param name="x"></param>
			/// <param name="y"></param>
			/// <param name="zoom"></param>
			public void endSelection(int x, int y)
			{
				//Draw to remove
				drawSelection();

				this.end.X          = x;
				this.end.Y          = y;
				this.zoom           = this.control.zoom;
				this.shadeNonActive = true;
				
				//Invalidate to ShadeNonActive area
				this.control.Invalidate();
				this.control.OnSelectionChanged(this.ImageRectangle);
			}

			/// <summary>
			/// 
			/// </summary>
			/// <param name="x"></param>
			/// <param name="y"></param>
			/// <param name="zoom"></param>
			public void updateSelection(int x, int y)
			{
				//Draw Existing	to remove
				drawSelection();

				this.end.X          = x;
				this.end.Y          = y;
				this.shadeNonActive = false;
				
				//Draw New 
				drawSelection();
			}

			/// <summary>
			/// 
			/// </summary>
			/// <param name="x"></param>
			/// <param name="y"></param>
			public void startSelection(int x, int y)
			{
				//Remove preexisting selection
				if (this.start.X != this.end.X)
					cleanSelection();

				this.start.X = this.end.X = x;
				this.start.Y = this.end.Y = y;
				this.shadeNonActive = false;
				//draw Basic
				drawSelection();
			}

			/// <summary>
			/// 
			/// </summary>
			private void drawSelection()
			{
				Rectangle rec = this.control.RectangleToScreen(this.ControlRectangle);
				ControlPaint.DrawReversibleFrame(rec, this.color, FrameStyle.Dashed);
			}

			/// <summary>
			/// 
			/// </summary>
			private void cleanSelection()
			{
				this.control.Invalidate();
			}



			/// <summary>
			/// 
			/// </summary>
			public int X
			{
				get
				{
					int val = Math.Min(this.start.X, this.end.X);
					return (this.zoom >= 1.0) ? val - ((val % (int)this.zoom)) : val;
				}
			}

			/// <summary>
			/// 
			/// </summary>
			public int Y
			{
				get
				{
					int val = Math.Min(this.start.Y, this.end.Y);
					return (this.zoom >= 1.0) ? (val - (val % (int)this.zoom)) : val;
				}
			}

			/// <summary>
			/// 
			/// </summary>
			public int Width
			{
				get
				{
					int min   = Math.Min(this.start.X, this.end.X);
					int max   = Math.Max(this.start.X, this.end.X);
					int iZoom = (int)this.zoom;
					return (this.zoom < 1.0) ? (max - min)
						: ((max + (iZoom - (max % iZoom))) - (min - (min % iZoom)));
				}
			}

			/// <summary>
			/// 
			/// </summary>
			public int Height
			{
				get
				{
					int min   = Math.Min(this.start.Y, this.end.Y);
					int max   = Math.Max(this.start.Y, this.end.Y);
					int iZoom = (int)this.zoom;
					return (this.zoom < 1.0) ? (max - min)
						: ((max + (iZoom - (max % iZoom))) - (min - (min % iZoom)));					
				}
			}

			/// <summary>
			/// 
			/// </summary>
			public Rectangle ControlRectangle
			{
				get
				{
					return new Rectangle(this.X, this.Y, this.Width, this.Height);
				}
			}

			/// <summary>
			/// 
			/// </summary>
			public Rectangle ImageRectangle
			{
				get
				{
					if (this.Empty())
					{
						return Rectangle.Empty;
					}
					else
					{
						Rectangle rectangle = this.ControlRectangle;
						rectangle.X = (int)(rectangle.X / this.control.zoom);
						rectangle.Y = (int)(rectangle.Y / this.control.zoom);
						rectangle.Width  = (int)(rectangle.Width / this.control.zoom);
						rectangle.Height = (int)(rectangle.Height / this.control.zoom);
						return rectangle;
					}
				}
			}


			/// <summary>
			/// 
			/// </summary>
			/// <param name="sender"></param>
			/// <param name="args"></param>
			private void Selection_OnPaint(object sender, System.Windows.Forms.PaintEventArgs e)
			{
				Rectangle rectangle = this.ControlRectangle;
				if (this.shadeNonActive)
				{
					Region paintRegion = new Region(e.ClipRectangle);
					paintRegion.Exclude(rectangle);
					e.Graphics.FillRegion(new SolidBrush(this.color), paintRegion);
				}
				//float width = (this.control.PaintMode == PaintStyle.PAINT_HIGHQUALITY) ? 1.0f : Math.Max((float)this.control.zoom, 1.0f);
				//e.Graphics.DrawRectangle(new Pen(SystemColors.Highlight, width), rectangle);
				//ControlPaint.DrawSelectionFrame(e.Graphics, true, rectangle, rectangle,this.color);
			}

			/// <summary>
			/// 
			/// </summary>
			/// <param name="sender"></param>
			/// <param name="e"></param>
			private void Selection_MouseDown(object sender, System.Windows.Forms.MouseEventArgs e)
			{
				if (e.Button == MouseButtons.Left)
				{
					this.control.Capture = true;
					this.startSelection(e.X, e.Y);
				}
			}

			/// <summary>
			/// 
			/// </summary>
			/// <param name="sender"></param>
			/// <param name="e"></param>
			private void Selection_MouseUp(object sender, System.Windows.Forms.MouseEventArgs e)
			{
				if(e.Button == MouseButtons.Left)
				{
					this.endSelection(e.X, e.Y);
					this.control.Capture = false;
				}
			}

			/// <summary>
			/// 
			/// </summary>
			/// <param name="sender"></param>
			/// <param name="e"></param>
			private void Selection_MouseMove(object sender, System.Windows.Forms.MouseEventArgs e)
			{
				if(e.Button == MouseButtons.Left)
				{
					this.updateSelection(e.X, e.Y);
				}
			}
		}
	}
}
