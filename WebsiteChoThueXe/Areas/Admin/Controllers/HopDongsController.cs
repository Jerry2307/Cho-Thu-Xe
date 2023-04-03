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
    public class HopDongsController : Controller
    {
        private WebChoThueXeEntities db = new WebChoThueXeEntities();

        // GET: Admin/HopDongs
        public ActionResult Index()
        {
            var hopDong = db.HopDong.Include(h => h.KhachHang);
            return View(hopDong.ToList());
        }

        // GET: Admin/HopDongs/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            HopDong hopDong = db.HopDong.Find(id);
            if (hopDong == null)
            {
                return HttpNotFound();
            }
            return View(hopDong);
        }

        // GET: Admin/HopDongs/Create
        public ActionResult Create()
        {
            ViewBag.MaKhachHang = new SelectList(db.KhachHang, "MaKhachHang", "TenKhachHang");
            return View();
        }

        // POST: Admin/HopDongs/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "MaHopDong,MaKhachHang,NgayLap,TongTien")] HopDong hopDong)
        {
            if (ModelState.IsValid)
            {
                db.HopDong.Add(hopDong);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.MaKhachHang = new SelectList(db.KhachHang, "MaKhachHang", "TenKhachHang", hopDong.MaKhachHang);
            return View(hopDong);
        }

        // GET: Admin/HopDongs/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            HopDong hopDong = db.HopDong.Find(id);
            if (hopDong == null)
            {
                return HttpNotFound();
            }
            ViewBag.MaKhachHang = new SelectList(db.KhachHang, "MaKhachHang", "TenKhachHang", hopDong.MaKhachHang);
            return View(hopDong);
        }

        // POST: Admin/HopDongs/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "MaHopDong,MaKhachHang,NgayLap,TongTien")] HopDong hopDong)
        {
            if (ModelState.IsValid)
            {
                db.Entry(hopDong).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.MaKhachHang = new SelectList(db.KhachHang, "MaKhachHang", "TenKhachHang", hopDong.MaKhachHang);
            return View(hopDong);
        }

        // GET: Admin/HopDongs/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            HopDong hopDong = db.HopDong.Find(id);
            if (hopDong == null)
            {
                return HttpNotFound();
            }
            return View(hopDong);
        }

        // POST: Admin/HopDongs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            HopDong hopDong = db.HopDong.Find(id);
            db.HopDong.Remove(hopDong);
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
