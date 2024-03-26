using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SocialMediaWebApp.Models;

namespace SocialMediaWebApp.Controllers
{
    public class UserRegistrationController : Controller
    {
        DataAccessLayer dal = new DataAccessLayer();

        // GET: UserRegistration
        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Register(Users u)
        {
            if(ModelState.IsValid)
            {
                dal.RegisterUser(u);
                return RedirectToAction("Login","UserLogin");
            }
            return View();
        }
    }
}