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
    
    public partial class ChiTietThanhToan
    {
        public string SoHoaDon { get; set; }
        public string MaThanhToan { get; set; }
        public double TongTien { get; set; }
    
        public virtual PhuongThucThanhToan PhuongThucThanhToan { get; set; }
        public virtual HoaDon HoaDon { get; set; }
    }
}
