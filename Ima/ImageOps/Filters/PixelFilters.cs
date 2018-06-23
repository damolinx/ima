using Ima.Utilites;
using System;

namespace Ima.ImageOps.Filters
{
    public class FilterInvert : FilterBase
    {
        public FilterInvert()
            : base("Invert")
        {
        }

        public unsafe override void Filter(PixelData* pPixel)
        {
            pPixel->red = (byte)(255 - pPixel->red);
            pPixel->green = (byte)(255 - pPixel->green);
            pPixel->blue = (byte)(255 - pPixel->blue);
        }
    }

    public class FilterRed : FilterBase
    {
        public FilterRed()
            : base("Red")
        {
        }

        public unsafe override void Filter(PixelData* pPixel)
        {
            pPixel->red = (byte)(0.299 * pPixel->red + 0.587 * pPixel->green + 0.114 * pPixel->blue);
            pPixel->green = 0;
            pPixel->blue = 0;
        }
    }

    public class FilterGreen : FilterBase
    {
        public FilterGreen()
            : base("Green")
        {
        }

        public unsafe override void Filter(PixelData* pPixel)
        {
            pPixel->green = (byte)(0.299 * pPixel->red + 0.587 * pPixel->green + 0.114 * pPixel->blue);
            pPixel->red = 0;
            pPixel->blue = 0;
        }
    }

    public class FilterBlue : FilterBase
    {
        public FilterBlue()
            : base("Blue")
        {
        }

        public unsafe override void Filter(PixelData* pPixel)
        {
            pPixel->blue = (byte)(0.299 * pPixel->red + 0.587 * pPixel->green + 0.114 * pPixel->blue);
            pPixel->red = 0;
            pPixel->green = 0;
        }
    }

    public class FilterSepia : FilterBase
    {
        public FilterSepia()
            : base("Sepia")
        {
        }

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
        public FilterGrayAdjusted()
            : base("GrayScale")
        {
        }

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
        public FilterRedChannel()
            : base("Red Channel")
        {
        }

        public unsafe override void Filter(PixelData* pPixel)
        {
            pPixel->green = 0;
            pPixel->blue = 0;
        }
    }

    public class FilterGreenChannel : FilterBase
    {
        public FilterGreenChannel()
            : base("Green Channel")
        {
        }

        public unsafe override void Filter(PixelData* pPixel)
        {
            pPixel->red = 0;
            pPixel->blue = 0;
        }
    }

    public class FilterBlueChannel : FilterBase
    {
        public FilterBlueChannel()
            : base("Blue Channel")
        {
        }

        public unsafe override void Filter(PixelData* pPixel)
        {
            pPixel->red = 0;
            pPixel->green = 0;
        }
    }

    public class FilterMean : FilterBase
    {
        public FilterMean()
            : base("Mean", inPlace: false)
        {
            this.Border = 1;
        }

        public unsafe override void Filter(int x, int y, PixelGet getPixel, PixelData* pPixel, int x0, int y0, int x1, int y1)
        {
            var pPixel1 = getPixel(x, y + 1);
            var pPixel2 = getPixel(x - 1, y);
            var pPixel3 = getPixel(x + 1, y);
            var pPixel4 = getPixel(x, y - 1);
            pPixel->red = (byte)MathEx.Clamp((pPixel1->red + pPixel2->red + pPixel3->red + pPixel4->red + pPixel->red) / 5.0, 0, 255);
            pPixel->green = (byte)MathEx.Clamp((pPixel1->green + pPixel2->green + pPixel3->green + pPixel4->green + pPixel->green) / 5.0, 0, 255);
            pPixel->blue = (byte)MathEx.Clamp((pPixel1->blue + pPixel2->blue + pPixel3->blue + pPixel4->blue + pPixel->blue) / 5.0, 0, 255);
        }
    }

    public class FilterBlackWhite : ThresholdFilterBase
    {
        private int _sqrThreshold;

        public FilterBlackWhite()
            : base("Black/White")
        {
            this.Maximum = 255;
            this.Minimum = 0;
            this.Threshold = 127;
            this.PropertyName = "B/W Threshold";
        }
        public override int Threshold
        {
            get => base.Threshold;
            set
            {
                base.Threshold = value;
                _sqrThreshold = base.Threshold * base.Threshold;
            }
        }

        public unsafe override void Filter(PixelData* pPixel)
        {
            int distance = (255 - pPixel->red) * (255 - pPixel->red) + (255 - pPixel->green) * (255 - pPixel->green) + (255 - pPixel->blue) * (255 - pPixel->blue);
            pPixel->red = pPixel->green = pPixel->blue = (distance <= _sqrThreshold) ? (byte)255 : (byte)0;
        }
    }

