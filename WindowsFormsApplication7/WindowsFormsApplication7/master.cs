using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevComponents.DotNetBar;
using WindowsFormsApplication7;
using masters;
using cmnall;
namespace WindowsFormsApplication5
{
    
    public partial class master : DevComponents.DotNetBar.Metro.MetroForm
    {
       public int item_count = 0,cc_count=0,sp_count=0;
        Form3 f3;
        public master(Form3 f)
        {
            InitializeComponent();
            f3 = f ; 
        }
        
       

        private void buttonX1_Click(object sender, EventArgs e)
        {
            slidePanel1.IsOpen = false;
            //circularProgress1.IsRunning = true;
        }
        
        private void buttonX2_Click(object sender, EventArgs e)
        {
            if (item_count == 0)
            {
                masters.itemmaster itm = new itemmaster(this);
                itm.MdiParent = this;
                itm.StartPosition = FormStartPosition.CenterScreen;
                itm.Show();

                item_count = 1;
            }
            else
            {
                MessageBox.Show("ITEM  MASTER  IS  ALREADY  OPENED ");
            }

            //f3 = new Form3(this);

            //f3.TopLevel = true;

            //f3.MdiParent = this;

            //f3.Show();



        }

        public void fclose( Form f)
        {
            f.Close();
        }

        private void master_Load(object sender, EventArgs e)
        {
           
            this.BackColor = Color.WhiteSmoke;
            
           
        }
        supplier s;
        private void buttonX4_Click(object sender, EventArgs e)
        {

            if (sp_count == 0)
            {
                masters.supplier itm = new supplier(this);
                itm.MdiParent = this;
                itm.StartPosition = FormStartPosition.CenterScreen;
                itm.Show();

                sp_count = 1;
            }
            else
            {
                MessageBox.Show("   SUPPLIER  MASTER  IS  ALREADY  OPENED ");
            }
        }

        private void buttonX3_Click(object sender, EventArgs e)
        {
            if (cc_count == 0)
            {
                masters.costcenter itm = new costcenter(this);
                itm.MdiParent = this;
                itm.StartPosition = FormStartPosition.CenterScreen;
                itm.Show();

                cc_count = 1;
            }
            else
            {
                MessageBox.Show("COST  CENTER  MASTER  IS  ALREADY  OPENED ");
            }
        }

        private void master_FormClosing(object sender, FormClosingEventArgs e)
        {
            cmnall.cmncls.check_off("mstr");
        }

       
    }
}