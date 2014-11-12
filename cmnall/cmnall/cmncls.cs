using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Data;
using System.Drawing;


namespace cmnall
{
   public static class cmncls
   {


      public static String user;
      public static String constring;
      
      static SqlConnection con;
      static SqlCommand cmd;
      static SqlDataAdapter adpt;
      static DataSet dset, dset2;
      static string constr, cmdstr;
       
        public static int grn = 0;


        public static String get_constr111()
        {

            constr = @"Data Source=.\SQLEXPRESS;Initial Catalog=M3;Integrated Security=True";
            cmdstr = "select pk ,constr  from sync where pk=1";
            String str="Data Source=.\\SQLEXPRESS;Initial Catalog=Chloritech_db;Integrated Security=True";
            con = new SqlConnection(constr);
            cmd = new SqlCommand(cmdstr, con);
            adpt = new SqlDataAdapter(cmd);
            dset = new DataSet();
            con.Open();

            adpt.Fill(dset, "chk");

            foreach (DataRow row in dset.Tables["chk"].Rows)
            {
                
                   str = row["constr"].ToString() ;
               
               
                

            }
            con.Close();
            return str;
        }


        public static bool allowe_me( String frm)
        {
            constr = @"Data Source=.\SQLEXPRESS;Initial Catalog=M3;Integrated Security=True";
            cmdstr = "select * from login_details where user_name='"+user+"'";
            con = new SqlConnection(constr);
            cmd = new SqlCommand(cmdstr, con);
            adpt = new SqlDataAdapter(cmd);
            dset = new DataSet();
            con.Open();

            adpt.Fill(dset, "chk");

            foreach (DataRow row in dset.Tables["chk"].Rows)
            {
                if (Convert.ToInt32(row[frm]) == 1)
                    return true;
                else
                    return false;

               
            }
            return false;

            con.Close();
 
        }


        public static bool check(String str)
        {
            
            bool b=true;
            constr = @"Data Source=.\SQLEXPRESS;Initial Catalog=M3;Integrated Security=True";     
            cmdstr = "select pk ,"+str+" from sync where pk=1";

            con = new SqlConnection(constr);
            cmd = new SqlCommand(cmdstr, con);
            adpt = new SqlDataAdapter(cmd);
            dset = new DataSet();
            con.Open();

            adpt.Fill(dset, "chk");

            foreach (DataRow row in dset.Tables["chk"].Rows)
            {
                if (1 == Convert.ToInt16(row[str]))
                {
                    b = true;
                    row[str] = 0;
                }
                else
                    b = false;
               

                SqlCommandBuilder cmdb = new SqlCommandBuilder(adpt);
                adpt.Update(dset, "chk");
               
            }
            con.Close();
            return b;
          
        }







        public static void check_off(String str)
        {
           
            constr = @"Data Source=.\SQLEXPRESS;Initial Catalog=M3;Integrated Security=True";
            cmdstr = "select pk ," + str + " from sync where pk=1";

            con = new SqlConnection(constr);
            cmd = new SqlCommand(cmdstr, con);
            adpt = new SqlDataAdapter(cmd);
            dset = new DataSet();
            con.Open();

            adpt.Fill(dset, "chk");

            foreach (DataRow row in dset.Tables["chk"].Rows)
            {
              
                    row[str] = 1;
               
                SqlCommandBuilder cmdb = new SqlCommandBuilder(adpt);
                adpt.Update(dset, "chk");
            }

            con.Close();
        }


       public static void  chnage_image()
       {
           Image pic = Image.FromFile(@"F:\pract-4.png");

           ImageConverter ic = new ImageConverter();
           byte[] bt = (byte[])ic.ConvertTo(pic, typeof(byte[]));

           constr = @"Data Source=.\SQLEXPRESS;Initial Catalog=M3;Integrated Security=True";
           con = new SqlConnection(constr);
           con.Open();

           cmdstr = "select * from M3.dbo.login_details where user_name='bijal'";
           cmd = new SqlCommand(cmdstr, con);
           adpt = new SqlDataAdapter(cmd);
           dset2 = new DataSet();
           adpt.Fill(dset2, "login");

           foreach (DataRow row in dset2.Tables["login"].Rows)
           {
               
                   row["user_name"] = "";

              
                   row["img"] = bt;
              
               SqlCommandBuilder cmdb = new SqlCommandBuilder(adpt);
               adpt.Update(dset2, "login");


           }
          
           con.Close();

       }

        public static string get_constr()
        {
            string cons = "";
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

                cons = row["constr"].ToString();


            }

            return cons;
            con.Close();
        }

    }

    
}
