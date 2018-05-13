using System;

namespace Ima.ImageOps.Filters
{
    public class FilterInvert : FilterBase
    {
        public FilterInvert() : base("Invert") { this.Direct = true; }
        public unsafe override void Filter(PixelData* pPixel)
        {
            pPixel->red = (byte)(255 - pPixel->red);
            pPixel->green = (byte)(255 - pPixel->green);
            pPixel->blue = (byte)(255 - pPixel->blue);
        }
    }

    public class FilterRed : FilterBase
    {
        public FilterRed() : base("Red") { this.Direct = true; }

        public unsafe override void Filter(PixelData* pPixel)
        {
            pPixel->red = (byte)(0.299 * pPixel->red + 0.587 * pPixel->green + 0.114 * pPixel->blue);
            pPixel->green = 0;
            pPixel->blue = 0;
        }
    }

    public class FilterGreen : FilterBase
    {
        public FilterGreen() : base("Green") { this.Direct = true; }

        public unsafe override void Filter(PixelData* pPixel)
        {
            pPixel->green = (byte)(0.299 * pPixel->red + 0.587 * pPixel->green + 0.114 * pPixel->blue);
            pPixel->red = 0;
            pPixel->blue = 0;
        }
    }

    public class FilterBlue : FilterBase
    {
        public FilterBlue() : base("Blue") { this.Direct = true; }

        public unsafe override void Filter(PixelData* pPixel)
        {
            pPixel->blue = (byte)(0.299 * pPixel->red + 0.587 * pPixel->green + 0.114 * pPixel->blue);
            pPixel->red = 0;
            pPixel->green = 0;
        }
    }

    public class FilterSepia : FilterBase
    {
        public FilterSepia() : base("Sepia") { this.Direct = true; }

        public unsafe override void Filter(PixelData* pPixel)
        {
            double gray = (0.299 * pPixel->red + 0.587 * pPixel->green + 0.114 * pPixel->blue);
            pPixel->red = (byte)Math.Min(255, gray + 40);
            pPixel->green = (byte)Math.Min(255, gray + 26);
            pPixel->blue = (byte)gray;
        }
    }

    public class FilterGrayAdjusted : FilterBase
    {
        public FilterGrayAdjusted() : base("GrayScale") { this.Direct = true; }
        public unsafe override void Filter(PixelData* pPixel)
        {
            byte gray = (byte)(0.299 * pPixel->red + 0.587 * pPixel->green + 0.114 * pPixel->blue);
            pPixel->red = gray;
            pPixel->green = gray;
            pPixel->blue = gray;
        }
    }
    public class FilterRedChannel : FilterBase
    {
        public FilterRedChannel() : base("Red Channel") { this.Direct = true; }

        public unsafe override void Filter(PixelData* pPixel)
        {
            pPixel->green = 0;
            pPixel->blue = 0;
        }
    }

    public class FilterGreenChannel : FilterBase
    {
        public FilterGreenChannel() : base("Green Channel") { this.Direct = true; }

        public unsafe override void Filter(PixelData* pPixel)
        {
            pPixel->red = 0;
            pPixel->blue = 0;
        }
    }

    public class FilterBlueChannel : FilterBase
    {
        public FilterBlueChannel() : base("Blue Channel") { this.Direct = true; }

        public unsafe override void Filter(PixelData* pPixel)
        {
            pPixel->red = 0;
            pPixel->green = 0;
        }
    }

    public class FilterMean : FilterBase
    {
        /// <summary>
        /// Working array
        /// </summary>
        byte[] colors = new byte[5];

