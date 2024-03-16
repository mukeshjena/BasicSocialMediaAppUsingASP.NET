using System;
using System.Web.Mvc;
using SocialMedia.Models;

namespace SocialMedia.Controllers
{
    public class UserLoginController : Controller
    {
        DataAccessLayer dal = new DataAccessLayer();

        // GET: UserLogin
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(Login login)
        {
            if (ModelState.IsValid)
            {
                // Construct User object from loginViewModel
                User user = new User
                {
                    Username = login.Username,
                    Password = login.Password
                };

                try
                {
                    int userId = dal.LoginUser(user);
                    if (userId != 0)
                    {
                        // Store user ID in TempData
                        TempData["UserId"] = userId;
                        // User authenticated, redirect to home page or other appropriate action
                        return RedirectToAction("Index", "Home");
                    }
                    else
                    {
                        ModelState.AddModelError("", "Invalid username or password");
                    }
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", "An error occurred while processing your request. Please try again later.");
                    // Log the exception for further investigation
                    // logger.LogError(ex, "An error occurred during login.");
                }
            }
            return View("Index", login);
        }

    }
}
