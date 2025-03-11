using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TravelAgency.DbAcess
{
    class DatabaseConnection
    {
        private static string _connectionString = "Data Source=localhost;Initial Catalog=KudahterDB;Integrated Security=True;";

        public static SqlConnection GetConnection()
        {
            return new SqlConnection(_connectionString);
        }

        public static bool TestConnection()
        {
            try
            {
                using (SqlConnection connection = GetConnection())
                {
                    connection.Open();
                    return true;
                }
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
