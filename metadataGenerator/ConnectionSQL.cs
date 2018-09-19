using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Configuration;
using System.Data.SqlClient;

namespace metadataGenerator
{
    public class ConnectionSQL
    {
        string connectionString = ConfigurationManager.AppSettings["SQLServer"];
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

        public DataTable ShowDataInGridView(string Query_)
        {
            DataTable table = new DataTable();
            SqlDataAdapter dr = new SqlDataAdapter(Query_, connectionString);
            DataSet ds = new DataSet();
            dr.Fill(ds);
            dr.Fill(table);
            return table;
        }
    }
}
