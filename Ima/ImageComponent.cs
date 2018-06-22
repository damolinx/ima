using Ima.ImageOps;
using Ima.ImageOps.Filters;
using System;
using System.Windows.Forms;

//TODO Evaluate adding a level between GenericComponent & Library/Image Canvas to add Zoom control on both
namespace Ima
{

    /// <summary>
    /// 
    /// </summary>
    public delegate bool CloseImageDelegate();

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
                index = i;
                filter = f;
            }
        }

        private System.Windows.Forms.ToolBarButton tbBtn_RotateFlip;
        private System.Windows.Forms.ToolBarButton tbBtn_SizeStretch;
        private System.Windows.Forms.ToolBarButton tbBtn_Separator_1;
        private Ima.Controls.ImageControl imageControl;
        private System.Windows.Forms.ToolBarButton tbBtn_Color_Filter;
        private System.Windows.Forms.ContextMenu contextMenuColorFilters;
        private System.Windows.Forms.ToolBarButton tbBtn_CloseImage;
        private System.Windows.Forms.ToolBarButton tbBtn_Separator_2;
        private System.Windows.Forms.ToolBarButton tbBtn_Separator_3;


        private ImageWrapper wrapper;
        protected IStatusNotification status;
        private bool autoFit;
        private int currentColorFilter;

        private FilterStruct[] colorFilters = new[]
        {
            new FilterStruct(10, new FilterBlackWhite()),
            new FilterStruct(5, new FilterInvert()),
            new FilterStruct(9, new FilterRed()),
            new FilterStruct(8, new FilterGreen()),
            new FilterStruct(7, new FilterBlue()),
            new FilterStruct(6, new FilterSepia()),
            new FilterStruct(4, new FilterGrayAdjusted())
        };

        private System.Windows.Forms.ToolBarButton tbBtn_AutoFit;
        private System.Windows.Forms.ToolBarButton tbBtn_Selection;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="filename"></param>
        /// <param name="bitmap"></param>
        public ImageComponent()
            : this(null)
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
            this.LastOperation = string.Empty;

            //Menu
            this.SetCurrentColorFilter(this.colorFilters.Length - 1);
            for (int i = 0; i < this.colorFilters.Length; i++)
            {
                this.contextMenuColorFilters.MenuItems.Add(i, new ImageComponent.ColorFilterMenuItem(this, this.colorFilters[i]));
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
            this.contextMenuColorFilters = new System.Windows.Forms.ContextMenu();
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
            this.panel.SizeChanged += this.panel_SizeChanged;
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
            this.toolBar.ButtonClick += this.toolBar_ButtonClick;
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
            this.imageControl.SelectionChanged += this.imageControl_SelectionChanged;
            // 
            // tbBtn_Color_Filter
            // 
            this.tbBtn_Color_Filter.DropDownMenu = this.contextMenuColorFilters;
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
                if (this.autoFit != value)
                {
                    this.autoFit = value;
                    if (value)
                    {
                        if (this.status != null)
                        {
                            this.status.ZoomEnable = false;
                        }

                        ForceAutoFit();
                    }
                    else
                    {
                        if (this.status != null)
                        {
                            //TODO 0 -> to 1, otherwise ValueChange is not raised
                            this.status.ZoomValue = 1;
                            this.status.ZoomEnable = true;
                            this.status.ZoomValue = 0;
                        }
                        else
                        {
                            this.imageControl.Zoom = 1;
                        }
                    }
                }
            }
            get
            {
                return this.autoFit;
            }
        }

        public void ForceAutoFit()
        {
            this.imageControl.Zoom = Math.Min(1.0, Math.Max(0.01,
                Math.Floor(100.0 * Math.Min(((double)this.panel.Size.Width) / this.imageControl.Image?.Width ?? 1,
                ((double)this.panel.Size.Height) / this.imageControl.Image?.Height ?? 1)) / 100.0));
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
                        this.status.RemoveZoomEvent(this.zoom_ValueChanged);
                        this.status = null;
                    }
                }
                else
                {
                    this.status = value;
                    this.status.ZoomEnable = !this.AutoFit;
                    this.status.ZoomMin = -9;
                    this.status.ZoomMax = 9;
                    this.status.AddZoomEvent(this.zoom_ValueChanged);
                    this.status.ZoomValue = this.status.ZoomValue;
                }
            }
        }

        private void SetCurrentColorFilter(int i)
        {
            FilterStruct f = this.colorFilters[i];
            this.tbBtn_Color_Filter.ImageIndex = f.index;
            this.tbBtn_Color_Filter.Text = f.filter.Name;
            this.tbBtn_Color_Filter.ToolTipText = f.filter.Description;
            this.currentColorFilter = i;
        }

        private void toolBar_ButtonClick(object sender, ToolBarButtonClickEventArgs e)
        {
            switch (this.toolBar.Buttons.IndexOf(e.Button))
            {
                case 0:
                    if (this.CloseDelegate != null && this.CloseDelegate())
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
                    var flipRotateDialog = new FlipRotateWindow(this.ImageWrapper);
                    flipRotateDialog.Show();
                    break;
                case 6:
                    var stretchResizeDialog = new StretchResizeWindow(this.ImageWrapper);
                    stretchResizeDialog.Show();
                    break;
                case 8:
                    var window = new BasicFilterWindow(this.ImageWrapper, this.colorFilters[this.currentColorFilter].filter);
                    window.ShowDialog(this);
                    break;
            }
        }

        #region Properties

        public string LastOperation
        {
            get; set;
        }

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
                    this.ImageWrapper.Changed -= ImageWrapper_Changed;
                }
                this.wrapper = value;
                if (value != null)
                {
                    this.imageControl.Image = value.Bitmap;
                    this.ImageWrapper.Changed += ImageWrapper_Changed;

                    //Scale for view
                    if (this.AutoFit)
                    {
                        ForceAutoFit();
                    }

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
            get; set;
        }

        #endregion

        private void ImageWrapper_Changed(object sender, ImageChangedEventArgs e)
        {
            this.imageControl.Image = this.ImageWrapper.Bitmap;
            this.imageControl.Invalidate();
        }

        private void panel_SizeChanged(object sender, System.EventArgs e)
        {
            if (this.ImageWrapper != null)
            {
                if (this.AutoFit)
                {
                    ForceAutoFit();
                }
            }
        }

        private void zoom_ValueChanged(object sender, System.EventArgs e)
        {
            if (this.status.ZoomValue < 0)
            {
                this.imageControl.Zoom = 1.0 + (this.status.ZoomValue / 10.0);
                this.status.ZoomMsg = this.imageControl.Zoom.ToString("F2") + "x";
            }
            else if (this.status.ZoomValue == 0)
            {
                this.imageControl.Zoom = 1.0;
                this.status.ZoomMsg = "Actual Size";
            }
            else
            {
                this.imageControl.Zoom = 1.0 + this.status.ZoomValue;
                this.status.ZoomMsg = this.imageControl.Zoom.ToString("F1") + "x";
            }
        }

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
            public ColorFilterMenuItem(ImageComponent component, FilterStruct f)
            {
                this.filterStruct = f;
                this.imageComponent = component;
                this.Text = f.filter.Name;
                this.Click += ColorFilterMenuItem_Click;
            }

            private void ColorFilterMenuItem_Click(object sender, EventArgs e)
            {
                this.imageComponent.SetCurrentColorFilter(this.Index); //should be the same

                var form = (this.filterStruct.filter is ThresholdFilterBase filter)
                    ? (Form)new ThresholdWindow(this.imageComponent.ImageWrapper, filter)
                    : new BasicFilterWindow(this.imageComponent.ImageWrapper, this.filterStruct.filter);

                form.ShowDialog(this.imageComponent);
            }
        }
    }
}
