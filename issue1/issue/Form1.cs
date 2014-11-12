using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;
using DevComponents.DotNetBar;
using cmnall;

namespace issue
{
    public partial class Form1 : Form
    {
        SqlConnection con;
        SqlCommand cmd;
        SqlDataAdapter adpt;
        SqlCommandBuilder bldr;
        DataSet dset;
        BindingSource bndsrc;
        

        string constr, cmdstr;
        int n = 0, m = 0;
        int count = 0,temp=0,flag=0,count1=0;
        int tbc = 4, cbc = 5;
        
        public String getText()
        {

            String mystring = textBox40.Text;
            return mystring;
        }
          
        public class OurT : TextBox
        {

            public OurT(int i)
            {
                Font f = new Font("Segoe ui Symbol", 12, FontStyle.Regular);
                this.Font = f;
                this.Name = "" + i;
                
            }

        }

        public class OurC : ComboBox
        {
            public OurC(int i)
            {
                Font f = new Font("Segoe ui Symbol", 12, FontStyle.Regular);
                this.Font = f;
                this.Name = "" + i;
                

            }

        }

        public class OurDT : DateTimePicker
        {
            public OurDT()
            {
                Font f = new Font("Segeo ui Symbol", 12, FontStyle.Regular);
                this.Font = f;

            }
        }

        List<TextBox> ltb = new List<TextBox>();
        List<ComboBox> lcb = new List<ComboBox>();
        List<DateTimePicker> dtp = new List<DateTimePicker>();
        Size sz = new Size(40, 29);

        public Form1()
        {
            InitializeComponent();
        }

        public void main_logic(object sender, EventArgs e)
        {
            
            ComboBox cb2 = (ComboBox)sender;
            int index = 0;
            for (int i = 0; i < lcb.Count; i++)
            {
                if (lcb[i].Name == cb2.Name)
                {
                    index = i;
                    break;
                }

            }

            try
            {

                int a;
                String mystring = cb2.SelectedValue.ToString();
                a = Convert.ToInt32(mystring);



                cmdstr = "select id,itemname from tblitemmaster where category=" + a + "";
                con = new SqlConnection(constr);
                cmd = new SqlCommand(cmdstr, con);
                adpt = new SqlDataAdapter(cmd);
                dset = new DataSet();
                con.Open();

                adpt.Fill(dset, "itemx");

                //BindingSource bndsrc = new BindingSource();
                bndsrc = new BindingSource();
                bndsrc.DataSource = dset;
                bndsrc.DataMember = "itemx";

                lcb[index + 1].DataSource = bndsrc;
                lcb[index + 1].ValueMember = "itemname";
                lcb[index + 1].DisplayMember = "itemname";
                //BindingSource bndsrc1 = new BindingSource();
                //bndsrc.DataSource = dset;
                //bndsrc.DataMember = "itemx";
                //lcb[index + 2].DataSource = bndsrc;

                //lcb[index + 2].ValueMember = "id";
                //lcb[index + 2].DisplayMember = "itemname";

                con.Close();
            }
            catch (Exception s)
            {
                Console.WriteLine(" " + s);
            }

        }


        public void main_logic2(object sender, EventArgs e)
        {
            ComboBox cb1 = (ComboBox)sender;
           
            int index = 0;
            for (int i = 0; i < lcb.Count; i++)
            {

                if (lcb[i].Name == cb1.Name)
                {
                    index = i;
                    break;
                }

            }

           
            //try
            //{

                String mystring2 = cb1.SelectedValue.ToString();
                cmdstr = "select subcostID,subcostcentre from tbl2subcostcentre where mainID="+ Convert.ToInt32(mystring2) +"";
               
                cmd = new SqlCommand(cmdstr, con);
                adpt = new SqlDataAdapter(cmd);
                dset = new DataSet();
                con.Open();

                adpt.Fill(dset, "cs");

                
                bndsrc = new BindingSource();
                bndsrc.DataSource = dset;
                bndsrc.DataMember = "cs";

                lcb[index + 1].DataSource = bndsrc;
                lcb[index + 1].ValueMember = "subcostID";
                lcb[index + 1].DisplayMember = "subcostcentre";


                con.Close();

        }

