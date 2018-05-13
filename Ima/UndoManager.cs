using System.Collections.Generic;
using System.Drawing;

namespace Ima
{
    /// <summary>
    /// Undo Manager
    /// </summary>
    class UndoManager
    {
        /// <summary>
        /// Stack Depth
        /// </summary>
        private const int Depth = 10;

        private int _pendingChanges;

        /// <summary>
        /// Undo stack
        /// </summary>
        private readonly Stack<NamedPair> _undo = new Stack<NamedPair>(Depth);

        /// <summary>
        /// Redo Stack
        /// </summary>
        private readonly Stack<NamedPair> _redo = new Stack<NamedPair>(Depth);

        public void ResetPending()
        {
            _pendingChanges = 0;
        }

        public bool PendingChanges => this._pendingChanges > 0;

        /// <summary>
        /// Stores an Undo pair
        /// </summary>
        /// <param name="name"></param>
        /// <param name="bitmap"></param>
        public void AddUndo(string name, Bitmap bitmap)
        {
            if (!this.Lock)
            {
                _pendingChanges += 1;
                _undo.Push(new NamedPair(name, bitmap));
                _redo.Clear();
            }
        }
        public NamedPair GetRedo(string name, Bitmap current)
        {
            _pendingChanges += 1;
            var pair = _redo.Pop();
            var namedPair = new NamedPair(name, current);
            _undo.Push(namedPair);
            return pair;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="current"></param>
        /// <returns></returns>
        public NamedPair GetUndo(string name, Bitmap current)
        {
            _pendingChanges -= 1;
            var redoPair = new NamedPair(name, current);
            _redo.Push(redoPair);
            return _undo.Pop();
        }

        public string GetUndoName()
        {
            return _undo.Peek()?.Name;
        }

        public string GetRedoName()
        {
            return _redo.Peek()?.Name;
        }

        public bool CanUndo()
        {
            return _undo.Count > 0;
        }

        public bool CanRedo()
        {
            return _redo.Count > 0;
        }

        public void Clear()
        {
            _undo.Clear();
            _redo.Clear();
            this.ResetPending();
        }

        public Bitmap DiscardAll()
        {
            this.ResetPending();
            _redo.Clear();
            _pendingChanges = 0;
            while (_undo.Count > 1)
            {
                _undo.Pop();
            }
            return _undo.Pop().Bitmap;
        }

        public bool Lock
        {
            get; set;
        }

        /// <summary>
        /// Utility class
        /// </summary>
        public class NamedPair
        {
            public NamedPair(string name, Bitmap bitmap)
            {
                this.Name = name;
                this.Bitmap = bitmap;
            }

            /// <summary>
            /// Name
            /// </summary>
            public string Name
            {
                get; private set;
            }

            /// <summary>
            /// Bitmap
            /// </summary>
            public Bitmap Bitmap
            {
                get; private set;
            }
        }
    }
}