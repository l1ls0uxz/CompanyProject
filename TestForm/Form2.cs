using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net;
using System.Net.NetworkInformation;

namespace TestForm
{
    public partial class FormTest : Form
    {
        //private Guid _userId;
        public FormTest()
        {
            InitializeComponent();
            //_userId = userId;
        }

        private void Form2_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            IPAddress ipAddress = IPAddress.Parse("192.168.1.91"); // Địa chỉ IP của người dùng
            PhysicalAddress physicalAddress = null;

            foreach (NetworkInterface nic in NetworkInterface.GetAllNetworkInterfaces())
            {
                IPInterfaceProperties properties = nic.GetIPProperties();
                foreach (UnicastIPAddressInformation ip in properties.UnicastAddresses)
                {
                    if (ip.Address.Equals(ipAddress))
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
                richTextBox1.AppendText("MAC Address: " + macAddress + Environment.NewLine);
            }
            else
            {
                richTextBox1.AppendText("MAC Address not found." + Environment.NewLine);
            }
        }
    }
}
