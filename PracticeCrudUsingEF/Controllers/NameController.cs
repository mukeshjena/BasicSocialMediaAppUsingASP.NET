using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PracticeEF.DbCtx;
using PracticeEF.Models;
using System.Data.Entity;

namespace PracticeEF.Controllers
{
    [Authorize]
    public class NameController : Controller
    {
        // GET: Name
        public ActionResult Names()
        {
            MukeshDb db = new MukeshDb();
            var res = db.Friends.ToList();
            List<NameModel> list = new List<NameModel>();
            foreach (var item in res)
            {
                list.Add(new NameModel()
                {
                    Id = item.Id,
                    Name = item.Name,
                });
            }
            return View(list);
        }

        public ActionResult Delete(int id)
        {
            MukeshDb db =new MukeshDb();

            var res = db.Friends.Where(m => m.Id == id).FirstOrDefault();
            if(res != null)
            {
                db.Friends.Remove(res);
                db.SaveChanges();
            }
            return RedirectToAction("Names");
        }

        /*public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(NameModel m)
        {
            if(ModelState.IsValid)
            {
                MukeshDb db = new MukeshDb();
                db.Friends.Add(new Friend
                {
                    Name = m.Name
                });
                db.SaveChanges();
            }
            return RedirectToAction("Names");
        }

        public ActionResult Edit(int id)
        {
            MukeshDb db = new MukeshDb();
            var res = db.Friends.Where(f => f.Id == id).FirstOrDefault();

            var m = new NameModel
                {
                    Id = res.Id,
                    Name = res.Name
                };

            return View(m);
        }

        [HttpPost]
        public ActionResult Edit(NameModel m)
        {
            if(ModelState.IsValid)
            {
                MukeshDb db = new MukeshDb();
                var res = db.Friends.Where(f => f.Id == m.Id).FirstOrDefault();
                if(res != null)
                {
                    res.Name = m.Name;
                    db.SaveChanges();
                }
            }
            return RedirectToAction("Names");
        }*/

        /*public ActionResult CreateOrEdit(int? id)
        {
            var m = new NameModel();
            if(id.HasValue)
            {
                MukeshDb db = new MukeshDb();
                var res = db.Friends.Where(f => f.Id == id).FirstOrDefault();
                if(res != null)
                {
                    m.Id = res.Id;
                    m.Name = res.Name;
                }
            }
            return View(m);
        }

        [HttpPost]
        public ActionResult CreateOrEdit(NameModel m)
        {
            if(ModelState.IsValid)
            {
                MukeshDb db = new MukeshDb();
                if(m.Id.HasValue)
                {
                    var res = db.Friends.Where(f => f.Id == m.Id).FirstOrDefault();
                    if(res != null)
                    {
                        res.Name = m.Name;
                    }
                }
                else
                {
                    db.Friends.Add(
                        new Friend
                        {
                            Name = m.Name
                        });
                }
                db.SaveChanges();
                return RedirectToAction("Names");
            }
            return View(m);
        }*/

        public ActionResult Edit(int id)
        {
            MukeshDb db = new MukeshDb();
            var res = db.Friends.Where(f => f.Id == id).FirstOrDefault();
            NameModel m = new NameModel();
            if(res != null)
            {
                m.Id = res.Id;
                m.Name = res.Name;
            }
            ViewBag.Name = "Edit";
            return View("Create",m);
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(NameModel m)
        {
            MukeshDb db = new MukeshDb();
            Friend f = new Friend();
            f.Id = m.Id;
            f.Name = m.Name;
            if(m.Id == 0)
            {
                db.Friends.Add(f);
                db.SaveChanges();
            }
            else
            {
                db.Entry(f).State = EntityState.Modified;
                db.SaveChanges();
            }
            return RedirectToAction("Names");
        }

        
    }
}