        public void main_logic3(object sender, EventArgs e)
        {
            ComboBox cb1 = (ComboBox)sender;

            int index = 0;
            for (int i = 0; i < lcb.Count; i++)
            {

                if (lcb[i].Name == cb1.Name)
                {
                    index = i;
                    
                    break;
                }

            }

           
            //try
            //{

            
            String mystring2 = cb1.SelectedValue.ToString();
          


            

            cmdstr = "select semisubID ,semisubcostcentre from tbl3semisubcostcentre  where subcostID = " +   mystring2  + "";
          
            cmd = new SqlCommand(cmdstr, con);
            adpt = new SqlDataAdapter(cmd);
            dset = new DataSet();
            //con.Open();

            adpt.Fill(dset, "cs");


            bndsrc = new BindingSource();
            bndsrc.DataSource = dset;
            bndsrc.DataMember = "cs";

            lcb[index + 1].DataSource = bndsrc;
            lcb[index + 1].ValueMember = "semisubID";
            lcb[index + 1].DisplayMember = "semisubcostcentre";


            con.Close();
        }

        
        private void buttonX1_Click(object sender, EventArgs e)
        {
           

            ltb.Add(new OurT(n++));
            lcb.Add(new OurC(m++));
            lcb.Add(new OurC(m++));
            ltb.Add(new OurT(n++));
            ltb.Add(new OurT(n++));
            lcb.Add(new OurC(m++));
            lcb.Add(new OurC(m++));
            lcb.Add(new OurC(m++));
            
            dtp.Add(new OurDT());
            ltb.Add(new OurT(n++));

         
            cmdstr = "select catid,category from tblcategorymasters";

                
            cmd = new SqlCommand(cmdstr, con);
            adpt = new SqlDataAdapter(cmd);
            dset = new DataSet();
            con.Open();             

            adpt.Fill(dset, "db");



            bndsrc = new BindingSource();
            bndsrc.DataSource = dset;
            bndsrc.DataMember = "db";
            lcb[count * cbc].DataSource = bndsrc;
            lcb[count * cbc].ValueMember = "catid";
            lcb[count * cbc].DisplayMember = "category";


            cmd.CommandText = "select * from tbl1maincostcentre";
            adpt.Fill(dset, "cs2");
            BindingSource bndsrc2 = new BindingSource();
            bndsrc2.DataSource = dset;
            bndsrc2.DataMember = "cs2";
            lcb[count * cbc + 2].DataSource = bndsrc2;
            lcb[count * cbc + 2].DisplayMember = "maincostcentre";
            lcb[count * cbc + 2].ValueMember = "mainID"; ;
            con.Close();



            cmdstr = "select subcostID,subcostcentre from tbl2subcostcentre";
                
            cmd = new SqlCommand(cmdstr, con);
            adpt = new SqlDataAdapter(cmd);
            dset = new DataSet();
            con.Open();

            adpt.Fill(dset, "cs");


            bndsrc = new BindingSource();
            bndsrc.DataSource = dset;
            bndsrc.DataMember = "cs";

            lcb[count * cbc + 3].DataSource = bndsrc;
            lcb[count * cbc + 3].ValueMember = "subcostID";
            lcb[count * cbc + 3].DisplayMember = "subcostcentre";


            con.Close();

            
    


            sz.Width = 30;
            ltb[count * tbc].Size = sz;
            flowLayoutPanel1.Controls.Add(ltb[count * tbc]);
 

            sz.Width = 106;
            lcb[count * cbc].Size = sz;
            flowLayoutPanel1.Controls.Add(lcb[count * cbc]);
            lcb[count * cbc].SelectedIndexChanged += new System.EventHandler(this.main_logic);

            sz.Width = 152;
            lcb[(count * cbc) + 1].Size = sz;
            flowLayoutPanel1.Controls.Add(lcb[count * cbc + 1]);
            

            sz.Width = 72;
            ltb[(count * tbc) + 1].Size = sz;
            flowLayoutPanel1.Controls.Add(ltb[count * tbc + 1]);

            sz.Width = 92;
            ltb[(count * tbc) + 2].Size = sz;
            flowLayoutPanel1.Controls.Add(ltb[count * tbc + 2]);

            sz.Width = 153;
            lcb[(count * cbc) + 2].Size = sz;
            flowLayoutPanel1.Controls.Add(lcb[count * cbc + 2]);
            lcb[(count * cbc) + 2].SelectedIndexChanged += new System.EventHandler(this.main_logic2);


            sz.Width = 137;
            lcb[(count * cbc) + 3].Size = sz;
            flowLayoutPanel1.Controls.Add(lcb[count * cbc + 3]);
            lcb[(count * cbc) + 3].SelectedIndexChanged += new System.EventHandler(this.main_logic3);


            sz.Width = 159;
            lcb[(count * cbc) + 4].Size = sz;
            flowLayoutPanel1.Controls.Add(lcb[count * cbc + 4]);

            sz.Width = 177;
            dtp[count].Size = sz;
            flowLayoutPanel1.Controls.Add(dtp[count]);

            sz.Width = 92;
            ltb[(count * tbc) + 3].Size = sz;
            flowLayoutPanel1.Controls.Add(ltb[count * tbc + 3]);

            m = m + 4;
            n = n + 5;
            count++;
        }

