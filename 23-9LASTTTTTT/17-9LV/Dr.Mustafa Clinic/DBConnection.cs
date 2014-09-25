using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;

namespace Dr.Mustafa_Clinic
{
    class DBConnection
    {
        private string sql_string;
        private string strCon;
        SqlDataAdapter da_1;

        public string Sql
        {

            set { sql_string = value; }
        }

        public string connection_string
        {

            set { strCon = value; }

        }
        public DataSet GetConnection
        {

            get { return MyDataSet(); }

        }

        private DataSet MyDataSet()
        {
            SqlConnection con = new System.Data.SqlClient.SqlConnection(strCon);
            con.Open();
            DataSet Dat_set = new DataSet();
            da_1.Fill(Dat_set);
            con.Close();

            return Dat_set;

        }

    }
        

}
