using System;
using System.Drawing;
using System.IO;

namespace Ima.ImageOps
{
    public static class ImageUtilities
    {
        public static bool RotateFlipImage(string path, RotateFlipType type)
        {
            Image image = null;
            FileStream stream = null;
            try
            {
                stream = new FileStream(path, FileMode.Open, FileAccess.ReadWrite, FileShare.Read);
                image = Image.FromStream(stream, false);
                stream.Close();
                image.RotateFlip(type);
                image.Save(path);
                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message + "\n" + e.StackTrace);
            }
            finally
            {
                if (image != null)
                    image.Dispose();
                if (stream != null)
                    stream.Close();
            }
            return false;
        }
    }
}
