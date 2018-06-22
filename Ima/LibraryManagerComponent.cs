using Ima.Library;
using System;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace Ima
{
    /// <summary>
    /// Notification
    /// </summary>
    public delegate void OpenLibraryItemEventHandler(object sender, OpenLibraryEventArgs e);

    /// <summary>
    /// Summary description for LibraryManagerComponent.
    /// </summary>
    public class LibraryManagerComponent : Ima.GenericComponent
    {
        private System.Windows.Forms.TreeView libTree;
        private System.Windows.Forms.ImageList imgLibraryItems;
        private System.Windows.Forms.ToolBarButton tbBtn_Add;
        private System.Windows.Forms.ToolBarButton tbBtn_Remove;
        private System.Windows.Forms.ToolBarButton tbBtn_Separator_1;
        private System.Windows.Forms.ToolBarButton tbBtn_Properties;
        private System.ComponentModel.IContainer components;

        /// <summary>
        /// Stores a reference to manager
        /// </summary>
        LibraryManager manager;

        /// <summary>
        /// Constructor
        /// </summary>
        public LibraryManagerComponent()
            : this(null)
        {
        }

        /// <summary>
        /// Constructor
        /// </summary>
        public LibraryManagerComponent(LibraryManager library)
        {
            // This call is required by the Windows.Forms Form Designer.
            InitializeComponent();

            // Load Library
            this.Manager = library;
        }

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (components != null)
                {
                    components.Dispose();
                }
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(LibraryManagerComponent));
            this.imgLibraryItems = new System.Windows.Forms.ImageList(this.components);
            this.libTree = new System.Windows.Forms.TreeView();
            this.tbBtn_Add = new System.Windows.Forms.ToolBarButton();
            this.tbBtn_Remove = new System.Windows.Forms.ToolBarButton();
            this.tbBtn_Separator_1 = new System.Windows.Forms.ToolBarButton();
            this.tbBtn_Properties = new System.Windows.Forms.ToolBarButton();
            this.panel.SuspendLayout();
            this.bottomPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel
            // 
            this.panel.Controls.Add(this.libTree);
            this.panel.Size = new System.Drawing.Size(192, 400);
            // 
            // imageList
            // 
            this.imageList.ImageSize = new System.Drawing.Size(16, 16);
            this.imageList.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList.ImageStream")));
            this.imageList.TransparentColor = System.Drawing.Color.White;
            this.imageList.Images.SetKeyName(0, "");
            this.imageList.Images.SetKeyName(1, "");
            this.imageList.Images.SetKeyName(2, "");
            // 
            // toolBar
            // 
            this.toolBar.Buttons.AddRange(new System.Windows.Forms.ToolBarButton[] {
            this.tbBtn_Add,
            this.tbBtn_Remove,
            this.tbBtn_Separator_1,
            this.tbBtn_Properties});
            this.toolBar.Size = new System.Drawing.Size(192, 26);
            this.toolBar.ButtonClick += this.toolBar_ButtonClick;
            // 
            // bottomPanel
            // 
            this.bottomPanel.Location = new System.Drawing.Point(0, 400);
            this.bottomPanel.Size = new System.Drawing.Size(192, 48);
            // 
            // imgLibraryItems
            // 
            this.imgLibraryItems.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imgLibraryItems.ImageStream")));
            this.imgLibraryItems.TransparentColor = System.Drawing.Color.White;
            this.imgLibraryItems.Images.SetKeyName(0, "");
            this.imgLibraryItems.Images.SetKeyName(1, "");
            this.imgLibraryItems.Images.SetKeyName(2, "");
            this.imgLibraryItems.Images.SetKeyName(3, "");
            // 
            // libTree
            // 
            this.libTree.AllowDrop = true;
            this.libTree.Dock = System.Windows.Forms.DockStyle.Fill;
            this.libTree.FullRowSelect = true;
            this.libTree.HideSelection = false;
            this.libTree.ImageIndex = 0;
            this.libTree.ImageList = this.imgLibraryItems;
            this.libTree.Indent = 20;
            this.libTree.ItemHeight = 16;
            this.libTree.LabelEdit = true;
            this.libTree.Location = new System.Drawing.Point(0, 0);
            this.libTree.Name = "libTree";
            this.libTree.SelectedImageIndex = 0;
            this.libTree.Size = new System.Drawing.Size(192, 400);
            this.libTree.TabIndex = 0;
            this.libTree.AfterSelect += this.libTree_AfterSelect;
            this.libTree.Click += this.libTree_Click;
            // 
            // tbBtn_Add
            // 
            this.tbBtn_Add.ImageIndex = 0;
            this.tbBtn_Add.Name = "tbBtn_Add";
            this.tbBtn_Add.ToolTipText = "Add a new Library Item";
            // 
            // tbBtn_Remove
            // 
            this.tbBtn_Remove.ImageIndex = 1;
            this.tbBtn_Remove.Name = "tbBtn_Remove";
            this.tbBtn_Remove.ToolTipText = "Remove a library Item.  Some items are not removable";
            // 
            // tbBtn_Separator_1
            // 
            this.tbBtn_Separator_1.Name = "tbBtn_Separator_1";
            this.tbBtn_Separator_1.Style = System.Windows.Forms.ToolBarButtonStyle.Separator;
            // 
            // tbBtn_Properties
            // 
            this.tbBtn_Properties.ImageIndex = 2;
            this.tbBtn_Properties.Name = "tbBtn_Properties";
            // 
            // LibraryManagerComponent
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(168F, 168F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.Name = "LibraryManagerComponent";
            this.Size = new System.Drawing.Size(192, 448);
            this.panel.ResumeLayout(false);
            this.bottomPanel.ResumeLayout(false);
            this.bottomPanel.PerformLayout();
            this.ResumeLayout(false);

        }
        #endregion

        #region Properties
        /// <summary>
        /// Library Manager
        /// </summary>
        public LibraryManager Manager
        {
            get
            {
                return this.manager;
            }

            set
            {
                //Clean previous
                if (this.manager != null)
                {
                    this.manager.AddedLibraryItem -= this.manager_AddedLibraryItem;
                    this.manager.RemovedLibraryItem -= this.manager_RemovedLibraryItem;
                    this.libTree.Nodes.Clear();
                }

                //Set new value
                this.manager = value;

                //Process pre-existing
                if (this.manager != null)
                {
                    this.manager.AddedLibraryItem += manager_AddedLibraryItem;
                    this.manager.RemovedLibraryItem += manager_RemovedLibraryItem;
                    foreach (LibraryItem item in this.manager.Items)
                    {
                        this.AddItem(item);
                    }
                }
            }
        }
        #endregion

        /// <summary>
        /// Adds Node
        /// </summary>
        /// <param name="item"></param>
        protected void AddItem(LibraryItem item)
        {
            LibraryItemNode node = new LibraryItemNode(item);
            foreach (LibraryItem subItem in item.GetLibraryItems(true))
            {
                this.AddItem(subItem, node);
            }
            this.libTree.Nodes.Add(node);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="item"></param>
        /// <param name="parent"></param>
        protected void AddItem(LibraryItem item, LibraryItemNode parent)
        {
            LibraryItemNode node = new LibraryItemNode(item);
            foreach (LibraryItem subItem in item.GetLibraryItems(true))
            {
                this.AddItem(subItem, node);
            }
            parent.Nodes.Add(node);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="node"></param>
        /// <returns></returns>
        protected bool RemoveItem(LibraryItem item)
        {

            foreach (LibraryItemNode node in this.libTree.Nodes)
            {
                if (node.Item == item)
                {
                    this.libTree.Nodes.Remove(node);
                    return true;
                }
            }
            Console.Out.WriteLine("TODO: Removal of recursive node can't be serialized and therefore is not yet supported");
            return false;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnShow_Click(object sender, System.EventArgs e)
        {
            if (libTree.SelectedNode is LibraryItemNode node)
            {
                OpenRequestEvent(new OpenLibraryEventArgs(node.Item));
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void libTree_AfterSelect(object sender, System.Windows.Forms.TreeViewEventArgs e)
        {
            this.toolBar.Buttons[1].Enabled = libTree.SelectedNode != null
                && ((LibraryItemNode)libTree.SelectedNode).Item.Deletable;

            this.toolBar.Buttons[3].Enabled = libTree.SelectedNode != null;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void libTree_Click(object sender, System.EventArgs e)
        {
            TreeNode node = libTree.GetNodeAt(libTree.PointToClient(new Point(MousePosition.X, MousePosition.Y)));
            if (node != null)
            {
                if (node != libTree.SelectedNode)
                {
                    libTree.SelectedNode = node;
                }
                btnShow_Click(libTree, EventArgs.Empty);
            }
        }

        /// <summary>
        /// Event fired when any of the layers changed
        /// </summary>
        public event OpenLibraryItemEventHandler OpenLibrary;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="e"></param>
        protected virtual void OpenRequestEvent(OpenLibraryEventArgs e)
        {
            OpenLibrary?.Invoke(this, e);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
        private void manager_AddedLibraryItem(object sender, LibraryManagerEventArgs args)
        {
            if (args.Item == this.manager.MyPictures)
            {
                this.manager.MyPictures.ImageIndex = 1; //TODO:  Lookup?
            }
            else if (args.Item == this.manager.SharedPictures)
            {
                this.manager.SharedPictures.ImageIndex = 2;  //TODO: Lookup?
            }
            this.AddItem(args.Item);
        }

        private void manager_RemovedLibraryItem(object sender, LibraryManagerEventArgs args)
        {
            this.RemoveItem(args.Item);
        }

        private void toolBar_ButtonClick(object sender, System.Windows.Forms.ToolBarButtonClickEventArgs e)
        {
            LibraryItemNode node;
            switch (toolBar.Buttons.IndexOf(e.Button))
            {
                case 0:
                    FolderBrowserDialog folderBrowser = new FolderBrowserDialog();
                    if (folderBrowser.ShowDialog() == DialogResult.OK)
                    {
                        var item = new FileLibraryItem(Path.GetFileName(folderBrowser.SelectedPath), folderBrowser.SelectedPath)
                        {
                            Deletable = true,
                            Recursive = true,
                            Visible = true,
                            Editable = true,
                            ImageIndex = 0,
                        };

                        this.Manager.AddItem(item);
                    }
                    break;
                case 1:
                    node = (LibraryItemNode)this.libTree.SelectedNode;
                    this.RemoveItem(node.Item);
                    break;
                case 3:
                    break;
            }
        }
    }

    /// <summary>
    /// Event arguments
    /// </summary>
    public class OpenLibraryEventArgs : EventArgs
    {
        public OpenLibraryEventArgs(LibraryItem item)
        {
            this.Item = item;
        }

        public LibraryItem Item
        {
            get; private set;
        }
    }

    /// <summary>
    /// 
    /// </summary>
    public class LibraryItemNode : TreeNode
    {
        public LibraryItemNode(LibraryItem item)
        {
            this.Item = item;
            this.Text = item.Name;
            this.ImageIndex = item.ImageIndex;
            this.SelectedImageIndex = item.ImageIndex;
        }

        /// <summary>
        /// 
        /// </summary>
        public string LibraryItemPath
        {
            get
            {
                return (Item as FileLibraryItem)?.Path ?? string.Empty;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public LibraryItem Item
        {
            get; private set;
        }
    }
}
