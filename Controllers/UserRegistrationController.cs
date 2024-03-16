using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SocialMedia.Models;

namespace SocialMedia.Controllers
{
    public class UserRegistrationController : Controller
    {
        DataAccessLayer dal = new DataAccessLayer();

        // GET: UserRegistration
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Register(User user)
        {
            if (ModelState.IsValid)
            {
                dal.RegisterUser(user);
                return RedirectToAction("Index", "UserLogin");
            }
            return View("Index", user);
        }
    }
}