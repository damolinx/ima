using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ima.ImageOps.Filters
{
    public class HistogramRetriever
        : FilterBase
    {
        private int[] red = new int[256];
        private int[] green = new int[256];
        private int[] blue = new int[256];
        private int[] gray = new int[256];

        public HistogramRetriever()
            : base("Histogram Retriever")
        {
            this.Direct = true;
        }

        public unsafe override void Filter(PixelData* pPixel)
        {
            byte gray = (byte)(0.299 * pPixel->red + 0.587 * pPixel->green + 0.114 * pPixel->blue);
            this.red[pPixel->red]++;
            this.green[pPixel->green]++;
            this.blue[pPixel->blue]++;
            this.gray[gray]++;
        }
    }
}
