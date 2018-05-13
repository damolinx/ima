using System;
using System.IO;
using System.Drawing;
using System.Drawing.Imaging;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Data;

using Ima.Library;
using Ima.Controls;

namespace Ima
{
	/// <summary>
	/// View Mode
	/// </summary>
	public enum CurrentView
	{
		VIEW_LIBRARY,
		VIEW_EDITOR,
		VIEW_HYBRID
	};
	/// <summary>
	/// Summary description for MainForm.
	/// </summary>
	public class MainForm : Ima.Controls.BaseForm
	{
		#region Constants

		/// <summary>
		/// 
		/// </summary>
		private const string ImageFileFilter = "Bitmap (*.bmp)|*.bmp" +
			"|Enhanced Windows Metafile (*.emf)|*.emf" +
			"|Exchangeable Image Format (*.exif)|*.exif" +
			"|Graphics Interchange Format (*.gif)|*.gif" +
			"|Icon Format|*.ico" +
			"|Joint Photographic Experts Group (*.jpg,*.jpeg)|*.jpg;*.jpeg" +
			"|Portable Network Graphics (*.png)|*.png" +
			"|Tag Image File Format (*.tiff)|*.tiff;*.tif" +
			"|Windows metafile (*.wmf)|*.wmf";

		/// <summary>
		/// 
		/// </summary>
		private const string ImageAllFileFilter = "All Image Files| *.bmp;*.emf;*.exif;*.gif;*.ico;*.jpg;*.jpeg;*.png;*.tif;*.tiff;*.wmf|All Files|*.*|" + ImageFileFilter;

        #endregion

        private IContainer components;
        private System.Windows.Forms.MainMenu mainMenu;
		private System.Windows.Forms.MenuItem menuItem_File;
		private System.Windows.Forms.MenuItem menuItem_Edit;
		private System.Windows.Forms.MenuItem menuItem_View;
		private System.Windows.Forms.MenuItem menuItem_Image;
		private System.Windows.Forms.MenuItem menuItem_Help;
		private System.Windows.Forms.MenuItem menuItem_Styles;
		private System.Windows.Forms.MenuItem menuItem_Effects;
		private System.Windows.Forms.MenuItem menuItem_Help_About;
		private System.Windows.Forms.MenuItem menuItem_File_Exit;
		private System.Windows.Forms.MenuItem menuItem_File_New;
		private System.Windows.Forms.MenuItem menuItem_File_Open;
		private System.Windows.Forms.MenuItem menuItem_File_Save;
		private System.Windows.Forms.MenuItem menuItem_File_SaveAs;
		private System.Windows.Forms.MenuItem menuItem_File_Separator_1;
		private System.Windows.Forms.MenuItem menuItem_File_ScannerCamera;
		private System.Windows.Forms.MenuItem menuItem_File_Separator_2;
		private System.Windows.Forms.MenuItem menuItem_File_Screenshot;
		private System.Windows.Forms.MenuItem menuItem_File_Close;
		private System.Windows.Forms.MenuItem menuItem_File_Separator_3;
		private System.Windows.Forms.MenuItem menuItem_Edit_Undo;
		private System.Windows.Forms.MenuItem menuItem_Edit_Redo;
		private System.Windows.Forms.MenuItem menuItem_Edit_DiscardAll;
		private System.Windows.Forms.MenuItem menuItem_Edit_Separator_1;
		private System.Windows.Forms.MenuItem menuItem_Edit_Cut;
		private System.Windows.Forms.MenuItem menuItem_Edit_Copy;
		private System.Windows.Forms.MenuItem menuItem_Edit_Paste;
		private System.Windows.Forms.MenuItem menuItem_Image_FlipRotate;
		private System.Windows.Forms.MenuItem menuItem_Image_StretchResize;
		private System.Windows.Forms.MenuItem menuItem_Image_Separator_1;
		private System.Windows.Forms.MenuItem menuItem_Image_Brightness;
		private System.Windows.Forms.MenuItem menuItem_Image_Contrast;
		private System.Windows.Forms.MenuItem menuItem_Image_Separator_2;
		private System.Windows.Forms.MenuItem menuItem_Image_Gamma;
		private System.Windows.Forms.MenuItem menuItem_Styles_OilPainting;
		private System.Windows.Forms.MenuItem menuItem_Styles_Pencil;
		private System.Windows.Forms.MenuItem menuItem_Styles_ColoredPencils;
		private System.Windows.Forms.MenuItem menuItem_Styles_FrostedGlass;
		private System.Windows.Forms.MenuItem menuItem_Colors_Invert;
		private System.Windows.Forms.MenuItem menuItem_Colors_Grayscale;
		private System.Windows.Forms.MenuItem menuItem_Colors_BlackWhite;
		private System.Windows.Forms.MenuItem menuItem_Colors_Separator_1;
		private System.Windows.Forms.MenuItem menuItem_Colors_Separator_2;
		private System.Windows.Forms.MenuItem menuItem_Effects_Sharpen;
		private System.Windows.Forms.MenuItem menuItem_Effects_Blur;
		private System.Windows.Forms.MenuItem menuItem_Effects_Emboss;
		private System.Windows.Forms.MenuItem menuItem_Effects_EdgeEnhance;
		private System.Windows.Forms.MenuItem menuItem_Effects_EdgeDetect;
		private System.Windows.Forms.MenuItem menuItemEffects_ED_Kirsh;
		private System.Windows.Forms.MenuItem menuItem_Effects_ED_Prewitt;
		private System.Windows.Forms.MenuItem menuItem_Effects_ED_Sobel;
		private System.Windows.Forms.MenuItem menuItem_Effects_ED_BW;
		private System.Windows.Forms.MenuItem menuItem_Effects_Separator_1;
		private System.Windows.Forms.MenuItem menuItem_Effects_Dilate;
		private System.Windows.Forms.MenuItem menuItem_Effects_Median;
		private System.Windows.Forms.MenuItem menuItem_Effects_Erosion;
		private System.Windows.Forms.MenuItem menuItem_Effects_Opening;
		private System.Windows.Forms.MenuItem menuItem_Effects_Closing;
		private System.Windows.Forms.MenuItem menuItem_Effects_Posterize;
		private System.Windows.Forms.MenuItem menuItem_Effects_Solarize;
		private System.Windows.Forms.MenuItem menuItem_Effects_Separator_3;
		private System.Windows.Forms.MenuItem menuItem_View_Thumbnail;
		private System.Windows.Forms.MenuItem menuItem_Colors;
		private System.Windows.Forms.MenuItem menuItem_Colors_Redscale;
		private System.Windows.Forms.MenuItem menuItem_Colors_Bluescale;
		private System.Windows.Forms.MenuItem menuItem_Colors_Greenscale;
		private System.Windows.Forms.MenuItem menuItem_Effects_Separator_2;
		private System.Windows.Forms.MenuItem menuItem_Effects_Mean;
		private System.Windows.Forms.MenuItem menuItem_View_Menu;
		private System.Windows.Forms.MenuItem menuItem_View_Separator_1;
		private Ima.LibraryManagerComponent libraryPane;
		private System.Windows.Forms.Panel viewsPanel;
		private System.Windows.Forms.Panel switchPanel;
		private Ima.NotificationComponent notificationComponent;
		private Ima.LibraryComponent libraryComponent;
		private Ima.ImageComponent imageComponent;
		private System.Windows.Forms.Panel paneLeftSideBar;
		private System.Windows.Forms.Splitter splitterVertical;

		/// <summary>
		/// 
		/// </summary>
		CurrentView currentView = CurrentView.VIEW_LIBRARY;

		/// <summary>
		/// Undo/Redo Manager
		/// </summary>
		private UndoManager undoManager = new UndoManager(); //TODO Bind this to an Image Control ... or ... support general undo?

		#region Constructor & Destructor

		/// <summary>
		/// 
		/// </summary>
		public MainForm()
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();

			//Set Title/Name
			this.Name = Configuration.APPLICATION_NAME;
			this.Text = Configuration.APPLICATION_NAME;

			//Add relevant listeners
			this.libraryPane.OpenLibrary += new OpenLibraryItemEventHandler(libraryPane_OpenLibrary);
			this.libraryComponent.OpenImage += new ImageOpenEventHandler(libraryComponent_OpenImage);

			this.libraryComponent.StatusNotification = this.notificationComponent;

			this.imageComponent.CloseDelegate = new CloseImageDelegate(this.CloseImage);
			
