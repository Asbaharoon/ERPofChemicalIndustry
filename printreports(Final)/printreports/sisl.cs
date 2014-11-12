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
    public partial class sisl : Form
    {
        public sisl()
        {
            InitializeComponent();
        }
        string sqlCon = " Data Source=.\\SQLEXPRESS;Initial Catalog=chloritech_db;Integrated Security=True";
        SqlConnection Con;
        SqlCommand cmd;
        DataSet ds;
        DataTable dt;
        SqlDataAdapter adapter;
        string contents;
        string pathq = @"D:\ERP with my management\printreports(Final)\printreports";
        string pathr = @"D:\ERP with my management\printreports(Final)\printreports";

        private void sisl_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'chloritech_dbDataSet.tblitemmaster' table. You can move, or remove it, as needed.
            this.tblitemmasterTableAdapter.Fill(this.chloritech_dbDataSet.tblitemmaster);

        }

        private void button1_Click(object sender, EventArgs e)
        {
            String itmname = comboBox1.SelectedValue.ToString();
            String date="'"+dateTimePicker1.Text+"'";
            dateTimePicker1.Value = dateTimePicker1.Value.Subtract(TimeSpan.FromDays(1));
            String date1 = "'" + dateTimePicker1.Text + "'";
            String date2 = "'" + dateTimePicker2.Text + "'";
            panel1.Visible = false;
            crystalReportViewer1.Visible = true;
            cmd = new SqlCommand();
            Con = new SqlConnection(sqlCon);
            cmd.CommandType = CommandType.Text;
            cmd.Connection = Con;
            ds = new DataSet();
            cws cws1 = new cws();

            Con.Open();

            //executing sisl_id            
            if (date == "'2012-04-01'")
            {
                contents = File.ReadAllText(pathq+@"\sisl_idmain1.sql");
                contents = contents.Replace("$itm", "'" + itmname + "'");                
            }
            else
            {
                contents = File.ReadAllText(pathq + @"\sisl_idmain.sql"); 
                contents = contents.Replace("$itm", "'" + itmname + "'");
                contents = contents.Replace("$start", "'2012-04-01'");
                contents = contents.Replace("$end", date1); 
            }
            cmd.CommandText = contents;
            //MessageBox.Show(contents);
            try
            {
                adapter = new SqlDataAdapter(cmd);
                adapter.Fill(ds, "ans_opening");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            try
            {
                dt = ds.Tables["ans_opening"];
                cws1.Tables["sisl_id"].Merge(dt);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            //grn fill
            contents = File.ReadAllText(pathq + @"\sisl_grn.sql"); 
            //MessageBox.Show(itmname);
            contents = contents.Replace("$itm", "'" + itmname + "'");
            contents = contents.Replace("$start",date);
            contents = contents.Replace("$end",date2); 
            //MessageBox.Show(contents);
            cmd.CommandText = contents;
            try
            {
                adapter = new SqlDataAdapter(cmd);
                adapter.Fill(ds, "ans_grn");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            try
            {
                dt = ds.Tables["ans_grn"];
                cws1.Tables["sisl_grn"].Merge(dt);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            //issue fill
            contents = File.ReadAllText(pathq + @"\sisl_issue.sql"); 
            //MessageBox.Show(itmname);
            contents = contents.Replace("$itm", "'" + itmname + "'");
            contents = contents.Replace("$start", date);
            contents = contents.Replace("$end", date2);
            //MessageBox.Show(contents);
            cmd.CommandText = contents;
            try
            {
                adapter = new SqlDataAdapter(cmd);
                adapter.Fill(ds, "ans_issue");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            try
            {
                dt = ds.Tables["ans_issue"];
                cws1.Tables["sisl_issue"].Merge(dt);                
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            ReportDocument rd1 = new ReportDocument();
            ReportDocument rd2 = new ReportDocument();
            ReportDocument rd3 = new ReportDocument();
            rd1.Load(pathr + @"\sisl_main.rpt");
            rd2.Load(pathr + @"\sisl1.rpt");
            rd3.Load(pathr + @"\sisl2.rpt");
            rd1.SetDataSource(cws1);
            rd2.SetDataSource(cws1);
            rd3.SetDataSource(cws1);
            
            crystalReportViewer1.ReportSource = rd1;
        }        

    }
}
