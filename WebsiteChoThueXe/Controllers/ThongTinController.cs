using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebsiteChoThueXe.Models;

namespace WebsiteChoThueXe.Controllers
{
    public class ThongTinController : Controller
    {
        // GET: ThongTin
        WebChoThueXeEntities db = new WebChoThueXeEntities();
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult LichSuDatXe()
        {
            string mkh = (string)Session["MaKhachHang"];
            List<ChiTietHopDong> ds = db.ChiTietHopDong.Where(p=>p.HopDong.KhachHang.MaKhachHang== mkh).ToList();
            return View(ds);
        }
    }
}