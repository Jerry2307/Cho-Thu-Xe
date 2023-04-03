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
    public class XesController : Controller
    {
        private WebChoThueXeEntities db = new WebChoThueXeEntities();

        // GET: Admin/Xes
        public ActionResult Index()
        {
            var xe = db.Xe.Include(x => x.HangXe).Include(x => x.NhaCungCap);
            return View(xe.ToList());
        }

        // GET: Admin/Xes/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Xe xe = db.Xe.Find(id);
            if (xe == null)
            {
                return HttpNotFound();
            }
            return View(xe);
        }

        // GET: Admin/Xes/Create
        public ActionResult Create()
        {
            ViewBag.MaHang = new SelectList(db.HangXe, "MaHang", "TenHang");
            ViewBag.MaNhaCungCap = new SelectList(db.NhaCungCap, "MaNhaCungCap", "MaTaiKhoan");
            return View();
        }

        // POST: Admin/Xes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "MaXe,MaHang,TenXe,MauXe,GiaThue,NamSanXuat,BienSoXe,AnhXe,TinhTrang,MoTa,HienTrang,SoLanThue,MaNhaCungCap,SoChoNgoi")] Xe xe)
        {
            if (ModelState.IsValid)
            {
                db.Xe.Add(xe);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.MaHang = new SelectList(db.HangXe, "MaHang", "TenHang", xe.MaHang);
            ViewBag.MaNhaCungCap = new SelectList(db.NhaCungCap, "MaNhaCungCap", "MaTaiKhoan", xe.MaNhaCungCap);
            return View(xe);
        }

        // GET: Admin/Xes/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Xe xe = db.Xe.Find(id);
            if (xe == null)
            {
                return HttpNotFound();
            }
            ViewBag.MaHang = new SelectList(db.HangXe, "MaHang", "TenHang", xe.MaHang);
            ViewBag.MaNhaCungCap = new SelectList(db.NhaCungCap, "MaNhaCungCap", "MaTaiKhoan", xe.MaNhaCungCap);
            return View(xe);
        }

        // POST: Admin/Xes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "MaXe,MaHang,TenXe,MauXe,GiaThue,NamSanXuat,BienSoXe,AnhXe,TinhTrang,MoTa,HienTrang,SoLanThue,MaNhaCungCap,SoChoNgoi")] Xe xe)
        {
            if (ModelState.IsValid)
            {
                db.Entry(xe).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.MaHang = new SelectList(db.HangXe, "MaHang", "TenHang", xe.MaHang);
            ViewBag.MaNhaCungCap = new SelectList(db.NhaCungCap, "MaNhaCungCap", "MaTaiKhoan", xe.MaNhaCungCap);
            return View(xe);
        }

        // GET: Admin/Xes/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Xe xe = db.Xe.Find(id);
            if (xe == null)
            {
                return HttpNotFound();
            }
            return View(xe);
        }

        // POST: Admin/Xes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            Xe xe = db.Xe.Find(id);
            db.Xe.Remove(xe);
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
