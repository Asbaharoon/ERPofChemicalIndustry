using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace grn
{
    public partial class help : Form
    {
        grnform fm1;
        black b;
        public help(grnform fm ,black b)
        {
            fm1 = fm;
            this.b = b;
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            fm1.label1.Text = textBox1.Text;
            fm1.set_grnd_amnt(textBox1.Text);
           
            fm1.Show();
            fm1.WindowState = FormWindowState.Maximized;
            b.Close();
            this.Close();
            
        }

        private void label2_Click(object sender, EventArgs e)
        {
            fm1.Show();
            fm1.WindowState = FormWindowState.Maximized;
            b.Close();
            this.Close();

        }

        private void help_FormClosing(object sender, FormClosingEventArgs e)
        {
            fm1.calculations();
        }

       
    }

}
