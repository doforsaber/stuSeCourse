using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Web.UI;
using AspNetMvc.QuickStart.DAL;
using AspNetMvc.QuickStart.Models;

namespace AspNetMvc.QuickStart.Controllers
{
    public class CoursesController : Controller
    {
        private EduContext db = new EduContext();
        private static readonly int PAGE_SIZE = 3;
        private static readonly int p = 2;
        // GET: Courses
        public ActionResult Index()
        {
            var courses = db.Courses as IQueryable<Course>;

            var recordCount = courses.Count();
            var pageCount = recordCount/PAGE_SIZE;
            if (recordCount%PAGE_SIZE != 0)
            {
                pageCount += 1;
            }

            courses = courses.OrderBy(m => m.No).Skip(0).Take(PAGE_SIZE);
            ViewBag.PageIndex = 0;
            ViewBag.PageCount = pageCount;

            return View(courses.ToList());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Index(string Name, int PageIndex)
        {
            var courses = db.Courses as IQueryable<Course>;
            if (!String.IsNullOrEmpty(Name))
            {
                courses = courses.Where(m => m.Name.Contains(Name));
            }

            var recordCount = courses.Count();
            var pageCount = recordCount/PAGE_SIZE;
            if (recordCount % PAGE_SIZE != 0)
            {
                pageCount += 1;
            }

            if (PageIndex >= pageCount && pageCount >= 1)
            {
                PageIndex = pageCount - 1;
            }

            courses = courses.OrderBy(m => m.No).Skip(PageIndex*PAGE_SIZE).Take(PAGE_SIZE);

            ViewBag.PageIndex = PageIndex;
            ViewBag.PageCount = pageCount;
            return View(courses.ToList());

        }

        // GET: Courses/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Course course = db.Courses.Find(id);
            if (course == null)
            {
                return HttpNotFound();
            }
            return View(course);
        }

        // GET: Courses/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Courses/Create
        // 为了防止“过多发布”攻击，请启用要绑定到的特定属性，有关 
        // 详细信息，请参阅 http://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,No,Name,Class,Credit,Desc")] Course course)
        {
            if (ModelState.IsValid)
            {
                db.Courses.Add(course);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(course);
        }

        // GET: Courses/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Course course = db.Courses.Find(id);
            if (course == null)
            {
                return HttpNotFound();
            }
            return View(course);
        }

        // POST: Courses/Edit/5
        // 为了防止“过多发布”攻击，请启用要绑定到的特定属性，有关 
        // 详细信息，请参阅 http://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,No,Name,Class,Credit,Desc")] Course course)
        {
            if (ModelState.IsValid)
            {
                db.Entry(course).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(course);
        }

        // GET: Courses/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Course course = db.Courses.Find(id);
            if (course == null)
            {
                return HttpNotFound();
            }
            return View(course);
        }

        // POST: Courses/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Course course = db.Courses.Find(id);
            db.Courses.Remove(course);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
