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
    
    public partial class ChiTietPhieuDenBu
    {
        public string SoPhieu { get; set; }
        public string SoHoaDon { get; set; }
        public double TongTien { get; set; }
    
        public virtual HoaDon HoaDon { get; set; }
        public virtual PhieuDenBu PhieuDenBu { get; set; }
    }
}
