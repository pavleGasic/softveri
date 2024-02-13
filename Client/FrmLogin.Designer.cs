namespace Client
{
    partial class FrmLogin
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

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmLogin));
            this.pnlMain = new System.Windows.Forms.Panel();
            this.ucLogin1 = new Client.UserControls.UCLogin();
            this.ucWindowTop1 = new Server.UserControls.UCWindowTop();
            this.ucRegister1 = new Client.UserControls.UCRegister();
            this.pnlMain.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlMain
            // 
            this.pnlMain.Controls.Add(this.ucRegister1);
            this.pnlMain.Controls.Add(this.ucLogin1);
            this.pnlMain.Location = new System.Drawing.Point(230, 122);
            this.pnlMain.Name = "pnlMain";
            this.pnlMain.Size = new System.Drawing.Size(476, 538);
            this.pnlMain.TabIndex = 3;
            // 
            // ucLogin1
            // 
            this.ucLogin1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ucLogin1.Location = new System.Drawing.Point(0, 0);
            this.ucLogin1.Name = "ucLogin1";
            this.ucLogin1.Size = new System.Drawing.Size(476, 538);
            this.ucLogin1.TabIndex = 2;
            // 
            // ucWindowTop1
            // 
            this.ucWindowTop1.Dock = System.Windows.Forms.DockStyle.Top;
            this.ucWindowTop1.Location = new System.Drawing.Point(0, 0);
            this.ucWindowTop1.Name = "ucWindowTop1";
            this.ucWindowTop1.Size = new System.Drawing.Size(908, 73);
            this.ucWindowTop1.TabIndex = 0;
            // 
            // ucRegister1
            // 
            this.ucRegister1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ucRegister1.Location = new System.Drawing.Point(0, 0);
            this.ucRegister1.Name = "ucRegister1";
            this.ucRegister1.Size = new System.Drawing.Size(476, 538);
            this.ucRegister1.TabIndex = 3;
            this.ucRegister1.Visible = false;
            // 
            // FrmLogin
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(12F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.BackgroundImage = global::Client.Properties.Resources._3415325;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(908, 708);
            this.Controls.Add(this.pnlMain);
            this.Controls.Add(this.ucWindowTop1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "FrmLogin";
            this.pnlMain.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        internal Server.UserControls.UCWindowTop ucWindowTop1;
        internal UserControls.UCLogin ucLogin1;
        internal System.Windows.Forms.Panel pnlMain;
        internal UserControls.UCRegister ucRegister1;
    }
}

