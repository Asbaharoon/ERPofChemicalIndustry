using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace WindowsFormsApplication7
{
    public partial class black : Form
    {
        public black()
        {
            InitializeComponent();
        }

        private void black_Load(object sender, EventArgs e)
        {
            Form4 F4 = new Form4(this);
            F4.ShowDialog();
        }
    }
}
