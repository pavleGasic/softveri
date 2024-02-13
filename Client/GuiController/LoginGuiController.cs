using Client.UserControls;
using Common.Communication;
using Common.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Client.GuiController
{
    internal class LoginGuiController
    {
        private static LoginGuiController instance;
        public static LoginGuiController Instance
        {
            get
            {
                if(instance == null)
                    instance = new LoginGuiController();
                return instance;
            }
        }

        private LoginGuiController() { }

        private FrmLogin frmLogin;

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
            frmLogin.ucWindowTop1.CloseButtonClicked += WindowTopControl_CloseClicked;
            frmLogin.ucWindowTop1.MinimizeButtonClicked += WindowTopControl_MinimizeClicked;
            frmLogin.ucWindowTop1.PanelMouseDown += WindowTopControl_PanelMouseDown;
            frmLogin.ucLogin1.ButtonClicked += LoginControl_ButtonClicked;
            frmLogin.ucLogin1.LinkClicked += LoginControl_LinkClicked;
            frmLogin.ucRegister1.ButtonClicked += RegisterControl_ButtonClicked;
            frmLogin.ucRegister1.LinkClicked += RegisterControl_LinkClicked;
        }

        private void WindowTopControl_MinimizeClicked(object sender, EventArgs e)
        {
            frmLogin.WindowState = FormWindowState.Minimized;
        }

        private void WindowTopControl_CloseClicked(object sender, EventArgs e)
        {
            Environment.Exit(0);
        }

        private void WindowTopControl_PanelDoubleClicked(object sender, EventArgs e)
        {
            if (frmLogin.WindowState == FormWindowState.Maximized)
            {
                frmLogin.WindowState = FormWindowState.Normal;
            }
            else
            {
                frmLogin.WindowState = FormWindowState.Maximized;
            }
        }

        private void WindowTopControl_PanelMouseDown(object sender, MouseEventArgs e)
        {
            ReleaseCapture();
            SendMessage(frmLogin.Handle, WM_NCLBUTTONDOWN, HT_CAPTION, 0);
        }
        #endregion

        #region Handle navigation
        private void LoginControl_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            frmLogin.ucLogin1.Visible = false;
            frmLogin.ucLogin1.txtPassword.Text = string.Empty;
            frmLogin.ucLogin1.txtUsername.Text = string.Empty;
            frmLogin.ucRegister1.Visible = true;
        }

        private void RegisterControl_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            frmLogin.ucLogin1.Visible = true;
            frmLogin.ucRegister1.Visible = false;
            frmLogin.ucRegister1.txtPassword.Text = string.Empty;
            frmLogin.ucRegister1.txtUsername.Text = string.Empty;
            frmLogin.ucRegister1.txtFirstname.Text = string.Empty;
            frmLogin.ucRegister1.txtLastname.Text = string.Empty;
        }
        #endregion

        internal void ShowLoginForm()
        {
            Communication.Instance.Connect();
            frmLogin = new FrmLogin();
            HandleControlEvents();
            Application.Run(frmLogin);
        }
        internal void FromMakeVisible()
        {
            frmLogin.ucLogin1.txtPassword.Text = string.Empty;
            frmLogin.ucLogin1.txtUsername.Text= string.Empty;
            frmLogin.ucRegister1.txtFirstname.Text = string.Empty;
            frmLogin.ucRegister1.txtLastname.Text = string.Empty;
            frmLogin.ucRegister1.txtPassword.Text = string.Empty;
            frmLogin.ucRegister1.txtUsername.Text = string.Empty;
            frmLogin.ucRegister1.lblErrorRegister.Text = string.Empty;
            frmLogin.ucLogin1.lblErrorLogin.Text = string.Empty;
            frmLogin.ucLogin1.Visible = true;
            frmLogin.ucRegister1.Visible = false;
            frmLogin.Visible = true;
        }

        public void LoginControl_ButtonClicked(object sender, EventArgs e)
        {
            if (!ValidateLogin()) return;
            Worker worker = new Worker() 
            {
                Username = frmLogin.ucLogin1.txtUsername.Text,
                Password = frmLogin.ucLogin1.txtPassword.Text
            };
            Response response = Communication.Instance.Login(worker);
            if (response.Exception == null)
            {
                HandleGoToDashboard((Worker)response.Result);
            }
            else
            {
                frmLogin.ucLogin1.lblErrorLogin.Text = "Invalid username or password";
            }
        }

        public void RegisterControl_ButtonClicked(object sender, EventArgs e)
        {
            if (!ValidateRegister()) return;
            Worker worker = new Worker()
            {
                FirstName = frmLogin.ucRegister1.txtFirstname.Text,
                LastName = frmLogin.ucRegister1.txtLastname.Text,
                Password = frmLogin.ucRegister1.txtPassword.Text,
                Username = frmLogin.ucRegister1.txtUsername.Text
            };
            Response response = Communication.Instance.Register(worker);
            if (response.Exception == null)
            {
                Worker w = new Worker()
                {
                    FirstName = worker.FirstName, LastName = worker.LastName,
                    Password = worker.Password, Username = worker.Username,
                    WorkerId = (int)response.Result
                };
                HandleGoToDashboard(w);
            }
            else
            {
                frmLogin.ucRegister1.lblErrorRegister.Text = "Register failed";
            }
        }

        private void HandleGoToDashboard(Worker w)
        {
            Session.Instance.Worker = w;
            DashboardGuiController.Instance.ShowDashboardForm();
            frmLogin.Visible = false;
        }

        #region Input validation
        public bool ValidateRegister()
        {
            if (!GuiHelper.IsPasswordValid(frmLogin.ucRegister1.txtPassword.Text))
            {
                frmLogin.ucRegister1.lblErrorRegister.Text = "Pass must contain 1 capital letter and number";
                return false;
            }
            else
            {
                if(!GuiHelper.IsTextBoxValid(frmLogin.ucRegister1.txtPassword.Text) ||
                    !GuiHelper.IsTextBoxValid(frmLogin.ucRegister1.txtUsername.Text) ||
                    !GuiHelper.IsTextBoxValid(frmLogin.ucRegister1.txtFirstname.Text)||
                    !GuiHelper.IsTextBoxValid(frmLogin.ucRegister1.txtLastname.Text))
                {
                    frmLogin.ucRegister1.lblErrorRegister.Text = "Every field is required";
                    return false;
                }
            }
            return true;
        }

        public bool ValidateLogin()
        {
            if (!GuiHelper.IsTextBoxValid(frmLogin.ucLogin1.txtPassword.Text) ||
                    !GuiHelper.IsTextBoxValid(frmLogin.ucLogin1.txtUsername.Text))
            {
                frmLogin.ucLogin1.lblErrorLogin.Text = "Every field is required";
                return false;
            }
            return true;
        }
        #endregion
    }
}
