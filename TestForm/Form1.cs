using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace TestForm
{
    public partial class Form1 : Form
    {
        private readonly string connectionString = "Server=127.0.0.1;Port=3306;Database=demohmiconnectpc1;Uid=root;Pwd=0546;";
        public Form1()
        {
            InitializeComponent();
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            // Validate the user's credentials
            if (IsValidCredentials(txtUsername.Text, txtPassword.Text))
            {
                // Generate a new GUID for the user
                Guid userId = Guid.NewGuid();

                // Connect to the MySQL database
                string connectionString = "server=localhost;database=mydatabase;uid=myusername;password=mypassword;";
                using MySqlConnection connection = new MySqlConnection(connectionString);
                connection.Open();

                // Create a new MySQL command to insert the user's ID into the database
                string query = "INSERT INTO users (id) VALUES (@id)";
                using MySqlCommand command = new MySqlCommand(query, connection);
                command.Parameters.AddWithValue("@id", userId);
                command.ExecuteNonQuery();

                // Disconnect from the MySQL database
                connection.Close();

                // Show the main form
                Form2 form2 = new Form2(userId);
                form2.Show();
                this.Hide();
            }
            else
            {
                MessageBox.Show("Invalid username or password.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private bool IsValidCredentials(string username, string password)
            {
                // Validate the user's credentials here
                // Return true if the credentials are valid, false otherwise
                return true;
            }

        private void btnCreate_Click(object sender, EventArgs e)
        {

        }
    }
}
