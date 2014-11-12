using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevComponents.DotNetBar;
using WindowsFormsApplication5;
using grn;
using Indent;
using issue;

namespace WindowsFormsApplication7
{
    public partial class Form3 : DevComponents.DotNetBar.Metro.MetroForm
    {
        Form fm;
        public Form3(Form f)
        {
            InitializeComponent();
            fm = f;
        }
        Point p = new Point();
        private void Form3_Load(object sender, EventArgs e)
        {
            p.X = 451;
            p.Y = 152;
            slidePanel1.Location = p;
            slidePanel2.Location = p;

            slidePanel2.IsOpen = false;
            slidePanel2.Visible = true;
            
           
            
        }

        private void metroTileItem1_Click(object sender, EventArgs e)
        {
            labelX1.Text = "DATA ENTRY MENU";
            slidePanel1.IsOpen = false;
            
            slidePanel2.IsOpen = true;
        }

        private void metroTileItem12_Click(object sender, EventArgs e)
        {
            labelX1.Text = "INVENTORY  AND  STORES MENU";
            slidePanel2.IsOpen = false;
            slidePanel1.IsOpen = true;
        }

        private void metroTileItem3_Click(object sender, EventArgs e)
        {
            black b1 = new black();
            b1.Show();
        }

        private void metroTileItem2_Click(object sender, EventArgs e)
        {
            if (cmnall.cmncls.allowe_me("master"))
            {


                if (cmnall.cmncls.check("mstr"))
                {
                    master m = new master(this);
                    m.Show();
                }
                else
                {
                    MessageBox.Show("one  instance  is  already opened");
                    return;
                }

            }
            else
            {
                MessageBox.Show("NO  PERMISSION  TO  ACCESS  MASTER");
                return;
            }
           
        }

        private void metroTileItem9_Click(object sender, EventArgs e)
        {
            if (cmnall.cmncls.allowe_me("grn"))
            {

                  
            if (cmnall.cmncls.check("grn"))
            {
                  grnform g = new grnform();
                    g.Show();
            }
               else {
                    MessageBox.Show("one  instance  is  already opened");
                    return;
                }

            }
            else
            {
                MessageBox.Show("NO  PERMISSION  TO  ACCESS  GRN");
                return;
            }
        }

        private void metroTileItem10_Click(object sender, EventArgs e)
        {
            if (cmnall.cmncls.allowe_me("indent"))
            {
                if (cmnall.cmncls.check("indent"))
                {
                    Indent.Form1 ind = new Indent.Form1();

                    ind.Show();


                }
                else
                {
                    MessageBox.Show("one  instance  is  already opened");
                }
            }
            else
            {

                MessageBox.Show("NO  PERMISSION TO  ACCESS INDENT");
            }
        }

        private void Form3_FormClosing(object sender, FormClosingEventArgs e)
        {
            fm.Show();
        }

        private void metroTileItem4_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void metroTileItem11_Click(object sender, EventArgs e)
        {
             if (cmnall.cmncls.allowe_me("issue"))
            {
                if (cmnall.cmncls.check("issue"))
                {
                    issue.Form1 iss = new issue.Form1();

                    iss.Show();


                }
                else
                {
                    MessageBox.Show("one  instance  is  already opened");
                }
            }
            else
            {

                MessageBox.Show("NO  PERMISSION TO  ACCESS ISSUE");
            }
        
        }
    }
}