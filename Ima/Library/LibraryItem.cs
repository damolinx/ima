using System;
using System.IO;
using System.Drawing;
using System.Collections;

namespace Ima
{
	/// <summary>
	/// Summary description for LibraryItem.
	/// </summary>
	public abstract class LibraryItem : IComparable
	{

		#region Fields
		protected string name       = string.Empty;
		protected string path       = string.Empty;
		protected int    count      = 0;
		protected int    imageIndex = -1;
		protected Image  image      = null;
		protected bool   prop_Visible    = true;
		protected bool   prop_Delete     = false;
		protected bool   prop_Recursive  = false;
		protected bool   prop_Edit       = false;

		#endregion

		/// <summary>
		/// Library Item Constructor
		/// </summary>
		/// <param name="name"></param>
		public LibraryItem(string name)
		{
			this.name       = name;
		}

		#region Properties

		/// <summary>
		/// 
		/// </summary>
		public virtual string Name
		{
			get
			{
				return this.name;
			}
			set
			{
				this.name = value;
			}
		}

		/// <summary>
		/// 
		/// </summary>
		public virtual string Path
		{
			get
			{
				return this.path;
			}
		}

		/// <summary>
		/// 
		/// </summary>
		public virtual int Count
		{
			get
			{
				return this.count;
			}
		}
		
		/// <summary>
		/// 
		/// </summary>
		public virtual int ImageIndex
		{
			get
			{
				return this.imageIndex;
			}
			set
			{
				this.imageIndex = value;
			}
		}
		
		/// <summary>
		/// 
		/// </summary>
		public virtual bool Recursive
		{
			get
			{
				return this.prop_Recursive;
			}
			set
			{
				this.prop_Recursive = value;
			}
		}

		/// <summary>
		/// 
		/// </summary>
		public virtual bool Visible
		{
			get
			{
				return this.prop_Visible;
			}
			set
			{
				this.prop_Visible = value;
			}
		}		

		/// <summary>
		/// 
		/// </summary>
		public virtual bool Deletable
		{
			get
			{
				return this.prop_Delete;
			}
			set
			{
				this.prop_Delete = value;
			}
		}


		/// <summary>
		/// 
		/// </summary>
		public virtual bool Editable
		{
			get
			{
				return this.prop_Edit;
			}
			set
			{
				this.prop_Edit = value;
			}
		}

		public abstract Image Thumbnail
		{
			get;
		}

		#endregion

		#region Overrides
		/// <summary>
		/// 
		/// </summary>
		/// <returns></returns>
		public override string ToString()
		{
			return this.name;
		}
		#endregion

		#region IComparable Members

		public int CompareTo(object obj)
		{
			return (obj is LibraryItem) ? this.name.CompareTo(( (LibraryItem)obj).name)
				: this.name.CompareTo(obj.ToString());
			
		}

		#endregion

		#region Abstract Method

		/// <summary>
		/// 
		/// </summary>
		/// <returns></returns>
		public abstract LibraryItem[] GetLibraryItems(bool sort);

		public abstract LibraryItem[] GetImageItems(bool sort);

		#endregion
	}

	public class FileLibraryItem : LibraryItem
	{
		/// <summary>
		/// 
		/// </summary>
		/// <param name="name"></param>
		/// <param name="path"></param>
		public FileLibraryItem(string name, string path) : base(name)
		{
			this.path = path;
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="path"></param>
		public FileLibraryItem(string path) : base(System.IO.Path.GetFileNameWithoutExtension(path))
		{
			this.path = path;
		}

		/// <summary>
		/// 
		/// </summary>
		/// <returns></returns>
		public override LibraryItem[] GetLibraryItems(bool sort)
		{
			ArrayList items = new ArrayList();
			if (this.Recursive)
			{
				foreach(string directory in Directory.GetDirectories(this.path))
				{
					FileLibraryItem item = new FileLibraryItem(directory);
					item.ImageIndex      = this.ImageIndex;
					items.Add(item);
				}
			}
			this.count = items.Count;
			return items.ToArray(typeof(FileLibraryItem)) as LibraryItem[];
		}

		/// <summary>
		/// 
		/// </summary>
		/// <returns></returns>
		public override LibraryItem[] GetImageItems(bool sort)
		{
			ArrayList items = new ArrayList();
			Tokens tokens = new Tokens(Configuration.APPLICATION_IMAGE_SUPPORTED, new char[]{';'});
			foreach (string token in tokens)
			{
				string[] files = Directory.GetFiles(this.Path, token);
				foreach(string file in files)
				{
					items.Add(new FileLibraryItem(file));
				}
			}
			this.count = items.Count;
			return items.ToArray(typeof(FileLibraryItem)) as LibraryItem[];
		}
		
		/// <summary>
		/// 
		/// </summary>
		public override Image Thumbnail
		{
			get
			{
				if (this.image != null)
				{
				}
				return this.image;
			}
		}
	}
}
