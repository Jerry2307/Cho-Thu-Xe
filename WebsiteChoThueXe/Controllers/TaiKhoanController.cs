using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebsiteChoThueXe.Common;
using WebsiteChoThueXe.Models;

namespace WebsiteChoThueXe.Controllers
{
    public class TaiKhoanController : Controller
    {
        private  WebChoThueXeEntities db = new WebChoThueXeEntities();
        // GET: TaiKhoan
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
             if(string.IsNullOrEmpty(TaiKhoan)&& string.IsNullOrEmpty(MatKhau))
            {
                Session["loi1"] = "Tên Đăng Nhập  không được để trống";
                Session["loi2"] = "Mật Khẩu không được để trống";
                return RedirectToAction("DangNhap");
            } 
                
            if (String.IsNullOrEmpty(TaiKhoan))
            {
                Session["loi2"] = "";
                Session["loi1"] = "Tên Đăng Nhập  không được để trống";
                return RedirectToAction("DangNhap");
            }
            if (String.IsNullOrEmpty(MatKhau))
            {
                Session["loi1"] = "";
                Session["loi2"] = "Mật Khẩu không được để trống";
                return RedirectToAction("DangNhap");
            }
            TaiKhoan tk = db.TaiKhoan.SingleOrDefault(n => n.TenDangNhap == TaiKhoan && n.MatKhau == MatKhau);
            if (tk != null)
            {
                KhachHang kh = db.KhachHang.FirstOrDefault(p => p.MaTaiKhoan == tk.MaTaiKhoan);
                if (kh != null)
                {
                    Session["TaiKhoan"] = tk;
                    Session["TenKhachHang"] = kh.TenKhachHang;
                    Session["MaKhachHang"] = kh.MaKhachHang;
                    Session["DiaChi"] = kh.DiaChi;
                    return RedirectToAction("Index", "TrangChu");
                }
            }
            else
            {
                Session["ThongBao"] = "Tên đăng nhập hoặt mật khẩu không đúng";
            }
                return View();
        }
        public ActionResult DangXuat()
        {
            Session["TaiKhoan"] = null;
            return RedirectToAction("Index", "TrangChu");
        }
        public ActionResult DangKy()
        {
            return View();
        }
        public ActionResult TaoTaiKhoan(string TaiKhoan, string MatKhau, string Email, string HoTen, string DiaChi, string SoBangLai, string CCCD, string SoDienThoai)
        {
            
            TaiKhoan tk= db.TaiKhoan.FirstOrDefault(p => p.TenDangNhap == TaiKhoan || p.Email.Trim()== Email);
            if (tk!= null)
            {
                return RedirectToAction("DangKy");
            }
            else
            {
                TaiKhoan a = new TaiKhoan();
                a.MaTaiKhoan = "TK"+DateTime.Now.ToString("ddMMyyyyHHmmss");
                a.MatKhau = MatKhau;
                a.TenDangNhap= TaiKhoan;
                a.Email = Email;
                db.TaiKhoan.Add(a);
                db.SaveChanges();
                KhachHang kh = new KhachHang();
                kh.MaKhachHang = "KH" + DateTime.Now.ToString("ddMMyyyyHHmmss");
                kh.TenKhachHang = HoTen;
                kh.DiaChi = DiaChi;
                kh.SoBangLai = SoBangLai;
                kh.CCCD = CCCD;
                kh.SoDienThoai = int.Parse(SoDienThoai);
                kh.MaTaiKhoan = a.MaTaiKhoan;
                db.KhachHang.Add(kh);
                db.SaveChanges();
                return RedirectToAction("DangNhap");
            }
        }

        public ActionResult LayLaiMatKhau(string Email)
            {



                if(Email == null)
                {
                return View();
                }    
                var TimTK = db.TaiKhoan.FirstOrDefault(p => p.Email.Trim() == Email);
                if (TimTK.Email.Trim() == Email)
                {
                    Random ran = new Random();
                    int so = ran.Next(1000, 100000);
                    string content = System.IO.File.ReadAllText(Server.MapPath("~/content/template/forgotPassword.html"));
             //       content = content.Replace("{{CustomerName}}", tendn);
                    content = content.Replace("{{MatKhau}}", so.ToString());
                    var toEmail = ConfigurationManager.AppSettings["ToEmailAddress"].ToString();
                    new MailHelper().SendMail(Email, "Mật khẩu đăng nhâp mới: ", content);

                    return RedirectToAction("DangNhap", "TaiKhoan");
                }
                else
                {
                    @ViewData["Loi2"] = "Sai Email";
                }
                return View();

            }

    }
}