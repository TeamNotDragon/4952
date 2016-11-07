using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using _4952.Models;

namespace _4952.Controllers
{
    public class HomeController : Controller
    {
        FileDBEntities db = new FileDBEntities();

        public ActionResult Index()
        {
            return View(db.Files.ToList());
        }
        
        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
        
        public ActionResult fileBox()
        {
            return PartialView();
        }
    }
}