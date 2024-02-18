using Client.ClientCommunication;
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
    internal class UpdateReservationGuiController
    {
        private static UpdateReservationGuiController instance;
        public static UpdateReservationGuiController Instance
        {
            get
            {
                if (instance == null)
                    instance = new UpdateReservationGuiController();
                return instance;
            }
        }

        private FrmDialog frmDialog;
        private UCUpdateReservation ucUpdateReservation;
        private Reservation reservation;

        public void ShowUpdateReservation(Reservation reservation)
        {
            frmDialog = new FrmDialog();
            ucUpdateReservation = new UCUpdateReservation();
            frmDialog.panel1.Controls.Add(ucUpdateReservation);
            ucUpdateReservation.Dock = DockStyle.Fill;
            InitializeValues(reservation);
            ucUpdateReservation.btnUpdate.Click += BtnUpdateReservation_Click;
            frmDialog.ShowDialog();
        }

        private void BtnUpdateReservation_Click(object sender, EventArgs e)
        {

            try
            {
                reservation.ReservationStatus = (ReservationStatus)ucUpdateReservation.cmbStatus.SelectedItem;
                Reservation response = Communication.Instance.UpdateReservationStatus(reservation);
                ReservationGuiController.Instance.refreshReservation = true;
                frmDialog.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Update reservation error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void InitializeValues(Reservation reservation)
        {
            ucUpdateReservation.txtDateFrom.Text = reservation.DateFrom.ToString("yyyy-MM-dd");
            ucUpdateReservation.txtDateTo.Text = reservation.DateTo.ToString("yyyy-MM-dd");
            ucUpdateReservation.txtCustomerName.Text = reservation.CustomerName;
            ucUpdateReservation.cmbStatus.DataSource = Enum.GetValues(typeof(ReservationStatus));
            ucUpdateReservation.cmbStatus.SelectedItem = reservation.ReservationStatus;
            if (reservation.ReservationStatus == ReservationStatus.Returned)
            {
                ucUpdateReservation.cmbStatus.Enabled = false;
                ucUpdateReservation.btnUpdate.Enabled = false;
            }
            this.reservation = reservation;
        }
    }
}
