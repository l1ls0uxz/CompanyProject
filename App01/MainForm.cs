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

        private async void btnGET_Click(object sender, EventArgs e)
        {
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

            // Lấy chuỗi MAC đầu tiên của pc
            string mac = "";
            foreach (NetworkInterface nic in NetworkInterface.GetAllNetworkInterfaces())
            {
                if (nic.OperationalStatus == OperationalStatus.Up && (!nic.Description.Contains("Virtual") && !nic.Description.Contains("Pseudo")))
                {
                    if (nic.GetPhysicalAddress().ToString() != "")
                    {
                        mac = nic.GetPhysicalAddress().ToString();
                        break; // Thoát vòng lặp sau khi lấy được MAC đầu tiên
                    }
                }
            }

            // Chuyển chuỗi MAC thành mảng byte
            byte[] macBytes = Encoding.UTF8.GetBytes(mac);

            // Khởi tạo đối tượng mã hóa MD5
            MD5 md5 = MD5.Create();

            // Mã hóa mảng byte của chuỗi MAC bằng MD5
            byte[] hashedBytes = md5.ComputeHash(macBytes);

            // Chuyển mảng byte mã hóa thành chuỗi hexa
            StringBuilder stringBuilder = new StringBuilder();
            for (int i = 0; i < hashedBytes.Length; i++)
            {
                stringBuilder.Append(hashedBytes[i].ToString("X2")); // X2 để chuyển byte thành chuỗi hexa có 2 ký tự
            }

            // Chuỗi MAC đã được mã hóa
            string hashedMac = stringBuilder.ToString();

            // Hiển thị chuỗi MAC đã được mã hóa trong RichTextBox
            dtView.AppendText("Hashed MAC Address: " + hashedMac + Environment.NewLine);

            // Hiển thị chuỗi MAC trong dtView
            dtView.AppendText("MAC Address: " + mac + Environment.NewLine);
        }
    }
}
