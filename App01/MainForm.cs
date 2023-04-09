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
using System.Net.NetworkInformation;
using System.Security.Cryptography;

namespace App01
{
    public partial class MainForm : Form
    {
        //string datareturn;
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

        private async void btnGET_Click(object sender, EventArgs e)
        {
            dtView.Clear();
            var responce = await Helper.GetDate(
               cbxName.Text.ToString(),
               dateTimePicker1.Value.ToString("yyyy-MM-dd HH:mm:ss"),
               dateTimePicker2.Value.ToString("yyyy-MM-dd HH:mm:ss")
               );
            dtView.Text = Helper.BeautifyJson(responce);
        }

        private void dtView_TextChanged(object sender, EventArgs e)
        {

        }

        private void btnMac_Click(object sender, EventArgs e)
        {
            // Clear the RichTextBox
            dtView.Clear();

            // Get the first MAC address of the PC
            string mac = "";
            foreach (NetworkInterface nic in NetworkInterface.GetAllNetworkInterfaces())
            {
                if (nic.OperationalStatus == OperationalStatus.Up && (!nic.Description.Contains("Virtual") && !nic.Description.Contains("Pseudo")))
                {
                    if (nic.GetPhysicalAddress().ToString() != "")
                    {
                        mac = nic.GetPhysicalAddress().ToString();
                        break; // Exit the loop after getting the first MAC address
                    }
                }
            }

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

            // The MAC address has been encrypted
            string hashedMac = stringBuilder.ToString();

            // Display the encrypted MAC address in a RichTextBox
            dtView.AppendText("Hashed MAC Address: " + hashedMac + Environment.NewLine);

            // Display the MAC address in the dtView
            dtView.AppendText("MAC Address: " + mac + Environment.NewLine);
        }
    }
}
