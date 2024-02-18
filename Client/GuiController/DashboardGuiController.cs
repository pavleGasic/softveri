using Client.ClientCommunication;
using Client.GuiController.ReservationControllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Client.GuiController
{
    internal class DashboardGuiController
    {
        private static DashboardGuiController instance;
        public static DashboardGuiController Instance
        {
            get
            {
                if (instance == null)
                    instance = new DashboardGuiController();
                return instance;
            }
        }
        private DashboardGuiController() { }

        private FrmDashboard frmDashboard;

        #region Initialize mouse capture
        public const int WM_NCLBUTTONDOWN = 0xA1;
        public const int HT_CAPTION = 0x2;

        [DllImportAttribute("user32.dll")]
        public static extern int SendMessage(IntPtr hWnd, int Msg, int wParam, int lParam);
        [DllImportAttribute("user32.dll")]
        public static extern bool ReleaseCapture();
        #endregion
        #region Handle top window control
        private void HandleControlEvents()
        {
            frmDashboard.ucWindowTop1.CloseButtonClicked += WindowTopControl_CloseClicked;
            frmDashboard.ucWindowTop1.MinimizeButtonClicked += WindowTopControl_MinimizeClicked;
            frmDashboard.ucWindowTop1.PanelMouseDown += WindowTopControl_PanelMouseDown;
            frmDashboard.ucWindowTop1.MaximizeButtonClicked += WindowTopControl_MaximizeClicked;
            frmDashboard.ucSidebar1.btnSignOut.Click += SignOut_Click;
            frmDashboard.ucSidebar1.btnCustomer.Click += Customer_Click;
            frmDashboard.ucSidebar1.btnFilm.Click += Film_Click;
            frmDashboard.ucSidebar1.btnReservation.Click += Reservation_Click;
        }

        private void WindowTopControl_MinimizeClicked(object sender, EventArgs e)
        {
            frmDashboard.WindowState = FormWindowState.Minimized;
        }

        private void WindowTopControl_MaximizeClicked(object sender, EventArgs e)
        {
            if (frmDashboard.WindowState == FormWindowState.Maximized)
            {
                frmDashboard.WindowState = FormWindowState.Normal;
            }
            else
            {
                frmDashboard.WindowState = FormWindowState.Maximized;
            }
        }

        private void WindowTopControl_CloseClicked(object sender, EventArgs e)
        {
            Environment.Exit(0);
        }

        private void WindowTopControl_PanelMouseDown(object sender, MouseEventArgs e)
        {
            ReleaseCapture();
            SendMessage(frmDashboard.Handle, WM_NCLBUTTONDOWN, HT_CAPTION, 0);
        }
        #endregion
        internal void ShowDashboardForm()
        {
            frmDashboard = new FrmDashboard();
            InitializeComponentsValues();
            HandleControlEvents();
            frmDashboard.Show();
        }

        private void InitializeComponentsValues()
        {
            if(Session.Instance.Worker != null)
            {
                frmDashboard.ucSidebar1.lblHello.Text = $"Hello, {Session.Instance.Worker.Username}";
                frmDashboard.ucSidebar1.lblName.Text = $"{Session.Instance.Worker.FirstName} {Session.Instance.Worker.LastName}";
            }
        }

        private void SignOut_Click(object sender, EventArgs e)
        {
            Communication.Instance.Logout();
            LoginGuiController.Instance.FromMakeVisible();
            Session.Instance.Worker = null;
            frmDashboard.Close();
        }
        private void Customer_Click(object sender, EventArgs e)
        {
            CustomersGuiController.Instance.ShowCustomerControl(frmDashboard.pnlMain);
        }
        private void Film_Click(object sender, EventArgs e)
        {
            FilmGuiController.Instance.ShowFilmControl(frmDashboard.pnlMain);
        }

        private void Reservation_Click(object sender, EventArgs e)
        {
            ReservationGuiController.Instance.ShowReservationControl(frmDashboard.pnlMain);
        }
    }
}
