using Client.ClientCommunication;
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
            frmLogin = new FrmLogin();
            HandleControlEvents();
            Application.Run(frmLogin);
        }

        private void HandleControlEvents()
        {
            frmLogin.ucLogin1.ButtonClicked += LoginControl_ButtonClicked;
            frmLogin.ucLogin1.LinkClicked += LoginControl_LinkClicked;
            frmLogin.ucRegister1.ButtonClicked += RegisterControl_ButtonClicked;
            frmLogin.ucRegister1.LinkClicked += RegisterControl_LinkClicked;
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
            try
            {
                Communication.Instance.Connect();
                Worker response = Communication.Instance.Login(worker);
                HandleGoToDashboard(response);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Login error!", MessageBoxButtons.OK, MessageBoxIcon.Error);    
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

            try
            {
                Communication.Instance.Connect();
                Worker response = Communication.Instance.Register(worker);
                Worker w = new Worker()
                {
                    FirstName = worker.FirstName,
                    LastName = worker.LastName,
                    Password = worker.Password,
                    Username = worker.Username,
                    WorkerId = response.WorkerId
                };
                HandleGoToDashboard(w);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Login error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
            string errorMessage = string.Empty;
            if (!GuiHelper.IsTextBoxValid(frmLogin.ucRegister1.txtPassword.Text) ||
                    !GuiHelper.IsTextBoxValid(frmLogin.ucRegister1.txtUsername.Text) ||
                    !GuiHelper.IsTextBoxValid(frmLogin.ucRegister1.txtFirstname.Text) ||
                    !GuiHelper.IsTextBoxValid(frmLogin.ucRegister1.txtLastname.Text))
            {
                errorMessage += "Every field is required!\n";
            }
            if (!GuiHelper.IsPasswordValid(frmLogin.ucRegister1.txtPassword.Text))
            {
                errorMessage += "Password must contain at least one capital letter and one number!\n";
            }
            if(errorMessage ==  string.Empty)
            {
                return true;
            }
            MessageBox.Show(errorMessage, "Validation error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            return false;
        }

        public bool ValidateLogin()
        {
            if (!GuiHelper.IsTextBoxValid(frmLogin.ucLogin1.txtPassword.Text) ||
                    !GuiHelper.IsTextBoxValid(frmLogin.ucLogin1.txtUsername.Text))
            {
                MessageBox.Show("Every field is required", "Validation error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                frmLogin.ucLogin1.lblErrorLogin.Text = "Every field is required";
                return false;
            }
            return true;
        }
        #endregion
    }
}
