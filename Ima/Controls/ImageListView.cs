using System;
using System.Windows.Forms;

namespace Ima.Controls
{
	/// <summary>
	/// Summary description for ImageListView.
	/// http://www.thecodeproject.com/cs/miscctrl/listviewxp.asp
	/// </summary>
	public class ImageListView : ListView
	{
		/// <summary>
		/// 
		/// </summary>
		private NativeMethods.LVS_EX styles;

		/// <summary>
		/// 
		/// </summary>
		public ImageListView()
		{
			//
			// TODO: Add constructor logic here
			//
		}

		/// <summary>
		/// Sets Double_Buffering and BorderSelect style
		/// </summary>
		public void SetExStyles()
		{
			//TODO border selection? hmm test it
			//styles = (NativeCalls.LVS_EX)NativeCalls.SendMessage(this.Handle, (int) NativeCalls.LVM.LVM_GETEXTENDEDLISTVIEWSTYLE, 0,0);
			//styles |= NativeCalls.LVS_EX.LVS_EX_DOUBLEBUFFER | NativeCalls.LVS_EX.LVS_EX_BORDERSELECT;
			//NativeCalls.SendMessage(this.Handle, (int) NativeCalls.LVM.LVM_SETEXTENDEDLISTVIEWSTYLE, 0, (int) styles);
		}

		/// <summary>
		/// Sets ListViewExtended Styles
		/// </summary>
		/// <param name="exStyle">The Styles you wish to set.</param>
		internal void SetExStyles(NativeMethods.LVS_EX exStyle)
		{
			styles = (NativeMethods.LVS_EX)NativeMethods.SendMessage(this.Handle, (int) NativeMethods.LVM.LVM_GETEXTENDEDLISTVIEWSTYLE, IntPtr.Zero, IntPtr.Zero);
			styles |= exStyle;
			NativeMethods.SendMessage(this.Handle, (int) NativeMethods.LVM.LVM_SETEXTENDEDLISTVIEWSTYLE, IntPtr.Zero, (IntPtr)styles);
		}

	}
}
