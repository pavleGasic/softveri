namespace Client.UserControls
{
    partial class UCAddFilm
    {
        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.lblErrorAddActor = new System.Windows.Forms.Label();
            this.btnSave = new System.Windows.Forms.Button();
            this.txtTitle = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.cmbGenre = new System.Windows.Forms.ComboBox();
            this.numUDQuantity = new System.Windows.Forms.NumericUpDown();
            this.btnAddActors = new System.Windows.Forms.Button();
            this.txtImageUrl = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.txtPrizePerDay = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.numUDQuantity)).BeginInit();
            this.SuspendLayout();
            // 
            // lblErrorAddActor
            // 
            this.lblErrorAddActor.AutoSize = true;
            this.lblErrorAddActor.Font = new System.Drawing.Font("Courier New", 9F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblErrorAddActor.ForeColor = System.Drawing.Color.IndianRed;
            this.lblErrorAddActor.Location = new System.Drawing.Point(261, 665);
            this.lblErrorAddActor.Name = "lblErrorAddActor";
            this.lblErrorAddActor.Size = new System.Drawing.Size(0, 28);
            this.lblErrorAddActor.TabIndex = 34;
            this.lblErrorAddActor.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // btnSave
            // 
            this.btnSave.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.btnSave.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(1)))), ((int)(((byte)(2)))), ((int)(((byte)(50)))));
            this.btnSave.Font = new System.Drawing.Font("Courier New", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSave.ForeColor = System.Drawing.SystemColors.Window;
            this.btnSave.Location = new System.Drawing.Point(546, 591);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(152, 75);
            this.btnSave.TabIndex = 33;
            this.btnSave.Text = "Save";
            this.btnSave.UseVisualStyleBackColor = false;
            // 
            // txtTitle
            // 
            this.txtTitle.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.txtTitle.Font = new System.Drawing.Font("Courier New", 13.875F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtTitle.Location = new System.Drawing.Point(255, 129);
            this.txtTitle.Name = "txtTitle";
            this.txtTitle.Size = new System.Drawing.Size(406, 49);
            this.txtTitle.TabIndex = 32;
            // 
            // label2
            // 
            this.label2.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Courier New", 10.875F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(249, 93);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(100, 33);
            this.label2.TabIndex = 31;
            this.label2.Text = "Title";
            // 
            // label1
            // 
            this.label1.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Courier New", 13.875F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(301, 42);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(282, 41);
            this.label1.TabIndex = 30;
            this.label1.Text = "Add new film";
            // 
            // label4
            // 
            this.label4.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Courier New", 10.875F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(249, 185);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(287, 33);
            this.label4.TabIndex = 38;
            this.label4.Text = "Initial quantity";
            // 
            // label3
            // 
            this.label3.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Courier New", 10.875F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(251, 279);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(100, 33);
            this.label3.TabIndex = 40;
            this.label3.Text = "Genre";
            // 
            // cmbGenre
            // 
            this.cmbGenre.Font = new System.Drawing.Font("Courier New", 13.875F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbGenre.FormattingEnabled = true;
            this.cmbGenre.Location = new System.Drawing.Point(255, 316);
            this.cmbGenre.Name = "cmbGenre";
            this.cmbGenre.Size = new System.Drawing.Size(406, 49);
            this.cmbGenre.TabIndex = 41;
            // 
            // numUDQuantity
            // 
            this.numUDQuantity.Font = new System.Drawing.Font("Courier New", 13.875F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.numUDQuantity.Location = new System.Drawing.Point(255, 221);
            this.numUDQuantity.Name = "numUDQuantity";
            this.numUDQuantity.Size = new System.Drawing.Size(406, 49);
            this.numUDQuantity.TabIndex = 42;
            // 
            // btnAddActors
            // 
            this.btnAddActors.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.btnAddActors.BackColor = System.Drawing.Color.White;
            this.btnAddActors.Font = new System.Drawing.Font("Courier New", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAddActors.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(1)))), ((int)(((byte)(2)))), ((int)(((byte)(50)))));
            this.btnAddActors.Location = new System.Drawing.Point(246, 585);
            this.btnAddActors.Name = "btnAddActors";
            this.btnAddActors.Size = new System.Drawing.Size(152, 86);
            this.btnAddActors.TabIndex = 43;
            this.btnAddActors.Text = "Add actors";
            this.btnAddActors.UseVisualStyleBackColor = false;
            // 
            // txtImageUrl
            // 
            this.txtImageUrl.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.txtImageUrl.Font = new System.Drawing.Font("Courier New", 13.875F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtImageUrl.Location = new System.Drawing.Point(257, 410);
            this.txtImageUrl.Name = "txtImageUrl";
            this.txtImageUrl.Size = new System.Drawing.Size(406, 49);
            this.txtImageUrl.TabIndex = 45;
            // 
            // label5
            // 
            this.label5.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Courier New", 10.875F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(251, 374);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(185, 33);
            this.label5.TabIndex = 44;
            this.label5.Text = "Image(url)";
            // 
            // txtPrizePerDay
            // 
            this.txtPrizePerDay.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.txtPrizePerDay.Font = new System.Drawing.Font("Courier New", 13.875F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtPrizePerDay.Location = new System.Drawing.Point(255, 504);
            this.txtPrizePerDay.Name = "txtPrizePerDay";
            this.txtPrizePerDay.Size = new System.Drawing.Size(406, 49);
            this.txtPrizePerDay.TabIndex = 47;
            // 
            // label6
            // 
            this.label6.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Courier New", 10.875F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(249, 468);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(236, 33);
            this.label6.TabIndex = 46;
            this.label6.Text = "Prize per day";
            // 
            // UCAddFilm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(12F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.txtPrizePerDay);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.txtImageUrl);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.btnAddActors);
            this.Controls.Add(this.numUDQuantity);
            this.Controls.Add(this.cmbGenre);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.lblErrorAddActor);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.txtTitle);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Name = "UCAddFilm";
            this.Size = new System.Drawing.Size(910, 721);
            ((System.ComponentModel.ISupportInitialize)(this.numUDQuantity)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        public System.Windows.Forms.Label lblErrorAddActor;
        public System.Windows.Forms.Button btnSave;
        public System.Windows.Forms.TextBox txtTitle;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        public System.Windows.Forms.ComboBox cmbGenre;
        public System.Windows.Forms.NumericUpDown numUDQuantity;
        public System.Windows.Forms.Button btnAddActors;
        public System.Windows.Forms.TextBox txtImageUrl;
        private System.Windows.Forms.Label label5;
        public System.Windows.Forms.TextBox txtPrizePerDay;
        private System.Windows.Forms.Label label6;
    }
}
