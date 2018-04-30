using System;
using System.Drawing;
using System.Windows.Forms;

namespace Ima.Controls
{
	/// <summary>
	/// Summary description for PreviewForm.
	/// </summary>
	public class PreviewForm : Ima.Controls.ToolForm
	{
		#region Constants
		/// <summary>
		/// Default Width
		/// </summary>
		protected static int BASE_WIDTH = 240;

		/// <summary>
		/// Default Height
		/// </summary>
		protected static int BASE_HEIGHT = 180;
		#endregion

		#region Fields
		/// <summary>
		/// Image used to display 
		/// </summary>
		protected PictureBox imageBox;

		/// <summary>
		/// Reference to the bitmap to modify
		/// </summary>
		protected ImageWrapper image;

		/// <summary>
		/// Used to track when is the first time this form is shown
		/// </summary>
		protected bool firstTime = true;
		#endregion

		#region Constructors

		/// <summary>
		/// Constructor
		/// </summary>
		public PreviewForm() 
			: this ("Preview", null)
		{
		}

		/// <summary>
		/// Constructor
		/// </summary>
		/// <param name="title">Tool Window title (also used as internal name)</param>
		/// <param name="bitmap">Default Image Wrapper to use</param>
		public PreviewForm(string title, ImageWrapper imagewrapper) 
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
		public PreviewForm(string title, int w, int h, ImageWrapper imagewrapper) 
			: base(title)
		{
			InitializeComponent();
			this.ClientSize = new Size(w + 2 * this.imageBox.Location.X,
				h + 2 * this.imageBox.Location.Y);

			this.imageBox.Size = new Size(w, h);

			if (imagewrapper != null)
			{
				this.image = imagewrapper;
				this.image.Changed += new ImageChangedEventHandler(OnOriginalBitmapChanged);
			}
		}
		#endregion

		#region Windows Form Designer generated code
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.imageBox = new System.Windows.Forms.PictureBox();
			this.SuspendLayout();
			// 
			// imageBox
			// 
			this.imageBox.Location = new System.Drawing.Point(5, 5);
			this.imageBox.Name = "imageBox";
			this.imageBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
			this.imageBox.TabIndex = 0;
			this.imageBox.TabStop = false;
			// 
			// PreviewForm
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.ClientSize = new System.Drawing.Size(292, 266);
			this.Controls.Add(this.imageBox);
			this.Name = "PreviewForm";
			this.Closing += new System.ComponentModel.CancelEventHandler(this.PreviewForm_Closing);
			this.VisibleChanged += new System.EventHandler(this.PreviewForm_VisibleChanged);
			this.ResumeLayout(false);

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

			this.imageBox.Image = this.image.GetThumbnail((int)(this.image.Width  / factor),
																  (int)(this.image.Height / factor), true);
		}

		/// <summary>
		/// Nice clean-up
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void PreviewForm_Closing(object sender, System.ComponentModel.CancelEventArgs e)
		{
			this.image.Changed -= new ImageChangedEventHandler(OnOriginalBitmapChanged);
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
				if (this.Visible && firstTime)
				{
					firstTime = false;
					OnOriginalBitmapChanged(this.image, null);
				}
			}
		}
	}
}