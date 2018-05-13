using Ima.ImageOps;

namespace Ima
{
    /// <summary>
    /// Summary description for ThresholdWindow.
    /// </summary>
    public class ThresholdWindow : FilterPropertyWindow
    {
        public ThresholdWindow(ImageWrapper image, ThresholdFilterBase filter)
            : base(image, filter, nameof(ThresholdFilterBase.Threshold), filter.Minimum, filter.Maximum)
        {
        }
    }
}