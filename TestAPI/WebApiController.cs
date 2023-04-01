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
using TestAPI.Filters;

namespace TestAPI
{
    public class WebApiController : ApiController
    {
        // Connect MySQL Database
        string datareturn;
        string connStr = "server=127.0.0.1;user=root;database=demohmiconnectpc1;port=3306;password=0546";

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

        // GET api/webapi/name/id
        [Route("api/{controller}/{name}/{id}")]
        public string GetItemByNameAndId(string name, int id)
        {
            string query = "select * from " + $"{name}" + " where id = " + $"{id}";

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

        // POST api/webapi
        public void Post([FromBody] string value)
        {
        }

        // PUT api/webapi/5 
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/webapi/5 
        public void Delete(int id)
        {
        }
    }
}
