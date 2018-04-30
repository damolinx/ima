using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Windows.Forms;

//TODO Evaluate adding a level between GenericComponent & Library/Image Canvas to add Zoom control on both
namespace Ima
{

	/// <summary>
	/// 
	/// </summary>
	public delegate bool CloseImageDelegate ();

	/// <summary>
	/// Summary description for ImageComponent.
	/// </summary>
	public class ImageComponent : Ima.GenericComponent
	{
		/// <summary>
		/// 
		/// </summary>
		private struct FilterStruct
		{
			public int index;
			public FilterBase filter;

			/// <summary>
			/// 
			/// </summary>
			/// <param name="i"></param>
			/// <param name="f"></param>
			public FilterStruct(int i, FilterBase f)
			{
				index  = i;
				filter = f;
			}
		}

		/// <summary>
		/// Stores last operation name for current image
		/// //TODO Analize if this can be added to undo manager
		/// </summary>
		private string LastOpName;

		private System.Windows.Forms.ToolBarButton tbBtn_RotateFlip;
		private System.Windows.Forms.ToolBarButton tbBtn_SizeStretch;
		private System.Windows.Forms.ToolBarButton tbBtn_Separator_1;
		private Ima.Controls.ImageControl imageControl;
		private System.Windows.Forms.ToolBarButton tbBtn_Color_Filter;
		private System.Windows.Forms.ContextMenu cmenColorFilters;
		private System.Windows.Forms.ToolBarButton tbBtn_CloseImage;
		private System.Windows.Forms.ToolBarButton tbBtn_Separator_2;
		private System.Windows.Forms.ToolBarButton tbBtn_Separator_3;


		/// <summary>
		/// 
		/// </summary>
		private ImageWrapper wrapper;

		/// <summary>
		/// reference to the proper IStatusNotification object
		/// </summary>
		protected IStatusNotification status;

		/// <summary>
		/// 
		/// </summary>
		bool autoFit = true;

		/// <summary>
		/// 
		/// </summary>
		int currentColorFilter;

		/// <summary>
		/// 
		/// </summary>
		FilterStruct[] colorFilters = new FilterStruct[] {
															 new FilterStruct(10, new FilterBlackWhite()),
															 new FilterStruct(5, new FilterInvert()),
															 new FilterStruct(9, new FilterRed()),
															 new FilterStruct(8, new FilterGreen()),
															 new FilterStruct(7, new FilterBlue()), 
															 new FilterStruct(6, new FilterSepia()),
															 new FilterStruct(4, new FilterGrayAdjusted())};
		private System.Windows.Forms.ToolBarButton tbBtn_AutoFit;
		private System.Windows.Forms.ToolBarButton tbBtn_Selection;

		/// <summary>
		/// 
		/// </summary>
		private CloseImageDelegate closeDelegate;

		/// <summary>
		/// 
		/// </summary>
		/// <param name="filename"></param>
		/// <param name="bitmap"></param>
		public ImageComponent() : this(null)
		{
		}

		/// <summary>
		/// 
		/// </summary>
		public ImageComponent(ImageWrapper wrapper)
		{
			// This call is required by the Windows.Forms Form Designer.
			InitializeComponent();


			this.ImageWrapper = wrapper;
			this.LastOpName   = string.Empty;

			//Menu
			this.setCurrentColorFilter(this.colorFilters.Length - 1);
			for(int i = 0; i < this.colorFilters.Length; i++)
			{
				this.cmenColorFilters.MenuItems.Add(i, new ImageComponent.ColorFilterMenuItem(this, this.colorFilters[i]));
			}
		}

