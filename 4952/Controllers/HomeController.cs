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
                
                using (SqlConnection connection = new SqlConnection(ConnectionStringBS.connectionString))
                {
                    connection.Open();

                    SqlCommand command = connection.CreateCommand();
                    SqlTransaction transaction;

                    // Start a local transaction.
                    transaction = connection.BeginTransaction("AddFile");

                    // Must assign both transaction object and connection
                    // to Command object for a pending local transaction
                    command.Connection = connection;
                    command.Transaction = transaction;

                    command.CommandText = "Insert into [File] (userID, data, fileName, fileSize, fileDateCreated) VALUES (@userID, @data, @fileName, @fileSize, @fileDateCreated)";
                    command.Parameters.Add("@userID", SqlDbType.Int).Value = 2;
                    command.Parameters.Add("@data", SqlDbType.VarBinary, -1).Value = uploadedFile;
                    command.Parameters.Add("@fileName", SqlDbType.VarChar, 255).Value = model.File.FileName;
                    command.Parameters.Add("@fileSize", SqlDbType.Int).Value = uploadedFile.Length;
                    command.Parameters.Add("@fileDateCreated", SqlDbType.DateTime).Value = DateTime.Now;
                    command.ExecuteNonQuery();

                    // Attempt to commit the transaction.
                    transaction.Commit();
                }


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