using System;

namespace Ima.ImageOps
{
    public abstract class FilterBase
    {
        protected FilterBase(string name, bool inPlace = true)
        {
            this.InPlace = inPlace;
            this.Name = name;
        }

        public int Border
        {
            get; protected set;
        }

        public string Description
        {
            get; protected set;
        }

        public bool InPlace
        {
            get; protected set;
        }

        public string Name
        {
            get; protected set;
        }

        /// <summary>
        /// In-place filter 
        /// </summary>
        public unsafe virtual void Filter(PixelData* pPixel)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Buffered filter 
        /// </summary>
        public unsafe virtual void Filter(int x, int y, PixelGet getPixel, PixelData* pPixel, int x0, int y0, int x1, int y1)
        {
            throw new NotImplementedException();
        }
    }
}
