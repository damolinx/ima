using System;

namespace Ima.Controls
{
    public partial class ToolForm : System.Windows.Forms.Form
    {
        public ToolForm()
        {
            InitializeComponent();
        }

        protected virtual void Apply()
        {
            this.Close();
        }

        protected virtual void Cancel()
        {
            this.Close();
        }

        #region Private

        private void ApplyButton_Click(object sender, EventArgs e)
        {
            this.Apply();
        }

        private void CancelButton_Click(object sender, EventArgs e)
        {
            this.Cancel();
        }

        #endregion
    }
}
