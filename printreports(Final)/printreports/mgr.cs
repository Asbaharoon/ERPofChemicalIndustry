using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace printreports
{
    public partial class mgr : Form
    {
        public mgr()
        {
            InitializeComponent();
        }

       

        

        private void buttonX2_Click(object sender, EventArgs e)
        {
            slidePanel1.IsOpen = false;

        }

        private void buttonX1_Click(object sender, EventArgs e)
        {
            slidePanel1.IsOpen = true;
        }
    }
}
