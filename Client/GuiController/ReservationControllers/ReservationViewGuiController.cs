using Client.UserControls;
using Common.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client.GuiController.ReservationControllers
{
    internal class ReservationViewGuiController
    {
        private static ReservationViewGuiController instance;
        public static ReservationViewGuiController Instance
        {
            get
            {
                if (instance == null)
                    instance = new ReservationViewGuiController();
                return instance;
            }
        }

        private ReservationViewGuiController() { }

        private FrmDialog frmDialog;
        private UCViewReservation ucViewReservation;

        public void ShowViewReservation(Reservation reservation)
        {
            frmDialog = new FrmDialog();
            frmDialog.panel1.Controls.Clear();
            ucViewReservation = new UCViewReservation();
            ucViewReservation.flowLayoutPanel1.Controls.Clear();
            foreach(var item in reservation.ReservationItems)
            {
                UCReservationViewCard card = new UCReservationViewCard();
                card.lblSerialNumber.Text = item.SerialNumber.ToString();
                card.lblPrice.Text = item.Price.ToString();
                card.lblTitle.Text = item.Film.Title;
                card.pictureBox1.LoadAsync(item.Film.ImageUrl);
                ucViewReservation.flowLayoutPanel1.Controls.Add(card);
            }
            frmDialog.panel1.Controls.Add(ucViewReservation);
            frmDialog.ShowDialog();
        }
    }
}