        public void bind()
        {
            constr = "Data Source=.\\SQLEXPRESS;Initial Catalog=chloritech_db;Integrated Security=True";
            constr = cmnall.cmncls.get_constr111();
        }
        private void Form1_Load(object sender, EventArgs e)
        {

            bind();
            cmdstr = "select catid,category from tblcategorymasters";

            con = new SqlConnection(constr);
            cmd = new SqlCommand(cmdstr, con);
            adpt = new SqlDataAdapter(cmd);
            dset = new DataSet();
            con.Open();
            

            adpt.Fill(dset, "issue");

            bndsrc = new BindingSource();
            bndsrc.DataSource = dset;
            bndsrc.DataMember = "issue";
            comboBox2.DataSource = bndsrc;
            comboBox2.ValueMember = "catid";
            comboBox2.DisplayMember = "category";

            con.Close();
          

            temp = max_no_of_issue();

            textBox18.Text = String.Format("{0} of {0}", (temp + 1));

            textBox40.Text = "" + (temp + 1);

            cmdstr = "select id,issuecategory from tblissuecategory";

            con = new SqlConnection(constr);
            cmd = new SqlCommand(cmdstr, con);
            adpt = new SqlDataAdapter(cmd);
            dset = new DataSet();
            con.Open();

            adpt.Fill(dset, "issue");

            bndsrc = new BindingSource();
            bndsrc.DataSource = dset;
            bndsrc.DataMember = "issue";
            comboBox4.DataSource = bndsrc;
            comboBox4.ValueMember = "id";
            comboBox4.DisplayMember = "issuecategory";

            con.Close();

            buttonX1.PerformClick();
        }

        public int max_no_of_issue()
        {
            int max = 0;
            cmdstr = "select issuslipno from tblissueslip";
            cmd = new SqlCommand(cmdstr, con);
            adpt = new SqlDataAdapter(cmd);
            con.Open();

            adpt.Fill(dset, "issuen");


            foreach (DataRow srow in dset.Tables["issuen"].Rows)
            {
                int val = Convert.ToInt32(srow["issuslipno"].ToString());
                if (val >= max && val != 2000)
                {
                    max = val;
                }
            }

            con.Close();
            return max;
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                int a;
                String mystring = comboBox2.SelectedValue.ToString();
                a = Convert.ToInt32(mystring);

                cmdstr = "select id,units,itemname from tblitemmaster where category=" + a + "";
               
                cmd = new SqlCommand(cmdstr, con);
                adpt = new SqlDataAdapter(cmd);
                dset = new DataSet();
                con.Open();

                adpt.Fill(dset, "iss1");


                bndsrc = new BindingSource();
                bndsrc.DataSource = dset;
                bndsrc.DataMember = "iss1";



                comboBox5.DataSource = bndsrc;
                comboBox5.ValueMember = "id";
                comboBox5.DisplayMember = "itemname";

                con.Close();
            }
            catch (Exception s)
            {
                Console.WriteLine(" " + s);
            }
        }

