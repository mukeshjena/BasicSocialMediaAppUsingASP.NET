using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SocialMediaWebApp.Controllers
{
    public class HomeController : Controller
    {
        DataAccessLayer dal = new DataAccessLayer();

        // GET: Home
        public ActionResult Index()
        {
            int userId = Session["UserId"] != null ? (int)Session["UserId"] : 0;//retrive the userid from login
            var homePagePosts = dal.GetHomePagePosts();

            ViewBag.UserId = userId;//store userid in viewbag
            return View(homePagePosts);
        }
    }
}