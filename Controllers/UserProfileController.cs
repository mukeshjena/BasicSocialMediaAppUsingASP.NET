using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SocialMedia.Controllers
{
    public class UserProfileController : Controller
    {
        DataAccessLayer dal = new DataAccessLayer();

        // GET: UserProfile
        public ActionResult Index(int userId)
        {
            var userProfileData = dal.GetUserProfile(userId);
            return View(userProfileData);
        }
    }
}