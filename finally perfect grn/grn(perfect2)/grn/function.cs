using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Data;
using cmnall;

namespace grn
{
    public partial class grnform : Form
    {
        

        public void initial_data_binding()
        {

            constr = @"Data Source=.\SQLEXPRESS;Initial Catalog=chloritech_db;Integrated Security=True";
            constr = cmnall.cmncls.get_constr111();
            cmdstr = "select id,suppliername from tblsuppliermaster";

            con = new SqlConnection(constr);
            cmd = new SqlCommand(cmdstr, con);
            adpt = new SqlDataAdapter(cmd);
            dset = new DataSet();
            con.Open();

            adpt.Fill(dset, "supl");

            BindingSource bndsrc1 = new BindingSource();
            bndsrc1.DataSource = dset;
            bndsrc1.DataMember = "supl";
            comboBox3.DataSource = bndsrc1;
            comboBox3.ValueMember = "id";
            comboBox3.DisplayMember = "suppliername";

            cmdstr = "select id,storestype from tblstorestype";
            con = new SqlConnection(constr);
            cmd = new SqlCommand(cmdstr, con);
            adpt = new SqlDataAdapter(cmd);
            adpt.Fill(dset, "store");
           
            bndsrc = new BindingSource();
            bndsrc.DataSource = dset;
            bndsrc.DataMember = "store";
            comboBox4.DataSource = bndsrc;
            comboBox4.ValueMember = "id";
            comboBox4.DisplayMember = "storestype";
            

        }

        public void full_screen_mode()
        {
            this.Height = Screen.PrimaryScreen.WorkingArea.Height;
            this.Width = Screen.PrimaryScreen.WorkingArea.Width;
            this.Location = Screen.PrimaryScreen.WorkingArea.Location;
        }

        public int max_no_of_grn()
        {
            int max = 0;
            cmdstr = "select grnno from tblgrndetails";  
            cmd = new SqlCommand(cmdstr, con);
            adpt = new SqlDataAdapter(cmd);                        
            
            adpt.Fill(dset, "grnn");


            foreach (DataRow srow in dset.Tables["grnn"].Rows)
            {
                int val = Convert.ToInt32(srow["grnno"].ToString());
                if (val >= max && val != 2000)
                {
                    max = val;
                }
            }

            
            return max;
        }

        public void comb_dynamic_bind()
        {

            cmdstr = "select catid,category from tblcategorymasters";


            cmd = new SqlCommand(cmdstr, con);
            adpt = new SqlDataAdapter(cmd);
            dset = new DataSet();


            adpt.Fill(dset, "grn");



            bndsrc = new BindingSource();
            bndsrc.DataSource = dset;
            bndsrc.DataMember = "grn";
            lcb[count * 3].DataSource = bndsrc;
            lcb[count * 3].ValueMember = "catid";
            lcb[count * 3].DisplayMember = "category";
        }

        public void event_handlers()
        {
          
            for(int  w  = 0 ; w < ltb.Count ; w++)
            {

                ltb[idx + 3].Enter += new System.EventHandler(ex_stor_logic);
                ltb[idx + 5].Enter += new System.EventHandler(rej_logic);
                ltb[idx + 7].Enter += new System.EventHandler(amt_logic);
            }
            
            //ltb[idx + 10].KeyDown += new System.Windows.Forms.KeyEventHandler(this.add_row);
            ltb[ltb.Count-1].KeyDown += new System.Windows.Forms.KeyEventHandler(this.add_row);
            //reff = idx + 10;

            idx = idx + 11;
            lcb[count * 3].SelectedIndexChanged += new System.EventHandler(this.main_logic);
        }

        public void dynamic_readonly_texbox()
        {
            for (int i = (count * trow) + 1; i < ltb.Count; i++)
            {
                sz.Width = 72;
                ltb[i].Size = sz;
                if (i == (count * 10) + 7 + count || i == (count + 1) * 10 + count)
                {
                    sz.Width = 159;
                    ltb[i].Size = sz;
                }


                if (i == j + 1 || i == j + 2 || i == j + 4 || i == j + 6)
                {
                    ltb[i].ReadOnly = false;

                }



                flowLayoutPanel1.Controls.Add(ltb[i]);



            }
        }




         
    }
}
