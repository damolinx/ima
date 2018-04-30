using System;
using System.IO;
using System.Windows.Forms;

using Ima.Library;

namespace Ima
{
	/// <summary>
	/// Summary description for Main.
	/// </summary>
	public sealed class MainClass
	{
		/// <summary>
		/// Static Instance
		/// </summary>
		static MainClass instance = null;

		/// <summary>
		/// Main form instance
		/// </summary>
		MainForm frmMain;

		/// <summary>
		/// Main Library instance
		/// </summary>
		LibraryManager mgrLibrary;

		#region Constructors & Initialization
		/// <summary>
		/// Constructor
		/// </summary>
		private MainClass()
		{
			MainClass.instance = this;
			this.mgrLibrary    = new LibraryManager();
			this.frmMain       = new MainForm();

		}

		/// <summary>
		/// Execute after Process Args so application is initted with proper settings
		/// </summary>
		public void Init()
		{
			//Verify Library Base
			if (!Directory.Exists(Configuration.Instance.Path_Library_Base))
			{
				try
				{
					Directory.CreateDirectory(Configuration.Instance.Path_Library_Base);
				}
				catch (Exception ex)
				{
					MainClass.Instance.WriteError("Application was unable to create base directory.  Certain features may not work as expected", ex); 
					Application.Exit();
				}
			}
			
			//Library Manager Init
			
			this.frmMain.LibraryManager = this.mgrLibrary;

			if (!Configuration.Instance.IsActive(Configuration.LIBRARY_NOLOADSYSTEM))
			{
				this.mgrLibrary.loadDefaults();
			}
			if (!Configuration.Instance.IsActive(Configuration.LIBRARY_NOLOADUSER))
			{
				this.mgrLibrary.loadUserItems();
			}

			//TODO
			if (Configuration.Instance.IsActive(Configuration.STYLE_NOTNICE))
			{
				Application.EnableVisualStyles();
			}
		}
		#endregion

		#region Properties
		/// <summary>
		/// 
		/// </summary>
		public static MainClass Instance
		{
			get
			{
				if(MainClass.instance == null)
				{
					new MainClass();
				}
				return MainClass.instance;
			}
		}
		/// <summary>
		/// 
		/// </summary>
		public Form MainForm
		{
			get
			{
				return this.frmMain;
			}
		}
		/// <summary>
		/// 
		/// </summary>
		public LibraryManager LibraryManager
		{
			get
			{
				return this.mgrLibrary;
			}
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="msg"></param>
		public void WriteError(string msg, Exception e)
		{
			Console.Error.WriteLine(msg);
		}
		#endregion

		#region Initialization

		/// <summary>
		/// The main entry point for the application.
		/// </summary>
		/// <param name="args">Application Arguments</param>
		[STAThread]
		public static void Main(string[] args)
		{
			//Set initial configuration.  Default values are overriden by cmdln args
			MainClass.Instance.ProcessArgs(args);
			//Init application
			MainClass.Instance.Init();
			//Process pending events
			Application.DoEvents();
			//Start Application
			Application.Run(MainClass.Instance.MainForm);
		}
		/// <summary>
		/// Parses cmd arguments
		/// </summary>
		/// <param name="args"></param>
		public void ProcessArgs(string[] args)
		{
			for(int i = 0; i < args.Length; i++)
			{
				if (args[i][0] != '/')
				{
					if (File.Exists(args[i]))  //TODO test to remove unsupported to
					{
						Configuration.Instance.FilesToOpen.Add(args[0]);
					}
					else
					{
						Console.Error.WriteLine("[EE] Ignoring unknown file '" + args[i] + "'");
					}
				}
				else
				{
					switch(args[i].ToLower())
					{
						case "/notnice":
							Configuration.Instance.SetProperty(Configuration.STYLE_NOTNICE, Configuration.VALUE_ON);
							break;
						default:
							Console.Error.WriteLine("[EE] Ignoring unknown parameter '" + args[i] + "'");
							break;
					}
				}
			}
		}
		#endregion
	}
}
