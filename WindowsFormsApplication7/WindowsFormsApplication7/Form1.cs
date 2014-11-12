using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevComponents.DotNetBar.Controls;
using System.Data.SqlClient;
using Microsoft.SqlServer.Management.Smo;
using Microsoft.SqlServer.Management.Common;
using System.DirectoryServices.AccountManagement;



namespace WindowsFormsApplication7
{

    public partial class Form1 : DevComponents.DotNetBar.Metro.MetroForm
    {

        Form fm;
        SqlConnection con;
        SqlCommand cmd;
        SqlDataAdapter adpt;
        DataSet dset, dset2;
        string constr, cmdstr;
        string server = @".\SQLEXPRESS";

        public Form1(Form f )
        {
            InitializeComponent();
            fm = f;
        }


        public void active_dir()
    {
        //// List of strings for your names
        //List<string> allUsers = new List<string>();

        //// create your domain context and define the OU container to search in
        //PrincipalContext ctx = new PrincipalContext(ContextType.Domain, "chloritech.com", "deepak", "singha@321");

        //// define a "query-by-example" principal - here, we search for a UserPrincipal (user)
        //UserPrincipal qbeUser = new UserPrincipal(ctx);

        //// create your principal searcher passing in the QBE principal    
        //PrincipalSearcher srch = new PrincipalSearcher(qbeUser);

        ////bool ans = ctx.ValidateCredentials("tejendra", "April2013");
        ////MessageBox.Show("" + ans);

        //// find all matches
        //foreach (var found in srch.FindAll())
        //{
        //    // do whatever here - "found" is of type "Principal" - it could be user, group, computer.....          
        //    //allUsers.Add(found.Sid);

        //    comboBoxEx1.Items.Add  (found.SamAccountName);
        //}
        //comboBoxEx1.Text = "SELECT  USER";
    }

        private void Form1_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'm3DataSet.login_details' table. You can move, or remove it, as needed.
            this.login_detailsTableAdapter1.Fill(this.m3DataSet.login_details);
            slidePanel1.BackColor = Color.RoyalBlue;
            tabControl1.BackColor = Color.White;
            labelX10.ForeColor = Color.RoyalBlue;
            labelX14.ForeColor = Color.RoyalBlue;
            
            active_dir();
            tabControlPanel4.CanvasColor = Color.White;
            constr = @"Data Source=.\SQLEXPRESS;Initial Catalog=M3;Integrated Security=True";
            con = new SqlConnection(constr);
        
           
            // TODO: This line of code loads data into the 'chloritechDataSet.login_details' table. You can move, or remove it, as needed.
            this.login_detailsTableAdapter.Fill(this.chloritechDataSet.login_details);

        }

        private void buttonX1_Click(object sender, EventArgs e)
        {
          

            DialogResult mb = MessageBox.Show("ARE  YOU  SURE  ABOUT  ADDING "+comboBoxEx1.Text+" AS  SYSTEM USER ",
       "VERIFICATION",
       MessageBoxButtons.YesNoCancel,
       MessageBoxIcon.Question,
       MessageBoxDefaultButton.Button1);
            try
            {

            if (DialogResult.Yes == mb)
            {

          cmdstr = "select * from login_details ";
          cmd = new SqlCommand(cmdstr, con);
          adpt = new SqlDataAdapter(cmd);
          dset2 = new DataSet();
          adpt.Fill(dset2, "login");
          DataRow row = dset2.Tables["login"].NewRow();
         
              
         row["user_name"] = comboBoxEx1.SelectedItem;


         dset2.Tables["login"].Rows.Add(row);
          SqlCommandBuilder cmdb = new SqlCommandBuilder(adpt);
         adpt.Update(dset2, "login");
         MessageBox.Show("user added  sucessfully ");
         
        }
                 else 
            {
                return;
            }
            }
            catch
            {MessageBox.Show("USER  IS  ALREADY ADDED");
                return;
            }

                
            }
           

        
        string img="";
        private void labelX9_Click(object sender, EventArgs e)
        {
            
            OpenFileDialog fd = new OpenFileDialog();
            fd.Title = "select  JPG  image ";
            fd.InitialDirectory = @"d:\";
            if (fd.ShowDialog() == DialogResult.OK) 
            img = fd.FileName.ToString();
            { labelX9.Text = fd.FileName.ToString(); }
        }

        private void buttonX2_Click(object sender, EventArgs e)
        {

            DialogResult mb = MessageBox.Show("YOU ARE  UPDATING DEATILS OF " + comboBoxEx1.Text + " DO  YOU  WANT TO  CONTINUE ? ",
       "VERIFICATION",
       MessageBoxButtons.YesNo,
       MessageBoxIcon.Question,
       MessageBoxDefaultButton.Button1);

            if (DialogResult.No == mb)
                return;

            Image pic = Image.FromFile(img);

            ImageConverter ic = new ImageConverter();
          byte[] bt = (byte[])ic.ConvertTo(pic, typeof(byte[]));

          con.Open();
         
              cmdstr = "select * from M3.dbo.login_details where user_name='" + comboBoxEx3.Text + "'";
              cmd = new SqlCommand(cmdstr, con);
              adpt = new SqlDataAdapter(cmd);
              dset2 = new DataSet();
              adpt.Fill(dset2, "login");

              
          foreach (DataRow row in dset2.Tables["login"].Rows)
          {
              if (textBoxX1.Text != "")
                  row["user_name"] = textBoxX1.Text;
            
                  if (img != "")
                      row["img"] = bt;


                  SqlCommandBuilder cmdb = new SqlCommandBuilder(adpt);
                  adpt.Update(dset2, "login");
             
              
          }
          MessageBox.Show("sucessfully  updated");
          con.Close();

        }
        
