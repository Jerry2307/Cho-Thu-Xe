//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace WebsiteChoThueXe.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class KhachHang
    {
        public KhachHang()
        {
            this.HopDong = new HashSet<HopDong>();
        }
    
        public string MaKhachHang { get; set; }
        public string TenKhachHang { get; set; }
        public string DiaChi { get; set; }
        public string CCCD { get; set; }
        public int SoDienThoai { get; set; }
        public string SoBangLai { get; set; }
        public string MaTaiKhoan { get; set; }
    
        public virtual ICollection<HopDong> HopDong { get; set; }
        public virtual TaiKhoan TaiKhoan { get; set; }
    }
}
