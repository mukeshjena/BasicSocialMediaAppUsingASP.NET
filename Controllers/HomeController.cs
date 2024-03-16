using System.Web.Mvc;
using SocialMedia.Models;

namespace SocialMedia.Controllers
{
    public class HomeController : Controller
    {
        DataAccessLayer dal = new DataAccessLayer();

        // GET: Home
        public ActionResult Index()
        {
            // Retrieve user ID from TempData
            int userId = TempData["UserId"] != null ? (int)TempData["UserId"] : 0;

            // Now fetch the home page posts
            var homePagePosts = dal.GetHomePagePosts();

            // Pass userId to the view
            ViewBag.UserId = userId;

            return View(homePagePosts);
        }
    }
}
