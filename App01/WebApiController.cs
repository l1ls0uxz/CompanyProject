using MySql.Data.MySqlClient;
using Mysqlx.Connection;
using Newtonsoft.Json;
using System.Web.Http;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http.Results;
using App01.Filters;
using Mysqlx.Crud;
using System.Net.NetworkInformation;
using Org.BouncyCastle.Bcpg;
using System.Xml.Linq;
using System.Data.SqlClient;
using System.Collections;

namespace App01
{
    public class WebApiController : ApiController
    {
        // Connect MySQL Database
        string datareturn;
        string connStr = "server=127.0.0.1;user=root;database=demohmiconnectpc1;port=3306;password=0546";

        // check table exists
        public bool IsTableExists(string tableName)
        {
            using (MySqlConnection conn = new MySqlConnection(connStr))
            {
                conn.Open();
                string query = "SELECT COUNT(*) FROM information_schema.tables WHERE table_schema = @dbName AND table_name = @tableName";
                MySqlCommand cmd = new MySqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@dbName", "demohmiconnectpc1"); // Replace "myDatabase" with the name of your database (already changed)
                cmd.Parameters.AddWithValue("@tableName", tableName);

                int count = Convert.ToInt32(cmd.ExecuteScalar());
                return count > 0;
            }
        }
        /*
        // GET api/webapi/name
        [Route("api/{controller}/{name}")]
        //[GzipCompressionAtribute]
        public string GetItemByName(string name)
        {

            string query = "select * from " + $"{name}";

            DataTable table = new DataTable();
            MySqlDataReader myReader;
            using (MySqlConnection mycon = new MySqlConnection(connStr))
            {
                mycon.Open();
                using (MySqlCommand myCommand = new MySqlCommand(query, mycon))
                {
                    myReader = myCommand.ExecuteReader();
                    table.Load(myReader);
                    myReader.Close();
                    mycon.Close();
                }
            }
            datareturn = JsonConvert.SerializeObject(table);
            return datareturn;
        }
        */
        /*
        // GET api/webapi/name/id
        //[Route("api/{controller}/{name}/{id}")]
        //public string GetItemByNameAndId(string name, int id)
        //{
        //    string query = "select * from " + $"{name}" + " where id = " + $"{id}";

        //    DataTable table = new DataTable();
        //    MySqlDataReader myReader;
        //    using (MySqlConnection mycon = new MySqlConnection(connStr))
        //    {
        //        mycon.Open();
        //        using (MySqlCommand myCommand = new MySqlCommand(query, mycon))
        //        {
        //            myReader = myCommand.ExecuteReader();
        //            table.Load(myReader);
        //            myReader.Close();
        //            mycon.Close();
        //        }
        //    }

        //    datareturn = JsonConvert.SerializeObject(table);
        //    return datareturn;
        //}
        */

        // GET api/webapi/name/datefrom/dateto
        //[Route("api/{controller}/{name}/{datetfrom}/{dateto}")]
        public string GetDate(string name, string datefrom, string dateto)
        {
            // check if table exists
            if (!IsTableExists(name))
            {
                return "Table does not exist in the Database.";
            }

            // continues to execute
            string query = "select * from " + $"{name}" + " where DateTime between " + $"{datefrom}" + "and " + $"{dateto}" + " order by DateTime desc";
            //string query1 = "select * from " + $"{name}" + " where DateTime between " + $"{datefrom}" + "and " + $"{dateto}" + " order by DateTime desc";
            DataTable table = new DataTable();
            MySqlDataReader myReader;
            using (MySqlConnection mycon = new MySqlConnection(connStr))
            {
                mycon.Open();
                using (MySqlCommand myCommand = new MySqlCommand(query, mycon))
                {
                    myReader = myCommand.ExecuteReader();   
                    table.Load(myReader);
                    myReader.Close();
                    mycon.Close();
                }
            }

            datareturn = JsonConvert.SerializeObject(table);
            return datareturn;
        }

        // GET api/webapi/mac
        [Route("api/macaddress/{ipAddress}")]
        public IHttpActionResult GetMac(string ipAddress)
        {
            PhysicalAddress physicalAddress = null;

            foreach (NetworkInterface nic in NetworkInterface.GetAllNetworkInterfaces())
            {
                IPInterfaceProperties properties = nic.GetIPProperties();
                foreach (UnicastIPAddressInformation ip in properties.UnicastAddresses)
                {
                    if (ip.Address.ToString() == ipAddress)
                    {
                        physicalAddress = nic.GetPhysicalAddress();
                        break;
                    }
                }
                if (physicalAddress != null) break;
            }

            if (physicalAddress != null)
            {
                string macAddress = BitConverter.ToString(physicalAddress.GetAddressBytes());
                return Ok(macAddress);
            }
            else
            {
                return NotFound();
            }
        }

        // POST api/webapi/username/password/mac
        [Route("api/{controller}/{name}/{username}/{password}")]
        public string GetData(string name, string username, string password)
        {
            // check if table exists
            if (!IsTableExists(name))
            {
                return "Table does not exist in the Database.";
            }
            // Insert data into first table
            string query = "INSERT INTO " + $"{name}" + " (username, password) VALUES ($'{username}', $'{password}')";
            // INSERT INTO usernpass (username, password) VALUES('user2', 'pass123'); this is query use in MySQL
            DataTable table = new DataTable();
            MySqlDataReader myReader;
            using (MySqlConnection mycon = new MySqlConnection(connStr))
            {
                mycon.Open();
                using (MySqlCommand myCommand = new MySqlCommand(query, mycon))
                {
                    myReader = myCommand.ExecuteReader();
                    table.Load(myReader);
                    myReader.Close();
                    mycon.Close();
                }
            }
            return datareturn;
        }

        // POST api/webapi/mac
        [Route("api/{controller}/{username}/{mac}")]
        public string GetMacc(string mac, string username)
        {
            try
            {
                // Insert data into second table
                string query = "INSERT INTO tokenapi (datetime, mac, username) VALUES (NOW(), " + $"'{mac}'" + ", " + $"'{username}'" + ")";
                // INSERT INTO tokenapi (datetime, mac, username) VALUES(NOW(), 'a04c0c0a4a60', 'user2'); this is query use in MySQL
                DataTable table = new DataTable();
                MySqlDataReader myReader;
                using (MySqlConnection mycon = new MySqlConnection(connStr))
                {
                    mycon.Open();
                    using (MySqlCommand myCommand = new MySqlCommand(query, mycon))
                    {
                        myReader = myCommand.ExecuteReader();
                        table.Load(myReader);
                        myReader.Close();
                        mycon.Close();
                    }
                }
                return datareturn;
            }
            catch (Exception ex)
            {
                return (ex.Message);
            }
            
            
        }

        // PUT api/webapi/1
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/webapi/1
        public void Delete(int id)
        {
        }
    }
}
