﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace WebApplication1.Models
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class logistic_dbEntities : DbContext
    {
        public logistic_dbEntities()
            : base("name=logistic_dbEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<Area> Areas { get; set; }
        public virtual DbSet<AttemptHistory> AttemptHistories { get; set; }
        public virtual DbSet<Parcel> Parcels { get; set; }
        public virtual DbSet<ParcelType> ParcelTypes { get; set; }
        public virtual DbSet<Partner> Partners { get; set; }
        public virtual DbSet<Path> Paths { get; set; }
        public virtual DbSet<Path_Partner> Path_Partner { get; set; }
        public virtual DbSet<Path_Partner_Parcel> Path_Partner_Parcel { get; set; }
        public virtual DbSet<SecurityQuestion> SecurityQuestions { get; set; }
    }
}
