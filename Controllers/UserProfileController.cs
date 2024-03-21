using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SocialMediaWebApp.Controllers
{
    public class UserProfileController : Controller
    {
        DataAccessLayer dal = new DataAccessLayer();

        // GET: UserProfile
        public ActionResult Profile(int userId)
        {
            var userProfile = dal.GetUserProfile(userId);
            return View(userProfile);
        }
    }
}