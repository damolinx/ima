using Ima.Controls;
using Ima.ImageOps;
using Ima.ImageOps.Filters;
using Ima.Library;
using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Windows.Forms;

namespace Ima
{
    /// <summary>
    /// Summary description for MainForm.
    /// </summary>
    public partial class MainForm : Ima.Controls.BaseForm
    {
        private const string ImageFileFilter = "Bitmap (*.bmp)|*.bmp" +
            "|Enhanced Windows Metafile (*.emf)|*.emf" +
            "|Exchangeable Image Format (*.exif)|*.exif" +
            "|Graphics Interchange Format (*.gif)|*.gif" +
            "|Icon Format|*.ico" +
            "|Joint Photographic Experts Group (*.jpg,*.jpeg)|*.jpg;*.jpeg" +
            "|Portable Network Graphics (*.png)|*.png" +
            "|Tag Image File Format (*.tiff)|*.tiff;*.tif" +
            "|Windows metafile (*.wmf)|*.wmf";

        private const string ImageAllFileFilter = "All Image Files| *.bmp;*.emf;*.exif;*.gif;*.ico;*.jpg;*.jpeg;*.png;*.tif;*.tiff;*.wmf|All Files|*.*|" + ImageFileFilter;

        private ViewMode currentView = ViewMode.Library;

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
            this.Name = Program.APPLICATION_NAME;
            this.Text = Program.APPLICATION_NAME;

            //Add relevant listeners
            this.libraryPane.OpenLibrary += new OpenLibraryItemEventHandler(libraryPane_OpenLibrary);
            this.libraryComponent.OpenImage += new ImageOpenEventHandler(libraryComponent_OpenImage);

            this.libraryComponent.StatusNotification = this.notificationComponent;

            this.imageComponent.CloseDelegate = this.CloseImage;

            ResetGUIState();
        }

        /// <summary>
        /// 
        /// </summary>
        private void ResetGUIState()
        {
            bool hasImage = !this.imageComponent.Empty;

            ///File
            menuItem_File_Close.Visible = menuItem_File_Close.Enabled = hasImage;
            menuItem_File_Separator_1.Visible = hasImage;
            menuItem_File_Save.Visible = menuItem_File_Save.Enabled = hasImage;
            menuItem_File_SaveAs.Visible = menuItem_File_SaveAs.Enabled = hasImage;

            ///Edit
            menuItem_Edit_Undo.Visible = menuItem_Edit_Undo.Enabled = hasImage;
            menuItem_Edit_Redo.Visible = menuItem_Edit_Redo.Enabled = hasImage;
            menuItem_Edit_DiscardAll.Visible = menuItem_Edit_DiscardAll.Enabled = hasImage;
            menuItem_Edit_Separator_1.Visible = hasImage;
            menuItem_Edit_Cut.Visible = menuItem_Edit_Cut.Enabled = hasImage;
            menuItem_Edit_Copy.Visible = menuItem_Edit_Copy.Enabled = hasImage;
            menuItem_Edit_Paste.Visible = menuItem_Edit_Paste.Enabled = hasImage;

            ///View
            menuItem_View_Thumbnail.Visible = menuItem_View_Thumbnail.Enabled = hasImage;

            ///Image
            menuItem_Image.Visible = hasImage;
            menuItem_Image_FlipRotate.Visible = menuItem_Image_FlipRotate.Enabled = hasImage;
            menuItem_Image_StretchResize.Visible = menuItem_Image_StretchResize.Enabled = hasImage;
            menuItem_Image_Separator_1.Visible = hasImage;
            menuItem_Image_Brightness.Visible = menuItem_Image_Brightness.Enabled = hasImage;
            menuItem_Image_Contrast.Visible = menuItem_Image_Contrast.Enabled = hasImage;
            menuItem_Image_Gamma.Visible = menuItem_Image_Gamma.Enabled = hasImage;
            menuItem_Image_Separator_2.Visible = hasImage;

            ///Colors
            menuItem_Colors.Visible = hasImage;
            menuItem_Colors_BlackWhite.Visible = menuItem_Colors_BlackWhite.Enabled = hasImage;
            menuItem_Colors_Invert.Visible = menuItem_Colors_Invert.Enabled = hasImage;
            menuItem_Colors_Grayscale.Visible = menuItem_Colors_Grayscale.Enabled = hasImage;
            menuItem_Colors_Redscale.Visible = menuItem_Colors_Redscale.Enabled = hasImage;
            menuItem_Colors_Greenscale.Visible = menuItem_Colors_Greenscale.Enabled = hasImage;
            menuItem_Colors_Bluescale.Visible = menuItem_Colors_Bluescale.Enabled = hasImage;

            ///Effects
            menuItem_Effects.Visible = hasImage;

            ///Styles
            menuItem_Styles.Visible = hasImage;
        }

