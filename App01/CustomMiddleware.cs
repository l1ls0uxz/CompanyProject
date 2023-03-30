using Microsoft.Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace App01
{
    public class CustomMiddleware : OwinMiddleware
    {
        public CustomMiddleware(OwinMiddleware next) : base(next)
        {
        }
        public async override Task Invoke(IOwinContext context)
        {
            context.Response.Headers["PC-Name"] = Environment.MachineName;
            await Next.Invoke(context);
        }
    }
}
