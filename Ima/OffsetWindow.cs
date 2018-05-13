using Ima.ImageOps;
using Ima.ImageOps.Filters;

namespace Ima
{
    /// <summary>
    /// Summary description for OffsetWindow.
    /// </summary>
    public class OffsetWindow : FilterPropertyWindow
    {
        public OffsetWindow(ImageWrapper image, KernelFilterBase filter)
            : base(image, filter, nameof(KernelFilterBase.Offset), filter.Minimum, filter.Maximum)

        {
        }
    }
}