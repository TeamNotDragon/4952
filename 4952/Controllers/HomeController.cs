using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using _4952.Models;
using System.Data.SqlClient;
using System.Data;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Diagnostics;

namespace _4952.Controllers
{
    public class HomeController : Controller
    {
        FileDBEntities db = new FileDBEntities();

        public ActionResult Index()
        {
            var model = new MyViewModel();
            model.Files = db.Files.ToList();
            return View(model);
        }

        [HttpPost]
        public ActionResult Index(MyViewModel model)
        {

            if (model.File != null)
            {

                byte[] uploadedFile = new byte[model.File.InputStream.Length];
                model.File.InputStream.Read(uploadedFile, 0, uploadedFile.Length);
                db.Files.Add(new Models.File()
                {
                    fileID = new Random().Next(int.MinValue, int.MaxValue),
                    userID = 1,
                    data = uploadedFile,
                    fileName = model.File.FileName,
                    fileSize = uploadedFile.Length.ToString(),
                    fileDateCreated = DateTime.Now.ToString("dd/MM/yyyy")
                });

                db.SaveChanges();

            }

            model.Files = db.Files.ToList();
            return View(model);
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