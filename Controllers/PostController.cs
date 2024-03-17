using System.Web.Mvc;
using SocialMedia.Models;

namespace SocialMedia.Controllers
{
    public class PostController : Controller
    {
        DataAccessLayer dal = new DataAccessLayer();

        // GET: Post/Create
        public ActionResult Create(int userId)
        {
            // Pass userId to the view
            ViewBag.UserId = userId;

            return View();
        }

        // POST: Post/Create
        [HttpPost]
        public ActionResult Create(Post post, int userId)
        {
            if (ModelState.IsValid)
            {
                // Set user ID for the post
                post.UserId = userId;

                // Create post
                dal.CreatePost(post);

                // Store userId in TempData
                TempData["UserId"] = userId;

                // Redirect to homepage
                return RedirectToAction("Index", "Home");
            }
            // If model state is not valid, return the view with errors
            return View(post);
        }

        // POST: Post/Delete
        [HttpPost]
        public ActionResult Delete(int postId, int userId)
        {
            // Ensure userId matches the userId of the post before allowing deletion
            dal.DeletePostFromHomePage(postId, userId);

            // Redirect to the homepage
            return RedirectToAction("Index", "Home");
        }

    }
}
