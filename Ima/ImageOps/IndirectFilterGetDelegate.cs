using System;
using System.Drawing;
using System.Drawing.Imaging;

namespace Ima.ImageOps
{
    /// <summary>
    /// IndirectFilterGetDelegate
    /// </summary>
    class IndirectFilterGetDelegate
    {
        private Bitmap bitmap;
        private BitmapData bitmapData;
        private int width;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="bitmap"></param>
        /// <param name="bitmapData"></param>
        public IndirectFilterGetDelegate(int width, Bitmap bitmap, BitmapData bitmapData)
        {
            this.bitmap = bitmap;
            this.bitmapData = bitmapData;
            this.width = width;
        }

        public unsafe PixelData* GetPixel(int x, int y)
        {
            return ((PixelData*)((Byte*)bitmapData.Scan0.ToPointer() + y * width + x * sizeof(PixelData)));
        }
    }
}
