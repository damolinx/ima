using System.Runtime.InteropServices;

namespace Ima.ImageOps
{
    /// <summary>
    /// Defines the RGB memory structure for a bitmap
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public struct PixelData
    {
        public byte blue;
        public byte green;
        public byte red;
        public byte alpha;
    };
}
