using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using PracticeEF.DbCtx;
using PracticeEF.Models;

namespace PracticeEF.Controllers
{
    [Authorize]
    public class AuthController : Controller
    {
        [AllowAnonymous]
        public ActionResult Register()
        {
            List<string> cities = new List<string>()
            {
                "Noida",
                "Delhi",
                "Mumbai"
            };
            SelectList citySelectList = new SelectList(cities);
            ViewBag.Cities = citySelectList;
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        public ActionResult Register(UserModel r)
        {
            if (ModelState.IsValid)
            {
                MukeshDb db = new MukeshDb();
                User existingUser = db.Users.FirstOrDefault(u => u.username == r.username);
                if (existingUser != null)
                {
                    ModelState.AddModelError("username", "Username is already taken.");
                    return View(r);
                }
                existingUser = db.Users.FirstOrDefault(u => u.email == r.email);
                if (existingUser != null)
                {
                    ModelState.AddModelError("email", "Email is already taken.");
                    return View(r);
                }
                if (r.password != r.ConfirmPassword)
                {
                    ModelState.AddModelError("ConfirmPassword", "The password and confirmation password do not match.");
                    return View(r);
                }

                User newUser = new User();
                newUser.email = r.email;
                newUser.password = r.password;
                newUser.username = r.username;
                newUser.fullname = r.fullname;
                newUser.gender = r.gender;
                newUser.city = r.city;
                newUser.dateOfBirth = r.dateOfBirth;

                db.Users.Add(newUser);
                db.SaveChanges();

                return RedirectToAction("Login");
            }
            List<string> cities = new List<string>()
                {
                    "Noida",
                    "Delhi",
                    "Mumbai"
                };
            SelectList citySelectList = new SelectList(cities);
            ViewBag.Cities = citySelectList;
            return View(r);
        }

        [AllowAnonymous]
        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        [AllowAnonymous]
        public ActionResult Login(UserModel l)
        {
            MukeshDb db = new MukeshDb();
            var res = db.Users.Where(u => u.username == l.username).FirstOrDefault();
            if (res == null)
            {
                TempData["InvalidName"] = "Please Enter Valid Username";
            }
            else
            {
                if(res.username == l.username && res.password == l.password)
                {
                    FormsAuthentication.SetAuthCookie(res.username, false);
                    Session["UserId"] = res.userId;
                    return RedirectToAction("Index","Home");
                }
                else
                {
                    TempData["InvalidPass"] = "Please Enter Valid Password";
                }
            }
            return View();
        }

        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            Session.Clear();
            return RedirectToAction("Login", "Auth");
        }
    }
}