        #endregion

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

        protected void SwitchView(bool closedImage, ViewMode view)
        {
            if (this.currentView != view)
            {
                //Remove existing
                switch (this.currentView)
                {
                    case ViewMode.Editor:

                        if (closedImage || this.imageComponent.Empty || this.CloseImage())
                        {
                            this.switchPanel.Controls.Remove(this.imageComponent);
                            this.imageComponent.StatusNotification = null;
                        }
                        else
                            return;
                        break;
                    case ViewMode.Library:
                        this.switchPanel.Controls.Remove(this.libraryComponent);
                        this.libraryComponent.StatusNotification = null;
                        break;
                    case ViewMode.Hybrid:
                        break;
                }

                switch (view)
                {
                    case ViewMode.Editor:
                        this.switchPanel.Controls.Add(this.imageComponent);
                        this.imageComponent.StatusNotification = this.notificationComponent;
                        break;
                    case ViewMode.Library:
                        this.switchPanel.Controls.Add(this.libraryComponent);
                        this.libraryComponent.StatusNotification = this.notificationComponent;
                        break;
                    case ViewMode.Hybrid:
                        break;
                }
                this.currentView = view;
            }
        }

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
                    using (var newBM = new Bitmap(500, 500, PixelFormat.Format32bppArgb))
                    {
                        newBM.Save(filename);
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
                ResetGUIState();
                return true;
            }
            else
            {
                return false;
            }
        }

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
                ResetGUIState();
                SwitchView(true, ViewMode.Editor);
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool CloseImage()
        {
            if (!this.imageComponent.Empty)
            {
                if (this.undoManager.PendingChanges)
                {
                    switch (MessageBox.Show(this, "Image '" + Path.GetFileName(this.imageComponent.ImageWrapper.Filename) + "' has been modified.  Would you like to save it before closing it?", "Question", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question))
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

                ResetGUIState();
                SwitchView(true, ViewMode.Library);
                //TODO Check the generation # to use to avoid clearing a cache 
                GC.Collect();
            }
            return true;
        }

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
                        MessageBox.Show(this, Path.GetFileName(this.imageComponent.ImageWrapper.Filename) + " is a read-only file.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
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

        public void OnBitmapChanged(object sender, ImageChangedEventArgs args)
        {
            this.undoManager.AddUndo(args.Name, args.Bitmap);
        }

        #region File Menu actions
        private void menuItem_File_New_Click(object sender, EventArgs e)
        {
            var dialog = new OpenFileDialog
            {
                Title = "New Image",
                Filter = MainForm.ImageFileFilter,
                CheckPathExists = true,
                CheckFileExists = false,
                DefaultExt = "bmp",
                AddExtension = true,
            };

            if (dialog.ShowDialog(this) == DialogResult.OK)
            {
                this.CreateImage(dialog.FileName);
            }
        }

        private void menuItem_File_Open_Click(object sender, EventArgs e)
        {
            var dialog = new OpenFileDialog
            {
                Title = "Open Image",
                Filter = MainForm.ImageAllFileFilter,
                CheckPathExists = true,
                CheckFileExists = true,
            };

            if (dialog.ShowDialog(this) == DialogResult.OK)
            {
                this.OpenImage(dialog.FileName);
            }
        }

        private void menuItem_File_Close_Click(object sender, EventArgs e)
        {
            this.CloseImage();
        }

        private void menuItem_File_Save_Click(object sender, EventArgs e)
        {
            if (!this.SaveImage(this.imageComponent.ImageWrapper.Filename, this.imageComponent.ImageWrapper.Bitmap.RawFormat))
            {
                MessageBox.Show(this, "Image not saved", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void menuItem_File_SaveAs_Click(object sender, EventArgs e)
        {
            string extension = Path.GetExtension(this.imageComponent.ImageWrapper.Filename);
            SaveFileDialog sfd = new SaveFileDialog();
            sfd.Filter = "Current Format (*." + extension.ToLowerInvariant() + ")|*." + extension;
            sfd.CheckPathExists = true;
            sfd.CheckFileExists = false;
            sfd.DefaultExt = "bmp";
            sfd.AddExtension = true;
            if (sfd.ShowDialog() == DialogResult.OK)
            {
                if (StringComparer.OrdinalIgnoreCase.Equals(sfd.FileName, this.imageComponent.ImageWrapper.Filename)
                    || !File.Exists(sfd.FileName)
                    || (MessageBox.Show(this, "Image " + Path.GetFileName(sfd.FileName) + " already exists.  Do you want to overwrite it?", "Question", MessageBoxButtons.YesNo) == DialogResult.Yes))
                {
                    bool saved = false;
                    switch (Path.GetExtension(sfd.FileName).ToLowerInvariant())
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

        private void menuItem_File_ScannerCamera_Click(object sender, EventArgs e)
        {

        }

        private void menuItem_File_Screenshot_Click(object sender, EventArgs e)
        {
        }

        private void menuItem_File_Exit_Click(object sender, EventArgs e)
        {
            if (this.CloseImage())
            {
                this.Close();
            }
        }

        #endregion

        #region Edit Menu actions
        private void menuItem_Edit_Popup(object sender, EventArgs e)
        {
            if (undoManager.CanUndo())
            {
                this.menuItem_Edit_Undo.Text = "Undo " + undoManager.GetUndoName();
                this.menuItem_Edit_Undo.Enabled = true;
                this.menuItem_Edit_DiscardAll.Enabled = true;
            }
            else
            {
                this.menuItem_Edit_Undo.Text = "Nothing to Undo";
                this.menuItem_Edit_Undo.Enabled = false;
                this.menuItem_Edit_DiscardAll.Enabled = false;
            }

            if (undoManager.CanRedo())
            {
                this.menuItem_Edit_Redo.Text = "Redo " + undoManager.GetRedoName();
                this.menuItem_Edit_Redo.Enabled = true;
            }
            else
            {
                this.menuItem_Edit_Redo.Text = "Nothing to Redo";
                this.menuItem_Edit_Redo.Enabled = false;
            }

            this.menuItem_Edit_Paste.Enabled = !this.imageComponent.Empty
                && (Clipboard.GetDataObject() != null)
                && Clipboard.GetDataObject().GetDataPresent(DataFormats.Bitmap);
        }

        private void menuItem_Edit_Undo_Click(object sender, EventArgs e)
        {
            this.undoManager.Lock = true;
            UndoManager.NamedPair pair = this.undoManager.GetUndo(this.imageComponent.LastOperation, this.imageComponent.ImageWrapper.Bitmap);
            this.imageComponent.LastOperation = pair.Name;
            this.imageComponent.ImageWrapper.Bitmap = pair.Bitmap;
            this.undoManager.Lock = false;
        }

        private void menuItem_Edit_Redo_Click(object sender, EventArgs e)
        {
            this.undoManager.Lock = true;
            UndoManager.NamedPair pair = this.undoManager.GetRedo(this.imageComponent.LastOperation, this.imageComponent.ImageWrapper.Bitmap);
            this.imageComponent.LastOperation = pair.Name;
            this.imageComponent.ImageWrapper.Bitmap = pair.Bitmap;
            this.undoManager.Lock = false;
        }

        private void menuItem_Edit_DiscardAll_Click(object sender, EventArgs e)
        {
            this.undoManager.Lock = true;
            this.imageComponent.ImageWrapper.Bitmap = this.undoManager.DiscardAll();
            this.undoManager.Lock = false;
        }

        private void menuItem_Edit_Cut_Click(object sender, EventArgs e)
        {

        }

        private void menuItem_Edit_Copy_Click(object sender, EventArgs e)
        {
            Clipboard.SetDataObject(this.imageComponent.ImageWrapper.Bitmap, true);
        }

        private void menuItem_Edit_Paste_Click(object sender, EventArgs e)
        {

        }
        #endregion

        #region View Menu Actions

        private void menuItem_View_Thumbnail_Click(object sender, EventArgs e)
        {
            ThumbnailForm.ShowView(this, this.imageComponent.ImageWrapper);
        }

        #endregion

        #region Image Menu Actions

        private void menuItem_Image_FlipRotate_Click(object sender, EventArgs e)
        {
            var window = new FlipRotateWindow(this.imageComponent.ImageWrapper);
            window.ShowDialog(this);
        }

        private void menuItem_Image_StretchResize_Click(object sender, EventArgs e)
        {
            var window = new StretchResizeWindow(this.imageComponent.ImageWrapper);
            window.ShowDialog(this);
        }

        private void menuItem_Image_Brightness_Click(object sender, EventArgs e)
        {
            var window = new ThresholdWindow(this.imageComponent.ImageWrapper, new FilterBrightness());
            window.ShowDialog(this);
        }

        private void menuItem_Image_Contrast_Click(object sender, EventArgs e)
        {
            var window = new ThresholdWindow(this.imageComponent.ImageWrapper, new FilterContrast());
            window.ShowDialog(this);
        }

        private void menuItem_Image_Gamma_Click(object sender, EventArgs e)
        {
            var window = new ThresholdWindow(this.imageComponent.ImageWrapper, new FilterGamma());
            window.ShowDialog(this);
        }

        #endregion

        #region Colors Menu Actions

        private void menuItem_Colors_BlackWhite_Click(object sender, EventArgs e)
        {
            var window = new ThresholdWindow(this.imageComponent.ImageWrapper, new FilterBlackWhite());
            window.ShowDialog(this);
        }

        private void menuItem_Colors_Invert_Click(object sender, EventArgs e)
        {
            var window = new BasicFilterWindow(this.imageComponent.ImageWrapper, new FilterInvert());
            window.ShowDialog(this);
        }

        private void menuItem_Colors_Grayscale_Click(object sender, EventArgs e)
        {
            var window = new BasicFilterWindow(this.imageComponent.ImageWrapper, new FilterGrayAdjusted());
            window.ShowDialog(this);
        }

        private void menuItem_Colors_Redscale_Click(object sender, EventArgs e)
        {
            var window = new BasicFilterWindow(this.imageComponent.ImageWrapper, new FilterRed());
            window.ShowDialog(this);
        }

        private void menuItem_Colors_Greenscale_Click(object sender, EventArgs e)
        {
            var window = new BasicFilterWindow(this.imageComponent.ImageWrapper, new FilterGreen());
            window.ShowDialog(this);
        }

        private void menuItem_Colors_Bluescale_Click(object sender, EventArgs e)
        {
            var window = new BasicFilterWindow(this.imageComponent.ImageWrapper, new FilterBlue());
            window.ShowDialog(this);
        }
        #endregion

        #region Styles Menu Actions

        private void menuItem_Styles_ColoredPencils_Click(object sender, EventArgs e)
        {
            var window = new ThresholdWindow(this.imageComponent.ImageWrapper, new FilterColoredPencil());
            window.ShowDialog(this);
        }

        private void menuItem_Styles_FrostedGlass_Click(object sender, EventArgs e)
        {
            var window = new ThresholdWindow(this.imageComponent.ImageWrapper, new FilterFrostedGlass());
            window.ShowDialog(this);
        }

        private void menuItem_Styles_OilPainting_Click(object sender, EventArgs e)
        {
            var window = new ThresholdWindow(this.imageComponent.ImageWrapper, new FilterOil());
            window.ShowDialog(this);
        }

        private void menuItem_Styles_Pencil_Click(object sender, EventArgs e)
        {
            var window = new ThresholdWindow(this.imageComponent.ImageWrapper, new FilterPencil());
            window.ShowDialog(this);
        }

        #endregion

        #region Effects Menu Actions

        private void menuItem_Effects_Blur_Click(object sender, EventArgs e)
        {
            var window = new BasicFilterWindow(this.imageComponent.ImageWrapper, new FilterMatrixBlur());
            window.ShowDialog(this);
        }

        private void menuItem_Effects_Sharpen_Click(object sender, EventArgs e)
        {
            var window = new BasicFilterWindow(this.imageComponent.ImageWrapper, new FilterMatrixSharpen());
            window.ShowDialog(this);
        }

        private void menuItem_Effects_Emboss_Click(object sender, EventArgs e)
        {
            var window = new OffsetWindow(this.imageComponent.ImageWrapper, new FilterMatrixEmboss());
            window.ShowDialog(this);
        }

        private void menuItem_Effects_EdgeEnhance_Click(object sender, EventArgs e)
        {
        }

        private void menuItem_Effects_ED_BW_Click(object sender, EventArgs e)
        {
            var window = new ThresholdWindow(this.imageComponent.ImageWrapper, new FilterMatrixBW());
            window.ShowDialog(this);
        }

        private void menuItemEffects_ED_Kirsh_Click(object sender, EventArgs e)
        {
            var window = new BasicFilterWindow(this.imageComponent.ImageWrapper, new FilterMatrixKirshED());
            window.ShowDialog(this);
        }

        private void menuItem_Effects_ED_Prewitt_Click(object sender, EventArgs e)
        {
            var window = new BasicFilterWindow(this.imageComponent.ImageWrapper, new FilterMatrixPrewittED());
            window.ShowDialog(this);
        }

        private void menuItem_Effects_ED_Sobel_Click(object sender, EventArgs e)
        {
            var window = new BasicFilterWindow(this.imageComponent.ImageWrapper, new FilterMatrixSobelED());
            window.ShowDialog(this);
        }

        private void menuItem_Effects_Dilate_Click(object sender, EventArgs e)
        {
            //TODO:
        }

        private void menuItem_Effects_Median_Click(object sender, EventArgs e)
        {
            //TODO:
        }

        private void menuItem_Effects_Erosion_Click(object sender, EventArgs e)
        {
            //TODO:
        }

        private void menuItem_Effects_Posterize_Click(object sender, EventArgs e)
        {
            var window = new ThresholdWindow(this.imageComponent.ImageWrapper, new FilterPosterize());
            window.ShowDialog(this);
        }

        private void menuItem_Effects_Solarize_Click(object sender, EventArgs e)
        {
            var window = new ThresholdWindow(this.imageComponent.ImageWrapper, new FilterSolarize());
            window.ShowDialog(this);
        }

        private void menuItem_Effects_Mean_Click(object sender, EventArgs e)
        {
            var window = new BasicFilterWindow(this.imageComponent.ImageWrapper, new FilterMean());
            window.ShowDialog(this);
        }

        #endregion

        private void libraryPane_OpenLibrary(object sender, OpenLibraryEventArgs e)
        {
            this.libraryComponent.Library = e.Item;
            this.SwitchView(false, ViewMode.Library);
        }

        private void libraryComponent_OpenImage(object sender, ImageOpenEventArgs e)
        {
            this.OpenImage(e.Item.Item.Path);
        }

        private void menuItem_Help_About_Click(object sender, EventArgs e)
        {
            AboutForm.Instance.ShowDialog(this);
        }
    }
}