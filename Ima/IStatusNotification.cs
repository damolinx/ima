using System;
using System.Windows.Forms;

namespace Ima
{
	/// <summary>
	/// Summary description for IStatusNotification.
	/// </summary>
	public interface IStatusNotification
	{
		/// <summary>
		/// 
		/// </summary>
		/// <param name="msg"></param>
		void StatusMessage(string msg);

		/// <summary>
		/// 
		/// </summary>
		void StartProgress(int start, int end);

		/// <summary>
		/// 
		/// </summary>
		void EndProgress();

		/// <summary>
		/// 
		/// </summary>
		/// <param name="percent"></param>
		int Progress
		{
			get;
			set;
		}

		/// <summary>
		/// 
		/// </summary>
		bool ZoomEnable
		{
			get;
			set;
		}

		/// <summary>
		/// 
		/// </summary>
		int ZoomValue
		{
			get;
			set;
		}

		/// <summary>
		/// 
		/// </summary>
		int ZoomMax
		{
			get;
			set;
		}

		/// <summary>
		/// 
		/// </summary>
		int ZoomMin
		{
			get;
			set;
		}

		/// <summary>
		/// 
		/// </summary>
		string ZoomMsg
		{
			get;
			set;
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="e"></param>
		void AddZoomEvent(EventHandler e);

		/// <summary>
		/// 
		/// </summary>
		/// <param name="e"></param>
		void RemoveZoomEvent(EventHandler e);
	}
}
