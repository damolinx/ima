using System;
using System.IO;
using System.Net;
using System.Drawing;
using System.Windows.Forms;
using Microsoft.Win32;

namespace Ima
{
    /// <summary>
    /// 
    /// </summary>
    public class WallpaperDialog : Ima.Controls.ToolForm
    {
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.RadioButton rbtnCentered;
        private System.Windows.Forms.RadioButton rBtnTiled;
        private System.Windows.Forms.RadioButton rBtnStretched;
        private System.Windows.Forms.Label lblStretched;
        private System.Windows.Forms.Label lblTiled;
        private System.Windows.Forms.ImageList imgLstWallpaper;
        private System.ComponentModel.IContainer components;
        private System.Windows.Forms.Label lblCentered;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.RadioButton rBtnCenteredAdjusted;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.GroupBox groupBoxStyle;
        private System.Windows.Forms.PictureBox pictureBox;

        /// <summary>
        /// 
        /// </summary>
        private LibraryItem item;

        /// <summary>
        /// 
        /// </summary>
        public WallpaperDialog() : this(null)
        {

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="path"></param>
        public WallpaperDialog(LibraryItem item)
        {
            this.item = item;
            InitializeComponent();
        }

        /// <summary>
        /// 
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(WallpaperDialog));
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnOK = new System.Windows.Forms.Button();
            this.groupBoxStyle = new System.Windows.Forms.GroupBox();
            this.label1 = new System.Windows.Forms.Label();
            this.imgLstWallpaper = new System.Windows.Forms.ImageList(this.components);
            this.rBtnCenteredAdjusted = new System.Windows.Forms.RadioButton();
            this.lblTiled = new System.Windows.Forms.Label();
            this.lblStretched = new System.Windows.Forms.Label();
            this.lblCentered = new System.Windows.Forms.Label();
            this.rBtnStretched = new System.Windows.Forms.RadioButton();
            this.rBtnTiled = new System.Windows.Forms.RadioButton();
            this.rbtnCentered = new System.Windows.Forms.RadioButton();
            this.pictureBox = new System.Windows.Forms.PictureBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.groupBoxStyle.SuspendLayout();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnCancel
            // 
            this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(290, 362);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.TabIndex = 0;
            this.btnCancel.Text = "&Cancel";
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnOK
            // 
            this.btnOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnOK.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnOK.Location = new System.Drawing.Point(202, 362);
            this.btnOK.Name = "btnOK";
            this.btnOK.TabIndex = 1;
            this.btnOK.Text = "&OK";
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // groupBoxStyle
            // 
            this.groupBoxStyle.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
                | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBoxStyle.Controls.Add(this.label1);
            this.groupBoxStyle.Controls.Add(this.rBtnCenteredAdjusted);
            this.groupBoxStyle.Controls.Add(this.lblTiled);
            this.groupBoxStyle.Controls.Add(this.lblStretched);
            this.groupBoxStyle.Controls.Add(this.lblCentered);
            this.groupBoxStyle.Controls.Add(this.rBtnStretched);
            this.groupBoxStyle.Controls.Add(this.rBtnTiled);
            this.groupBoxStyle.Controls.Add(this.rbtnCentered);
            this.groupBoxStyle.Location = new System.Drawing.Point(8, 234);
            this.groupBoxStyle.Name = "groupBoxStyle";
            this.groupBoxStyle.Size = new System.Drawing.Size(362, 112);
            this.groupBoxStyle.TabIndex = 2;
            this.groupBoxStyle.TabStop = false;
            this.groupBoxStyle.Text = "Style";
            // 
            // label1
            // 
            this.label1.ImageIndex = 0;
            this.label1.ImageList = this.imgLstWallpaper;
            this.label1.Location = new System.Drawing.Point(16, 72);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(32, 24);
            this.label1.TabIndex = 8;
            // 
            // imgLstWallpaper
            // 
            this.imgLstWallpaper.ColorDepth = System.Windows.Forms.ColorDepth.Depth32Bit;
            this.imgLstWallpaper.ImageSize = new System.Drawing.Size(32, 32);
            this.imgLstWallpaper.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imgLstWallpaper.ImageStream")));
            this.imgLstWallpaper.TransparentColor = System.Drawing.Color.White;
            // 
            // rBtnCenteredAdjusted
            // 
            this.rBtnCenteredAdjusted.Location = new System.Drawing.Point(64, 72);
            this.rBtnCenteredAdjusted.Name = "rBtnCenteredAdjusted";
            this.rBtnCenteredAdjusted.Size = new System.Drawing.Size(152, 24);
            this.rBtnCenteredAdjusted.TabIndex = 7;
            this.rBtnCenteredAdjusted.Text = "Centered (&fit to screen)";
            // 
            // lblTiled
            // 
            this.lblTiled.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.lblTiled.ImageIndex = 2;
            this.lblTiled.ImageList = this.imgLstWallpaper;
            this.lblTiled.Location = new System.Drawing.Point(225, 72);
            this.lblTiled.Name = "lblTiled";
            this.lblTiled.Size = new System.Drawing.Size(32, 24);
            this.lblTiled.TabIndex = 6;
            // 
            // lblStretched
            // 
            this.lblStretched.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.lblStretched.ImageIndex = 1;
            this.lblStretched.ImageList = this.imgLstWallpaper;
            this.lblStretched.Location = new System.Drawing.Point(225, 24);
            this.lblStretched.Name = "lblStretched";
            this.lblStretched.Size = new System.Drawing.Size(32, 24);
            this.lblStretched.TabIndex = 5;
            // 
            // lblCentered
            // 
            this.lblCentered.ImageIndex = 0;
            this.lblCentered.ImageList = this.imgLstWallpaper;
            this.lblCentered.Location = new System.Drawing.Point(16, 24);
            this.lblCentered.Name = "lblCentered";
            this.lblCentered.Size = new System.Drawing.Size(32, 24);
            this.lblCentered.TabIndex = 4;
            // 
            // rBtnStretched
            // 
            this.rBtnStretched.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.rBtnStretched.Location = new System.Drawing.Point(273, 24);
            this.rBtnStretched.Name = "rBtnStretched";
            this.rBtnStretched.Size = new System.Drawing.Size(72, 24);
            this.rBtnStretched.TabIndex = 3;
            this.rBtnStretched.Text = "&Stretched";
            // 
            // rBtnTiled
            // 
            this.rBtnTiled.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.rBtnTiled.Location = new System.Drawing.Point(273, 72);
            this.rBtnTiled.Name = "rBtnTiled";
            this.rBtnTiled.Size = new System.Drawing.Size(56, 24);
            this.rBtnTiled.TabIndex = 2;
            this.rBtnTiled.Text = "&Tiled";
            // 
            // rbtnCentered
            // 
            this.rbtnCentered.Checked = true;
            this.rbtnCentered.Location = new System.Drawing.Point(64, 24);
            this.rbtnCentered.Name = "rbtnCentered";
            this.rbtnCentered.Size = new System.Drawing.Size(152, 24);
            this.rbtnCentered.TabIndex = 0;
            this.rbtnCentered.TabStop = true;
            this.rbtnCentered.Text = "&Centered";
            // 
            // pictureBox
            // 
            this.pictureBox.BackColor = System.Drawing.SystemColors.Desktop;
            this.pictureBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pictureBox.Location = new System.Drawing.Point(8, 8);
            this.pictureBox.Name = "pictureBox";
            this.pictureBox.Size = new System.Drawing.Size(240, 180);
            this.pictureBox.TabIndex = 3;
            this.pictureBox.TabStop = false;
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.Gray;
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.panel2);
            this.panel1.Controls.Add(this.pictureBox);
            this.panel1.Enabled = false;
            this.panel1.Location = new System.Drawing.Point(61, 16);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(256, 208);
            this.panel1.TabIndex = 4;
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.Lime;
            this.panel2.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panel2.Enabled = false;
            this.panel2.Location = new System.Drawing.Point(216, 192);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(16, 8);
            this.panel2.TabIndex = 4;
            // 
            // WallpaperDialog
            // 
            this.AcceptButton = this.btnOK;
            this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(378, 400);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.groupBoxStyle);
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.btnCancel);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.MaximizeBox = false;
            this.Name = "WallpaperDialog";
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.Text = "Wallpaper";
            this.groupBoxStyle.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnOK_Click(object sender, System.EventArgs e)
        {
            if (this.item == null)
                return;

            Wallpaper.Style style = (rbtnCentered.Checked) ? Wallpaper.Style.Centered
                : (rBtnStretched.Checked) ? Wallpaper.Style.Stretched : Wallpaper.Style.Tiled;

            /* TODO		if (style == Wallpaper.Style.Stretched &&
                            SystemInformation.PrimaryMonitorSize.Width/SystemInformation.PrimaryMonitorSize.Height != this.item..Width / this.imageSize.Height &&
                            MessageBox.Show(this, "Image selected has a different shape than your Desktop, this means image will look deformed. Do you want to set it as wallpaper?\nRecommendation: Use 'Centered' so image is not deformed", Configuration.Instance.GetProperty(Configuration.APPLICATION_NAME, string.Empty).ToString(), MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                        {
                            return;
                        } */

            Cursor cursor = Cursor.Current;
            Cursor.Current = Cursors.WaitCursor;
            try
            {
                Wallpaper.Set(new Uri(this.item.Path), style);
            }
            catch (Exception ex)
            {
                Console.Out.WriteLine(ex.Message + "\n" + ex.StackTrace);
            }
            finally
            {
                Cursor.Current = cursor;
            }
            this.Close();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnCancel_Click(object sender, System.EventArgs e)
        {
            this.Close();
        }
    }

    /// <summary>
    /// 
    /// </summary>
    public sealed class Wallpaper
    {

        /// <summary>
        /// 
        /// </summary>
        public enum Style : int
        {
            Tiled,
            Centered,
            Stretched
        }

        /// <summary>
        /// 
        /// </summary>
        private Wallpaper() { }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="uri"></param>
        /// <param name="style"></param>
        public static void Set(Uri uri, Style style)
        {
            System.IO.Stream s = new WebClient().OpenRead(uri.ToString());

            System.Drawing.Image img = System.Drawing.Image.FromStream(s);
            string tempPath = Path.Combine(Configuration.Instance.Path_Library_Base, "ima.bmp");
            img.Save(tempPath, System.Drawing.Imaging.ImageFormat.Bmp);

            RegistryKey key = Registry.CurrentUser.OpenSubKey(@"Control Panel\Desktop", true);
            if (style == Style.Stretched)
            {
                key.SetValue(@"WallpaperStyle", 2.ToString());
                key.SetValue(@"TileWallpaper", 0.ToString());
            }

            if (style == Style.Centered)
            {
                key.SetValue(@"WallpaperStyle", 1.ToString());
                key.SetValue(@"TileWallpaper", 0.ToString());
            }

            if (style == Style.Tiled)
            {
                key.SetValue(@"WallpaperStyle", 1.ToString());
                key.SetValue(@"TileWallpaper", 1.ToString());
            }

            NativeMethods.SystemParametersInfo(NativeMethods.SPI_SETDESKWALLPAPER,
                0,
                tempPath,
                NativeMethods.SPIF_UPDATEINIFILE | NativeMethods.SPIF_SENDWININICHANGE);
        }
    }
}
