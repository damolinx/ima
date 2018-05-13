using Ima.Library;
using System;
using System.IO;
using System.Windows.Forms;

namespace Ima
{
    /// <summary>
    /// Summary description for Main.
    /// </summary>
    public sealed class MainClass
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        /// <param name="args">Application Arguments</param>
        [STAThread]
        public static void Main(params string[] args)
        {
            var main = new MainClass();

            //Set initial configuration.  Default values are overriden by cmdln args
            main.ProcessArgs(args);

            //Init application
            main.Init();

            //Process pending events
            Application.DoEvents();

            //Start Application
            Application.Run(main.MainForm);
        }


        /// <summary>
        /// Constructor
        /// </summary>
        private MainClass()
        {
            MainForm = new MainForm
            {
                LibraryManager = new LibraryManager()
            };
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
                    Console.Error.WriteLine("Application was unable to create base directory.  Certain features may not work as expected", ex);
                    Application.Exit();
                }
            }

            // Library Manager Init

            if (!Configuration.Instance.IsActive(Configuration.LIBRARY_NOLOADSYSTEM))
            {
                MainForm.LibraryManager.loadDefaults();
            }
            if (!Configuration.Instance.IsActive(Configuration.LIBRARY_NOLOADUSER))
            {
                MainForm.LibraryManager.loadUserItems();
            }

            //TODO
            if (Configuration.Instance.IsActive(Configuration.STYLE_NOTNICE))
            {
                Application.EnableVisualStyles();
            }
        }

        public MainForm MainForm { get; }

        /// <summary>
        /// Parses cmd arguments
        /// </summary>
        public void ProcessArgs(params string[] args)
        {
            for (int i = 0; i < args.Length; i++)
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
                    switch (args[i].ToLowerInvariant())
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
    }
}
