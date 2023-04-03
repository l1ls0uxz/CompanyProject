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
            
        }

        private void btnCreate_Click(object sender, EventArgs e)
        {

        }
    }
}
