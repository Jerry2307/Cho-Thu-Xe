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
    
    public partial class NhaCungCap
    {
        public NhaCungCap()
        {
            this.Xe = new HashSet<Xe>();
        }
    
        public string MaNhaCungCap { get; set; }
        public string MaTaiKhoan { get; set; }
        public string TenNhaCungCap { get; set; }
        public string DiaChi { get; set; }
        public int SoDienThoai { get; set; }
        public string CCCD { get; set; }
        public int GioiTinh { get; set; }
        public System.DateTime NgaySinh { get; set; }
    
        public virtual TaiKhoan TaiKhoan { get; set; }
        public virtual ICollection<Xe> Xe { get; set; }
    }
}