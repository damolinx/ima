using System;
using System.Drawing;

namespace Ima.ImageOps.Filters
{
    public class FilterFrostedGlass : ThresholdFilterBase
    {
        private readonly Random _rand;

        public FilterFrostedGlass() 
            : base("Frosted Glass")
        {
            this.InPlace = false;
            _rand = new Random();
            this.Minimum = 0;
            this.Maximum = 10;
            this.Threshold = 1;
            this.PropertyName = "Frost thickness";
        }

        public FilterFrostedGlass(int seed) 
            : base("Frosted Glass")
        {
            this.InPlace = false;
            _rand = new Random(seed);
            this.Minimum = 0;
            this.Maximum = 10;
            this.Threshold = 1;
            this.PropertyName = "Frost thickness";
        }

        public unsafe override void Filter(int x, int y, PixelGet getPixel, PixelData* pPixel, int x0, int y0, int x1, int y1)
        {
            int rand_x = Math.Min(Math.Max(x0, x + _rand.Next(-this.Threshold, this.Threshold)), x1);
            int rand_y = Math.Min(Math.Max(y0, y + _rand.Next(-this.Threshold, this.Threshold)), y1);

            PixelData* pRandPixel = getPixel(rand_x, rand_y);

            pPixel->red = pRandPixel->red;
            pPixel->green = pRandPixel->green;
            pPixel->blue = pRandPixel->blue;
        }
    }

    public class FilterRotate : ThresholdFilterBase
    {
        private Point _center;
        private double _angle;

        public FilterRotate(double angle, Point center) : base("Rotation")
        {
            this.InPlace = false;
            this.Minimum = 0;
            this.Maximum = 359;
            this.Threshold = 0;
            this.PropertyName = "Angle";
            _center = center;
            _angle = 2.0 * Math.PI * angle / 360.0;
            this.FillColor = Color.White;
        }

        public FilterRotate()
            : this(0, Point.Empty)
        {
        }

        public Color FillColor
        {
            get; set;
        }

        public unsafe override void Filter(int x, int y, PixelGet getPixel, PixelData* pPixel, int x0, int y0, int x1, int y1)
        {
            int rot_x = (int)((x - _center.X) * Math.Cos(_angle) - (y - _center.Y) * Math.Sin(_angle) + _center.X);
            int rot_y = (int)((x - _center.X) * Math.Sin(_angle) + (y - _center.Y) * Math.Cos(_angle) + _center.Y);

            if (rot_x >= x0 && rot_x <= x1 && rot_y >= y0 && rot_y <= y1)
            {
                PixelData* pRotPixel = getPixel(rot_x, rot_y);
                pPixel->red = pRotPixel->red;
                pPixel->green = pRotPixel->green;
                pPixel->blue = pRotPixel->blue;
            }
            else
            {
                pPixel->red = this.FillColor.R;
                pPixel->green = this.FillColor.G;
                pPixel->blue = this.FillColor.B;
            }
        }
    }
}
