using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Threading;
namespace grn
{
    public partial class grnform : Form
    {
        SqlConnection con;
        SqlCommand cmd;
        SqlDataAdapter adpt;
        DataSet dset, dset2;
        string constr, cmdstr;
        BindingSource bndsrc;
       /* number  of  textboxgerated  dynamically */
        int trow = 11;
        string grnd_amt;
        Size sz = new Size(40, 29);
        int reff;
        int dex = 0;
        int idx = 0;
        int temp = 0;
        int n = 0,m=0,flag=0;
        int count = 0, j;
        int t = 0;

        List<TextBox> ltb = new List<TextBox>();
        List<ComboBox> lcb = new List<ComboBox>();
        List<TextBox> tex = new List<TextBox>();
        
        
        public grnform()
        {
            InitializeComponent();
        }



        private void buttonX1_Click(object sender, EventArgs e)
        {

            try
            {

                #region anish_smit code

                j = count * 11;

                ltb.Add(new OurT(m++));
                lcb.Add(new OurC(n++));
                lcb.Add(new OurC(n++));
                lcb.Add(new OurC(n++));

                tex.Add(new OurT(t++));
                tex.Add(new OurT(t++));
                tex.Add(new OurT(t++));
                tex.Add(new OurT(t++));


                for (int i = 0; i < 10; i++)
                {
                    ltb.Add(new OurT(m++));

                }

                sz.Width = 30;

                ltb[count * trow].Size = sz;
                ltb[count * trow].Text = "" + (count + 1);

                sz.Width = 106;

                lcb[count * 3].Size = sz;

                sz.Width = 56;

                lcb[(count * 3) + 1].Size = sz;

                sz.Width = 150;

                lcb[(count * 3) + 2].Size = sz;

                comb_dynamic_bind();

                flowLayoutPanel1.Controls.Add(ltb[count * trow]);


                flowLayoutPanel1.Controls.Add(lcb[count * 3]);



                flowLayoutPanel1.Controls.Add(lcb[count * 3 + 1]);

                flowLayoutPanel1.Controls.Add(lcb[count * 3 + 2]);

                sz.Width = 159;

                for (int i = 0; i < 4; i++)
                {
                    tex[count + dex + i].Size = sz;
                    tex[count + i + dex].ReadOnly = false;

                    flowLayoutPanel2.Controls.Add(tex[count + dex + i]);

                }


                event_handlers();

                dynamic_readonly_texbox();

                count++;
                dex = dex + 3;


            }
            catch (Exception ex)
            {
                MessageBox.Show("ERROR  WHILE  INSERTING  ROW , CHECK  YOUR  INPUT  AND  TRY  AGAIN  " + ex.ToString());
                return;
            }
            }
        
    
            #endregion
        
        private void Form1_Load(object sender, EventArgs e)
        {
            
            
            full_screen_mode();

            initial_data_binding();

            temp = max_no_of_grn();

            buttonX1.PerformClick();


            textBox2.Text = String.Format("{0} of {0}", (temp + 1));

            textBox35.Text = "" + (temp + 1);
         }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

            try
            {
                int a;
                String mystring = comboBox1.SelectedValue.ToString();
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
                comboBox2.DataSource = bndsrc;
                comboBox2.ValueMember = "id";
                comboBox2.DisplayMember = "id";
                //BindingSource bndsrc1 = new BindingSource();
                //bndsrc.DataSource = dset;
                //bndsrc.DataMember = "itemx";
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

        public void main_logic(object sender, EventArgs e)
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

            try
            {

                int a;
                String mystring = cb1.SelectedValue.ToString();
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
                lcb[index + 1].ValueMember = "id";
                lcb[index + 1].DisplayMember = "id";
                //BindingSource bndsrc1 = new BindingSource();
                //bndsrc.DataSource = dset;
                //bndsrc.DataMember = "itemx";
                lcb[index + 2].DataSource = bndsrc;

                lcb[index + 2].ValueMember = "id";
                lcb[index + 2].DisplayMember = "itemname";

                con.Close();
            }
            catch (Exception s)
            {
                MessageBox.Show("Error in  binding  item & catgory  data ");
            }
        }

