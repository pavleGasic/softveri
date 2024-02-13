using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Server.UserControls
{
    public delegate void ButtonClicked(object sender, EventArgs e);
    public delegate void PanelMouseEvent(object sender, MouseEventArgs e);


    public partial class UCWindowTop : UserControl
    { 
        public event ButtonClicked MinimizeButtonClicked;
        public event ButtonClicked CloseButtonClicked;
        public event ButtonClicked MaximizeButtonClicked;
        public event PanelMouseEvent PanelMouseDown;

        public UCWindowTop()
        {
            InitializeComponent();
        }

        private void btnMinimize_Click(object sender, EventArgs e)
        {
            MinimizeButtonClicked?.Invoke(this, e);
        }

        private void btnMaximize_Click(object sender, EventArgs e)
        {
            MaximizeButtonClicked?.Invoke(this, e);
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            CloseButtonClicked?.Invoke(this, e);
        }

        private void panel_MouseDown_1(object sender, MouseEventArgs e)
        {
            PanelMouseDown?.Invoke(this, e);
        }
    }
}
