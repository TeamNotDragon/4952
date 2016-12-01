using System;
using System.Linq;
using System.Web.Mvc;
using _4952.Models;
using System.Data;
using System.Text;
using Microsoft.AspNet.Identity;

namespace _4952.Controllers
{
    public class HomeController : Controller
    {
        azureEntities db = new azureEntities();

        public ActionResult Index(string searchString)
        {
            int id = (int)Session["userID"];
            var model = new FileViewModel();
            model.fileMetadataList = (from file in db.Files
                                      where file.userID == id
                                      select new FileMetadata()
                                      {
                                          fileID = file.FileID,
                                          fileName = file.fileName,
                                          fileSize = file.fileSize,
                                          fileDateCreated = file.fileDateCreated,
                                      }).ToList();

            if (!String.IsNullOrEmpty(searchString))
                model.fileMetadataList = model.fileMetadataList.Where(s => s.fileName.Contains(searchString)).ToList();
            return View(model);
        }

        [HttpGet]
        public ActionResult Search(string searchString)
        {
            int id = (int)Session["userID"];
            var model = new FileViewModel();
            model.fileMetadataList = (from file in db.Files
                                      where file.userID == id
                                      && file.fileName.Contains(searchString)
                                      select new FileMetadata()
                                      {
                                          fileID = file.FileID,
                                          fileName = file.fileName,
                                          fileSize = file.fileSize,
                                          fileDateCreated = file.fileDateCreated,
                                      }).ToList();
            return View(model);
        }

        [HttpPost]
        public ActionResult Index()
        {
            byte[] fileBytes = Encoding.UTF8.GetBytes(Request["fileData"]);
            db.Files.Add(new Models.File()
            {
                userID = (int)Session["userID"],
                data = fileBytes,
                fileName = Request["fileName"],
                fileSize = fileBytes.Length,
                fileDateCreated = DateTime.Now
            });
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        [HttpPost]
        public ActionResult FileSelected(string submitButton)
        {
            int selectedFile;
            if (int.TryParse(Request.Form["rGroup"], out selectedFile))
            {
                switch (submitButton)
                {
                    case "Download":
                        return DownloadFile(selectedFile);
                    case "Delete":
                        return DeleteFile(selectedFile);
                }
            }
            return RedirectToAction("Index");
        }

        public ActionResult DownloadFile(int id)
        {
            Models.File file = db.Files.Find(id);
            if (file != null && file.userID == (int)Session["userID"])
            {
                return File(file.data, System.Net.Mime.MediaTypeNames.Application.Octet, file.fileName.Trim());
            }
            return RedirectToAction("Index");
        }

        public ActionResult DeleteFile(int id)
        {
            Models.File file = db.Files.Find(id);
            if (file != null && file.userID == (int)Session["userID"])
            {
                db.Files.Remove(file);
                db.SaveChanges();
            }
            return RedirectToAction("Index");
        }

        public ActionResult Manual()
        {
            return View();
        }

        public ActionResult Credits()
        {
            return View();
        }

        public ActionResult fileBox()
        {
            return PartialView();
        }
    }
}