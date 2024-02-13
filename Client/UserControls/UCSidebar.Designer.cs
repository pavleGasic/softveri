namespace Client.UserControls
{
    partial class UCSidebar
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
            this.panel1 = new System.Windows.Forms.Panel();
            this.btnSignOut = new System.Windows.Forms.Button();
            this.lblName = new System.Windows.Forms.Label();
            this.lblHello = new System.Windows.Forms.Label();
            this.btnProfile = new System.Windows.Forms.Button();
            this.btnCustomer = new System.Windows.Forms.Button();
            this.btnFilm = new System.Windows.Forms.Button();
            this.btnReservation = new System.Windows.Forms.Button();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.SystemColors.ActiveBorder;
            this.panel1.Controls.Add(this.btnCustomer);
            this.panel1.Controls.Add(this.btnFilm);
            this.panel1.Controls.Add(this.btnReservation);
            this.panel1.Controls.Add(this.btnSignOut);
            this.panel1.Controls.Add(this.lblName);
            this.panel1.Controls.Add(this.lblHello);
            this.panel1.Controls.Add(this.btnProfile);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(400, 1000);
            this.panel1.TabIndex = 0;
            // 
            // btnSignOut
            // 
            this.btnSignOut.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSignOut.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(1)))), ((int)(((byte)(2)))), ((int)(((byte)(50)))));
            this.btnSignOut.Font = new System.Drawing.Font("Courier New", 10.125F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSignOut.ForeColor = System.Drawing.Color.White;
            this.btnSignOut.Location = new System.Drawing.Point(90, 852);
            this.btnSignOut.Name = "btnSignOut";
            this.btnSignOut.Size = new System.Drawing.Size(227, 76);
            this.btnSignOut.TabIndex = 4;
            this.btnSignOut.Text = "Sign out";
            this.btnSignOut.UseVisualStyleBackColor = false;
            // 
            // lblName
            // 
            this.lblName.AutoSize = true;
            this.lblName.Font = new System.Drawing.Font("Courier New", 7.875F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblName.Location = new System.Drawing.Point(132, 81);
            this.lblName.Name = "lblName";
            this.lblName.Size = new System.Drawing.Size(153, 23);
            this.lblName.TabIndex = 2;
            this.lblName.Text = "Pavle Gasic";
            // 
            // lblHello
            // 
            this.lblHello.AutoSize = true;
            this.lblHello.Font = new System.Drawing.Font("Courier New", 7.875F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblHello.Location = new System.Drawing.Point(132, 32);
            this.lblHello.Name = "lblHello";
            this.lblHello.Size = new System.Drawing.Size(179, 23);
            this.lblHello.TabIndex = 1;
            this.lblHello.Text = "Hello, gasa14";
            // 
            // btnProfile
            // 
            this.btnProfile.BackColor = System.Drawing.SystemColors.ActiveBorder;
            this.btnProfile.BackgroundImage = global::Client.Properties.Resources.account_profile_user_ecommerce_icon_224942;
            this.btnProfile.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnProfile.FlatAppearance.BorderSize = 0;
            this.btnProfile.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnProfile.Location = new System.Drawing.Point(16, 16);
            this.btnProfile.Name = "btnProfile";
            this.btnProfile.Size = new System.Drawing.Size(94, 95);
            this.btnProfile.TabIndex = 0;
            this.btnProfile.UseVisualStyleBackColor = false;
            // 
            // btnCustomer
            // 
            this.btnCustomer.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.btnCustomer.Font = new System.Drawing.Font("Courier New", 10.125F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCustomer.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(1)))), ((int)(((byte)(2)))), ((int)(((byte)(50)))));
            this.btnCustomer.Location = new System.Drawing.Point(90, 520);
            this.btnCustomer.Name = "btnCustomer";
            this.btnCustomer.Size = new System.Drawing.Size(227, 76);
            this.btnCustomer.TabIndex = 7;
            this.btnCustomer.Text = "Customer";
            this.btnCustomer.UseVisualStyleBackColor = false;
            // 
            // btnFilm
            // 
            this.btnFilm.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.btnFilm.Font = new System.Drawing.Font("Courier New", 10.125F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnFilm.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(1)))), ((int)(((byte)(2)))), ((int)(((byte)(50)))));
            this.btnFilm.Location = new System.Drawing.Point(90, 345);
            this.btnFilm.Name = "btnFilm";
            this.btnFilm.Size = new System.Drawing.Size(227, 76);
            this.btnFilm.TabIndex = 6;
            this.btnFilm.Text = "Film";
            this.btnFilm.UseVisualStyleBackColor = false;
            // 
            // btnReservation
            // 
            this.btnReservation.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.btnReservation.Font = new System.Drawing.Font("Courier New", 10.125F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnReservation.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(1)))), ((int)(((byte)(2)))), ((int)(((byte)(50)))));
            this.btnReservation.Location = new System.Drawing.Point(90, 170);
            this.btnReservation.Name = "btnReservation";
            this.btnReservation.Size = new System.Drawing.Size(227, 76);
            this.btnReservation.TabIndex = 5;
            this.btnReservation.Text = "Reservation";
            this.btnReservation.UseVisualStyleBackColor = false;
            // 
            // UCSidebar
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(12F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.panel1);
            this.Name = "UCSidebar";
            this.Size = new System.Drawing.Size(400, 1000);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        public System.Windows.Forms.Button btnProfile;
        public System.Windows.Forms.Label lblName;
        public System.Windows.Forms.Label lblHello;
        public System.Windows.Forms.Button btnSignOut;
        public System.Windows.Forms.Button btnCustomer;
        public System.Windows.Forms.Button btnFilm;
        public System.Windows.Forms.Button btnReservation;
    }
}
