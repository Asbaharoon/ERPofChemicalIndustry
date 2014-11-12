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

namespace purchase
{
    public partial class Form3 : Form
    {
        int pono_print;
        public Form3(int a)
        {
            InitializeComponent();
            pono_print = a;
        }
        string sqlCon = @"Data Source=.\SQLEXPRESS;Initial Catalog=chloritech_db;Integrated Security=True";
        SqlConnection Con;
        SqlCommand cmd;
        DataSet ds;
        DataTable dt;
        SqlDataAdapter adapter;
        ReportDocument rd1 = new ReportDocument();
        string path = @"G:\Users\onyx\Dropbox\project2013\25april\purchase\purchase";
        string folderPath;
        private void Form3_Load(object sender, EventArgs e)
        {
            cmd = new SqlCommand();
            Con = new SqlConnection(sqlCon);
            cmd.CommandType = CommandType.Text;
            cmd.Connection = Con;
            ds = new DataSet();
            Con.Open();
            
            cmd.CommandText = "select ts.id,ts.suppliername,ts.openingbalance,ts.add1,ts.add2,ts.add3,ts.city,ts.pincode,ts.state,ts.email,ts.phone from chloritech_db.dbo.tblsuppliermaster ts , chloritech_db.dbo.tblpo po where ts.suppliername=po.supplier_name and po.pono="+pono_print;
            try
            {
                adapter = new SqlDataAdapter(cmd);
                adapter.Fill(ds, "supdet");
                DataSetpo dpo = new DataSetpo();
                dt = ds.Tables["supdet"];
                dpo.Tables["tblsuppliermaster"].Merge(dt);

                //main part
                cmd.CommandText = "select * from tblpodetails where pono=" + pono_print;
                adapter = new SqlDataAdapter(cmd);
                adapter.Fill(ds, "supmain");
                dt = ds.Tables["supmain"];
                dpo.Tables["tblpodetails"].Merge(dt);
                //end
                rd1.Load(path + @"\po.rpt");
                crystalReportViewer1.ReportSource = rd1;
                rd1.SetDataSource(dpo);
                //retrive parameter top

                cmd.CommandText = "select * from chloritech_db.dbo.tblpo where pono=" + pono_print;
                    adapter = new SqlDataAdapter(cmd);
                    adapter.Fill(ds, "pardata");
                    foreach (DataRow rrow in ds.Tables["pardata"].Rows)
                    {
                        rd1.SetParameterValue("voucher_no", rrow["voucher"].ToString());
                        rd1.SetParameterValue("Dated", rrow["date"]);
                        rd1.SetParameterValue("mtp", rrow["terms"].ToString());
                        rd1.SetParameterValue("srf", rrow["ref"].ToString());
                        rd1.SetParameterValue("dth", rrow["dispatch"].ToString());
                        rd1.SetParameterValue("dest", rrow["destination"].ToString());
                        rd1.SetParameterValue("Tax_des", rrow["tex_dec"].ToString());
                        rd1.SetParameterValue("tot_amt", rrow["gross"].ToString());
                        rd1.SetParameterValue("tot_tax", rrow["tex"].ToString());
                    }
                // end retrive
                    DialogResult mb = MessageBox.Show("Do you want to generate Pdf to send ", "VERIFICATION", MessageBoxButtons.YesNo,MessageBoxIcon.Question, MessageBoxDefaultButton.Button1);
                    if (DialogResult.Yes == mb)
                    {
                        
                        FolderBrowserDialog fdb = new FolderBrowserDialog();
                        
                            if (fdb.ShowDialog() == DialogResult.OK )
                            {
                                folderPath = fdb.SelectedPath;                             
                            }
                            if (folderPath != null)
                            {
                                folderPath = folderPath + @"\po_" + pono_print + ".pdf";
                                rd1.ExportToDisk(ExportFormatType.PortableDocFormat, folderPath);
                            }
                            else 
                            {
                                MessageBox.Show("Select Folder so save file");
                            }
                    }
                    else
                    {
                        return;
                    }
                
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            
        }

    }
}
