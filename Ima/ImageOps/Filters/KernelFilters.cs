using System;

namespace Ima.ImageOps.Filters
{
    public abstract class KernelFilterBase : ThresholdFilterBase
    {
        private int[] _matrix;

        public KernelFilterBase(int size, string name)
            : base(name)
        {
            this.Direct = false;
            this.Property = "Matrix (2n + 1)";
            this.Minimum = 0;
            this.Maximum = 5;
            this.Threshold = size;
            this.Border = (this.Threshold - 1) / 2;
            this.Offset = 0;
            this.Weight = 1;
        }

        public int[] Matrix
        {
            get
            {
                return this._matrix;
            }
            set
            {
                this._matrix = value;
                this.Threshold = (_matrix == null) ? 0 : _matrix.Length;
            }
        }

        public int Weight
        {
            get; set;
        }

        public int Offset
        {
            get; set;
        }

        protected unsafe void applyConvolution3x3(int x, int y, PixelGet getPixel, PixelData* pPixel)
        {
            PixelData* pPixelClone1, pPixelClone2, pPixelClone3, pPixelClone4, pPixelClone5,
                pPixelClone6, pPixelClone7, pPixelClone8, pPixelClone9;

            pPixelClone1 = getPixel(x - 1, y + 1);
            pPixelClone2 = getPixel(x, y + 1);
            pPixelClone3 = getPixel(x + 1, y + 1);
            pPixelClone4 = getPixel(x - 1, y);
            pPixelClone5 = getPixel(x, y);
            pPixelClone6 = getPixel(x + 1, y);
            pPixelClone7 = getPixel(x - 1, y - 1);
            pPixelClone8 = getPixel(x, y - 1);
            pPixelClone9 = getPixel(x + 1, y - 1);

            int tmp = Offset + (_matrix[0] * pPixelClone1->red + _matrix[1] * pPixelClone2->red + _matrix[2] * pPixelClone3->red
                + _matrix[3] * pPixelClone4->red + _matrix[4] * pPixelClone5->red + _matrix[5] * pPixelClone6->red
                + _matrix[6] * pPixelClone7->red + _matrix[7] * pPixelClone8->red + _matrix[8] * pPixelClone9->red) / Weight;

            pPixel->red = (byte)((tmp >= 0) ? ((tmp <= 255) ? tmp : 255) : 0);

            tmp = Offset + (_matrix[0] * pPixelClone1->green + _matrix[1] * pPixelClone2->green + _matrix[2] * pPixelClone3->green
                + _matrix[3] * pPixelClone4->green + _matrix[4] * pPixelClone5->green + _matrix[5] * pPixelClone6->green
                + _matrix[6] * pPixelClone7->green + _matrix[7] * pPixelClone8->green + _matrix[8] * pPixelClone9->green) / Weight;

            pPixel->green = (byte)((tmp >= 0) ? ((tmp <= 255) ? tmp : 255) : 0);

            tmp = Offset + (_matrix[0] * pPixelClone1->blue + _matrix[1] * pPixelClone2->blue + _matrix[2] * pPixelClone3->blue
                + _matrix[3] * pPixelClone4->blue + _matrix[4] * pPixelClone5->blue + _matrix[5] * pPixelClone6->blue
                + _matrix[6] * pPixelClone7->blue + _matrix[7] * pPixelClone8->blue + _matrix[8] * pPixelClone9->blue) / Weight;

            pPixel->blue = (byte)((tmp >= 0) ? ((tmp <= 255) ? tmp : 255) : 0);
        }
    }

    public class FilterMatrixBlur : KernelFilterBase
    {
        public FilterMatrixBlur()
            : base(3, "Blur")
        {
            this.Weight = 16;
            this.Offset = 0;
            this.Matrix = new int[] { 1, 2, 1, 2, 4, 2, 1, 2, 1 };
        }

        public unsafe override void Filter(int x, int y, PixelGet getPixel, PixelData* pPixel, int x0, int y0, int x1, int y1)
        {
            this.applyConvolution3x3(x, y, getPixel, pPixel);
        }
    }

    public class FilterMatrixEmboss : KernelFilterBase
    {
        public FilterMatrixEmboss()
            : base(127, "Emboss")
        {
            this.Weight = 1;
            this.Offset = 127;
            this.Property = "Color threshold";
            this.Minimum = 0;
            this.Maximum = 255;
            this.Threshold = 1;
            this.Matrix = new int[] { -1, 0, -1, 0, 4, 0, -1, 0, -1 };
        }

        public unsafe override void Filter(int x, int y, PixelGet getPixel, PixelData* pPixel, int x0, int y0, int x1, int y1)
        {
            this.applyConvolution3x3(x, y, getPixel, pPixel);
        }
    }

    public class FilterMatrixSharpen : KernelFilterBase
    {
        public FilterMatrixSharpen()
            : base(3, "Sharpen")
        {
            this.Weight = 1;
            this.Offset = 0;
            this.Matrix = new int[] { 0, -1, 0, -1, 5, -1, 0, -1, 0 };
        }

        public unsafe override void Filter(int x, int y, PixelGet getPixel, PixelData* pPixel, int x0, int y0, int x1, int y1)
        {
            this.applyConvolution3x3(x, y, getPixel, pPixel);
        }
    }

    public class FilterMatrixBW : KernelFilterBase
    {
        public FilterMatrixBW()
            : base(3, "Black/White Edge Detect")
        {
            this.Weight = 1;
            this.Offset = 0;
            this.Matrix = new int[] { 0 };
            this.Property = "Color threshold";
            this.Minimum = 0;
            this.Maximum = 255;
            this.Threshold = 20;
        }

