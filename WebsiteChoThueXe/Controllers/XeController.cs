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

namespace WebsiteChoThueXe.Controllers
{
    public class XeController : Controller
    {
        private WebChoThueXeEntities db = new WebChoThueXeEntities();

        // GET: Xe
        public ActionResult Index(String TenHang, String SoChoNgoi,String NamSanXuat ,String GiaBD, String GiaKT)
        {
            double bd =0.0, kt= 999999999.0;
            if (TenHang == null)
            {
                TenHang = "";
            }
            if (SoChoNgoi == null)
            {
                SoChoNgoi = "";
            }
            if (NamSanXuat == null)
            {
                NamSanXuat = "";
            }
            if (GiaBD == null || GiaBD == "")
            {
                bd = 0.0;
            }
            else
            {
                bd= double.Parse(GiaBD);

            }
            if (GiaKT == null|| GiaKT =="")
            {
                kt = 999999999;
            }
            else
            {
                kt = double.Parse(GiaKT);
            }
            var xe = db.Xe.Where(p=>p.TinhTrang == 0 && p.MaHang.Contains(TenHang) && p.NamSanXuat.ToString().Contains(NamSanXuat) && p.SoChoNgoi.ToString().Contains(SoChoNgoi) && p.GiaThue >= bd && p.GiaThue <= kt);
            return View(xe.ToList());
        }

        // GET: Xe/Details/5
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
        public ActionResult KiemTra(string MaXe, string NgayBatDau, string SoNgay)
        {
            int ngay= int.Parse(SoNgay) ;
            ngay--;
            DateTime nbd = DateTime.Parse(NgayBatDau);
            DateTime nkt;
            if (ngay == 0)
            {
                nkt = nbd;
            }
            else
            {
                nkt = nbd.AddDays(ngay);
            }
           

            DateTime ngaycuoithang = DateTime.Now;
            ngaycuoithang = ngaycuoithang.AddMonths(1);
            ngaycuoithang = ngaycuoithang.AddDays(-(ngaycuoithang.Day));
            List<int> ds = new List<int>();
            DateTime dtResult = DateTime.Now;
            dtResult = dtResult.AddMonths(0);
            dtResult = dtResult.AddDays(-(dtResult.Day));
            DateTime dautien = dtResult.AddDays(1);
            List<ChiTietHopDong> dshd = db.ChiTietHopDong.Where(p => ((p.NgayGiaoXe.Month == DateTime.Now.Month && p.NgayGiaoXe.Year == DateTime.Now.Year)
       || (p.NgayTraXe.Month == DateTime.Now.Month && p.NgayTraXe.Year == DateTime.Now.Year))&&p.MaXe== MaXe).ToList();
            foreach (var i in dshd)
            {
                for (DateTime ngaybd = i.NgayGiaoXe; ngaybd <= i.NgayTraXe; ngaybd = ngaybd.AddDays(1))
                {
                    ds.Add(ngaybd.Day);
                }

            }

            if (nbd < DateTime.Now && ngay <= 0)
            {
                Session["Ngay"] = "Ngày Phải lớn hơn ngày hiện tại";
                Session["SoNgay"] = "Số Ngày phải lớn hơn 0";
                return RedirectToAction("Details","Xe", new { id = MaXe });
            }
            if (nbd < DateTime.Now )
            {
                Session["Ngay"] = "Ngày Phải lớn hơn ngày hiện tại";
                Session["SoNgay"] = "";
                return RedirectToAction("Details", "Xe", new { id = MaXe });
            }
            if ( ngay <= -1)
            {
                Session["Ngay"] = "";
                Session["SoNgay"] = "Số Ngày phải lớn hơn 0";
                return RedirectToAction("Details", "Xe", new { id = MaXe });
            }
            for (DateTime i = nbd; i <= nkt; i = i.AddDays(1))
            {
                foreach (int kt in ds)
                {
                    if(kt == i.Day)
                    {
                        Session["Ngay"] = "Thời gian này xe đã được đặt";
                        Session["SoNgay"] = "";
                        return RedirectToAction("Details", "Xe", new { id = MaXe });
                    }
                }
            }
            Session["Ngay"] = "";
            Session["SoNgay"] = "";
            return RedirectToAction("ThemVaoGioXe", "GioHang", new {MaXe = MaXe,NgayBatDau =nbd ,NgayKetThuc =nkt,SoNgay =ngay+1 });
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
