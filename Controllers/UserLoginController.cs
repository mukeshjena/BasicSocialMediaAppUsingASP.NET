using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI.WebControls;
using SocialMediaWebApp.Models;
using Login = SocialMediaWebApp.Models.Login;

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
        public ActionResult Login(Login l)
        {
            if(ModelState.IsValid)
            {
                Users user = new Users
                {
                    Username = l.Username,
                    Password = l.Password
                };
                int userId = dal.LoginUser(user);
                    if(userId != 0)
                    {
                        Session["UserId"] = userId;//store id for future use in homepage

                        return RedirectToAction("Index","Home");
                    }
            }
            return View("Login",l);
        }
    }
}