        public unsafe override void Filter(int x, int y, PixelGet getPixel, PixelData* pPixel, int x0, int y0, int x1, int y1)
        {
            PixelData* localPixel = pPixel;
            PixelData* localPixel1 = getPixel(x + 1, y);
            PixelData* localPixel2 = getPixel(x, y + 1);

            if (((localPixel->red - localPixel1->red) * (localPixel->red - localPixel1->red)) +
                ((localPixel->green - localPixel1->green) * (localPixel->green - localPixel1->green)) +
                ((localPixel->blue - localPixel1->blue) * (localPixel->blue - localPixel1->blue)) >= (this.Threshold * this.Threshold)
                ||

                ((localPixel->red - localPixel2->red) * (localPixel->red - localPixel2->red)) +
                ((localPixel->green - localPixel2->green) * (localPixel->green - localPixel2->green)) +
                ((localPixel->blue - localPixel2->blue) * (localPixel->blue - localPixel2->blue)) >= (this.Threshold * this.Threshold))
            {
                pPixel->red = (byte)0;
                pPixel->green = (byte)0;
                pPixel->blue = (byte)0;
            }
            else
            {
                pPixel->red = (byte)255;
                pPixel->green = (byte)255;
                pPixel->blue = (byte)255;
            }
        }
    }

    public class FilterMatrixSobelED : KernelFilterBase
    {
        private readonly int[] mat1 = new int[] { 1, 2, 1, 0, 0, 0, -1, -2, -1 };
        private readonly int[] mat2 = new int[] { 1, 0, -1, 2, 0, -2, 1, 0, -1 };

        public FilterMatrixSobelED()
            : base(3, "Sobel Edge Detect")
        {
            this.Weight = 13;
            this.Offset = 0;
            this.Matrix = null;
        }

        public unsafe override void Filter(int x, int y, PixelGet getPixel, PixelData* pPixel, int x0, int y0, int x1, int y1)
        {
            PixelData pixel1 = new PixelData();
            PixelData pixel2 = new PixelData();

            this.Matrix = mat1;
            this.applyConvolution3x3(x, y, getPixel, &pixel1);

            this.Matrix = mat2;
            this.applyConvolution3x3(x, y, getPixel, &pixel2);

            pPixel->red = (byte)Math.Sqrt(pixel1.red * pixel1.red + pixel2.red * pixel2.red);
            pPixel->green = (byte)Math.Sqrt(pixel1.green * pixel1.green + pixel2.green * pixel2.green);
            pPixel->blue = (byte)Math.Sqrt(pixel1.blue * pixel1.blue + pixel2.blue * pixel2.blue);
        }
    }

    public class FilterMatrixPrewittED : KernelFilterBase
    {
        private readonly int[] mat1 = new int[] { 1, 1, 1, 0, 0, 0, -1, -1, -1 };
        private readonly int[] mat2 = new int[] { 1, 0, -1, 1, 0, -1, 1, 0, -1 };

        public FilterMatrixPrewittED()
            : base(3, "Prewitt Edge Detect")
        {
            this.Weight = 13;
            this.Offset = 0;
            this.Matrix = null;
        }

        public unsafe override void Filter(int x, int y, PixelGet getPixel, PixelData* pPixel, int x0, int y0, int x1, int y1)
        {
            var pixel1 = new PixelData();
            var pixel2 = new PixelData();

            this.Matrix = mat1;
            this.applyConvolution3x3(x, y, getPixel, &pixel1);

            this.Matrix = mat2;
            this.applyConvolution3x3(x, y, getPixel, &pixel2);

            pPixel->red = (byte)Math.Sqrt(pixel1.red * pixel1.red + pixel2.red * pixel2.red);
            pPixel->green = (byte)Math.Sqrt(pixel1.green * pixel1.green + pixel2.green * pixel2.green);
            pPixel->blue = (byte)Math.Sqrt(pixel1.blue * pixel1.blue + pixel2.blue * pixel2.blue);
        }
    }

    public class FilterMatrixKirshED : KernelFilterBase
    {
        private readonly int[] mat1 = new int[] { 5, -3, -3, 5, -3, -3, 5, -3, -3 };
        private readonly int[] mat2 = new int[] { 5, 5, 5, -3, -3, -3, -3, -3, -3 };

        public FilterMatrixKirshED()
            : base(3, "Kirsh Edge Detect")
        {
            this.Weight = 13;
            this.Offset = 0;
            this.Matrix = null;
        }

        public unsafe override void Filter(int x, int y, PixelGet getPixel, PixelData* pPixel, int x0, int y0, int x1, int y1)
        {
            PixelData pixel1 = new PixelData();
            PixelData pixel2 = new PixelData();

            this.Matrix = mat1;
            this.applyConvolution3x3(x, y, getPixel, &pixel1);

            this.Matrix = mat2;
            this.applyConvolution3x3(x, y, getPixel, &pixel2);

            pPixel->red = (byte)Math.Sqrt(pixel1.red * pixel1.red + pixel2.red * pixel2.red);
            pPixel->green = (byte)Math.Sqrt(pixel1.green * pixel1.green + pixel2.green * pixel2.green);
            pPixel->blue = (byte)Math.Sqrt(pixel1.blue * pixel1.blue + pixel2.blue * pixel2.blue);
        }
    }
}
