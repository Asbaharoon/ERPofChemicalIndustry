using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;
using cmnall2;
using cmnall;


namespace Indent
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
        int n = 0,temp=0,flag=0,m=0;
        int count=0;
        //reff;
        
        public class OurT : TextBox {

            public OurT(int i)
            {
                Font f = new Font("Segoe ui Symbol", 12, FontStyle.Regular);
                this.Font = f;
                this.Name="" + i;
            }
        
        }
       
        public class OurC : ComboBox {
        public OurC(int i)
            {
                Font f = new Font("Segoe ui Symbol", 12, FontStyle.Regular);
                this.Font = f;
                this.DropDownWidth = 450;
                this.DropDownHeight = 106;
                this.Name = ""+i;
            }
        
        }
        
        List<TextBox> ltb = new List<TextBox>();
        List<ComboBox> lcb = new List<ComboBox>();
        Size sz = new Size(40, 29);
        int index = 0;

        public Form1()
        {
            InitializeComponent();
        }
        public void bind()
        {
            constr = "Data Source=.\\SQLEXPRESS;Initial Catalog=chloritech_db;Integrated Security=True";
            cmnall2.com.get_constr111();
        }

        private void Form1_Load(object sender, EventArgs e)
        {


            bind();
            cmdstr="select id,itemname from tblitemmaster";
            con = new SqlConnection(constr);
            cmd = new SqlCommand(cmdstr, con);
            adpt = new SqlDataAdapter(cmd);
            bldr = new SqlCommandBuilder(adpt);
            dset = new DataSet();
            con.Open();

            adpt.Fill(dset, "ind");

            bndsrc = new BindingSource();
            bndsrc.DataSource = dset;
            bndsrc.DataMember = "ind";
            comboBox5.DataSource = bndsrc;
            comboBox5.ValueMember = "id";
            comboBox5.DisplayMember = "itemname";
            
            
            
            temp = max_no_of_indent();
            //MessageBox.Show("" + temp);
            textBox16.Text = String.Format("{0} of {0}", (temp + 1));

            textBox1.Text = "" + (temp + 1);            
            buttonX1.PerformClick();

        }

        public int max_no_of_indent()
        {
            int max = 0;
            cmdstr = "select indentno from tblindentdet";
            cmd = new SqlCommand(cmdstr, con);
            adpt = new SqlDataAdapter(cmd);

            adpt.Fill(dset, "indentn");


            foreach (DataRow srow in dset.Tables["indentn"].Rows)
            {
                int val = Convert.ToInt32(srow["indentno"].ToString());
                if (val >= max && val != 2000)
                {
                    max = val;
                }
            }


            return max;

         
        }

        private void buttonX1_Click(object sender, EventArgs e)
        {
            ltb.Add(new OurT(n++));
            lcb.Add(new OurC(m++));
            ltb.Add(new OurT(n++));
            lcb.Add(new OurC(m++));
            ltb.Add(new OurT(n++));
            ltb.Add(new OurT(n++));
            ltb.Add(new OurT(n++));

            cmdstr = "select id,itemname,units from tblitemmaster";

            con = new SqlConnection(constr);
            cmd = new SqlCommand(cmdstr, con);
            adpt = new SqlDataAdapter(cmd);
            dset = new DataSet();
            con.Open();

            adpt.Fill(dset, "db");



            BindingSource bndsrc = new BindingSource();
            bndsrc.DataSource = dset;
            bndsrc.DataMember = "db";
            lcb[count*2].DataSource = bndsrc;
            lcb[count * 2].ValueMember = "itemname";
            lcb[count*2].DisplayMember = "itemname";


            cmdstr = "select id,abbrunits from tblunitsmaster";
            con = new SqlConnection(constr);
            cmd = new SqlCommand(cmdstr, con);
            adpt = new SqlDataAdapter(cmd);
            bldr = new SqlCommandBuilder(adpt);
            dset = new DataSet();
            con.Open();

            adpt.Fill(dset, "unit");

            bndsrc = new BindingSource();
            bndsrc.DataSource = dset;
            bndsrc.DataMember = "unit";

            lcb[(count * 2) + 1].DataSource = bndsrc;
            lcb[(count * 2) + 1].ValueMember = "abbrunits";
            lcb[(count * 2) + 1].DisplayMember = "abbrunits";
            con.Close();





            sz.Width = 49;
            ltb[count*5].Size = sz;
            ltb[count * 5].ReadOnly = true;
            ltb[count * 5].Text = ""+(count + 1);
            flowLayoutPanel1.Controls.Add(ltb[count*5]);              
            sz.Width = 226;
            
            lcb[count*2].Size = sz;
            flowLayoutPanel1.Controls.Add(lcb[count*2]);           
            lcb[count*2].SelectedIndexChanged += new System.EventHandler(this.new_logic);

            sz.Width = 206;
            ltb[(count * 5) + 1].Size = sz;
            flowLayoutPanel1.Controls.Add(ltb[count * 5 + 1]);


            sz.Width = 135;
            lcb[(count*2)+1].Size = sz;
            flowLayoutPanel1.Controls.Add(lcb[count * 2 + 1]);
           



            sz.Width = 100;
            ltb[(count * 5) + 2].Size = sz;
            flowLayoutPanel1.Controls.Add(ltb[count * 5 + 2]);
         

            sz.Width = 100;
            ltb[(count * 5) + 3].Size = sz;
            flowLayoutPanel1.Controls.Add(ltb[count * 5 + 3]);
          

            sz.Width = 221;
            ltb[(count * 5) + 4].Size = sz;
            flowLayoutPanel1.Controls.Add(ltb[count * 5 + 4]);



            m = m + 1;
            n = n + 4;
            count++;


        }

       

       

        

        public void new_logic(Object sender, EventArgs e)
        {
            ComboBox cb1 = (ComboBox)sender;
         
            for (int i = 0; i < lcb.Count; i++)
            {
                if (lcb[i].Name == cb1.Name)
                {
                    index = i;
                    break;
                }

            }

            try
            {                
                String mystring = cb1.SelectedValue.ToString();                 
                cmdstr = "DECLARE @TEMP INT SET @TEMP=(select DISTINCT units from tblitemmaster where itemname='"+mystring+"') select abbrunits from tblunitsmaster where id= @TEMP";                
                con = new SqlConnection(constr);
                cmd = new SqlCommand(cmdstr, con);
                adpt = new SqlDataAdapter(cmd);
                dset = new DataSet();
                con.Open();
                adpt.Fill(dset, "indent");                
               
                foreach (DataRow abbrow in dset.Tables["indent"].Rows)
                {
                    lcb[index + 1].SelectedValue = abbrow["abbrunits"];
                }
                index = index + 1; 
                con.Close();
            }
            catch (Exception s)
            {
                Console.WriteLine(" " + s);
            }
        }
           
        public void ins_up()
        {
            cmdstr = "insert into tblindent values(@indentno,@indentdate,@indentor)";
            cmd = new SqlCommand(cmdstr, con);
            dset = new DataSet();
            //cmd.Parameters.Add("@id", SqlDbType.Int);
            
            cmd.Parameters.Add("@indentno", SqlDbType.Int);
            cmd.Parameters.Add("@indentdate", SqlDbType.DateTime);            
            cmd.Parameters.Add("@indentor", SqlDbType.NVarChar);

            //cmd.Parameters["@id"].Value = Convert.ToInt32(textBox30.Text);
            cmd.Parameters["@indentno"].Value = Convert.ToInt32(textBox1.Text);
            cmd.Parameters["@indentdate"].Value = dateTimePicker4.Value;            
            cmd.Parameters["@indentor"].Value = textBox2.Text;
            adpt = new SqlDataAdapter(cmd);
            cmd.CommandText = cmdstr;


            adpt.InsertCommand = cmd;
            adpt.Fill(dset, "db");

        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                ins_up();
                ins_bottom();
                MessageBox.Show("Row Inserted"); 
            }
            catch 
            {
                MessageBox.Show("You are trying to Update Existing Indent . Kindly Press Update Button" ); 
            }
            int aftermax = max_no_of_indent()+1;
            textBox1.Text = ""+aftermax;
            textBox2.Text = "";
            textBox16.Text = String.Format("{0} of {0}", (aftermax));
        }
        public void ins_bottom()        
        {
            cmdstr = "select * from tblindentdet";
            cmd = new SqlCommand(cmdstr, con);
            adpt = new SqlDataAdapter(cmd);
            dset = new DataSet();
            adpt.Fill(dset, "detail");
            int indx = 0, indx2 = 0;
            for (int r = 0; r < count; r++)
            {

                DataRow row = dset.Tables[0].NewRow();

                row["srno"] = Convert.ToInt32(ltb[indx].Text);
                row["indentno"] = Convert.ToInt32(textBox1.Text);
                
                //Convert.ToInt32(ltb[indx].Text);
                //label1.Text = "" + indx2;
                row["item"] = Convert.ToString(lcb[indx2].SelectedValue);
                row["Itemdesription"] = ltb[indx + 1].Text;
                row["Unit"] = Convert.ToString(lcb[indx2+1].SelectedValue);
                row["indentqty"] = Convert.ToDouble(ltb[indx+2].Text);
                row["qtyinstore"] = Convert.ToDouble(ltb[indx+3].Text);
                row["Purpose"] = ltb[indx+4].Text;


                indx = indx + 5;
                indx2 = indx2 + 2;
                dset.Tables[0].Rows.Add(row);
                SqlCommandBuilder cmdb = new SqlCommandBuilder(adpt);
                adpt.Update(dset, "detail");

            }
        }

       
        private void button3_Click(object sender, EventArgs e)
        {
            
            int indent_no = Convert.ToInt32(textBox1.Text), indent_no_new = 0, count1 = 0, temp2 = (temp + 1);


            if (((Button)sender).Name.ToString() == "button3")
            {
                if (flag == 1)
                {
                    if (textBox1.Text == "1")
                    {
                        button3.Enabled = false;
                    }
                    indent_no_new = Convert.ToInt32(textBox16.Text);
                    flag = 0;
                }
                else
                    indent_no_new = (indent_no - 1);          
                 
                
            }
            else if (((Button)sender).Name.ToString() == "button4")
            {
                indent_no_new = 1;
            }
            else if (((Button)sender).Name.ToString() == "button5")
            {
                indent_no_new = (indent_no + 1);
//                if(indent_no_new > temp+2)

            }
            else if (((Button)sender).Name.ToString() == "button6")
            {
                indent_no_new = temp; 
            }
            textBox1.Text = "" + indent_no_new;
            textBox16.Text = String.Format("{0} of {1}", indent_no_new, temp2);
            flowLayoutPanel1.Visible = false;
            for (int i = 0; i < ltb.Count; i++)
            {
                flowLayoutPanel1.Controls.Remove(ltb[i]);
                ltb[i].Dispose();
                m--;
            }
            for (int i = 0; i < lcb.Count; i++)
            {
                flowLayoutPanel1.Controls.Remove(lcb[i]);
                lcb[i].Dispose();
            }
            flowLayoutPanel1.Visible = true;
            ltb.Clear();
            lcb.Clear();
            count = 0;
            //idx = 0;
            //to get value of srno
            cmdstr = String.Format("select srno from tblindentdet where indentno={0}", indent_no_new);

            con = new SqlConnection(constr);
            cmd = new SqlCommand(cmdstr, con);
            adpt = new SqlDataAdapter(cmd);
            dset = new DataSet();
            con.Open();

            adpt.Fill(dset, "indentdet");
            foreach (DataRow srow in dset.Tables["indentdet"].Rows)
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

            cmdstr = "select srno,item,Unit,Itemdesription,indentqty,qtyinstore,purpose from tblindentdet where indentno=" + indent_no_new;
            con = new SqlConnection(constr);
            cmd = new SqlCommand(cmdstr, con);
            adpt = new SqlDataAdapter(cmd);
            dset = new DataSet();
            con.Open();
            adpt.Fill(dset, "indentret");
            int ind = 0, aus = 0;
            foreach (DataRow rrow in dset.Tables["indentret"].Rows)
            {
                
                ltb[ind].Text = rrow["srno"].ToString();                
                lcb[aus].SelectedValue = rrow["item"].ToString(); 
                //lcb[aus].Text = rrow["item"].ToString();
                if (rrow["Itemdesription"].ToString() == "")
                {
                    ltb[ind + 1].Text = "NULL";
                }
                else
                    ltb[ind + 1].Text = rrow["Itemdesription"].ToString();
                lcb[aus + 1].SelectedValue = rrow["Unit"].ToString();
                //lcb[aus + 1].Text = rrow["Unit"].ToString();
                ltb[ind + 2].Text = rrow["indentqty"].ToString();
                ltb[ind + 3].Text = rrow["qtyinstore"].ToString();
                ltb[ind + 4].Text = rrow["purpose"].ToString();
                
                ind = ind + 5;
                aus = aus + 2;
            }
            cmdstr = "select * from tblindent where indentno=" + indent_no_new;
            con = new SqlConnection(constr);
            cmd = new SqlCommand(cmdstr, con);
            adpt = new SqlDataAdapter(cmd);
            dset = new DataSet();
            con.Open();
            adpt.Fill(dset, "indentup");
            foreach (DataRow rrow in dset.Tables["indentup"].Rows)
            {
                textBox2.Text = rrow["Indentor"].ToString();
                dateTimePicker4.Value = (DateTime)rrow["indentdate"];
            }
        }

        private void textBox16_Click(object sender, EventArgs e)
        {
            textBox16.Text = ""; 
        }

        private void textBox16_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                button3.Enabled = true;
                flag = 1;
                try
                {
                    button3.PerformClick();
                }
                catch 
                {
 
                }
            }
        }

        private void buttonX2_Click(object sender, EventArgs e)
        {
            try
            {
                int tmp = ltb.Count, tmp2 = lcb.Count;
                for (int i = 0; i < 5; i++)
                {
                    ltb[tmp - 1 - i].Dispose();
                    ltb.RemoveAt(tmp - 1 - i);
                }
                for (int i = 0; i < 2; i++)
                {
                    lcb[tmp2 - 1 - i].Dispose();
                    lcb.RemoveAt(tmp2 - 1 - i);
                }
                count--;
            }
            catch 
            {
                MessageBox.Show("All Rows are Deleted");
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            cmdstr = "select * from tblindentdet where indentno=" + Convert.ToInt32(textBox1.Text);
            cmd = new SqlCommand(cmdstr, con);
            adpt = new SqlDataAdapter(cmd);
            dset = new DataSet();
            adpt.Fill(dset, "detail");
            int indx = 0, indx2 = 0;

            try
            {
                foreach (DataRow row in dset.Tables[0].Rows)
                {
                    row["srno"] = Convert.ToInt32(ltb[indx].Text);
                    row["item"] = Convert.ToString(lcb[indx2].SelectedValue);
                    row["Itemdesription"] = ltb[indx + 1].Text;
                    row["Unit"] = Convert.ToString(lcb[indx2 + 1].SelectedValue);
                    row["indentqty"] = Convert.ToDouble(ltb[indx + 2].Text);
                    row["qtyinstore"] = Convert.ToDouble(ltb[indx + 3].Text);
                    row["Purpose"] = ltb[indx + 4].Text;


                    indx = indx + 5;
                    indx2 = indx2 + 2;
                    SqlCommandBuilder cmdb = new SqlCommandBuilder(adpt);
                    adpt.Update(dset, "detail");
                }

            }
            catch(Exception ex)
            {
                MessageBox.Show("Input String Not in Correct form or some Field is Empty");
            }

            cmdstr = "select * from tblindent where indentno=" + Convert.ToInt32(textBox1.Text); MessageBox.Show(cmdstr);
            cmd = new SqlCommand(cmdstr, con);
            adpt = new SqlDataAdapter(cmd);
            dset = new DataSet();
            adpt.Fill(dset, "detail1");
            try
            {
                foreach (DataRow row in dset.Tables["detail1"].Rows)
                {
                    row["indentno"] = textBox1.Text;
                    row["indentdate"] = dateTimePicker4.Value;
                    row["Indentor"] = textBox2.Text;
                    SqlCommandBuilder cmdb = new SqlCommandBuilder(adpt);
                    adpt.Update(dset, "detail1");
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show("Input String Not in Correct form or some Field is Empty");
            }
            MessageBox.Show("Update Successfull");
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            int check_no = Convert.ToInt32(textBox1.Text);
            if (check_no == 1)
            {
                button4.Enabled = false;
                button3.Enabled = false;
                button5.Enabled = true;
                button6.Enabled = true;
            }
            else if (check_no > 1 && check_no < (temp + 1))
            {
                button4.Enabled = true;
                button3.Enabled = true;
                button5.Enabled = true;
                button6.Enabled = true;
            }
            else if (check_no == (temp + 1))
            {
                button4.Enabled = true;
                button3.Enabled = true;
                button5.Enabled = false;
                button6.Enabled = false;
            }
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            cmnall.cmncls.check_off("indent");
        }

        


       
        }


    }

