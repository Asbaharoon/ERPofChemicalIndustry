using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace purchase
{
    public partial class Form2 : Form
    {
        Object[] indent_nos;
        Form1 f1;
        public Form2(Object[] items , Form1 f)
        {
            InitializeComponent();
            indent_nos = items;
            f1 = f;
        }

        String constr = @"Data Source=.\SQLEXPRESS;Initial Catalog=chloritech_db;Integrated Security=True";
        SqlConnection con;
        SqlCommand cmd;
        SqlDataAdapter adpt;
        DataSet dset, dset1;
        String cmdstr;


        private void Form2_Load(object sender, EventArgs e)
        {
            con = new SqlConnection(constr);
            int ind_no1 = (int)indent_nos[0];
            cmdstr = "select ind.indentno , ind.item ,ind.indentqty , ind.Unit from chloritech_db.dbo.tblindentdet ind where ind.indentno in (" + ind_no1;
            
            for (int s = 1; s < indent_nos.Length; s++)
            {
                int ind_no = (int)indent_nos[s];
                cmdstr = cmdstr + "," + ind_no;
            }

            cmdstr = cmdstr + ")";
            cmd = new SqlCommand(cmdstr, con);
            adpt = new SqlDataAdapter(cmd);
            dset = new DataSet();
            MessageBox.Show(cmdstr);
            adpt.Fill(dset, "ind");
            dataGridView1.DataSource = dset.Tables["ind"];
        }

        private void buttonX1_Click(object sender, EventArgs e)
        {
            String cmdstr = "delete from chloritech_db.dbo.potemp select * from chloritech_db.dbo.potemp";
            DataSet dset2;
            int total_row = 0;
            cmd = new SqlCommand(cmdstr, con);
            adpt = new SqlDataAdapter(cmd);
            dset2 = new DataSet();
            adpt.Fill(dset2, "detail");

            foreach (DataGridViewRow ro in dataGridView1.Rows)
            {
                if (Convert.ToBoolean(ro.Cells["status"].Value))
                {
                    DataRow row = dset2.Tables[0].NewRow();

                    row["indentno"] = Convert.ToInt32(ro.Cells[1].Value);
                    row["item"] = ro.Cells[2].Value.ToString();
                    row["indentqty"] = Convert.ToDouble(ro.Cells[3].Value);
                    row["Unit"] = ro.Cells[4].Value.ToString();

                    dset2.Tables[0].Rows.Add(row);
                    SqlCommandBuilder cmdb = new SqlCommandBuilder(adpt);
                    adpt.Update(dset2, "detail");

                    total_row++;
                }

            }
            f1.delete_all();
            f1.retrive_data(total_row);
            Close();
        }
    }
}
