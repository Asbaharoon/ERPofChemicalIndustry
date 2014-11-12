using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevComponents.DotNetBar;
using System.Data.SqlClient;
using WindowsFormsApplication5;

namespace masters
{
    public partial class costcenter : DevComponents.DotNetBar.Metro.MetroForm
    {
        master ms;
        public costcenter(master m)
        {
            InitializeComponent();
            ms = m;
        }
        SqlConnection con;
        SqlCommand cmd;
        SqlDataAdapter adpt;
        DataSet dset, dset2;
        string constr, cmdstr, cmdstr1;
        BindingSource bndsrc;
        int max_semicostid;

        private void costcenter_Load(object sender, EventArgs e)
        {
            constr = @"Data Source=.\SQLEXPRESS;Initial Catalog=chloritech_db;Integrated Security=True";
            cmdstr = "select  mainID , maincostcentre from chloritech_db.dbo.tbl1maincostcentre";
            cmdstr1 = "select  subcostID , subcostcentre from chloritech_db.dbo.tbl2subcostcentre";

            con = new SqlConnection(constr);
            cmd = new SqlCommand(cmdstr, con);
            adpt = new SqlDataAdapter(cmd);
            dset = new DataSet();
            con.Open();

            adpt.Fill(dset, "main");

            bndsrc = new BindingSource();
            bndsrc.DataSource = dset;
            bndsrc.DataMember = "main";
            comboBoxEx1.DataSource = bndsrc;
            comboBoxEx1.ValueMember = "mainID";
            comboBoxEx1.DisplayMember = "maincostcentre";
            con.Close();

            con = new SqlConnection(constr);
            cmd = new SqlCommand(cmdstr1, con);
            adpt = new SqlDataAdapter(cmd);
            dset = new DataSet();
            con.Open();

            adpt.Fill(dset, "sub");

            bndsrc = new BindingSource();
            bndsrc.DataSource = dset;
            bndsrc.DataMember = "sub";
            comboBoxEx2.DataSource = bndsrc;
            comboBoxEx2.ValueMember = "subcostID";
            comboBoxEx2.DisplayMember = "subcostcentre";
            con.Close();
        }

        private void buttonX5_Click(object sender, EventArgs e)
        {
            if (textBoxX1.Text == "")
            {
                MessageBox.Show("Insert Appropiate value for 'SEMISUBCOSTCENTRE'");
                return;
            }
            int temp= (max_sucostid() + 1);
            cmdstr = "select * from chloritech_db.dbo.tbl3semisubcostcentre";
            cmd = new SqlCommand(cmdstr, con);
            adpt = new SqlDataAdapter(cmd);
            dset = new DataSet();

            adpt.Fill(dset, "c"); 
            //dataGridView1.DataSource = dset.Tables["c"];
            DataRow row = dset.Tables["c"].NewRow();

            row["semisubID"] = temp;//MessageBox.Show(""+(max_sucostid() + 1));
            row["subcostID"] = Convert.ToInt32(comboBoxEx2.SelectedValue); //MessageBox.Show("" + Convert.ToInt32(comboBoxEx2.SelectedValue));

            row["semisubcostcentre"] = textBoxX1.Text;
            try
            {
                dset.Tables["c"].Rows.Add(row);
                SqlCommandBuilder cmdb = new SqlCommandBuilder(adpt);
                adpt.Update(dset, "c");
            }
            catch 
            {                
            }         

            con.Close();
        }
        public int max_sucostid() 
        {
            cmdstr1 = "select MAX(semisubID) as ans from chloritech_db.dbo.tbl3semisubcostcentre";
            cmd = new SqlCommand(cmdstr1, con);
            adpt = new SqlDataAdapter(cmd);
            dset2 = new DataSet();
            adpt.Fill(dset2, "costinsert");
            foreach (DataRow row in dset2.Tables[0].Rows)
                {
                    max_semicostid = Convert.ToInt32(row["ans"]); 
                }
            return max_semicostid;
        }

        private void costcenter_FormClosing(object sender, FormClosingEventArgs e)
        {
            ms.cc_count = 0;
        }
       
    }
}
