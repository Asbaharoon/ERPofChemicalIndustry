using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Configuration;
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
using System.IO;
namespace printreports
{
    public partial class cwr : Form
    {
        public cwr()
        {
            InitializeComponent();
        }
        string sqlCon = @"Data Source=.\SQLEXPRESS;Initial Catalog=chloritech_db;Integrated Security=True";
        SqlConnection Con;
        SqlCommand cmd;
        DataSet ds;
        DataTable dt;
        SqlDataAdapter adapter;
        SqlCommand upgCommand;
        string contents;
        string itm;
        string pathq = @"D:\ERP with my management\printreports(Final)\printreports\cwrquery";
        string pathr = @"D:\ERP with my management\printreports(Final)\printreports";
        private void cwr_Load(object sender, EventArgs e)
        {
            //
        }

        private void button1_Click(object sender, EventArgs e)
        {
            slidePanel1.IsOpen = false;
            String date = "'"+dateTimePicker1.Text+"'"; // start date
            dateTimePicker1.Value = dateTimePicker1.Value.Subtract(TimeSpan.FromDays(1));
            String date1 = "'" + dateTimePicker1.Text + "'"; // one day before start date
            String date2 = "'" + dateTimePicker2.Text + "'"; // end date
            panel1.Visible = false;
            crystalReportViewer1.Visible = true;
            int cat = 1;                    // Compulsary Change  (int)comboBox1.SelectedValue
            cmd = new SqlCommand();
            Con = new SqlConnection(sqlCon);
            cmd.CommandType = CommandType.Text;
            cmd.Connection = Con;
            ds = new DataSet();            
            upgCommand = new SqlCommand();
            upgCommand.Connection = Con;
            upgCommand.CommandType = CommandType.Text;
            Con.Open();
            if (date == "'2012-04-01'")
            {
                process_query(date2, true,cat);
                upgCommand.CommandText = File.ReadAllText(pathq+@"\cwrfinal.sql");
                try
                {
                    MessageBox.Show(upgCommand.ExecuteNonQuery().ToString());
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }

            }
            else
            {
                process_query(date1,true,cat);
                process_query(date2,false,cat);
                upgCommand.CommandText = File.ReadAllText(pathq + @"\cwrfinal1.sql");
                try
                {
                    upgCommand.ExecuteNonQuery();
                    //MessageBox.Show(upgCommand.ExecuteNonQuery().ToString());
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }
            }
            //grand exection for report 
            cmd.CommandText = "select * from chloritech_db.dbo.report_cwr";
            try
            {
                adapter = new SqlDataAdapter(cmd);
                adapter.Fill(ds, "ans");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            try
            {
                cws cws1 = new cws();
                dt = ds.Tables["ans"];
                cws1.Tables["report_cwr"].Merge(dt);
                //dataGridView1.DataSource = cws1.Tables["report1_temp"];

                ReportDocument rd1 = new ReportDocument();
                rd1.Load(pathr + @"\cat_rpt.rpt");
                crystalReportViewer1.ReportSource = rd1;                
                rd1.SetDataSource(cws1);
                rd1.SetParameterValue("Name",date);
                rd1.SetParameterValue("Name2", date2);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }


        }
        public void process_query(String date,Boolean rank,int id)
        {
            //Creating tebletemp
            contents = File.ReadAllText(pathq + @"\cwr1.sql");
            if (rank == true)
            {
                contents = contents.Replace("$var", "cwrtmp1");
            }
            else 
            {
                contents = contents.Replace("$var", "cwrtmp2");
            }
            contents = contents.Replace("$id", ""+id);
            contents = contents.Replace("$start", "'2012-04-01'");
            contents = contents.Replace("$end", date);
            //MessageBox.Show(contents + "From SHQ");
            cmd.CommandText = contents; 
            //MessageBox.Show(contents);

            ds = new DataSet();
            try
            {
                adapter = new SqlDataAdapter(cmd);
                adapter.Fill(ds, "tmptbl");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }

            //MessageBox.Show("step 1 done");

            //update 1
            String contents1 = File.ReadAllText(pathq + @"\cwr1upd1.sql");            
            if (rank == true)
            {
                contents1 = contents1.Replace("$var", "cwrtmp1");
            }
            else
            {
                contents1 = contents1.Replace("$var", "cwrtmp2");
            }
            //MessageBox.Show(contents1);
            upgCommand.CommandText = contents1;
            try
            {
                upgCommand.ExecuteNonQuery();
                //MessageBox.Show(upgCommand.ExecuteNonQuery().ToString());
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }          

        }

        private void crystalReportViewer1_Load(object sender, EventArgs e)
        {

        }

        private void buttonX5_Click(object sender, EventArgs e)
        {
            slidePanel1.IsOpen = false;
        }

    }
}
