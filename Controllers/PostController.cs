using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SocialMediaWebApp.Models;

namespace SocialMediaWebApp.Controllers
{
    public class PostController : Controller
    {
        DataAccessLayer dal = new DataAccessLayer();

        // GET: Post/Create
        public ActionResult Create(int userId)
        {
            ViewBag.UserId = userId;//retrive from homepage

            return View();
        }

        [HttpPost]
        public ActionResult Create(Post p, int userId)
        {
            if(ModelState.IsValid)
            {
                p.UserId = userId;

                dal.CreatePost(p);

                Session["UserId"] = userId;

                return RedirectToAction("Index","Home");
            }
            return View(p);
        }

        [HttpPost]
        public ActionResult Delete(int postId, int userId)
        {
            dal.DeletePostFromHomePage(postId, userId);
            return RedirectToAction("Index","Home");
        }
    }
}