using System;
using System.Drawing;

namespace Ima.ImageOps
{
    public class ImageChangedEventArgs : EventArgs
    {
        public ImageChangedEventArgs(Bitmap old)
            : this(null, old)
        {
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
            get;
        }

        /// <summary>
        /// Action name
        /// </summary>
        public string Name
        {
            get;
        }
    }
}
