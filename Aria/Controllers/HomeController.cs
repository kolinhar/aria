using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Aria.Controllers
{
    public class HomeController : Controller
    {
        // GET: Default
        public ActionResult Index()
        {
            if (string.IsNullOrWhiteSpace(MvcApplication.MongoLink))
                return RedirectToAction("Index", "Config");

            ViewBag.payload = new Aria.DB.MongoDb().ReadMessages();
            return View();
        }
    }
}