        private void buttonX2_Click(object sender, EventArgs e)
        {
            try
            {
                int tmp = ltb.Count, tmp2 = lcb.Count;
                for (int i = 0; i < tbc; i++)
                {
                    ltb[tmp - 1 - i].Dispose();
                    ltb.RemoveAt(tmp - 1 - i);
                }
                for (int i = 0; i < cbc; i++)
                {
                    lcb[tmp2 - 1 - i].Dispose();
                    lcb.RemoveAt(tmp2 - 1 - i);
                }
                dtp[dtp.Count - 1].Dispose();
                dtp.RemoveAt(dtp.Count - 1);


                count--;
                m = m - cbc;
                n = n - tbc;
            }
            catch
            {
                MessageBox.Show("All rows are deleted");
            }


            
        }

        private void buttonX4_Click(object sender, EventArgs e)
        {
            try
            {

                int issueslip_no = Convert.ToInt32(textBox40.Text), issueslip_no_new = 0, count1 = 0, temp2 = (temp + 1);


                if (((ButtonX)sender).Name.ToString() == "buttonX4")
                {
                    if (flag == 1)
                    {
                        issueslip_no_new = Convert.ToInt32(textBox18.Text);
                        if (issueslip_no_new <= 0)
                        {
                            MessageBox.Show("Not Valid Issueslip_no1");
                            flag = 0;
                            return;
                        }
                        flag = 0;
                       
                    }
                    else
                    {
                        issueslip_no_new = (issueslip_no - 1);
                        if (issueslip_no <= 0)
                        {
                            MessageBox.Show("Not Valid Issueslip_no2");
                            return;

                        }
                        buttonX4.Enabled = true;
                    }
                }
                else if (((ButtonX)sender).Name.ToString() == "buttonX5")
                {
                    issueslip_no_new = 1;
                }
                else if (((ButtonX)sender).Name.ToString() == "buttonX7")
                {

                    issueslip_no_new = (issueslip_no + 1);

                }
                else if (((ButtonX)sender).Name.ToString() == "buttonX3")
                {
                    issueslip_no_new = temp;
                }
                textBox40.Text = "" + issueslip_no_new;
                textBox18.Text = String.Format("{0} of {1}", issueslip_no_new, temp2);
                flowLayoutPanel1.Hide();
                for (int i = 0; i < ltb.Count; i++)
                {
                    ltb[i].Dispose();
                    m--;
                }
                for (int i = 0; i < lcb.Count; i++)
                {
                    lcb[i].Dispose();
                }
                for (int i = 0; i < dtp.Count; i++)
                {
                    dtp[i].Dispose();
                }

                ltb.Clear();
                lcb.Clear();
                dtp.Clear();

                count = 0;
                flowLayoutPanel1.Show();
                //to get value of srno
                cmdstr = String.Format("select srno from tblissuedetails where issueslipno={0}", issueslip_no_new);


                cmd = new SqlCommand(cmdstr, con);
                adpt = new SqlDataAdapter(cmd);
                dset = new DataSet();


                adpt.Fill(dset, "issuedet");
                foreach (DataRow srow in dset.Tables["issuedet"].Rows)
                {
                    count1 = count1 + 1;

                }
                //To generate Structure
                for (int j = 1; j <= count1; j++)
                {
                    //retrive_struct();
                    buttonX1.PerformClick();
                }


                //To retrive data

                cmdstr = "select * from tblissuedetails where issueslipno=" + issueslip_no_new;
                con = new SqlConnection(constr);
                cmd = new SqlCommand(cmdstr, con);
                adpt = new SqlDataAdapter(cmd);
                dset = new DataSet();

                adpt.Fill(dset, "issueret");
                int ind = 0, aus = 0, tp = 0;
                foreach (DataRow rrow in dset.Tables["issueret"].Rows)
                {
                    ltb[ind].Text = rrow["srno"].ToString();
                    lcb[aus].SelectedValue = rrow["category"].ToString();
                    lcb[aus + 1].Text = rrow["itemname"].ToString();
                    //rrow["itemdesc"].ToString();
                    ltb[ind + 1].Text = rrow["counit"].ToString();
                    ltb[ind + 2].Text = rrow["qtyissued"].ToString();
                    lcb[aus + 2].SelectedValue = rrow["maincostcentre"].ToString();
                    lcb[aus + 3].SelectedValue = rrow["subcostcentre"].ToString();
                    lcb[aus + 4].SelectedValue = rrow["costcenter"].ToString();
                    dtp[tp].Text = rrow["dois"].ToString();
                    ltb[ind + 3].Text = rrow["remarks"].ToString();

                    ind = ind + 4;
                    aus = aus + 5;
                    tp = tp + 1;

                }

                cmdstr = "select * from tblissueslip where issuslipno=" + issueslip_no_new;
                con = new SqlConnection(constr);
                cmd = new SqlCommand(cmdstr, con);
                adpt = new SqlDataAdapter(cmd);
                dset = new DataSet();

                adpt.Fill(dset, "issuerup");
                foreach (DataRow rrow in dset.Tables["issuerup"].Rows)
                {

                    textBox40.Text = rrow["issuslipno"].ToString();
                    textBox2.Text = rrow["refno"].ToString();
                    dateTimePicker3.Text = rrow["date"].ToString();
                    comboBox4.SelectedValue = rrow["issuecategory"].ToString();
                }

                con.Close();
            }
            catch
            {
                MessageBox.Show("Somthing  went  Wrong  While  Retriving  Data"); 
            }
            }
           
        

    

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                cmdstr = "select * from tblissuedetails";
                cmd = new SqlCommand(cmdstr, con);
                adpt = new SqlDataAdapter(cmd);
                dset = new DataSet();
                adpt.Fill(dset, "detail");
                int indx = 0, indx2 = 0, indx3 = 0;
                for (int r = 0; r < count; r++)
                {

                    DataRow row = dset.Tables[0].NewRow();

                    //row["id"] = Convert.ToInt32(textBox1.Text);
                    row["issueslipno"] = Convert.ToInt32(textBox40.Text);
                    row["srno"] = Convert.ToInt32(ltb[indx].Text);

                    //label1.Text = " " + indx2;
                    row["category"] = Convert.ToInt32(lcb[indx2].SelectedValue);
                    row["itemname"] = Convert.ToString(lcb[indx2 + 1].SelectedValue);
                    //row["itemdesc"] = Convert.ToString(lcb[indx2].SelectedValue);
                    row["counit"] = Convert.ToInt32(ltb[indx + 1].Text);

                    row["qtyissued"] = Convert.ToDouble(ltb[indx + 2].Text);
                    row["maincostcentre"] = Convert.ToInt32(lcb[indx2 + 2].SelectedValue);
                    row["subcostcentre"] = Convert.ToInt32(lcb[indx2 + 3].SelectedValue);
                    row["costcenter"] = Convert.ToInt32(lcb[indx2 + 4].SelectedValue);



                    row["remarks"] = ltb[indx + 3].Text;
                    row["dois"] = Convert.ToDateTime(dtp[indx3].Text);


                    indx = indx + 4;
                    indx2 = indx2 + 5;
                    indx3 = indx3 + 1;
                    dset.Tables[0].Rows.Add(row);
                    SqlCommandBuilder cmdb = new SqlCommandBuilder(adpt);
                    adpt.Update(dset, "detail");



                }

