using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace purchase
{
    public partial class blackload : Form
    {
        public blackload()
        {
            InitializeComponent();
        }

        private void Form3_Load(object sender, EventArgs e)
        {
            circularProgress1.IsRunning = true;
        }
        public void msg()
        {
            MessageBox.Show("eroor"); 
        }
    }
}
