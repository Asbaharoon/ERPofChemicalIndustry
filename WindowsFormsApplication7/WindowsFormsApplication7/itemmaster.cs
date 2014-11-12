using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;
using DevComponents.DotNetBar;
using WindowsFormsApplication5;

namespace masters
{
    public partial class itemmaster : DevComponents.DotNetBar.Metro.MetroForm
    {
        SqlConnection con;
        SqlCommand cmd;
        SqlDataAdapter adpt;
        DataSet dset, dset2;
        string constr, cmdstr,cmdstr1;
        BindingSource bndsrc;
        int flag = 0,temp=0;
        master ms;
        public itemmaster(master m )
        {
            InitializeComponent();
            ms = m;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
          
            constr = @"Data Source=.\SQLEXPRESS;Initial Catalog=chloritech_db;Integrated Security=True";
            cmdstr = "select catid,category from tblcategorymasters";
            cmdstr1 = "select id,abbrunits from tblunitsmaster";

            try 
            {
                con = new SqlConnection(constr);
                cmd = new SqlCommand(cmdstr, con);
                adpt = new SqlDataAdapter(cmd);
                dset = new DataSet();
                con.Open();

                adpt.Fill(dset, "itemm");

                bndsrc = new BindingSource();
                bndsrc.DataSource = dset;
                bndsrc.DataMember = "itemm";
                comboBoxEx1.DataSource = bndsrc;
                comboBoxEx1.ValueMember = "catid";
                comboBoxEx1.DisplayMember = "category";
                con.Close();

                con = new SqlConnection(constr);
                cmd = new SqlCommand(cmdstr1, con);
                adpt = new SqlDataAdapter(cmd);
                dset = new DataSet();
                con.Open();

                adpt.Fill(dset, "itemm2");

                bndsrc = new BindingSource();
                bndsrc.DataSource = dset;
                bndsrc.DataMember = "itemm2";
                comboBoxEx2.DataSource = bndsrc;
                comboBoxEx2.ValueMember = "id";
                comboBoxEx2.DisplayMember = "abbrunits";
                con.Close();
            }
            catch
            {
                MessageBox.Show("Sql Exception ... ");
            }
            temp = max_no_of_item();
            textBoxX5.Text = String.Format("{0} of {0}", (temp + 1));

            textBoxX1.Text = "" + (temp + 1);
        }

        public int max_no_of_item()
        {
            int max = 0;
            cmdstr = "select id from tblitemmaster";
            cmd = new SqlCommand(cmdstr, con);
            adpt = new SqlDataAdapter(cmd);

            adpt.Fill(dset, "itemm3");


            foreach (DataRow srow in dset.Tables["itemm3"].Rows)
            {
                int val = Convert.ToInt32(srow["id"].ToString());
                if (val >= max && val != 2000)
                {
                    max = val;
                }
            }


            return max;
        }

        private void buttonX1_Click(object sender, EventArgs e)
        {
            int item_no = Convert.ToInt32(textBoxX1.Text),item_no_new = 0,temp2=(temp+1);


            if (((ButtonX)sender).Name.ToString() == "buttonX1")
            {
                if (flag == 1)
                {
                    item_no_new = Convert.ToInt32(textBoxX5.Text);
                    flag = 0;
                }
                else
                {
                    item_no_new = (item_no - 1);
                }
            }
            else if (((ButtonX)sender).Name.ToString() == "buttonX4")
            {
                item_no_new = 1;
            }
            else if (((ButtonX)sender).Name.ToString() == "buttonX2")
            {
                item_no_new = (item_no + 1);
            }
            else if (((ButtonX)sender).Name.ToString() == "buttonX3")
            {
                item_no_new = temp;
            }

            textBoxX1.Text = " " + item_no_new;
            textBoxX5.Text = String.Format("{0} of {1}",item_no_new,temp2);


            cmdstr = "select id,itemname,category,units,openingqty,openingval from tblitemmaster where id=" +item_no_new;
            con = new SqlConnection(constr);
            cmd = new SqlCommand(cmdstr, con);
            adpt = new SqlDataAdapter(cmd);
            dset = new DataSet();
            con.Open();
            adpt.Fill(dset, "itemret");
            
            foreach (DataRow rrow in dset.Tables["itemret"].Rows)
            {
                textBoxX1.Text = rrow["id"].ToString();
                textBoxX2.Text=rrow["itemname"].ToString();
                comboBoxEx1.SelectedValue = rrow["category"].ToString();
                comboBoxEx2.SelectedValue = rrow["units"].ToString();
                textBoxX3.Text = rrow["openingqty"].ToString();
                textBoxX4.Text = rrow["openingval"].ToString();
               
            }
            con.Close();
           
        }

        private void buttonX5_Click(object sender, EventArgs e)
        {
            if (textBoxX2.Text == "")
            {
                MessageBox.Show("Itemname filed is Empty");
                return;
            }
            cmdstr = "select * from tblitemmaster";
            cmd = new SqlCommand(cmdstr, con);
            adpt = new SqlDataAdapter(cmd);
            dset2 = new DataSet();
            con.Open();
            adpt.Fill(dset2, "insertdb");

            for (int r = 0; r < 6; r++)
            {

                DataRow row = dset2.Tables[0].NewRow();

                row["id"] = Convert.ToInt32(textBoxX1.Text);
                row["itemname"] = textBoxX2.Text;
                row["category"] = Convert.ToInt32(comboBoxEx1.SelectedValue);
                row["units"] = Convert.ToInt32(comboBoxEx2.SelectedValue);
                if (textBoxX3.Text!="") 
                    row["openingqty"] = Convert.ToDouble(textBoxX3.Text);
                if (textBoxX4.Text != "") 
                    row["openingval"] = Convert.ToDouble(textBoxX4.Text);



                dset2.Tables[0].Rows.Add(row);
                SqlCommandBuilder cmdb = new SqlCommandBuilder(adpt);
                adpt.Update(dset2, "insertdb");

            }
            con.Close();
        }

        private void textBoxX5_Click(object sender, EventArgs e)
        {
            textBoxX5.Text = "  ";
        }

        private void textBoxX5_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                flag = 1;
                buttonX1.PerformClick();
            }
        }

        private void itemmaster_FormClosing(object sender, FormClosingEventArgs e)
        {
            ms.item_count = 0;
        }


    }
}
