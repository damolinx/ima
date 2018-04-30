using System;
using System.IO;
using System.Drawing;

using System.Collections;
using System.Windows.Forms;
using Microsoft.ApplicationBlocks.ConfigurationManagement;

namespace Ima
{
    /// <summary>
    /// Summary description for Configuration.
    /// </summary>
    public class Configuration
    {
        /// <summary>
        /// Static instance
        /// </summary>
        static Configuration instance = null;

        /// <summary>
        /// Files to open
        /// </summary>
        ArrayList fileList = new ArrayList();

        /// <summary>
        /// Properties
        /// </summary>
        Hashtable properties;

        public static readonly string APPLICATION_NAME = "Image Manipulation Application";
        public static readonly string APPLICATION_VERSION = "1.0";
        public static readonly string APPLICATION_IMAGE_SUPPORTED = "*.bmp;*.emf;*.exif;*.gif;*.ico;*.jpg;*.jpeg;*.png;*.tif;*.tiff;*.wmf";

        public static readonly string VALUE_ON = "ON";
        public static readonly string VALUE_OFF = "OFF";
        public static readonly string STYLE_NOTNICE = "Style.NotNice";

        public static readonly string UNDO_DEPTH = "Undo.Depth";

        public static readonly string SUFFIX_CONTROL_SIZE = ".Control.Size";
        public static readonly string SUFFIX_CONTROL_LOCATION = ".Control.Location";
        public static readonly string SUFFIX_CONTROL_VISIBILITY = ".Control.Visible";
        public static readonly string SUFFIX_PREVIEW_USE_THUMBNAIL = ".ViewThumbnail";

        public static readonly string USER_PLACES = "User.Places";

        public static readonly string LIBRARY_NOLOADSYSTEM = "Library.NoLoadSystem";
        public static readonly string LIBRARY_NOLOADUSER = "Library.NoLoadUser";

        /// <summary>
        /// Protected Constructor to enforce singleton
        /// </summary>
        protected Configuration()
        {
            ConfigurationManager.Initialize();
            this.properties = ConfigurationManager.Read();
        }

        /// <summary>
        /// Saves current configuration
        /// </summary>
        public void Close()
        {
            ConfigurationManager.Write(this.properties);
        }

        /// <summary>
        /// Singleton Class
        /// </summary>
        /// <returns></returns>
        public static Configuration Instance
        {
            get
            {
                return (instance == null) ? (instance = new Configuration()) : instance;
            }
        }

        /// <summary>
        /// Serialized Recent files list
        /// </summary>
        public IList FilesToOpen
        {
            get
            {
                return this.fileList;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public object GetProperty(object key, object def)
        {
            return (this.properties.ContainsKey(key)) ? this.properties[key] : def;
        }

        /// <summary>
        /// 
        /// </summary>
        public void SetProperty(object key, object def)
        {
            this.properties[key] = def;
        }

        /// <summary>
        /// Test if property value is VALUE_ON
        /// </summary>
        public bool IsActive(object key)
        {
            if (this.properties.ContainsKey(key))
            {
                return String.Compare(Configuration.VALUE_ON, this.properties[key].ToString()) == 0;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="control"></param>
        public void SaveVisibility(Control control)
        {
            string strID = (control.Name.Length > 0) ? control.Name : control.Text;

            //Avoid un-named controls to overwrite each other
            if (strID.Length == 0)
                return;

            this.SetProperty(strID + SUFFIX_CONTROL_VISIBILITY, (control.Visible) ? VALUE_ON : VALUE_OFF);
        }

        /// <summary>
        /// 
        /// </summary>
        public void RestoreVisibility(Control control)
        {
            string strID = (control.Name.Length > 0) ? control.Name : control.Text;

            //Avoid un-named controls to overwrite each other
            if (strID.Length == 0)
                return;

            string prop = (string)this.GetProperty(strID + SUFFIX_CONTROL_VISIBILITY, string.Empty);

            if (prop.Equals(VALUE_ON))
            {
                control.Visible = !prop.Equals(VALUE_OFF);
            }
            else if (prop.Equals(VALUE_OFF))
            {
                control.Visible = false;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="control"></param>
        public void SaveSize(Control control)
        {
            string strID = (control.Name.Length > 0) ? control.Name : control.Text;

            //Avoid un-named controls to overwrite each other
            if (strID.Length == 0)
                return;

            this.SetProperty(strID + SUFFIX_CONTROL_SIZE, control.Size.Width + "," + control.Size.Height);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="control"></param>
        public void RestoreSize(Control control)
        {
            string strID = (control.Name.Length > 0) ? control.Name : control.Text;

            //Avoid un-named controls to overwrite each other
            if (strID.Length == 0)
                return;

            string prop = (string)this.GetProperty(strID + SUFFIX_CONTROL_SIZE, string.Empty);

            if (prop != string.Empty)
            {
                int i = prop.IndexOf(",");
                if (i != -1)
                {
                    int W, H;
                    try
                    {
                        W = Convert.ToInt32(prop.Substring(0, i));
                        H = Convert.ToInt32(prop.Substring(i + 1));
                    }
                    catch
                    {
                        this.SetProperty(strID + SUFFIX_CONTROL_SIZE, string.Empty);
                        return;
                    }
                    control.Size = new Size(W, H);
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="control"></param>
        public void SaveLocation(Control control)
        {
            string strID = (control.Name.Length > 0) ? control.Name : control.Text;

            //Avoid un-named controls to overwrite each other
            if (strID.Length == 0)
                return;

            this.SetProperty(strID + SUFFIX_CONTROL_LOCATION, control.Location.X + "," + control.Location.Y);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="control"></param>
        public void RestoreLocation(Control control)
        {
            string strID = (control.Name.Length > 0) ? control.Name : control.Text;

            //Avoid un-named controls to overwrite each other
            if (strID.Length == 0)
                return;

            string prop = (string)this.GetProperty(strID + SUFFIX_CONTROL_LOCATION, string.Empty);
            if (prop != string.Empty)
            {
                int i = prop.IndexOf(",");
                if (i != -1)
                {
                    int X, Y;
                    try
                    {
                        X = Convert.ToInt32(prop.Substring(0, i));
                        Y = Convert.ToInt32(prop.Substring(i + 1));
                    }
                    catch
                    {
                        this.SetProperty(strID + SUFFIX_CONTROL_LOCATION, string.Empty);
                        return;
                    }
                    control.Location = new Point(X, Y);
                }
            }
        }

        public string Path_Library_Base
        {
            get
            {
                return Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Personal), APPLICATION_NAME);
            }
        }

        public string Path_MyPictures
        {
            get
            {
                return Environment.GetFolderPath(Environment.SpecialFolder.MyPictures);
            }
        }
    }
}
