﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace MasraEmas.Models
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class MasraContext : DbContext
    {
        public MasraContext()
            : base("name=MasraContext")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<EmasOwner> EmasOwners { get; set; }
        public virtual DbSet<Member> Members { get; set; }
        public virtual DbSet<Payment> Payments { get; set; }
        public virtual DbSet<Vehicle> Vehicles { get; set; }
    }
}