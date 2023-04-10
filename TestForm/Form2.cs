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
using SharpPcap;

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
            // Lấy địa chỉ MAC của máy khách
            string macAddress = string.Empty;
            IPGlobalProperties properties = IPGlobalProperties.GetIPGlobalProperties();
            NetworkInterface[] interfaces = NetworkInterface.GetAllNetworkInterfaces();

            foreach (NetworkInterface nic in interfaces)
            {
                if (nic.NetworkInterfaceType == NetworkInterfaceType.Ethernet ||
                    nic.NetworkInterfaceType == NetworkInterfaceType.Wireless80211)
                {
                    if (nic.OperationalStatus == OperationalStatus.Up)
                    {
                        macAddress = nic.GetPhysicalAddress().ToString();
                        break;
                    }
                }
            }
        }
    }
}
