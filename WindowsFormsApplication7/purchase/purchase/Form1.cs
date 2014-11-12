using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;
using cmnall;

namespace purchase
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        SqlConnection con;
        SqlCommand cmd;
        SqlDataAdapter adpt;
        DataSet dset,dset1;
        String cmdstr;
        int pono,mxpo;

        //for add row
        List<TextBox> ltb = new List<TextBox>();
        List<ComboBox> lcb = new List<ComboBox>();
        List<DateTimePicker> ldp = new List<DateTimePicker>();
       
        int m = 0, n = 0, count = 0, trow = 5, crow = 2;
        Size s = new Size();


        private void Form1_Load(object sender, EventArgs e)
        {
            
            String constr = @"Data Source=.\SQLEXPRESS;Initial Catalog=chloritech_db;Integrated Security=True";
            cmdstr = "select id,suppliername from tblsuppliermaster";
            con = new SqlConnection(constr);
            cmd = new SqlCommand(cmdstr, con);
            adpt = new SqlDataAdapter(cmd);
            dset = new DataSet();
            con.Open();
            adpt.Fill(dset, "supl");

            BindingSource bndsrc1 = new BindingSource();
            bndsrc1.DataSource = dset;
            bndsrc1.DataMember = "supl";
            comboBox4.DataSource = bndsrc1;
            comboBox4.ValueMember = "suppliername";
            comboBox4.DisplayMember = "suppliername";

            cmdstr = "select indentno from tblindent";
            cmd = new SqlCommand(cmdstr, con);
            adpt = new SqlDataAdapter(cmd);
            adpt.Fill(dset, "indt");
            BindingSource bndsrc2 = new BindingSource();
            bndsrc2.DataSource = dset;
            bndsrc2.DataMember = "indt";
            comboBox1.DataSource = bndsrc2;
            comboBox1.ValueMember = "indentno";
            comboBox1.DisplayMember = "indentno";

            textBox14.Text = (max_po() + 1).ToString();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            listBox1.Items.Add(comboBox1.SelectedValue);
        }       

        private void button2_Click(object sender, EventArgs e)
        {

            Object[] item=new Object[listBox1.Items.Count];
            listBox1.Items.CopyTo(item, 0);
           Form2 dgv = new Form2(item , this);
            dgv.ShowDialog();
        }       

        public void retrive_data(int  total_row)
        {
            for (int i = 1; i <= total_row; i++)
            {
                add_row();
            }
            cmdstr = "select * from chloritech_db.dbo.potemp";
            cmd = new SqlCommand(cmdstr, con);
            adpt = new SqlDataAdapter(cmd);
            dset1 = new DataSet();
            adpt.Fill(dset1, "select_indent");
            int tb = 0, cb = 0;
            foreach (DataRow rrow in dset1.Tables["select_indent"].Rows)
            {
                ltb[tb].Text = rrow["indentno"].ToString();
                lcb[cb].SelectedText = rrow["item"].ToString();
                ltb[tb + 2].Text = rrow["indentqty"].ToString();
                lcb[cb + 1].SelectedValue = rrow["Unit"].ToString();
                tb = tb + 5;
                cb = cb + 2;
            }
        }

        private void buttonX1_Click(object sender, EventArgs e)
        {
            s.Width = 240;
           // genrate  control  objects 
            for (int i = 0; i < 5; i++)
            {
                ltb.Add(new OurT(m++));
                
            }

            lcb.Add(new OurC(n++));
            lcb.Add(new OurC(n++));
            ldp.Add(new DateTimePicker());

            

            // adjust  dynamic  controll's  size and  read only  property  

            ltb[count * trow + 1].Size = s;
            ltb[count * trow + 1].ReadOnly = false;
            ltb[count * trow + 3].ReadOnly = false;
            s.Width = 200;
            lcb[count * crow].Size = s;
            s.Width = 80;
            lcb[count * crow + 1].Size = s;
            s.Width = 130;
            ldp[count].Size = s; 
            
            //adding dynamic  event  handlers
            ltb[count * trow + 4].Enter += new System.EventHandler(total_count);



            //databindings  of  dynamic  contolls

            comb_dynamic_bind();// function for Bind Units



            // adding  controls  to flow  layout  pannel 

            flowLayoutPanel1.Controls.Add(ltb[count*trow]);
            flowLayoutPanel1.Controls.Add(lcb[count*crow]);
            flowLayoutPanel1.Controls.Add(ltb[count*trow+1]);
            ldp[count].CustomFormat = "dd/MM/yyyy";
            ldp[count].Format = DateTimePickerFormat.Custom;
            Font f = new Font("Segoe ui Symbol", 12, FontStyle.Regular);
            ldp[count].Font = f;
            flowLayoutPanel1.Controls.Add(ldp[count]);
            flowLayoutPanel1.Controls.Add(ltb[count * trow + 2]);
            flowLayoutPanel1.Controls.Add(ltb[count * trow + 3]);
            flowLayoutPanel1.Controls.Add(lcb[count * crow+1]);
            flowLayoutPanel1.Controls.Add(ltb[count * trow + 4]);
           

            count++;    

        }

        private void total_count(object sender, EventArgs e)
        {

            TextBox tb1 = (TextBox)sender;


            for (int i = 0; i < ltb.Count; i++)
            {
                try
                {

                    if (ltb[i].Name == tb1.Name)
                    {
                        //index = i;
                        double qty = Convert.ToDouble(ltb[i - 2].Text);
                        double br = Convert.ToDouble(ltb[i - 1].Text);
                        ltb[i].Text = "" + (qty * br);

                        break;
                    }
                }
                catch { }

            }
        }
        public void comb_dynamic_bind()
        {
            cmdstr = "select abbrunits from chloritech_db.dbo.tblunitsmaster";
            cmd = new SqlCommand(cmdstr, con);
            adpt = new SqlDataAdapter(cmd);
            dset = new DataSet();
            adpt.Fill(dset, "unit");
            BindingSource bndsrc = new BindingSource();
            bndsrc.DataSource = dset;
            bndsrc.DataMember = "unit";
            lcb[count * crow + 1].DataSource = bndsrc;
            lcb[count * crow + 1].ValueMember = "abbrunits";
            lcb[count * crow + 1].DisplayMember = "abbrunits";
        }

        private void textBox6_TextChanged(object sender, EventArgs e)
        {

        }

        private void comboBox4_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
        double sum = 0 , gsum = 0;
        private void textBox9_Enter(object sender, EventArgs e)
        {
            
            for (int i = 0; i < count; i++)
            {
                sum= sum + Convert.ToDouble(ltb[i*trow+4].Text);

            }
            textBox9.Text = "Rs " + sum;
        }

        private void textBox15_Enter(object sender, EventArgs e)
        {
            textBox15.Text = "Rs " + (sum + Convert.ToDouble(textBox10.Text));
            gsum = sum + Convert.ToDouble(textBox10.Text);
        }

        private void buttonX2_Click(object sender, EventArgs e)
        {
            String cmdstr = " select * from tblpo";
            DataSet dset2;
           
            cmd = new SqlCommand(cmdstr, con);
            adpt = new SqlDataAdapter(cmd);
            dset2 = new DataSet();
            int x =adpt.Fill(dset2, "main_entry");
            SqlCommandBuilder cmdb = new SqlCommandBuilder(adpt);
            DataRow row = dset2.Tables[0].NewRow();
            try
            {
               row["pono"] = Convert.ToInt64(textBox14.Text);
               row["supplier_name"] = comboBox4.Text;
               row["date"] = dateTimePicker2.Value;
               row["voucher"] = textBox6.Text;


               row["ref"] = textBox8.Text;
               row["terms"] = textBox7.Text;
               row["dispatch"] = textBox12.Text;
               row["destination"] = textBox13.Text;

               row["total"] = sum;
               row["tex"] = Convert.ToDouble(textBox10.Text);
               row["tex_dec"] = textBox11.Text;
               row["gross"] = gsum;

               dset2.Tables[0].Rows.Add(row);
               
               
                   x = adpt.Update(dset2, "main_entry");
               }
               catch
               {
                   MessageBox.Show("Entry Format Mismatch or Field Empty , Check Entered Value");
               }

            try
            {
                cmdstr = "select * from tblpodetails";

                cmd.CommandText = cmdstr;
                adpt.SelectCommand = cmd;
                x = adpt.Fill(dset2, "detail_entry");
                for (int i = 0; i < count; i++)
                {
                    row = dset2.Tables[1].NewRow();

                    row["pono"] = Convert.ToInt64(textBox14.Text);
                    row["indent_no"] = ltb[trow * i].Text;
                    row["item"] = lcb[crow * i].Text;
                    row["alias"] = ltb[trow * i + 1].Text;


                    row["due_date"] = ldp[i].Value;
                    row["qty"] = Convert.ToDouble(ltb[trow * i + 2].Text);
                    row["rate"] = Convert.ToDouble(ltb[trow * i + 3].Text);
                    row["unit"] = lcb[crow * i + 1].Text;

                    row["total"] = Convert.ToDouble(ltb[trow * i + 4].Text);


                    dset2.Tables[1].Rows.Add(row);
                    cmdb = new SqlCommandBuilder(adpt);
                    x = adpt.Update(dset2, "detail_entry");

                }
            }
            catch 
            {
                MessageBox.Show("Entry Format Mismatch or Field Empty , Check Entered Value");
            }
            

        }

        public void delete_all()
        {
            for (int i = 0; i < ltb.Count; i++)
            { ltb[i].Dispose();
            
            }
            ltb.Clear();
            for (int i = 0; i < lcb.Count; i++)
            { lcb[i].Dispose();
           
            }
            lcb.Clear();

            for (int i = 0; i < ldp.Count; i++)
            {
                ldp[i].Dispose();
                
            }
            ldp.Clear();
            count = 0;
        }      

        private void buttonX3_Click(object sender, EventArgs e)
        {
            delete_all();
        }

        private void buttonX4_Click(object sender, EventArgs e)
        {
            mxpo = max_po();
            if (Convert.ToInt32(textBox14.Text) <= mxpo)
            {
                Form3 f3 = new Form3(Convert.ToInt32(textBox14.Text));
                f3.Show();
            }
            else 
            {
                MessageBox.Show("PO Number " + Convert.ToInt32(textBox14.Text)+" is not available . To generate PO Press Insert First");
            }
            
        }

        private void button4_Click(object sender, EventArgs e)
        {
            // Retriving Only table portion 
            
            try
            {
                pono = Convert.ToInt32(textBox16.Text);
            }
            catch 
            {
                MessageBox.Show("Enter Valid PO Number");
                return;
            }
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
                //n--;
            }
            for (int i = 0; i < ldp.Count; i++)
            {
                flowLayoutPanel1.Controls.Remove(ldp[i]);
                ldp[i].Dispose();
                //n--;
            }
            flowLayoutPanel1.Visible = true;
            ltb.Clear();
            lcb.Clear();
            ldp.Clear();
            count = 0;

            cmdstr = "select * from chloritech_db.dbo.tblpodetails tpd where tpd.pono=" + pono;
            cmd = new SqlCommand(cmdstr, con);
            adpt = new SqlDataAdapter(cmd);
            dset1 = new DataSet();
            int tot_po;
            tot_po = (adpt.Fill(dset1, "podet")); 
            if (tot_po == 0)
            {
                MessageBox.Show("No Record Found");
                return;
            }
            for (int i = 1; i <= tot_po; i++)
            {
                add_row();
            }
            int tb = 0, cb = 0, dt = 0;
            foreach (DataRow rrow in dset1.Tables["podet"].Rows)
            {
                ltb[tb].Text = rrow["indent_no"].ToString();
                lcb[cb].SelectedText = rrow["item"].ToString();
                ltb[tb + 1].Text = rrow["alias"].ToString();
                ldp[dt].Value = Convert.ToDateTime(rrow["due_date"]);
                ltb[tb + 2].Text = rrow["qty"].ToString();
                ltb[tb + 3].Text = rrow["rate"].ToString();
                lcb[cb + 1].SelectedValue = rrow["Unit"].ToString();
                ltb[tb + 4].Text = rrow["total"].ToString();
                tb = tb + 5;
                cb = cb + 2;
                dt++;
            }
            // table portion retrived

            // other Retrive
            cmdstr = "select * from chloritech_db.dbo.tblpo tpo where tpo.pono=" + pono;
            cmd = new SqlCommand(cmdstr, con);
            adpt = new SqlDataAdapter(cmd);
            dset1 = new DataSet();
            adpt.Fill(dset1, "po");

            foreach (DataRow rrow in dset1.Tables["po"].Rows)
            {
                comboBox4.SelectedText = rrow["supplier_name"].ToString();
                dateTimePicker2.Value = Convert.ToDateTime(rrow["date"]);
                textBox14.Text = rrow["pono"].ToString();
                textBox7.Text = rrow["terms"].ToString();
                textBox13.Text = rrow["destination"].ToString();
                textBox8.Text = rrow["ref"].ToString();
                textBox12.Text = rrow["dispatch"].ToString();
                textBox6.Text = rrow["voucher"].ToString();
                textBox9.Text = rrow["total"].ToString();
                textBox10.Text = rrow["tex"].ToString();
                textBox11.Text = rrow["tex_dec"].ToString();
                textBox15.Text = rrow["gross"].ToString();
            }
        }

        private void button8_Click(object sender, EventArgs e)
        {
            listBox1.Items.Clear();
        }

        private void button9_Click(object sender, EventArgs e)
        {
            mailpo mp = new mailpo();
            mp.ShowDialog();
        }
        public void add_row()
        {
            s.Width = 240;
           // genrate  control  objects 
            for (int i = 0; i < 5; i++)
            {
                ltb.Add(new OurT(m++));
                
            }

            lcb.Add(new OurC(n++));
            lcb.Add(new OurC(n++));
            ldp.Add(new DateTimePicker());

            

            // adjust  dynamic  controll's  size and  read only  property  

            ltb[count * trow + 1].Size = s;
            ltb[count * trow + 1].ReadOnly = false;
            ltb[count * trow + 3].ReadOnly = false;
            s.Width = 200;
            lcb[count * crow].Size = s;
            s.Width = 80;
            lcb[count * crow + 1].Size = s;
            s.Width = 130;
            ldp[count].Size = s; 
            
            //adding dynamic  event  handlers
            ltb[count * trow + 4].Enter += new System.EventHandler(total_count);



            //databindings  of  dynamic  contolls

            comb_dynamic_bind();// function for Bind Units



            // adding  controls  to flow  layout  pannel 

            flowLayoutPanel1.Controls.Add(ltb[count*trow]);
            flowLayoutPanel1.Controls.Add(lcb[count*crow]);
            flowLayoutPanel1.Controls.Add(ltb[count*trow+1]);
            ldp[count].CustomFormat = "dd/MM/yyyy";
            ldp[count].Format = DateTimePickerFormat.Custom;
            Font f = new Font("Segoe ui Symbol", 12, FontStyle.Regular);
            ldp[count].Font = f;
            flowLayoutPanel1.Controls.Add(ldp[count]);
            flowLayoutPanel1.Controls.Add(ltb[count * trow + 2]);
            flowLayoutPanel1.Controls.Add(ltb[count * trow + 3]);
            flowLayoutPanel1.Controls.Add(lcb[count * crow+1]);
            flowLayoutPanel1.Controls.Add(ltb[count * trow + 4]);
           

            count++;



        }
        public int max_po()
        {
            cmdstr = "select max(pono) as pono from tblpo";
            cmd = new SqlCommand(cmdstr, con);
            adpt = new SqlDataAdapter(cmd);
            adpt.Fill(dset, "mxpo");
            foreach (DataRow rrow in dset.Tables["mxpo"].Rows)
            {
                mxpo = Convert.ToInt32(rrow["pono"]);
            }
            return (mxpo);
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            cmnall.cmncls.check_off("po");
        }
        
    }
}
