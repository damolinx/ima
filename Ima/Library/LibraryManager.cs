using System;
using System.Text;
using System.Collections;

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
		#region Configuration constants

		public static readonly string LIBRARY_USERITEMS                   = "Library.UserItems";
		public static readonly string LIBRARY_USERITEMS_PREFIX            = "Library.UserItem.";
		public static readonly string LIBRARY_USERITEMS_PATH_SUFFIX       = ".Path";
		public static readonly string LIBRARY_USERITEMS_IMAGEINDEX_SUFFIX = ".ImageIndex";
		public static readonly string LIBRARY_USERITEMS_RECURSIVE_SUFFIX  = ".Recursive";

		#endregion

		#region Fields
		/// <summary>
		/// "My Pictures" library item index
		/// </summary>
		int iMyPics = -1;

		/// <summary>
		/// "Shared Pictures" library item index
		/// </summary>
		int iSharedPics = -1;

		/// <summary>
		/// Main Storage
		/// </summary>
		ArrayList items = new ArrayList();
		#endregion
	
		#region Methods
		/// <summary>
		/// Load default Library Folders.  They can be omited to create secondary libraries
		/// </summary>
		public void loadDefaults()
		{
			String path;
			FileLibraryItem libraryItem;

			//My Pictures
			if (this.iMyPics == -1)
			{
				path                  = Environment.GetFolderPath(Environment.SpecialFolder.MyPictures);
				libraryItem           = new FileLibraryItem("My Pictures", path);
				libraryItem.Deletable = false;
				libraryItem.Recursive = true;
				libraryItem.Visible   = true;
				libraryItem.Editable  = false;
				this.AddItem(libraryItem, out this.iMyPics);
			}

			//Shared Pictures
			if (this.iSharedPics == -1)
			{
				StringBuilder strBuilder = new StringBuilder(255);
				NativeCalls.SHGetSpecialFolderPath(IntPtr.Zero, strBuilder, NativeCalls.CSIDL_COMMON_DOCUMENTS, false);
				path                     = strBuilder.ToString();
				libraryItem              = new FileLibraryItem("Shared Pictures", path);
				libraryItem.Deletable    = false;
				libraryItem.Recursive    = true;
				libraryItem.Visible      = true;
				libraryItem.Editable     = false;
				this.AddItem(libraryItem, out this.iSharedPics);
			}
		}

		/// <summary>
		/// Loads user defined items
		/// </summary>
		public void loadUserItems()
		{
			string temp = Configuration.Instance.GetProperty(LIBRARY_USERITEMS, string.Empty) as string;
			string[] items = temp.Split(new char[]{';'});
			foreach(string item in items)
			{
				string prop      = LIBRARY_USERITEMS_PREFIX + item;
				string path      = Configuration.Instance.GetProperty(prop + LIBRARY_USERITEMS_PATH_SUFFIX, string.Empty) as string;
				string image     = Configuration.Instance.GetProperty(prop + LIBRARY_USERITEMS_IMAGEINDEX_SUFFIX, "-1") as string;
				string recursive = Configuration.Instance.GetProperty(prop + LIBRARY_USERITEMS_IMAGEINDEX_SUFFIX, false.ToString()) as string;
				LibraryItem libraryItem = new FileLibraryItem(item, path);
				this.items.Add(libraryItem);
				try
				{
					 libraryItem.ImageIndex = int.Parse(image);
				}
				catch
				{
					libraryItem.ImageIndex = -1;
				}
				try
				{
					libraryItem.Recursive = bool.Parse(recursive);
				}
				catch
				{
					libraryItem.Recursive = false;
				}
			}
		}

		/// <summary>
		/// Adds a new Library Item
		/// </summary>
		/// <param name="name"></param>
		/// <param name="path"></param>
		/// <param name="index"></param>
		public void AddItem(string name, string path, out int index)
		{
			LibraryItem libraryItem = new FileLibraryItem(name, path);
			AddItem(libraryItem, out index);
		}

		/// <summary>
		/// Adds a new Library Item
		/// </summary>
		/// <param name="libraryItem"></param>
		public void AddItem(LibraryItem libraryItem)
		{
			int index = -1;
			AddItem(libraryItem, out index);
		}

		/// <summary>
		/// Adds a new Library Item
		/// </summary>
		/// <param name="libraryItem"></param>
		/// <param name="index"></param>
		public void AddItem(LibraryItem libraryItem, out int index)
		{
			index = this.items.Add(libraryItem);
			this.AddLibraryItemEvent(new LibraryManagerEventArgs(libraryItem));
		}
		/// <summary>
		/// Removes an existing Library Item
		/// </summary>
		/// <param name="item"></param>
		/// <returns></returns>
		public void RemoveItem(LibraryItem libraryItem)
		{
			if (this.items.Contains(libraryItem))
			{
				this.items.Remove(libraryItem);
				this.RemoveLibraryItemEvent(new LibraryManagerEventArgs(libraryItem));
			}
		}

		#endregion

		#region Event Handling

		/// <summary>
		/// Event fired when any of the layers changed
		/// </summary>
		public event OnLibraryItemEventHandler AddedLibraryItem;

		/// <summary>
		/// 
		/// </summary>
		/// <param name="e"></param>
		protected virtual void AddLibraryItemEvent(LibraryManagerEventArgs e) 
		{
			if (AddedLibraryItem != null)
				AddedLibraryItem(this, e);
		}
		/// <summary>
		/// Event fired when any of the layers changed
		/// </summary>
		public event OnLibraryItemEventHandler RemovedLibraryItem;

		/// <summary>
		/// 
		/// </summary>
		/// <param name="e"></param>
		protected virtual void RemoveLibraryItemEvent(LibraryManagerEventArgs e) 
		{
			if (RemovedLibraryItem != null)
				RemovedLibraryItem(this, e);
		}
		#endregion

		#region Properties
		/// <summary>
		/// 
		/// </summary>
		public ArrayList Items
		{
			get
			{
				return this.items;
			}
		}

		/// <summary>
		/// Retrieves "My Pictures" item if it was created by loadDefaults
		/// </summary>
		public LibraryItem MyPictures
		{
			get
			{
				return (this.iMyPics >= 0) 
					? this.items[this.iMyPics] as LibraryItem 
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
					? this.items[this.iSharedPics] as LibraryItem 
					: null;
			}
		}
		#endregion
	}

	/// <summary>
	/// Generic argument container for library manager event
	/// </summary>
	public class LibraryManagerEventArgs : EventArgs
	{
		/// <summary>
		/// Empty 
		/// </summary>
		static new LibraryManagerEventArgs Empty = new LibraryManagerEventArgs(null);

		/// <summary>
		/// Stored Item
		/// </summary>
		LibraryItem item;

		/// <summary>
		/// Constructor
		/// </summary>
		/// <param name="item"></param>
		public LibraryManagerEventArgs(LibraryItem item)
		{
			this.item = item;
		}
		
		/// <summary>
		/// Relevant Library Item
		/// </summary>
		public LibraryItem Item
		{
			get
			{
				return this.item;
			}
		}
	}
}
