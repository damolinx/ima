using Microsoft.Win32;
using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Net;
using System.Windows.Forms;

namespace Ima
{
    /// <summary>
    /// 
    /// </summary>
    public class WallpaperDialog : Ima.Controls.ToolForm
    {
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

        private LibraryItem item;

        public WallpaperDialog()
            : this(null)
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(WallpaperDialog));
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
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox)).BeginInit();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
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
            this.groupBoxStyle.Location = new System.Drawing.Point(14, 380);
            this.groupBoxStyle.Name = "groupBoxStyle";
            this.groupBoxStyle.Size = new System.Drawing.Size(908, 190);
            this.groupBoxStyle.TabIndex = 2;
            this.groupBoxStyle.TabStop = false;
            this.groupBoxStyle.Text = "Style";
            // 
            // label1
            // 
            this.label1.ImageIndex = 0;
            this.label1.ImageList = this.imgLstWallpaper;
            this.label1.Location = new System.Drawing.Point(29, 122);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(57, 40);
            this.label1.TabIndex = 8;
            // 
            // imgLstWallpaper
            // 
            this.imgLstWallpaper.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imgLstWallpaper.ImageStream")));
            this.imgLstWallpaper.TransparentColor = System.Drawing.Color.White;
            this.imgLstWallpaper.Images.SetKeyName(0, "");
            this.imgLstWallpaper.Images.SetKeyName(1, "");
            this.imgLstWallpaper.Images.SetKeyName(2, "");
            // 
            // rBtnCenteredAdjusted
            // 
            this.rBtnCenteredAdjusted.Location = new System.Drawing.Point(115, 122);
            this.rBtnCenteredAdjusted.Name = "rBtnCenteredAdjusted";
            this.rBtnCenteredAdjusted.Size = new System.Drawing.Size(274, 40);
            this.rBtnCenteredAdjusted.TabIndex = 7;
            this.rBtnCenteredAdjusted.Text = "Centered (&fit to screen)";
            // 
            // lblTiled
            // 
            this.lblTiled.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.lblTiled.ImageIndex = 2;
            this.lblTiled.ImageList = this.imgLstWallpaper;
            this.lblTiled.Location = new System.Drawing.Point(533, 122);
            this.lblTiled.Name = "lblTiled";
            this.lblTiled.Size = new System.Drawing.Size(58, 40);
            this.lblTiled.TabIndex = 6;
            // 
            // lblStretched
            // 
            this.lblStretched.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.lblStretched.ImageIndex = 1;
            this.lblStretched.ImageList = this.imgLstWallpaper;
            this.lblStretched.Location = new System.Drawing.Point(533, 41);
            this.lblStretched.Name = "lblStretched";
            this.lblStretched.Size = new System.Drawing.Size(58, 40);
            this.lblStretched.TabIndex = 5;
            // 
            // lblCentered
            // 
            this.lblCentered.ImageIndex = 0;
            this.lblCentered.ImageList = this.imgLstWallpaper;
            this.lblCentered.Location = new System.Drawing.Point(29, 41);
            this.lblCentered.Name = "lblCentered";
            this.lblCentered.Size = new System.Drawing.Size(57, 40);
            this.lblCentered.TabIndex = 4;
            // 
            // rBtnStretched
            // 
            this.rBtnStretched.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.rBtnStretched.Location = new System.Drawing.Point(619, 41);
            this.rBtnStretched.Name = "rBtnStretched";
            this.rBtnStretched.Size = new System.Drawing.Size(130, 40);
            this.rBtnStretched.TabIndex = 3;
            this.rBtnStretched.Text = "&Stretched";
            // 
            // rBtnTiled
            // 
            this.rBtnTiled.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.rBtnTiled.Location = new System.Drawing.Point(619, 122);
            this.rBtnTiled.Name = "rBtnTiled";
            this.rBtnTiled.Size = new System.Drawing.Size(101, 40);
            this.rBtnTiled.TabIndex = 2;
            this.rBtnTiled.Text = "&Tiled";
            // 
            // rbtnCentered
            // 
            this.rbtnCentered.Checked = true;
            this.rbtnCentered.Location = new System.Drawing.Point(115, 41);
            this.rbtnCentered.Name = "rbtnCentered";
            this.rbtnCentered.Size = new System.Drawing.Size(274, 40);
            this.rbtnCentered.TabIndex = 0;
            this.rbtnCentered.TabStop = true;
            this.rbtnCentered.Text = "&Centered";
            // 
            // pictureBox
            // 
            this.pictureBox.BackColor = System.Drawing.SystemColors.Desktop;
            this.pictureBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pictureBox.Location = new System.Drawing.Point(14, 14);
            this.pictureBox.Name = "pictureBox";
            this.pictureBox.Size = new System.Drawing.Size(432, 304);
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
            this.panel1.Location = new System.Drawing.Point(237, 12);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(461, 352);
            this.panel1.TabIndex = 4;
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.Lime;
            this.panel2.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panel2.Enabled = false;
            this.panel2.Location = new System.Drawing.Point(389, 325);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(29, 13);
            this.panel2.TabIndex = 4;
            // 
            // WallpaperDialog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(168F, 168F);
            this.ClientSize = new System.Drawing.Size(936, 661);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.groupBoxStyle);
            this.Name = "WallpaperDialog";
            this.Text = "Wallpaper";
            this.Controls.SetChildIndex(this.groupBoxStyle, 0);
            this.Controls.SetChildIndex(this.panel1, 0);
            this.groupBoxStyle.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox)).EndInit();
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        protected override void Apply()
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
    }

    /// <summary>
    /// 
    /// </summary>
    public sealed class Wallpaper
    {
        public enum Style
        {
            Tiled,
            Centered,
            Stretched
        }

        private Wallpaper() { }

        public static void Set(Uri uri, Style style)
        {
            using (var s = new WebClient().OpenRead(uri.ToString()))
            {
                var img = Image.FromStream(s);
                string tempPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Personal), "ima.bmp");
                img.Save(tempPath, ImageFormat.Bmp);

                var key = Registry.CurrentUser.OpenSubKey(@"Control Panel\Desktop", true);
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
}