        /// <summary>
        /// 
        /// </summary>
        public FilterMean() : base("Mean")
        {
            this.Direct = false;
            this.Border = 1;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="getPixel"></param>
        /// <param name="pPixel"></param>
        /// <param name="x0"></param>
        /// <param name="y0"></param>
        /// <param name="x1"></param>
        /// <param name="y1"></param>
        public unsafe override void Filter(int x, int y, PixelGet getPixel, PixelData* pPixel, int x0, int y0, int x1, int y1)
        {
            PixelData* pPixelClone1, pPixelClone2, pPixelClone3, pPixelClone4;
            pPixelClone1 = getPixel(x, y + 1);
            pPixelClone2 = getPixel(x - 1, y);
            pPixelClone3 = getPixel(x + 1, y);
            pPixelClone4 = getPixel(x, y - 1);

            colors[0] = pPixelClone1->red;
            colors[1] = pPixelClone2->red;
            colors[2] = pPixelClone3->red;
            colors[3] = pPixelClone4->red;
            colors[4] = pPixel->red;
            Array.Sort(colors);
            pPixel->red = colors[2];

            colors[0] = pPixelClone1->green;
            colors[1] = pPixelClone2->green;
            colors[2] = pPixelClone3->green;
            colors[3] = pPixelClone4->green;
            colors[4] = pPixel->green;
            Array.Sort(colors);
            pPixel->green = colors[2];

            colors[0] = pPixelClone1->blue;
            colors[1] = pPixelClone2->blue;
            colors[2] = pPixelClone3->blue;
            colors[3] = pPixelClone4->blue;
            colors[4] = pPixel->blue;
            Array.Sort(colors);
            pPixel->blue = colors[2];

        }
    }

    public class FilterBlackWhite : ThresholdFilterBase
    {
        public FilterBlackWhite() : base("Black/White")
        {
            this.Maximum = 255;
            this.Minimum = 0;
            this.Threshold = 127;
            this.Property = "B/W Threshold";
            this.Direct = true;
        }

        public unsafe override void Filter(PixelData* pPixel)
        {
            int distance = (255 - pPixel->red) * (255 - pPixel->red) + (255 - pPixel->green) * (255 - pPixel->green) + (255 - pPixel->blue) * (255 - pPixel->blue);
            pPixel->red = pPixel->green = pPixel->blue = (distance <= (this.Threshold * this.Threshold)) ? (byte)255 : (byte)0;
        }
    }

    public class FilterBrightness : ThresholdFilterBase
    {
        public FilterBrightness() : base("Brightness")
        {
            this.Maximum = 255;
            this.Minimum = -255;
            this.Property = "Brightness";
            this.Direct = true;
        }

        public unsafe override void Filter(PixelData* pPixel)
        {
            int tmp;
            tmp = pPixel->red + this.Threshold;
            pPixel->red = (byte)((tmp >= 0) ? ((tmp <= 255) ? tmp : 255) : 0);

            tmp = pPixel->green + this.Threshold;
            pPixel->green = (byte)((tmp >= 0) ? ((tmp <= 255) ? tmp : 255) : 0);

            tmp = pPixel->blue + this.Threshold;
            pPixel->blue = (byte)((tmp >= 0) ? ((tmp <= 255) ? tmp : 255) : 0);
        }
    }

    public class FilterContrast : ThresholdFilterBase
    {
        double fpContrast;
        public FilterContrast() : base("Contrast")
        {
            fpContrast = 0.0;
            this.Maximum = 100;
            this.Minimum = -100;
            this.Property = "Contrast";
            this.Direct = true;
        }

