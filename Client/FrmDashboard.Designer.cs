namespace Client
{
    partial class FrmDashboard
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmDashboard));
            this.pnlSidebar = new System.Windows.Forms.Panel();
            this.ucSidebar1 = new Client.UserControls.UCSidebar();
            this.pnlMain = new System.Windows.Forms.Panel();
            this.ucWindowTop1 = new Server.UserControls.UCWindowTop();
            this.pnlSidebar.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlSidebar
            // 
            this.pnlSidebar.Controls.Add(this.ucSidebar1);
            this.pnlSidebar.Dock = System.Windows.Forms.DockStyle.Left;
            this.pnlSidebar.Location = new System.Drawing.Point(0, 70);
            this.pnlSidebar.Name = "pnlSidebar";
            this.pnlSidebar.Size = new System.Drawing.Size(400, 1000);
            this.pnlSidebar.TabIndex = 1;
            // 
            // ucSidebar1
            // 
            this.ucSidebar1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ucSidebar1.Location = new System.Drawing.Point(0, 0);
            this.ucSidebar1.Name = "ucSidebar1";
            this.ucSidebar1.Size = new System.Drawing.Size(400, 1000);
            this.ucSidebar1.TabIndex = 0;
            // 
            // pnlMain
            // 
            this.pnlMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlMain.Location = new System.Drawing.Point(400, 70);
            this.pnlMain.Name = "pnlMain";
            this.pnlMain.Size = new System.Drawing.Size(1296, 1000);
            this.pnlMain.TabIndex = 2;
            // 
            // ucWindowTop1
            // 
            this.ucWindowTop1.Dock = System.Windows.Forms.DockStyle.Top;
            this.ucWindowTop1.Location = new System.Drawing.Point(0, 0);
            this.ucWindowTop1.Name = "ucWindowTop1";
            this.ucWindowTop1.Size = new System.Drawing.Size(1696, 70);
            this.ucWindowTop1.TabIndex = 0;
            // 
            // FrmDashboard
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(12F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1696, 1070);
            this.Controls.Add(this.pnlMain);
            this.Controls.Add(this.pnlSidebar);
            this.Controls.Add(this.ucWindowTop1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "FrmDashboard";
            this.pnlSidebar.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        public Server.UserControls.UCWindowTop ucWindowTop1;
        private System.Windows.Forms.Panel pnlSidebar;
        public System.Windows.Forms.Panel pnlMain;
        public UserControls.UCSidebar ucSidebar1;
    }
}