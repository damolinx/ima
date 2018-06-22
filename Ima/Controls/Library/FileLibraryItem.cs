using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;

namespace Ima.Library
{
    public class FileLibraryItem : LibraryItem
    {
        public FileLibraryItem(string name, string path)
            : base(name)
        {
            this.Path = path;
        }

        public FileLibraryItem(string path)
            : base(System.IO.Path.GetFileNameWithoutExtension(path))
        {
            this.Path = path;
        }

        public override LibraryItem[] GetLibraryItems(bool sort)
        {
            //TODO: Handle folder access exceptions
            var items = this.Recursive
                ? Directory.GetDirectories(this.Path)
                 .Select(dir => new FileLibraryItem(dir) { ImageIndex = this.ImageIndex })
                 .ToArray()
                : Array.Empty<LibraryItem>();

            this.Count = items.Length;
            return items;
        }

        public override LibraryItem[] GetImageItems(bool sort)
        {
            var items = Program.APPLICATION_IMAGE_SUPPORTED.Split(new[] { ';' }, StringSplitOptions.RemoveEmptyEntries)
                .SelectMany(extensionSearchPattern => GetFiles(this.Path, extensionSearchPattern))
                .Select(file => new FileLibraryItem(file))
                .ToArray();

            this.Count = items.Length;
            return items;
        }

        public override Image Thumbnail
        {
            get;
        }

        #region Private

        private static IEnumerable<string> GetFiles(string path, string searchPattern)
        {
            try
            {
                return Directory.GetFiles(path, searchPattern);
            }
            catch (UnauthorizedAccessException)
            {
                // No access
                return Array.Empty<string>();
            }
        }

        #endregion
    }
}
