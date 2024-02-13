using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Server
{
    public partial class FrmServer : Form
    {
        #region Initialize mouse capture
        public const int WM_NCLBUTTONDOWN = 0xA1;
        public const int HT_CAPTION = 0x2;

        [DllImportAttribute("user32.dll")]
        public static extern int SendMessage(IntPtr hWnd, int Msg, int wParam, int lParam);
        [DllImportAttribute("user32.dll")]
        public static extern bool ReleaseCapture();
        #endregion 

        private ServerLogic server;
        public FrmServer()
        {
            this.ControlBox = false;
            this.Text = String.Empty;
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            InitializeComponent();
            HandleControleEvents();    
        }

        #region Handle top window control
        private void HandleControleEvents()
        {
            ucWindowTop1.CloseButtonClicked += WindowTopControl_CloseClicked;
            ucWindowTop1.MaximizeButtonClicked += WindowTopControl_MaximizeClicked;
            ucWindowTop1.MinimizeButtonClicked += WindowTopControl_MinimizeClicked;
            ucWindowTop1.PanelMouseDown += WindowTopControl_PanelMouseDown;
        }

        private void WindowTopControl_MinimizeClicked(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void WindowTopControl_MaximizeClicked(object sender, EventArgs e)
        {
            if (this.WindowState == FormWindowState.Maximized)
            {
                this.WindowState = FormWindowState.Normal;
            }
            else
            {
                this.WindowState = FormWindowState.Maximized;
            }
        }

        private void WindowTopControl_CloseClicked(object sender, EventArgs e)
        {
            Environment.Exit(0);
        }

        private void WindowTopControl_PanelDoubleClicked(object sender, EventArgs e)
        {
            if (this.WindowState == FormWindowState.Maximized)
            {
                this.WindowState = FormWindowState.Normal;
            }
            else
            {
                this.WindowState = FormWindowState.Maximized;
            }
        }

        private void WindowTopControl_PanelMouseDown(object sender, MouseEventArgs e)
        {
            ReleaseCapture();
            SendMessage(this.Handle, WM_NCLBUTTONDOWN, HT_CAPTION, 0);
        }
        private void panel2_MouseDown(object sender, MouseEventArgs e)
        {
            ReleaseCapture();
            SendMessage(this.Handle, WM_NCLBUTTONDOWN, HT_CAPTION, 0);
        }
        #endregion

        private void btnStart_Click(object sender, EventArgs e)
        {
            server = new ServerLogic(this);
            btnStop.Enabled = true;
            btnStart.Enabled = false;
            server.Start();
            lblServerState.Text = "Server je pokrenut.";
        }

        private void btnStop_Click(object sender, EventArgs e)
        {
            btnStart.Enabled = true;
            btnStop.Enabled = false;
            server.Stop();
            lblServerState.Text = "Server nije pokrenut.";
        }

        
    }
}
