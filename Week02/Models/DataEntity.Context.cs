﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Week02.Models
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class ShoeStoreEntities : DbContext
    {
        public ShoeStoreEntities()
            : base("name=ShoeStoreEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<CT_HoaDon> CT_HoaDon { get; set; }
        public virtual DbSet<Hoa_don> Hoa_don { get; set; }
        public virtual DbSet<Khach_hang> Khach_hang { get; set; }
        public virtual DbSet<Nhom_sp> Nhom_sp { get; set; }
        public virtual DbSet<San_pham> San_pham { get; set; }
        public virtual DbSet<sysdiagram> sysdiagrams { get; set; }
    }
}
