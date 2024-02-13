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
    public delegate void LinkClick(object sender, LinkLabelLinkClickedEventArgs e);
    public delegate void ButtonClick(object sender, EventArgs e);

    public partial class UCLogin : UserControl
    {
        public LinkClick LinkClicked;
        public ButtonClick ButtonClicked;
        public UCLogin()
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
