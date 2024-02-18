using Client.ClientCommunication;
using Client.UserControls;
using Common.Communication;
using Common.Domain;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Client.GuiController.ReservationControllers
{
    internal class ChooseFilmGuiController
    {
        private static ChooseFilmGuiController instance;
        public static ChooseFilmGuiController Instance
        {
            get
            {
                if (instance == null)
                    instance = new ChooseFilmGuiController();
                return instance;
            }
        }
        private ChooseFilmGuiController() { }

        private UCChooseFilm ucChooseFilm;
        private FrmBigDialog frmBigDialog;
        private List<Film> selectedFilms = new List<Film>();
        private Dictionary<int, bool> selectedCards = new Dictionary<int, bool>();
        private string oldSearchValue = "";
        private double totalPricePerDay = 0;
        private double totalPrice = 0;
        public void ShowChooseFilmControl()
        {
            frmBigDialog = new FrmBigDialog();
            ucChooseFilm = new UCChooseFilm();
            frmBigDialog.panel1.Controls.Clear();
            HandleControls();
            frmBigDialog.panel1.Controls.Add(ucChooseFilm);
            ucChooseFilm.Dock = DockStyle.Fill;
            InitializeCustomerComboBox();
            GetFilms();
            frmBigDialog.ShowDialog();
        }

        private void HandleControls()
        {
            ucChooseFilm.btnSearch.Click += BtnSearch_Click;
            ucChooseFilm.dateTimePicker1.ValueChanged += DatePicker_ValueChanged;
            ucChooseFilm.btnNext.Click += BtnNext_Click;
            frmBigDialog.FormClosed += Frm_FromClosed;
        }

        private void Frm_FromClosed(object sender, FormClosedEventArgs e)
        {
            selectedCards.Clear();
            oldSearchValue = "";
            totalPrice = 0;
            totalPricePerDay = 0;
            selectedFilms = new List<Film>();
        }

        private void BtnNext_Click(object sender, EventArgs e)
        {
            if(selectedFilms.Count == 0)
            {
                MessageBox.Show("Select films first", "Validation error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (ucChooseFilm.cmbCustomers.SelectedIndex == -1)
            {
                MessageBox.Show("Select customer first", "Validation error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (ucChooseFilm.dateTimePicker1.Value.Date == DateTime.Now.Date)
            {
                MessageBox.Show("Choose valid date", "Validation error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            ReservationConfirmGuiController.Instance.ShowReservationConfirmation(DateTime.Now, ucChooseFilm.dateTimePicker1.Value, totalPrice, selectedFilms, (Customer)ucChooseFilm.cmbCustomers.SelectedItem);
            GetFilms();
        }

        private void DatePicker_ValueChanged(object sender, EventArgs e)
        {
            totalPrice = totalPricePerDay * (ucChooseFilm.dateTimePicker1.Value.AddHours(1) - DateTime.Now).Days;
            if (totalPrice >= 0)
            {
                ucChooseFilm.lblTotal.Text = $"{totalPrice}RSD";
            }
        }

        private void BtnSearch_Click(object sender, EventArgs e)
        {
            if(oldSearchValue != ucChooseFilm.txtSearch.Text)
            {
                GetFilms();
            }
        }
        private void InitializeCustomerComboBox()
        {
            try
            {
                List<Customer> customers = Communication.Instance.GetCustomers(new Customer() { SearchFilter = "%%" });
                ucChooseFilm.cmbCustomers.Items.Clear();
                ucChooseFilm.cmbCustomers.DataSource = customers;
                ucChooseFilm.cmbCustomers.DisplayMember = "FullName";
                ucChooseFilm.cmbCustomers.SelectedIndex = -1;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Get customers error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void GetFilms()
        {
            try
            {
                List<Film> films = Communication.Instance.GetFilms(new Film() { SearchFilter = '%' + ucChooseFilm.txtSearch.Text + '%' });
                oldSearchValue = ucChooseFilm.txtSearch.Text;
                ucChooseFilm.flowLayoutPanel1.Controls.Clear();
                foreach (var f in films)
                {
                    UCFilmCard card = new UCFilmCard();
                    if (selectedCards.ContainsKey(f.FilmId))
                    {
                        card.chbAddCard.Checked = selectedCards[f.FilmId];
                    }
                    card.film = f;
                    card.lblTitle.Text = f.Title;
                    card.lblPrice.Text = f.PricePerDay.ToString();
                    card.lblQuantity.Text = f.Quantity.ToString();
                    card.pictureBox1.LoadAsync(f.ImageUrl);
                    if (f.Quantity == 0)
                    {
                        card.chbAddCard.Enabled = false;
                    }
                    else
                    {
                        card.btnCard.Click += ChooseFilm_Click;
                        card.pictureBox1.Click += ChooseFilm_Click;
                        card.panel1.Click += ChooseFilm_Click;
                        card.panel2.Click += ChooseFilm_Click;
                        card.label1.Click += ChooseFilm_Click;
                        card.label2.Click += ChooseFilm_Click;
                        card.label3.Click += ChooseFilm_Click;
                        card.label4.Click += ChooseFilm_Click;
                        card.label5.Click += ChooseFilm_Click;
                        card.lblPrice.Click += ChooseFilm_Click;
                        card.lblQuantity.Click += ChooseFilm_Click;
                        card.lblTitle.Click += ChooseFilm_Click;
                        card.chbAddCard.Click += ChooseFilm_Click;
                    }
                    f.Actors.ForEach(a =>
                    {
                        card.lbxCast.Items.Add(a.Name + " -> " + a.RoleName);
                    });
                    ucChooseFilm.flowLayoutPanel1.Controls.Add(card);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Get films error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ChooseFilm_Click(object sender, EventArgs e)
        {
            Control btnCard = sender as Control;
            UCFilmCard card = btnCard.Parent as UCFilmCard;
            card = card!=null && btnCard.Parent != null? card : btnCard.Parent.Parent as UCFilmCard;
            if (card != null)
            {
                if((btnCard is CheckBox && card.chbAddCard.Checked) || ( !(btnCard is CheckBox) && !card.chbAddCard.Checked))
                {
                    card.chbAddCard.Checked = true;
                    selectedFilms.Add(card.film);
                    if (!selectedCards.ContainsKey(card.film.FilmId))
                    {
                        selectedCards.Add(card.film.FilmId, true);
                    }
                    else
                    {
                        selectedCards[card.film.FilmId] = true;
                    }
                    totalPricePerDay += card.film.PricePerDay;
                    totalPrice += card.film.PricePerDay * (ucChooseFilm.dateTimePicker1.Value.AddHours(1) - DateTime.Now).Days;
                }
                else
                {
                    card.chbAddCard.Checked = false;
                    selectedFilms.Remove(card.film);
                    if (!selectedCards.ContainsKey(card.film.FilmId))
                    {
                        selectedCards.Add(card.film.FilmId, false);
                    }
                    else
                    {
                        selectedCards[card.film.FilmId] = false;
                    }
                    totalPricePerDay -= card.film.PricePerDay;
                    totalPrice -= card.film.PricePerDay * (ucChooseFilm.dateTimePicker1.Value.AddHours(1) - DateTime.Now).Days;
                }
            }
            if (totalPrice >= 0)
            {
                ucChooseFilm.lblTotal.Text = $"{totalPrice}RSD";
            }
        }
    }
}
