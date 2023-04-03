using DigitalStore.Controllers.MoMo;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebsiteChoThueXe.Models;

namespace WebsiteChoThueXe.Controllers
{
    public class GioHangController : Controller
    {
        // GET: GioHang
        private WebChoThueXeEntities db = new WebChoThueXeEntities();
        public ActionResult Index()
        {
            List<GioHang> giohang = Session["GioHang"] as List<GioHang>;

            ViewBag.TongTien = tongtien();
            return View(giohang);
        }
        public double tongtien()
        {
            double tongtien = 0;
            List<GioHang> giohang = Session["GioHang"] as List<GioHang>;
            if (giohang != null)
            {
                tongtien = giohang.Sum(n => n.ThanhTien);
            }
            return tongtien;
        }
        [HttpGet]
        public RedirectToRouteResult ThemVaoGioXe(string MaXe, DateTime NgayBatDau, DateTime NgayKetThuc, int SoNgay)
        {
            //if (Session["Taikhoan"] == null || Session["Taikhoan"].ToString() == "")
            //{
            //    return RedirectToAction("Dangnhap", "NguoiDung");
            //}
            if (Session["GioHang"] == null)
            {
                Session["GioHang"] = new List<GioHang>();
            }
            List<GioHang> giohang = Session["GioHang"] as List<GioHang>;
            if (giohang.FirstOrDefault(m => m.MaXe == MaXe) == null)
            {
                Xe x = db.Xe.Find(MaXe);
                GioHang newitem = new GioHang();
                newitem.MaXe = x.MaXe;
                newitem.NgayBatDau = NgayBatDau;
                newitem.NgayKetThuc = NgayKetThuc;
                newitem.SoNgay = SoNgay;
                newitem.TenXe = x.TenXe;
                newitem.AnhXe = x.AnhXe;
                newitem.DonGia = (double)x.GiaThue;
                newitem.TongTien = +newitem.ThanhTien;
                giohang.Add(newitem);

            }
            else
            {
                Session["GioHang"] = giohang;
                return RedirectToAction("Index","Xe");
            }

            Session["GioHang"] = giohang;
            return RedirectToAction("Index");

        }
        public RedirectToRouteResult XoaGioHang(string MaXe)
        {

            List<GioHang> giohang = Session["GioHang"] as List<GioHang>;
            GioHang item = giohang.FirstOrDefault(m => m.MaXe == MaXe);
            if (item != null)
            {
                giohang.Remove(item);
                Session["GioHang"] = giohang;

            }
            return RedirectToAction("Index");

        }
        public ActionResult ThanhToan(String thanhtoan)
        {

            //if (Session["Taikhoan"] == null || Session["Taikhoan"].ToString() == "")
            //{
            //    return RedirectToAction("Dangnhap", "NguoiDung");
            //}
            if (Session["Giohang"] == null)
            {
                return RedirectToAction("Index", "TrangChu");
            }
            if (thanhtoan == null)
            {

            }
            else if (thanhtoan == "0")
            {
                return RedirectToAction("LuuCSDL", new { id = 0 });
            }
            else if (thanhtoan == "1")
            {
                return RedirectToAction("ThanhToanMomo");
            }
            else if (thanhtoan == "2")
            {
                return RedirectToAction("Index", new { id = 2 });
            }
            return RedirectToAction("Index");
        }
        public ActionResult LuuCSDL(int id)
        {

            string makh = Session["MaKhachHang"].ToString();
            KhachHang kh = db.KhachHang.FirstOrDefault(p => p.MaKhachHang == makh);
            HopDong hdong = new HopDong();
            hdong.MaHopDong = "HOPD" + DateTime.Now.ToString("ddMMyyyyHHmm");
            hdong.MaKhachHang = kh.MaKhachHang;
            hdong.NgayLap = DateTime.Now;
            Double TongTien = 0.0;
            List<GioHang> giohang = Session["GioHang"] as List<GioHang>;
            foreach (GioHang hang in giohang)
            {
                TongTien = TongTien + hang.ThanhTien;
            }
            hdong.TongTien = TongTien;
            db.HopDong.Add(hdong);
            db.SaveChanges();

            //Lưu chi tiết hợp đồng
    
            ChiTietHopDong cthd = new ChiTietHopDong();
            foreach (GioHang hang in giohang)
            {
                cthd.MaHopDong = hdong.MaHopDong;
                cthd.MaXe = hang.MaXe;
                cthd.NgayGiaoXe = hang.NgayBatDau;
                cthd.NgayTraXe = hang.NgayKetThuc;
                Xe xe = db.Xe.FirstOrDefault(p => p.MaXe == hang.MaXe);
                cthd.HienTrang = xe.HienTrang;
            }
            db.ChiTietHopDong.Add(cthd);
            db.SaveChanges();
            
            //Lưu Hóa Đơn

            HoaDon hd = new HoaDon();
            hd.SoHoaDon = "HOAD" + DateTime.Now.ToString("ddMMyyyyHHmm");
            hd.MaHopDong = hdong.MaHopDong;
            hd.NgayLap = hdong.NgayLap;
            hd.TongTien = hdong.TongTien;  
            
            db.HoaDon.Add(hd);
            db.SaveChanges();
            Session["GioHang"] = null;
            //string content = System.IO.File.ReadAllText(Server.MapPath("~/content/template/neworder.html"));

            //content = content.Replace("{{CustomerName}}", hdong.KhachHang.TenKhachHang);
            //content = content.Replace("{{NgayLap}}", hd.NgayLap.ToString());
            //content = content.Replace("{{DonGia}}", hd.TongTien.ToString());
            //var toEmail = ConfigurationManager.AppSettings["ToEmailAddress"].ToString();

            //new MailHelper().SendMail("thaihuuket@gmail.com", "Đơn hàng mới từ CiaCar", content);
            //new MailHelper().SendMail(toEmail, "Đơn hàng mới từ CiaCar", content);



            return RedirectToAction("ThongBaoThanhCong");
        }
        public ActionResult ThongBaoThanhCong()
        {
            return View();
        }
            public ActionResult ThanhToanMomo()
        {

            
            string endpoint = "https://test-payment.momo.vn/gw_payment/transactionProcessor";
            //string endpoint = "https://test-payment.momo.vn/gw_payment/transactionProcessor";
            string partnerCode = "MOMOOJOI20210710";
            string accessKey = "iPXneGmrJH0G8FOP";
            string serectkey = "sFcbSGRSJjwGxwhhcEktCHWYUuTuPNDB";
            
            //test cá nhân
            //string endpoint = "https://test-payment.momo.vn/gw_payment/transactionProcessor";
            //string partnerCode = "MOMOCXIQ20220412";
            //string accessKey = "BYf1GRGlMP1l3vx9";
            //string serectkey = "PKrZxIiNbyQj54XDPr6fEw5IttBEXxIJ";

            string orderInfo = "0000"; // Thông Tin
            //đường link sau khi thanh toán xong trả về
            string returnUrl = "https://localhost:44332/GioHang/ReturnUrl";
            string notifyurl = "http://ba1adf48beba.ngrok.io/GioHang/SavePayment"; //lưu ý: notifyurl không được sử dụng localhost, có thể sử dụng ngrok để public localhost trong quá trình test
            string amount = "1000";//TongTien.ToString(); // đưa vào số tiền cần thanh toán 
            string orderid = DateTime.Now.Ticks.ToString();
            string requestId = DateTime.Now.Ticks.ToString();
            string extraData = "";

            string rawHash = "partnerCode=" +
            partnerCode + "&accessKey=" +
            accessKey + "&requestId=" +
            requestId + "&amount=" +
            amount + "&orderId=" +
            orderid + "&orderInfo=" +
            orderInfo + "&returnUrl=" +
            returnUrl + "&notifyUrl=" +
            notifyurl + "&extraData=" +
            extraData;

            MoMoSecurity crypto = new MoMoSecurity();
            //sign signature SHA256
            string signature = crypto.signSHA256(rawHash, serectkey);

            //build body json request
            JObject message = new JObject
            {
                { "partnerCode", partnerCode },
                { "accessKey", accessKey },
                { "requestId", requestId },
                { "amount", amount },
                { "orderId", orderid },
                { "orderInfo", orderInfo },
                { "returnUrl", returnUrl },
                { "notifyUrl", notifyurl },
                { "extraData", extraData },
                { "requestType", "captureMoMoWallet" },
                { "signature", signature }
            };

            string responseFromMomo = PaymentRequest.sendPaymentRequest(endpoint, message.ToString());

            JObject jmessage = JObject.Parse(responseFromMomo);
            return Redirect(jmessage.GetValue("payUrl").ToString());
        }
        public ActionResult ReturnUrl()
        {
            string param = Request.QueryString.ToString().Substring(0, Request.QueryString.ToString().IndexOf("signature") - 1);
            param = Server.UrlDecode(param);
            MoMoSecurity crypto = new MoMoSecurity();
            string serectkey = "sFcbSGRSJjwGxwhhcEktCHWYUuTuPNDB";
            string signature = crypto.signSHA256(param, serectkey);
            if (signature != Request["signature"].ToString())
            {
                ViewBag.mesage = "Lỗi Không thể Thanh Toán";
                return View();
            }
            if (!Request["errorCode"].Equals("0"))
            {
                ViewBag.mesage = "Thanh Toán Thất Bại";
                return View();
            }
            else
            {
                return RedirectToAction("LuuCSDL", new { id = 0 });
            }
            return View();
        }
    }
}