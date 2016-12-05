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
        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Register(User account)
        {
            if (ModelState.IsValid)
            {
                azureEntities db = new azureEntities();
                if(db.Users.Any(user => user.email == account.email))
                {
                    ViewBag.Message = "Account already exists";
                    return View();
                }

                
                db.Users.Add(account);
                db.SaveChanges();
                
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
                if (user.email == null || user.email == null)
                {
                    ModelState.AddModelError("", "Enter a username / Password");
                    return View();
                }

                var usr = db.Users.Single(u => u.email == user.email && u.password == user.password);
                if (usr != null)
                {
                    Session["userID"] = usr.userID;
                    Session["email"] = usr.email.ToString();
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    ModelState.AddModelError("", "Username or Password is Wrong");
                }
            }
            return View();
        }

        public ActionResult Logout()
        {
            Session["userID"] = null;
            Session["email"] = null;
            return RedirectToAction("Login");
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