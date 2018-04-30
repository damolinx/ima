using System;
using System.Windows.Forms;

namespace Ima.Controls
{
	/// <summary>
	/// 
	/// </summary>
	public class ThumbnailForm : Ima.Controls.PreviewForm
	{
		/// <summary>
		/// Singleton.  This may change, but not now
		/// </summary>
		private static ThumbnailForm instance;

		/// <summary>
		/// Constructor
		/// </summary>
		/// <param name="title"></param>
		/// <param name="bitmap"></param>
		private ThumbnailForm(string title, ImageWrapper imagewrapper) : base(title, imagewrapper)
		{
			this.imageBox.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right | AnchorStyles.Bottom;
			this.Closed         += new EventHandler(ThumbnailForm_Closed);
		}

		/// <summary>
		/// Single instance is Visible
		/// </summary>
		/// <returns></returns>
		public static bool IsVisible()
		{
			return (ThumbnailForm.instance != null) ? ThumbnailForm.instance.Visible : false;
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="owner"></param>
		/// <param name="image"></param>
		public static void ShowView(Form owner, ImageWrapper image)
		{
			if(ThumbnailForm.instance == null)
			{
				ThumbnailForm.instance = new ThumbnailForm("Thumbnail", image);
			}
			else
			{
				ThumbnailForm.instance.image = image;
			}
			ThumbnailForm.instance.Owner = owner;
			ThumbnailForm.instance.TopMost = true;
			ThumbnailForm.instance.Show();
		}

		/// <summary>
		/// 
		/// </summary>
		public static void CloseView()
		{
			if(ThumbnailForm.instance != null)
			{
				ThumbnailForm.instance.Close();
			}
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void ThumbnailForm_Closed(object sender, EventArgs e)
		{
			ThumbnailForm.instance = null;
		}

		/// <summary>
		/// If image is changed, this event allows to refresh view
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="args"></param>
		public override void OnOriginalBitmapChanged(object sender, ImageChangedEventArgs args)
		{
			double factor = Math.Max(1,
				Math.Max(((double)this.image.Width) / imageBox.Width,
				((double)this.image.Height) / imageBox.Height));

			this.imageBox.Image = this.image.GetThumbnail((int)(this.image.Width  / factor),
				(int)(this.image.Height / factor), true);
		}
	}
}
