﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace EFDAL
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class GreenBoxEntities : DbContext
    {
        public GreenBoxEntities()
            : base("name=GreenBoxEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<aspect> aspects { get; set; }
        public virtual DbSet<capacity> capacities { get; set; }
        public virtual DbSet<dvd> dvds { get; set; }
        public virtual DbSet<genre> genres { get; set; }
        public virtual DbSet<partaker> partakers { get; set; }
        public virtual DbSet<rating> ratings { get; set; }
        public virtual DbSet<status> status { get; set; }
        public virtual DbSet<studio> studios { get; set; }
        public virtual DbSet<dvd_partaker> dvd_partakers { get; set; }
    }
}
