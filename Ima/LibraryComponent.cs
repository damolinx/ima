using System;
using System.IO;
using System.Data;
using System.Text;
using System.Threading;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace Ima
{
	#region Delegates
	/// <summary>
	/// Notification function for an image change
	/// </summary>
	public delegate void ImageOpenEventHandler(object sender, ImageOpenEventArgs e);
	/// <summary>
	/// Delegate to add a item to current view
	/// </summary>
	public delegate void DelegateAddLibraryItem(LibraryListItem item);
	/// <summary>
	/// Delegate to notify thread conclusion
	/// </summary>
	public delegate void DelegateThreadFinished();
	#endregion

	/// <summary>
	/// Summary description for LibraryComponent.
	/// </summary>
	public class LibraryComponent : Ima.GenericComponent
	{	
		#region Constants
		protected const int ImageListGenericIcon = 0;
		protected const int ImageListFailedIcon  = 1;
		protected const int ImageListThumbnailStartIndex = ImageListFailedIcon + 1;
		#endregion

		#region Components
		private Ima.Controls.ImageListView listView;
		private System.Windows.Forms.ToolTip tipListView;
		private System.Windows.Forms.ContextMenu contextMenu;
		private System.Windows.Forms.MenuItem menuItem_ListItem_Copy;
		private System.Windows.Forms.MenuItem menuItem_ListItem_Paste;
		private System.Windows.Forms.ToolBarButton tbBtn_Rotate_CCW;
		private System.Windows.Forms.ToolBarButton tbBtn_Rotate_CW;
		private System.Windows.Forms.ToolBarButton tbBtn_Edit;
		private System.Windows.Forms.ToolBarButton tbBtn_Separator_1;
		private System.Windows.Forms.ToolBarButton tbBtn_Wallpaper;
		private System.Windows.Forms.ToolBarButton tbBtn_Print;
		private System.Windows.Forms.MenuItem menuItem_ListItem_Delete;
		private System.Windows.Forms.MenuItem menuItem_ListItem_Rename;
		private System.Windows.Forms.MenuItem menuItem_ListItem_Edit;
		private System.Windows.Forms.MenuItem menuItem_ListItem_Separator_1;
		private System.Windows.Forms.MenuItem menuItem_ListItem_Separator_2;
		private System.Windows.Forms.MenuItem menuItem_ListItem_Separator_3;
		private System.Windows.Forms.MenuItem menuItem_ListItem_Properties;
		private System.ComponentModel.IContainer components;
		private System.Windows.Forms.ImageList previewImageList;
		#endregion
		
		/// <summary>
		/// Library Item being displayed
		/// </summary>
		LibraryItem library;
		/// <summary>
		/// Image Thread
		/// </summary>
		Thread imageThread;
		/// <summary>
		/// Call to manually stop thread
		/// </summary>
		ManualResetEvent eventThreadStop;
		/// <summary>
		/// Call when thread finalized
		/// </summary>
		ManualResetEvent eventThreadFinish;
		/// <summary>
		/// Delegate to handle teh addition of a new library item
		/// </summary>
		DelegateAddLibraryItem delegateAddLibraryItem;
		/// <summary>
		/// 
		/// </summary>
		DelegateThreadFinished delegateThreadFinished;
		/// <summary>
		/// Misc var used to display proper tooltip
		/// </summary>
		LibraryListItem lastLibListItem;
		/// <summary>
		/// reference to the proper IStatusNotification object
		/// </summary>
		protected IStatusNotification status;
		/// <summary>
		/// Constructor
		/// </summary>
		public LibraryComponent()
		{
			// This call is required by the Windows.Forms Form Designer.
			InitializeComponent();

			// TODO: Add any initialization after the InitializeComponent call
			eventThreadStop        = new ManualResetEvent(false);
			eventThreadFinish      = new ManualResetEvent(false);
			delegateAddLibraryItem = new DelegateAddLibraryItem(this.AddLibraryItem);
			delegateThreadFinished = new DelegateThreadFinished(this.ThreadFinished);
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="disposing"></param>
		protected override void Dispose(bool disposing)
		{
			this.StopThread();
			if( disposing )
			{
				if(components != null)
				{
					components.Dispose();
				}
			}
			base.Dispose (disposing);
		}

		#region Component Designer generated code
		/// <summary> 
		/// Required method for Designer support - do not modify 
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.components = new System.ComponentModel.Container();
			System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(LibraryComponent));
			this.listView = new Ima.Controls.ImageListView();
			this.contextMenu = new System.Windows.Forms.ContextMenu();
			this.menuItem_ListItem_Edit = new System.Windows.Forms.MenuItem();
			this.menuItem_ListItem_Separator_1 = new System.Windows.Forms.MenuItem();
			this.menuItem_ListItem_Copy = new System.Windows.Forms.MenuItem();
			this.menuItem_ListItem_Paste = new System.Windows.Forms.MenuItem();
			this.menuItem_ListItem_Rename = new System.Windows.Forms.MenuItem();
			this.menuItem_ListItem_Separator_2 = new System.Windows.Forms.MenuItem();
			this.menuItem_ListItem_Delete = new System.Windows.Forms.MenuItem();
			this.menuItem_ListItem_Separator_3 = new System.Windows.Forms.MenuItem();
			this.menuItem_ListItem_Properties = new System.Windows.Forms.MenuItem();
			this.previewImageList = new System.Windows.Forms.ImageList(this.components);
			this.tipListView = new System.Windows.Forms.ToolTip(this.components);
			this.tbBtn_Rotate_CCW = new System.Windows.Forms.ToolBarButton();
			this.tbBtn_Rotate_CW = new System.Windows.Forms.ToolBarButton();
			this.tbBtn_Edit = new System.Windows.Forms.ToolBarButton();
			this.tbBtn_Separator_1 = new System.Windows.Forms.ToolBarButton();
			this.tbBtn_Wallpaper = new System.Windows.Forms.ToolBarButton();
			this.tbBtn_Print = new System.Windows.Forms.ToolBarButton();
			this.panel.SuspendLayout();
			this.bottomPanel.SuspendLayout();
			this.SuspendLayout();
			// 
			// panel
			// 
			this.panel.Controls.Add(this.listView);
			this.panel.Name = "panel";
			this.panel.Size = new System.Drawing.Size(400, 288);
			// 
			// imageList
			// 
			this.imageList.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList.ImageStream")));
			this.imageList.TransparentColor = System.Drawing.Color.White;
			// 
			// toolBar
			// 
			this.toolBar.Buttons.AddRange(new System.Windows.Forms.ToolBarButton[] {
																					   this.tbBtn_Rotate_CCW,
																					   this.tbBtn_Rotate_CW,
																					   this.tbBtn_Edit,
																					   this.tbBtn_Separator_1,
																					   this.tbBtn_Wallpaper,
																					   this.tbBtn_Print});
			this.toolBar.Name = "toolBar";
			this.toolBar.Size = new System.Drawing.Size(400, 56);
			this.toolBar.ButtonClick += new System.Windows.Forms.ToolBarButtonClickEventHandler(this.toolBar_ButtonClick);
			// 
			// bottomPanel
			// 
			this.bottomPanel.Location = new System.Drawing.Point(0, 288);
			this.bottomPanel.Name = "bottomPanel";
			this.bottomPanel.Size = new System.Drawing.Size(400, 56);
			// 
			// listView
			// 
			this.listView.AllowDrop = true;
			this.listView.ContextMenu = this.contextMenu;
			this.listView.Dock = System.Windows.Forms.DockStyle.Fill;
			this.listView.HideSelection = false;
			this.listView.LabelEdit = true;
			this.listView.LargeImageList = this.previewImageList;
			this.listView.Location = new System.Drawing.Point(0, 0);
			this.listView.Name = "listView";
			this.listView.Size = new System.Drawing.Size(400, 288);
			this.listView.Sorting = System.Windows.Forms.SortOrder.Ascending;
			this.listView.TabIndex = 0;
			this.listView.DoubleClick += new System.EventHandler(this.listView_DoubleClick);
			this.listView.AfterLabelEdit += new System.Windows.Forms.LabelEditEventHandler(this.listView_AfterLabelEdit);
			this.listView.SelectedIndexChanged += new System.EventHandler(this.listView_SelectedIndexChanged);
			this.listView.MouseMove += new System.Windows.Forms.MouseEventHandler(this.listView_MouseMove);
			this.listView.Enter += new System.EventHandler(this.listView_Enter);
			// 
			// contextMenu
			// 
			this.contextMenu.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
																						this.menuItem_ListItem_Edit,
																						this.menuItem_ListItem_Separator_1,
																						this.menuItem_ListItem_Copy,
																						this.menuItem_ListItem_Paste,
																						this.menuItem_ListItem_Rename,
																						this.menuItem_ListItem_Separator_2,
																						this.menuItem_ListItem_Delete,
																						this.menuItem_ListItem_Separator_3,
																						this.menuItem_ListItem_Properties});
			// 
			// menuItem_ListItem_Edit
			// 
			this.menuItem_ListItem_Edit.DefaultItem = true;
			this.menuItem_ListItem_Edit.Index = 0;
			this.menuItem_ListItem_Edit.Text = "&Edit...";
			this.menuItem_ListItem_Edit.Click += new System.EventHandler(this.menuItem_ListItem_Edit_Click);
			// 
			// menuItem_ListItem_Separator_1
			// 
			this.menuItem_ListItem_Separator_1.Index = 1;
			this.menuItem_ListItem_Separator_1.Text = "-";
			// 
			// menuItem_ListItem_Copy
			// 
			this.menuItem_ListItem_Copy.Index = 2;
			this.menuItem_ListItem_Copy.Text = "&Copy";
			this.menuItem_ListItem_Copy.Click += new System.EventHandler(this.menuItem_ListItem_Copy_Click);
			// 
			// menuItem_ListItem_Paste
			// 
			this.menuItem_ListItem_Paste.Index = 3;
			this.menuItem_ListItem_Paste.Text = "&Paste";
			this.menuItem_ListItem_Paste.Click += new System.EventHandler(this.menuItem_ListItem_Paste_Click);
			// 
			// menuItem_ListItem_Rename
			// 
			this.menuItem_ListItem_Rename.Index = 4;
			this.menuItem_ListItem_Rename.Text = "&Rename...";
			this.menuItem_ListItem_Rename.Click += new System.EventHandler(this.menuItem_ListItem_Rename_Click);
			// 
			// menuItem_ListItem_Separator_2
			// 
			this.menuItem_ListItem_Separator_2.Index = 5;
			this.menuItem_ListItem_Separator_2.Text = "-";
			// 
			// menuItem_ListItem_Delete
			// 
			this.menuItem_ListItem_Delete.Index = 6;
			this.menuItem_ListItem_Delete.Text = "&Delete";
			this.menuItem_ListItem_Delete.Click += new System.EventHandler(this.menuItem_ListItem_Delete_Click);
			// 
			// menuItem_ListItem_Separator_3
			// 
			this.menuItem_ListItem_Separator_3.Index = 7;
			this.menuItem_ListItem_Separator_3.Text = "-";
			// 
			// menuItem_ListItem_Properties
			// 
			this.menuItem_ListItem_Properties.Index = 8;
			this.menuItem_ListItem_Properties.Text = "Propert&ies";
			this.menuItem_ListItem_Properties.Click += new System.EventHandler(this.menuItem_ListItem_Properties_Click);
			// 
			// previewImageList
			// 
			this.previewImageList.ColorDepth = System.Windows.Forms.ColorDepth.Depth32Bit;
			this.previewImageList.ImageSize = new System.Drawing.Size(128, 128);
			this.previewImageList.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("previewImageList.ImageStream")));
			this.previewImageList.TransparentColor = System.Drawing.Color.Transparent;
			// 
			// tipListView
			// 
			this.tipListView.Active = false;
			this.tipListView.AutoPopDelay = 5000;
			this.tipListView.InitialDelay = 1000;
			this.tipListView.ReshowDelay = 500;
			this.tipListView.ShowAlways = true;
			// 
			// tbBtn_Rotate_CCW
			// 
			this.tbBtn_Rotate_CCW.ImageIndex = 0;
			this.tbBtn_Rotate_CCW.Text = "Rot. &Left";
			// 
			// tbBtn_Rotate_CW
			// 
			this.tbBtn_Rotate_CW.ImageIndex = 1;
			this.tbBtn_Rotate_CW.Text = "Rot. &Right";
			// 
			// tbBtn_Edit
			// 
			this.tbBtn_Edit.ImageIndex = 2;
			this.tbBtn_Edit.Text = "&Edit";
			// 
			// tbBtn_Separator_1
			// 
			this.tbBtn_Separator_1.Style = System.Windows.Forms.ToolBarButtonStyle.Separator;
			// 
			// tbBtn_Wallpaper
			// 
			this.tbBtn_Wallpaper.ImageIndex = 3;
			this.tbBtn_Wallpaper.Text = "&Wallpaper";
			// 
			// tbBtn_Print
			// 
			this.tbBtn_Print.ImageIndex = 4;
			this.tbBtn_Print.Text = "&Print";
			// 
			// LibraryComponent
			// 
			this.Name = "LibraryComponent";
			this.panel.ResumeLayout(false);
			this.bottomPanel.ResumeLayout(false);
			this.ResumeLayout(false);

		}
		#endregion

		/// <summary>
		/// 
		/// </summary>
		public IStatusNotification StatusNotification
		{
			get
			{
				return this.status;
			}
			set
			{
				if ((this.status = value) != null)
				{
					
				}
				listView_SelectedIndexChanged(this, System.EventArgs.Empty);
			}
		}

		/// <summary>
		/// 
		/// </summary>
		public Ima.Controls.ImageListView ListView
		{
			get
			{
				return this.listView;
			}
		}

		/// <summary>
		/// 
		/// </summary>
		public LibraryItem Library
		{
			get
			{
				return this.library;
			}
			set
			{
				if (this.library == value)
					return;
				this.library = value;
				//Stop current loading
				this.StopThread();
				//Init
				this.listView.Clear();
				this.lastLibListItem = null;
				//Remove previous thumbnails but leave special images
				for(int i = this.previewImageList.Images.Count - 1; i >= ImageListThumbnailStartIndex; i--)
				{
					this.previewImageList.Images.RemoveAt(i);
				}
				//Reset events
				this.eventThreadStop.Reset();
				this.eventThreadFinish.Reset();
				//After clearing, return if nothing more to do
				if (this.library == null)
					return;
				//Send Message to ListView for doublebuff
				this.listView.SetExStyles();
				//Create and Launch Thread
				if (this.status != null)
				{
					this.status.StatusMessage("Loading '" + this.library.Name + "'");
					this.status.StartProgress(0, 100);
				}

                loadImages();
                //this.imageThread      = new Thread(new ThreadStart(this.loadImages));
                //this.imageThread.Name = "Library Image loading: " + this.library.Name;
                //this.imageThread.Start();
			}
		}

		/// <summary>
		/// 
		/// </summary>
		/// <returns></returns>
		protected int getCouldNotLoadImage()
		{
			if (this.previewImageList.Images[ImageListFailedIcon] == null)
			{
				Size largeSize = this.previewImageList.ImageSize;
				Bitmap bitmap  = new Bitmap(largeSize.Width, largeSize.Height);
				Graphics gfx   = Graphics.FromImage(bitmap);				
				int dw         = largeSize.Width  / 5;
				int dh         = largeSize.Height / 5;
				int hw         = largeSize.Width  / 50;
				
				gfx.DrawRectangle(SystemPens.ControlLight, 0, 0, 
					largeSize.Width - 1, largeSize.Height - 1);
				LinearGradientBrush brush = new LinearGradientBrush(new Rectangle(0, 0, largeSize.Width, largeSize.Height), 
					SystemColors.ControlLightLight, 
					SystemColors.ControlDarkDark,
					LinearGradientMode.Vertical);
				brush.SetBlendTriangularShape(0.5f);
				
				gfx.FillPolygon(brush, new Point[]{new Point( hw + dw, -hw + dh), 
													  new Point(-hw + dw,  hw + dh),
													  new Point(largeSize.Width - hw - dw, largeSize.Height + hw - dh), 
													  new Point(largeSize.Width + hw - dw, largeSize.Height - hw - dh)});

				gfx.FillPolygon(brush, new Point[]{new Point(largeSize.Width - hw - dw, -hw + dh),
													  new Point(largeSize.Width + hw - dw,  hw + dh),
													  new Point( hw + dw, largeSize.Height + hw - dh),
													  new Point(-hw + dw, largeSize.Height - hw - dh)});
				
				this.previewImageList.Images[ImageListFailedIcon] = bitmap;
			}
			return ImageListFailedIcon;
		}

		/// <summary>
		/// 
		/// </summary>
		/// <returns></returns>
		protected int getGenericImage(string extension)
		{
			//TODO return a proper generic image
			return ImageListGenericIcon;
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="item"></param>
		/// <param name="index"></param>
		/// <param name="size"></param>
		public void generateThumbnail(LibraryListItem item)
		{
			double ratio     = 0.0;
			Image image      = null;
			Stream stream    = null;
			Size largeSize   = this.previewImageList.ImageSize;
			Bitmap thumbnail = new Bitmap(largeSize.Width, largeSize.Height);
			
			try
			{
				stream        = new FileStream(item.Item.Path, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
				image         = Image.FromStream(stream);
				
				//Save some data
				item.Size     = image.Size;
				item.FileSize = stream.Length;
				
				//Draw Thumbnail
				if (image.Width <= largeSize.Width && image.Height <= largeSize.Height)
				{
					ratio = 1.0;
				}
				else
				{
					ratio = Math.Min(((double)largeSize.Width) / image.Width, ((double)largeSize.Height) / image.Height);
				}
				Rectangle rec = new Rectangle(0, 0, (int)(image.Width * ratio),(int)(image.Height * ratio));
				Graphics gfx  = Graphics.FromImage(thumbnail);
				gfx.Clear(SystemColors.Window);
				gfx.PixelOffsetMode   = PixelOffsetMode.HighQuality;
				gfx.InterpolationMode = InterpolationMode.High;
				gfx.DrawImage(image, (largeSize.Width - rec.Width) / 2, (largeSize.Height - rec.Height) / 2, rec.Width, rec.Height);
			}
			catch(Exception e)
			{
				Console.Out.WriteLine("There was an error while trying to open image '" + item.Text + "'.\n\nError Reason:\n" + e.Message + "\n" + e.StackTrace);
				thumbnail.Dispose();
				thumbnail = null;
			}
			finally
			{
				if (image != null) 
					image.Dispose();
				if (stream != null) 
					stream.Close();
			}

			if (thumbnail == null)
			{
				item.ImageIndex = getCouldNotLoadImage();
			}
			else if (item.ImageIndex >= ImageListThumbnailStartIndex)
			{
				int index       = item.ImageIndex;
				this.previewImageList.Images[index] = thumbnail;
			}
			else
			{ 
				item.ImageIndex = this.previewImageList.Images.Add(thumbnail, Color.Transparent);
			}
		}

		/// <summary>
		/// 
		/// </summary>
		private void loadImages()
		{
			LibraryItem[] items = this.library.GetImageItems(true);

			//AUTO Arrange Removes almost all flickering while adding on forst pass!!
			//this.listView.AutoArrange = false;  
			// Stage 1:  Fast loading with generic icon
			foreach(LibraryItem libraryItem in items)
			{
				LibraryListItem libraryListItem = new LibraryListItem(libraryItem);
				this.Invoke(this.delegateAddLibraryItem, new Object[] {libraryListItem});
				Application.DoEvents();
				libraryListItem.ImageIndex      = getGenericImage(Path.GetExtension(libraryListItem.Item.Path));
				
				if ( eventThreadStop.WaitOne(0, true))
				{
					eventThreadFinish.Set();
					return;
				}
			}
			this.listView.AutoArrange = true;

			// Stage 2:  Slower Thumbnail Generation
			foreach(LibraryListItem libraryListItem in this.listView.Items)
			{
				this.generateThumbnail(libraryListItem);

				if ( eventThreadStop.WaitOne(0, true))
				{
					eventThreadFinish.Set();
					return;
				}
			}
			try
			{
				this.Invoke(this.delegateThreadFinished, null);
			}
			catch
			{
				Console.Out.WriteLine("TODO:   Invoke fails if application is closed and not cancelled on time");
			}
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="item"></param>
		private void AddLibraryItem(LibraryListItem item)
		{
			if (this.status != null) 
			{
				this.status.Progress = (int)(100.0 * (this.listView.Items.Count + 1.0) / this.Library.Count);
			}
			
			this.listView.Items.Add(item);

			if(this.listView.Items.Count == this.Library.Count)
			{
				this.listView.Items[0].Selected = true;
			}
		}

		/// <summary>
		/// 
		/// </summary>
		private void ThreadFinished()
		{
			if (this.status != null)
			{
				this.status.StatusMessage(this.Library.Count + (this.Library.Count == 1 ? " image" : " images"));
				this.status.EndProgress();
			}
		}

		/// <summary>
		/// 
		/// </summary>
		private void StopThread()
		{
			//TODO check this logic, as I think it is a flawed
			if ( this.imageThread != null)
			{
				// set event "Stop"
				this.eventThreadStop.Set();

				// wait for thread, it will either stop or finish
				while (this.imageThread.IsAlive)
				{
					// set event "Stop"
					this.eventThreadStop.Set();

					if ( WaitHandle.WaitAll(
						new ManualResetEvent[] {this.eventThreadFinish}, 50, false))
					{
						break;
					}
					Application.DoEvents();
				}
			}
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void listView_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			switch(this.listView.SelectedIndices.Count)
			{
				case 0:
					if (this.status != null && this.Library != null)
						this.status.StatusMessage(this.Library.Count + (this.Library.Count == 1 ? " image" : " images"));
					tbBtn_Rotate_CCW.Enabled = false;
					tbBtn_Rotate_CW.Enabled  = false;
					tbBtn_Edit.Enabled       = false;
					tbBtn_Wallpaper.Enabled  = false;
					tbBtn_Print.Enabled      = false;
					break;
				case 1:
					if (this.status != null)
						this.status.StatusMessage((this.listView.SelectedIndices[0] + 1) + " of " + this.Library.Count + (this.Library.Count == 1 ? " image" : " images"));
					tbBtn_Rotate_CCW.Enabled = true;
					tbBtn_Rotate_CW.Enabled  = true;
					tbBtn_Edit.Enabled       = true;
					tbBtn_Wallpaper.Enabled  = true;
					tbBtn_Print.Enabled      = true;
					break;
				default:
					if (this.status != null)
						this.status.StatusMessage( "Selected " + this.listView.SelectedIndices.Count + " of " + this.Library.Count + (this.Library.Count == 1 ? " image" : " images"));
					tbBtn_Rotate_CCW.Enabled = true;
					tbBtn_Rotate_CW.Enabled  = true;
					tbBtn_Edit.Enabled       = false;
					tbBtn_Wallpaper.Enabled  = false;
					tbBtn_Print.Enabled      = true;
					break;
			}
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void listView_Enter(object sender, System.EventArgs e)
		{
			listView_SelectedIndexChanged(sender, e);
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void listView_AfterLabelEdit(object sender, System.Windows.Forms.LabelEditEventArgs e)
		{
			if (e.Label == null || e.Label.Length == 0)
			{
				e.CancelEdit = true;
				MessageBox.Show(this, "Picture name can't be empty", Configuration.APPLICATION_NAME, MessageBoxButtons.OK, MessageBoxIcon.Error);
				return;
			}
			else
			{
				LibraryListItem item = this.listView.Items[e.Item] as LibraryListItem;
				string name = Path.Combine(Path.GetDirectoryName(item.Item.Path), e.Label + Path.GetExtension(item.Item.Path));
				try
				{
					File.Move(item.Item.Path, name);
					item.Item.Name = item.Text =  e.Label;
				}
				catch(Exception ex)
				{
					MessageBox.Show(this, "Failed to rename '" + Path.GetFileName(item.Item.Path) + "'\n\nSystem Error:\n" + ex.StackTrace, Configuration.Instance.GetProperty(Configuration.APPLICATION_NAME, string.Empty).ToString(), MessageBoxButtons.OK, MessageBoxIcon.Error);
				}
			}
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void listView_MouseMove(object sender, System.Windows.Forms.MouseEventArgs e)
		{
			LibraryListItem item = listView.GetItemAt(e.X, e.Y) as LibraryListItem;
			if (item == null)
			{
				this.tipListView.Active = false;
			}
			else if (item != lastLibListItem)
			{
				this.tipListView.Active = true;
				this.tipListView.SetToolTip(this.listView, "Name: " + Path.GetFileName(item.Item.Path) + "\n" +
					"Type: " + Path.GetExtension(item.Item.Path).Substring(1).ToUpper() + "\n" +
					"Dimensions: " + item.Size.Width + " x " + item.Size.Height + "\n" +
					"Size: " + (item.FileSize / 1024) + " KB");
			}
			lastLibListItem = item;
		}

		private void menuItem_ListItem_Copy_Click(object sender, System.EventArgs e)
		{
			string[] files = new string[this.listView.SelectedIndices.Count];
			for(int i = 0; i < files.Length; i++)
			{
				files[i] = ((LibraryListItem)listView.Items[this.listView.SelectedIndices[i]]).Item.Path;
			}
			Clipboard.SetDataObject(new DataObject(DataFormats.FileDrop, files));		
		}

		private void menuItem_ListItem_Paste_Click(object sender, System.EventArgs e)
		{
			IDataObject data = Clipboard.GetDataObject();
			if (data.GetDataPresent(DataFormats.FileDrop))
			{
				string[] files = (string[])data.GetData(DataFormats.FileDrop);
				foreach(string file in files)
				{
					string newname = Path.Combine(this.Library.Path, Path.GetFileName(file));
					if (!File.Exists(newname) || MessageBox.Show(this, "Image '" + file + "' already exists in .  Would you like to replace it?", "Replace", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
						File.Copy(file, newname, false);
				}
				//TODO: this should correct itself when I implement FileSystemWatchers
			}		
		}
		
		/// <summary>
		/// Event fired when any of the layers changed
		/// </summary>
		public event ImageOpenEventHandler OpenImage;
		
		/// <summary>
		/// 
		/// </summary>
		/// <param name="e"></param>
		protected virtual void OnOpenImage(ImageOpenEventArgs e) 
		{
			if (OpenImage != null)
				OpenImage(this, e);
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void listView_DoubleClick(object sender, EventArgs e)
		{
			Point point = listView.PointToClient(new Point(MousePosition.X, MousePosition.Y));
			LibraryListItem item = listView.GetItemAt(point.X, point.Y) as LibraryListItem;
			if (item != null)
			{
				OnOpenImage(new ImageOpenEventArgs(item));
			}
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void toolBar_ButtonClick(object sender, System.Windows.Forms.ToolBarButtonClickEventArgs e)
		{
			switch(this.toolBar.Buttons.IndexOf(e.Button))
			{
				case 0:
					rotateSelection(RotateFlipType.Rotate270FlipNone);
					break;
				case 1: 
					rotateSelection(RotateFlipType.Rotate90FlipNone);
					break;
				case 2: 
					OnOpenImage(new ImageOpenEventArgs(this.listView.SelectedItems[0] as LibraryListItem));
					break;
				case 4:
					LibraryListItem item = this.listView.SelectedItems[0] as LibraryListItem;
					if (item != null)
					{
						WallpaperDialog dialog = new WallpaperDialog(item.Item);
						dialog.ShowDialog(this);
					}
					break;
				case 5:
					string[] files = new string[this.listView.SelectedIndices.Count];
					for(int i = 0; i < files.Length; i++)
					{
						files[i] = ((LibraryListItem)this.listView.SelectedItems[i]).Item.Path;
					}
					NativeCalls.printFiles(files);
					break;
				default:
					break;
			}
		}

		/// <summary>
		/// 
		/// </summary>
		private void rotateSelection(RotateFlipType type)
		{
			//TODO: thread it
			StringBuilder builder = new StringBuilder();
			Cursor cursor         = Cursor.Current;
			Cursor.Current        = Cursors.WaitCursor;
			for(int i = 0, max = this.listView.SelectedIndices.Count; i < max; i++)
			{
				if (this.status != null) 
				{
					this.status.StatusMessage("Rotating " + (i + 1) + " of " + max + " images");
				}
				LibraryListItem libItem = this.listView.SelectedItems[i] as LibraryListItem;
				if (! ImageUtilities.RotateFlipImage(libItem.Item.Path, type))
				{
					builder.Append(libItem.Text);
				}
				else
				{
					this.generateThumbnail(libItem);
					this.listView.Update();
				}
				Application.DoEvents();
			}
			Cursor.Current = cursor;
			if (builder.Length > 0)
			{
				MessageBox.Show(this, "The following files could not be rotated: " + builder.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
			this.listView_SelectedIndexChanged(this.listView, EventArgs.Empty);
		}

		private void menuItem_ListItem_Delete_Click(object sender, System.EventArgs e)
		{
		
		}

		private void menuItem_ListItem_Rename_Click(object sender, System.EventArgs e)
		{
			this.listView.SelectedItems[0].BeginEdit();
		}

		private void menuItem_ListItem_Edit_Click(object sender, System.EventArgs e)
		{
			this.toolBar_ButtonClick(this.toolBar, new ToolBarButtonClickEventArgs(this.toolBar.Buttons[4]));		
		}

		private void menuItem_ListItem_Properties_Click(object sender, System.EventArgs e)
		{
		
		}
	}

	/// <summary>
	/// 
	/// </summary>
	public class ImageOpenEventArgs : EventArgs
	{

		/// <summary>
		/// 
		/// </summary>
		LibraryListItem item;

		/// <summary>
		/// 
		/// </summary>
		/// <param name="item"></param>
		public ImageOpenEventArgs(LibraryListItem item)
		{
			this.item = item;
		}

		/// <summary>
		/// 
		/// </summary>
		public LibraryListItem Item
		{
			get
			{
				return item;
			}
		}
	}

	/// <summary>
	/// Library ListItem extends ListViewItem to store a LibraryItem and retrive data from it
	/// </summary>
	public class LibraryListItem: ListViewItem
	{
		/// <summary>
		/// 
		/// </summary>
		LibraryItem libraryItem;

		Size size;

		double fileSize;

		/// <summary>
		/// Constructor
		/// </summary>
		/// <param name="item"></param>
		public LibraryListItem(LibraryItem item)
		{
			this.libraryItem = item;
			this.Text        = this.libraryItem.Name;
			this.ImageIndex  = -1;
			this.size        = Size.Empty;
			this.fileSize    = 0;
		}

		/// <summary>
		/// 
		/// </summary>
		public LibraryItem Item
		{
			get
			{
				return this.libraryItem;
			}
		}

		/// <summary>
		/// 
		/// </summary>
		public Size Size
		{
			get
			{
				return this.size;
			}
			set
			{
				this.size = value;
			}
		}
		
		/// <summary>
		/// 
		/// </summary>
		public double FileSize
		{
			get
			{
				return this.fileSize;
			}
			set
			{
				this.fileSize = value;
			}
		}
	}
}
