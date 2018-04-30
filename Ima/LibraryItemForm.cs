using System;
using System.Windows.Forms;

namespace Ima
{
	/// <summary>
	/// Summary description for LibraryItemForm.
	/// </summary>
	public class LibraryItemForm : Ima.Controls.ToolForm
	{
		private System.Windows.Forms.Label lblName;
		private System.Windows.Forms.Label lblLocation;
		private System.Windows.Forms.Label lblIcon;
		private System.Windows.Forms.Button btnOK;
		private System.Windows.Forms.Button btnCancel;
		private System.Windows.Forms.ListView listView;
		private System.Windows.Forms.TextBox txtName;
		private System.Windows.Forms.CheckBox chkRecursive;
		private System.Windows.Forms.TextBox txtPath;

		/// <summary>
		/// Constructor
		/// </summary>
		public LibraryItemForm() : this(string.Empty, string.Empty)
		{
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="name"></param>
		/// <param name="path"></param>
		public LibraryItemForm(string name, string path)
		{
			InitializeComponent();
			//
			// TODO: Add constructor logic here
			//
			this.Text         = "Add Folder";
			this.Name         = "Add Folder";
			this.txtName.Text = name;
			this.txtPath.Text = path;
		}

		/// <summary>
		/// 
		/// </summary>
		private void InitializeComponent()
		{
			this.lblName = new System.Windows.Forms.Label();
			this.txtName = new System.Windows.Forms.TextBox();
			this.lblLocation = new System.Windows.Forms.Label();
			this.lblIcon = new System.Windows.Forms.Label();
			this.listView = new System.Windows.Forms.ListView();
			this.btnOK = new System.Windows.Forms.Button();
			this.btnCancel = new System.Windows.Forms.Button();
			this.txtPath = new System.Windows.Forms.TextBox();
			this.chkRecursive = new System.Windows.Forms.CheckBox();
			this.SuspendLayout();
			// 
			// lblName
			// 
			this.lblName.Location = new System.Drawing.Point(8, 16);
			this.lblName.Name = "lblName";
			this.lblName.TabIndex = 0;
			this.lblName.Text = "Name:";
			// 
			// txtName
			// 
			this.txtName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
				| System.Windows.Forms.AnchorStyles.Right)));
			this.txtName.Location = new System.Drawing.Point(8, 32);
			this.txtName.Name = "txtName";
			this.txtName.Size = new System.Drawing.Size(296, 20);
			this.txtName.TabIndex = 1;
			this.txtName.Text = "";
			// 
			// lblLocation
			// 
			this.lblLocation.Location = new System.Drawing.Point(8, 64);
			this.lblLocation.Name = "lblLocation";
			this.lblLocation.TabIndex = 2;
			this.lblLocation.Text = "Location:";
			// 
			// lblIcon
			// 
			this.lblIcon.Location = new System.Drawing.Point(8, 112);
			this.lblIcon.Name = "lblIcon";
			this.lblIcon.TabIndex = 4;
			this.lblIcon.Text = "Icon:";
			// 
			// listView
			// 
			this.listView.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
				| System.Windows.Forms.AnchorStyles.Left) 
				| System.Windows.Forms.AnchorStyles.Right)));
			this.listView.HideSelection = false;
			this.listView.Location = new System.Drawing.Point(8, 128);
			this.listView.MultiSelect = false;
			this.listView.Name = "listView";
			this.listView.Size = new System.Drawing.Size(296, 88);
			this.listView.TabIndex = 5;
			// 
			// btnOK
			// 
			this.btnOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.btnOK.DialogResult = System.Windows.Forms.DialogResult.OK;
			this.btnOK.Location = new System.Drawing.Point(136, 248);
			this.btnOK.Name = "btnOK";
			this.btnOK.TabIndex = 7;
			this.btnOK.Text = "&OK";
			this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
			// 
			// btnCancel
			// 
			this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.btnCancel.Location = new System.Drawing.Point(224, 248);
			this.btnCancel.Name = "btnCancel";
			this.btnCancel.TabIndex = 8;
			this.btnCancel.Text = "&Cancel";
			// 
			// txtPath
			// 
			this.txtPath.Location = new System.Drawing.Point(8, 80);
			this.txtPath.Name = "txtPath";
			this.txtPath.ReadOnly = true;
			this.txtPath.Size = new System.Drawing.Size(296, 20);
			this.txtPath.TabIndex = 3;
			// 
			// chkRecursive
			// 
			this.chkRecursive.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.chkRecursive.Location = new System.Drawing.Point(8, 224);
			this.chkRecursive.Name = "chkRecursive";
			this.chkRecursive.Size = new System.Drawing.Size(168, 24);
			this.chkRecursive.TabIndex = 6;
			this.chkRecursive.Text = "Show &Internal folders";
			// 
			// LibraryItemForm
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.ClientSize = new System.Drawing.Size(312, 278);
			this.Controls.Add(this.txtPath);
			this.Controls.Add(this.btnCancel);
			this.Controls.Add(this.btnOK);
			this.Controls.Add(this.listView);
			this.Controls.Add(this.lblIcon);
			this.Controls.Add(this.lblLocation);
			this.Controls.Add(this.txtName);
			this.Controls.Add(this.lblName);
			this.Controls.Add(this.chkRecursive);
			this.Name = "LibraryItemForm";
			this.ResumeLayout(false);

		}


		/// <summary>
		/// 
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void btnOK_Click(object sender, System.EventArgs e)
		{
			this.Close();
		}

		/// <summary>
		/// Name for the Library Item
		/// </summary>
		public string ItemName
		{
			get
			{
				return this.txtName.Text;
			}
			set
			{
				this.txtName.Text = value;
			}
		}
	
		/// <summary>
		/// Path for the Library Item
		/// </summary>
		public string ItemPath
		{
			get
			{
				return this.txtPath.Text;
			}
			set
			{
				this.txtPath.Text = value;
			}
		}

		/// <summary>
		/// Recursive property value for Item
		/// </summary>
		public bool ItemRecursive
		{
			get
			{
				return this.chkRecursive.Checked;
			}
			set
			{
				this.chkRecursive.Checked = value;
			}
		}

		/// <summary>
		/// ImageIndex for item
		/// </summary>
		public int ImageIndex
		{
			get
			{
				return (this.listView.SelectedIndices.Count == 0) ? 0 : this.listView.SelectedIndices[0];
			}

			set
			{
				this.listView.Items[value].Selected = true;
			}
		}

		/// <summary>
		/// Sets or retrieves image list to be used by ListView
		/// </summary>
		public ImageList ImageList
		{
			get
			{
				return this.listView.LargeImageList;
			}

			set
			{
				this.listView.LargeImageList = value;
				this.listView.Items.Clear();
				if (value != null)
				{
					for(int i = 0; i < value.Images.Count; i++)
					{
						this.listView.Items.Add( new ListViewItem(string.Empty, i));
					}
				}
			}
		}
	}
}