        Int16[] bit = new Int16[11];

        private void buttonX4_Click(object sender, EventArgs e)
        {
            cb_bit();
            constr = @"Data Source=.\SQLEXPRESS;Initial Catalog=M3;Integrated Security=True";
            con = new SqlConnection(constr);
            con.Open();
            cmdstr = "select * from login_details where user_name='" + comboBoxEx2.Text + "'";
            cmd = new SqlCommand(cmdstr, con);
            adpt = new SqlDataAdapter(cmd);
            dset2 = new DataSet();
            adpt.Fill(dset2, "login");
            foreach (DataRow row in dset2.Tables["login"].Rows)
            {
                row["grn"] = bit[0];
                row["issue"] = bit[4];
                row["indent"] = bit[5];
                row["rpt_sil"] = bit[1];
                row["rpt_cwr"] = bit[2];
                row["rpt_ail"] = bit[3];
                row["rpt_cwssr"] = bit[6];
                row["m3"] = bit[7];
                row["master"] = bit[8];
                row["po"] = bit[10];
                row["posend"] =  bit[9];
                

            }
            SqlCommandBuilder cmdb = new SqlCommandBuilder(adpt);
            adpt.Update(dset2, "login");
            MessageBox.Show("sucessfully  updated");
            
        }
        private void cb_bit()
        {
            CheckBoxX cb;

            for (int i = 0; i < 11; i++)
            {

                cb = (CheckBoxX)panelEx1.Controls["checkBoxX" + (i + 1)];
                if (cb.Checked)
                    bit[i] = 1;
                else
                    bit[i] = 0;
            }
        }

        private void buttonX5_Click(object sender, EventArgs e)
        {
            slidePanel1.IsOpen = false;
        }

        private void Form1_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right && !slidePanel1.IsOpen)
                slidePanel1.IsOpen = true;
            else
                slidePanel1.IsOpen = false;
        }

        private void buttonX7_Click(object sender, EventArgs e)
        {
            textBoxX2.Text = server;
            labelX14.Text = "NEW  YEAR MANAGEMENT";
            tabControl1.Visible = false;
            tabControl2.Location = tabControl1.Location;
            tabControl2.Visible = true;
            get_database();
            
        }

        public void get_database()
        {

            string constr1 = @"Data Source="+textBoxX2.Text+";Integrated Security=True";
            cmdstr = "select NAME from master.sys.databases;";

        con = new SqlConnection(constr1);
        cmd = new SqlCommand(cmdstr, con);
        adpt = new SqlDataAdapter(cmd);
        dset = new DataSet();
        con.Open();

        adpt.Fill(dset, "db");

        BindingSource bndsrc1 = new BindingSource();
        bndsrc1.DataSource = dset;
        bndsrc1.DataMember = "db";
        comboBoxEx4.DataSource = bndsrc1;
        comboBoxEx4.ValueMember = "NAME";
        comboBoxEx4.DisplayMember = "NAME";

        comboBoxEx5.DataSource = bndsrc1;
        comboBoxEx5.ValueMember = "NAME";
        comboBoxEx5.DisplayMember = "NAME";

        con.Close();
    }
        private void buttonX6_Click(object sender, EventArgs e)
        {
            labelX14.Text = "ACCESS PROVIDER";
            tabControl2.Visible = false;
            //tabControl2.Location = tabControl1.Location;
            tabControl1.Visible = true;
        }

        private void tabControl1_Click(object sender, EventArgs e)
        {

        }

        private void buttonX9_Click(object sender, EventArgs e)
        {
            Server serv = new Server(@".\SQLEXPRESS");

            Database db1 = new Database(serv, textBoxX3.Text);
            db1.Create();



            Database db = serv.Databases[comboBoxEx5.Text];
            //new Database(serv,"smit");




            Transfer t = new Transfer(db);
            t.CopyAllObjects = true;
            //t.DropDestinationObjectsFirst = true;
            //t.CopySchema = true;
            t.CopyData = true;
            t.DestinationServer = @".\SQLEXPRESS";
            t.DestinationDatabase = textBoxX3.Text;
            t.Options.IncludeIfNotExists = true;
            t.CreateTargetDatabase = true;


            t.TransferData();
            MessageBox.Show("Data transfer  sucessful");
            //db.Create();
        }

        private void buttonX8_Click(object sender, EventArgs e)
        {
            constr = @"Data Source=.\SQLEXPRESS;Initial Catalog=M3;Integrated Security=True";
            cmdstr = "select pk ,constr from sync where pk=1";

            con = new SqlConnection(constr);
            cmd = new SqlCommand(cmdstr, con);
            adpt = new SqlDataAdapter(cmd);
            dset = new DataSet();
            con.Open();

            adpt.Fill(dset, "chk");

            foreach (DataRow row in dset.Tables["chk"].Rows)
            {

                row["constr"] = "Data Source=.\\SQLEXPRESS;Initial Catalog="+comboBoxEx4.Text+";Integrated Security=True"; 

                SqlCommandBuilder cmdb = new SqlCommandBuilder(adpt);
                adpt.Update(dset, "chk");
            }

        }

        private void button1_Click(object sender, EventArgs e)
        {
            cmnall.cmncls.chnage_image();
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            cmnall.cmncls.check_off("m3");
            fm.Show();
        }
    }
}