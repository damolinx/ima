using System;

namespace Ima.ImageOps.Filters
{
    public class FilterPencil : ThresholdFilterBase
    {
        public FilterPencil()
            : base("Pencil", false)
        {
            this.Maximum = 100;
            this.Minimum = 0;
            this.PropertyName = "Pen Strenght";
            this.Border = 1;
            this.Threshold = 50;
        }

        public unsafe override void Filter(int x, int y, PixelGet getPixel, PixelData* pPixel, int x0, int y0, int x1, int y1)
        {
            var ratio = this.Threshold / 100.0;
            var grayscaled = 0.299 * pPixel->red + 0.587 * pPixel->green + 0.114 * pPixel->blue;
            var max_diff = 0.0;
            var max_temp = 0.0;

            var pPixels = new[]
            {
                getPixel(x - 1, y + 1),
                getPixel(x, y + 1),
                getPixel(x + 1, y + 1),
                getPixel(x - 1, y),
                getPixel(x + 1, y),
                getPixel(x - 1, y - 1),
                getPixel(x, y - 1),
                getPixel(x + 1, y - 1)
            };

            foreach (PixelData* pCurrent in pPixels)
            {
                max_temp = Math.Abs((0.299 * pCurrent->red + 0.587 * pCurrent->green + 0.114 * pCurrent->blue) - grayscaled);
                if (max_temp > max_diff)
                {
                    max_diff = max_temp;
                }
            }
            var mask = 1.0 / (max_diff / Math.Sqrt(grayscaled + 1.0) / 3.0 + 1.0);
            pPixel->red = pPixel->green = pPixel->blue = (byte)(grayscaled + ratio * ((255 - grayscaled) * mask - max_diff * grayscaled / 100.0));
        }
    }

    public class FilterColoredPencil : ThresholdFilterBase
    {
        public FilterColoredPencil()
            : base("Colored Pencil", false)
        {
            this.Maximum = 100;
            this.Minimum = 0;
            this.PropertyName = "Pen Strenght";
            this.Border = 1;
            this.Threshold = 50;
        }

        public unsafe override void Filter(int x, int y, PixelGet getPixel, PixelData* pPixel, int x0, int y0, int x1, int y1)
        {
            double mask;
            int max_diff, max_temp;
            var ratio = this.Threshold / 100.0;

            var pPixels = new[]
            {
                getPixel(x - 1, y + 1),
                getPixel(x, y + 1),
                getPixel(x + 1, y + 1),
                getPixel(x - 1, y),
                getPixel(x + 1, y),
                getPixel(x - 1, y - 1),
                getPixel(x, y - 1),
                getPixel(x + 1, y - 1)
            };

            ///Red
            max_diff = 0;
            foreach (var pCurrent in pPixels)
            {
                max_temp = Math.Abs(pCurrent->red - pPixel->red);
                if (max_temp > max_diff)
                {
                    max_diff = max_temp;
                }
            }
            mask = 1.0 / (max_diff / Math.Sqrt(pPixel->red + 1.0) / 3.0 + 1.0);
            pPixel->red += (byte)(ratio * ((255 - pPixel->red) * mask - max_diff * pPixel->red / 100.0));

            ///Green
            max_diff = 0;
            foreach (var pCurrent in pPixels)
            {
                max_temp = Math.Abs(pCurrent->green - pPixel->green);
                if (max_temp > max_diff)
                {
                    max_diff = max_temp;
                }
            }
            mask = 1.0 / (max_diff / Math.Sqrt(pPixel->green + 1.0) / 3.0 + 1.0);
            pPixel->green += (byte)(ratio * ((255 - pPixel->green) * mask - max_diff * pPixel->green / 100.0));

            ///Blue
            max_diff = 0;
            foreach (var pCurrent in pPixels)
            {
                max_temp = Math.Abs(pCurrent->blue - pPixel->blue);
                if (max_temp > max_diff)
                {
                    max_diff = max_temp;
                }
            }
            mask = 1.0 / (max_diff / Math.Sqrt(pPixel->blue + 1.0) / 3.0 + 1.0);
            pPixel->blue += (byte)(ratio * ((255 - pPixel->blue) * mask - max_diff * pPixel->blue / 100.0));
        }
    }

    public class FilterOil : ThresholdFilterBase
    {
        public FilterOil() 
            : base("Oil", false)
        {
            this.Maximum = 5;
            this.Minimum = 1;
            this.PropertyName = "Brush Size";
            this.Border = 1;
            this.Threshold = 3;
        }

        public override int Threshold
        {
            get
            {
                return Border;
            }

            set
            {
                this.Border = value;
                base.Threshold = 2 * value + 1;
            }
        }

        private static unsafe int MostFrequent(PixelData*[] color)
        {
            int[] freq = new int[color.Length];

            foreach (var pPixel in color)
            {
                for (int i = 0; i < color.Length; i++)
                {
                    if (pPixel->blue == color[i]->blue && pPixel->green == color[i]->green && pPixel->red == color[i]->red)
                    {
                        freq[i]++;
                        break;
                    }
                }
            }

            int maxIndex = 0;

            for (int i = 1; i < color.Length; i++)
            {
                if (freq[i] > freq[maxIndex])
                {
                    maxIndex = i;
                }
            }

            return maxIndex;
        }


        public unsafe override void Filter(int x, int y, PixelGet getPixel, PixelData* pPixel, int x0, int y0, int x1, int y1)
        {
            var pixels = new PixelData*[this.Threshold * this.Threshold];

            int f1 = (int)(0.5 * (this.Threshold - 1));

            for (int i = 0; i < this.Threshold; i++)
            {
                for (int j = 0; j < this.Threshold; j++)
                {
                    int xi = x - f1 + i;
                    int yi = y - f1 + j;
                    pixels[i * this.Threshold + j] = getPixel((xi < 0) ? 0 : ((xi > x1) ? x1 : xi),
                        (yi < 0) ? 0 : ((yi > y1) ? y1 : yi));
                }
            }

            int index = MostFrequent(pixels);
            pPixel->red = pixels[index]->red;
            pPixel->green = pixels[index]->green;
            pPixel->blue = pixels[index]->blue;
        }
    }
}
