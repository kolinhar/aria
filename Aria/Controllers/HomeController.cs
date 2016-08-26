using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Aria.DB;

namespace Aria.Controllers
{
    public class HomeController : Controller
    {
        // GET: Default
        public ActionResult Index()
        {
            if (string.IsNullOrWhiteSpace(MvcApplication.MongoLink))
                return RedirectToAction("Index", "Config");

            Dictionary<string, List<Message>> messages = new Dictionary<string, List<Message>>();
            //THE DAY BEFORE YESTERDAY
            messages.Add(DateTime.Now.Subtract(new TimeSpan(2, 0, 0, 0, 0)).ToShortDateString(), new List<Message>());
            messages[DateTime.Now.Subtract(new TimeSpan(2, 0, 0, 0, 0)).ToShortDateString()].AddRange(new Aria.DB.MongoDb().ReadMessages(DateTime.Now.Subtract(new TimeSpan(2, 0, 0, 0, 0))));
            //YESTERDAY
            messages.Add(DateTime.Now.Subtract(new TimeSpan(1,0,0,0,0)).ToShortDateString(), new List<Message>());
            messages[DateTime.Now.Subtract(new TimeSpan(1,0,0,0,0)).ToShortDateString()].AddRange(new Aria.DB.MongoDb().ReadMessages(DateTime.Now.Subtract(new TimeSpan(1,0,0,0,0))));
            //TODAY
            messages.Add(DateTime.Now.ToShortDateString(), new List<Message>());
            messages[DateTime.Now.ToShortDateString()].AddRange(new Aria.DB.MongoDb().ReadMessages(DateTime.Now));

            ViewBag.payload = messages;
            return View();
        }
    }
}