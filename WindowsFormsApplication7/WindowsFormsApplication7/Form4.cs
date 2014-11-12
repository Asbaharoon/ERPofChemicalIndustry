using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevComponents.DotNetBar;
using printreports;

namespace WindowsFormsApplication7
{
    public partial class Form4 : DevComponents.DotNetBar.Metro.MetroForm
    {
        black b1;
        public Form4(black b )
        {
            InitializeComponent();
            b1 = b;
        }

        private void Form4_Load(object sender, EventArgs e)
        {
           
        }

        private void Form4_FormClosing(object sender, FormClosingEventArgs e)
        {
            b1.Close();
        }

        private void buttonX1_Click(object sender, EventArgs e)
        {
            if (cmnall.cmncls.allowe_me("rpt_ail"))
            {


                    ail a = new ail();
                    a.Show();
                

            }
            else
            {
                MessageBox.Show("NO  PERMISSION  TO  ACCESS  THIS  REPORT");
                return;
            }
           
        }

        private void buttonX4_Click(object sender, EventArgs e)
        {
            if (cmnall.cmncls.allowe_me("rpt_cwssr"))
            {


                cwss cw = new cwss();
                cw.Show();


            }
            else
            {
                MessageBox.Show("NO  PERMISSION  TO  ACCESS  THIS  REPORT");
                return;
            }
           
        }

        private void buttonX2_Click(object sender, EventArgs e)
        {
            if (cmnall.cmncls.allowe_me("rpt_cwr"))
            {


                cwr cw = new cwr();
                cw.Show();


            }
            else
            {
                MessageBox.Show("NO  PERMISSION  TO  ACCESS  THIS  REPORT");
                return;
            }
           

        }

        private void buttonX3_Click(object sender, EventArgs e)
        {
            if (cmnall.cmncls.allowe_me("rpt_sil"))
            {


                sisl s = new sisl();
                s.Show();


            }
            else
            {
                MessageBox.Show("NO  PERMISSION  TO  ACCESS  THIS  REPORT");
                return;
            }
            
        }
    }
}