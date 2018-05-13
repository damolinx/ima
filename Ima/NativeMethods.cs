using System;
using System.Runtime.InteropServices;
using System.Text;

namespace Ima
{
    internal static class NativeMethods
    {
        /// <summary>
        /// "Shared Documents" constant to be used with SHGetSpecialFolderPath
        /// </summary>
        public static readonly int CSIDL_COMMON_DOCUMENTS = 0x002e;

        /// <summary>
        /// Native call
        /// </summary>
        [DllImport("shell32.dll")]
        public static extern bool SHGetSpecialFolderPath(IntPtr hwndOwner, [Out]StringBuilder lpszPath, int nFolder, bool fCreate);

        /// <summary>
        /// Constant for SystemParametersInfo
        /// </summary>
        public static readonly int SPI_SETDESKWALLPAPER = 20;
        /// <summary>
        /// Constant for SystemParametersInfo
        /// </summary>
        public static readonly int SPIF_UPDATEINIFILE = 0x01;
        /// <summary>
        /// Constant for SystemParametersInfo
        /// </summary>
        public static readonly int SPIF_SENDWININICHANGE = 0x02;

        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        public static extern int SystemParametersInfo(int uAction, int uParam, string lpvParam, int fuWinIni);

        [DllImport("user32.dll")]
        public static extern bool SetProcessDPIAware();

        // Performs an operation on a specified file.
        [DllImport("shell32.dll")]
        public static extern IntPtr ShellExecuteA(
            IntPtr hwnd,               // Handle to a parent window.
            [MarshalAs(UnmanagedType.LPTStr)]
            String lpOperation,   // Pointer to a null-terminated string, referred to in 
                                  // this case as a verb, that specifies the action to 
                                  // be performed.
            [MarshalAs(UnmanagedType.LPTStr)]
            String lpFile,        // Pointer to a null-terminated string that specifies 
                                  // the file or object on which to execute the specified 
                                  // verb.
            [MarshalAs(UnmanagedType.LPTStr)]
            String lpParameters,  // If the lpFile parameter specifies an executable file, 
                                  // lpParameters is a pointer to a null-terminated string 
                                  // that specifies the parameters to be passed to the 
                                  // application.
            [MarshalAs(UnmanagedType.LPTStr)]
            String lpDirectory,   // Pointer to a null-terminated string that specifies
                                  // the default directory. 
            Int32 nShowCmd);      // Flags that specify how an application is to be 
                                  // displayed when it is opened.

        [DllImport("shell32.dll", EntryPoint = "ShellExecute")]
        public static extern IntPtr ShellExecute(IntPtr hwnd,
            [MarshalAs(UnmanagedType.LPTStr)]
            string lpOperation,
            string lpFile,
            string lpParameters,
            string lpDirectory,
            int nShowCmd);

        public enum LVS_EX
        {
            LVS_EX_GRIDLINES = 0x00000001,
            LVS_EX_SUBITEMIMAGES = 0x00000002,
            LVS_EX_CHECKBOXES = 0x00000004,
            LVS_EX_TRACKSELECT = 0x00000008,
            LVS_EX_HEADERDRAGDROP = 0x00000010,
            LVS_EX_FULLROWSELECT = 0x00000020,
            LVS_EX_ONECLICKACTIVATE = 0x00000040,
            LVS_EX_TWOCLICKACTIVATE = 0x00000080,
            LVS_EX_FLATSB = 0x00000100,
            LVS_EX_REGIONAL = 0x00000200,
            LVS_EX_INFOTIP = 0x00000400,
            LVS_EX_UNDERLINEHOT = 0x00000800,
            LVS_EX_UNDERLINECOLD = 0x00001000,
            LVS_EX_MULTIWORKAREAS = 0x00002000,
            LVS_EX_LABELTIP = 0x00004000,
            LVS_EX_BORDERSELECT = 0x00008000,
            LVS_EX_DOUBLEBUFFER = 0x00010000,
            LVS_EX_HIDELABELS = 0x00020000,
            LVS_EX_SINGLEROW = 0x00040000,
            LVS_EX_SNAPTOGRID = 0x00080000,
            LVS_EX_SIMPLESELECT = 0x00100000
        }
        public enum LVM
        {
            LVM_FIRST = 0x1000,
            LVM_SETEXTENDEDLISTVIEWSTYLE = (LVM_FIRST + 54),
            LVM_GETEXTENDEDLISTVIEWSTYLE = (LVM_FIRST + 55),
        }


        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        public static extern int SendMessage(IntPtr handle, int msg, IntPtr wparam, IntPtr lparam);

        /// <summary>
        /// 
        /// </summary>
        public enum ShowWindowCommands
        {
            SW_HIDE = 0,    // Hides the window and activates another window.
            SW_SHOWNORMAL = 1,  // Sets the show state based on the SW_ flag specified in the STARTUPINFO 
            SW_NORMAL = 1,  // structure passed to the CreateProcess function by the program that started the application.
            SW_SHOWMINIMIZED = 2,   // Activates the window and displays it as a minimized window.
            SW_SHOWMAXIMIZED = 3,   // Maximizes the specified window.
            SW_MAXIMIZE = 3,    // Activates the window and displays it as a maximized window.
            SW_SHOWNOACTIVATE = 4,  // Displays a window in its most recent size and position. The active window remains active.
            SW_SHOW = 5,    // Activates the window and displays it in its current size and position.
            SW_MINIMIZE = 6,    // Minimizes the specified window and activates the next top-level window in the z-order.
            SW_SHOWMINNOACTIVE = 7, // Displays the window as a minimized window. The active window remains active.
            SW_SHOWNA = 8,  // Displays the window in its current state. The active window remains active.
            SW_RESTORE = 9, // Activates and displays the window.
            SW_SHOWDEFAULT = 10,
        }

        // Common verbs
        public const string SHEXEC_OpenFile = "open";
        // Opens the file specified by the 
        // lpFile parameter. The file can 
        // be an executable file, a document 
        // file, or a folder.

        public const string SHEXEC_EditFile = "edit";
        // Launches an editor and opens the 
        // document for editing. If lpFile 
        // is not a document file, the 
        // function will fail.

        public const string SHEXEC_ExploreFolder = "explore";
        // Explores the folder specified by 
        // lpFile.

        public const string SHEXEC_FindInFolder = "find";
        // Initiates a search starting from 
        // the specified directory.

        public const string SHEXEC_PrintFile = "print";
        // Prints the document file specified 
        // by lpFile. If lpFile is not a 
        // document file, the function will 
        // fail.

        public static void PrintFiles(params string[] files)
        {
            try
            {
                WIA.CommonDialog dialog = new WIA.CommonDialogClass();
                WIA.VectorClass vector = new WIA.VectorClass();

                for (int i = 0; i < files.Length; i++)
                {
                    object obj = files[i];
                    vector.Add(ref obj, i);
                }
                object vecObj = vector;
                dialog.ShowPhotoPrintingWizard(ref vecObj);
            }
            catch
            {
            }
        }
    }
}
