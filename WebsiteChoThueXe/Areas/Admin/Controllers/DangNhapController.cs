using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebsiteChoThueXe.Models;

namespace WebsiteChoThueXe.Areas.Admin.Controllers
{
    public class DangNhapController : Controller
    {
        // GET: Admin/TaiKhoan
        private WebChoThueXeEntities db = new WebChoThueXeEntities();   
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult DangNhap()
        {
            return View();
        }
        public ActionResult KiemTra(string TaiKhoan, string MatKhau)
        {
            if (string.IsNullOrEmpty(TaiKhoan) && string.IsNullOrEmpty(MatKhau))
            {
                Session["loi1"] = "Tên Đăng Nhập  không được để trống";
                Session["loi2"] = "Mật Khẩu không được để trống";
                return RedirectToAction("DangNhap");
            }

            if (String.IsNullOrEmpty(TaiKhoan))
            {
                Session["loi2"] = "";
                Session["loi1"] = "Tên Đăng Nhập  không được để trống";
                Session["ThongBao"] = "";
                return RedirectToAction("DangNhap");
            }
            if (String.IsNullOrEmpty(MatKhau))
            {
                Session["loi1"] = "";
                Session["loi2"] = "Mật Khẩu không được để trống";
                Session["ThongBao"] = "";
                return RedirectToAction("DangNhap");
            }
            TaiKhoan tk = db.TaiKhoan.SingleOrDefault(n => n.TenDangNhap == TaiKhoan && n.MatKhau == MatKhau);
            if (tk != null)
            {
                NhaCungCap ncc = db.NhaCungCap.FirstOrDefault(p => p.MaTaiKhoan == tk.MaTaiKhoan);
                if (ncc != null)
                {
                    Session["TenNhaCungCap"] = ncc.TenNhaCungCap;
                    Session["MaNhaCungCap"] = ncc.MaNhaCungCap.Trim();
                    Session["DiaChi"] = ncc.DiaChi;
                    return RedirectToAction("Index", "TrangChuAdmin");
                }
            }
            else
            {
                Session["loi2"] = "";
                Session["loi1"] = "";
                Session["ThongBao"] = "Tên đăng nhập hoặt mật khẩu không đúng";
            }
            return RedirectToAction("DangNhap");
        }
        public ActionResult DangXuat()
        {
            Session["TaiKhoan"] = null;
            return RedirectToAction("DangNhap", "DangNhap");
        }
    }
}