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
    
    public partial class PhieuDenBu
    {
        public PhieuDenBu()
        {
            this.ChiTietPhieuDenBu = new HashSet<ChiTietPhieuDenBu>();
        }
    
        public string SoPhieu { get; set; }
        public string MaHopDong { get; set; }
        public string MaXe { get; set; }
        public System.DateTime NgayLap { get; set; }
        public string LoiViPham { get; set; }
        public double Gia { get; set; }
    
        public virtual ChiTietHopDong ChiTietHopDong { get; set; }
        public virtual ICollection<ChiTietPhieuDenBu> ChiTietPhieuDenBu { get; set; }
    }
}