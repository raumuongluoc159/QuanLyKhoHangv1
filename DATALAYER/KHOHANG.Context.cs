﻿//------------------------------------------------------------------------------
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
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class Entities : DbContext
    {
        public Entities()
            : base("name=Entities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<tb_CHUNGTU> tb_CHUNGTU { get; set; }
        public virtual DbSet<tb_CHUNGTUCHITIET> tb_CHUNGTUCHITIET { get; set; }
        public virtual DbSet<tb_CONGTY> tb_CONGTY { get; set; }
        public virtual DbSet<tb_DONVI> tb_DONVI { get; set; }
        public virtual DbSet<tb_DONVITINH> tb_DONVITINH { get; set; }
        public virtual DbSet<tb_HANGHOA> tb_HANGHOA { get; set; }
        public virtual DbSet<tb_NHACUNGCAP> tb_NHACUNGCAP { get; set; }
        public virtual DbSet<tb_SYS_FUNC> tb_SYS_FUNC { get; set; }
        public virtual DbSet<tb_TONKHO> tb_TONKHO { get; set; }
        public virtual DbSet<tb_XUATXU> tb_XUATXU { get; set; }
    }
}