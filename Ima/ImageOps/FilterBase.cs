namespace Ima.ImageOps
{
    /// <summary>
    /// 
    /// </summary>
    public abstract class FilterBase
    {
        public FilterBase(string name)
        {
            this.Direct = true;
            this.Name = name;
        }

        public unsafe virtual void Filter(PixelData* pPixel) { }

        public unsafe virtual void Filter(int x, int y, PixelGet getPixel, PixelData* pPixel, int x0, int y0, int x1, int y1) { }

        public string Name
        {
            get; protected set;
        }

        public string Description
        {
            get; protected set;
        }

        public bool Direct
        {
            get; protected set;
        }

        public int Border
        {
            get; protected set;
        }
    }
}
