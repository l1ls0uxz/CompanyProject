using App01.Shared;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.UI.WebControls;
using System.Windows.Forms;
using System.Net.Http;
using System.Net.Http.Headers;
using MySqlX.XDevAPI.Relational;
using MySqlX.XDevAPI;
using MySqlX.XDevAPI.Common;

namespace App01
{
    public partial class MainForm : Form
    {
        string datareturn;
        public MainForm()
        {
            InitializeComponent();
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
            LoginForm loginForm = new LoginForm();
            loginForm.Show();
        }

        private void btnAbout_Click(object sender, EventArgs e)
        {
            About about = new About();
            about.Show();
        }

        private async void btnExcel_Click(object sender, EventArgs e)
        {
            var responce = await Helper.GetDate(
               cbxName.Text.ToString(),
               dateTimePicker1.Value.ToString("yyyy-MM-dd HH:mm:ss"),
               dateTimePicker2.Value.ToString("yyyy-MM-dd HH:mm:ss")
               );
            dtView.Text = Helper.BeautifyJson(responce);
        }
    }
}
