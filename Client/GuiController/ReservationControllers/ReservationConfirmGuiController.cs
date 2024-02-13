using Client.UserControls;
using Common.Communication;
using Common.Domain;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Client.GuiController.ReservationControllers
{
    internal class ReservationConfirmGuiController
    {
        private static ReservationConfirmGuiController instance;
        public static ReservationConfirmGuiController Instance
        {
            get
            {
                if (instance == null)
                    instance = new ReservationConfirmGuiController();
                return instance;
            }
        }

        private ReservationConfirmGuiController() { }

        private FrmDialog frmDialog;
        private UCReservationDetails ucReservationDetails;
        private Reservation reservation;

        public void ShowReservationConfirmation(DateTime dateFrom, DateTime dateTo, double totalPrice, List<Film> selectedFilms, Customer customer)
        {
            frmDialog = new FrmDialog();
            ucReservationDetails = new UCReservationDetails();
            frmDialog.panel1.Controls.Add(ucReservationDetails);
            ucReservationDetails.Dock = DockStyle.Fill;
            InitializeValues(dateFrom, dateTo, totalPrice, selectedFilms, customer);
            ucReservationDetails.btnSave.Click += BtnSaveReservation_Click;
            frmDialog.ShowDialog();
        }

        private void BtnSaveReservation_Click(object sender, EventArgs e)
        {
            Response response = Communication.Instance.AddReservation(reservation);
            if(response.Exception == null && (int)response.Result != -1)
            {
                if(MessageBox.Show("Reservation saved successfully", "Save success", MessageBoxButtons.OK, MessageBoxIcon.Information) == DialogResult.OK)
                {
                    ReservationGuiController.Instance.refreshReservation = true;
                    frmDialog.Close();
                }
            }
            else
            {
                MessageBox.Show("Failed to save reservation", "Save failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void InitializeValues(DateTime dateFrom, DateTime dateTo, double totalPrice, List<Film> selectedFilms, Customer customer)
        {
            int sno = 1;
            reservation = new Reservation()
            {
                Customer = customer,
                DateFrom = dateFrom,
                DateTo = dateTo,
                Worker = Session.Instance.Worker,
                ReservationStatus = ReservationStatus.InUse,
                TotalPrice = totalPrice,
                ReservationItems = selectedFilms.Select(f => new ReservationItem()
                {
                    Film = new Film()
                    {
                        FilmId = f.FilmId,
                        Title = f.Title,
                        Quantity = f.Quantity,
                        Actors = f.Actors,
                        Genre = f.Genre,
                        ImageUrl = f.ImageUrl,
                        PricePerDay = f.PricePerDay
                    },
                    SerialNumber = sno++,
                    Price = f.PricePerDay * (dateTo - dateFrom).Days,
                }).ToList()
            };
            ucReservationDetails.lblCustomer.Text = customer.FullName;
            ucReservationDetails.lblReturnDate.Text = dateTo.ToString("yyyy/MM/dd");
            ucReservationDetails.lblTotalPrize.Text = totalPrice.ToString() + " RSD";
            ucReservationDetails.dataGridView1.DataSource = selectedFilms;
            ucReservationDetails.dataGridView1.Columns["Genre"].Visible = false;
            ucReservationDetails.dataGridView1.Columns["FilmId"].HeaderText = "Serial number";
            ucReservationDetails.dataGridView1.Columns["Quantity"].Visible = false;
            ucReservationDetails.dataGridView1.Columns["ImageUrl"].Visible = false;
            ucReservationDetails.dataGridView1.Columns["PricePerDay"].HeaderText = "Price per day";
            sno = 1;
            foreach (DataGridViewRow row in ucReservationDetails.dataGridView1.Rows) 
            {
                row.Cells[0].Value = sno++;
            }
            foreach (DataGridViewColumn column in ucReservationDetails.dataGridView1.Columns)
            {
                column.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            }
            
        }
    }
}