        /// <summary>
        /// 
        /// </summary>
        public override int Threshold
        {
            get
            {
                return base.Threshold;
            }

            set
            {
                base.Threshold = value;
                this.fpContrast = (this.Threshold + 100.0) / 100.0;
                this.fpContrast *= this.fpContrast;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="pPixel"></param>
        public unsafe override void Filter(PixelData* pPixel)
        {
            double pixel;

            /* red */
            pixel = pPixel->red * 1 / 255.0;
            pixel -= 0.5;
            pixel *= this.fpContrast;
            pixel += 0.5;
            pixel *= 255.0;
            pPixel->red = (byte)((pixel >= 0) ? ((pixel <= 255) ? pixel : 255) : 0);

            /* green */
            pixel = pPixel->green * 1 / 255.0;
            pixel -= 0.5;
            pixel *= this.fpContrast;
            pixel += 0.5;
            pixel *= 255.0;
            pPixel->green = (byte)((pixel >= 0) ? ((pixel <= 255) ? pixel : 255) : 0);

            /* blue */
            pixel = pPixel->blue * 1 / 255.0;
            pixel -= 0.5;
            pixel *= this.fpContrast;
            pixel += 0.5;
            pixel *= 255.0;
            pPixel->blue = (byte)((pixel >= 0) ? ((pixel <= 255) ? pixel : 255) : 0);
        }
    }

    public class FilterGamma : ThresholdFilterBase
    {
        private byte[] redGamma = new byte[256];
        private byte[] greenGamma = new byte[256];
        private byte[] blueGamma = new byte[256];

        public FilterGamma()
            : base("Gamma")
        {
            this.Maximum = 100;
            this.Minimum = 1;
            this.Threshold = 20;
            this.Property = "Gamma correction";
            this.Direct = true;
        }

        /// <summary>
        /// 
        /// </summary>
        public override int Threshold
        {
            get
            {
                return base.Threshold;
            }

            set
            {
                base.Threshold = value;

                if (this.Threshold == 0)
                {
                    for (int i = 0; i < 256; ++i)
                    {
                        redGamma[i] = greenGamma[i] = blueGamma[i] = (byte)i;
                    }
                }
                else
                {
                    ///http://www.teamten.com/lawrence/graphics/gamma/
                    double fpGamma = this.Threshold / 20.0;
                    double invGamma = 1.0 / fpGamma;
                    for (int i = 0; i < 256; ++i)
                    {
                        redGamma[i] = greenGamma[i] = blueGamma[i] =
                            (byte)Math.Min(255.0,
                                           (255.0 * Math.Pow(i / 255.0, invGamma)) + 0.5);
                    }
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="pPixel"></param>
        public unsafe override void Filter(PixelData* pPixel)
        {
            pPixel->red = redGamma[pPixel->red];
            pPixel->green = redGamma[pPixel->green];
            pPixel->blue = redGamma[pPixel->blue];
        }
    }

    public class FilterNoise : ThresholdFilterBase
    {
        Random rand;
        byte red = 255, green = 255, blue = 255;

        public FilterNoise() : base("Random Noise")
        {
            this.Direct = true;
            rand = new Random();
            this.Minimum = 0;
            this.Maximum = 100;
            this.Property = "Noise Density";
        }

        public FilterNoise(int seed) : base("Random Noise")
        {
            this.Direct = true;
            rand = new Random(seed);
            this.Minimum = 0;
            this.Maximum = 100;
            this.Property = "Noise Density";
        }

        public void setNoiseColor(byte R, byte G, byte B)
        {
            this.red = R;
            this.green = G;
            this.blue = B;
        }

        public unsafe override void Filter(PixelData* pPixel)
        {
            if (rand.Next(this.Minimum, this.Maximum) <= this.Threshold)
            {
                pPixel->red = red;
                pPixel->green = green;
                pPixel->blue = blue;
            }
        }
    }

    public class FilterPosterize : ThresholdFilterBase
    {
        byte[] lookup = new byte[256];
        public FilterPosterize() : base("Posterize")
        {
            this.Maximum = 8;
            this.Minimum = 0;
            this.Property = "Number of colors (2^n)";
            this.Direct = true;
            this.Threshold = 4;
        }

        public override int Threshold
        {
            get
            {
                return base.Threshold;
            }

            set
            {
                base.Threshold = value;
                int pow = (int)Math.Pow(2, this.Threshold);
                for (int i = 0; i < this.lookup.Length; i++)
                {
                    this.lookup[i] = (byte)(i - (i % pow));
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="pPixel"></param>
        public unsafe override void Filter(PixelData* pPixel)
        {
            pPixel->red = this.lookup[pPixel->red];
            pPixel->green = this.lookup[pPixel->green];
            pPixel->blue = this.lookup[pPixel->blue];
        }
    }

    public class FilterSolarize : ThresholdFilterBase
    {
        int pow;
        public FilterSolarize() : base("Solarize")
        {
            this.Maximum = 8;
            this.Minimum = 0;
            this.Property = "Number of colors (2^n)";
            this.Direct = true;
            this.Threshold = 8;
        }

        /// <summary>
        /// 
        /// </summary>
        public override int Threshold
        {
            get
            {
                return base.Threshold;
            }

            set
            {
                base.Threshold = value;
                this.pow = (int)Math.Pow(2, this.Threshold);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="pPixel"></param>
        public unsafe override void Filter(PixelData* pPixel)
        {
            pPixel->red = (byte)((pPixel->red > this.pow) ? (255 - pPixel->red) : pPixel->red);
            pPixel->green = (byte)((pPixel->green > this.pow) ? (255 - pPixel->green) : pPixel->green);
            pPixel->blue = (byte)((pPixel->blue > this.pow) ? (255 - pPixel->blue) : pPixel->blue);
        }
    }
}