    public class FilterBrightness
        : ThresholdFilterBase
    {
        public FilterBrightness()
            : base("Brightness")
        {
            this.Maximum = 255;
            this.Minimum = -255;
            this.PropertyName = "Brightness";
        }

        public unsafe override void Filter(PixelData* pPixel)
        {
            pPixel->red = (byte)MathEx.Clamp(pPixel->red + this.Threshold, 0, 255);
            pPixel->green = (byte)MathEx.Clamp(pPixel->green + this.Threshold, 0, 255);
            pPixel->blue = (byte)MathEx.Clamp(pPixel->blue + this.Threshold, 0, 255);
        }
    }

    public class FilterContrast : ThresholdFilterBase
    {
        public FilterContrast()
            : base("Contrast")
        {
            this.Maximum = 100;
            this.Minimum = -100;
            this.PropertyName = "Contrast";
        }

        public unsafe override void Filter(PixelData* pPixel)
        {
            double fpContrast = (this.Threshold + 100.0) / 100.0;
            fpContrast *= fpContrast;

            pPixel->red = (byte)MathEx.Clamp(255 * ((fpContrast * ((pPixel->red * 1 / 255.0) - 0.5)) + 0.5), 0, 255);
            pPixel->green = (byte)MathEx.Clamp(255 * ((fpContrast * ((pPixel->green * 1 / 255.0) - 0.5)) + 0.5), 0, 255);
            pPixel->blue = (byte)MathEx.Clamp(255 * ((fpContrast * ((pPixel->blue * 1 / 255.0) - 0.5)) + 0.5), 0, 255);
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
            this.PropertyName = "Gamma correction";
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

        public unsafe override void Filter(PixelData* pPixel)
        {
            pPixel->red = redGamma[pPixel->red];
            pPixel->green = redGamma[pPixel->green];
            pPixel->blue = redGamma[pPixel->blue];
        }
    }

    public class FilterNoise : ThresholdFilterBase
    {
        private readonly Random rand;

        public FilterNoise()
            : base("Random Noise")
        {
            rand = new Random();
            this.Minimum = 0;
            this.Maximum = 100;
            this.PropertyName = "Noise Density";
        }

        public FilterNoise(int seed) : base("Random Noise")
        {
            this.InPlace = true;
            rand = new Random(seed);
            this.Minimum = 0;
            this.Maximum = 100;
            this.PropertyName = "Noise Density";
        }

        public PixelData NoiseColor
        {
            get; set;
        }

        public unsafe override void Filter(PixelData* pPixel)
        {
            if (rand.Next(this.Minimum, this.Maximum) <= this.Threshold)
            {
                pPixel->red = NoiseColor.red;
                pPixel->green = NoiseColor.green;
                pPixel->blue = NoiseColor.blue;
            }
        }
    }

    public class FilterPosterize : ThresholdFilterBase
    {
        private byte[] _lookup;

        public FilterPosterize()
            : base("Posterize")
        {
            _lookup = new byte[256];
            this.Maximum = 8;
            this.Minimum = 0;
            this.PropertyName = "Number of colors (2^n)";
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
                for (int i = 0; i < _lookup.Length; i++)
                {
                    _lookup[i] = (byte)(i - (i % pow));
                }
            }
        }

        public unsafe override void Filter(PixelData* pPixel)
        {
            pPixel->red = _lookup[pPixel->red];
            pPixel->green = _lookup[pPixel->green];
            pPixel->blue = _lookup[pPixel->blue];
        }
    }

    public class FilterSolarize : ThresholdFilterBase
    {
        private int _pow;

        public FilterSolarize()
            : base("Solarize")
        {
            this.Maximum = 8;
            this.Minimum = 0;
            this.PropertyName = "Number of colors (2^n)";
            this.Threshold = 8;
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
                _pow = (int)Math.Pow(2, this.Threshold);
            }
        }

        public unsafe override void Filter(PixelData* pPixel)
        {
            pPixel->red = (byte)MathEx.Clamp((pPixel->red > this._pow) ? (255 - pPixel->red) : pPixel->red, 0, 255);
            pPixel->green = (byte)MathEx.Clamp((pPixel->green > this._pow) ? (255 - pPixel->green) : pPixel->green, 0, 255);
            pPixel->blue = (byte)MathEx.Clamp((pPixel->blue > this._pow) ? (255 - pPixel->blue) : pPixel->blue, 0, 255);
        }
    }
}
