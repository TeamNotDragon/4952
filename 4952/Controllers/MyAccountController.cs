using _4952.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace _4952.Controllers
{
    public class MyAccountController : Controller
    {
        // GET: MyAccount
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Register(User account)
        {
            if (ModelState.IsValid)
            {
                using (azureEntities db = new azureEntities())
                {
                    db.Users.Add(account);
                    db.SaveChanges();
                }
                ModelState.Clear();
                ViewBag.Message = "Successfully Registered";
                return RedirectToAction("Login");
            }
            return View();
        }

        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(UserAccount user)
        {
            using (azureEntities db = new azureEntities())
            {
                var usr = db.Users.Single(u => u.email == user.email && u.password == user.password);
                if (usr != null)
                {
                    Session["userID"] = usr.userID.ToString();
                    Session["email"] = usr.email.ToString();
                    return RedirectToAction("LoggedIn");
                }
                else
                {
                    ModelState.AddModelError("", "Username or Password is Wrong");
                }
            }
            return View();
        }

        public ActionResult LoggedIn()
        {
            {
                if(Session["UserID"] != null)
                {
                    return View();
                }
                else
                {
                    return RedirectToAction("Login");
                }
            }
        }

    }
}