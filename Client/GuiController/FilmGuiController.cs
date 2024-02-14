using Client.UserControls;
using Common.Communication;
using Common.Domain;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Client.GuiController
{
    internal class FilmGuiController
    {
        private static FilmGuiController instance;
        public static FilmGuiController Instance
        {
            get
            {
                if (instance == null)
                    instance = new FilmGuiController();
                return instance;
            }
        }

        private UCFilm ucFilm;
        private FrmDialog frmAddActorDialog;
        private FrmDialog frmAddFilmDialog;
        private FrmDialog frmAssignActorDialog;
        private UCAddActor ucAddActor;
        private UCAddFilm ucAddFilm;
        private UCAssignActorsToFilm ucAssignActorsToFilm;
        private BindingList<Actor> assignedActors = new BindingList<Actor>();

        public void ShowFilmControl(Panel panel)
        {
            ucFilm = new UCFilm();
            panel.Controls.Clear();
            ucFilm.Dock = DockStyle.Fill;
            HandleControls();
            GetFilms("");
            panel.Controls.Add(ucFilm);
        }

        private void GetFilms(string search)
        {
            Response response = Communication.Instance.GetFilms(new Film() { SearchFilter = '%' + search + '%' });
            if(response.Exception == null && response.Result is List<Film> filmList)
            {
                ucFilm.dataGridView1.DataSource= filmList;
                ucFilm.dataGridView1.Columns["filmId"].Visible = false;
                ucFilm.dataGridView1.Columns["imageUrl"].Visible = false;
                ucFilm.dataGridView1.Columns["genre"].Visible = false;
                ucFilm.dataGridView1.Columns["priceperday"].HeaderText = "Price(RSD/day)";
                MakeDataGridViewAutoSizeColumns(ucFilm.dataGridView1);
            }
            else
            {
                MessageBox.Show("Database error getting films", "Database error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void HandleControls()
        {
            ucFilm.btnAddActor.Click += AddActor_Click;
            ucFilm.btnAdd.Click += AddFilm_Click;
            ucFilm.btnDelete.Click += DeleteFilm_Click;
        }

    
        #region Creating dialogs
        private void DeleteFilm_Click(object sender, EventArgs e)
        {
            if (ucFilm.dataGridView1.SelectedRows.Count == 1)
            {
                Film selectedFilm = (Film)ucFilm.dataGridView1.SelectedRows[0].DataBoundItem;
                Response response = Communication.Instance.DeleteFilm(selectedFilm);
                if(response.Exception == null && (int)response.Result != -1)
                {
                    MessageBox.Show("Film deleted successfully", "Film delete", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    GetFilms("");
                }
                else
                {
                    MessageBox.Show("Film delete failed", "Film delete", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show("You must select row first", "Invalid operation", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void AddFilm_Click(object sender, EventArgs e)
        {
            frmAddFilmDialog = new FrmDialog();
            ucAddFilm = new UCAddFilm();
            frmAddFilmDialog.panel1.Controls.Clear();
            frmAddFilmDialog.panel1.Controls.Add(ucAddFilm);
            ucAddFilm.Dock = DockStyle.Fill;
            ucAddFilm.btnSave.Click += SaveFilm_Click;
            ucAddFilm.btnAddActors.Click += AssignActorsForm_Click;
            ucAddFilm.txtPrizePerDay.TextChanged += PrizeChanged_TextChanged;
            FillGenreComboBox();
            frmAddFilmDialog.ShowDialog();
        }

        private void PrizeChanged_TextChanged(object sender, EventArgs e)
        {
            if(!ucAddFilm.txtPrizePerDay.Text.Contains("RSD"))
            {
                ucAddFilm.txtPrizePerDay.Text += "RSD";
            }
        }

        private void FillGenreComboBox()
        {
            Response response = Communication.Instance.GetGenres();
            if(response.Exception == null && response.Result != null && response.Result is List<Genre> genres)
            {
                ucAddFilm.cmbGenre.DataSource = genres;
                ucAddFilm.cmbGenre.DisplayMember = "GenreName";
                ucAddFilm.cmbGenre.ValueMember = "GenreId";
            }
            else
            {
                if(MessageBox.Show("Cannot get genres", "Get genres", MessageBoxButtons.OK, MessageBoxIcon.Error) == DialogResult.OK)
                {
                    frmAddFilmDialog.Close();
                }
            }

        }

        private void AddActor_Click(object sender, EventArgs e)
        {
            frmAddActorDialog = new FrmDialog();
            ucAddActor = new UCAddActor();
            frmAddActorDialog.panel1.Controls.Clear();
            frmAddActorDialog.panel1.Controls.Add(ucAddActor);
            ucAddActor.Dock = DockStyle.Fill;
            ucAddActor.btnAdd.Click += AddActorDialog_Click;
            ucAddActor.chbMale.CheckedChanged += AddActorMale_CheckedChanged;
            ucAddActor.chbFemale.CheckedChanged += AddActorFemale_CheckedChanged;
            frmAddActorDialog.ShowDialog();
        }
        #endregion

        #region Add actor events
        private void AddActorMale_CheckedChanged(object sender, EventArgs e)
        {
            if (ucAddActor.chbMale.Checked)
            {
                ucAddActor.chbFemale.Checked = false;
            }
        }
        private void AddActorFemale_CheckedChanged(object sender, EventArgs e)
        {
            if (ucAddActor.chbFemale.Checked)
            {
                ucAddActor.chbMale.Checked = false;
            }
        }

        private void AddActorDialog_Click(object sender, EventArgs e)
        {
            if (ucAddActor.txtName.Text.Length < 3)
            {
                ucAddActor.txtName.Text = string.Empty;
                ucAddActor.chbFemale.Checked = false;
                ucAddActor.chbFemale.Checked = false;
                ucAddActor.lblErrorAddActor.Text = "Invalid values in fields";
            }
            else
            {
                Gender? gender = null;
                if (ucAddActor.chbMale.Checked) gender = Gender.Male;
                if (ucAddActor.chbFemale.Checked) gender = Gender.Female;
                Actor actor = new Actor()
                {
                    Name = ucAddActor.txtName.Text,
                    Gender = gender
                };
                Response response = Communication.Instance.AddActor(actor);
                if (response.Exception == null)
                {
                    if (MessageBox.Show("Actor added successfully", "Actor add", MessageBoxButtons.OK, MessageBoxIcon.Information) == DialogResult.OK)
                    {
                        frmAddActorDialog.Close();
                    }
                }
                else
                {
                    MessageBox.Show("Adding actor failed", "Customer add", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
        #endregion
        #region Add film events
        private void AssignActorsForm_Click(object sender, EventArgs e)
        {
            if(ucAddFilm.txtTitle.Text.Length != 0)
            {
                frmAssignActorDialog = new FrmDialog();
                ucAssignActorsToFilm = new UCAssignActorsToFilm();
                frmAssignActorDialog.panel1.Controls.Clear();
                frmAssignActorDialog.panel1.Controls.Add(ucAssignActorsToFilm);
                ucAssignActorsToFilm.Dock = DockStyle.Fill;
                ucAssignActorsToFilm.btnSave.Click += SaveActors_Click;
                ucAssignActorsToFilm.btnAddActor.Click += AddActorToFilm_Click;
                ucAssignActorsToFilm.btnDeleteActor.Click += UnassignActorFromFilm_Click;
                ucAssignActorsToFilm.dataGridView1.DataSource = assignedActors;
                MakeDataGridViewAutoSizeColumns(ucAssignActorsToFilm.dataGridView1);
                ucAssignActorsToFilm.lblAssign.Text = $"Assign actors to {ucAddFilm.txtTitle.Text}";
                FillActorsComboBox();
                frmAssignActorDialog.ShowDialog();
            }
            else
            {
                MessageBox.Show("Before assigning actors enter the film title", "Alert", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void UnassignActorFromFilm_Click(object sender, EventArgs e)
        {
            if (ucAssignActorsToFilm.dataGridView1.SelectedRows.Count == 1)
            {
                Actor selectedActor = (Actor) ucAssignActorsToFilm.dataGridView1.SelectedRows[0].DataBoundItem;
                assignedActors.Remove(selectedActor);
                ucAssignActorsToFilm.dataGridView1.DataSource = assignedActors;
            }
        }

        private void FillActorsComboBox()
        {
            Response response = Communication.Instance.GetActors();
            if (response.Exception == null && response.Result != null && response.Result is List<Actor> actors)
            {
                ucAssignActorsToFilm.cmbActor.DataSource = actors;
                ucAssignActorsToFilm.cmbActor.DisplayMember = "Name";
                ucAssignActorsToFilm.cmbActor.ValueMember = "ActorId";
            }
            else
            {
                if (MessageBox.Show("Cannot get actors", "Get actors", MessageBoxButtons.OK, MessageBoxIcon.Error) == DialogResult.OK)
                {
                    frmAssignActorDialog.Close();
                }
            }
        }

        private void SaveFilm_Click(object sender, EventArgs e)
        {
            if(ucAddFilm.txtTitle.Text.Length > 0 && ucAddFilm.cmbGenre.SelectedIndex != -1
                && GuiHelper.GetDoubleValue(ucAddFilm.txtPrizePerDay.Text) != -1)
            {
                if(assignedActors.Count == 0)
                {
                    if(MessageBox.Show("Are you sure that you don't want to assign actor to this film?", "Assign actors", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.No)
                    {
                        return;
                    }
                }
                Film film = new Film()
                {
                    Genre = (Genre)ucAddFilm.cmbGenre.SelectedItem,
                    ImageUrl = ucAddFilm.txtImageUrl.Text,
                    Title = ucAddFilm.txtTitle.Text,
                    Quantity = (int)ucAddFilm.numUDQuantity.Value,
                    Actors = new List<Actor>(assignedActors),
                    PricePerDay = GuiHelper.GetDoubleValue(ucAddFilm.txtPrizePerDay.Text)
                };
                Response response = Communication.Instance.AddFilm(film);
                if(response.Exception == null)
                {
                    if(MessageBox.Show("Film saved successfully", "Film saved", MessageBoxButtons.OK, MessageBoxIcon.Information) == DialogResult.OK){
                        ucAddFilm.txtTitle.Text = string.Empty;
                        ucAddFilm.txtImageUrl.Text = string.Empty;
                        ucAddFilm.txtPrizePerDay.Text = string.Empty;
                        ucAddFilm.numUDQuantity.Value = 0;
                        ucAddFilm.cmbGenre.SelectedIndex = -1;
                        assignedActors.Clear();
                        GetFilms("");
                    }
                }
                else
                {
                    MessageBox.Show("Film save failed", "Film saved", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show("All fields are required or check values in fields", "Parameters error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        #endregion

        private void SaveActors_Click(object sender, EventArgs e)
        {
            frmAssignActorDialog.Close();
        }

        private void AddActorToFilm_Click(object sender, EventArgs e)
        {
            if(ucAssignActorsToFilm.txtRoleName.Text.Length != 0 && ucAssignActorsToFilm.cmbActor.SelectedIndex != -1)
            {
                Actor actor = ucAssignActorsToFilm.cmbActor.SelectedItem as Actor;
                actor.RoleName = ucAssignActorsToFilm.txtRoleName.Text;
                assignedActors.Add(actor);
                ucAssignActorsToFilm.dataGridView1.DataSource = assignedActors;
                ucAssignActorsToFilm.dataGridView1.Columns["actorid"].Visible = false;
                ucAssignActorsToFilm.dataGridView1.Columns["gender"].Visible = false;
                MakeDataGridViewAutoSizeColumns(ucAssignActorsToFilm.dataGridView1);
                ucAssignActorsToFilm.txtRoleName.Text = string.Empty;
            }
        }

        private void MakeDataGridViewAutoSizeColumns(DataGridView dataGridView1)
        {
            foreach (DataGridViewColumn column in dataGridView1.Columns)
            {
                column.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            }
        }
    }
}