        private void buttonX2_Click(object sender, EventArgs e)
        {
            if (flowLayoutPanel2.Visible)
            {
                buttonX2.Symbol = ">";
                flowLayoutPanel2.Visible = false;
                textBox41.Hide();
                textBox42.Hide();
                textBox43.Hide();
                textBox44.Hide();
            }
            else
            {
                buttonX2.Symbol = "<";
                flowLayoutPanel2.Visible = true;
                textBox41.Show();
                textBox42.Show();
                textBox43.Show();
                textBox44.Show();
                HorizontalScroll.Value = 1200;
            }

        }

        private void textBox5_Enter(object sender, EventArgs e)
        {
            int cq=0,aq=0;

            aq = Convert.ToInt32(textBox4.Text);
            cq = Convert.ToInt32(textBox3.Text);
            textBox5.Text = "" + (cq - aq);

        }

        public void ex_stor_logic(object sender, EventArgs e)
        {
            

            TextBox tb1 = (TextBox)sender;
            label1.Text = tb1.Name;

            
            
                for (int i = 0; i < ltb.Count; i++)
                {
                    if (ltb[i].Name == tb1.Name)
                    {
                        try
                        {
                            //index = i;
                            int c = Convert.ToInt32(ltb[i - 2].Text);
                            int a = Convert.ToInt32(ltb[i - 1].Text);
                            ltb[i].Text = "" + (c - a);
                        }
                        catch (Exception ex)
                        {
                           
                        }
                        

                        break;
                    }


                }
            
            
        }

        public void rej_logic(object sender, EventArgs e)
        {
            TextBox tb1 = (TextBox)sender;
            label1.Text = tb1.Name;

            try
            {
                for (int i = 0; i < ltb.Count; i++)
                {
                    if (ltb[i].Name == tb1.Name)
                    {
                        //index = i;
                        int aqty = Convert.ToInt32(ltb[i - 3].Text);
                        int ac = Convert.ToInt32(ltb[i - 1].Text);
                        ltb[i].Text = "" + (aqty - ac);

                        break;
                    }


                }
            }
            catch (Exception ex)
            {
               
            }
        }

        public void amt_logic(object sender, EventArgs e)
        {

            TextBox tb1 = (TextBox)sender;
            label1.Text = tb1.Name;

            try
            {
                for (int i = 0; i < ltb.Count; i++)
                {
                    if (ltb[i].Name == tb1.Name)
                    {
                        //index = i;
                        int acq = Convert.ToInt32(ltb[i - 3].Text);
                        double br = Convert.ToDouble(ltb[i - 1].Text);
                        ltb[i].Text = "" + (acq * br);

                        break;
                    }


                }
            }
            catch (Exception ex)
            {
               
 
            }
        }

        public void set_grnd_amnt(string str)
        {
            grnd_amt = str;

        }

        private void button1_Click(object sender, EventArgs e)
        {          
            black b = new black();
            b.Show();
            help hp = new help(this,b);
            hp.ShowDialog();  
        }

