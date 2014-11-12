using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevComponents.DotNetBar;
using System.Data.SqlClient;
using cmnall;
using WindowsFormsApplication5;

namespace masters
{
    public partial class supplier : DevComponents.DotNetBar.Metro.MetroForm
    {
        SqlConnection con;
        SqlCommand cmd;
        SqlDataAdapter adpt;
        DataSet dset,dset2;
        string constr, cmdstr;
        master ms;
        int temp = 0,flag=0;

        public supplier(master m)
        {
            InitializeComponent();
            ms = m; 
        }

        private void buttonX1_Click(object sender, EventArgs e)
        {
            int supplier_no = Convert.ToInt32(textBoxX1.Text), supplier_no_new = 0, temp2 = (temp + 1);


            if (((ButtonX)sender).Name.ToString() == "buttonX1")
            {
                if (flag == 1)
                {
                    supplier_no_new = Convert.ToInt32(textBoxX5.Text);
                    flag = 0;
                }
                else
                {
                    supplier_no_new = (supplier_no - 1);
                }
            }
            else if (((ButtonX)sender).Name.ToString() == "buttonX4")
            {
                supplier_no_new = 1;
            }
            else if (((ButtonX)sender).Name.ToString() == "buttonX2")
            {
                supplier_no_new = (supplier_no + 1);
            }
            else if (((ButtonX)sender).Name.ToString() == "buttonX3")
            {
                supplier_no_new = temp;
            }

            textBoxX1.Text = " " + supplier_no_new;
            textBoxX5.Text = String.Format("{0} of {1}", supplier_no_new, temp2);


            cmdstr = "select * from tblsuppliermaster where id=" + supplier_no_new;
            con = new SqlConnection(constr);
            cmd = new SqlCommand(cmdstr, con);
            adpt = new SqlDataAdapter(cmd);
            dset = new DataSet();
            con.Open();
            adpt.Fill(dset, "supplierret");

            foreach (DataRow rrow in dset.Tables["supplierret"].Rows)
            {
                textBoxX1.Text = rrow["id"].ToString();
                textBoxX2.Text = rrow["suppliername"].ToString();
                textBoxX3.Text = rrow["openingbalance"].ToString();
                textBoxX6.Text = rrow["add1"].ToString(); 
                textBoxX7.Text = rrow["add2"].ToString();
                textBoxX8.Text = rrow["add3"].ToString();
                textBoxX9.Text = rrow["city"].ToString();
                textBoxX10.Text = rrow["pincode"].ToString();
                textBoxX11.Text = rrow["state"].ToString();
                textBoxX4.Text = rrow["email"].ToString();
                textBoxX12.Text = rrow["phone"].ToString();

            }
            con.Close();
           

          

        }

        private void Form2_Load(object sender, EventArgs e)
        {

            constr = "Data Source=.\\SQLEXPRESS;Initial Catalog=chloritech_db;Integrated Security=True";
            constr = cmnall.cmncls.get_constr111();
            con = new SqlConnection(constr);
            dset = new DataSet();
            con.Open();

            temp = max_no_of_supplier();
            textBoxX5.Text = String.Format("{0} of {0}", (temp + 1));
            textBoxX1.Text = "" + (temp + 1);

        }

        public int max_no_of_supplier()
        {
            int max = 0;
            cmdstr = "select id from tblsuppliermaster";
            cmd = new SqlCommand(cmdstr, con);
            adpt = new SqlDataAdapter(cmd);

            adpt.Fill(dset, "supplier");


            foreach (DataRow srow in dset.Tables["supplier"].Rows)
            {
                int val = Convert.ToInt32(srow["id"].ToString());
                if (val >= max && val != 2000)
                {
                    max = val;
                }
            }


            return max;
        }

        private void buttonX5_Click(object sender, EventArgs e)
        {
            try
            {
                cmdstr = "select * from tblsuppliermaster";
                cmd = new SqlCommand(cmdstr, con);
                adpt = new SqlDataAdapter(cmd);
                dset2 = new DataSet();

                adpt.Fill(dset2, "insertdb");
                DataRow row = dset2.Tables[0].NewRow();

                row["id"] = Convert.ToInt32(textBoxX1.Text);
                row["suppliername"] = textBoxX2.Text;
                if (textBoxX3.Text != "")
                {
                    row["openingbalance"] = Convert.ToDouble(textBoxX3.Text);
                }
                row["add1"] = textBoxX6.Text;
                row["add2"] = textBoxX6.Text;
                row["add3"] = textBoxX6.Text;
                row["city"] = textBoxX9.Text;
                row["pincode"] = textBoxX10.Text;
                row["state"] = textBoxX11.Text;
                row["email"] = textBoxX4.Text;
                if (textBoxX12.Text != "")
                {
                    row["phone"] = Convert.ToDouble(textBoxX12.Text);
                }

                dset2.Tables[0].Rows.Add(row);
                SqlCommandBuilder cmdb = new SqlCommandBuilder(adpt);
                adpt.Update(dset2, "insertdb");
            }
            catch 
            {
                MessageBox.Show("Input String not in Correct Format or Empty");
            }
            con.Close();
        }

        private void textBoxX5_Click(object sender, EventArgs e)
        {
            textBoxX5.Text = "";
        }

        private void textBoxX5_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                flag = 1;
                buttonX1.PerformClick();
            }
        }

        private void supplier_FormClosing(object sender, FormClosingEventArgs e)
        {
            ms.sp_count = 0;
        }

        }
    }
