using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevComponents.DotNetBar;
using System.Net;
using System.Net.Mail;
using System.Threading;
using System.ComponentModel;
using System.IO.Ports;
using System.Data.SqlClient;

namespace purchase
{
    public partial class mailpo : DevComponents.DotNetBar.Metro.MetroForm
    {
        public mailpo()
        {
            InitializeComponent();
        }
        SqlConnection con;
        SqlCommand cmd;
        SqlDataAdapter adpt;
        DataSet dset, dset1;
        String cmdstr;
        Attachment atc;

        private void mailpo_Load(object sender, EventArgs e)
        {
            String constr = @"Data Source=.\SQLEXPRESS;Initial Catalog=chloritech_db;Integrated Security=True";
            cmdstr = "select pono from tblpo";
            con = new SqlConnection(constr);
            cmd = new SqlCommand(cmdstr, con);
            adpt = new SqlDataAdapter(cmd);
            dset = new DataSet();
            con.Open();
            adpt.Fill(dset, "tblpo");

            BindingSource bndsrc1 = new BindingSource();
            bndsrc1.DataSource = dset;
            bndsrc1.DataMember = "tblpo";
            comboBoxEx1.DataSource = bndsrc1;
            comboBoxEx1.ValueMember = "pono";
            comboBoxEx1.DisplayMember = "pono";

            cmdstr = "select suppliername from tblsuppliermaster";
            cmd = new SqlCommand(cmdstr, con);
            adpt = new SqlDataAdapter(cmd);
            adpt.Fill(dset, "sup_name");
            BindingSource bndsrc2 = new BindingSource();
            bndsrc2.DataSource = dset;
            bndsrc2.DataMember = "sup_name";
            comboBoxEx2.DataSource = bndsrc2;
            comboBoxEx2.ValueMember = "suppliername";
            comboBoxEx2.DisplayMember = "suppliername";
        }

        private void buttonX1_Click(object sender, EventArgs e)
        {
            String input = string.Empty;
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Filter = "pdf files (*.pdf)|*.pdf|All files (*.*)|*.*";
            dialog.InitialDirectory = "C:"; dialog.Title = "Select a pdf file";
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                input = dialog.FileName;
                labelX7.Text = input;
                buttonX1.Text = "Change File";
            }
                
        }
        blackload f3;
        private void buttonX2_Click(object sender, EventArgs e)
        {
            Thread mt;
            MailMessage mail = new MailMessage();
            //String from = textBoxX1.Text+"@chloritech.com";
            String from = textBoxX1.Text;
            var client = new SmtpClient("smtp.gmail.com", 587)
            {
                Credentials = new NetworkCredential(from, textBoxX2.Text),
                EnableSsl = true
            };

            try { atc = new Attachment(labelX7.Text); }
            catch { MessageBox.Show("No Attacment Found"); }

            //set the addresses
            mail.From = new MailAddress(from);
            mail.To.Add(textBoxX3.Text);

            //set the content
            mail.Subject = textBoxX5.Text;
            mail.Body = "<html><body>" + textBoxX4.Text + "</body></html>";
            mail.IsBodyHtml = true;
            mail.Attachments.Add(atc);
            //mt = new Thread(new ThreadStart(uploadme));
            //mt.Start();
            //blackload f3;
            try
            {
               
                client.Send(mail); 
               // f3.msg();
            }
            catch
            {
                
                MessageBox.Show("Mail not sent. Check Internet Connection");
            }
            finally
            {
                this.UseWaitCursor = false; //mt.Abort();
            }
            //mt.Abort();
            
        }
       
        public void uploadme()
        {
             f3 = new blackload();

            f3.ShowDialog();
        }
    }
}