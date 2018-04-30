using System;
using System.Drawing;
using System.Collections;

namespace Ima
{
	/// <summary>
	/// Undo Manager
	/// </summary>
	class UndoManager
	{
		/// <summary>
		/// 
		/// </summary>
		private int pendingChanges = 0;

		/// <summary>
		/// Stack Depth
		/// </summary>
		private static int depth = 10;
		
		/// <summary>
		/// Avoids the recording of Undo/Redo pairs
		/// </summary>
		private bool locked = false;
		
		/// <summary>
		/// Undo stack
		/// </summary>
		Stack undo = new Stack(depth);
		
		/// <summary>
		/// Redo Stack
		/// </summary>
		Stack redo = new Stack(depth);

		/// <summary>
		/// 
		/// </summary>
		public void ResetPending()
		{
			this.pendingChanges = 0;
		}

		/// <summary>
		/// 
		/// </summary>
		/// <returns></returns>
		public bool PendingChanges()
		{
			return this.pendingChanges > 0;
		}

		/// <summary>
		/// Stores an Undo pair
		/// </summary>
		/// <param name="name"></param>
		/// <param name="bitmap"></param>
		public void addUndo(string name, Bitmap bitmap)
		{
			if (!this.locked)
			{
				this.pendingChanges += 1;
				undo.Push(new NamedPair(name, bitmap));
				redo.Clear();
			}
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="name"></param>
		/// <param name="current"></param>
		/// <returns></returns>
		public NamedPair getRedo(string name, Bitmap current)
		{
			this.pendingChanges += 1;
			NamedPair pair = (NamedPair)redo.Pop();
			NamedPair namedPair = new NamedPair(name, current);
			undo.Push(namedPair);
			return pair;
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="current"></param>
		/// <returns></returns>
		public NamedPair getUndo(string name, Bitmap current)
		{
			this.pendingChanges -= 1;
			NamedPair redoPair = new NamedPair(name, current);
			redo.Push(redoPair);
			return (NamedPair)undo.Pop();
		}

		/// <summary>
		/// 
		/// </summary>
		/// <returns></returns>
		public string getUndoName()
		{
			return ((NamedPair)undo.Peek()).Name;
		}

		/// <summary>
		/// 
		/// </summary>
		/// <returns></returns>
		public string getRedoName()
		{
			return ((NamedPair)redo.Peek()).Name;
		}

		/// <summary>
		/// 
		/// </summary>
		/// <returns></returns>
		public bool canUndo()
		{
			return undo.Count > 0;
		}

		/// <summary>
		/// 
		/// </summary>
		/// <returns></returns>
		public bool canRedo()
		{
			return redo.Count > 0;
		}

		/// <summary>
		/// 
		/// </summary>
		public void Clear()
		{
			this.undo.Clear();
			this.redo.Clear();
			this.ResetPending();
		}

		/// <summary>
		/// 
		/// </summary>
		/// <returns></returns>
		public Bitmap discardAll()
		{
			this.ResetPending();
			this.redo.Clear();
			this.pendingChanges = 0;
			while (this.undo.Count > 1)
			{
				this.undo.Pop();
			}
			return ((NamedPair)this.undo.Pop()).Bitmap;
		}

		/// <summary>
		/// 
		/// </summary>
		public bool Lock
		{
			get
			{
				return this.locked;
			}

			set
			{
				this.locked = value;
			}
		}

		/// <summary>
		/// Utility class
		/// </summary>
		public class NamedPair
		{
			/// <summary>
			/// Name
			/// </summary>
			string name;

			/// <summary>
			/// Bitmap
			/// </summary>
			Bitmap bitmap;

			/// <summary>
			/// 
			/// </summary>
			/// <param name="name">Pair Name</param>
			/// <param name="bitmap">Bitmap</param>
			public NamedPair(string name, Bitmap bitmap)
			{
				this.name = name;
				this.bitmap = bitmap;
			}

			/// <summary>
			/// Name
			/// </summary>
			public string Name
			{
				get
				{
					return this.name;
				}
			}

			/// <summary>
			/// Bitmap
			/// </summary>
			public Bitmap Bitmap
			{
				get
				{
					return this.bitmap;
				}
			}
		}
	} // EOC UndoManager
}