			resetGUIState();
		}

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		protected override void Dispose( bool disposing )
		{
			if( disposing )
			{
				if (components != null) 
				{
					components.Dispose();
				}
			}
			base.Dispose( disposing );
		}
		#endregion

		#region Windows Form Designer generated code
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
            this.components = new System.ComponentModel.Container();
            this.mainMenu = new System.Windows.Forms.MainMenu(this.components);
            this.menuItem_File = new System.Windows.Forms.MenuItem();
            this.menuItem_File_New = new System.Windows.Forms.MenuItem();
            this.menuItem_File_Open = new System.Windows.Forms.MenuItem();
            this.menuItem_File_Close = new System.Windows.Forms.MenuItem();
            this.menuItem_File_Separator_1 = new System.Windows.Forms.MenuItem();
            this.menuItem_File_Save = new System.Windows.Forms.MenuItem();
            this.menuItem_File_SaveAs = new System.Windows.Forms.MenuItem();
            this.menuItem_File_Separator_2 = new System.Windows.Forms.MenuItem();
            this.menuItem_File_ScannerCamera = new System.Windows.Forms.MenuItem();
            this.menuItem_File_Screenshot = new System.Windows.Forms.MenuItem();
            this.menuItem_File_Separator_3 = new System.Windows.Forms.MenuItem();
            this.menuItem_File_Exit = new System.Windows.Forms.MenuItem();
            this.menuItem_Edit = new System.Windows.Forms.MenuItem();
            this.menuItem_Edit_Undo = new System.Windows.Forms.MenuItem();
            this.menuItem_Edit_Redo = new System.Windows.Forms.MenuItem();
            this.menuItem_Edit_DiscardAll = new System.Windows.Forms.MenuItem();
            this.menuItem_Edit_Separator_1 = new System.Windows.Forms.MenuItem();
            this.menuItem_Edit_Cut = new System.Windows.Forms.MenuItem();
            this.menuItem_Edit_Copy = new System.Windows.Forms.MenuItem();
            this.menuItem_Edit_Paste = new System.Windows.Forms.MenuItem();
            this.menuItem_View = new System.Windows.Forms.MenuItem();
            this.menuItem_View_Thumbnail = new System.Windows.Forms.MenuItem();
            this.menuItem_View_Separator_1 = new System.Windows.Forms.MenuItem();
            this.menuItem_View_Menu = new System.Windows.Forms.MenuItem();
            this.menuItem_Image = new System.Windows.Forms.MenuItem();
            this.menuItem_Image_FlipRotate = new System.Windows.Forms.MenuItem();
            this.menuItem_Image_StretchResize = new System.Windows.Forms.MenuItem();
            this.menuItem_Image_Separator_1 = new System.Windows.Forms.MenuItem();
            this.menuItem_Image_Brightness = new System.Windows.Forms.MenuItem();
            this.menuItem_Image_Contrast = new System.Windows.Forms.MenuItem();
            this.menuItem_Image_Gamma = new System.Windows.Forms.MenuItem();
            this.menuItem_Image_Separator_2 = new System.Windows.Forms.MenuItem();
            this.menuItem_Colors = new System.Windows.Forms.MenuItem();
            this.menuItem_Colors_BlackWhite = new System.Windows.Forms.MenuItem();
            this.menuItem_Colors_Invert = new System.Windows.Forms.MenuItem();
            this.menuItem_Colors_Separator_1 = new System.Windows.Forms.MenuItem();
            this.menuItem_Colors_Grayscale = new System.Windows.Forms.MenuItem();
            this.menuItem_Colors_Redscale = new System.Windows.Forms.MenuItem();
            this.menuItem_Colors_Greenscale = new System.Windows.Forms.MenuItem();
            this.menuItem_Colors_Bluescale = new System.Windows.Forms.MenuItem();
            this.menuItem_Colors_Separator_2 = new System.Windows.Forms.MenuItem();
            this.menuItem_Effects = new System.Windows.Forms.MenuItem();
            this.menuItem_Effects_Blur = new System.Windows.Forms.MenuItem();
            this.menuItem_Effects_Sharpen = new System.Windows.Forms.MenuItem();
            this.menuItem_Effects_Emboss = new System.Windows.Forms.MenuItem();
            this.menuItem_Effects_EdgeEnhance = new System.Windows.Forms.MenuItem();
            this.menuItem_Effects_EdgeDetect = new System.Windows.Forms.MenuItem();
            this.menuItem_Effects_ED_BW = new System.Windows.Forms.MenuItem();
            this.menuItemEffects_ED_Kirsh = new System.Windows.Forms.MenuItem();
            this.menuItem_Effects_ED_Prewitt = new System.Windows.Forms.MenuItem();
            this.menuItem_Effects_ED_Sobel = new System.Windows.Forms.MenuItem();
            this.menuItem_Effects_Separator_1 = new System.Windows.Forms.MenuItem();
            this.menuItem_Effects_Dilate = new System.Windows.Forms.MenuItem();
            this.menuItem_Effects_Median = new System.Windows.Forms.MenuItem();
            this.menuItem_Effects_Erosion = new System.Windows.Forms.MenuItem();
            this.menuItem_Effects_Opening = new System.Windows.Forms.MenuItem();
            this.menuItem_Effects_Closing = new System.Windows.Forms.MenuItem();
            this.menuItem_Effects_Separator_2 = new System.Windows.Forms.MenuItem();
            this.menuItem_Effects_Posterize = new System.Windows.Forms.MenuItem();
            this.menuItem_Effects_Solarize = new System.Windows.Forms.MenuItem();
            this.menuItem_Effects_Separator_3 = new System.Windows.Forms.MenuItem();
            this.menuItem_Effects_Mean = new System.Windows.Forms.MenuItem();
            this.menuItem_Styles = new System.Windows.Forms.MenuItem();
            this.menuItem_Styles_ColoredPencils = new System.Windows.Forms.MenuItem();
            this.menuItem_Styles_FrostedGlass = new System.Windows.Forms.MenuItem();
            this.menuItem_Styles_OilPainting = new System.Windows.Forms.MenuItem();
            this.menuItem_Styles_Pencil = new System.Windows.Forms.MenuItem();
            this.menuItem_Help = new System.Windows.Forms.MenuItem();
            this.menuItem_Help_About = new System.Windows.Forms.MenuItem();
            this.libraryPane = new Ima.LibraryManagerComponent();
            this.imageComponent = new Ima.ImageComponent();
            this.viewsPanel = new System.Windows.Forms.Panel();
            this.switchPanel = new System.Windows.Forms.Panel();
            this.libraryComponent = new Ima.LibraryComponent();
            this.notificationComponent = new Ima.NotificationComponent();
            this.splitterVertical = new System.Windows.Forms.Splitter();
            this.paneLeftSideBar = new System.Windows.Forms.Panel();
            this.viewsPanel.SuspendLayout();
            this.switchPanel.SuspendLayout();
            this.paneLeftSideBar.SuspendLayout();
            this.SuspendLayout();
            // 
            // mainMenu
            // 
            this.mainMenu.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.menuItem_File,
            this.menuItem_Edit,
            this.menuItem_View,
            this.menuItem_Image,
            this.menuItem_Colors,
            this.menuItem_Effects,
            this.menuItem_Styles,
            this.menuItem_Help});
            // 
            // menuItem_File
            // 
            this.menuItem_File.Index = 0;
            this.menuItem_File.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.menuItem_File_New,
            this.menuItem_File_Open,
            this.menuItem_File_Close,
            this.menuItem_File_Separator_1,
            this.menuItem_File_Save,
            this.menuItem_File_SaveAs,
            this.menuItem_File_Separator_2,
            this.menuItem_File_ScannerCamera,
            this.menuItem_File_Screenshot,
            this.menuItem_File_Separator_3,
            this.menuItem_File_Exit});
            this.menuItem_File.Text = "&File";
            // 
            // menuItem_File_New
            // 
            this.menuItem_File_New.Index = 0;
            this.menuItem_File_New.Shortcut = System.Windows.Forms.Shortcut.CtrlN;
            this.menuItem_File_New.Text = "&New...";
            this.menuItem_File_New.Click += new System.EventHandler(this.menuItem_File_New_Click);
            // 
            // menuItem_File_Open
            // 
            this.menuItem_File_Open.Index = 1;
            this.menuItem_File_Open.Shortcut = System.Windows.Forms.Shortcut.CtrlO;
            this.menuItem_File_Open.Text = "&Open...";
            this.menuItem_File_Open.Click += new System.EventHandler(this.menuItem_File_Open_Click);
            // 
            // menuItem_File_Close
            // 
            this.menuItem_File_Close.Index = 2;
            this.menuItem_File_Close.Text = "&Close";
            this.menuItem_File_Close.Click += new System.EventHandler(this.menuItem_File_Close_Click);
            // 
            // menuItem_File_Separator_1
            // 
            this.menuItem_File_Separator_1.Index = 3;
            this.menuItem_File_Separator_1.Text = "-";
            // 
            // menuItem_File_Save
            // 
            this.menuItem_File_Save.Index = 4;
            this.menuItem_File_Save.Shortcut = System.Windows.Forms.Shortcut.CtrlS;
            this.menuItem_File_Save.Text = "&Save";
            this.menuItem_File_Save.Click += new System.EventHandler(this.menuItem_File_Save_Click);
            // 
            // menuItem_File_SaveAs
            // 
            this.menuItem_File_SaveAs.Index = 5;
            this.menuItem_File_SaveAs.Shortcut = System.Windows.Forms.Shortcut.CtrlShiftS;
            this.menuItem_File_SaveAs.Text = "Save &As...";
            this.menuItem_File_SaveAs.Click += new System.EventHandler(this.menuItem_File_SaveAs_Click);
            // 
            // menuItem_File_Separator_2
            // 
            this.menuItem_File_Separator_2.Index = 6;
            this.menuItem_File_Separator_2.Text = "-";
            // 
            // menuItem_File_ScannerCamera
            // 
            this.menuItem_File_ScannerCamera.Index = 7;
            this.menuItem_File_ScannerCamera.Text = "From Scanner or Camera...";
            this.menuItem_File_ScannerCamera.Click += new System.EventHandler(this.menuItem_File_ScannerCamera_Click);
            // 
            // menuItem_File_Screenshot
            // 
            this.menuItem_File_Screenshot.Index = 8;
            this.menuItem_File_Screenshot.Text = "Screenshot...";
            this.menuItem_File_Screenshot.Click += new System.EventHandler(this.menuItem_File_Screenshot_Click);
            // 
            // menuItem_File_Separator_3
            // 
            this.menuItem_File_Separator_3.Index = 9;
            this.menuItem_File_Separator_3.Text = "-";
            // 
            // menuItem_File_Exit
            // 
            this.menuItem_File_Exit.Index = 10;
            this.menuItem_File_Exit.Shortcut = System.Windows.Forms.Shortcut.AltF4;
            this.menuItem_File_Exit.Text = "E&xit";
            this.menuItem_File_Exit.Click += new System.EventHandler(this.menuItem_File_Exit_Click);
            // 
            // menuItem_Edit
            // 
            this.menuItem_Edit.Index = 1;
            this.menuItem_Edit.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.menuItem_Edit_Undo,
            this.menuItem_Edit_Redo,
            this.menuItem_Edit_DiscardAll,
            this.menuItem_Edit_Separator_1,
            this.menuItem_Edit_Cut,
            this.menuItem_Edit_Copy,
            this.menuItem_Edit_Paste});
            this.menuItem_Edit.Text = "&Edit";
            this.menuItem_Edit.Popup += new System.EventHandler(this.menuItem_Edit_Popup);
            // 
            // menuItem_Edit_Undo
            // 
            this.menuItem_Edit_Undo.Index = 0;
            this.menuItem_Edit_Undo.Shortcut = System.Windows.Forms.Shortcut.CtrlZ;
            this.menuItem_Edit_Undo.Text = "&Undo";
            this.menuItem_Edit_Undo.Click += new System.EventHandler(this.menuItem_Edit_Undo_Click);
            // 
            // menuItem_Edit_Redo
            // 
            this.menuItem_Edit_Redo.Index = 1;
            this.menuItem_Edit_Redo.Shortcut = System.Windows.Forms.Shortcut.CtrlY;
            this.menuItem_Edit_Redo.Text = "&Redo";
            this.menuItem_Edit_Redo.Click += new System.EventHandler(this.menuItem_Edit_Redo_Click);
            // 
            // menuItem_Edit_DiscardAll
            // 
            this.menuItem_Edit_DiscardAll.Index = 2;
            this.menuItem_Edit_DiscardAll.Text = "Discard &All Changes";
            this.menuItem_Edit_DiscardAll.Click += new System.EventHandler(this.menuItem_Edit_DiscardAll_Click);
            // 
            // menuItem_Edit_Separator_1
            // 
            this.menuItem_Edit_Separator_1.Index = 3;
            this.menuItem_Edit_Separator_1.Text = "-";
            // 
            // menuItem_Edit_Cut
            // 
            this.menuItem_Edit_Cut.Index = 4;
            this.menuItem_Edit_Cut.Shortcut = System.Windows.Forms.Shortcut.CtrlX;
            this.menuItem_Edit_Cut.Text = "Cu&t";
            this.menuItem_Edit_Cut.Click += new System.EventHandler(this.menuItem_Edit_Cut_Click);
            // 
            // menuItem_Edit_Copy
            // 
            this.menuItem_Edit_Copy.Index = 5;
            this.menuItem_Edit_Copy.Shortcut = System.Windows.Forms.Shortcut.CtrlC;
            this.menuItem_Edit_Copy.Text = "&Copy";
            this.menuItem_Edit_Copy.Click += new System.EventHandler(this.menuItem_Edit_Copy_Click);
            // 
            // menuItem_Edit_Paste
            // 
            this.menuItem_Edit_Paste.Index = 6;
            this.menuItem_Edit_Paste.Shortcut = System.Windows.Forms.Shortcut.CtrlV;
            this.menuItem_Edit_Paste.Text = "&Paste";
            this.menuItem_Edit_Paste.Click += new System.EventHandler(this.menuItem_Edit_Paste_Click);
            // 
            // menuItem_View
            // 
            this.menuItem_View.Index = 2;
            this.menuItem_View.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.menuItem_View_Thumbnail,
            this.menuItem_View_Separator_1,
            this.menuItem_View_Menu});
            this.menuItem_View.Text = "&View";
            this.menuItem_View.Popup += new System.EventHandler(this.menuItem_View_Popup);
            // 
            // menuItem_View_Thumbnail
            // 
            this.menuItem_View_Thumbnail.Index = 0;
            this.menuItem_View_Thumbnail.Text = "&Thumbnail";
            this.menuItem_View_Thumbnail.Click += new System.EventHandler(this.menuItem_View_Thumbnail_Click);
            // 
            // menuItem_View_Separator_1
            // 
            this.menuItem_View_Separator_1.Index = 1;
            this.menuItem_View_Separator_1.Text = "-";
            // 
            // menuItem_View_Menu
            // 
            this.menuItem_View_Menu.Index = 2;
            this.menuItem_View_Menu.Shortcut = System.Windows.Forms.Shortcut.CtrlM;
            this.menuItem_View_Menu.Text = "&Menu";
            // 
            // menuItem_Image
            // 
            this.menuItem_Image.Index = 3;
            this.menuItem_Image.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.menuItem_Image_FlipRotate,
            this.menuItem_Image_StretchResize,
            this.menuItem_Image_Separator_1,
            this.menuItem_Image_Brightness,
            this.menuItem_Image_Contrast,
            this.menuItem_Image_Gamma,
            this.menuItem_Image_Separator_2});
            this.menuItem_Image.Text = "&Image";
            // 
            // menuItem_Image_FlipRotate
            // 
            this.menuItem_Image_FlipRotate.Index = 0;
            this.menuItem_Image_FlipRotate.Text = "&Flip or Rotate...";
            this.menuItem_Image_FlipRotate.Click += new System.EventHandler(this.menuItem_Image_FlipRotate_Click);
            // 
            // menuItem_Image_StretchResize
            // 
            this.menuItem_Image_StretchResize.Index = 1;
            this.menuItem_Image_StretchResize.Text = "&Stretch or Resize...";
            this.menuItem_Image_StretchResize.Click += new System.EventHandler(this.menuItem_Image_StretchResize_Click);
            // 
            // menuItem_Image_Separator_1
            // 
            this.menuItem_Image_Separator_1.Index = 2;
            this.menuItem_Image_Separator_1.Text = "-";
            // 
            // menuItem_Image_Brightness
            // 
            this.menuItem_Image_Brightness.Index = 3;
            this.menuItem_Image_Brightness.Text = "&Brightness...";
            this.menuItem_Image_Brightness.Click += new System.EventHandler(this.menuItem_Image_Brightness_Click);
            // 
            // menuItem_Image_Contrast
            // 
            this.menuItem_Image_Contrast.Index = 4;
            this.menuItem_Image_Contrast.Text = "Contrast...";
            this.menuItem_Image_Contrast.Click += new System.EventHandler(this.menuItem_Image_Contrast_Click);
            // 
            // menuItem_Image_Gamma
            // 
            this.menuItem_Image_Gamma.Index = 5;
            this.menuItem_Image_Gamma.Text = "Gamma Correction...";
            this.menuItem_Image_Gamma.Click += new System.EventHandler(this.menuItem_Image_Gamma_Click);
            // 
            // menuItem_Image_Separator_2
            // 
            this.menuItem_Image_Separator_2.Index = 6;
            this.menuItem_Image_Separator_2.Text = "-";
            // 
            // menuItem_Colors
            // 
            this.menuItem_Colors.Index = 4;
            this.menuItem_Colors.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.menuItem_Colors_BlackWhite,
            this.menuItem_Colors_Invert,
            this.menuItem_Colors_Separator_1,
            this.menuItem_Colors_Grayscale,
            this.menuItem_Colors_Redscale,
            this.menuItem_Colors_Greenscale,
            this.menuItem_Colors_Bluescale,
            this.menuItem_Colors_Separator_2});
            this.menuItem_Colors.Text = "&Colors";
            // 
            // menuItem_Colors_BlackWhite
            // 
            this.menuItem_Colors_BlackWhite.Index = 0;
            this.menuItem_Colors_BlackWhite.Text = "Black && &White...";
            this.menuItem_Colors_BlackWhite.Click += new System.EventHandler(this.menuItem_Colors_BlackWhite_Click);
            // 
            // menuItem_Colors_Invert
            // 
            this.menuItem_Colors_Invert.Index = 1;
            this.menuItem_Colors_Invert.Text = "&Invert";
            this.menuItem_Colors_Invert.Click += new System.EventHandler(this.menuItem_Colors_Invert_Click);
            // 
            // menuItem_Colors_Separator_1
            // 
            this.menuItem_Colors_Separator_1.Index = 2;
            this.menuItem_Colors_Separator_1.Text = "-";
            // 
            // menuItem_Colors_Grayscale
            // 
            this.menuItem_Colors_Grayscale.Index = 3;
            this.menuItem_Colors_Grayscale.Text = "&Grayscale";
            this.menuItem_Colors_Grayscale.Click += new System.EventHandler(this.menuItem_Colors_Grayscale_Click);
            // 
            // menuItem_Colors_Redscale
            // 
            this.menuItem_Colors_Redscale.Index = 4;
            this.menuItem_Colors_Redscale.Text = "&Redscale";
            this.menuItem_Colors_Redscale.Click += new System.EventHandler(this.menuItem_Colors_Redscale_Click);
            // 
            // menuItem_Colors_Greenscale
            // 
            this.menuItem_Colors_Greenscale.Index = 5;
            this.menuItem_Colors_Greenscale.Text = "G&reenscale";
            this.menuItem_Colors_Greenscale.Click += new System.EventHandler(this.menuItem_Colors_Greenscale_Click);
            // 
            // menuItem_Colors_Bluescale
            // 
            this.menuItem_Colors_Bluescale.Index = 6;
            this.menuItem_Colors_Bluescale.Text = "&Bluescale";
            this.menuItem_Colors_Bluescale.Click += new System.EventHandler(this.menuItem_Colors_Bluescale_Click);
            // 
            // menuItem_Colors_Separator_2
            // 
            this.menuItem_Colors_Separator_2.Index = 7;
            this.menuItem_Colors_Separator_2.Text = "-";
            // 
            // menuItem_Effects
            // 
            this.menuItem_Effects.Index = 5;
            this.menuItem_Effects.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.menuItem_Effects_Blur,
            this.menuItem_Effects_Sharpen,
            this.menuItem_Effects_Emboss,
            this.menuItem_Effects_EdgeEnhance,
            this.menuItem_Effects_EdgeDetect,
            this.menuItem_Effects_Separator_1,
            this.menuItem_Effects_Dilate,
            this.menuItem_Effects_Median,
            this.menuItem_Effects_Erosion,
            this.menuItem_Effects_Opening,
            this.menuItem_Effects_Closing,
            this.menuItem_Effects_Separator_2,
            this.menuItem_Effects_Posterize,
            this.menuItem_Effects_Solarize,
            this.menuItem_Effects_Separator_3,
            this.menuItem_Effects_Mean});
            this.menuItem_Effects.Text = "&Effects";
            // 
            // menuItem_Effects_Blur
            // 
            this.menuItem_Effects_Blur.Index = 0;
            this.menuItem_Effects_Blur.Text = "&Blur...";
            this.menuItem_Effects_Blur.Click += new System.EventHandler(this.menuItem_Effects_Blur_Click);
            // 
            // menuItem_Effects_Sharpen
            // 
            this.menuItem_Effects_Sharpen.Index = 1;
            this.menuItem_Effects_Sharpen.Text = "&Sharpen...";
            this.menuItem_Effects_Sharpen.Click += new System.EventHandler(this.menuItem_Effects_Sharpen_Click);
            // 
            // menuItem_Effects_Emboss
            // 
            this.menuItem_Effects_Emboss.Index = 2;
            this.menuItem_Effects_Emboss.Text = "Emb&oss...";
            this.menuItem_Effects_Emboss.Click += new System.EventHandler(this.menuItem_Effects_Emboss_Click);
            // 
            // menuItem_Effects_EdgeEnhance
            // 
            this.menuItem_Effects_EdgeEnhance.Index = 3;
            this.menuItem_Effects_EdgeEnhance.Text = "Edge En&hance...";
            this.menuItem_Effects_EdgeEnhance.Click += new System.EventHandler(this.menuItem_Effects_EdgeEnhance_Click);
            // 
            // menuItem_Effects_EdgeDetect
            // 
            this.menuItem_Effects_EdgeDetect.Index = 4;
            this.menuItem_Effects_EdgeDetect.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.menuItem_Effects_ED_BW,
            this.menuItemEffects_ED_Kirsh,
            this.menuItem_Effects_ED_Prewitt,
            this.menuItem_Effects_ED_Sobel});
            this.menuItem_Effects_EdgeDetect.Text = "&Edge Detect";
            // 
            // menuItem_Effects_ED_BW
            // 
            this.menuItem_Effects_ED_BW.Index = 0;
            this.menuItem_Effects_ED_BW.Text = "&Black && White";
            this.menuItem_Effects_ED_BW.Click += new System.EventHandler(this.menuItem_Effects_ED_BW_Click);
            // 
            // menuItemEffects_ED_Kirsh
            // 
            this.menuItemEffects_ED_Kirsh.Index = 1;
            this.menuItemEffects_ED_Kirsh.Text = "&Kirsh";
            this.menuItemEffects_ED_Kirsh.Click += new System.EventHandler(this.menuItemEffects_ED_Kirsh_Click);
            // 
            // menuItem_Effects_ED_Prewitt
            // 
            this.menuItem_Effects_ED_Prewitt.Index = 2;
            this.menuItem_Effects_ED_Prewitt.Text = "&Prewitt";
            this.menuItem_Effects_ED_Prewitt.Click += new System.EventHandler(this.menuItem_Effects_ED_Prewitt_Click);
            // 
            // menuItem_Effects_ED_Sobel
            // 
            this.menuItem_Effects_ED_Sobel.Index = 3;
            this.menuItem_Effects_ED_Sobel.Text = "&Sobel";
            this.menuItem_Effects_ED_Sobel.Click += new System.EventHandler(this.menuItem_Effects_ED_Sobel_Click);
            // 
            // menuItem_Effects_Separator_1
            // 
            this.menuItem_Effects_Separator_1.Index = 5;
            this.menuItem_Effects_Separator_1.Text = "-";
            // 
            // menuItem_Effects_Dilate
            // 
            this.menuItem_Effects_Dilate.Index = 6;
            this.menuItem_Effects_Dilate.Text = "D&ilate...";
            this.menuItem_Effects_Dilate.Click += new System.EventHandler(this.menuItem_Effects_Dilate_Click);
            // 
            // menuItem_Effects_Median
            // 
            this.menuItem_Effects_Median.Index = 7;
            this.menuItem_Effects_Median.Text = "&Median";
            this.menuItem_Effects_Median.Click += new System.EventHandler(this.menuItem_Effects_Median_Click);
            // 
            // menuItem_Effects_Erosion
            // 
            this.menuItem_Effects_Erosion.Index = 8;
            this.menuItem_Effects_Erosion.Text = "&Erosion";
            this.menuItem_Effects_Erosion.Click += new System.EventHandler(this.menuItem_Effects_Erosion_Click);
            // 
            // menuItem_Effects_Opening
            // 
            this.menuItem_Effects_Opening.Index = 9;
            this.menuItem_Effects_Opening.Text = "&Opening";
            this.menuItem_Effects_Opening.Click += new System.EventHandler(this.menuItem_Effects_Opening_Click);
            // 
            // menuItem_Effects_Closing
            // 
            this.menuItem_Effects_Closing.Index = 10;
            this.menuItem_Effects_Closing.Text = "&Closing";
            this.menuItem_Effects_Closing.Click += new System.EventHandler(this.menuItem_Effects_Closing_Click);
            // 
            // menuItem_Effects_Separator_2
            // 
            this.menuItem_Effects_Separator_2.Index = 11;
            this.menuItem_Effects_Separator_2.Text = "-";
            // 
            // menuItem_Effects_Posterize
            // 
            this.menuItem_Effects_Posterize.Index = 12;
            this.menuItem_Effects_Posterize.Text = "&Posterize...";
            this.menuItem_Effects_Posterize.Click += new System.EventHandler(this.menuItem_Effects_Posterize_Click);
            // 
            // menuItem_Effects_Solarize
            // 
            this.menuItem_Effects_Solarize.Index = 13;
            this.menuItem_Effects_Solarize.Text = "Solari&ze...";
            this.menuItem_Effects_Solarize.Click += new System.EventHandler(this.menuItem_Effects_Solarize_Click);
            // 
            // menuItem_Effects_Separator_3
            // 
            this.menuItem_Effects_Separator_3.Index = 14;
            this.menuItem_Effects_Separator_3.Text = "-";
            // 
            // menuItem_Effects_Mean
            // 
            this.menuItem_Effects_Mean.Index = 15;
            this.menuItem_Effects_Mean.Text = "Mea&n";
            this.menuItem_Effects_Mean.Click += new System.EventHandler(this.menuItem_Effects_Mean_Click);
            // 
            // menuItem_Styles
            // 
            this.menuItem_Styles.Index = 6;
            this.menuItem_Styles.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.menuItem_Styles_ColoredPencils,
            this.menuItem_Styles_FrostedGlass,
            this.menuItem_Styles_OilPainting,
            this.menuItem_Styles_Pencil});
            this.menuItem_Styles.Text = "&Styles";
            // 
            // menuItem_Styles_ColoredPencils
            // 
            this.menuItem_Styles_ColoredPencils.Index = 0;
            this.menuItem_Styles_ColoredPencils.Text = "&Colored Pencils...";
            this.menuItem_Styles_ColoredPencils.Click += new System.EventHandler(this.menuItem_Styles_ColoredPencils_Click);
            // 
            // menuItem_Styles_FrostedGlass
            // 
            this.menuItem_Styles_FrostedGlass.Index = 1;
            this.menuItem_Styles_FrostedGlass.Text = "&Frosted Glass...";
            this.menuItem_Styles_FrostedGlass.Click += new System.EventHandler(this.menuItem_Styles_FrostedGlass_Click);
            // 
            // menuItem_Styles_OilPainting
            // 
            this.menuItem_Styles_OilPainting.Index = 2;
            this.menuItem_Styles_OilPainting.Text = "&Oil Painting...";
            this.menuItem_Styles_OilPainting.Click += new System.EventHandler(this.menuItem_Styles_OilPainting_Click);
            // 
            // menuItem_Styles_Pencil
            // 
            this.menuItem_Styles_Pencil.Index = 3;
            this.menuItem_Styles_Pencil.Text = "&Pencil...";
            this.menuItem_Styles_Pencil.Click += new System.EventHandler(this.menuItem_Styles_Pencil_Click);
            // 
            // menuItem_Help
            // 
            this.menuItem_Help.Index = 7;
            this.menuItem_Help.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.menuItem_Help_About});
            this.menuItem_Help.Text = "&Help";
            // 
            // menuItem_Help_About
            // 
            this.menuItem_Help_About.Index = 0;
            this.menuItem_Help_About.Text = "&About...";
            this.menuItem_Help_About.Click += new System.EventHandler(this.menuItem_Help_About_Click);
            // 
            // libraryPane
            // 
            this.libraryPane.Dock = System.Windows.Forms.DockStyle.Fill;
            this.libraryPane.Location = new System.Drawing.Point(0, 0);
            this.libraryPane.Manager = null;
            this.libraryPane.Name = "libraryPane";
            this.libraryPane.Size = new System.Drawing.Size(360, 248);
            this.libraryPane.TabIndex = 3;
            // 
            // imageComponent
            // 
            this.imageComponent.AutoFit = true;
            this.imageComponent.CloseDelegate = null;
            this.imageComponent.Dock = System.Windows.Forms.DockStyle.Fill;
            this.imageComponent.ImageWrapper = null;
            this.imageComponent.LastOperation = "";
            this.imageComponent.Location = new System.Drawing.Point(0, 0);
            this.imageComponent.Name = "imageComponent";
            this.imageComponent.Size = new System.Drawing.Size(432, 300);
            this.imageComponent.StatusNotification = null;
            this.imageComponent.TabIndex = 2;
            // 
            // viewsPanel
            // 
            this.viewsPanel.Controls.Add(this.switchPanel);
            this.viewsPanel.Controls.Add(this.notificationComponent);
            this.viewsPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.viewsPanel.Location = new System.Drawing.Point(369, 3);
            this.viewsPanel.Name = "viewsPanel";
            this.viewsPanel.Size = new System.Drawing.Size(316, 248);
            this.viewsPanel.TabIndex = 0;
            // 
            // switchPanel
            // 
            this.switchPanel.Controls.Add(this.libraryComponent);
            this.switchPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.switchPanel.Location = new System.Drawing.Point(0, 0);
            this.switchPanel.Name = "switchPanel";
            this.switchPanel.Size = new System.Drawing.Size(316, 194);
            this.switchPanel.TabIndex = 0;
            // 
            // libraryComponent
            // 
            this.libraryComponent.Dock = System.Windows.Forms.DockStyle.Fill;
            this.libraryComponent.Library = null;
            this.libraryComponent.Location = new System.Drawing.Point(0, 0);
            this.libraryComponent.Name = "libraryComponent";
            this.libraryComponent.Size = new System.Drawing.Size(316, 194);
            this.libraryComponent.StatusNotification = null;
            this.libraryComponent.TabIndex = 4;
            // 
            // notificationComponent
            // 
            this.notificationComponent.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.notificationComponent.Location = new System.Drawing.Point(0, 194);
            this.notificationComponent.Name = "notificationComponent";
            this.notificationComponent.Progress = 0;
            this.notificationComponent.Size = new System.Drawing.Size(316, 54);
            this.notificationComponent.TabIndex = 5;
            this.notificationComponent.ZoomEnable = true;
            this.notificationComponent.ZoomMax = 10;
            this.notificationComponent.ZoomMin = 0;
            this.notificationComponent.ZoomMsg = "";
            this.notificationComponent.ZoomValue = 0;
            // 
            // splitterVertical
            // 
            this.splitterVertical.Location = new System.Drawing.Point(363, 3);
            this.splitterVertical.Name = "splitterVertical";
            this.splitterVertical.Size = new System.Drawing.Size(6, 248);
            this.splitterVertical.TabIndex = 5;
            this.splitterVertical.TabStop = false;
            // 
            // paneLeftSideBar
            // 
            this.paneLeftSideBar.Controls.Add(this.libraryPane);
            this.paneLeftSideBar.Dock = System.Windows.Forms.DockStyle.Left;
            this.paneLeftSideBar.Location = new System.Drawing.Point(3, 3);
            this.paneLeftSideBar.Name = "paneLeftSideBar";
            this.paneLeftSideBar.Size = new System.Drawing.Size(360, 248);
            this.paneLeftSideBar.TabIndex = 4;
            // 
            // MainForm
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(9, 22);
            this.ClientSize = new System.Drawing.Size(688, 251);
            this.Controls.Add(this.viewsPanel);
            this.Controls.Add(this.splitterVertical);
            this.Controls.Add(this.paneLeftSideBar);
            this.Menu = this.mainMenu;
            this.Name = "MainForm";
            this.Padding = new System.Windows.Forms.Padding(3, 3, 3, 0);
            this.viewsPanel.ResumeLayout(false);
            this.switchPanel.ResumeLayout(false);
            this.paneLeftSideBar.ResumeLayout(false);
            this.ResumeLayout(false);

		}

		/// <summary>
		/// 
		/// </summary>
		private void resetGUIState()
		{
			bool image = !this.imageComponent.Empty;

			///File
			menuItem_File_Close.Visible       = menuItem_File_Close.Enabled  = image;
			menuItem_File_Separator_1.Visible = image;
			menuItem_File_Save.Visible        = menuItem_File_Save.Enabled   = image;
			menuItem_File_SaveAs.Visible      = menuItem_File_SaveAs.Enabled = image;

			///Edit
			menuItem_Edit_Undo.Visible        = menuItem_Edit_Undo.Enabled       = image;
			menuItem_Edit_Redo.Visible        = menuItem_Edit_Redo.Enabled       = image;
			menuItem_Edit_DiscardAll.Visible  = menuItem_Edit_DiscardAll.Enabled = image;
			menuItem_Edit_Separator_1.Visible = image;
			menuItem_Edit_Cut.Visible         = menuItem_Edit_Cut.Enabled        = image;
			menuItem_Edit_Copy.Visible        = menuItem_Edit_Copy.Enabled       = image;
			menuItem_Edit_Paste.Visible       = menuItem_Edit_Paste.Enabled      = image;

			///View
			menuItem_View_Thumbnail.Visible = menuItem_View_Thumbnail.Enabled = image;

			///Image
			menuItem_Image.Visible               = image;
			menuItem_Image_FlipRotate.Visible    = menuItem_Image_FlipRotate.Enabled    = image;
			menuItem_Image_StretchResize.Visible = menuItem_Image_StretchResize.Enabled = image;
			menuItem_Image_Separator_1.Visible   = image;
			menuItem_Image_Brightness.Visible    = menuItem_Image_Brightness.Enabled    = image;
			menuItem_Image_Contrast.Visible      = menuItem_Image_Contrast.Enabled      = image;
			menuItem_Image_Gamma.Visible         = menuItem_Image_Gamma.Enabled         = image;
			menuItem_Image_Separator_2.Visible   = image;
			
			///Colors
			menuItem_Colors.Visible            = image;
			menuItem_Colors_BlackWhite.Visible = menuItem_Colors_BlackWhite.Enabled = image;
			menuItem_Colors_Invert.Visible     = menuItem_Colors_Invert.Enabled     = image;
			menuItem_Colors_Grayscale.Visible  = menuItem_Colors_Grayscale.Enabled  = image;
			menuItem_Colors_Redscale.Visible   = menuItem_Colors_Redscale.Enabled   = image;
			menuItem_Colors_Greenscale.Visible = menuItem_Colors_Greenscale.Enabled = image;
			menuItem_Colors_Bluescale.Visible  = menuItem_Colors_Bluescale.Enabled  = image;
		}
		#endregion

		/// <summary>
		/// 
		/// </summary>
		public LibraryManager LibraryManager
		{
			get
			{
				return this.libraryPane.Manager;				
			}
			set
			{
				this.libraryPane.Manager = value;				
			}
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="view"></param>
		protected void switchView(bool closedImage, CurrentView view)
		{
			if (this.currentView != view)
			{
				//Remove existing
				switch(this.currentView)
				{
					case CurrentView.VIEW_EDITOR:

						if (closedImage || this.imageComponent.Empty || this.CloseImage())
						{
							this.switchPanel.Controls.Remove(this.imageComponent);
							this.imageComponent.StatusNotification = null;
						}
						else
							return;
						break;
					case CurrentView.VIEW_LIBRARY:
						this.switchPanel.Controls.Remove(this.libraryComponent);
						this.libraryComponent.StatusNotification = null;
						break;
					case CurrentView.VIEW_HYBRID:
						break;
				}
				
				switch(view)
				{
					case CurrentView.VIEW_EDITOR:
						this.switchPanel.Controls.Add(this.imageComponent);		
						this.imageComponent.StatusNotification = this.notificationComponent;
						break;
					case CurrentView.VIEW_LIBRARY:
						this.switchPanel.Controls.Add(this.libraryComponent);
						this.libraryComponent.StatusNotification = this.notificationComponent;
						break;
					case CurrentView.VIEW_HYBRID:
						break;
				}
				this.currentView = view;
			}
		}

		#region Image Management

		/// <summary>
		/// 
		/// </summary>
		/// <param name="filename"></param>
		/// <returns></returns>
		public bool CreateImage(string filename)
		{
			if (File.Exists(filename))
			{
				if (MessageBox.Show(this, "Image " + Path.GetFileName(filename) + " already exists. Do you want to overwrite it?", "Question", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
				{
					try
					{
						File.Delete(filename);
					} 
					catch (UnauthorizedAccessException)
					{
						MessageBox.Show(this, "You do not have permissions on image " + Path.GetFileName(filename), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
						return false;
					}
					catch (IOException)
					{
						MessageBox.Show(this, "Image " + Path.GetFileName(filename) + " is in use by an unknown application.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
						return false;
					}
					catch (Exception ex)
					{
						MessageBox.Show(this, "An error ocurred while accesing file " + filename + ".\n\nSystem Message:\n" + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
						return false;
					}
				}
				else
				{
					return false;
				}
			}

			if (CloseImage())
			{
				try
				{
					using(Bitmap newBM = new Bitmap(500, 500, PixelFormat.Format32bppArgb))
					{
						newBM.Save(filename);
						newBM.Dispose();
					}
					this.imageComponent.ImageWrapper = new ImageWrapper(filename);
				}
				catch (Exception e)
				{
					this.imageComponent.ImageWrapper = null;
					MessageBox.Show(this,
						"There has been an error creating image " + Path.GetFileName(filename) + ".\n\nSystem Message:\n" + e.Message,
						"Error",
						MessageBoxButtons.OK,
						MessageBoxIcon.Error);
					return false;
				}
				this.imageComponent.ImageWrapper.Changed += new ImageChangedEventHandler(OnBitmapChanged);
				resetGUIState();
				return true;
			}
			else
			{
				return false;
			}
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="filename"></param>
		/// <returns></returns>
		public bool OpenImage(string filename)
		{
			if (CloseImage())
			{
				if (!File.Exists(filename))
				{
					MessageBox.Show(this, "Image " + Path.GetFileName(filename) + " not found.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
					return false;
				}
				try
				{
					this.imageComponent.ImageWrapper = new ImageWrapper(filename);
				}
				catch (Exception e)
				{
					this.imageComponent.ImageWrapper = null;
					MessageBox.Show(this,
						"There has been an error opening image " + Path.GetFileName(filename) + ".\n\nSystem Message:\n" + e.Message,
						"Error",
						MessageBoxButtons.OK,
						MessageBoxIcon.Error);
					return false;
				}
				this.imageComponent.ImageWrapper.Changed += new ImageChangedEventHandler(OnBitmapChanged);
				resetGUIState();
				switchView(true, CurrentView.VIEW_EDITOR);
				return true;
			}
			else
			{
				return false;
			}
		}

		/// <summary>
		/// 
		/// </summary>
		/// <returns></returns>
		public bool CloseImage()
		{
			if (!this.imageComponent.Empty)
			{
				if (this.undoManager.PendingChanges())
				{
					switch(MessageBox.Show(this, "Image '" + Path.GetFileName(this.imageComponent.ImageWrapper.Filename) + "' has been modified.  Would you like to save it before closing it?", "Question", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question))
					{
						case DialogResult.Yes:
							if (!this.SaveImage(this.imageComponent.ImageWrapper.Filename, this.imageComponent.ImageWrapper.Bitmap.RawFormat)
								&& MessageBox.Show(this, "Image was not saved. Would you like to close it anyway?", "Question", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.No)
							{
								return false;
							}
							break;
						case DialogResult.Cancel:
							return false;
					}
					
				}
				this.undoManager.Clear();
				this.imageComponent.ImageWrapper.Changed -= new ImageChangedEventHandler(OnBitmapChanged);
				
				resetGUIState();
				switchView(true, CurrentView.VIEW_LIBRARY);
				//TODO Check the generation # to use to avoid clearing a cache 
				System.GC.Collect();
			}
			return true;
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="filename"></param>
		/// <param name="format"></param>
		/// <returns></returns>
		public bool SaveImage(string filename, ImageFormat format)
		{
			if (!this.imageComponent.Empty)
			{
				FileAttributes attr = File.GetAttributes(this.imageComponent.ImageWrapper.Filename);

				if ((attr & FileAttributes.ReadOnly) != 0)
				{
					try
					{
						File.SetAttributes(this.imageComponent.ImageWrapper.Filename, attr & ~FileAttributes.ReadOnly);
					}
					catch
					{
						MessageBox.Show(this, Path.GetFileName(this.imageComponent.ImageWrapper.Filename)+ " is a read-only file.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
						return false;
					}
				}

				try
				{
					this.imageComponent.ImageWrapper.Save(filename, format);
				}
				catch (Exception e)
				{
					MessageBox.Show(this,
						"There has been an error saving current image.\n\nSystem Message:\n" + e.Message,
						"Error",
						MessageBoxButtons.OK,
						MessageBoxIcon.Error);
					return false;
				}
				return true;
			}
			else
			{
				return false;
			}
		}

		#endregion

		#region Events

		/// <summary>
		/// 
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="args"></param>
		public void OnBitmapChanged(object sender, ImageChangedEventArgs args)
		{
			this.undoManager.addUndo(args.Name, args.Bitmap);
		}
		#endregion

		#region File Menu actions
		private void menuItem_File_New_Click(object sender, System.EventArgs e)
		{
			OpenFileDialog dialog  = new OpenFileDialog();
			dialog.Title           = "New Image";
			dialog.Filter          = MainForm.ImageFileFilter;
			dialog.CheckPathExists = true;
			dialog.CheckFileExists = false;
			dialog.DefaultExt      = "bmp";
			dialog.AddExtension    = true;
			if (dialog.ShowDialog(this) == DialogResult.OK)
			{
				this.CreateImage(dialog.FileName);
			}
		}

		private void menuItem_File_Open_Click(object sender, System.EventArgs e)
		{
			OpenFileDialog dialog  = new OpenFileDialog();
			dialog.Title           = "Open Image";
			dialog.Filter          = MainForm.ImageAllFileFilter;
			dialog.CheckPathExists = true;
			dialog.CheckFileExists = true;
			if (dialog.ShowDialog(this) == DialogResult.OK)
			{
				this.OpenImage(dialog.FileName);
			}
		}

		private void menuItem_File_Close_Click(object sender, System.EventArgs e)
		{
			this.CloseImage();
		}

		private void menuItem_File_Save_Click(object sender, System.EventArgs e)
		{
			if (!this.SaveImage(this.imageComponent.ImageWrapper.Filename, this.imageComponent.ImageWrapper.Bitmap.RawFormat))
			{
				MessageBox.Show(this, "Image not saved", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
			}
		}

		private void menuItem_File_SaveAs_Click(object sender, System.EventArgs e)
		{
			string extension = Path.GetExtension(this.imageComponent.ImageWrapper.Filename);
			SaveFileDialog sfd = new SaveFileDialog();
			sfd.Filter = "Current Format (*." + extension.ToLowerInvariant() + ")|*." + extension;
			sfd.CheckPathExists = true;
			sfd.CheckFileExists = false;
			sfd.DefaultExt      = "bmp";
			sfd.AddExtension    = true;
			if (sfd.ShowDialog() == DialogResult.OK )
			{
				if (String.Compare(sfd.FileName, this.imageComponent.ImageWrapper.Filename, true) == 0
					|| !File.Exists(sfd.FileName)
					|| (MessageBox.Show(this, "Image " + Path.GetFileName(sfd.FileName) + " already exists.  Do you want to overwrite it?", "Question", MessageBoxButtons.YesNo) == DialogResult.Yes))
				{
					bool saved = false;
					switch(Path.GetExtension(sfd.FileName).ToLowerInvariant())
					{
						case "bmp": // BMP
							saved = this.SaveImage(sfd.FileName, ImageFormat.Bmp);
							break;
						case "exif": // EXIF
							saved = this.SaveImage(sfd.FileName, ImageFormat.Exif);
							break;
						case "emf": // EMF
							saved = this.SaveImage(sfd.FileName, ImageFormat.Emf);
							break;
						case "gif": // GIF
							saved = this.SaveImage(sfd.FileName, ImageFormat.Gif);
							break;
						case "ico": // ICO
							saved = this.SaveImage(sfd.FileName, ImageFormat.Icon);
							break;
						case "jpg":  // JPG
						case "jpeg": // JPEG
							saved = this.SaveImage(sfd.FileName, ImageFormat.Jpeg);
							break;
						case "png": // PNG
							saved = this.SaveImage(sfd.FileName, ImageFormat.Png);
							break;
						case "tif":  // TIF
						case "tiff": // TIFF
							saved = this.SaveImage(sfd.FileName, ImageFormat.Tiff);
							break;
						case "wmf": // WMF
							saved = this.SaveImage(sfd.FileName, ImageFormat.Wmf);
							break;
						default:
							saved = this.SaveImage(sfd.FileName, ImageFormat.Bmp);
							break;
					}
					if (saved)
					{
						this.imageComponent.ImageWrapper.Filename = sfd.FileName;
					}
					else
					{
						MessageBox.Show(this, "Image not saved", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
					}
				}
			}		
		}

		private void menuItem_File_ScannerCamera_Click(object sender, System.EventArgs e)
		{
		
		}

		private void menuItem_File_Screenshot_Click(object sender, System.EventArgs e)
		{
		}

		private void menuItem_File_Exit_Click(object sender, System.EventArgs e)
		{
			if (this.CloseImage())
			{
				this.Close();
			}
		}
		#endregion

		#region Edit Menu actions
		private void menuItem_Edit_Popup(object sender, System.EventArgs e)
		{
			if (undoManager.canUndo())
			{
				this.menuItem_Edit_Undo.Text          = "Undo " + undoManager.getUndoName();
				this.menuItem_Edit_Undo.Enabled       = true;
				this.menuItem_Edit_DiscardAll.Enabled = true;
			}
			else
			{
				this.menuItem_Edit_Undo.Text          = "Nothing to Undo";
				this.menuItem_Edit_Undo.Enabled       = false;
				this.menuItem_Edit_DiscardAll.Enabled = false;
			}

			if (undoManager.canRedo())
			{
				this.menuItem_Edit_Redo.Text    = "Redo " + undoManager.getRedoName();
				this.menuItem_Edit_Redo.Enabled = true;
			}
			else
			{
				this.menuItem_Edit_Redo.Text    = "Nothing to Redo";
				this.menuItem_Edit_Redo.Enabled = false;
			}

			this.menuItem_Edit_Paste.Enabled    = !this.imageComponent.Empty 
				&& (Clipboard.GetDataObject() != null)
				&& Clipboard.GetDataObject().GetDataPresent(DataFormats.Bitmap);
		}

		private void menuItem_Edit_Undo_Click(object sender, System.EventArgs e)
		{
			this.undoManager.Lock           = true;
			UndoManager.NamedPair pair      = this.undoManager.getUndo(this.imageComponent.LastOperation, this.imageComponent.ImageWrapper.Bitmap);
			this.imageComponent.LastOperation  = pair.Name;
			this.imageComponent.ImageWrapper.Bitmap = pair.Bitmap;
			this.undoManager.Lock           = false;		
		}

		private void menuItem_Edit_Redo_Click(object sender, System.EventArgs e)
		{
			this.undoManager.Lock           = true;
			UndoManager.NamedPair pair      = this.undoManager.getRedo(this.imageComponent.LastOperation, this.imageComponent.ImageWrapper.Bitmap);
			this.imageComponent.LastOperation  = pair.Name;
			this.imageComponent.ImageWrapper.Bitmap = pair.Bitmap;
			this.undoManager.Lock           = false;		
		}

		private void menuItem_Edit_DiscardAll_Click(object sender, System.EventArgs e)
		{
			this.undoManager.Lock = true;
			this.imageComponent.ImageWrapper.Bitmap = this.undoManager.discardAll();
			this.undoManager.Lock = false;		
		}

		private void menuItem_Edit_Cut_Click(object sender, System.EventArgs e)
		{
		
		}

		private void menuItem_Edit_Copy_Click(object sender, System.EventArgs e)
		{
			Clipboard.SetDataObject(this.imageComponent.ImageWrapper.Bitmap, true);
		}

		private void menuItem_Edit_Paste_Click(object sender, System.EventArgs e)
		{
		
		}
		#endregion

		#region View Menu Actions

		private void menuItem_View_Popup(object sender, System.EventArgs e)
		{
			menuItem_View_Thumbnail.Checked = ThumbnailForm.IsVisible();
		}

		private void menuItem_View_Thumbnail_Click(object sender, System.EventArgs e)
		{
			if (ThumbnailForm.IsVisible())
			{
				ThumbnailForm.CloseView();
			}
			else
			{
				ThumbnailForm.ShowView(this, this.imageComponent.ImageWrapper);
			}
		}
		#endregion

		#region Image Menu Actions
		/// <summary>
		/// 
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void menuItem_Image_FlipRotate_Click(object sender, System.EventArgs e)
		{
			FlipRotateWindow window = new FlipRotateWindow(this.imageComponent.ImageWrapper);
			window.ShowDialog(this);
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void menuItem_Image_StretchResize_Click(object sender, System.EventArgs e)
		{
			StretchResizeWindow window = new StretchResizeWindow(this.imageComponent.ImageWrapper);
			window.ShowDialog(this);		
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void menuItem_Image_Brightness_Click(object sender, System.EventArgs e)
		{
			ThresholdWindow window = new ThresholdWindow(this.imageComponent.ImageWrapper, new FilterBrightness());
			window.ShowDialog(this);
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void menuItem_Image_Contrast_Click(object sender, System.EventArgs e)
		{
			ThresholdWindow window = new ThresholdWindow(this.imageComponent.ImageWrapper, new FilterContrast());
			window.ShowDialog(this);		
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void menuItem_Image_Gamma_Click(object sender, System.EventArgs e)
		{
			ThresholdWindow window = new ThresholdWindow(this.imageComponent.ImageWrapper, new FilterGamma());
			window.ShowDialog(this);		
		}
		#endregion

		#region Colors Menu Actions

		private void menuItem_Colors_BlackWhite_Click(object sender, System.EventArgs e)
		{
			ThresholdWindow window = new ThresholdWindow(this.imageComponent.ImageWrapper, new FilterBlackWhite());
			window.ShowDialog(this);
		}

		private void menuItem_Colors_Invert_Click(object sender, System.EventArgs e)
		{
			BasicFilterWindow window = new BasicFilterWindow(this.imageComponent.ImageWrapper, new FilterInvert());
			window.ShowDialog(this);
		}

		private void menuItem_Colors_Grayscale_Click(object sender, System.EventArgs e)
		{
			BasicFilterWindow window = new BasicFilterWindow(this.imageComponent.ImageWrapper, new FilterGrayAdjusted());
			window.ShowDialog(this);		
		}

		private void menuItem_Colors_Redscale_Click(object sender, System.EventArgs e)
		{
			BasicFilterWindow window = new BasicFilterWindow(this.imageComponent.ImageWrapper, new FilterRed());
			window.ShowDialog(this);		
		}

		private void menuItem_Colors_Greenscale_Click(object sender, System.EventArgs e)
		{
			BasicFilterWindow window = new BasicFilterWindow(this.imageComponent.ImageWrapper, new FilterGreen());
			window.ShowDialog(this);		
		}

		private void menuItem_Colors_Bluescale_Click(object sender, System.EventArgs e)
		{
			BasicFilterWindow window = new BasicFilterWindow(this.imageComponent.ImageWrapper, new FilterBlue());
			window.ShowDialog(this);		
		}
		#endregion

		#region Styles Menu Actions

		private void menuItem_Styles_ColoredPencils_Click(object sender, System.EventArgs e)
		{
			ThresholdWindow window = new ThresholdWindow(this.imageComponent.ImageWrapper, new FilterColoredPencil());
			window.ShowDialog(this);		
		}

		private void menuItem_Styles_FrostedGlass_Click(object sender, System.EventArgs e)
		{
			ThresholdWindow window = new ThresholdWindow(this.imageComponent.ImageWrapper, new FilterFrostedGlass());
			window.ShowDialog(this);		
		}

		private void menuItem_Styles_OilPainting_Click(object sender, System.EventArgs e)
		{
			ThresholdWindow window = new ThresholdWindow(this.imageComponent.ImageWrapper, new FilterOil());
			window.ShowDialog(this);	
		}

		private void menuItem_Styles_Pencil_Click(object sender, System.EventArgs e)
		{
			ThresholdWindow window = new ThresholdWindow(this.imageComponent.ImageWrapper, new FilterPencil());
			window.ShowDialog(this);		
		}
		#endregion

		#region Effects Menu Actions
		private void menuItem_Effects_Blur_Click(object sender, System.EventArgs e)
		{
			BasicFilterWindow window = new BasicFilterWindow(this.imageComponent.ImageWrapper, new FilterMatrixBlur());
			window.ShowDialog(this);
		}

		private void menuItem_Effects_Sharpen_Click(object sender, System.EventArgs e)
		{
			BasicFilterWindow window = new BasicFilterWindow(this.imageComponent.ImageWrapper, new FilterMatrixSharpen());
			window.ShowDialog(this);		
		}

		private void menuItem_Effects_Emboss_Click(object sender, System.EventArgs e)
		{
		
		}

		private void menuItem_Effects_EdgeEnhance_Click(object sender, System.EventArgs e)
		{
		
		}

		private void menuItem_Effects_ED_BW_Click(object sender, System.EventArgs e)
		{
			ThresholdWindow window = new ThresholdWindow(this.imageComponent.ImageWrapper, new FilterMatrixBW());
			window.ShowDialog(this);
		}

		private void menuItemEffects_ED_Kirsh_Click(object sender, System.EventArgs e)
		{
			BasicFilterWindow window = new BasicFilterWindow(this.imageComponent.ImageWrapper, new FilterMatrixKirshED());
			window.ShowDialog(this);		
		}

		private void menuItem_Effects_ED_Prewitt_Click(object sender, System.EventArgs e)
		{
			BasicFilterWindow window = new BasicFilterWindow(this.imageComponent.ImageWrapper, new FilterMatrixPrewittED());
			window.ShowDialog(this);		
		}

		private void menuItem_Effects_ED_Sobel_Click(object sender, System.EventArgs e)
		{
			BasicFilterWindow window = new BasicFilterWindow(this.imageComponent.ImageWrapper, new FilterMatrixSobelED());
			window.ShowDialog(this);		
		}

		private void menuItem_Effects_Dilate_Click(object sender, System.EventArgs e)
		{
		
		}

		private void menuItem_Effects_Median_Click(object sender, System.EventArgs e)
		{
		
		}

		private void menuItem_Effects_Erosion_Click(object sender, System.EventArgs e)
		{
		
		}

		private void menuItem_Effects_Opening_Click(object sender, System.EventArgs e)
		{
		
		}

		private void menuItem_Effects_Closing_Click(object sender, System.EventArgs e)
		{
		
		}

		private void menuItem_Effects_Posterize_Click(object sender, System.EventArgs e)
		{
		
		}

		private void menuItem_Effects_Solarize_Click(object sender, System.EventArgs e)
		{
		
		}

		private void menuItem_Effects_Mean_Click(object sender, System.EventArgs e)
		{
			BasicFilterWindow window = new BasicFilterWindow(this.imageComponent.ImageWrapper, new FilterMean());
			window.ShowDialog(this);
		}
		#endregion

		private void libraryPane_OpenLibrary(object sender, OpenLibraryEventArgs e)
		{
			this.libraryComponent.Library = e.Item;
			this.switchView(false, CurrentView.VIEW_LIBRARY);
		}
	
		/// <summary>
		/// 
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void libraryComponent_OpenImage(object sender, ImageOpenEventArgs e)
		{
			this.OpenImage(e.Item.Item.Path);
		}

		private void menuItem_Help_About_Click(object sender, System.EventArgs e)
		{
			AboutForm dialog = AboutForm.Instance();
			dialog.ShowDialog(this);
		}
	}

	/// <summary>
	/// Help class to add a description
	/// </summary>
	class MenuItem : System.Windows.Forms.MenuItem
	{
		/// <summary>
		/// Description associated to the menu Item
		/// </summary>
		public string Description
		{
			get
			{
				return this.Description;
			}
			set
			{
				this.Description = value;
			}
		}
	}
}