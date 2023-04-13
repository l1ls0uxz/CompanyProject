using App01.Shared;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//using System.Web.UI.WebControls;
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
        private NotifyIcon notifyIcon;
        //string datareturn;
        public MainForm()
        {
            InitializeComponent();
            // Create the NotifyIcon object
            notifyIcon = new NotifyIcon();
            notifyIcon.Icon = new Icon("icon.ico");
            notifyIcon.Text = "My Application";
            notifyIcon.Visible = true;

            // Add event handlers to the NotifyIcon
            notifyIcon.DoubleClick += new EventHandler(NotifyIcon_DoubleClick);
            ContextMenu menu = new ContextMenu();
            MenuItem showMenuItem = new MenuItem("Show");
            showMenuItem.Click += new EventHandler(ShowMenuItem_Click);
            menu.MenuItems.Add(showMenuItem);
            MenuItem exitMenuItem = new MenuItem("Exit");
            exitMenuItem.Click += new EventHandler(ExitMenuItem_Click);
            menu.MenuItems.Add(exitMenuItem);
            notifyIcon.ContextMenu = menu;

            // Set the form's properties
            this.Text = "My Application";
            this.Resize += new EventHandler(Home_Resize);
            this.ShowInTaskbar = false;
        }
        private void ExitMenuItem_Click(Object sender, EventArgs e)
        {
            // Close the application
            Application.Exit();
        }

        private void Home_Resize(Object sender, EventArgs e)
        {
            // If the form is being minimized, hide it and show the NotifyIcon
            if (this.WindowState == FormWindowState.Minimized)
            {
                this.Hide();
                notifyIcon.Visible = true;
            }
        }

        private void NotifyIcon_DoubleClick(Object sender, EventArgs e)
        {
            // Show the form and hide the NotifyIcon
            this.Show();
            this.WindowState = FormWindowState.Normal;
            notifyIcon.Visible = false;
        }

        private void ShowMenuItem_Click(Object sender, EventArgs e)
        {
            // Show the form and hide the NotifyIcon
            this.Show();
            this.WindowState = FormWindowState.Normal;
            notifyIcon.Visible = false;
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
