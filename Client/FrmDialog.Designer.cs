namespace Client
{
    partial class FrmDialog
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmDialog));
            this.ucWindowTop1 = new Server.UserControls.UCWindowTop();
            this.panel1 = new System.Windows.Forms.Panel();
            this.SuspendLayout();
            // 
            // ucWindowTop1
            // 
            this.ucWindowTop1.Dock = System.Windows.Forms.DockStyle.Top;
            this.ucWindowTop1.Location = new System.Drawing.Point(0, 0);
            this.ucWindowTop1.Name = "ucWindowTop1";
            this.ucWindowTop1.Size = new System.Drawing.Size(910, 73);
            this.ucWindowTop1.TabIndex = 0;
            // 
            // panel1
            // 
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 73);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(910, 721);
            this.panel1.TabIndex = 1;
            // 
            // FrmDialog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(12F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(910, 794);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.ucWindowTop1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "FrmDialog";
            this.ResumeLayout(false);

        }

        #endregion

        private Server.UserControls.UCWindowTop ucWindowTop1;
        public System.Windows.Forms.Panel panel1;
    }
}