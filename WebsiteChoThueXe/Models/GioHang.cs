using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebsiteChoThueXe.Models
{
    public class GioHang
    {
        public string MaXe { get; set; }
        public string TenXe { get; set; }
        public string AnhXe { get; set; }
        public double DonGia { get; set; }
        public DateTime NgayBatDau { get; set; }
        public DateTime NgayKetThuc { get; set; }
        public  int SoNgay { get; set; }
        public double TongTien;
        public double ThanhTien
        {
            get
            {
                

                return DonGia * SoNgay;
            }
        }
    }
}