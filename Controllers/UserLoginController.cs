using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SocialMediaWebApp.Models;

namespace SocialMediaWebApp.Controllers
{
    public class UserLoginController : Controller
    {
        DataAccessLayer dal = new DataAccessLayer();
        // GET: UserLogin
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(Users u)
        {
            if(ModelState.IsValid)
            {
                try
                {
                    int userId = dal.LoginUser(u);
                    if(userId != 0)
                    {
                        Session["UserId"] = userId;//store id for future use in homepage

                        return RedirectToAction("Index","Home");
                    }
                    else
                    {
                        ModelState.AddModelError("", "Invalid username or password");
                    }
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", "An error occurred while processing your request. Please try again later.");
                }
            }
            return View("Login",u);
        }
    }
}