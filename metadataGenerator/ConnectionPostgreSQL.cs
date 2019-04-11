using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Npgsql;
using NpgsqlTypes;
using System.Data;
using System.Configuration;

namespace metadataGenerator
{
    public class ConnectionPostgreSQL
    {
        Parameters Parameters = new Parameters();
        //string connectionString = ConfigurationManager.AppSettings["PostgreSQLConnection"];
        Logger Logger = new Logger();

        public DataTable getResults(string query)
        {
            try
            {
                string connectionString = Parameters.cnnString();
                NpgsqlConnection connection = new NpgsqlConnection(connectionString);
                connection.Open();
                DataSet ds = new DataSet();
                NpgsqlDataAdapter dataAdapter = new NpgsqlDataAdapter(query, connection);
                dataAdapter.Fill(ds);
                DataTable table = ds.Tables[0];
                return table;
            }
            catch (Exception e)
            {
                Logger.createLog(e.Message.ToString(), "e");
                return null;
            }
            
        }
    }
}
