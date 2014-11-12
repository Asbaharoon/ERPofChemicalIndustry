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
    public partial class cwss : Form
    {
        public cwss()
        {
            InitializeComponent();
        }
        string sqlCon = @"Data Source=.\SQLEXPRESS;Initial Catalog=chloritech_d;Integrated Security=True";
        SqlConnection Con;
        SqlCommand cmd;
        DataSet ds;
        DataTable dt;
        SqlDataAdapter adapter;
        SqlCommand upgCommand;
        string contents;
        string path = @"G:\Users\onyx\Dropbox\project2013\PrintReport\Printreport 17 Apr\printreports\printreports\queries for cwsr";
        private void cwss_Load(object sender, EventArgs e)
        {
            //wait for code
        }

        private void button1_Click(object sender, EventArgs e)
        {
            slidePanel1.IsOpen = false;
            String date = dateTimePicker1.Text ;
           
            dateTimePicker1.Value = dateTimePicker1.Value.Subtract(TimeSpan.FromDays(1));
            
            String date1 = "'"+dateTimePicker1.Text+"'";
            String date2 = "'" + dateTimePicker2.Text + "'";
            panel1.Visible = false;
            crystalReportViewer1.Visible = true;
            cmd = new SqlCommand();
            Con = new SqlConnection(sqlCon);
            cmd.CommandType = CommandType.Text;
            cmd.Connection = Con;
            Con.Open();

            //call 911
            if (date == "2012-04-01")
            {
                process_query("'2012-03-01'","'2012-03-30'", true);
            }
            else
                process_query("'2012-04-01'", date1, true);

                process_query("'2012-04-01'",date2, false);


                //delete data
                try
                {
                upgCommand.CommandType = CommandType.Text;
                upgCommand.CommandText = "delete from chloritech_db.dbo.report1_temp";
                MessageBox.Show(upgCommand.ExecuteNonQuery().ToString()); 
                }
                catch (Exception ex)
                {
                MessageBox.Show(ex.ToString());
                }


            //update_2
                upgCommand.CommandType = CommandType.Text;
                upgCommand.CommandText = File.ReadAllText(path+@"\update_2.sql");
                try
                {
                    MessageBox.Show(upgCommand.ExecuteNonQuery().ToString());
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }
            //qry_3

                upgCommand.CommandType = CommandType.Text;
                upgCommand.CommandText = File.ReadAllText(path + @"\qry_3.sql");
                try
                {
                    MessageBox.Show(upgCommand.ExecuteNonQuery().ToString());
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }

            //final update

    upgCommand.CommandType = CommandType.Text;
    upgCommand.CommandText = File.ReadAllText(path + @"\update_3_on_cwsr_final.sql");
                try
                {
                    MessageBox.Show(upgCommand.ExecuteNonQuery().ToString());
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }



                //copy data to report1_temp
                upgCommand.CommandType = CommandType.Text;
                upgCommand.CommandText = File.ReadAllText(path+@"\copy.sql");
                try
                {
                    MessageBox.Show(upgCommand.ExecuteNonQuery().ToString());
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }

                ////update 3
                //upgCommand.CommandType = CommandType.Text;
                //upgCommand.CommandText = File.ReadAllText(@"C:\Users\smith\Documents\Visual Studio 2010\Projects\printreports\printreports\update1 on rpt1_temp.sql");
                //try
                //{
                //    MessageBox.Show(upgCommand.ExecuteNonQuery().ToString());
                //}
                //catch (Exception ex)
                //{
                //    MessageBox.Show(ex.ToString());
                //}

                //grand exection for report 
            
            cmd.CommandText = "select * from chloritech_d.dbo.report1_temp";
                try
                {
                    adapter = new SqlDataAdapter(cmd);
                    adapter.Fill(ds, "ad_query1");
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }
                try
                {                    
                    cws cws1 = new cws();
                    dt = ds.Tables["ad_query1"];
                    cws1.Tables["report1_temp"].Merge(dt);
                    dataGridView1.DataSource = cws1.Tables["report1_temp"];
                    ReportDocument rd1 = new ReportDocument();
                    rd1.Load(@"G:\Users\onyx\Dropbox\project2013\PrintReport\Printreport 17 Apr\printreports\printreports\csr.rpt");
                    crystalReportViewer1.ReportSource = rd1;
                    rd1.SetDataSource(cws1);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }
            
            ////delete data
            //String sql = "DELETE*FROM report1_temp";
            //cmd = new SqlCommand(sql, Con);
            //cmd.ExecuteNonQuery();


            //delete tables
            

            //String sql1 = "drop table chloritech_db.dbo.report1";
            //cmd = new SqlCommand(sql1, Con);
            //cmd.ExecuteNonQuery();

            //sql1 = "drop table chloritech_db.dbo.report2";
            //cmd = new SqlCommand(sql1, Con);
            //cmd.ExecuteNonQuery();
            //Con.Close();
        }
        
        
        public void process_query(String sdate ,String date, Boolean rank)
        {
            //Creating tebletemp

            contents = File.ReadAllText(path+@"\qry_1.sql");
            contents = contents.Replace("$start", sdate);
            contents = contents.Replace("$end", date);
            MessageBox.Show(contents + "From SHQ");
            cmd.CommandText = contents;

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

            MessageBox.Show("QRY_1 done");

            //update 1

            upgCommand = new SqlCommand();
            upgCommand.Connection = Con;
            upgCommand.CommandType = CommandType.Text;
            upgCommand.CommandText = File.ReadAllText(path + @"\update_1.sql");
            try
            {
                MessageBox.Show(upgCommand.ExecuteNonQuery().ToString());
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }

            ////update 2
            //upgCommand.CommandType = CommandType.Text;
            //upgCommand.CommandText = File.ReadAllText(@"C:\Users\smith\Documents\Visual Studio 2010\Projects\printreports\printreports\update2 on temptbl.sql");
            //try
            //{
            //    MessageBox.Show(upgCommand.ExecuteNonQuery().ToString());
            //}
            //catch (Exception ex)
            //{
            //    MessageBox.Show(ex.ToString());
            //}

            //grand Exectuion for tbl report1

            upgCommand.CommandType = CommandType.Text;
            contents = File.ReadAllText(path+@"\qry_2.sql");
            contents = contents.Replace("$start", sdate);
            contents = contents.Replace("$end", date);
            if (rank == false)
            {
                contents = contents.Replace("rcpt1", "rcpt2");
            }
            MessageBox.Show(contents + "From rq");
            upgCommand.CommandText = contents;
            try
            {
                MessageBox.Show(upgCommand.ExecuteNonQuery().ToString());
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            
            //String sql = "drop table chloritech_db.dbo.temptable";
            //cmd = new SqlCommand(sql, Con);
            //cmd.ExecuteNonQuery();
        }

        private void buttonX5_Click(object sender, EventArgs e)
        {
            slidePanel1.IsOpen = false;
        }

        private void crystalReportViewer1_Load(object sender, EventArgs e)
        {

        }
    }
}
