using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;

namespace metadataGenerator
{
    public class ConnectionSQL
    {
        string connectionString = "Data Source=192.168.30.138;" +
                                    "Initial Catalog=TUES;" +
                                    "User id=sa;" +
                                    "Password=Ankara123;";
        SqlConnection con;

        public void openConnection()
        {
            con = new SqlConnection(connectionString);
            con.Open();
        }

        public void closeConnection()
        {
            con.Close();
        }

        public SqlDataReader DataReader(string Query_)
        {
            SqlCommand cmd = new SqlCommand(Query_, con);
            SqlDataReader dr = cmd.ExecuteReader();
            return dr;
        }

        public object ShowDataInGridView(string Query_)
        {
            SqlDataAdapter dr = new SqlDataAdapter(Query_, connectionString);
            DataSet ds = new DataSet();
            dr.Fill(ds);
            object dataum = ds.Tables[0];
            return dataum;
        }
    }
}