        public void calculations()
        {
            try
            {
                double amt = 0, gamt = Convert.ToDouble(grnd_amt), ratio;


                //if  grand  amount  is  zero 
                if (gamt == 0)
                {
                    for (int i = 8; i < ltb.Count; i = i + 11)
                    {
                        ltb[i].Text = "" + 0;
                    }


                    for (int i = 9; i < ltb.Count; i = i + 11)
                    {

                        ltb[i].Text = "" + 0;
                    }
                    for (int i = 10; i < ltb.Count; i = i + 11)
                    {

                        ltb[i].Text = "" + 0;
                    }
                    return;
                }

                label1.Text = "" + gamt;
                for (int i = 7; i < ltb.Count; i = i + 11)
                {
                    amt = amt + Convert.ToDouble(ltb[i].Text);

                }

                ratio = (gamt - amt) / amt;
                for (int i = 8; i < ltb.Count; i = i + 11)
                {
                    ltb[i].Text = "" + ratio;
                }


                for (int i = 9; i < ltb.Count; i = i + 11)
                {
                    Double am = Convert.ToDouble(ltb[i - 2].Text);
                    ltb[i].Text = "" + (ratio * am);
                }
                for (int i = 10; i < ltb.Count; i = i + 11)
                {
                    Double aam = Convert.ToDouble(ltb[i - 1].Text), amnt = Convert.ToDouble(ltb[i - 3].Text);
                    ltb[i].Text = "" + (aam + amnt);
                }

                label8.Text = grnd_amt + "";
            }
            catch (Exception ex)
            {
                MessageBox.Show("PLEASE  INSERT  CORRECT GRAND  AMOUNT  VALUE  ");
                return;
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            try
            {

                int grn_no = Convert.ToInt32(textBox35.Text), grn_no_new = 0, count1 = 0, temp2 = (temp + 1);



                if (((Button)sender).Name.ToString() == "button3")
                {
                    if (flag == 1)
                    {
                        grn_no_new = Convert.ToInt32(textBox2.Text);
                        flag = 0;
                    }
                    else
                        grn_no_new = (grn_no - 1);
                }
                else if (((Button)sender).Name.ToString() == "button2")
                {
                    grn_no_new = 1;
                }
                else if (((Button)sender).Name.ToString() == "button4")
                {
                    grn_no_new = (grn_no + 1);
                }
                else if (((Button)sender).Name.ToString() == "button5")
                {
                    grn_no_new = temp;
                }
                textBox35.Text = "" + grn_no_new;
                textBox2.Text = String.Format("{0} of {1}", grn_no_new, temp2);
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

                for (int i = 0; i < tex.Count; i++)
                {
                    flowLayoutPanel2.Controls.Remove(tex[i]);
                    tex[i].Dispose();
                }

                flowLayoutPanel1.Visible = true;
                ltb.Clear();
                lcb.Clear();
                tex.Clear();
                count = 0;
                idx = 0;
                dex = 0;
                //to get value of srno
                cmdstr = String.Format("select srno from tblgrndetails where grnno={0}", grn_no_new);

                con = new SqlConnection(constr);
                cmd = new SqlCommand(cmdstr, con);
                adpt = new SqlDataAdapter(cmd);
                dset = new DataSet();
                con.Open();

                adpt.Fill(dset, "grndet");
                foreach (DataRow srow in dset.Tables["grndet"].Rows)
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

                //cmdstr = "select id,grnno,srno,category,itemdescription,challanqty,actualqty,accepted,rejected,rate,amout,Ratio,addon from tblgrndetails where grnno="+grn_no_new;
                cmdstr = "select * from tblgrndetails where grnno=" + grn_no_new;
                con = new SqlConnection(constr);
                cmd = new SqlCommand(cmdstr, con);
                adpt = new SqlDataAdapter(cmd);
                dset = new DataSet();
                con.Open();
                adpt.Fill(dset, "grnret");
                int ind = 0, aus = 0, texv = 0;
                foreach (DataRow rrow in dset.Tables["grnret"].Rows)
                {
                    ltb[ind].Text = rrow["srno"].ToString();
                    lcb[aus].SelectedValue = rrow["category"].ToString();
                    lcb[aus + 1].SelectedValue = rrow["itemdescription"].ToString();
                    ltb[ind + 1].Text = rrow["challanqty"].ToString();
                    ltb[ind + 2].Text = rrow["actualqty"].ToString();
                    ltb[ind + 3].Text = rrow["excs"].ToString();
                    ltb[ind + 4].Text = rrow["accepted"].ToString();
                    ltb[ind + 5].Text = rrow["rejected"].ToString();
                    ltb[ind + 6].Text = rrow["rate"].ToString();
                    ltb[ind + 7].Text = rrow["amout"].ToString();
                    ltb[ind + 8].Text = rrow["Ratio"].ToString();
                    ltb[ind + 9].Text = rrow["addon"].ToString();
                    ltb[ind + 10].Text = rrow["total"].ToString();

                    tex[texv].Text = rrow["modvat"].ToString();
                    tex[texv + 1].Text = rrow["vat"].ToString();
                    tex[texv + 2].Text = rrow["dnote"].ToString();
                    tex[texv + 3].Text = rrow["remarks"].ToString();

                    ind = ind + 11;
                    aus = aus + 3;
                    texv = texv + 4;
                }

                cmdstr = "select * from tblgrn where grnno=" + grn_no_new;
                con = new SqlConnection(constr);
                cmd = new SqlCommand(cmdstr, con);
                adpt = new SqlDataAdapter(cmd);
                dset = new DataSet();
                con.Open();
                adpt.Fill(dset, "gr");
                
                    foreach (DataRow row in dset.Tables["gr"].Rows)
                    {
                        textBox35.Text = row["grnno"].ToString();
                        dateTimePicker5.Value = Convert.ToDateTime(row["grndate"]);
                        comboBox4.SelectedValue = Convert.ToInt32(row["grntype"].ToString());
                        textBox34.Text = row["transporter"].ToString();
                        textBox39.Text = row["vehicalnumber"].ToString();
                        textBox33.Text = row["rr/lrno"].ToString();
                        dateTimePicker6.Value = Convert.ToDateTime(row["rdate"]);
                        textBox32.Text = row["challanno"].ToString();
                        dateTimePicker7.Value = Convert.ToDateTime(row["cdate"]);
                        textBox31.Text = row["invoiceno"].ToString();
                        dateTimePicker8.Value = Convert.ToDateTime(row["idate"]);
                        textBox40.Text = row["pjvno"].ToString();
                        dateTimePicker1.Value = Convert.ToDateTime(row["pjvdate"]);

                        dateTimePicker4.Value = Convert.ToDateTime(row["dateofrecpt"]);
                        comboBox3.SelectedValue = Convert.ToInt32(row["suppliername"]);
                        textBox37.Text = row["indentno"].ToString();
                        dateTimePicker2.Value = Convert.ToDateTime(row["indate"]);
                        textBox36.Text = row["pono"].ToString();
                        dateTimePicker3.Value = Convert.ToDateTime(row["date"]);
                        textBox38.Text = row["indentor"].ToString();


                    }




            }
            catch
            {
                MessageBox.Show("INPUT  NUMBER  WAS  NOT IN CORRECT  FORMAT ... TRY  AGAIN :)");
            }

        }

        public float total_amt()
        {
            //int idx = 11;
            //for(int )
            return 0;

        }

        private void textBox2_Click(object sender, EventArgs e)
        {
            textBox2.Text = "";            
        }       

        private void textBox2_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
             //   MessageBox.Show(textBox2.Text);
                flag = 1;
                button3.PerformClick();
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            try
            {
                // insertion  of  under  part
                cmdstr = "select * from tblgrndetails";
                cmd = new SqlCommand(cmdstr, con);
                adpt = new SqlDataAdapter(cmd);
                dset2 = new DataSet();
                adpt.Fill(dset2, "detail");
                int indx = 0, indx2 = 0, indx3 = 0;
                for (int r = 0; r < count; r++)
                {

                    DataRow row = dset2.Tables["detail"].NewRow();


                    row["grnno"] = Convert.ToInt32(textBox35.Text);
                    row["srno"] = Convert.ToInt32(ltb[indx].Text);
                    label1.Text = "" + indx2;
                    row["category"] = Convert.ToInt32(lcb[indx2].SelectedValue);
                    row["itemdescription"] = Convert.ToInt32(lcb[indx2 + 1].SelectedValue);
                    row["challanqty"] = Convert.ToInt32(ltb[indx + 1].Text);
                    row["actualqty"] = Convert.ToInt32(ltb[indx + 2].Text);
                    row["excs"] = Convert.ToInt32(ltb[indx + 3].Text);
                    row["accepted"] = Convert.ToInt32(ltb[indx + 4].Text);
                    row["rejected"] = Convert.ToInt32(ltb[indx + 5].Text);
                    row["rate"] = Convert.ToDouble(ltb[indx + 6].Text);
                    row["amout"] = Convert.ToDouble(ltb[indx + 7].Text);
                    row["Ratio"] = Convert.ToDouble(ltb[indx + 8].Text);
                    row["addon"] = Convert.ToDouble(ltb[indx + 9].Text);
                    row["total"] = Convert.ToDouble(ltb[indx + 10].Text);

                    row["modvat"] = Convert.ToDouble(tex[indx3].Text);
                    row["vat"] = Convert.ToDouble(tex[indx3 + 1].Text);
                    row["dnote"] = Convert.ToDouble(tex[indx3 + 2].Text); ;
                    row["remarks"] = tex[indx3 + 3].Text;
                    row["recpdate"] = dateTimePicker4.Value;
                    row["grndate"] = dateTimePicker5.Value;
                    row["dor"] = dateTimePicker4.Value;

                    indx = indx + 11;
                    indx2 = indx2 + 3;
                    dset2.Tables["detail"].Rows.Add(row);
                    SqlCommandBuilder cmdb = new SqlCommandBuilder(adpt);
                    adpt.Update(dset2, "detail");

                }


                //  insertion  of  upper  part 
                cmdstr = "select * from tblgrn";
                cmd = new SqlCommand(cmdstr, con);
                adpt = new SqlDataAdapter(cmd);
                dset2 = new DataSet();
                adpt.Fill(dset2, "grn");


                {
                    DataRow row = dset2.Tables["grn"].NewRow();


                    row["grnno"] = Convert.ToInt32(textBox35.Text);
                    row["grndate"] = dateTimePicker5.Value;
                    row["grntype"] = Convert.ToInt32(comboBox4.SelectedValue);
                    row["transporter"] = textBox34.Text;
                    row["vehicalnumber"] = textBox39.Text;
                    row["rr/lrno"] = textBox33.Text;
                    row["rdate"] = dateTimePicker6.Value;
                    row["challanno"] = textBox32.Text;
                    row["cdate"] = dateTimePicker7.Value;
                    row["invoiceno"] = textBox31.Text;
                    row["idate"] = dateTimePicker8.Value;
                    row["pjvno"] = textBox40.Text;
                    row["pjvdate"] = dateTimePicker1.Value;

                    row["dateofrecpt"] = dateTimePicker4.Value;
                    row["suppliername"] = Convert.ToInt32(comboBox3.SelectedValue);
                    row["indentno"] = Convert.ToInt32(textBox37.Text);
                    row["indate"] = dateTimePicker2.Value;
                    row["pono"] = textBox36.Text;
                    row["date"] = dateTimePicker3.Value;
                    row["indentor"] = textBox38.Text;

                    dset2.Tables["grn"].Rows.Add(row);
                    SqlCommandBuilder cmdb = new SqlCommandBuilder(adpt);
                    adpt.Update(dset2, "grn");

                }


                MessageBox.Show("data inserted  sucessfully");
            }
            catch
            {
                MessageBox.Show("DATA NOT  INSERTED  .... CHECK  YOUR  VALUE  AND  TRY  AGAIN");
                return;
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

        
        private void textBox10_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                buttonX1.PerformClick();                
            }
        }

