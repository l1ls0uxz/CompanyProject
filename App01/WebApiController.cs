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
using Mysqlx;
using System.Security.Cryptography;
using System.Diagnostics;
using System.Text.RegularExpressions;

namespace App01
{
    public class WebApiController : ApiController
    {
        /// Connect MySQL Database
        string datareturn;
        string connStr = "server=127.0.0.1;user=root;database=demohmiconnectpc1;port=3306;password=0546";

        // check if token is valid
        private bool IsValidToken(string token)
        {
            try
            {
                string checktoken = "select * from tokenapi where mac=@mac";
                DataTable dt = new DataTable();
                MySqlDataReader myReaderr;
                using (MySqlConnection mycon = new MySqlConnection(connStr))
                {
                    mycon.Open();
                    using (MySqlCommand myCommand = new MySqlCommand(checktoken, mycon))
                    {
                        myCommand.Parameters.AddWithValue("@mac", token);
                        myReaderr = myCommand.ExecuteReader();
                        dt.Load(myReaderr);
                        myReaderr.Close();
                        mycon.Close();
                    }
                }
                if (dt.Rows.Count > 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch
            {
                return false;
            }
        }

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

        // GET api/webapi/name/datefrom/dateto/token
        [Route("api/{controller}/{name}/{datetfrom}/{dateto}/{token}")]
        public string GetDate(string name, string datefrom, string dateto, string token)
        {
            // check if token is valid
            if (!IsValidToken(token))
            {
                return "Wrong or invalid Token. Please check and try again!";
            }
            // check if table exists
            if (!IsTableExists(name))
            {
                return "Table does not exist in the Database.";
            }

            // continues to execute
            string query = "select * from " + $"{name}" + " where DateTime between " + $"{datefrom}" + "and " + $"{dateto}" + " order by DateTime desc";
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
        [Route("api/{controller}/{username}/{password}/{mac}")]
        public string GetData(string username, string password, string mac)
        {
            // Scan for MAC addresses using the arp command
            Process process = new Process();
            process.StartInfo.FileName = "arp";
            process.StartInfo.Arguments = "-a";
            process.StartInfo.UseShellExecute = false;
            process.StartInfo.RedirectStandardOutput = true;
            process.Start();
            string output = process.StandardOutput.ReadToEnd();
            process.WaitForExit();
            List<string> macAddresses = new List<string>();
            foreach (Match match in Regex.Matches(output, @"([0-9A-Fa-f]{2}[:-]){5}([0-9A-Fa-f]{2})"))
            {
                macAddresses.Add(match.Value);
            }

            // Compare the MAC addresses obtained from the arp command with the MAC address in the API link
            if (macAddresses.Contains(mac))
            {
                // username and password import from api link
                string usernameFromApiLink = username;
                string passwordFromApiLink = password;

                // create a MySqlConnection object and open the database 
                MySqlConnection connection = new MySqlConnection(connStr);
                connection.Open();

                // construct the SQL query and execute it
                string query = "SELECT password FROM usernpass WHERE username = @username";
                MySqlCommand command = new MySqlCommand(query, connection);
                command.Parameters.AddWithValue("@username", usernameFromApiLink);
                string passwordFromDatabase = (string)command.ExecuteScalar();
                connection.Close();

                // Compare the passwords
                bool passwordsMatch = string.Equals(passwordFromDatabase, passwordFromApiLink);
                if (passwordsMatch == true)
                {
                    // Convert a MAC address string to a byte array
                    byte[] macBytes = Encoding.UTF8.GetBytes(mac);

                    // Initialize an MD5 encryption object
                    MD5 md5 = MD5.Create();

                    // Encrypt the byte array of a MAC address using MD5
                    byte[] hashedBytes = md5.ComputeHash(macBytes);

                    // Convert the encrypted byte array to a hexadecimal string
                    StringBuilder stringBuilder = new StringBuilder();
                    for (int i = 0; i < hashedBytes.Length; i++)
                    {
                        stringBuilder.Append(hashedBytes[i].ToString("X2")); // X2 to convert bytes into a hexadecimal string with 2 characters.
                    }
                    connection.Open();
                    // The MAC address has been encrypted
                    string hashedMac = stringBuilder.ToString();
                    // write token to database
                    try
                    {
                        string hashedMacQuery = "INSERT INTO tokenapi (datetime, mac) VALUES (NOW(), " + $"'{hashedMac}'" + ")";
                        DataTable table = new DataTable();
                        MySqlDataReader myReader;
                        using (MySqlConnection mycon = new MySqlConnection(connStr))
                        {
                            mycon.Open();
                            using (MySqlCommand myCommand = new MySqlCommand(hashedMacQuery, mycon))
                            {
                                myReader = myCommand.ExecuteReader();
                                table.Load(myReader);
                                myReader.Close();
                                mycon.Close();
                            }

                        }
                    }
                    catch (Exception error)
                    {
                        return error.Message.ToString();
                    }
                    connection.Close();
                    return hashedMac;
                }
                else
                {
                    return "Please check your username, password and try again.";
                }
            }
            else
            {
                return "MAC address not found in API link or you use a fake MAC address?. Import your real MAC and try again. Example Format: 'e0-bb-9e-75-7c-b3' or 'e0:bb:9e:75:7c:b3'";
            }
        }
        // If you want to create additional routes, create them from here.
        // POST api/webapi/mac
        [Route("api/{controller}/{username}/{mac}")]
        public string GetMacc(string mac, string username)
        {
            try
            {
                // Insert data into second table
                string query = "INSERT INTO tokenapi (datetime, mac, username) VALUES (NOW(), " + $"'{mac}'" + ", " + $"'{username}'" + ")";
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
