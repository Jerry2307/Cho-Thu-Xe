using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using WebsiteChoThueXe.Models;
using EntityState = System.Data.Entity.EntityState;
namespace WebsiteChoThueXe.Areas.Admin.Controllers
{
    public class HangXesController : Controller
    {
        private WebChoThueXeEntities db = new WebChoThueXeEntities();

        // GET: Admin/HangXes
        public ActionResult Index()
        {
            return View(db.HangXe.ToList());
        }

        // GET: Admin/HangXes/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            HangXe hangXe = db.HangXe.Find(id);
            if (hangXe == null)
            {
                return HttpNotFound();
            }
            return View(hangXe);
        }

        // GET: Admin/HangXes/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Admin/HangXes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "MaHang,TenHang")] HangXe hangXe)
        {
            if (ModelState.IsValid)
            {
                db.HangXe.Add(hangXe);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(hangXe);
        }

        // GET: Admin/HangXes/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            HangXe hangXe = db.HangXe.Find(id);
            if (hangXe == null)
            {
                return HttpNotFound();
            }
            return View(hangXe);
        }

        // POST: Admin/HangXes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "MaHang,TenHang")] HangXe hangXe)
        {
            if (ModelState.IsValid)
            {
                db.Entry(hangXe).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(hangXe);
        }

        // GET: Admin/HangXes/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            HangXe hangXe = db.HangXe.Find(id);
            if (hangXe == null)
            {
                return HttpNotFound();
            }
            return View(hangXe);
        }

        // POST: Admin/HangXes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            HangXe hangXe = db.HangXe.Find(id);
            db.HangXe.Remove(hangXe);
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
