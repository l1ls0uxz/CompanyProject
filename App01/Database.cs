/* Author: Lil s0uxz
 * Github profile: https://github.com/lils0uxz
 * Version: beta-1.0.1
 */

using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

namespace App01
{
    class Database
    {
        // connect to mysql database
        private MySqlConnection connection = new MySqlConnection("server=127.0.0.1;port=3306;username=root;password=0546;database=demohmiconnectpc");


        // create a function to open the connection
        public void openConnection()
        {
            if (connection.State == System.Data.ConnectionState.Closed)
            {
                connection.Open();
            }
        }

        // create a function to close the connection
        public void closeConnection()
        {
            if (connection.State == System.Data.ConnectionState.Open)
            {
                connection.Close();
            }
        }

        // create a function to return the connection
        public MySqlConnection getConnection()
        {
            return connection;
        }
    }
}
