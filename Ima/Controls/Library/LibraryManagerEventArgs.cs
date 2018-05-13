using System;

namespace Ima.Library
{
    /// <summary>
    /// Generic argument container for library manager event
    /// </summary>
    public class LibraryManagerEventArgs : EventArgs
    {
        /// <summary>
        /// Empty 
        /// </summary>
        public static readonly new LibraryManagerEventArgs Empty = new LibraryManagerEventArgs(null);

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="item"></param>
        public LibraryManagerEventArgs(LibraryItem item)
        {
            this.Item = item;
        }

        /// <summary>
        /// Relevant Library Item
        /// </summary>
        public LibraryItem Item
        {
            get; private set;
        }
    }
}
