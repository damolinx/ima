using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ima
{
    partial class MainForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

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
            // menuItem_Effects_Separator_2
            // 
            this.menuItem_Effects_Separator_2.Index = 9;
            this.menuItem_Effects_Separator_2.Text = "-";
            // 
            // menuItem_Effects_Posterize
            // 
            this.menuItem_Effects_Posterize.Index = 10;
            this.menuItem_Effects_Posterize.Text = "&Posterize...";
            this.menuItem_Effects_Posterize.Click += new System.EventHandler(this.menuItem_Effects_Posterize_Click);
            // 
            // menuItem_Effects_Solarize
            // 
            this.menuItem_Effects_Solarize.Index = 11;
            this.menuItem_Effects_Solarize.Text = "Solari&ze...";
            this.menuItem_Effects_Solarize.Click += new System.EventHandler(this.menuItem_Effects_Solarize_Click);
            // 
            // menuItem_Effects_Separator_3
            // 
            this.menuItem_Effects_Separator_3.Index = 12;
            this.menuItem_Effects_Separator_3.Text = "-";
            // 
            // menuItem_Effects_Mean
            // 
            this.menuItem_Effects_Mean.Index = 13;
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
            this.libraryPane.Dock = System.Windows.Forms.DockStyle.Left;
            this.libraryPane.Location = new System.Drawing.Point(0, 0);
            this.libraryPane.Manager = null;
            this.libraryPane.Name = "libraryPane";
            this.libraryPane.Size = new System.Drawing.Size(360, 736);
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
            this.viewsPanel.Size = new System.Drawing.Size(1014, 736);
            this.viewsPanel.TabIndex = 0;
            // 
            // switchPanel
            // 
            this.switchPanel.Controls.Add(this.libraryComponent);
            this.switchPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.switchPanel.Location = new System.Drawing.Point(0, 0);
            this.switchPanel.Name = "switchPanel";
            this.switchPanel.Size = new System.Drawing.Size(1014, 682);
            this.switchPanel.TabIndex = 0;
            // 
            // libraryComponent
            // 
            this.libraryComponent.Dock = System.Windows.Forms.DockStyle.Fill;
            this.libraryComponent.Library = null;
            this.libraryComponent.Location = new System.Drawing.Point(0, 0);
            this.libraryComponent.Name = "libraryComponent";
            this.libraryComponent.Size = new System.Drawing.Size(1014, 682);
            this.libraryComponent.StatusNotification = null;
            this.libraryComponent.TabIndex = 4;
            // 
            // notificationComponent
            // 
            this.notificationComponent.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.notificationComponent.Location = new System.Drawing.Point(0, 682);
            this.notificationComponent.Name = "notificationComponent";
            this.notificationComponent.Progress = 0;
            this.notificationComponent.Size = new System.Drawing.Size(1014, 54);
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
            this.splitterVertical.Size = new System.Drawing.Size(6, 736);
            this.splitterVertical.TabIndex = 5;
            this.splitterVertical.TabStop = false;
            // 
            // paneLeftSideBar
            // 
            this.paneLeftSideBar.Controls.Add(this.libraryPane);
            this.paneLeftSideBar.Dock = System.Windows.Forms.DockStyle.Left;
            this.paneLeftSideBar.Location = new System.Drawing.Point(3, 3);
            this.paneLeftSideBar.Name = "paneLeftSideBar";
            this.paneLeftSideBar.Size = new System.Drawing.Size(360, 736);
            this.paneLeftSideBar.TabIndex = 4;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(168F, 168F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.ClientSize = new System.Drawing.Size(1386, 739);
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

        #endregion

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
    }
}
