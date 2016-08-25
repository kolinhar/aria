using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;
using System.Web.Routing;

namespace Aria
{
    public class MvcApplication : System.Web.HttpApplication
    {
        public static string MongoLink = String.Empty;

        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            ReadConfig();
        }

        private void ReadConfig()
        {
            if (File.Exists(Server.MapPath("Config.json")))
            {
                string content = File.ReadAllText(Server.MapPath("Config.json"));
                MongoLink = Json.Decode<Config>(content).MongoLink;
            }
            else
            {
                File.WriteAllText(Server.MapPath("Config.json"), Json.Encode(new Config()));
            }
        }
    }

    class Config
    {
        public string MongoLink { get; set; }
    }


}
