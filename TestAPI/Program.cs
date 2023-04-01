using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Owin.Hosting;
using MySql.Data.MySqlClient;
//using System.Windows.Forms;
using static System.Net.Mime.MediaTypeNames;
using System.Drawing;
using System.ComponentModel;

namespace TestAPI
{
    public class Program
    {
        public Program()
        {

        }
        [STAThread]
        static void Main(string[] args)
        {
            using (WebApp.Start<Startup>("http://localhost:55555"))
            {
                
                Console.WriteLine("Server started. Press any key to stop.");
                Console.ReadKey();
                
            }
            //Application.EnableVisualStyles();
            //Application.Run(new Program());
            //Application.SetCompatibleTextRenderingDefault(false);
        }
        //private void InitializeComponent()
        //{
        //    this.SuspendLayout();
        //    // 
        //    // Program
        //    // 
        //    this.ClientSize = new System.Drawing.Size(500, 500);
        //    this.Name = "Program";
        //    this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
        //    this.Load += new System.EventHandler(this.Program_Load);
        //    this.ResumeLayout(false);

        //}
        //private void Program_Load(object sender, EventArgs e)
        //{

        //}
    }
}
