using Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using System.Web.Http;
//using System.Web.UI.WebControls;

namespace App01
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            // Adding to the pipeline with our own middleware
            app.Use(async (context, next) =>
            {
                // Add Header
                context.Response.Headers["Product"] = "Api for MySQL Database v1.0.0.6";

                // Call next middleware
                await next.Invoke();
            });

            // Custom Middleare
            app.Use(typeof(CustomMiddleware));

            // Configure Web API for self-host.
            var config = new HttpConfiguration();
            config.Routes.MapHttpRoute(
                name: "Api-1",
                routeTemplate: "api/{controller}/{name}",
                defaults: new { id = RouteParameter.Optional }
               );
            config.Routes.MapHttpRoute(
                name: "Api-2",
                routeTemplate: "api/{controller}/{name}/{id}",
                defaults: new { id = RouteParameter.Optional }
             );
            config.Routes.MapHttpRoute(
                name: "Api-3",
                routeTemplate: "api/{controller}/{name}/{Datefrom}/{Dateto}",
                defaults: new { id = RouteParameter.Optional }
                );
            config.Routes.MapHttpRoute(
                name: "Api-4",
                routeTemplate: " ",
                defaults: new { id = RouteParameter.Optional }
                );

            // Web Api
            app.UseWebApi(config);
        }
    }
}
