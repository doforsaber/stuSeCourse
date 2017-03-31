using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using AspNetMvc.QuickStart.DAL;
using AspNetMvc.QuickStart.Models;

namespace AspNetMvc.QuickStart.Controllers
{
    public class StudentsController : Controller
    {
        private static readonly int PAGE_SIZE = 3;
        private EduContext db = new EduContext();

        // GET: Students
        public ActionResult Index()
        {
            var students = db.Students as IQueryable<Student>;
            var recordCount = students.Count();
            var pageCount = GetPageCount(recordCount);

            ViewBag.PageIndex = 0;
            ViewBag.PageCount = pageCount;

            ViewBag.GenderList = GetGenderList();
            return View(getPagedDataSource(students, 0, recordCount));
        }

        // GET: Students
        public ActionResult Select(int? id)
        {
            var students = db.Students as IQueryable<Student>;
            var recordCount = students.Count();
            var pageCount = GetPageCount(recordCount);

            ViewBag.PageIndex = 0;
            ViewBag.PageCount = pageCount;

            ViewBag.GenderList = GetGenderList();
            return View(getPagedDataSource(students, 0, recordCount));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Select(string Gender, string Name, int PageIndex)
        {
            var students = db.Students as IQueryable<Student>;

            if (!String.IsNullOrEmpty(Name))
            {
                students = students.Where(m => m.Name.Contains(Name));
            }

            if (!String.IsNullOrEmpty(Gender))
            {
                students = students.Where(m => m.Gender.ToString() == Gender);
            }

            var recordCount = students.Count();
            var pageCount = GetPageCount(recordCount);
            if (PageIndex >= pageCount && pageCount >= 1)
            {
                PageIndex = pageCount - 1;
            }

            students = students.OrderBy(m => m.Name).Skip(PageIndex * PAGE_SIZE).Take(PAGE_SIZE);

            ViewBag.PageIndex = PageIndex;
            ViewBag.PageCount = pageCount;
            ViewBag.GenderList = GetGenderList();
            return View(students.ToList());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Index(string Gender, string Name, int PageIndex)
        {
            var students = db.Students as IQueryable<Student>;
          
            if (!String.IsNullOrEmpty(Name))
            {
                students = students.Where(m => m.Name.Contains(Name));
            }

            if (!String.IsNullOrEmpty(Gender))
            {
                students = students.Where(m => m.Gender.ToString() == Gender);
            }

            var recordCount = students.Count();
            var pageCount = GetPageCount(recordCount);
            if (PageIndex >= pageCount && pageCount >= 1)
            {
                PageIndex = pageCount - 1;
            }

            students = students.OrderBy(m => m.Name).Skip(PageIndex*PAGE_SIZE).Take(PAGE_SIZE);

            ViewBag.PageIndex = PageIndex;
            ViewBag.PageCount = pageCount;
            ViewBag.GenderList = GetGenderList();
            return View(students.ToList());
        }

        // GET: Students/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Student student = db.Students.Find(id);
            if (student == null)
            {
                return HttpNotFound();
            }
            return View(student);
        }

        // GET: Students/Create
        public ActionResult Create()
        {
            ViewBag.GenderList = GetGenderList();
            return View();
        }

        // POST: Students/Create
        // 为了防止“过多发布”攻击，请启用要绑定到的特定属性，有关 
        // 详细信息，请参阅 http://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,No,Name,Gender,BirthDay")] Student student)
        {
            if (ModelState.IsValid)
            {
                db.Students.Add(student);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(student);
        }

        // GET: Students/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Student student = db.Students.Find(id);
            if (student == null)
            {
                return HttpNotFound();
            }
            ViewBag.GenderList = GetGenderList();
            return View(student);
        }

        // POST: Students/Edit/5
        // 为了防止“过多发布”攻击，请启用要绑定到的特定属性，有关 
        // 详细信息，请参阅 http://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,No,Name,Gender,BirthDay")] Student student)
        {
            if (ModelState.IsValid)
            {
                db.Entry(student).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(student);
        }

        // GET: Students/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Student student = db.Students.Find(id);
            if (student == null)
            {
                return HttpNotFound();
            }
            return View(student);
        }

        // POST: Students/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Student student = db.Students.Find(id);
            db.Students.Remove(student);
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

        public List<SelectListItem> GetGenderList()
        {
            return new List<SelectListItem>()
            {
                new SelectListItem
                {
                    Text = "男",
                    Value = "1"
                },
                new SelectListItem
                {
                    Text = "女",
                    Value = "0"
                }
            };
        }

        private List<SelectListItem> getGenderListFromStudent()
        {
            var genders = db.Students.OrderBy(m => m.Gender).Select(m => m.Gender).Distinct();
            var items = new List<SelectListItem>();
            foreach (int gender in genders)
            {
                items.Add(new SelectListItem()
                {
                    Text = gender + "",
                    Value = gender + ""
                });
            }
            return items;
        }

        //-------------------------------------
        private int GetPageCount(int recordCount)
        {
            int pageCount = recordCount/PAGE_SIZE;
            if (recordCount%PAGE_SIZE != 0)
            {
                pageCount += 1;
            }
            return pageCount;
        }

        private List<Student> getPagedDataSource(IQueryable<Student> students, int pageIndex, int recordCount)
        {
            var pageCount = GetPageCount(recordCount);
            if (pageIndex >= pageCount && pageCount >= 1)
            {
                pageIndex = pageCount - 1;
            }

            return students.OrderBy(m => m.Name).Skip(pageIndex*PAGE_SIZE).Take(PAGE_SIZE).ToList();
        }
    }
}
