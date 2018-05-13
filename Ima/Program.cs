using Ima.Library;
using System;
using System.Windows.Forms;

namespace Ima
{
    static class Program
    {
        public const string APPLICATION_NAME = "Image Manipulation Application";
        public const string APPLICATION_VERSION = "1.0";
        public const string APPLICATION_IMAGE_SUPPORTED = "*.bmp;*.emf;*.exif;*.gif;*.ico;*.jpg;*.jpeg;*.png;*.tif;*.tiff;*.wmf";

        [STAThread]
        public static void Main(params string[] args)
        {
            var libraryManager = new LibraryManager();
            libraryManager.LoadDefaults();
            libraryManager.LoadUserItems();

            var mainForm = new MainForm
            {
                LibraryManager = libraryManager
            };
            mainForm.FormClosing += (sender, e) =>
            {
                Properties.Settings.Default.Save();
            };
            Application.EnableVisualStyles();
            Application.Run(mainForm);
        }
    }
}
