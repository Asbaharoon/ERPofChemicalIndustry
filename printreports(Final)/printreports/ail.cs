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
    public partial class ail : Form
    {
        public ail()
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
        string pathq = @"G:\Users\onyx\Dropbox\project2013\Project Final\printreports(Final)\printreports\queries for ail";
        string pathr = @"G:\Users\onyx\Dropbox\project2013\Project Final\printreports(Final)\printreports";

        private void button1_Click(object sender, EventArgs e)
        {
       
            String date =   dateTimePicker1.Text ;
           
            dateTimePicker1.Value = dateTimePicker1.Value.Subtract(TimeSpan.FromDays(1));
            
            String date1 = "'"+dateTimePicker1.Text+"'";
            String date2 = "'" + dateTimePicker2.Text + "'";

            if (dateTimePicker1.Value > dateTimePicker2.Value)
            {
                MessageBox.Show("PLEASE  SELECT  PROPER  DATE  IN  ORDER");
                return;
            }

            slidePanel1.IsOpen = false;
            crystalReportViewer1.Visible = true;
            cmd = new SqlCommand();
            Con = new SqlConnection(sqlCon);
            cmd.CommandType = CommandType.Text;
            cmd.Connection = Con;
            Con.Open();

            //call 911
            if (date == "2012-04-01")
            {
                //MessageBox.Show("if  condition  satisfied ");
                process_query("'2012-03-01'","'2012-03-30'", true);
            }
            else
                process_query("'2012-04-01'", date1, true);

                process_query("'2012-04-01'",date2, false);


                ////delete data
                //try
                //{
                //upgCommand.CommandType = CommandType.Text;
                //upgCommand.CommandText = "delete from chloritech_db.dbo.report1_temp";
                //MessageBox.Show(upgCommand.ExecuteNonQuery().ToString()); 
                //}
                //catch (Exception ex)
                //{
                //MessageBox.Show(ex.ToString());
                //}


            //update_2
                upgCommand.CommandType = CommandType.Text;
                upgCommand.CommandText = File.ReadAllText(pathq+@"\update_2(qty).sql");
                try
                {
                    //MessageBox.Show(upgCommand.ExecuteNonQuery().ToString());
                }
                catch (Exception ex)
                {
                    //MessageBox.Show(ex.ToString());
                }
            //qry_3

                upgCommand.CommandType = CommandType.Text;
                upgCommand.CommandText = File.ReadAllText(pathq + @"\qry_3(qty).sql");
                try
                {
                    //MessageBox.Show(upgCommand.ExecuteNonQuery().ToString());
                }
                catch (Exception ex)
                {
                    //MessageBox.Show(ex.ToString());
                }

            //final update

    upgCommand.CommandType = CommandType.Text;
    upgCommand.CommandText = File.ReadAllText(pathq + @"\update_3(qty).sql");
                try
                {
                    //MessageBox.Show(upgCommand.ExecuteNonQuery().ToString());
                }
                catch (Exception ex)
                {
                    //MessageBox.Show(ex.ToString());
                }



                //copy data to report1_temp
                upgCommand.CommandType = CommandType.Text;
                upgCommand.CommandText = File.ReadAllText(pathq+@"\copy(qty).sql");
                try
                {
                    //MessageBox.Show(upgCommand.ExecuteNonQuery().ToString());
                }
                catch (Exception ex)
                {
                    //MessageBox.Show(ex.ToString());
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

                cmd.CommandText = "select * from chloritech_d.dbo.ail_rcpt";
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
                    cws1.Tables["ail_rcpt"].Merge(dt);
                   
                    ReportDocument rd1 = new ReportDocument();
                    rd1.Load(pathr + @"\ail_rpt.rpt");
                    crystalReportViewer1.ReportSource = rd1;
                    rd1.SetDataSource(cws1);
                   
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }

                 
        

        }

        public void process_query(String sdate, String date, Boolean rank)
        {
            //Creating tebletemp

            contents = File.ReadAllText(pathq + @"\qry_1(qty).sql");
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
            upgCommand.CommandText = File.ReadAllText(pathq + @"\update_1(qty).sql");
            try
            {
                //MessageBox.Show(upgCommand.ExecuteNonQuery().ToString());
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }

           

            upgCommand.CommandType = CommandType.Text;
            contents = File.ReadAllText(pathq + @"\qry_2(qty).sql");
            contents = contents.Replace("$start", sdate);
            contents = contents.Replace("$end", date);
            if (rank == false)
            {
                contents = contents.Replace("rcp1", "rcp2");
            }
            //MessageBox.Show(contents + "From rq");
            upgCommand.CommandText = contents;
            try
            {
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
            slidePanel1.IsOpen =false;
        }

        private void buttonX1_Click(object sender, EventArgs e)
        {
            slidePanel1.IsOpen = true;
        }

        private void buttonX3_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void buttonX2_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

    }
}
