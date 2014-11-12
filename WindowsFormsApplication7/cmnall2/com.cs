using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Data;
using System.Drawing;


namespace cmnall2
{

    public static class com
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
            String str = "Data Source=.\\SQLEXPRESS;Initial Catalog=Chloritech_db;Integrated Security=True";
            con = new SqlConnection(constr);
            cmd = new SqlCommand(cmdstr, con);
            adpt = new SqlDataAdapter(cmd);
            dset = new DataSet();
            con.Open();

            adpt.Fill(dset, "chk");

            foreach (DataRow row in dset.Tables["chk"].Rows)
            {

                str = row["constr"].ToString();




            }
            con.Close();
            return str;
        }

    }
}
