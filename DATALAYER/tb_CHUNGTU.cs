//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace DATALAYER
{
    using System;
    using System.Collections.Generic;
    
    public partial class tb_CHUNGTU
    {
        public System.Guid Khoa { get; set; }
        public string MaDV { get; set; }
        public string SCT { get; set; }
        public Nullable<System.DateTime> NGAY { get; set; }
        public string DONVI { get; set; }
        public string DiaChi { get; set; }
        public string SDT { get; set; }
        public string Email { get; set; }
        public string LoaiChungTu { get; set; }
        public string GhiChu { get; set; }
        public Nullable<int> SoLuong { get; set; }
        public string MACTY { get; set; }
        public Nullable<System.DateTime> CREATED_DATE { get; set; }
        public Nullable<int> CREATED_BY { get; set; }
        public Nullable<System.DateTime> UPDATED_DATE { get; set; }
        public Nullable<int> UPDATED_BY { get; set; }
        public Nullable<System.DateTime> DELETE_DATE { get; set; }
        public Nullable<int> DELETE_BY { get; set; }
    
        public virtual tb_DONVI tb_DONVI { get; set; }
    }
}