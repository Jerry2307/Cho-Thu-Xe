using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebsiteChoThueXe.Models;

namespace WebsiteChoThueXe.Areas.Admin.Controllers
{
    public class ThongKeController : Controller
    {
        private WebChoThueXeEntities db = new WebChoThueXeEntities();
        // GET: Admin/ThongKe
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult TKTheoKhachHang(string MaNCC)
        {
            if(MaNCC == null)
            {

            }
            //var xe = db.Xe.Include(x => x.HangXe).Include(x => x.NhaCungCap);
            //return View(xe.ToList());
            List<KhachHang> kh= db.KhachHang.ToList();    
            return View(kh);
        }
        public ActionResult TKTheoXe(string MaNCC)
        {
            if (MaNCC == null)
            {

            }
            //var xe = db.Xe.Include(x => x.HangXe).Include(x => x.NhaCungCap);
            //return View(xe.ToList());
            List<Xe> xe = db.Xe.ToList();
            return View(xe);
        }

    }
}