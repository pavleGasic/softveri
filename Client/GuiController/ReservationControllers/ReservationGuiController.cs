using Client.UserControls;
using Common.Communication;
using Common.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Client.GuiController.ReservationControllers
{
    internal class ReservationGuiController
    {
        private static ReservationGuiController instance;
        public static ReservationGuiController Instance
        {
            get
            {
                if (instance == null)
                    instance = new ReservationGuiController();
                return instance;
            }
        }
        private ReservationGuiController() { }
        internal bool refreshReservation = false;
        private UCReservation ucReservation;
        public void ShowReservationControl(Panel panel)
        {
            ucReservation = new UCReservation();
            panel.Controls.Clear();
            ucReservation.Dock = DockStyle.Fill;
            HandleControls();
            GetReservations();
            panel.Controls.Add(ucReservation);
        }

        private void GetReservations()
        {
            Response response = Communication.Instance.GetReservations();
            if (response.Exception == null && response.Result is List<Reservation> reservations)
            {
                ucReservation.dataGridView1.DataSource = null;
                ucReservation.dataGridView1.DataSource = reservations;
                ucReservation.dataGridView1.Columns["reservationId"].HeaderText = "Reservation no";
                ucReservation.dataGridView1.Columns["dateFrom"].HeaderText = "Date from";
                ucReservation.dataGridView1.Columns["dateTo"].HeaderText = "Date to";
                ucReservation.dataGridView1.Columns["totalPrice"].HeaderText = "Price";
                ucReservation.dataGridView1.Columns["ReservationStatus"].HeaderText = "Status";
                ucReservation.dataGridView1.Columns["worker"].Visible = false;
                ucReservation.dataGridView1.Columns["customer"].Visible = false;
                ucReservation.dataGridView1.Columns["customerName"].HeaderText = "Customer";
                foreach (DataGridViewColumn column in ucReservation.dataGridView1.Columns)
                {
                    column.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                }       
            }
            else
            {
                MessageBox.Show("Error when getting reservations", "Reservation error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void HandleControls()
        {
            ucReservation.btnCreate.Click += CreateReservation_Click;
            ucReservation.btnDelete.Click += DeleteReservation_Click;
            ucReservation.btnView.Click += ViewReservation_Click;
            ucReservation.btnUpdate.Click += UpdateReservation_Click;
        }

        private void UpdateReservation_Click(object sender, EventArgs e)
        {
            if (ucReservation.dataGridView1.SelectedRows.Count == 1)
            {
                Reservation selectedReservation = (Reservation)ucReservation.dataGridView1.SelectedRows[0].DataBoundItem;
                UpdateReservationGuiController.Instance.ShowUpdateReservation(selectedReservation);
                if (refreshReservation)
                {
                    GetReservations();
                    refreshReservation = false;
                }
            }
            else
            {
                MessageBox.Show("Select reservation first", "Reservation error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ViewReservation_Click(object sender, EventArgs e)
        {
            if (ucReservation.dataGridView1.SelectedRows.Count == 1)
            {
                Reservation selectedReservation = (Reservation)ucReservation.dataGridView1.SelectedRows[0].DataBoundItem;             ReservationViewGuiController.Instance.ShowViewReservation(selectedReservation);
            }
            else
            {
                MessageBox.Show("Select reservation first", "Reservation error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            
        }

        private void DeleteReservation_Click(object sender, EventArgs e)
        {
            if (ucReservation.dataGridView1.SelectedRows.Count == 1)
            {
                Reservation selectedReservation = (Reservation)ucReservation.dataGridView1.SelectedRows[0].DataBoundItem;
                if(selectedReservation.ReservationStatus != ReservationStatus.Returned)
                {
                    MessageBox.Show("Cannon delete reservation that is not returned", "Reservation error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    if (MessageBox.Show($"Do you want to delete reservation number {selectedReservation.ReservationId}?", "Delete reservation", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        Response response = Communication.Instance.DeleteReservation(selectedReservation);
                        if (response.Exception == null && response.Result is int)
                        {
                            MessageBox.Show("Delete reservation successful", "Reservation delete", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            GetReservations();
                        }
                        else
                        {
                            MessageBox.Show("Delete reservation failed", "Reservation error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }
            }
            else
            {
                MessageBox.Show("Select reservation first", "Reservation error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void CreateReservation_Click(object sender, EventArgs e)
        {
            ChooseFilmGuiController.Instance.ShowChooseFilmControl();
            if (refreshReservation)
            {
                GetReservations();
                refreshReservation = false;
            }
        }
    }
}
