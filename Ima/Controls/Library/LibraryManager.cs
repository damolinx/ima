using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Ima.Library
{
    /// <summary>
    /// Generic Handler for Library Events
    /// </summary>
    public delegate void OnLibraryItemEventHandler(object sender, LibraryManagerEventArgs args);

    /// <summary>
    /// Summary description for LibraryManager.
    /// </summary>
    public class LibraryManager
    {
        public const string LIBRARY_USERITEMS = "Library.UserItems";
        public const string LIBRARY_USERITEMS_PREFIX = "Library.UserItem";
        public const string LIBRARY_USERITEMS_PATH_SUFFIX = ".Path";
        public const string LIBRARY_USERITEMS_IMAGEINDEX_SUFFIX = ".ImageIndex";
        public const string LIBRARY_USERITEMS_RECURSIVE_SUFFIX = ".Recursive";

        /// <summary>
        /// "My Pictures" library item index
        /// </summary>
        private int iMyPics = -1;

        /// <summary>
        /// "Shared Pictures" library item index
        /// </summary>
        private int iSharedPics = -1;

        /// <summary>
        /// Load default Library Folders.  They can be omited to create secondary libraries
        /// </summary>
        public void LoadDefaults()
        {
            string path;
            FileLibraryItem libraryItem;

            //My Pictures
            if (this.iMyPics == -1)
            {
                path = Environment.GetFolderPath(Environment.SpecialFolder.MyPictures);
                libraryItem = new FileLibraryItem("My Pictures", path)
                {
                    Deletable = false,
                    Recursive = true,
                    Visible = true,
                    Editable = false,
                };
                this.AddItem(libraryItem, out this.iMyPics);
            }

            //Shared Pictures
            if (this.iSharedPics == -1)
            {
                var strBuilder = new StringBuilder(255);
                NativeMethods.SHGetSpecialFolderPath(IntPtr.Zero, strBuilder, NativeMethods.CSIDL_COMMON_DOCUMENTS, false);
                path = strBuilder.ToString();
                libraryItem = new FileLibraryItem("Shared Pictures", path)
                {
                    Deletable = false,
                    Recursive = true,
                    Visible = true,
                    Editable = false,
                };
                this.AddItem(libraryItem, out this.iSharedPics);
            }
        }

        /// <summary>
        /// Loads user defined items
        /// </summary>
        public void LoadUserItems()
        {
            var paths = Properties.Settings.Default.UserFolders;
            if (paths != null)
            {
                foreach (string path in paths)
                {
                    var libraryItem = new FileLibraryItem(Path.GetFileName(path), path)
                    {
                        ImageIndex = -1,
                        Deletable = true,
                        Recursive = true
                    };

                    this.AddItem(libraryItem, out var index);
                }
            }
        }

        /// <summary>
        /// Adds a new Library Item
        /// </summary>
        /// <param name="libraryItem"></param>
        public void AddItem(LibraryItem libraryItem)
        {
            int index = -1;
            AddItem(libraryItem, out index);
            Properties.Settings.Default.UserFolders.Add(libraryItem.Path);
        }

        /// <summary>
        /// Adds a new Library Item
        /// </summary>
        public void AddItem(LibraryItem libraryItem, out int index)
        {
            index = this.Items.Count;
            this.Items.Add(libraryItem);
            this.AddLibraryItemEvent(new LibraryManagerEventArgs(libraryItem));
        }
        /// <summary>
        /// Removes an existing Library Item
        /// </summary>
        public void RemoveItem(LibraryItem libraryItem)
        {
            if (this.Items.Contains(libraryItem))
            {
                this.Items.Remove(libraryItem);
                this.RemoveLibraryItemEvent(new LibraryManagerEventArgs(libraryItem));
            }
        }

        /// <summary>
        /// Event fired when any of the layers changed
        /// </summary>
        public event OnLibraryItemEventHandler AddedLibraryItem;

        protected virtual void AddLibraryItemEvent(LibraryManagerEventArgs e)
        {
            AddedLibraryItem?.Invoke(this, e);
        }
        /// <summary>
        /// Event fired when any of the layers changed
        /// </summary>
        public event OnLibraryItemEventHandler RemovedLibraryItem;

        protected virtual void RemoveLibraryItemEvent(LibraryManagerEventArgs e)
        {
            RemovedLibraryItem?.Invoke(this, e);
        }

        public List<LibraryItem> Items { get; } = new List<LibraryItem>();

        /// <summary>
        /// Retrieves "My Pictures" item if it was created by loadDefaults
        /// </summary>
        public LibraryItem MyPictures
        {
            get
            {
                return (this.iMyPics >= 0)
                    ? this.Items[this.iMyPics]
                    : null;
            }
        }

        /// <summary>
        /// Retrieves "My Pictures" item if it was created by loadDefaults
        /// </summary>
        public LibraryItem SharedPictures
        {
            get
            {
                return (this.iSharedPics >= 0)
                    ? this.Items[this.iSharedPics]
                    : null;
            }
        }
    }
}