        private void button7_Click(object sender, EventArgs e)
        {
            //for (int i = 0; i < 10; i++)
            //{                
            //    ltb[ltb.Count-i].Dispose();
            //    ltb[ltb.Count - i].Clear();
            //}
        }

        private void buttonX3_Click(object sender, EventArgs e)
        {
            try
            {

                int tmp = ltb.Count, tmp2 = lcb.Count, tmp3 = tex.Count;
                for (int i = 0; i < 11; i++)
                {
                    ltb[tmp - 1 - i].Dispose();
                    ltb.RemoveAt(tmp - 1 - i);
                }
                for (int i = 0; i < 3; i++)
                {
                    lcb[tmp2 - 1 - i].Dispose();
                    lcb.RemoveAt(tmp2 - 1 - i);
                }

                for (int i = 0; i < 4; i++)
                {
                    tex[tmp3 - 1 - i].Dispose();
                    tex.RemoveAt(tmp3 - 1 - i);
                }

                count--;
                m = m - 11;
                t = t - 4;
                idx = idx - 11;
                dex = dex - 3;

            }
            catch (Exception ex)
            {
                MessageBox.Show("ALL  ROWS  ARE  DELETED ");
                return;
            }

        }

        private void button8_Click(object sender, EventArgs e)
        {
            //try
            //{

                cmdstr = "select * from tblgrndetails where grnno=" + textBox35.Text;
                cmd = new SqlCommand(cmdstr, con);
                adpt = new SqlDataAdapter(cmd);
                dset2 = new DataSet();
                adpt.Fill(dset2, "detail1");
                int indx = 0, indx2 = 0, indx3 = 0;
                foreach (DataRow row in dset2.Tables["detail1"].Rows)
                {

                    row["grnno"] = Convert.ToInt32(textBox35.Text);
                    row["srno"] = Convert.ToInt32(ltb[indx].Text);
                    label1.Text = "" + indx2;
                    row["category"] = Convert.ToInt32(lcb[indx2].SelectedValue);
                    row["itemdescription"] = Convert.ToInt32(lcb[indx2 + 1].SelectedValue);
                    row["challanqty"] = Convert.ToInt32(ltb[indx + 1].Text);
                    row["actualqty"] = Convert.ToInt32(ltb[indx + 2].Text);
                    row["excs"] = Convert.ToInt32(ltb[indx + 3].Text);
                    row["accepted"] = Convert.ToInt32(ltb[indx + 4].Text);
                    row["rejected"] = Convert.ToInt32(ltb[indx + 5].Text);
                    row["rate"] = Convert.ToDouble(ltb[indx + 6].Text);
                    row["amout"] = Convert.ToDouble(ltb[indx + 7].Text);
                    row["Ratio"] = Convert.ToDouble(ltb[indx + 8].Text);
                    row["addon"] = Convert.ToDouble(ltb[indx + 9].Text);
                    row["total"] = Convert.ToDouble(ltb[indx + 10].Text);

                    row["modvat"] = Convert.ToDouble(tex[indx3].Text);
                    row["vat"] = Convert.ToDouble(tex[indx3 + 1].Text);
                    row["dnote"] = Convert.ToDouble(tex[indx3 + 2].Text); ;
                    row["remarks"] = tex[indx3 + 3].Text;
                    row["recpdate"] = dateTimePicker4.Value;
                    row["grndate"] = dateTimePicker5.Value;
                    row["dor"] = dateTimePicker4.Value;

                    indx = indx + 11;
                    indx2 = indx2 + 3;

                    SqlCommandBuilder cmdb = new SqlCommandBuilder(adpt);
                    adpt.Update(dset2, "detail1");
                }



                cmdstr = "select * from tblgrn where grnno="+textBox35.Text;
                cmd = new SqlCommand(cmdstr, con);
                adpt = new SqlDataAdapter(cmd);
                dset2 = new DataSet();
                adpt.Fill(dset2, "grn");



                foreach (DataRow row in dset2.Tables["grn"].Rows)
                {


                    row["grnno"] = Convert.ToInt32(textBox35.Text);
                    row["grndate"] = dateTimePicker5.Value;
                    row["grntype"] = Convert.ToInt32(comboBox4.SelectedValue);
                    row["transporter"] = textBox34.Text;
                    row["vehicalnumber"] = textBox39.Text;
                    row["rr/lrno"] = textBox33.Text;
                    row["rdate"] = dateTimePicker6.Value;
                    row["challanno"] = textBox32.Text;
                    row["cdate"] = dateTimePicker7.Value;
                    row["invoiceno"] = textBox31.Text;
                    row["idate"] = dateTimePicker8.Value;
                    row["pjvno"] = textBox40.Text;
                    row["pjvdate"] = dateTimePicker1.Value;

                    row["dateofrecpt"] = dateTimePicker4.Value;
                    row["suppliername"] = Convert.ToInt32(comboBox3.SelectedValue);
                    row["indentno"] = Convert.ToInt32(textBox37.Text);
                    row["indate"] = dateTimePicker2.Value;
                    row["pono"] = textBox36.Text;
                    row["date"] = dateTimePicker3.Value;
                    row["indentor"] = textBox38.Text;


                    SqlCommandBuilder cmdb = new SqlCommandBuilder(adpt);
                    adpt.Update(dset2, "grn");

                }


                MessageBox.Show("updation  sucessful");




            //}
            //catch
            //{
            //    MessageBox.Show("DATA UPDATION  FAILED ..... CHECK  VALUES  AND  TRY  AGAIN ");
            //}


        }

        private void grnform_FormClosing(object sender, FormClosingEventArgs e)
        {
            
            cmnall.cmncls.check_off("grn");
           
        }

        private void textBox35_TextChanged(object sender, EventArgs e)
        {
            int check_no = Convert.ToInt32(textBox35.Text);
            if (check_no == 1)
            {
                button2.Enabled = false;
                button3.Enabled = false;
                button4.Enabled = true;
                button5.Enabled = true;
            }
            else if (check_no > 1 && check_no < (temp + 1))
            {
                button2.Enabled = true;
                button3.Enabled = true;
                button4.Enabled = true;
                button5.Enabled = true;
            }
            else if (check_no == (temp + 1))
            {
                button2.Enabled = true;
                button3.Enabled = true;
                button4.Enabled = false;
                button5.Enabled = false;
            }
        }

       

        
    }
}
