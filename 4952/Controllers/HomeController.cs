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
        azureEntities db = new azureEntities();
        static int fixedUserIDForTestingPurposesOnly = 2;

        public ActionResult Index()
        {
            var model = new FileViewModel();
            model.fileMetadataList = db.Files
                                        .Where(x => x.userID == fixedUserIDForTestingPurposesOnly).ToList()
                                        .Select(x => new FileMetadata(x)).ToList();
            return View(model);
        }

        [HttpPost]
        public ActionResult Index(FileViewModel model)
        {
            if (model.filePosted != null)
            {
                byte[] fileBytes = new byte[model.filePosted.InputStream.Length];
                model.filePosted.InputStream.Read(fileBytes, 0, fileBytes.Length);
                db.Files.Add(new Models.File()
                {
                    userID = fixedUserIDForTestingPurposesOnly,
                    data = fileBytes,
                    fileName = model.filePosted.FileName,
                    fileSize = fileBytes.Length,
                    fileDateCreated = DateTime.Now
                });
                db.SaveChanges();
            }
            model.fileMetadataList = db.Files.ToList().Select(x => new FileMetadata(x)).ToList();
            return View(model);
        }

        public ActionResult DownloadFile(int id)
        {
            Models.File file = db.Files.Find(id);
            if (file.userID != fixedUserIDForTestingPurposesOnly)
            {
                return RedirectToAction("Index");
            }
            return File(file.data, System.Net.Mime.MediaTypeNames.Application.Octet, file.fileName.Trim());
        }

        public ActionResult DeleteFile(int id) {
            Models.File file = db.Files.Find(id);
            if (file.userID == fixedUserIDForTestingPurposesOnly)
            {
                db.Files.Remove(file);
                db.SaveChanges();
            }
            return RedirectToAction("Index");
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