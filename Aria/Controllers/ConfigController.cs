using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Aria.Controllers
{
    public class ConfigController : Controller
    {
        // GET: Config
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult AddMongo(string mongo)
        {
            //DB.MongoDb.TestConnection(mongo);
            System.IO.File.WriteAllText(Server.MapPath("../Config.json"), System.Web.Helpers.Json.Encode(new Config() { MongoLink = mongo }));
            MvcApplication.MongoLink = mongo;
            return RedirectToAction("Index", "Home");
        }
    }
}