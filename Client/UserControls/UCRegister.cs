using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Client.UserControls
{

    public partial class UCRegister : UserControl
    {
        public LinkClick LinkClicked;
        public ButtonClick ButtonClicked;
        public UCRegister()
        {
            InitializeComponent();
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            LinkClicked.Invoke(this, e);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            ButtonClicked.Invoke(this, e);
        }
    }
}
