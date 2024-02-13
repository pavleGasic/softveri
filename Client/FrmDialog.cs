using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Client
{
    public partial class FrmDialog : Form
    {
        #region Initialize mouse capture
        public const int WM_NCLBUTTONDOWN = 0xA1;
        public const int HT_CAPTION = 0x2;

        [DllImportAttribute("user32.dll")]
        public static extern int SendMessage(IntPtr hWnd, int Msg, int wParam, int lParam);
        [DllImportAttribute("user32.dll")]
        public static extern bool ReleaseCapture();
        #endregion 
        public FrmDialog()
        {
            this.ControlBox = false;
            this.Text = String.Empty;
            this.FormBorderStyle = FormBorderStyle.FixedSingle;            
            InitializeComponent();
            HandleControlEvents();
        }

        #region Handle top window control
        private void HandleControlEvents()
        {
            this.ucWindowTop1.CloseButtonClicked += WindowTopControl_CloseClicked;
            this.ucWindowTop1.MinimizeButtonClicked += WindowTopControl_MinimizeClicked;
            this.ucWindowTop1.PanelMouseDown += WindowTopControl_PanelMouseDown;
        }
        private void WindowTopControl_MinimizeClicked(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void WindowTopControl_CloseClicked(object sender, EventArgs e)
        {
            this.Close();
        }

        private void WindowTopControl_PanelMouseDown(object sender, MouseEventArgs e)
        {
            ReleaseCapture();
            SendMessage(this.Handle, WM_NCLBUTTONDOWN, HT_CAPTION, 0);
        }
        #endregion
    }
}