                cmdstr = "select * from tblissueslip";
                cmd = new SqlCommand(cmdstr, con);
                adpt = new SqlDataAdapter(cmd);
                dset = new DataSet();
                adpt.Fill(dset, "detail1");




                DataRow arow = dset.Tables[0].NewRow();


                arow["issuslipno"] = Convert.ToInt32(textBox40.Text);
                arow["refno"] = textBox2.Text;
                arow["date"] = Convert.ToDateTime(dateTimePicker3.Text);
                arow["issuecategory"] = comboBox4.SelectedValue;


                dset.Tables[0].Rows.Add(arow);
                SqlCommandBuilder cmdb1 = new SqlCommandBuilder(adpt);
                adpt.Update(dset, "detail1");
                MessageBox.Show("inserted successfully");
            }
            catch
            {
                MessageBox.Show("You r tring to update existing issue.kindly press update button");
            }
               
        }

        private void textBox18_Click(object sender, EventArgs e)
        {
            textBox18.Text = " ";
        }

        private void textBox18_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
               
                flag = 1;
                buttonX4.PerformClick();
            }
        }

        private void add_row(object sender, KeyEventArgs e)
        {
            if (((TextBox)sender).Name == "" + (ltb.Count - 1))
            {

                //MessageBox.Show("last  textbox  nu  naam che  " + ((TextBox)sender).Name);
                if (e.KeyCode == Keys.Enter)
                {
                    buttonX1.PerformClick();
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                cmdstr = "select * from tblissuedetails where issueslipno=" + textBox40.Text;
                cmd = new SqlCommand(cmdstr, con);
                adpt = new SqlDataAdapter(cmd);
                dset = new DataSet();
                adpt.Fill(dset, "detail");
                int indx = 0, indx2 = 0, indx3 = 0;

                foreach (DataRow row in dset.Tables["detail"].Rows)
                {



                    //row["id"] = Convert.ToInt32(textBox1.Text);
                    row["issueslipno"] = Convert.ToInt32(textBox40.Text);
                    row["srno"] = Convert.ToInt32(ltb[indx].Text);

                    //label1.Text = " " + indx2;
                    row["category"] = Convert.ToInt32(lcb[indx2].SelectedValue);
                    row["itemname"] = Convert.ToString(lcb[indx2 + 1].SelectedValue);
                    //row["itemdesc"] = lcb[indx2].SelectedText;
                    row["counit"] = Convert.ToInt32(ltb[indx + 1].Text);

                    row["qtyissued"] = Convert.ToDouble(ltb[indx + 2].Text);
                    row["maincostcentre"] = Convert.ToInt32(lcb[indx2 + 2].SelectedValue);
                    row["subcostcentername"] = Convert.ToInt32(lcb[indx2 + 3].SelectedValue);
                    row["costcentername"] = Convert.ToInt32(lcb[indx2 + 4].SelectedValue);



                    row["remarks"] = ltb[indx + 3].Text;
                    row["dois"] = Convert.ToDateTime(dtp[indx3].Text);


                    indx = indx + 4;
                    indx2 = indx2 + 5;
                    indx3 = indx3 + 1;

                    SqlCommandBuilder cmdb = new SqlCommandBuilder(adpt);
                    adpt.Update(dset, "detail");



                }

                cmdstr = "select * from tblissueslip where issuslipno=" + textBox40.Text;
                cmd = new SqlCommand(cmdstr, con);
                adpt = new SqlDataAdapter(cmd);
                dset = new DataSet();
                adpt.Fill(dset, "detail1");




                foreach (DataRow arow in dset.Tables[0].Rows)
                {


                    arow["issuslipno"] = Convert.ToInt32(textBox40.Text);
                    arow["refno"] = textBox2.Text;
                    arow["date"] = Convert.ToDateTime(dateTimePicker3.Text);
                    arow["issuecategory"] = comboBox4.SelectedValue;



                    SqlCommandBuilder cmdb1 = new SqlCommandBuilder(adpt);
                    adpt.Update(dset, "detail1");
                }
            }
            catch
            {
                MessageBox.Show("input String is not in correct format  or  some field is Empty");

            }
                MessageBox.Show("update successfully");

            
           

            }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            cmnall.cmncls.check_off("issue");
        }
        



}
}
