using Client.UserControls;
using Common.Communication;
using Common.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Client.GuiController
{
    internal class CustomersGuiController
    {
        private static CustomersGuiController instance;
        public static CustomersGuiController Instance
        {
            get
            {
                if (instance == null)
                    instance = new CustomersGuiController();
                return instance;
            }
        }
        private CustomersGuiController() { }

        private UCCustomer ucCustomer;
        private UCAddCustomer ucAddCustomer;
        private UCUpdateCustomer ucUpdateCustomer;
        private FrmDialog frmDialog;
        private Customer selectedCustomer;
        internal void ShowCustomerControl(Panel panel)
        {
            ucCustomer = new UCCustomer();
            panel.Controls.Clear();
            ucCustomer.Dock = DockStyle.Fill;
            HandleControls();
            panel.Controls.Add(ucCustomer);
            RequestForGetCustomers(ucCustomer.txtSearch.Text);
        }

        private void HandleControls()
        {
            ucCustomer.btnAdd.Click += AddCustomer_Click;
            ucCustomer.btnSearch.Click += SearchCustomer_Click;
            ucCustomer.btnUpdate.Click += UpdateCustomer_Click;
        }

        private void UpdateCustomer_Click(object sender, EventArgs e)
        {
            if (ucCustomer.dataGridView1.SelectedRows.Count == 1)
            {
                selectedCustomer = (Customer)ucCustomer.dataGridView1.SelectedRows[0].DataBoundItem;
                frmDialog = new FrmDialog();
                ucUpdateCustomer = new UCUpdateCustomer();
                frmDialog.panel1.Controls.Add(ucUpdateCustomer);
                ucUpdateCustomer.Dock = DockStyle.Fill;
                ucUpdateCustomer.txtEmail.Text = selectedCustomer.Email;
                ucUpdateCustomer.txtFirstName.Text = selectedCustomer.FirstName;
                ucUpdateCustomer.txtLastName.Text = selectedCustomer.LastName;
                ucUpdateCustomer.btnUpdate.Click += UpdateCustomerDialog_Click;
                frmDialog.ShowDialog();
            }
            else
            {
                MessageBox.Show("You must select row first", "Invalid operation", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void UpdateCustomerDialog_Click(object sender, EventArgs e)
        {
            if (!GuiHelper.ValidateAddCustomer(ucUpdateCustomer.txtFirstName.Text, ucUpdateCustomer.txtLastName.Text, ucUpdateCustomer.txtEmail.Text))
            {
                ucUpdateCustomer.txtEmail.Text = selectedCustomer.Email;
                ucUpdateCustomer.txtFirstName.Text = selectedCustomer.FirstName;
                ucUpdateCustomer.txtLastName.Text = selectedCustomer.LastName;
                ucUpdateCustomer.lblErrorUpdateCustomer.Text = "Invalid values in fields";
            }
            else
            {
                selectedCustomer.FirstName = ucUpdateCustomer.txtFirstName.Text;
                selectedCustomer.LastName = ucUpdateCustomer.txtLastName.Text;
                selectedCustomer.Email = ucUpdateCustomer.txtEmail.Text;
                Response response = Communication.Instance.EditCustomer(selectedCustomer);
                if (response.Exception == null)
                {
                    if (MessageBox.Show("Customer added successfully", "Customer add", MessageBoxButtons.OK, MessageBoxIcon.Information) == DialogResult.OK)
                    {
                        RequestForGetCustomers(string.Empty);
                        ucCustomer.txtSearch.Text = string.Empty;
                        frmDialog.Close();
                    }
                }
                else
                {
                    MessageBox.Show("Adding customer failed", "Customer add", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void AddCustomer_Click(object sender, EventArgs e)
        {
            frmDialog = new FrmDialog();
            ucAddCustomer = new UCAddCustomer();
            frmDialog.panel1.Controls.Add(ucAddCustomer);
            ucAddCustomer.Dock = DockStyle.Fill;
            ucAddCustomer.btnAdd.Click += AddCustomerDialog_Click;
            frmDialog.ShowDialog();
        }
       
        private void AddCustomerDialog_Click(object sender, EventArgs e)
        {
            if(!GuiHelper.ValidateAddCustomer(ucAddCustomer.txtFirstName.Text, ucAddCustomer.txtLastName.Text, ucAddCustomer.txtEmail.Text))
            {
                ucAddCustomer.txtEmail.Text = string.Empty;
                ucAddCustomer.txtFirstName.Text = string.Empty;
                ucAddCustomer.txtLastName.Text = string.Empty;
                ucAddCustomer.lblErrorAddCustomer.Text = "Invalid values in fields";
            }
            else
            {
                Customer customer = new Customer()
                {
                    Email = ucAddCustomer.txtEmail.Text,
                    FirstName = ucAddCustomer.txtFirstName.Text,
                    LastName = ucAddCustomer.txtLastName.Text,
                };
                Response response = Communication.Instance.AddCustomer(customer);
                if(response.Exception == null)
                {
                    if(MessageBox.Show("Customer added successfully", "Customer add", MessageBoxButtons.OK, MessageBoxIcon.Information) == DialogResult.OK)
                    {
                        RequestForGetCustomers(string.Empty);
                        ucCustomer.txtSearch.Text = string.Empty;
                        frmDialog.Close();
                    }
                }
                else
                {
                    MessageBox.Show("Adding customer failed", "Customer add", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
        private void SearchCustomer_Click(object sender, EventArgs e)
        {
            RequestForGetCustomers(ucCustomer.txtSearch.Text);
        }

        private void RequestForGetCustomers(string search)
        {
            Response response = Communication.Instance.GetCustomers(new Customer() { SearchFilter = '%' + search + '%'});
            if (response.Exception == null && response.Result is List<Customer>)
            {
                ucCustomer.dataGridView1.DataSource = null;
                ucCustomer.dataGridView1.DataSource = (List<Customer>)response.Result;
                ucCustomer.dataGridView1.Columns["customerid"].Visible = false;
                ucCustomer.dataGridView1.Columns["FullName"].Visible = false;
                ucCustomer.dataGridView1.Columns["FirstName"].HeaderText = "First name";
                ucCustomer.dataGridView1.Columns["LastName"].HeaderText = "Last name";
                foreach (DataGridViewColumn column in ucCustomer.dataGridView1.Columns)
                {
                    column.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                }
            }
            else
            {
                MessageBox.Show("Getting customer failed", "Get customers", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


    }
}