		#region Component Designer generated code
		/// <summary> 
		/// Required method for Designer support - do not modify 
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ImageComponent));
            this.tbBtn_RotateFlip = new System.Windows.Forms.ToolBarButton();
            this.tbBtn_SizeStretch = new System.Windows.Forms.ToolBarButton();
            this.tbBtn_Separator_1 = new System.Windows.Forms.ToolBarButton();
            this.tbBtn_Selection = new System.Windows.Forms.ToolBarButton();
            this.imageControl = new Ima.Controls.ImageControl();
            this.tbBtn_Color_Filter = new System.Windows.Forms.ToolBarButton();
            this.cmenColorFilters = new System.Windows.Forms.ContextMenu();
            this.tbBtn_CloseImage = new System.Windows.Forms.ToolBarButton();
            this.tbBtn_Separator_2 = new System.Windows.Forms.ToolBarButton();
            this.tbBtn_AutoFit = new System.Windows.Forms.ToolBarButton();
            this.tbBtn_Separator_3 = new System.Windows.Forms.ToolBarButton();
            this.panel.SuspendLayout();
            this.bottomPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel
            // 
            this.panel.AutoScroll = true;
            this.panel.BackColor = System.Drawing.SystemColors.AppWorkspace;
            this.panel.Controls.Add(this.imageControl);
            this.panel.Size = new System.Drawing.Size(528, 350);
            this.panel.SizeChanged += new System.EventHandler(this.panel_SizeChanged);
            // 
            // imageList
            // 
            this.imageList.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList.ImageStream")));
            this.imageList.TransparentColor = System.Drawing.Color.White;
            this.imageList.Images.SetKeyName(0, "");
            this.imageList.Images.SetKeyName(1, "");
            this.imageList.Images.SetKeyName(2, "");
            this.imageList.Images.SetKeyName(3, "");
            this.imageList.Images.SetKeyName(4, "");
            this.imageList.Images.SetKeyName(5, "");
            this.imageList.Images.SetKeyName(6, "");
            this.imageList.Images.SetKeyName(7, "");
            this.imageList.Images.SetKeyName(8, "");
            this.imageList.Images.SetKeyName(9, "");
            this.imageList.Images.SetKeyName(10, "");
            this.imageList.Images.SetKeyName(11, "");
            // 
            // toolBar
            // 
            this.toolBar.Buttons.AddRange(new System.Windows.Forms.ToolBarButton[] {
            this.tbBtn_CloseImage,
            this.tbBtn_Separator_1,
            this.tbBtn_AutoFit,
            this.tbBtn_Selection,
            this.tbBtn_Separator_2,
            this.tbBtn_RotateFlip,
            this.tbBtn_SizeStretch,
            this.tbBtn_Separator_3,
            this.tbBtn_Color_Filter});
            this.toolBar.Size = new System.Drawing.Size(528, 64);
            this.toolBar.ButtonClick += new System.Windows.Forms.ToolBarButtonClickEventHandler(this.toolBar_ButtonClick);
            // 
            // bottomPanel
            // 
            this.bottomPanel.Location = new System.Drawing.Point(0, 350);
            this.bottomPanel.Size = new System.Drawing.Size(528, 58);
            // 
            // tbBtn_RotateFlip
            // 
            this.tbBtn_RotateFlip.ImageIndex = 0;
            this.tbBtn_RotateFlip.Name = "tbBtn_RotateFlip";
            this.tbBtn_RotateFlip.Text = "&Rotate/Flip";
            this.tbBtn_RotateFlip.ToolTipText = "Opens the Rotate/Flip tool";
            // 
            // tbBtn_SizeStretch
            // 
            this.tbBtn_SizeStretch.ImageIndex = 1;
            this.tbBtn_SizeStretch.Name = "tbBtn_SizeStretch";
            this.tbBtn_SizeStretch.Text = "&Resize/Stretch";
            this.tbBtn_SizeStretch.ToolTipText = "Opens the Resize/Stretch tool";
            // 
            // tbBtn_Separator_1
            // 
            this.tbBtn_Separator_1.Name = "tbBtn_Separator_1";
            this.tbBtn_Separator_1.Style = System.Windows.Forms.ToolBarButtonStyle.Separator;
            // 
            // tbBtn_Selection
            // 
            this.tbBtn_Selection.ImageIndex = 2;
            this.tbBtn_Selection.Name = "tbBtn_Selection";
            this.tbBtn_Selection.Text = "&Select";
            this.tbBtn_Selection.ToolTipText = "Select an area of the picture to work with";
            // 
            // imageControl
            // 
            this.imageControl.BackColor = System.Drawing.SystemColors.AppWorkspace;
            this.imageControl.Image = null;
            this.imageControl.Location = new System.Drawing.Point(0, 0);
            this.imageControl.Name = "imageControl";
            this.imageControl.PaintMode = Ima.Controls.ImageControl.PaintStyle.PAINT_HIGHQUALITY;
            this.imageControl.SelectionActive = false;
            this.imageControl.Size = new System.Drawing.Size(368, 288);
            this.imageControl.TabIndex = 0;
            this.imageControl.Zoom = 1;
            this.imageControl.SelectionChanged += new Ima.Controls.SelectionChangedEventHandler(this.imageControl_SelectionChanged);
            // 
            // tbBtn_Color_Filter
            // 
            this.tbBtn_Color_Filter.DropDownMenu = this.cmenColorFilters;
            this.tbBtn_Color_Filter.ImageIndex = 4;
            this.tbBtn_Color_Filter.Name = "tbBtn_Color_Filter";
            this.tbBtn_Color_Filter.Style = System.Windows.Forms.ToolBarButtonStyle.DropDownButton;
            this.tbBtn_Color_Filter.Text = "&B/W";
            // 
            // tbBtn_CloseImage
            // 
            this.tbBtn_CloseImage.ImageIndex = 3;
            this.tbBtn_CloseImage.Name = "tbBtn_CloseImage";
            this.tbBtn_CloseImage.Text = "&Close";
            this.tbBtn_CloseImage.ToolTipText = "Close current image";
            // 
            // tbBtn_Separator_2
            // 
            this.tbBtn_Separator_2.Name = "tbBtn_Separator_2";
            this.tbBtn_Separator_2.Style = System.Windows.Forms.ToolBarButtonStyle.Separator;
            // 
            // tbBtn_AutoFit
            // 
            this.tbBtn_AutoFit.ImageIndex = 11;
            this.tbBtn_AutoFit.Name = "tbBtn_AutoFit";
            this.tbBtn_AutoFit.Pushed = true;
            this.tbBtn_AutoFit.Style = System.Windows.Forms.ToolBarButtonStyle.ToggleButton;
            this.tbBtn_AutoFit.Text = "&Fit Image";
            this.tbBtn_AutoFit.ToolTipText = "Auto-adjust zoom level so image always fits in screen";
            // 
            // tbBtn_Separator_3
            // 
            this.tbBtn_Separator_3.Name = "tbBtn_Separator_3";
            this.tbBtn_Separator_3.Style = System.Windows.Forms.ToolBarButtonStyle.Separator;
            // 
            // ImageComponent
            // 
            this.Name = "ImageComponent";
            this.Size = new System.Drawing.Size(528, 408);
            this.panel.ResumeLayout(false);
            this.bottomPanel.ResumeLayout(false);
            this.bottomPanel.PerformLayout();
            this.ResumeLayout(false);

		}
		#endregion

		/// <summary>
		/// Auto Fit
		/// </summary>
		public bool AutoFit
		{
			set
			{
				this.autoFit = value;
				if (value)
				{
					if (this.status != null)
					{
						this.status.ZoomEnable = false;
					}
					//this.imageControl.Zoom = Math.Min(1.0, Math.Max(0.01, 
					//	Math.Floor(100.0 * Math.Min(((double)this.panel.Size.Width) / this.imageControl.Image.Width, 
					//	((double)this.panel.Size.Height) / this.imageControl.Image.Height)) / 100.0));
				}
				else
				{
					if (this.status != null)
					{
						//TODO 0 -> to 1, otherise ValueChange is not risen
						this.status.ZoomValue  = 1;
						this.status.ZoomEnable = true;
						this.status.ZoomValue  = 0;
					}
					else
					{
						this.imageControl.Zoom = 1;
					}
				}
			}
			get
			{
				return this.autoFit;
			}
		}

		/// <summary>
		/// Notification
		/// </summary>
		public IStatusNotification StatusNotification
		{
			get
			{
				return this.status;
			}
			set
			{
				if (value == null)
				{
					if (this.status != null)
					{
						this.status.RemoveZoomEvent(new System.EventHandler(this.zoom_ValueChanged));
						this.status = null;
					}
				}
				else
				{
					this.status = value;
					this.status.ZoomEnable = !this.AutoFit;
					this.status.ZoomMin    = -9;
					this.status.ZoomMax    =  9;
					this.status.AddZoomEvent(new System.EventHandler(this.zoom_ValueChanged));
					this.status.ZoomValue  = this.status.ZoomValue;
				}
			}
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="i"></param>
		private void setCurrentColorFilter(int i)
		{
			FilterStruct f = this.colorFilters[i];
			this.tbBtn_Color_Filter.ImageIndex  = f.index;
			this.tbBtn_Color_Filter.Text        = f.filter.Name;
			this.tbBtn_Color_Filter.ToolTipText = f.filter.Description;
			this.currentColorFilter = i;
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
					if (this.closeDelegate != null && this.closeDelegate())
					{
						this.imageControl.Image = null;
						this.ImageWrapper = null;
					}
					break;
				
				case 2:
					this.AutoFit = !this.autoFit;
					break;
				
				case 3:
					this.imageControl.SelectionActive = !this.imageControl.SelectionActive;
					e.Button.Pushed = !e.Button.Pushed; 
					break;

				case 5:
					FlipRotateWindow flipRotateDialog = new FlipRotateWindow(this.ImageWrapper);
					flipRotateDialog.Show();
					break;
				case 6:
					StretchResizeWindow stretchResizeDialog = new StretchResizeWindow(this.ImageWrapper);
					stretchResizeDialog.Show();
					break;
				case 8:
					BasicFilterWindow window = new BasicFilterWindow(this.ImageWrapper, this.colorFilters[this.currentColorFilter].filter);
					window.ShowDialog(this);
					break;
			}
		}

		#region Properties
		/// <summary>
		/// 
		/// </summary>
		public string LastOperation
		{
			get
			{
				return this.LastOpName;
			}
			set
			{
				this.LastOpName = value;
			}
		}

		/// <summary>
		/// 
		/// </summary>
		public ImageWrapper ImageWrapper
		{
			get
			{
				return this.wrapper;
			}
			set
			{
				if (this.wrapper != null)
				{
					this.ImageWrapper.Changed-=new ImageChangedEventHandler(ImageWrapper_Changed);
				}
				this.wrapper = value;
				if (value != null)
				{
					this.imageControl.Image = value.Bitmap;
					this.ImageWrapper.Changed+=new ImageChangedEventHandler(ImageWrapper_Changed);
					
					//Scale for view
					this.AutoFit = this.autoFit;

					//Remove Selection
					this.imageControl.SelectionActive = false;
				}
				else
				{
					this.imageControl.Image = null;
				}
			}
		}

		/// <summary>
		/// Check if there is an image
		/// </summary>
		public bool Empty
		{
			get
			{
				return this.wrapper == null;
			}
		}

		/// <summary>
		/// Close delegate
		/// </summary>
		public CloseImageDelegate CloseDelegate
		{
			get
			{
				return this.closeDelegate;
			}
			set
			{
				this.closeDelegate = value;
			}
		}

		#endregion

		/// <summary>
		/// 
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void ImageWrapper_Changed(object sender, ImageChangedEventArgs e)
		{
			this.imageControl.Image = this.ImageWrapper.Bitmap;
			this.imageControl.Invalidate();
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void panel_SizeChanged(object sender, System.EventArgs e)
		{
			if(this.ImageWrapper != null)
				this.AutoFit = this.autoFit;
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void zoom_ValueChanged(object sender, System.EventArgs e)
		{
			if (this.status.ZoomValue < 0)
			{
				this.imageControl.Zoom = 1.0 + (this.status.ZoomValue / 10.0);
				this.status.ZoomMsg = "Zoom out by " + this.imageControl.Zoom.ToString("G2") + "x";
			}
			else if (this.status.ZoomValue == 0)
			{
				this.imageControl.Zoom = 1.0;
				this.status.ZoomMsg    = "Actual Size";
			}
			else
			{
				this.imageControl.Zoom = this.status.ZoomValue + 1;
				this.status.ZoomMsg    = "Zoom in by " + this.imageControl.Zoom.ToString("G1") + "x";
			}		
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="rec"></param>
		private void imageControl_SelectionChanged(object sender, System.Drawing.Rectangle rec)
		{
			if (this.wrapper != null)
			{
				this.wrapper.ActiveRegion = rec;
			}
		}


		/// <summary>
		/// Helper class to support Multivalue Button
		/// </summary>
		private class ColorFilterMenuItem : MenuItem
		{
			/// <summary>
			/// Filter
			/// </summary>
			FilterStruct filterStruct;

			/// <summary>
			/// Instance Reference
			/// </summary>
			ImageComponent imageComponent;
			
			/// <summary>
			/// Constructor
			/// </summary>
			/// <param name="f"></param>
			public ColorFilterMenuItem(ImageComponent component, FilterStruct f)
			{
				this.filterStruct   = f;
				this.imageComponent = component;
				this.Text           = f.filter.Name;
				this.Click         += new EventHandler(ColorFilterMenuItem_Click);
			}

			/// <summary>
			/// 
			/// </summary>
			/// <param name="sender"></param>
			/// <param name="e"></param>
			private void ColorFilterMenuItem_Click(object sender, EventArgs e)
			{
				this.imageComponent.setCurrentColorFilter(this.Index); //should be the same

				if (this.filterStruct.filter is ThresholdFilterBase)
				{
					ThresholdWindow window = new ThresholdWindow(this.imageComponent.ImageWrapper, (ThresholdFilterBase)this.filterStruct.filter);
					window.ShowDialog(this.imageComponent);
				}	
				else
				{
					BasicFilterWindow window = new BasicFilterWindow(this.imageComponent.ImageWrapper, this.filterStruct.filter);
					window.ShowDialog(this.imageComponent);
				}
			}
		}

	}
}
