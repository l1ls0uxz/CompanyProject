using Microsoft.Owin.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace TestAPI
{
    internal class WebServer
    {
        private IDisposable _disposed;
        public void Start()
        {
            _disposed = WebApp.Start<Startup>("http://localhost:55555");
        }
        public void Stop()
        {
            _disposed.Dispose();
        }
    }
}
