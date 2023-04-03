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
    public class ChiTietHopDongsController : Controller
    {
        private WebChoThueXeEntities db = new WebChoThueXeEntities();

        // GET: Admin/ChiTietHopDongs
        public ActionResult Index()
        {
            var chiTietHopDong = db.ChiTietHopDong.Include(c => c.HopDong).Include(c => c.Xe);
            return View(chiTietHopDong.ToList());
        }

        // GET: Admin/ChiTietHopDongs/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ChiTietHopDong chiTietHopDong = db.ChiTietHopDong.Find(id);
            if (chiTietHopDong == null)
            {
                return HttpNotFound();
            }
            return View(chiTietHopDong);
        }

        // GET: Admin/ChiTietHopDongs/Create
        public ActionResult Create()
        {
            ViewBag.MaHopDong = new SelectList(db.HopDong, "MaHopDong", "MaKhachHang");
            ViewBag.MaXe = new SelectList(db.Xe, "MaXe", "MaHang");
            return View();
        }

        // POST: Admin/ChiTietHopDongs/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "MaXe,MaHopDong,NgayGiaoXe,NgayTraXe,ThanhTien,HienTrang")] ChiTietHopDong chiTietHopDong)
        {
            if (ModelState.IsValid)
            {
                db.ChiTietHopDong.Add(chiTietHopDong);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.MaHopDong = new SelectList(db.HopDong, "MaHopDong", "MaKhachHang", chiTietHopDong.MaHopDong);
            ViewBag.MaXe = new SelectList(db.Xe, "MaXe", "MaHang", chiTietHopDong.MaXe);
            return View(chiTietHopDong);
        }

        // GET: Admin/ChiTietHopDongs/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ChiTietHopDong chiTietHopDong = db.ChiTietHopDong.Find(id);
            if (chiTietHopDong == null)
            {
                return HttpNotFound();
            }
            ViewBag.MaHopDong = new SelectList(db.HopDong, "MaHopDong", "MaKhachHang", chiTietHopDong.MaHopDong);
            ViewBag.MaXe = new SelectList(db.Xe, "MaXe", "MaHang", chiTietHopDong.MaXe);
            return View(chiTietHopDong);
        }

        // POST: Admin/ChiTietHopDongs/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "MaXe,MaHopDong,NgayGiaoXe,NgayTraXe,ThanhTien,HienTrang")] ChiTietHopDong chiTietHopDong)
        {
            if (ModelState.IsValid)
            {
                db.Entry(chiTietHopDong).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.MaHopDong = new SelectList(db.HopDong, "MaHopDong", "MaKhachHang", chiTietHopDong.MaHopDong);
            ViewBag.MaXe = new SelectList(db.Xe, "MaXe", "MaHang", chiTietHopDong.MaXe);
            return View(chiTietHopDong);
        }

        // GET: Admin/ChiTietHopDongs/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ChiTietHopDong chiTietHopDong = db.ChiTietHopDong.Find(id);
            if (chiTietHopDong == null)
            {
                return HttpNotFound();
            }
            return View(chiTietHopDong);
        }

        // POST: Admin/ChiTietHopDongs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            ChiTietHopDong chiTietHopDong = db.ChiTietHopDong.Find(id);
            db.ChiTietHopDong.Remove(chiTietHopDong);
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
