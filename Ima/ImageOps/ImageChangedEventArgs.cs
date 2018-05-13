using System;
using System.Drawing;

namespace Ima.ImageOps
{
    /// <summary>
    /// Event Class
    /// </summary>
    public class ImageChangedEventArgs : EventArgs
    {
        public ImageChangedEventArgs(Bitmap old)
        {
            this.Bitmap = old;
        }

        public ImageChangedEventArgs(string name, Bitmap old)
        {
            this.Name = name;
            this.Bitmap = old;
        }

        /// <summary>
        /// Previous Bitmap
        /// </summary>
        public Bitmap Bitmap
        {
            get; private set;
        }

        /// <summary>
        /// Action name
        /// </summary>
        public string Name
        {
            get; private set;
        }
    }
}
