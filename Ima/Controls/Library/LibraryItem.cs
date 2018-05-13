using System;
using System.Drawing;

namespace Ima
{
    /// <summary>
    /// Summary description for LibraryItem.
    /// </summary>
    public abstract class LibraryItem : IComparable
    {
        /// <summary>
        /// Library Item Constructor
        /// </summary>
        /// <param name="name"></param>
        public LibraryItem(string name)
        {
            this.ImageIndex = -1;
            this.Name = name;
            this.Visible = true;
        }

        public string Name
        {
            get; set;
        }

        public string Path
        {
            get; protected set;
        }

        public int Count
        {
            get; protected set;
        }

        public int ImageIndex
        {
            get; set;
        }

        public bool Recursive
        {
            get; set;
        }

        public bool Visible
        {
            get; set;
        }

        public bool Deletable
        {
            get; set;
        }

        public bool Editable
        {
            get; set;
        }

        public abstract Image Thumbnail
        {
            get;
        }

        public override string ToString()
        {
            return this.Name;
        }

        #region IComparable Members

        public int CompareTo(object obj)
        {
            return (obj is LibraryItem item)
                ? this.Name.CompareTo(item.Name)
                : this.Name.CompareTo(obj.ToString());

        }

        #endregion

        public abstract LibraryItem[] GetLibraryItems(bool sort);

        public abstract LibraryItem[] GetImageItems(bool sort);
    }
}
